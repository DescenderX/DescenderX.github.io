//This file is part of Tribes RPG.
//Written by Jason "phantom" Daley,  Matthiew "JeremyIrons" Bouchard, and more (yet undetermined)

//	Copyright (C) 2016  Jason Daley

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



function Game::pickObserverSpawn(%clientId) {
	%group = nameToID("MissionGroup\\ObserverDropPoints");
	%count = Group::objectCount(%group);

	if(%group == -1 || !%count)
		%group = nameToID("MissionGroup\\Teams\\team0\\DropPoints");
	%count = Group::objectCount(%group);
	if(%group == -1 || !%count)
		%group = nameToID("MissionGroup\\Teams\\team1\\DropPoints");
	%count = Group::objectCount(%group);
	if(%group == -1 || !%count)
		return -1;
	%spawnIdx = %clientId.lastObserverSpawn + 1;
	if(%spawnIdx >= %count)
		%spawnIdx = 0;
	%clientId.lastObserverSpawn = %spawnIdx;

	return Group::getObject(%group, %spawnIdx);
}

function Game::pickPlayerSpawn(%clientId, %respawn) {	
	if(%respawn) {		
		%group = nameToID("MissionGroup/Teams/team0/DropPoints/OnDeath" );
		if(fetchData(%clientId, "DeathByLawman") == 1) {
			storeData(%clientId, "DeathByLawman", "");	
			%group = nameToID("MissionGroup/LawmanArrest" );
			Client::sendMessage(%clientId, $MsgRed, "You were killed by a lawman and have been revived to serve your sentence - life! You must escape, somehow...");
		}
		else if (fetchData(%clientId, "DeathByThugs") == 1 || Zone::getType(fetchData(%clientId,"lastzone")) == "DUNGEON") {
			%favor = fetchData(%clientId,"FAVOR");			
			if(fetchData(%clientId, "DeathByThugs") == 1 || %favor <= 0) {
				%captureChance = GetLevel(fetchData(%clientId, "EXP"), %clientId) * getRandom();
				%captureResist = (fetchData(%clientId, "MaxWeight") + GetSkillWithBonus(%clientId,$SkillLuck)) / 100;
				if(fetchData(%clientId, "DeathByThugs") == 1 || %captureChance > %captureResist) {
					%group = nameToID("MissionGroup/FavorlessCapture" );
					Client::sendMessage(%clientId, $MsgRed, "You were found dead by Outlaws; revived; and sold as fodder!");
				}
			}
			storeData(%clientId, "DeathByThugs", "");	
		}
	}
	else {
		%group = nameToID("MissionGroup/Teams/team0/DropPoints/" @ fetchData(%clientId, "CLASS") );
	}

	%count = Group::objectCount(%group);
	if(!%count)
		return -1;
	%spawnIdx = floor(getRandom() * (%count - 0.1));
	%value = %count;

	for(%i = %spawnIdx; %i < %value; %i++)
	{
		%set = newObject("set",SimSet);
		%obj = Group::getObject(%group, %i);
		if(containerBoxFillSet(%set,$SimPlayerObjectType|$VehicleObjectType,GameBase::getPosition(%obj),2,2,4,0) == 0)
		{
			deleteObject(%set);
			return %obj;		
		}
		if(%i == %count - 1)
		{
			%i = -1;
			%value = %spawnIdx;
		}
		deleteObject(%set);
	}
	return -1;
}

function Game::playerSpawn(%clientId, %respawn)
{

	if(!$ghosting)
		return false;

	Client::clearItemShopping(%clientId);
	Client::clearItemBuying(%clientId);

	if(fetchData(%clientId, "isMimic")) {
		storeData(%clientId, "RACE", Client::getGender(%clientId) @ "Human");
		storeData(%clientId, "isMimic", "");
	}

	if(%clientId.RespawnMeInArena) {
		%group = nameToID("MissionGroup\\TheArena\\TeleportEntranceMarkers");
		if(%group != -1) {
			%num = Group::objectCount(%group);
			%r = floor(getRandom() * %num);
			%spawnMarker = Group::getObject(%group, %r);
		}
		else %spawnMarker = Game::pickPlayerSpawn(%clientId, %respawn);
		
		RefreshArenaTextBox(%clientId);
	}
	else
	{
		%spawnMarker = Game::pickPlayerSpawn(%clientId, %respawn);

		//the player is spawning normally, ie. not in the arena
		storeData(%clientId, "inArena", "");
		CloseArenaTextBox(%clientId);
	}
	

	%clientId.guiLock = "";
	%clientId.dead = "";
	if(fetchData(%clientId, "campPos") != "" && !%respawn) {
		//if the player HAS a campPos and it is his FIRST TIME SPAWNING, then spawn him at this campPos
		%spawnPos = fetchData(%clientId, "campPos");
		%spawnRot = fetchData(%clientId, "campRot");
	}
	else if(%spawnMarker == -1 || rpg::IsTheWorldEnding()) {
		if(fetchData(%clientId, "DeathWithEQ") > 0) {			
			storeData(%clientId, "DeathWithEQ", "");			
			%spawnPos = "-2428.75 -267.75 77.5942";
			%spawnRot = "0 0 0";
			%rpos = Vector::Random(1300);
			%spawnPos = Vector::Add(NewVector(vecx(%rpos), vecy(%rpos), "1500"), %spawnPos);
		} else if( fetchData(%clientId,"FAVOR") > 0 ) {
			%spawnPos = "-2428.75 -267.75 77.5942";
			%spawnRot = "0 0 0";
			%rpos = Vector::Random(1300);
			%spawnPos = rpg::TraceForGround(Vector::Add(NewVector(vecx(%rpos), vecy(%rpos), "1000"), %spawnPos), -1500, true);
		}
		else if(%spawnMarker != -1){
			%spawnPos = GameBase::getPosition(%spawnMarker);
			%spawnRot = GameBase::getRotation(%spawnMarker);
		}
	} else {
		%spawnPos = GameBase::getPosition(%spawnMarker);
		%spawnRot = GameBase::getRotation(%spawnMarker);
	}

	%race = fetchData(%clientId, "RACE");
	%armor = $RaceArmorType[%race];	if(%armor.mass == False)		%armor = "MaleHumanArmor0";
	if(%race == "DeathKnight" || %armor == "DeathKnight22")
		ChangeRace(%clientId, "Human");
	
	if(rpg::IsTheWorldEnding()) {
		if(string::findSubStr($WorldEndParticipants, rpg::getName(%clientId) @ ",") == -1) {
			if(rpg::BrokersRemaining() <= 0 &&									// Client JUST joined for the first time and the brokers are already dead
				!rpg::PlayerIsEqualizer(%clientId)) {							// They don't have an equalizer either
				%spawnPos = rpg::SavePlayerFromTheEnd(%clientId, "returnPos");	// They never had a chance to help! Save them automatically.
				%spawnRot = "0 0 0";
			}
		}
		rpg::AddClientToWorldEnd(%clientId);
	}
	
	%pl = spawnPlayer(%armor, %spawnPos, %spawnRot);
	//Client::setControlObject(%clientId, Client::getObserverCamera(%clientId));
	PlaySound(SoundSpawn2, %spawnPos);
	rpg::UpdateVisibleManagedZones();
	GameBase::startFadeIn(Client::getOwnedObject(%clientId));
	
	echo("SPAWN: cl:" @ %clientId @ " pl:" @ %pl @ " marker:" @ %spawnMarker @ " position: " @ %spawnPos @ " armor:" @ %armor);
	
	%loginMessage = fetchData(%clientId,"LoginMessage");
	if (%loginMessage != "") {
		Client::sendMessage(%clientId, $MsgBeige, "----------------------------------------------------");
		Client::sendMessage(%clientId, $MsgBeige, %loginMessage);
		Client::sendMessage(%clientId, $MsgBeige, "----------------------------------------------------");		
		centerprint(%clientId, %loginMessage, string::len(%loginMessage) * 0.75);
		storeData(%clientId,"LoginMessage","");
	}
	
	%equalizerLaunch = fetchData(%clientId,"EqualizerLaunch");
	if(%equalizerLaunch > 0) {
		rpg::EqualizerLaunchSequence(%clientId);
	}
	
	if(%pl != -1)
	{
		UpdateTeam(%clientId);
		GameBase::setTeam(%pl, Client::getTeam(%clientId));
		Client::setOwnedObject(%clientId, %pl);
		Client::setControlObject(%clientId, %pl);
		
		storeData(%clientId, "HasLoadedAndSpawned", True);

		if(%clientId.RespawnMeInArena) {
			RestorePreviousEquipment(%clientId);
			%clientId.RespawnMeInArena = "";
		} else {
			GiveThisStuff(%clientId, fetchData(%clientId, "spawnStuff"), False);
		}

		if(fetchData(%clientId, "FAVOR") < 0)
			storeData(%clientId, "FAVOR", 0);

		player::setitemcount(%clientId,BeltItemTool, 1);
		RefreshAll(%clientId);
				%spawnhp = 1;		%spawnmana = 1;
		if(%respawn)	      
		{			%spawnhp = fetchData(%clientId, "MaxHP");			%spawnmana = fetchData(%clientId, "MaxMANA");
		}
		else
		{
			%spawnhp = fetchData(%clientId, "tmphp");			%spawnmana = fetchData(%clientId, "tmpmana");
			storeData(%clientId, "tmphp", "");
			storeData(%clientId, "tmpmana", "");
		}		if(%spawnhp < 1)	%spawnhp = 1;		if(%spawnmana < 1)	%spawnmana = 1;		setHP(%clientId, %spawnhp);		setMANA(%clientId, %spawnmana);
		storeData(%clientId.possessId, "dumbAIflag", "");
		storeData(%clientId, "isDead", False);		
	}
	schedule("afterspawnstuff("@%clientId@");",0.1);
	schedule("GameBase::setPosition(" @ %clientId @ ", \"" @ %spawnPos @ "\");", 0.2);
	
	return true;
}
function afterspawnstuff(%clientId){	if(isDead(%clientId))		return;
	if(%clientId.hasSpawned)		return;	%clientId.hasSpawned = True;
	%name = rpg::getname(%clientId);	for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))	{		if(%cl.repack >= 24){			Client::sendMessage(%cl, $MsgWhite, %name@" has entered the world.");		}	}
	schedule("repackAlert("@%clientId@");",1.0);	if(%clientId.repack > 29)		initEscText(%clientId);
}

function Game::autoRespawn(%clientId) {
	if(%clientId.dead == 1)
		Game::playerSpawn(%clientId, True);
}