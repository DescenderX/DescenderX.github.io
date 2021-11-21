//This file is part of Tribes RPG.
//Tribes RPG server side scripts
//Written by Jason "phantom" Daley,  Matthiew "JeremyIrons" Bouchard, and more (yet undetermined)

//	Copyright (C) 2014  Jason Daley

//	This program is free software: you can redistribute it and/or modify
//	it under the terms of the GNU General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.

//	This program is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//	GNU General Public License for more details.

//	You should have received a copy of the GNU General Public License
//	along with this program.  If not, see <http://www.gnu.org/licenses/>.

//You may contact the author at beatme101@gmail.com or www.tribesrpg.org/contact.php

//This GPL does not apply to Starsiege: Tribes or any non-RPG related files included.
//Starsiege: Tribes, including the engine, retains a proprietary license forbidding resale.


//________________________________________________________________________________________________________________________________________________________
// DescX Note:
	// Biggest changes at the bottom...
	//
	// There was a big problem with getting bots to stop moving due to too many "frozen" states being ignored and/or
	// overridden. "Frozen" should mean "Frozen", period. Movement should be hard to initiate. There are many code paths
	// that were never cleaned up that cause the AI to overwrite its own behaviour. I've forced many functions to return
	// early to correct for this. By insisting that STOP means STOP, it gives an opportunity to debug bots and clear their state.
	// Otherwise, it might "look" like they're working when they aren't - e.g. the original base code allowed bots to cast by 
	// delaying how often the bot thinks. There were at least a half dozen things that could cause a bot to move while channeling,
	// and none of them were "obvious" because the think time was set so high that the problem wasn't noticeable.
	//
	// I'm mostly happy with how the bots play now, but I've got half a mind to turn this into a bigger project allowing
	// bots to free roam & attack like "fake players". It wouldn't be worth tweaking the remaining AI functions to
	// accomplish this -- I'd probably start with something like Spoonbot as a base and work up from there.
	//
	// Key changes that make the AI work better:
	//		putting #cast in Periodic(); (as opposed to being "fired" from a CastingBlade)
	//		making spell selection based on "kits" and class affinity
	//		letting the AI use #skills whenever they want

	


$AI::defaultPathType = 2; //run twoWay paths
$AIattackMode = 1;

//---------------------------------
//createAI()
//---------------------------------
function createAI(%aiName, %markerGroup, %name)
{
	dbecho($dbechoMode, "createAI(" @ %aiName @ ", " @ %markerGroup @ ", " @ %name @ ")");

	%group = nameToID( %markerGroup );
   
	if( %group == -1 || Group::objectCount(%group) == 0 )
	{
	      %spawnPos = %markerGroup;
	      %spawnRot = "0 0 0";
	}
	else
	{
		for(%i = 0; %i < Group::objectCount(%group); %i++)
		{
			%obj = Group::getObject(%group, %i);
			if(getObjectType(%obj) != "SimGroup")
				break;
		}
		%spawnMarker = Group::getObject(%group, %i);
		%spawnPos = GameBase::getPosition(%spawnMarker);
		%spawnRot = GameBase::getRotation(%spawnMarker);
	}

	%guardtype = clipTrailingNumbers(%aiName);
	%race = $BotInfo[%aiName, RACE];
	if(%race == "")
		%race = $AIRaceName[%guardtype];
	%armor = $RaceArmorType[%race];

	if( AI::spawn( %aiName, %armor, %spawnPos, %spawnRot, %name, "male2" ) != "false" )
	{
		%AiId = AI::getId(%aiName);
		ClearVariables(%AiId);
		
		storeData(%AiId, "BotInfoAiName", %aiName);
		storeData(%AiId, "RACE", %race);
		storeData(%AiId, "FAVORmode", "miss");
		storeData(%AiId, "RemortStep", 0);
		storeData(%AiId, "HasLoadedAndSpawned", True);
		storeData(%AiId, "botAttackMode", 1);
		storeData(%AiId, "tmpbotdata", "");
		storeData(%AiId, "HP", fetchData(%AiId, "MaxHP"));
		storeData(%AiId, "MANA", 1000);

		refreshHPREGEN(%AiId);
		refreshMANAREGEN(%AiId);

		storeData(%AiId, "FAVOR", $BotInfo[%aiName, FAVOR]);

		if(%group != -1)
		{
			// The order number is used for sorting waypoints, and other directives.  
			%orderNumber = 200;
         
			for(%i = 0; %i < Group::objectCount(%group); %i++)
			{
				%spawnMarker = Group::getObject(%group, %i);
				if(getObjectType(%spawnMarker) != "SimGroup")
				{
					%spawnPos = GameBase::getPosition(%spawnMarker);
           
					AI::DirectiveWaypoint( %aiName, %spawnPos, %orderNumber );
           
					%orderNumber++;
				}
			}

			AI::setAutomaticTargets(%aiName);
		}

		GameBase::startFadeIn(%AiId);
		PlaySound(SoundSpawn2, %spawnPos);
		Client::setSkin(%aiId, $RaceSkin[%race]);
	}
	else
	{
//		echo("Failure spawning bot:");
//		echo("%aiName: " @ %aiName);
//		echo("%armor: " @ %armor);
//		echo("%guardtype: " @ %guardtype);
//		echo("%spawnPos: " @ %spawnPos);
//		echo("%spawnRot: " @ %spawnRot);
//		echo("%name: " @ %name);
		return -1;
	}
}

//----------------------------------
// AI::setupAI()
//
// Called from Mission::init() which is defined in Objectives.cs (or Dm.cs for
//    deathmatch missions).  
//----------------------------------   
function AI::setupAI(%key, %team)
{
	dbecho($dbechoMode, "AI::setupAI(" @ %key @ ", " @ %team @ ")");

	//if there is no key then they don't exist yet
	if(%key == "")
	{
		%aiFound = 0;
		for( %T = 0; %T < 8; %T++ )
		{
			%groupId = nameToID("MissionGroup\\Teams\\team" @ %T @ "\\AI" );
			if( %groupId != -1 )
			{
				%teamItemCount = Group::objectCount(%groupId);
				if( %teamItemCount > 0 )
				{
					AI::initDrones(%T, %teamItemCount);
					%aiFound += %teamItemCount;
				}
			}
		}
		if( %aiFound == 0 )
			dbecho(1, "No drones exist...");
		else
			dbecho(1, %aiFound @ " drones installed..." );

		$numAi = %aiFound;
	}
	else     //respawning dead AI with original name and path
	{
		%group = nameToID("MissionGroup\\Teams\\team" @ %team @ "\\AI\\" @ %key);
		%num = Group::objectCount(%group);
		createAI(%key, %group, $BotInfo[%key, NAME]);
		%aiId = AI::getId(%key);

		GameBase::setTeam(%aiId, %team);
		AI::setVar(%key, pathType, $AI::defaultPathType);
		AI::setWeapons(%key);

		//**RPG (added because AI::onDroneKilled doesn't conserve the AI's team)
		storeData(%AiId, "botTeam", %team);
		//**
	}		
}

//------------------------------
// AI::setWeapons()
//------------------------------
function AI::setWeapons(%aiName, %loadout)
{
	dbecho($dbechoMode, "AI::setWeapons(" @ %aiName @ ")");

	%aiId = AI::getId(%aiName);

	if(%loadout == -1 || %loadout == "" || String::ICompare(%loadout, "default") == 0)
	{
		%items = $BotInfo[%aiName, ITEMS];
		if(%items == "")
			%items = $BotEquipment[clipTrailingNumbers(%aiName)];
		GiveThisStuff(%aiId, %items, False);
	}
	else {
		if ($BotEquipment[clipTrailingNumbers(%aiName)] != "")
			GiveThisStuff(%aiId, $BotEquipment[clipTrailingNumbers(%aiName)], False);
		else GiveThisStuff(%aiId, $LoadOut[%loadout], False);
	}

	HardcodeAIskills(%aiId);

	Game::refreshClientScore(%aiId);
  
	AI::SetVar(%aiName, triggerPct, 1.0 );
	AI::setVar(%aiName, iq, 100 );
	AI::setVar(%aiName, attackMode, $AIattackMode);
	AI::setAutomaticTargets( %aiName );

	ai::callbackPeriodic(%aiName, 3, AI::Periodic);

	AI::SelectBestWeapon(%aiId);	//this way the bot spawns and has a weapon in hand
}

function AI::NextWeapon(%aiId)
{
	dbecho($dbechoMode, "AI::NextWeapon(" @ %aiId @ ")");

	%item = Player::getMountedItem(%aiId, $WeaponSlot);

	if(%item == -1 || $NextWeapon[%item] == "")
		selectValidWeapon(%aiId);
	else
	{
		for(%weapon = $NextWeapon[%item]; %weapon != %item; %weapon = $NextWeapon[%weapon])
		{
			if(isSelectableWeapon(%clientId, %weapon))
			{
				%x = "";
				if(GetAccessoryVar(%weapon, $AccessoryType) == $RangedAccessoryType)
				{
					%x = GetBestRangedProj(%aiId, %weapon);
					if(%x != -1)
						storeData(%aiId, "LoadedProjectile " @ %weapon, %x);
				}

				if(%x != -1)
				{
					Player::useItem(%aiId, %weapon);
					if(Player::getMountedItem(%clientId, $WeaponSlot) == %weapon || Player::getNextMountedItem(%aiId, $WeaponSlot) == %weapon)
						break;
				}
			}
		}
	}

	AI::SetSpotDist(%aiId);
}

function AI::SelectBestWeapon(%aiId)
{
	dbecho($dbechoMode, "AI::SelectBestWeapon(" @ %aiId @ ")");

	%weapon = GetBestWeapon(%aiId);
	if(%weapon != -1)
	{
		%x = "";
		if(GetAccessoryVar(%weapon, $AccessoryType) == $RangedAccessoryType)
		{
			%x = GetBestRangedProj(%aiId, %weapon);
			if(%x != -1)
				storeData(%aiId, "LoadedProjectile " @ %weapon, %x);
		}

		if(%x != -1)
		{
			Player::useItem(%aiId, %weapon);
			AI::SetSpotDist(%aiId);
		}
	}

	return -1;
}

function AI::SetSpotDist(%aiId)
{
	dbecho($dbechoMode, "AI::SetSpotDist(" @ %aiId @ ")");

	if(fetchData(%aiId, "frozen") || fetchData(%aiId, "dumbAIflag"))
		return;

	%item = Player::getMountedItem(%aiId, $WeaponSlot);
	%range = 20;
	if(%item != -1) {
		%range = GetRange(%item);
	}
	AI::setVar(fetchData(%aiId, "BotInfoAiName"), SpotDist, %range);
	AI::setVar(fetchData(%aiId, "BotInfoAiName"), triggerPct, 1.0);
}

function AI::activelyFollow(%aiName, %curTarget, %bypass)
{
	dbecho($dbechoMode, "AI::activelyFollow(" @ %aiName @ ", " @ %curTarget @ ", " @ %bypass @ ")");

	%aiId = ai::getId(%aiName);

	if(GameBase::getTeam(%aiId) != GameBase::getTeam(%curTarget) || %bypass)
	{
		AI::newDirectiveFollow(%aiName, %curTarget, 0, 99);
	}
}

function AI::moveToAttackMarker(%name, %method)
{
	dbecho($dbechoMode, "AI::moveToAttackMarker(" @ %name @ ", " @ %method @ ")");

	%AIid = AI::getId(%name);

	if(fetchData(%aiId, "dumbAIflag"))
		return;

      %tempSet = nameToID("MissionGroup\\Teams\\team1\\AIattackMarkers");

	if(%tempSet != -1)
	{
		%num = Group::objectCount(%tempSet);

		if(%method == 0)
		{
			//pick a random marker
			%r = floor(getRandom() * %num);
		}
		else if(%method == 1)
		{
			//pick nearest marker
			%dist = 1000000;
			for(%i=0; %i<=%num-1; %i++)
			{
				%m = Group::getObject(%tempSet, %i);
				%testdist = Vector::getDistance(GameBase::getPosition(%AIid), GameBase::getPosition(%m));
				if(%testdist < %dist)
				{
					%dist = %testdist;
					%r = %i;
				}
			}
		}
	      %marker = Group::getObject(%tempSet, %r);
	
		%worldLoc = GameBase::getPosition(%marker);

		AI::newDirectiveWaypoint(%name, %worldLoc, 99);
		storeData(%aiId, "AIattackMarker", %marker);

		//echo(%name @ " IS PROCEEDING TO LOCATION " @ %worldLoc);

		return True;
	}
	return False;
}

function AI::moveSomewhere(%aiName)
{
	dbecho($dbechoMode, "AI::moveSomewhere(" @ %aiName @ ")");

	%aiId = AI::getId(%aiName);

	if(fetchData(%aiId, "dumbAIflag"))
		return;

	if(fetchData(%aiId, "AIattackMarker") != "")
	{
		%dist = Vector::getDistance(GameBase::getPosition(fetchData(%aiId, "AIattackMarker")), GameBase::getPosition(%aiId));
		if(%dist <= 25)
		{
			storeData(%aiId, "AIattackMarker", "");
		}
	}
	if(fetchData(%aiId, "AIattackMarker", ""))
	{
		%aiPos = GameBase::getPosition(%aiId);
		%minrad = 10;
		%maxrad = 100;

		%tempPos = RandomPositionXY(%minrad, %maxrad);

		%xPos = GetWord(%tempPos, 0) + GetWord(%aiPos, 0);
		%yPos = GetWord(%tempPos, 1) + GetWord(%aiPos, 1);
		%zPos = GetWord(%aiPos, 2); //doesn't matter; the bot can't go thru terrain
		
		%newPos = %xPos @ " " @ %yPos @ " " @ %zPos;

		storeData(%aiId, "AIattackMarker", "");
		AI::newDirectiveWaypoint(%aiName, %newPos, 99);

	}
}

//experimental function, which makes bot look around himself at preset angles and, whichever is the furthest, go there.
//i'm hoping this will simulate a smarter bot.

//test results: seems to work great!  hopefully it doesn't cause too much lag, but up to now... looks fine
function AI::moveToFurthest(%aiName)
{
	dbecho($dbechoMode, "AI::moveToFurthest(" @ %aiName @ ")");

	%aiId = AI::getId(%aiName);

	if(fetchData(%aiId, "dumbAIflag"))
		return;

	if(fetchData(%aiId, "AIattackMarker") != "")
	{
		%dist = Vector::getDistance(GameBase::getPosition(fetchData(%aiId, "AIattackMarker")), GameBase::getPosition(%aiId));
		if(%dist <= 25)
		{
			storeData(%aiId, "AIattackMarker", "");
		}
	}
	if(fetchData(%aiId, "AIattackMarker") == "")
	{
		%aiPos = GameBase::getPosition(%aiId);
		%aiPlayer = Client::getOwnedObject(%aiId);

		%furthest = -1;
		for(%i = 0; %i <= 6.283; %i+= 0.52)
		{
			GameBase::getLOSinfo(%aiPlayer, 1000, "0 0 " @ %i);
			%dist = Vector::getDistance(%aiPos, $los::position);
			if(%dist > %furthest && $los::position != "0 0 0" && $los::position != "")
			{
				%furthest = %dist;
				%chosenPos = $los::position;
			}
		}
		if(%furthest == -1)
		{
			//it seems the bot only sees sky, so revert to AI::moveSomewhere
			AI::moveSomewhere(%aiName);
			return;
		}

		%finalPos = %chosenPos;

		AI::newDirectiveWaypoint(%aiName, %finalPos, 99);

		//echo(%aiName @ " FOUND THE FURTHEST POINT AT LOCATION " @ %chosenPos);
	}
}


//-----------------------------------
// AI::initDrones()
//-----------------------------------
function AI::initDrones(%team, %numAi)
{
	dbecho($dbechoMode, "AI::initDrones(" @ %team @ ", " @ %numAi @ ")");

	dbecho(1, "spawning team " @ %team @ " ai...");
	for(%guard = 0; %guard < %numAi; %guard++)
	{
		//check for internal data
		%tempSet = nameToID("MissionGroup\\Teams\\team" @ %team @ "\\AI");
		%tempItem = Group::getObject(%tempSet, %guard);
		%aiName = Object::getName(%tempItem);

		%set = nameToID("MissionGroup\\Teams\\team" @ %team @ "\\AI\\" @ %aiName);
		%numPts = Group::objectCount(%set);

		if(%numPts > 0)
		{
			GatherBotInfo(%set);

			createAI(%aiName, %set, $BotInfo[%aiName, NAME]);
			%aiId = AI::getId( %aiName );
			AI::setVar( %aiName, iq,  100 );
			AI::setVar( %aiName, attackMode, $AIattackMode);
			AI::setVar( %aiName, pathType, $AI::defaultPathType);
			AI::setWeapons(%aiName);

			UpdateTeam(%aiId);

			//**RPG (added because AI::onDroneKilled doesn't conserve the AI's team)
			storeData(%aiId, "botTeam", %team);
			//**
		}
		else
			dbecho(1, "no info to spawn ai...");
	}
}


//------------------------------------------------------------------
//functions to test and move AI players.
//
//------------------------------------------------------------------

$numAI = 0;
function AI::helper(%aiName, %displayName, %commandIssuer, %loadout)
{
	dbecho($dbechoMode, "AI::helper(" @ %aiName @ ", " @ %displayName @ ", " @ %commandIssuer @ ")");

	if(GetWord(%commandIssuer, 0) == "TempSpawn")
	{
		//the %commandIssuer is a data string
		%spawnPos = GetWord(%commandIssuer, 1) @ " " @ GetWord(%commandIssuer, 2) @ " " @ GetWord(%commandIssuer, 3);
	}
	else if(GetWord(%commandIssuer, 0) == "MarkerSpawn")
	{
		//the %commandIssuer is a marker
		%spawnPos = GameBase::getPosition(GetWord(%commandIssuer, 1));
	}
	else if(GetWord(%commandIssuer, 0) == "SpawnPoint")
	{
		//the %commandIssuer is a Spawn Point
		//we must now figure out a position around this Spawn Point

		%spawnpoint = GetWord(%commandIssuer, 1);

		%info = Object::getName(%spawnpoint);

		%minrad = GetWord(%info, 1);
		%maxrad = GetWord(%info, 2);

		%spawnPointPos = GameBase::getPosition(%spawnpoint);
		%tempPos = RandomPositionXY(%minrad, %maxrad);
		%xPos = GetWord(%tempPos, 0) + GetWord(%spawnPointPos, 0);
		%yPos = GetWord(%tempPos, 1) + GetWord(%spawnPointPos, 1);
		%zPos = GetWord(%spawnPointPos, 2);

		%spawnPos = %xPos @ " " @ %yPos @ " " @ %zPos;
	}

	%n = getAInumber();

	%newName = %aiName @ %n;
	if(%aiName == %displayName) {
		
		%displayName = $NameForRace[%aiName] @ %newName;
	}
	$numAI++;
	SpawnAI(%newName, %displayName, %spawnPos, %commandIssuer, %loadout);

	setAInumber(%newName, %n);

	return %newName;
}
function SpawnAI(%newName, %displayName, %aiSpawnPos, %commandIssuer, %loadout)
{
	dbecho($dbechoMode, "SpawnAI(" @ %newName @ ", " @ %displayName @ ", " @ %aiSpawnPos @ ", " @ %commandIssuer @ ")");

	%retval = createAI(%newName, %aiSpawnPos, %displayName);

	if(%retval != -1)
	{
		%aiId = AI::getId( %newName );
		AI::setVar( %newName,  iq,  100 );
		AI::setVar( %newName,  attackMode, $AIattackMode);
		AI::setVar( %newName,  pathType, $AI::defaultPathType);
		//AI::SetVar( %newName,  seekOff, 1);
		AI::setAutomaticTargets( %newName );

		if(GetWord(%commandIssuer, 0) == "TempSpawn")
		{
			//the %commandIssuer is a data string
			storeData(%aiId, "SpawnBotInfo", %commandIssuer);
			%team = GetWord(%commandIssuer, 4);
			GameBase::setTeam(%aiId, %team);

			AI::SetVar(%newName, spotDist, $AIspotDist);
		}
		else if(GetWord(%commandIssuer, 0) == "MarkerSpawn")
		{
			//the %commandIssuer is a marker
			storeData(%aiId, "SpawnBotInfo", %commandIssuer);
			%team = GetWord(%commandIssuer, 2);
			if(%team == "") %team = GameBase::getMapName(GetWord(%commandIssuer, 1));
			if(%team == "") %team = 0;
			GameBase::setTeam(%aiId, %team);

			AI::SetVar(%newName, spotDist, $AIspotDist);
		}
		else if(GetWord(%commandIssuer, 0) == "SpawnPoint")
		{
			//the %commandIssuer is a spawn crystal
			storeData(%aiId, "SpawnBotInfo", %commandIssuer);

			$numAIperSpawnPoint[GetWord(%commandIssuer, 1)]++;
			UpdateTeam(%aiId);

			AI::SetVar(%newName, spotDist, $AIspotDist);
		}

		AI::setWeapons(%newName, %loadout);

		return ( %newName );
	}
	else
	{
		return -1;
	}
}

//
//This function will move an AI player to the position of an object
//that the players LOS is hitting(terrain included). Must be within 50 units.
//
//
function AI::moveToLOS(%aiName, %commandIssuer) 
{
	dbecho($dbechoMode, "AI::moveToLos(" @ %aiName @ ", " @ %commandIssuer @ ")");

	%issuerRot = GameBase::getRotation(%commandIssuer);
	%playerObj = Client::getOwnedObject(%commandIssuer);
	%playerPos = GameBase::getPosition(%commandIssuer);
      
	//check within max dist
	if(GameBase::getLOSInfo(%playerObj, 100, %issuerRot))
	{ 
		%newIssuedVec = $LOS::position;
		%distance = Vector::getDistance(%playerPos, %newIssuedVec);
		dbecho(2, "Command accepted, AI player(s) moving....");
		dbecho(2, "distance to LOS: " @ %distance);
		AI::newDirectiveWaypoint( %aiName, %newIssuedVec, 99 );
	}
	else
		dbecho(2, "Distance too far.");

	dbecho(2, "LOS point: " @ $LOS::position);
}

//This function will move an AI player to a position directly in front of
//the player passed, at a distance that is specified.
function AI::moveAhead(%aiName, %commandIssuer, %distance) 
{
	dbecho($dbechoMode, "AI::moveAhead(" @ %aiName @ ", " @ %commandIssuer @ ", " @ %distance @ ")");

	%issuerRot = GameBase::getRotation(%commandIssuer);
	%commPos  = GameBase::getPosition(%commandIssuer);
	dbecho(2, "Commanders Position: " @ %commPos);

	//get commanders x and y positions
	%comm_x = getWord(%commPos, 0);
	%comm_y = getWord(%commPos, 1);

	//get offset x and y positions
	%offSetPos = Vector::getFromRot(%issuerRot, %distance);
	%off_x = getWord(%offSetPos, 0);
	%off_y = getWord(%offSetPos, 1);

	//calc new position
	%new_x = %comm_x + %off_x;
	%new_y = %comm_y + %off_y;
	%newPos = %new_x  @ " " @ %new_y @ " 0";

	//move AI player
	dbecho(2, "AI moving to " @ %newPos);
	AI::newDirectiveWaypoint(%aiName, %newPos, 99);
}  

//
// OK, this is the complete command callback - issued for any command sent
//    to an AI. 
//
function AI::onCommand ( %name, %commander, %command, %waypoint, %targetId, %cmdText, %cmdStatus, %cmdSequence)
{
	dbecho($dbechoMode, "AI::onCommand(" @ %name @ ", " @ %commander @ ", " @ %command @ ", " @ %waypoint @ ", " @ %targetId @ ", " @ %cmdText @ ", " @ %cmdStatus @ ", " @ %cmdSequence @ ")");

	%aiId = AI::getId( %name );
	%T = GameBase::getTeam( %aiId );
	%groupId = nameToID("MissionGroup\\Teams\\team" @ %T @ "\\AI\\" @ %name ); 
	%nodeCount = Group::objectCount( %groupId );
	dbecho(2, "checking drone information...." @ " number of nodes: " @ %nodeCount);
	dbecho(2, "AI id: " @ %aiId @ " groupId: " @ %groupId);
   
	if($SinglePlayer) // || %nodeCount == 1
	{
		if( %command == 2 || %command == 1 )
		{
			// must convert waypoint location into world location.  waypoint location
			//    is given in range [0-1023, 0-1023].  
			%worldLoc = WaypointToWorld ( %waypoint );
			AI::newDirectiveWaypoint( %name, %worldLoc, 99 );
			dbecho ( 2, %name @ " IS PROCEEDING TO LOCATION " @ %worldLoc );
		}
		dbecho( 2, "AI::OnCommand() issued to  " @ %name @ "  with parameters: " );
		dbecho( 3, "Cmdr:        " @ %commander );
		dbecho( 3, "Command:     " @ %command );
		dbecho( 3, "Waypoint:    " @ %waypoint );
		dbecho( 3, "TargetId:    " @ %targetId );
		dbecho( 3, "cmdText:     " @ %cmdText );
		dbecho( 3, "cmdStatus:   " @ %cmdStatus );
		dbecho( 3, "cmdSequence: " @ %cmdSequence );
	}
	else
		return;   
}

// Play the given wave file FROM %source to %DEST.  The wave name is JUST the basic wave
// name without voice base info (which it will grab for you from the source clientId).
// Basically does some string fiddling for you.
//
// Example:
//    Ai::soundHelper( 2051, 2049, cheer3 );
//
function Ai::soundHelper( %sourceId, %destId, %waveFileName )
{
	dbecho($dbechoMode, "Ai::soundHelper(" @ %sourceId @ ", " @ %destId @ ", " @ %waveFileName @ ")");

	%wName = strcat( "~w", Client::getVoiceBase( %sourceId ) );
	%wName = strcat( %wName, ".w" );
	%wName = strcat( %wName, %waveFileName );
	%wName = strcat( %wName, ".wav" );

	dbecho( 2, "Trying to play " @ %wName );

	Client::sendMessage( %destId, 0, %wName );
}


function AI::onPeriodic( %aiName )
{
	dbecho($dbechoMode, "AI::onPeriodic(" @ %aiName @ ")");
	//AI::Periodic(%aiName);			// apparently this used to be commented out, lol?
	//echo("onPeriodic() called with " @ %aiName);
}


function AI::onDroneKilled(%aiName)
{
	dbecho($dbechoMode, "AI::onDroneKilled(" @ %aiName @ ")");

	if(!$SinglePlayer )
	{
	      %aiId = AI::getId(%aiName);

      	%team = fetchData(%aiId, "botTeam");
		storeData(%aiId, "botTeam", "");
		$aiNumTable[$tmpbotn[%aiName]] = "";
		$tmpbotn[%aiName] = "";

		if(fetchData(%aiId, "SpawnBotInfo") != "")
		{
			if(GetWord(fetchData(%aiId, "SpawnBotInfo"), 0) == "SpawnPoint")
			{
				//this bot originally spawned from a crystal
				$numAIperSpawnPoint[GetWord(fetchData(%aiId, "SpawnBotInfo"), 1)]--;
			}
			storeData(%aiId, "SpawnBotInfo", "");
			storeData(%aiId, "AIattackMarker", "");

			//pet stuff
			$PetList = RemoveFromCommaList($PetList, %aiId);
			%petowner = fetchData(%aiId, "petowner");
			storeData(%petowner, "PersonalPetList", RemoveFromCommaList(fetchData(%petowner, "PersonalPetList"), %aiId));
			Client::sendMessage(%petowner, $MsgRed, Client::getName(%aiId) @ " was slain!");
			storeData(%aiId, "petowner", "");
			
			if($possessedBy[%aiId] != "") {
				rpg::RevertPossession($possessedBy[%aiId]);
			}
			
			//botgroup stuff
			%b = AI::IsInWhichBotGroup(%aiId);
			if(%b != -1)
				AI::RemoveBotFromBotGroup(%aiId, %b);
		}
		else
		{
			schedule("AI::setupAI(" @ %aiName @ ", " @ %team @ ");", 60);
	      }
	}
	else
	{
		// just in case:
		dbecho( 2, "Non training callback called from Training" );
	}
}

//These will only be invoked if the target is REALLY close to the bot (since the SpotDist is only the range of the
//weapon).  This means that if the bot ever gets close enough to engage in battle, he will try his best to continue
//the fight by following the target.  Once the target is lost or dies, directive 99 will be cancelled and directive
//99 will take over (regular walking, formations etc)
function AI::onTargetLOSAcquired(%aiName, %idNum)
{
	dbecho($dbechoMode, "AI::onTargetLOSAcquired(" @ %aiName @ ", " @ %idNum @ ")");

	%aiId = AI::getId(%aiName);

	if(fetchData(%aiId, "SpawnBotInfo") != "" && !fetchData(%aiId, "dumbAIflag"))
		AI::newDirectiveFollow(%aiName, %idNum, 0, 99);
}

function AI::onTargetLOSLost(%aiName, %idNum)
{
	dbecho($dbechoMode, "AI::onTargetLOSLost(" @ %aiName @ ", " @ %idNum @ ")");

	%aiId = AI::getId(%aiName);

	if(fetchData(%aiId, "SpawnBotInfo") != "" && !fetchData(%aiId, "dumbAIflag"))
		AI::newDirectiveRemove(%aiName, 99);
}

function AI::onTargetLOSRegained(%aiName, %idNum)
{
	dbecho($dbechoMode, "AI::onTargetLOSRegained(" @ %aiName @ ", " @ %idNum @ ")");

	%aiId = AI::getId(%aiName);

	if(fetchData(%aiId, "SpawnBotInfo") != "" && !fetchData(%aiId, "dumbAIflag"))
		AI::newDirectiveFollow(%aiName, %idNum, 0, 99);
}

function AI::onTargetDied(%aiName, %idNum)
{
	dbecho($dbechoMode, "AI::onTargetDied(" @ %aiName @ ", " @ %idNum @ ")");

	%aiId = AI::getId(%aiName);

	if(fetchData(%aiId, "SpawnBotInfo") != "" && !fetchData(%aiId, "dumbAIflag"))
		AI::newDirectiveRemove(%aiName, 99);
}                                 

function AI::sayLater(%clientId, %guardId, %message, %look)
{
	dbecho($dbechoMode, "AI::sayLater(" @ %clientId @ ", " @ %guardId @ ", " @ %message @ ", " @ %look @ ")");

	%name = Client::getName(%clientId);

	Client::sendMessage(%clientId, $MsgBeige, $BotInfo[%guardId.name, NAME] @ " tells you, \"" @ %message @ "\"");
}

function getAInumber()
{
	dbecho($dbechoMode, "getAInumber()");

	for(%i = 0; %i <= 5000; %i++)
	{
		if($aiNumTable[%i] == "")
		{
			return %i;
		}
	}
}
function setAInumber(%aiName, %n)
{
	dbecho($dbechoMode, "setAInumber(" @ %aiName @ ", " @ %n @ ")");

	$aiNumTable[%n] = True;
	$tmpbotn[%aiName] = %n;
}

//_____________________________________________________________________________________________________________
function AI::SelectMovement(%aiName)
{
	dbecho($dbechoMode, "AI::SelectMovement(" @ %aiName @ ")");

	%aiId = AI::getId(%aiName);
	
	%isCastingNow 	= fetchData(%aiId, "SpellCastStep");
	if(fetchData(%aiId, "dumbAIflag") || fetchData(%aiId, "frozen") || fetchData(%aiId, "noBotSniff") ||
		fetchData(%aiId, "SpawnBotInfo") == "" || %isCastingNow == 1) {
			return;
	}
	
	
	if(fetchData(%aiId, "botAttackMode") == 1)
	{
		//Regular walk

		if(IsInArenaDueler(%aiId) || IsInRoster(%aiId))
			%r = OddsAre(1);
		else
			%r = OddsAre(2);
		
		if(%r) {
			if(OddsAre(16)) {
				%s = RandomRaceSound(fetchData(%aiId, "RACE"), RandomWait);
				if(%s != "NoSound")
					PlaySound(%s, GameBase::getPosition(%aiId));
			}
			AI::moveToFurthest(%aiName);
		}
	}
	else if(fetchData(%aiId, "botAttackMode") == 2) {
		%followId = fetchData(%aiId, "tmpbotdata");
		if(%followId != %aiId)
			AI::newDirectiveFollow(%aiName, %followId, 0, 99);
	}
	else if(fetchData(%aiId, "botAttackMode") == 3) {
		AI::newDirectiveWaypoint(%aiName, fetchData(%aiId, "tmpbotdata"), 99);
	}
	else if(fetchData(%aiId, "botAttackMode") == 4)
	{
		//BotGroup formation

		%a = AI::IsInWhichBotGroup(%aiId);
		%g = $tmpBotGroup[%a];

		//NOTE: can't make the bots follow a random bot in the group because at one point or another,
		//the bots will pick a follow combination which will NOT involve the team leader, leaving the
		//team leader alone.
		//This new method involves a North East oriented line.

		for(%i = 1; (%b = GetWord(%g, %i)) != -1; %i++)
		{
			if(%b == %aiId)
				%n = %i-1;
		}

		%followId = GetWord(%g, %n);

		if(!fetchData(%aiId, "frozen"))
		{
			if(%followId != %aiId)
				AI::newDirectiveFollow(%aiName, %followId, 0, 99);
			else
				AI::moveToFurthest(%aiName);					//team leader gets to move.
		}

	}
}

function HardcodeAIskills(%aiId) {
	dbecho($dbechoMode, "HardcodeAIskills(" @ %aiId @ ")");

	SetAllSkills(%aiId, 0);
	%lvl = fetchData(%aiId, "LVL");
	for(%i = 1; %i <= GetNumSkills(); %i++) {
		%mult = GetSkillMultiplier(%aiId, %i);
		if(%mult > 1.0) {
			%mult = 1.0;
		}
		$PlayerSkill[%aiId, %i] = GetCurrentSkillCap(%aiId) * %mult;
		//echo(%i @ " set to " @ $PlayerSkill[%aiId, %i]);
	}
	$PlayerSkill[%aiId, $SkillVitality] = GetCurrentSkillCap(%aiId) / 4;
}

//------ BotGroup stuff ---------------------------------

function AI::IsInWhichBotGroup(%aiId)
{
	dbecho($dbechoMode, "AI::IsInWhichBotGroup(" @ %aiId @ ")");

	for(%i = 0; (%a = GetWord($BotGroups, %i)) != -1; %i++)
	{
		for(%j = 0; (%b = GetWord($tmpBotGroup[%a], %j)) != -1; %j++)
		{
			if(%b == %aiId)
				return %a;
		}
	}
	return -1;
}

function AI::CreateBotGroup(%group)
{
	dbecho($dbechoMode, "AI::CreateBotGroup(" @ %group @ ")");

	$BotGroups = $BotGroups @ %group @ " ";
	$tmpBotGroup[%group] = "";
}

function AI::DiscardBotGroup(%group)
{
	dbecho($dbechoMode, "AI::DiscardBotGroup(" @ %group @ ")");

	for(%i = 0; (%a = GetWord($tmpBotGroup[%group], %i)) != -1; %i++)
		storeData(%a, "botAttackMode", 1);

	%nstuff = " ";
	for(%i = 0; GetWord($BotGroups, %i) != -1; %i++) {
		%w = GetWord($BotGroups, %i);
		%nstuff = %nstuff @ %w @ " ";
	}
	$BotGroups = %nstuff;
	$BotGroups = String::replace($BotGroups, " " @ %group @ " ", " ");
	$tmpBotGroup[%group] = "";
}

function AI::CountBotGroupMembers(%group)
{
	dbecho($dbechoMode, "AI::CountBotGroupMembers(" @ %group @ ")");

	for(%i = 0; (%a = GetWord($tmpBotGroup[%group], %i)) != -1; %i++){}
	return %i;
}

function AI::IsInBotGroup(%aiId, %group)
{
	dbecho($dbechoMode, "AI::IsInBotGroup(" @ %aiId @ ", " @ %group @ ")");

	for(%i = 0; (%a = GetWord($tmpBotGroup[%group], %i)) != -1; %i++)
	{
		if(%aiId == %a)
			return True;
	}
	return False;
}

function AI::BotGroupExists(%group)
{
	dbecho($dbechoMode, "AI::BotGroupExists(" @ %group @ ")");

	for(%i = 0; (%a = GetWord($BotGroups, %i)) != -1; %i++)
	{
		if(%a == %group)
			return True;
	}
	return False;
}

function AI::RemoveBotFromBotGroup(%aiId, %group)
{
	dbecho($dbechoMode, "AI::RemoveBotFromGroup(" @ %aiId @ ", " @ %group @ ")");

	$tmpBotGroup[%group] = String::Replace($tmpBotGroup[%group], %aiId @ " ", "");
	storeData(%aiId, "botAttackMode", 1);
}

function AI::AddBotToBotGroup(%aiId, %group)
{
	dbecho($dbechoMode, "AI::AddBotToBotGroup(" @ %aiId @ ", " @ %group @ ")");

	$tmpBotGroup[%group] = $tmpBotGroup[%group] @ %aiId @ " ";
	storeData(%aiId, "botAttackMode", 4);
}

//------ remastered directives ------------------------------

function AI::newDirectiveFollow(%aiName, %idNum, %rad, %directive)
{
	dbecho($dbechoMode, "AI::newDirectiveFollow(" @ %aiName @ ", " @ %idNum @ ", " @ %rad @ ", " @ %directive @ ")");

	%aiId = AI::getId(%aiName);
	AI::newDirectiveRemove(%aiName, %directive);

	$aidirectiveTable[%aiId, %directive] = "follow";
	AI::directiveFollow(%aiName, %idNum, %rad, %directive);
}

function AI::newDirectiveWaypoint(%aiName, %pos, %directive)
{
	dbecho($dbechoMode, "AI::newDirectiveWaypoint(" @ %aiName @ ", " @ %pos @ ", " @ %directive @ ")");

	%aiId = AI::getId(%aiName);
	AI::newDirectiveRemove(%aiName, %directive);

	$aidirectiveTable[%aiId, %directive] = "waypoint";
	AI::directiveWaypoint(%aiName, %pos, %directive);
}

function AI::newDirectiveRemove(%aiName, %directive)
{
	dbecho($dbechoMode, "AI::newDirectiveRemove(" @ %aiName @ ", " @ %directive @ ")");

	%aiId = AI::getId(%aiName);
	if($aidirectiveTable[%aiId, %directive] != "")
	{
		AI::directiveRemove(%aiName, %directive);
		$aidirectiveTable[%aiId, %directive] = "";
	}
}

//------- TownBot stuff ----------------------------------------

function InitTownBots(%reinit)
{
	dbecho($dbechoMode, "InitTownBots()");

	for(%i = 0; (%id = GetWord($TownBotList, %i)) != -1; %i++)
		deleteObject(%id);
		
	$TownBotList = "";

	%group = nameToId("MissionGroup/TownBots");
	if(%group != -1) {
		%cnt = Group::objectCount(%group);
		//echo(%cnt);
		for(%i = 0; %i <= %cnt - 1; %i++)
		{
			%object = Group::getObject(%group, %i);
			%name = Object::getName(%object);
			if(getObjectType(%object) == "SimGroup")
			{
				%marker = GatherBotInfo(%object);
			}
			if(rpg::IsTheWorldEnding()) {
				if($BotInfo[%name, NAME] != "Trinket Vendor" && $BotInfo[%name, NAME] != "Soulbroker") {
					if($BotInfo[%name, NAME] != "Sealbroker") 
						continue;
					if(string::findSubStr($BrokersRemaining,%name) == -1) 
						continue;
				}
			}
			if(%object.useDIS == 1)
				%townbot = newObject("", InteriorShape, $BotInfo[%name, RACE] @ ".dis");
			else
				%townbot = newObject("", "StaticShape", $BotInfo[%name, RACE] @ "TownBot", true);
			//%townbot = newObject("", "StaticShape", $BotInfo[%name, RACE] @ "TownBot", true);
			addToSet("MissionCleanup", %townbot);
			GameBase::setMapName(%townbot, $BotInfo[%name, NAME]);
			GameBase::setPosition(%townbot, GameBase::getPosition(%marker));
			
			if(rpg::IsTheWorldEnding()) {
				%ground = rpg::TraceForGround(%townbot, -1000);				
				if(%ground != false)
					GameBase::setPosition(%townbot, %ground);
			}
			GameBase::setRotation(%townbot, GameBase::getRotation(%marker));
			GameBase::setTeam(%townbot, $BotInfo[%name, TEAM]);
			GameBase::playSequence(%townbot, 0, "root");	//thanks Adger!!			
			%townbot.name = %name;
			$TownBotList = $TownBotList @ %townbot @ " ";
		}
	}
	
	if(%reinit == "") {
		// Group weather devices
		$WeatherDeviceObjects = Array::New("", "WeatherDeviceObjects");
		%group = nameToId("MissionGroup/WeatherDevices");
		if(%group != -1) {
			%cnt = Group::objectCount(%group);
			//echo(%cnt);
			for(%i = 0; %i <= %cnt - 1; %i++)
			{
				%object = Group::getObject(%group, %i);
				%name 	= Object::getName(%object);
				if(getObjectType(%object) == "SimGroup")
					%marker = GatherBotInfo(%object);
				%index = String::replace($BotInfo[%name, CUE, 1], "WeatherDevice", "");
				if(Array::Get($WeatherDevices @ %index, 0) < 0)
					continue;		// Weather devices only go "negative" after being destroyed.
					
				if(%object.useDIS == 1)
					%townbot = newObject("", InteriorShape, $BotInfo[%name, RACE] @ ".dis");
				else
					%townbot = newObject("", "StaticShape", $BotInfo[%name, RACE] @ "TownBot", true);
				addToSet("MissionCleanup", %townbot);
				GameBase::setMapName(%townbot, $BotInfo[%name, NAME]);
				GameBase::setPosition(%townbot, GameBase::getPosition(%marker));
				GameBase::setRotation(%townbot, GameBase::getRotation(%marker));
				GameBase::setTeam(%townbot, $BotInfo[%name, TEAM]);
				GameBase::playSequence(%townbot, 0, "root");	//thanks Adger!!			
				%townbot.name = %name;
				Array::Insert($WeatherDeviceObjects, %townbot, %index);
			}
		}
	}
}

function newRotateTownBot(%client, %object, %clientPos, %botPos){	%dist = vector::getDistance(%clientPos, %botPos);	%rot = Vector::getRotation(Vector::normalize(Vector::sub(%clientPos, %botPos)));	%rot = "0 -0 "@GetWord(%rot, 2);	if(%dist < 1.57)	{//Prevent us from trapping the player		player::applyimpulse(%client, vector::getfromrot(%rot, 30, 12));		schedule("newRotateTownBot("@%client@", "@%object@", gamebase::getposition("@%client@"), gamebase::getposition("@%object@"));",0.3);		return;	}	GameBase::setRotation(%object, %rot);}

function GatherBotInfo(%group)
{
	dbecho($dbechoMode, "GatherBotInfo(" @ %group @ ")");

	%biggestn = 0;
	%aiName = Object::getName(%group);

	%count = Group::objectCount(%group);
	for(%i = 0; %i <= %count-1; %i++)
	{
		%object = Group::getObject(%group, %i);
		if(getObjectType(%object) == "SimGroup")
		{
			%system = Object::getName(%object);
			%type = GetWord(%system, 0);
			%info = String::getSubStr(%system, String::len(%type)+1, 9999);

			%type2 = clipTrailingNumbers(%type);
			%n = floor(String::getSubStr(%type, String::len(%type2), 9999));

			if(%type == "NAME")
				$BotInfo[%aiName, NAME] = %info;
			else if(%type == "LVL" || %type == "LEVEL")
				$BotInfo[%aiName, LVL] = %info;
			else if(%type == "RACE")
				$BotInfo[%aiName, RACE] = %info;
			else if(%type == "NEED")
				$BotInfo[%aiName, NEED] = %info;
			else if(%type == "TAKE")
				$BotInfo[%aiName, TAKE] = %info;
			else if(%type == "GIVE")
				$BotInfo[%aiName, GIVE] = %info;
			else if(%type == "SHOP")
				$BotInfo[%aiName, SHOP] = %info;
			else if(%type == "ITEMS")
				$BotInfo[%aiName, ITEMS] = %info;
			else if(%type == "CSAY")
				$BotInfo[%aiName, CSAY] = %info;
			else if(%type == "LSAY")
				$BotInfo[%aiName, LSAY] = %info;
			else if(%type == "FAVOR")
				$BotInfo[%aiName, FAVOR] = %info;
			else if(%type == "SIMGROUP")
				$BotInfo[%aiName, SIMGROUP] = %info;
			else if(%type2 == "SAY")
				$BotInfo[%aiName, SAY, %n] = %info;
			else if(%type2 == "CUE")
				$BotInfo[%aiName, CUE, %n] = %info;
			else if(%type2 == "NSAY")
				$BotInfo[%aiName, NSAY, %n] = %info;
			else if(%type2 == "NCUE")
				$BotInfo[%aiName, NCUE, %n] = %info;

			if(%n > %biggestn)
				%biggestn = %n;
		}
		else
			%marker = %object;
	}
	$BotInfo[%aiName, SAY, %biggestn+1] = "";
	$BotInfo[%aiName, NSAY, %biggestn+1] = "";
	$BotInfo[%aiName, CUE, %biggestn+1] = "";
	$BotInfo[%aiName, NCUE, %biggestn+1] = "";

	//==============================================
	//The following is generally BotMaker-only code
	//==============================================
	if($BotInfo[%aiName, SIMGROUP] != "")
	{
		%g = nameToId("MissionGroup\\" @ $BotInfo[%aiName, SIMGROUP]);

		%count = Group::objectCount(%g);
		for(%i = 0; %i <= %count-1; %i++)
		{
			%object = Group::getObject(%g, %i);
			if(getObjectType(%object) == "SimGroup")
			{
				%system = Object::getName(%object);
				%type = GetWord(%system, 0);
				%info = String::getSubStr(%system, String::len(%type)+1, 9999);

				if(%type == "NAMES")
					$BotInfo[%aiName, NAMES] = %info;
				else if(%type == "DEFAULTS")
				{
					%class = GetWord(%info, 0);
					%stuff = String::getSubStr(%info, String::len(%class)+1, 9999);

					$BotInfo[%aiName, DEFAULTS, %class] = %stuff;
				}
			}
			else if(getObjectType(%object) == "Marker")
			{
				$BotInfo[%aiName, DESTSPAWN] = %object;
			}
		}
	}
	//==============================================

	return %marker;
}

function RPG::isAiControlled(%clientId)
{
	dbecho($dbechoMode, "RPG::isAiControlled(" @ %clientId @ ")");

	if(fetchData(%clientId, "BotInfoAiName") != "" || fetchData(%clientId, "SpawnBotInfo") != "")
		return True;
	else
		return False;
}

//These are for the pets
function Pet::BeforeTurnEvil(%clientId)
{
	dbecho($dbechoMode, "Pet::BeforeTurnEvil(" @ %clientId @ ")");

	internalSay(%clientId, 0, "#say I'm starting to get enough of this...");
}
function Pet::TurnEvil(%clientId)
{
	dbecho($dbechoMode, "Pet::TurnEvil(" @ %clientId @ ")");

	internalSay(%clientId, 0, "#shout To hell with you all! Die!");

	storeData(%aiId, "botAttackMode", 1);
	AI::newDirectiveRemove(fetchData(%clientId, "BotInfoAiName"), 99);
	storeData(%aiId, "tmpbotdata", "");

	GameBase::setTeam(%clientId, 1);
}


//____________________________________________________________________________________________________
// Rewrote periodic entirely. Bots actually respect "freezing" flags. They will cast spells as often
// as they can. They will use skills more often than the player. They will jump. They will target
// allies for grouping and healing. All of this is managed with streamlined logic. To change
// AI behaviour, start by looking the AI::is* series of functions and work backwards.
function AI::Periodic(%aiName)
{
	dbecho($dbechoMode, "AI::Periodic(" @ %aiName @ ")");

	%aiId 			= AI::getId(%aiName);
	%isCastingNow 	= fetchData(%aiId, "SpellCastStep");
	
	//_________________________________________________________________________
	// Stop thinking when casting, dumb, frozen, invalid, or "not sniffing"
	if(fetchData(%aiId, "dumbAIflag") || fetchData(%aiId, "frozen") || fetchData(%aiId, "noBotSniff") ||
		fetchData(%aiId, "SpawnBotInfo") == "" || %isCastingNow == 1) {
			return;
	}
	
	//_________________________________________________________________________
	// handle events
	%i = GetEventCommandIndex(%aiId, "onPosCloseEnough");
	if(%i != -1 && %isCastingNow == "") {
		%x = GetWord($EventCommand[%aiId, %i], 2);
		%y = GetWord($EventCommand[%aiId, %i], 3);
		%z = GetWord($EventCommand[%aiId, %i], 4);
		%dpos = %x @ " " @ %y @ " " @ %z;

		if(Vector::getDistance(%dpos, GameBase::getPosition(%aiId)) <= 5) {
			%name = GetWord($EventCommand[%aiId, %i], 0);
			%type = GetWord($EventCommand[%aiId, %i], 1);
			%cl = NEWgetClientByName(%name);
			if(%cl == -1)
				%cl = 2048;

			%cmd = String::NEWgetSubStr($EventCommand[%aiId, %i], String::findSubStr($EventCommand[%aiId, %i], ">")+1, 99999);
			%pcmd = ParseBlockData(%cmd, %aiId, "");
			$EventCommand[%aiId, %i] = "";
			schedule("internalSay(" @ %cl @ ", 0, \"" @ %pcmd @ "\", \"" @ %name @ "\");", 1);
		}
	}	
	%i = GetEventCommandIndex(%aiId, "onIdCloseEnough");
	if(%i != -1 && %isCastingNow == "") {
		%id = GetWord($EventCommand[%aiId, %i], 2);
		%dpos = GameBase::getPosition(%id);

		if(Vector::getDistance(%dpos, %aiPos) <= 10) {
			%name = GetWord($EventCommand[%aiId, %i], 0);
			%type = GetWord($EventCommand[%aiId, %i], 1);
			%cl = NEWgetClientByName(%name);
			if(%cl == -1)
				%cl = 2048;

			%cmd = String::NEWgetSubStr($EventCommand[%aiId, %i], String::findSubStr($EventCommand[%aiId, %i], ">")+1, 99999);
			%pcmd = ParseBlockData(%cmd, %aiId, "");
			$EventCommand[%aiId, %i] = "";
			schedule("internalSay(" @ %cl @ ", 0, \"" @ %pcmd @ "\", \"" @ %name @ "\");", 1);
		}
	}
	
	if(OddsAre(3))
		AI::SelectBestWeapon(%aiId);
	
	//________________________________________
	// Apply animation states
	if(AddBonusStatePoints(%aiId, "PTFY") > 0) {
		%r = floor(getRandom() * 100);
		if(%r >= 25) {
			AI::newDirectiveWaypoint(%aiName, GameBase::getposition(%aiId), 99);
			Item::setVelocity(%aiId, "0 0 0");
			Player::setAnimation(%aiId, 15);
		}
	} else if(Item::getVelocity(%aiId) == "0 0 0" && %aiId.sleepMode == "" && %isCastingNow == "") {
		%r = floor(getRandom() * 200)+1;
		if(%r >= 1 && %r <= 10)			Player::setAnimation(%aiId, 48);		// kneel
		else if(%r < 100)				Player::setAnimation(%aiId, 49);		// stand pos
		else 							Player::setAnimation(%aiId, 0);
	} else if(!%isMagicUser) {
		%jumpHeight = $AIJumpHeight[fetchData(%aiId,"RACE")];
		if(%jumpHeight > 0) {
			%r = floor(getRandom() * 100)+1;
			if(%r >= 21 && %r <= 50)	{
				Player::setAnimation(%aiId, 6);		// jump			
				Player::applyimpulse(%aiId, "0 0 50");
			}
		}
	}
	
	//________________________________________________________________
	// Determine AI characteristics and find all potential targets
	%aiTeam 		= GameBase::getTeam(%aiId);
	%aiPos 			= GameBase::getPosition(%aiId);
	%aiClass 		= fetchData(%aiId,"CLASS");
	%isHealer 		= AI::IsHealer(%aiId);
	%isRangedClass 	= AI::IsRanged(%aiId);
	%isCombatTrained= AI::IsCombatTrained(%aiId);
	%isMagicUser 	= AI::IsMagicUser(%aiId);
	%weapon 		= Player::getMountedItem(%aiId, $WeaponSlot);
	%closestId 		= "";
	%closestAllyId 	= "";	
	%closest 		= 500000;
	%closestAlly 	= 500000;		
	%b 				= $AImaxRange * 2;
	%set 			= newObject("set", SimSet);
	%n 				= containerBoxFillSet(%set, $SimPlayerObjectType, %aiPos, %b, %b, %b, 0);	
	for(%i = 0; %i < Group::objectCount(%set); %i++) {			
		%id = Player::getClient(Group::getObject(%set, %i));
		if(%id == %aiId) continue;
		%sameTeam = (GameBase::getTeam(%id) == %aiTeam);
		%dist = Vector::getDistance(%aiPos, GameBase::getPosition(%id));
		if(!%sameTeam && !fetchData(%id, "invisible") && %dist < %closest) {
			%closest = %dist;
			%closestId = %id;					
		} else if(%sameTeam && %dist <= %closestAlly) {
			%closestAlly = %dist;
			%closestAllyId = %id;
		}
	}
	deleteObject(%set);
	
	//________________________________________
	// Mark the closest enemy and ally
	%newTarget = ((%closestId != "") && (fetchData(%aiId, "AILastTarget") != %closestId));
	if(%closestId)		storeData(%aiId, "AILastTarget", %closestId);
	if(%closestAllyId)	storeData(%aiId, "AILastAllyID", %closestAllyId);

	//____________________________________________________________________________________________________
	// When there are no enemies around, try to play a noise and move according to stored bot flags
	if(%closest > $AImaxRange) {
		if(OddsAre(4)) PlaySound(RandomRaceSound(fetchData(%aiId, "RACE"), RandomWait), GameBase::getPosition(%aiId));
		AI::SelectMovement(%aiName);
		return;
	}
	
	//________________________________________
	// Combat loop: when an enemy is in range
	%range = 0;
	%weaponIsRanged = false;
	if(%weapon != -1) {
		%range = GetRange(%weapon);
		%weaponIsRanged = (%range > 0) && 
							(( $SkillType[%weapon] == $SkillWands && $AccessoryVar[%weapon, $AccessoryType] == $SwordAccessoryType)
							|| $AccessoryVar[%weapon, $AccessoryType] == $RangedAccessoryType);				
	}
	
	// Ranged classes with ranged weapons attempt to create distance and kite
	if(%weaponIsRanged && %isRangedClass && %closest < (%range / 2)) {	
		%tryMove = GameBase::getPosition(%closestId);
		%kiteBack = Vector::getFromRot(%aiId, -String::len(%aiClass), 1); // You read that right: go backward by the **length of the CLASS string ;)**
		%tryMove = NewVector(VecX(%tryMove) + VecX(%kiteBack), VecY(%tryMove) + VecY(%kiteBack), VecZ(%tryMove));
		AI::newDirectiveWaypoint(%aiName, %tryMove, 99);				
	}
	// Any ranged weapon will cause the bot to prefer "follow" mode while closing in
	else if(%weaponIsRanged && %closest > %range) {
		AI::newDirectiveFollow(%aiName, %closestId, 0, 99);
	}
	// Melee classes will charge if out of range
	else if(%closest > (%range/2)) {
		AI::newDirectiveWaypoint(%aiName, GameBase::getPosition(%closestId), 99);
	}
	// Anything in range of the bot will be tracked
	else {
		if(getRandom() <= 0.2){
			AI::newDirectiveWaypoint(%aiName, Vector::Add(GameBase::getPosition(%closestId),Vector::Random(8)), 99);	
		} else if(getRandom() <= 0.2){
			AI::newDirectiveWaypoint(%aiName, Vector::Add(GameBase::getPosition(%aiId),Vector::Random(4)), 99);	
		} else {
			AI::newDirectiveFollow(%aiName, %closestId, 0, 99);
		}
	}
	
	//_____________________________________________________
	// If we are not casting nor recovering... try to cast
	if(fetchData(%aiId, "AIJustCasted") > 0) {
		storeData(%aiId, "AIJustCasted", "");		// This is necessary to ensure the AI has a chance to move. Healers will often get stuck in a loop otherwise.
	} 
	else if(%isCastingNow == "" && %isMagicUser || %isCombatTrained) {		
		%target 		= fetchData(%aiId, "AILastTarget");
		%targetAlly 	= fetchData(%aiId, "AILastAllyID");			
		%index 			= -1;
		%forceSkillTree = false;
		%healing		= false;		
		%offensiveCast	= AI::IsAggressiveMagicUser(%aiId);
		
		if(%isMagicUser) {
			%validAlly = ((%targetAlly > 2048) && %aiTeam == GameBase::getTeam(%targetAlly));
			%wantsToHeal = (!%offensiveCast || getRandom() * 10 > 5);	//aggressive magic users that also heal balance casts by 50/50
			if(%isHealer && (%validAlly || %wantsToHeal)) { 
				%target = %targetAlly;
				storeData(%aiId, "AILastTarget", %targetAlly);
				storeData(%aiId, "AILastAllyID", "");
				%healing = true;
			}
		} else if (%isCombatTrained) {
			%forceSkillTree = $SkillCombatArts;	
		}
		
		//_________________________________________________________
		// Deciide on whether to target self
		%hasTarget = (%target > 2048);
		if(!%offensiveCast){
			if(getRandom()*100 > 50) {
				%hasTarget = false;
			}
		}
		
		%index = AI::GetBestSpell(%aiId, %hasTarget, %healing, %forceSkillTree);
		if(%index == -1)
			%index = AI::GetBestSpell(%aiId, %hasTarget, !%healing, %forceSkillTree);
		
		if(%index != -1) {
			storeData(%aiId, "AIJustCasted", 1);
			AI::newDirectiveRemove(%aiName, 99);
			Item::setVelocity(%aiId, "0 0 0");
			rpg::FaceEnemy(%aiId, %target);
			refreshMANAREGEN(%aiId);
			BeginCastSpell(%aiId, $Spell::keyword[%index]);
			if($DisallowAINinjitsu == true)
				return;	// by default, bots can cast, use a skill and make noises all on the same frame
		}
	}
	
	//___________________________________________________________________
	// Try to use a skill by spamming them at random
	%usedSkill = false;
	if(getRandom() * 100 > 33)	%usedSkill = AI::TryUseSkill(%aiId, $SkillListAICombat[$SkillThievery], AI::IsThief(%aiId));
	if(getRandom() * 100 > 33)	%usedSkill = AI::TryUseSkill(%aiId, $SkillListAICombat[$SkillSurvival], AI::IsSurvivalist(%aiId));
	if(getRandom() * 100 > 33)	%usedSkill = AI::TryUseSkill(%aiId, $SkillListAICombat[$SkillWordsmith], AI::IsWordsmith(%aiId));
	if(!%usedSkill) {
		if(%newTarget || OddsAre(6))	PlaySound(RandomRaceSound(fetchData(%aiId, "RACE"), Acquired), GameBase::getPosition(%aiId));
		else if(OddsAre(9))				PlaySound(RandomRaceSound(fetchData(%aiId, "RACE"), RandomWait), GameBase::getPosition(%aiId));
		return;
	}	
}

//____________________________________________________________________________________________________
// This function is dumb, but it works because every #skill has been programmed to ignore cooldown
// times if the AI is trying to use them. This allows AI to spam skills like nuts, which isn't always
// ideal, but given how I wrote skills to use a single cooldown per tree... this was the easiest way
// to ensure the AI would actually use skills and not block themselves.
//
// Put this in the category of, "If I ever turn this into a bigger project"... this works. AI will
// happily #steal, #leap, #bleed, #smash, or do whatever else you ask. Good enough :).

function AI::TryUseSkill(%aiId, %skillTree, %hasAffinity) {
	%lastSkill = -1;
	for(%x=0;(%skill = GetWord(%skillTree,%x)) != -1; %x++) {
		%isLoadoutSkill 	= AI::HasLoadoutSkill(fetchData(%aiId,"RACE"), %skill);
		if((%hasAffinity && (%isLoadoutSkill && SkillCanUse(%aiId,%skill)))) {
			if ((%lastSkill == -1) || ($AILastSkillUsed[%aiId] != %skill && getRandom() > 0.3))
				%lastSkill = %skill;
		}
		
	}
	if(%lastSkill != -1) {
		$AILastSkillUsed[%aiId] = %lastSkill;
		if($DebugAISpellConsderation) echo(%lastSkill);
		internalSay(%aiId, 0, %lastSkill);
		%skillTree = "";
		return true;
	}
	return false;
}

//____________________________________________________________________________________________________
function AI::IsHealer(%aiId) {					// will use healing spells on allies
	%aiClass = fetchData(%aiId,"CLASS");
	return (%aiClass == "Cleric" || %aiClass == "Paladin" || %aiClass == "Druid" || %aiClass == "Enchanter");
}
//____________________________________________________________________________________________________
function AI::IsRanged(%aiId) {					// tries to stay away/kite
	%aiClass = fetchData(%aiId,"CLASS");
	return (%aiClass == "Mage" || %aiClass == "Druid" || %aiClass == "Merchant" || %aiClass == "Ranger");
}
//____________________________________________________________________________________________________
function AI::IsAggressiveMagicUser(%aiId) {		// wont target self with spells
	%aiClass = fetchData(%aiId,"CLASS");
	return (%aiClass == "Mage" || %aiClass == "Druid" || %aiClass == "Invoker" || %aiClass == "Paladin");
}
//____________________________________________________________________________________________________
function AI::IsMagicUser(%aiId) {				// will try to cast spells
	%aiClass = fetchData(%aiId,"CLASS");
	return (%aiClass == "Mage" || %aiClass == "Bard" || %aiClass == "Druid" || %aiClass == "Enchanter" || %aiClass == "Invoker" || %aiClass == "Cleric" || %aiClass == "Paladin");
}
//____________________________________________________________________________________________________
function AI::IsThief(%aiId) {					// uses $SkillThievery
	%aiClass = fetchData(%aiId,"CLASS");
	return (%aiClass == "Thief" || %aiClass == "Merchant" || %aiClass == "Enchanter" || %aiClass == "Bard");
}
//____________________________________________________________________________________________________
function AI::IsSurvivalist(%aiId) {				// uses $SkillSurvival
	%aiClass = fetchData(%aiId,"CLASS");
	return (%aiClass == "Fighter" || %aiClass == "Ranger" || %aiClass == "Invoker" || %aiClass == "Paladin");
}
//____________________________________________________________________________________________________
function AI::IsWordsmith(%aiId) {				// uses $SkillWordsmith
	%aiClass = fetchData(%aiId,"CLASS");
	return (%aiClass == "Cleric" || %aiClass == "Merchant" || %aiClass == "Bard");
}
//____________________________________________________________________________________________________
function AI::IsCombatTrained(%aiId) {			// casts Combat Arts
	%aiClass = fetchData(%aiId,"CLASS");
	return (%aiClass == "Brawler" || %aiClass == "Ranger" || %aiClass == "Paladin");
}
//____________________________________________________________________________________________________
function AI::HasLoadoutSkill(%race, %skill) {
	if($AISkillKit[%race] == "all" || $AISkillKit[%race] == "") 
		return true;
	for(%i = 0;(%w = GetWord($AISkillKit[%race],%i)) != -1; %i++) {
		if(%w == %skill) 
			return true;
	}
	return false;
}

//________________________________________________________________________________________________
// GetBestSpell used to fart and hope the AI picked a good spell. The function now works with
// intention and design. The loop works to eliminate spells the AI can't cast. A good spell
// falls through the continues; getRandom() ensures the AI rotates through its spell kit.
//
// If it looks like AI aren't using the right spells, it's probably not a bug -- you probably
// have them set to a RACE that doesn't have the given spell in their spell kit, or the 
// CLASS of the AI doesn't have affinity.
//
// If AI have no "spell kit", or the spell kit is simply "any", they will try to cast anything
// that they have at least a 1.0 skill multiplier in. If you call this function with the last
// parameter set to a $Skill, the AI will only consider spells from that tree.
$DebugAISpellConsderation = false;
function AI::GetBestSpell(%clientId, %hasTarget, %healing, %forceSpecificSpellset) {
	%selectedSpell		= -1;
	%cltype 			= fetchData(%clientId, "CLASS"); 
	%race 				= fetchData(%clientId, "RACE"); 
	if($DebugAISpellConsderation) %debugConsider = "";
	
	for(%i = 1; %i <= $SpellDefIndex; %i++) {
		%spellname = $Spell::keyword[%i];
		if (!AI::HasLoadoutSkill(%race, %spellname)) continue;
		if (!SkillCanUse(%clientId, %spellname)) continue;
		if (fetchData(%clientId, "MANA") < $Spell::manaCost[%i]) continue;
		%spellPriority = $Spell::refVal[%i];
		if (%spellPriority == 0) continue;
		
		%skilltype = $SkillType[%spellname];
		if (%forceSpecificSpellset && %skilltype != %forceSpecificSpellset) continue;
		if ($SkillMultiplier[%cltype, %skilltype] < 1.0) continue;
		
		if(!%healing){
			%targetOther = ($Spell::targetType[%i] & $TargetOther) || ($Spell::targetType[%i] & $TargetDamageSpell);
			if (%hasTarget && !%targetOther) continue;
			if (!%hasTarget && %targetOther) continue;
		}
		%spellHitsEnemies 	= (($Spell::targetType[%i] & $TargetDamageSpell) && $Spell::damageValue[%i] > 0) || ($Spell::targetType[%i] & $TargetBuffEnemy);
		//if (%healing && %spellHitsEnemies) continue;
		if (!%healing && !%spellHitsEnemies && %hasTarget) continue;
		
		if($DebugAISpellConsderation) echo(%healing);
		
		if($AIAvoidLastSpell[%clientId] != %selectedSpell && (%selectedSpell == -1 || (getRandom() * 10 < 3))) {
			%selectedSpell = %i;
			if($DebugAISpellConsderation) %debugConsider = %debugConsider @ " " @ %spellname;
		}
	}
	if($DebugAISpellConsderation) echo(%debugConsider);
	
	if($AIAvoidLastSpell[%clientId] != "" && %selectedSpell == -1)
		%selectedSpell = $AIAvoidLastSpell[%clientId];

	return %selectedSpell;
}
