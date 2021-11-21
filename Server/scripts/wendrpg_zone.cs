//This file is part of Tribes RPG.
//Tribes RPG server side scripts
//Written by Jason "phantom" Daley,  Matthiew "JeremyIrons" Bouchard, and more (yet undetermined)

//	Copyright (C) 2019  Jason Daley

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



function InitZones()
{
	dbecho($dbechoMode, "InitZones()");

	$numZones = 0;
	%zcnt = 0;
	%umusiccnt = 0;

	%group = nameToId("MissionGroup\\Zones");

	if(%group != -1)
	{
		%count = Group::objectCount(%group);
		for(%i = 0; %i <= %count-1; %i++)
		{
			%object = Group::getObject(%group, %i);
			%system = Object::getName(%object);
			%type = GetWord(%system, 0);
			%desc = String::getSubStr(%system, String::len(%type)+1, 9999);

			//---------------------------------------------------------------
			//THIS PART GATHERS SOUNDS FOR THE GENERIC UNKNOWN ZONE
			// there is no EXIT sound for the unknown zone.
			//---------------------------------------------------------------
			if(GetWord(%system, 0) == "ENTERSOUND")
			{	
				$Zone::EnterSound[0] = GetWord(%system, 1);
			}
			else if(GetWord(%system, 0) == "AMBIENTSOUND")
			{
				$Zone::AmbientSound[0] = GetWord(%system, 1);
				$Zone::AmbientSoundPerc[0] = GetWord(%system, 2);
			}
			else if(GetWord(%system, 0) == "MUSIC")
			{
				$Zone::Music[0, %umusiccnt++] = GetWord(%system, 1);
				$Zone::MusicTicks[0, %umusiccnt] = GetWord(%system, 2);
			}
			//---------------------------------------------------------------
			else
			{
				%zcnt++;

				%tmpgroup = nameToId("MissionGroup\\Zones\\" @ %system);
				%tmpcount = Group::objectCount(%tmpgroup);
				%marker = "";
				%musiccnt = 0;

				for(%z = 0; %z <= %tmpcount-1; %z++)
				{
					%tmpobject = Group::getObject(%tmpgroup, %z);
	
					if(getObjectType(%tmpobject) == "Marker")
					{
						if(%marker == "")
						{
							%marker = %tmpobject;
							$numZones++;
						}
					}
					else if(getObjectType(%tmpobject) == "SimGroup")
					{
						%n = Object::getName(%tmpobject);
						
						if(GetWord(%n, 0) == "ENTERSOUND")
						{	
							$Zone::EnterSound[%zcnt] = GetWord(%n, 1);
						}
						else if(GetWord(%n, 0) == "AMBIENTSOUND")
						{
							$Zone::AmbientSound[%zcnt] = GetWord(%n, 1);
							$Zone::AmbientSoundPerc[%zcnt] = GetWord(%n, 2);
						}
						else if(GetWord(%n, 0) == "EXITSOUND")
						{
							$Zone::ExitSound[%zcnt] = GetWord(%n, 1);
						}
						else if(GetWord(%n, 0) == "MUSIC")
						{
							$Zone::Music[%zcnt, %musiccnt++] = GetWord(%n, 1);
							$Zone::MusicTicks[%zcnt, %musiccnt] = GetWord(%n, 2);
						}
					}
				}
				
				%mname = Object::getName(%marker);
				$Zone::Marker[%zcnt] = GameBase::getPosition(%marker);
				$Zone::Length[%zcnt] = GetWord(%mname, 0);
				$Zone::Width[%zcnt] = GetWord(%mname, 1);
				$Zone::Height[%zcnt] = GetWord(%mname, 2);
				$Zone::SHeight[%zcnt] = GetWord(%mname, 3);
				$Zone::Type[%zcnt] = %type;
				$Zone::Desc[%zcnt] = %desc;
				$Zone::FolderID[%zcnt] = %tmpgroup;
				$Zone::zoneID[%tmpgroup] = %zcnt;
			}
		}
		echo($numZones @ " zones initialized.");
	}
}

function RecursiveZone(%delay)
{
	dbecho($dbechoMode, "RecursiveZone(" @ %delay @ ")");

	//increment by 1 every $zoneCheckDelay seconds
	$zoneTicker[1]++;
	$zoneTicker[2]++;

	if($zoneTicker[1] >= 1) {
		DoZoneCheck(2, %delay);
		$zoneTicker[1] = "";
	}
	schedule("RecursiveZone(" @ %delay @ ");", %delay);
}

//_______________________________________________________________________________________________________________________________________
// DescX Note:
//		"world's crudest LOD"  ->  zone check determines object creation
//
if($ManagedObjectZoneSizeScale=="")
	$ManagedObjectZoneSizeScale = 4;

function DoZoneCheck(%w, %d)
{
	dbecho($dbechoMode, "DoZoneCheck(" @ %w @ ", " @ %d @ ")");

	//Massive zone check for entire world
	%mset = newObject("set", SimSet);
	%n = containerBoxFillSet(%mset, $SimPlayerObjectType, "0 0 0", 9999, 9999, 9999, 0);

	rpg::UpdateVisibleManagedZones();
	
	for(%z = 1; %z <= $numZones; %z++) {
		%set = newObject("set", SimSet);
		%n = containerBoxFillSet(%set, $SimPlayerObjectType, $Zone::Marker[%z], $Zone::Length[%z], $Zone::Width[%z], $Zone::Height[%z], $Zone::SHeight[%z]);
		Group::iterateRecursive(%set, setzoneflags, %z);
		deleteObject(%set);
	}
	Group::iterateRecursive(%mset, UpdateZone);
	deleteObject(%mset);
}
function setzoneflags(%object, %z)
{
	dbecho($dbechoMode, "setzoneflags(" @ %object @ ", " @ %z @ ")");

	%clientId = Player::getClient(%object);
	storeData(%clientId, "tmpzone", %z);
}function UpdateZone(%object)
{
	dbecho($dbechoMode, "UpdateZone(" @ %object @ ")");

	%clientId = Player::getClient(%object);	if(%clientId == -1)		return;
	%clientId.remoteEvalSpam = "";	%clientId.tabMenuSpam = "";
	%zoneflag = fetchData(%clientId, "tmpzone");

	$ThiefPositionTracker[%clientId]++;
	if($ThiefPositionTracker[%clientId] >= Cap(getRandom()*400,90,300)) {
		$ThiefPositionTracker[%clientId] = 0;
		%getGround = findGroundPos(GameBase::getPosition(%clientId), 2, 2);
		%dist = Vector::getDistance(%getGround, $ThiefPosition[%clientId]);
		if ($ThiefPosition[%clientId] != GameBase::getPosition(%clientId) &&
		!fetchData(%clientId, "invisible") && $ThiefPosition[%clientId] != "" && $ThiefPosition[%clientId] != "0 0 0") {
			%stolen = fetchData(%clientId,"STOLEN");
			if(%stolen > 5000) {
				%spawnChance = (fetchData(%clientId,"COINS")+1) / %stolen;
				%mult = Cap(%dist/10,1,5);
				
				while(%spawnChance < 1) { %spawnChance *= 100; %mult--; }
				while(%spawnChance > 1) { %spawnChance /= 10; %mult++; }
				if(%mult < 1) %mult = 1;
				%roll = getRandom() * %mult;
				%against = (%spawnChance*%mult*2);
				if(%roll < %against) {
					rpg::SpawnMandatoryLawman(%clientId);
				}
			}
		}
	} else if($ThiefPositionTracker[%clientId] == 2) {
		$ThiefPosition[%clientId] = findGroundPos(GameBase::getPosition(%clientId), 2, 2);
	}

	//check if the player was found inside a zone
	if(%zoneflag != "")
	{
		//the player is inside a zone!
	
		//check if the player's current zone matches the one he's detected in
		if(fetchData(%clientId, "zone") != $Zone::FolderID[%zoneflag])
		{
			//the client's current zone does not match the one he really is in, so boot the player out of his
			//current zone (if any)
			if(fetchData(%clientId, "zone") != "")
				Zone::DoExit(Zone::getIndex(fetchData(%clientId, "zone")), %clientId);
	
			//throw the player inside this new zone
			Zone::DoEnter(%zoneflag, %clientId);
		}
		else
		{
			//the client is in the same zone as he was since the last zonecheck
			if($Zone::AmbientSound[%zoneflag] != "")
			{
				%m = $Zone::AmbientSoundPerc[%zoneflag];
				if(%m == "") %m = 100;
	
				%r = floor(getRandom() * 100)+1;
				if(%r <= %m)
					Client::sendMessage(%clientId, 0, "~w" @ $Zone::AmbientSound[%zoneflag]);
			}
			if($Zone::Music[%zoneflag, 1] != "")
			{
				if(%clientId.MusicTicksLeft < 1)
				{
					for(%m = 1; $Zone::Music[%zoneflag, %m] != ""; %m++){}
					%m--;
					%clientId.currentMusic = floor(getRandom() * %m) + 1;

					Client::sendMessage(%clientId, 0, "~w" @ $Zone::Music[%zoneflag, %clientId.currentMusic]);
					%clientId.MusicTicksLeft = $Zone::MusicTicks[%zoneflag, %clientId.currentMusic]+2;
				}
			}
			if($Zone::Type[%zoneflag] == "WATER")
			{
				storeData(%clientId, "Thirst", 600);
				if(!IsDead(%clientId))
				{
					%noDrown = "";					
					if(AddBonusStatePoints(%clientId,"BREATH") > 0) {
						%noDrown = True;
						storeData(%clientId, "drownCounter", 0);
					}
					else {
						for(%i = 1; (%orb = $ItemList[Orb, %i]) != ""; %i++) {
							if($ProtectFromWater[%orb]) {
								if(Player::getItemCount(%clientId, %orb @ "0")) {
									storeData(%clientId, "drownCounter", 0);
									%noDrown = True;
									break;
								}
							}
						}
					}

					if(!%noDrown)
					{
						%dn = 10;

						storeData(%clientId, "drownCounter", 1, "inc");
						if((%dc = fetchData(%clientId, "drownCounter")) > %dn)
						{
							%dmg = Cap(floor(pow((%dc - %dn) / 1.2, 2)), 1.0, 1000) * "0.01";
							if(Player::getItemCount(%clientId, SlayerGear0)) {
								if(fetchData(%clientId, "SlayerSuicideWater") > 0) {
									storeData(%clientId, "SlayerSuicideWater", "");
									Client::sendMessage(%clientId, $MsgGreen, "You discover that your maximum rank Slayer Gear has a magical oxygen converter! You can't drown while wearing it and gain mana instead!");
								}
								refreshMANA(%clientId,-(%dmg*3));
							} else {								
								GameBase::virtual(%clientId, "onDamage", 0, %dmg, "0 0 0", "0 0 0", "0 0 0", "torso", "front_right", %clientId);
							}
							playSound(SoundDrown1, GameBase::getPosition(%clientId));
						}
					}
				}
			}
		}
		
		//this simulates underwater
		if($Zone::Type[%zoneflag] == "WATER") {
			gravWorkaround(%clientId, 1);			
		}
		else if($Zone::Desc[%zoneflag] == "Overville") {
			gravWorkaround(%clientId, 2);
		}
	} else {
		//the player is not inside any zone. if the player has a current zone, then we need to kick him out of it
		if(fetchData(%clientId, "zone") != "") {
			storeData(%clientId, "drownCounter", "");
			Zone::DoExit(Zone::getIndex(fetchData(%clientId, "zone")), %clientId);
		}
		
		if($Zone::Music[0, 1] != "") {
			if(%m == "") %m = 100;
			
			%r = floor(getRandom() * 100)+1;
			if(%r <= %m)
				Client::sendMessage(%clientId, 0, "~w" @ $Zone::Music[0, 1]);
		}
	
		//play the enter sound for the unknown zone
		if($Zone::EnterSound[0] != "")
			Client::sendMessage(%clientId, 0, "~w" @ $Zone::EnterSound[0]);
	}

	if(%clientId.MusicTicksLeft > 0)
		%clientId.MusicTicksLeft--;
	
	// Bleed targets. Awkward spot, not really worth a function... eh..
	for(%i = 1; %i <= $maxBonusStates; %i++) {	
		if($BonusStateCnt[%clientId, %i] > 0) {
			for(%z = 0; (%p1 = GetWord($BonusState[%clientId, %i], %z)) != -1; %z+=2) {
				%bonusType = %p1;
				if(%bonusType == "BLEED") {
					%attackerId = GetWord($BonusState[%clientId, %i], %z+1);
					GameBase::virtual(%clientId, "onDamage", $DrainDamageType, $BonusStateVal[%clientId, %i], 
					"0 0 0", "0 0 0", "0 0 0", "torso", "", %attackerId, "#bleed", true );					
				}
				
				else if(%bonusType == "POISON") {
					%attackerId = GetWord($BonusState[%clientId, %i], %z+1);
					GameBase::virtual(%clientId, "onDamage", $DrainDamageType, $BonusStateVal[%clientId, %i], 
					"0 0 0", "0 0 0", "0 0 0", "torso", "", %attackerId, "poison", true );					
				}
				
				else if(%bonusType == "DMG") {
					GameBase::virtual(%clientId, "onDamage", $DrainDamageType, $BonusStateVal[%clientId, %i], 
					"0 0 0", "0 0 0", "0 0 0", "torso", "", %clientId, "", true );					
				}
			}
		}
	}

	
	DecreaseBonusStateTicks(%clientId);
	
	if(!Player::isAiControlled(%clientId)) {
		%thirst = fetchData(%clientId, "Thirst");
		if(%thirst * 1 == 0)	storeData(%clientId, "Thirst", 0);
		if(%thirst > 600) 		storeData(%clientId, "Thirst", 600);
		storeData(%clientId, "Thirst", 1, "dec");
		%thirst = fetchData(%clientId, "Thirst");
		if(%thirst < -600) 		storeData(%clientId, "Thirst", -600);	
		refreshHPREGEN(%clientId);
		refreshMANAREGEN(%clientId);
	}
	
	%pos = GameBase::getPosition(%clientId);					// Check if the player has moved since last ZoneCheck
	if(%pos != %clientId.zoneLastPos && !IsDead(%clientId))
	{
		//train Weight Capacity
		if(OddsAre(4))
			UseSkill(%clientId, $SkillEndurance, False, True);

		//cycle thru orbs
		for(%i = 1; (%orb = $ItemList[Orb, %i]) != ""; %i++)
		{
			if(OddsAre($BurnOut[%orb]))
			{
				if(Player::getItemCount(%clientId, %orb @ "0"))
				{
					Client::sendMessage(%clientId, $MsgRed, "Your " @ %orb.description @ " has burned out.");
					Player::decItemCount(%clientId, %orb @ "0", 1);
					RefreshAll(%clientId);
				}
			}
			if($BurnOutInRain[%orb] > 0)
			{
				if(fetchData(%clientId, "zone") == "" && $isRaining)
				{
					if(OddsAre($BurnOutInRain[%orb]))
					{
						if(Player::getItemCount(%clientId, %orb @ "0"))
						{
							Client::sendMessage(%clientId, $MsgRed, "The rain has burned out your " @ %orb.description @ ".");
							Player::decItemCount(%clientId, %orb @ "0", 1);
							RefreshAll(%clientId);
						}
					}
				}
			}
		}

		//hard-coded list to save on CPU
		for(%z = 1; $ItemList[Badge, %z] != ""; %z++)
		{
			%item = $ItemList[Badge, %z];
			if(HasThisStuff(%clientId, %item @ " 1") || Belt::HasThisStuff(%clientId, %item))
			{
				%a = GetWord($BonusItem[%item], 0);
				%b = GetWord($BonusItem[%item], 1);
				%c = GetWord($BonusItem[%item], 2);
				%d = GetWord($BonusItem[%item], 3);
				
				%count = Cap(floor(rpg::GetItemCount(%clientId, %item)/2)*getRandom(),1,%c);
				
				
				if(OddsAre(%c/%count)) {
					if($BonusItemMessage[%item] != "") {
						Client::sendMessage(%clientId, $BonusItemMessageColor[%item], $BonusItemMessage[%item]);
					}
					if($BonusStateName[%a] != "") {
						UpdateBonusState(%clientId, %a @ " " @ %item, %d, %b);
					} else {
						GiveThisStuff(%clientId, %a @ " " @ %b, True);
					}
				}
			}
		}

		//perhaps leave scent
		if(!fetchData(%clientId, "invisible"))
		{
			if(OddsAre(floor(GetSkillWithBonus(%clientId,$SkillSurvival) / 100)+1))
			{
				storeData(%clientId, "lastScent", GameBase::getPosition(%clientId));
			}
		}
	}
	%clientId.zoneLastPos = %pos;

	storeData(%clientId, "tmpzone", "");
}

function gravWorkaround(%clientId, %method)
{
	dbecho($dbechoMode, "gravWorkaround(" @ %clientId @ ", " @ %method @ ")");

	if(%method == 1)
	{
		%rzdelay = 3;
		%steps = 16;

		for(%i = 0; %i < %steps; %i++)
		{
			%d = %i / (%steps / %rzdelay);
			schedule("Player::applyImpulse(" @ %clientId @ ", \"0 0 13\");", %d, %clientId);
		}
	}
	else if(%method == 2)
	{
		%rzdelay = 3;
		%steps = 16;

		for(%i = 0; %i < %steps; %i++)
		{
			%d = %i / (%steps / %rzdelay);
			schedule("Player::applyImpulse(" @ %clientId @ ", \"0 0 13\");", %d, %clientId);
		}
	}
}

function Zone::DoEnter(%z, %clientId)
{
	dbecho($dbechoMode, "Zone::DoEnter(" @ %z @ ", " @ %clientId @ ")");

	%oldZone = fetchData(%clientId, "zone");
	%newZone = $Zone::FolderID[%z];

	storeData(%clientId, "zone", $Zone::FolderID[%z]);

	if($Zone::Type[%z] == "PROTECTED")
	{
		%msg = "You have entered " @ $Zone::Desc[%z] @ ".  This is protected territory.";
		%color = $MsgBeige;
	}
	else if($Zone::Type[%z] == "DUNGEON")
	{
		%msg = "You have entered " @ $Zone::Desc[%z] @ ".  Beware of enemies!";
		%color = $MsgRed;
	}
	else if($Zone::Type[%z] == "WATER")
	{
		%msg = "";
		if(fetchData(%clientId, "InWaterZone") != true){
			storeData(%clientId, "InWaterZone", true);
			if(HasThisStuff(%clientId, "AstralFlask 1"))
				centerprint(%clientId, "<jc>This water looks clean enough to sample.\n <f2>Use your flask now to collect some water.", 10);
		}
	}
	else if($Zone::Type[%z] == "FREEFORALL")
	{
		%msg = "You have entered " @ $Zone::Desc[%z] @ ".";
		%color = $MsgRed;
	}

	if($Zone::EnterSound[%z] != "")
		%msg = %msg @ "~w" @ $Zone::EnterSound[%z];

	if(%msg != "")
		Client::sendMessage(%clientId, %color, %msg);

	if(!Player::isAiControlled(%clientId))
		Game::refreshClientScore(%clientId);	//this is so players can see which zone this client is in

	Zone::onEnter(%clientId, %oldZone, %newZone);
}

function Zone::DoExit(%z, %clientId)
{
	dbecho($dbechoMode, "Zone::DoExit(" @ %z @ ", " @ %clientId @ ")");

	%zoneLeft = fetchData(%clientId, "zone");

	storeData(%clientId, "zone", "");

	if($Zone::Type[%z] == "PROTECTED")
	{
		%msg = "You have left " @ $Zone::Desc[%z] @ ".";
		%color = $MsgRed;
	}
	else if($Zone::Type[%z] == "DUNGEON")
	{
		%msg = "You have left " @ $Zone::Desc[%z] @ ".";
		%color = $MsgBeige;
	}
	else if($Zone::Type[%z] == "WATER")
	{
		%msg = "";
		if(fetchData(%clientId, "InWaterZone") != ""){
			storeData(%clientId, "InWaterZone", "");
			centerprint(%clientId, "", 1);
		}
	}
	else if($Zone::Type[%z] == "FREEFORALL")
	{
		%msg = "You have left " @ $Zone::Desc[%z] @ ".";
		%color = $MsgBeige;
	}

	//Repack zone exit
	if(%clientId.repack){
		remoteeval(%clientId,RSound,3);
		%clientId.MusicTicksLeft = 0;
	}

	if($Zone::ExitSound[%z] != "")
		%msg = %msg @ "~w" @ $Zone::ExitSound[%z];

	if(%msg != "")
	      Client::sendMessage(%clientId, %color, %msg);

	if(!Player::isAiControlled(%clientId))
		Game::refreshClientScore(%clientId);	//this is so players can see which zone this client is in

	Zone::onExit(%clientId, %zoneLeft);
}

function IsInBetween(%x, %r1, %r2)
{
	dbecho($dbechoMode, "IsInBetween(" @ %x @ ", " @ %r1 @ ", " @ %r2 @ ")");

	if(%r1 > %r2)
	{
		%tmp = %r1;
		%r1 = %r2;
		%r2 = %tmp;
	}
	if(%x >= %r1 && %x <= %r2)
		return True;
	else
		return False;
}

function Zone::getIndex(%z)
{
	dbecho($dbechoMode, "Zone::getIndex(" @ %z @ ")");

	if(%z != "")
	{
		for(%i = 1; %i <= $numZones; %i++)
		{
			if($Zone::FolderID[%i] == %z)
			{
				return %i;
			}
		}
	}
	return -1;
}
function Zone::getMarker(%z)
{
	dbecho($dbechoMode, "Zone::getMarker(" @ %z @ ")");

	if(%z != "")
	{
		for(%i = 1; %i <= $numZones; %i++)
		{
			if($Zone::FolderID[%i] == %z)
			{
				return $Zone::Marker[%i];
			}
		}
	}
	return -1;
}
function Zone::getType(%z)
{
	dbecho($dbechoMode, "Zone::getType(" @ %z @ ")");

	if(%z != "")
	{
		for(%i = 1; %i <= $numZones; %i++)
		{
			if($Zone::FolderID[%i] == %z)
			{
				return $Zone::Type[%i];
			}
		}
	}
	return -1;
}
function Zone::getDesc(%z)
{
	dbecho($dbechoMode, "Zone::getDesc(" @ %z @ ")");

	if(%z != "")
	{
		for(%i = 1; %i <= $numZones; %i++)
		{
			if($Zone::FolderID[%i] == %z)
			{
				return $Zone::Desc[%i];
			}
		}
	}
	return -1;
}
//Added in rpg 6.8function Zone::descToId(%z){	dbecho($dbechoMode, "Zone::getDesc(" @ %z @ ")");	for(%i = 1; %i <= $numZones; %i++)	{		if($Zone::Desc[%i] == %z)		{			return $Zone::FolderID[%i];		}	}	return "";}
function Zone::getEnterSound(%z)
{
	dbecho($dbechoMode, "Zone::getEnterSound(" @ %z @ ")");

	if(%z != "")
	{
		for(%i = 1; %i <= $numZones; %i++)
		{
			if($Zone::FolderID[%i] == %z)
			{
				return $Zone::EnterSound[%i];
			}
		}
	}
	return -1;
}
function Zone::getAmbientSound(%z)
{
	dbecho($dbechoMode, "Zone::getAmbientSound(" @ %z @ ")");

	if(%z != "")
	{
		for(%i = 1; %i <= $numZones; %i++)
		{
			if($Zone::FolderID[%i] == %z)
			{
				return $Zone::AmbientSound[%i];
			}
		}
	}
	return -1;
}
function Zone::getAmbientSoundPerc(%z)
{
	dbecho($dbechoMode, "Zone::getAmbientSoundPerc(" @ %z @ ")");

	if(%z != "")
	{
		for(%i = 1; %i <= $numZones; %i++)
		{
			if($Zone::FolderID[%i] == %z)
			{
				return $Zone::AmbientSoundPerc[%i];
			}
		}
	}
	return -1;
}
function Zone::getExitSound(%z)
{
	dbecho($dbechoMode, "Zone::getExitSound(" @ %z @ ")");

	if(%z != "")
	{
		for(%i = 1; %i <= $numZones; %i++)
		{
			if($Zone::FolderID[%i] == %z)
			{
				return $Zone::ExitSound[%i];
			}
		}
	}
	return -1;
}

function Zone::onEnter(%clientId, %oldZone, %newZone)
{
	dbecho($dbechoMode, "Zone::onEnter(" @ %clientId @ ", " @ %oldZone @ ", " @ %newZone @ ")");

	refreshHPREGEN(%clientId);	//this is because you regen faster or slower depending on the zone you are in

	if(Zone::getType(%newZone) != "WATER")
	{
		storeData(%clientId, "drownCounter", "");
	}
	if(Zone::getType(%newZone) == "PROTECTED")
	{
		if(fetchData(%clientId, "isMimic"))
		{
			storeData(%clientId, "RACE", Client::getGender(%clientId) @ "Human");
			storeData(%clientId, "isMimic", "");
			UpdateTeam(%clientId);
			RefreshAll(%clientId);

			playSound(AbsorbABS, GameBase::getPosition(%clientId));
		}
	}
}

function Zone::onExit(%clientId, %zoneLeft)
{
	dbecho($dbechoMode, "Zone::onExit(" @ %clientId @ ", " @ %zoneLeft @ ")");

	refreshHPREGEN(%clientId);	//this is because you regen faster or slower depending on the zone you are in
}

function GetNearestZone(%clientId, %zonetype, %returnType, %requireDrops)
{
	dbecho($dbechoMode, "GetNearestZone(" @ %clientId @ ", " @ %zonetype @ ", " @ %returnType @ ")");

	//%zonetype can be "town", "dungeon" or "freeforall"

	%closestDist = 500000;
	%closestZone = "";
	%mpos = "";
	%clpos = GameBase::getPosition(%clientId);

	for(%i = 1; %i <= $numZones; %i++)
	{
		
		%type = $Zone::Type[%i];
		if( %type == "PROTECTED" && String::ICompare(%zonetype, "town") == 0 ||
			%type == "DUNGEON" && String::ICompare(%zonetype, "dungeon") == 0 || 
			%type == "FREEFORALL" && String::ICompare(%zonetype, "freeforall") == 0 || 
			%zonetype == -1)
		{
			%finalpos = $Zone::Marker[%i];
	
			%dist = Vector::getDistance(%finalpos, %clpos);
			%valid = True;
			if(%requireDrops == True) {
				%drops = nameToID("MissionGroup\\Zones\\" @ Object::getName($Zone::FolderID[%i]) @ "\\DropPoints");
				if(%drops == -1) 							%valid = False;
				else if(Group::objectCount(%drops) <= 0) 	%valid = False;					
			}
			if(%valid && %dist < %closestDist)
			{
				%closestDist = %dist;
				%closestZoneDesc = $Zone::Desc[%i];
				%closestZone = $Zone::FolderID[%i];				
				%mpos = %finalpos;
			}
		}
	}

	if(%mpos == "")		//no zones were found (this means there are NO zones in the map...)
		return False;
	
	//%returnType:
	//1 = returns the distance from the client to the nearest zone
	//2 = returns the description of the zone nearest to the client
	//3 = returns the zone id of the zone nearest to the client
	//4 = returns the position of the middle of the zone nearest to the client

	if(%returnType == 1)
		return %closestDist;
	else if(%returnType == 2)
		return %closestZoneDesc;
	else if(%returnType == 3)
		return %closestZone;
	else if(%returnType == 4)
		return %mpos;
}


function GetZoneByKeywords(%clientId, %keywords, %returnType)
{
	dbecho($dbechoMode, "GetZoneByKeywords(" @ %clientId @ ", " @ %keywords @ ", " @ %returnType @ ")");

	%mpos = "";

	%group = nameToId("MissionGroup\\Zones");

	if(%group != -1)
	{
		//IMPORTANT: zone markers must be objects 0 and 1 in the zone's folder

		%count = Group::objectCount(%group);
		for(%i = 0; %i <= %count-1; %i++)
		{
			%object = Group::getObject(%group, %i);
			%system = Object::getName(%object);
			%type = GetWord(%system, 0);
			%desc = String::getSubStr(%system, String::len(%type)+1, 9999);
			if(%type == "PROTECTED" || %type == "DUNGEON" || %type == "FREEFORALL")
			{
				if(String::findSubStr(%desc, %keywords) != -1)
				{
					//get the two markers
					%tmpgroup = nameToId("MissionGroup\\Zones\\" @ %system);

					%m1pos = GameBase::getPosition(Group::getObject(%tmpgroup, 0));
					%m2pos = GameBase::getPosition(Group::getObject(%tmpgroup, 1));

					%mx = (((GetWord(%m2pos, 0) - GetWord(%m1pos, 0)) / 2) + GetWord(%m1pos, 0));
					%my = (((GetWord(%m2pos, 1) - GetWord(%m1pos, 1)) / 2) + GetWord(%m1pos, 1));
					%mz = (((GetWord(%m2pos, 2) - GetWord(%m1pos, 2)) / 2) + GetWord(%m1pos, 2));

					%mpos = %mx @ " " @ %my @ " " @ %mz;
					%dist = Vector::getDistance(%mpos, GameBase::getPosition(%clientId));

					//%returnType:
					//1 = returns the distance from the client to the zone
					//2 = returns the description of the zone
					//3 = returns the zone id
					//4 = returns the position of the middle of the zone

					if(%returnType == 1)
						return %dist;
					else if(%returnType == 2)
						return %desc;
					else if(%returnType == 3)
						return %object;
					else if(%returnType == 4)
						return %mpos;
				}
			}
		}
		return False;	
	}
	else
		return False;
}

function Zone::getNumPlayers(%z, %all)
{
	dbecho($dbechoMode, "Zone::getNumPlayers(" @ %z @ ", " @ %all @ ")");

	if(%all)
		%list = GetEveryoneIdList();
	else
		%list = GetPlayerIdList();

	%n = 0;
	for(%i = 0; GetWord(%list, %i) != -1; %i++)
	{
		%id = GetWord(%list, %i);

		if(fetchData(%id, "zone") == %z)
			%n++;
	}

	return %n;
}

function ObjectInWhichZone(%object)
{
	dbecho($dbechoMode, "ObjectInWhichZone(" @ %object @ ")");

	//not perfect but good enough

	%fid = "";
	%closest = 99999;
	%objpos = GameBase::getPosition(%object);
	for(%z = 1; %z <= $numZones; %z++)
	{
		%rad = ($Zone::Length[%z] + $Zone::Width[%z] + $Zone::Height[%z]) / 3;
		%dist = Vector::getDistance(%objpos, $Zone::Marker[%z]);
		if(%dist <= %rad)
		{
			if(%dist < %closest)
			{
				%closest = %dist;
				%fid = $Zone::FolderID[%z];
			}
		}
	}
	return %fid;
}

function Zone::getPlayerList(%z, %type)
{
	dbecho($dbechoMode, "Zone::getPlayerList(" @ %z @ ", " @ %type @ ")");

	if(%type == 1)
		%list = GetEveryoneIdList();
	else if(%type == 2)
		%list = GetPlayerIdList();
	else if(%type == 3)
		%list = GetBotIdList();

	%n = 0;
	%aa = "";
	for(%i = 0; GetWord(%list, %i) != -1; %i++)
	{
		%id = GetWord(%list, %i);

		if(fetchData(%id, "zone") == %z)
			%aa = %aa @ %id @ " ";
	}

	return %aa;
}

//Added in rpg 6.8
function findZoneFrom(%zone, %pos){	if($Zone::compassPoint[%zone, 0] == ""){		return $Zone::Marker[%zone];	}	%closestDist = 999999;	%closest = 0;	for(%c = 0; $Zone::compassPoint[%zone, %c] != ""; %c++){		%dist = vector::getdistance(%pos, $Zone::compassPoint[%zone, %c]);		if(%dist < %closestDist){			%closestDist = %dist;			%closest = %c;		}	}	%pos = $Zone::compassPoint[%zone, %closest];	return %pos;}

//Added in rpg 6.8//Unlike getZoneAt, this returns a string.function getWorldAt(%pos, %zone){	if($Zone::World[%zone] != ""){		return $Zone::World[%zone];	}	%z = "Unknown";	return %z;}

//Added in rpg 6.8function getZoneAt(%pos){	%xt = GetWord(%pos, 0);	%yt = GetWord(%pos, 1);	%zt = GetWord(%pos, 2);	%zoneflag = "";	//for(%zone = 1; %zone <= $numZones; %zone++)	for(%zone = $numZones; %zone > 0; %zone--)	{		%x = GetWord($Zone::Marker[%zone], 0);		%y = GetWord($Zone::Marker[%zone], 1);		%z = GetWord($Zone::Marker[%zone], 2);		%w = $Zone::Length[%zone]/2;		%l = $Zone::Width[%zone]/2;		%h = $Zone::Height[%zone]/2;		if(%xt > (%x - %w) && %xt < (%x + %w)){			if(%yt > (%y - %l) && %yt < (%y + %l)){				if(%zt > (%z - %h) && %zt < (%z + %h)){					%zoneflag = %zone;					break;				}			}		}	}	return %zoneflag;}
//Added in rpg 6.8function getNearestZones(%clpos, %dist){	for(%i = 1; %i <= $numZones; %i++)	{		%finalpos = findZoneFrom(%i, %clpos);		%curDist = Vector::getDistance(%finalpos, %clpos);		if(%curDist < %dist)		{			%ret = %ret@%i@" "@getNESWa(%clpos, %finalpos)@" "@%curDist@" ";		}	}	return %ret;}
