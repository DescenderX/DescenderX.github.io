//This file is part of Tribes RPG.
//Tribes RPG server side scripts
//New NPC Chat menu system written by Jason "phantom" Daley, tribesrpg.org

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

function ClearVariables(%clientId)
{
	dbecho($dbechoMode2, "ClearVariables(" @ %clientId @ ")");

	%name = Client::getName(%clientId);

	//clear variables

	deleteVariables("CharacterSaveData*");

	$possessedBy[%clientId] = "";

	//this is only for bots
	$BotFollowDirective[fetchData(%clientId, "BotInfoAiName")] = "";

	//clear directives
	$aidirectiveTable[%clientId, 99] = "";

	%clientId.IsInvalid = "";
	%clientId.currentShop = "";
	%clientId.currentBank = "";
	%clientId.currentSmith = "";
	%clientId.adminLevel = "";
	%clientId.lastWaitActionTime = "";
	%clientId.choosingGroup = "";
	%clientId.choosingClass = "";
	%clientId.possessId = "";
	%clientId.sleepMode = "";
	%clientId.lastSaveCharTime = "";
	%clientId.replyTo = "";
	%clientId.stealType = "";
	%clientId.lastTriggerTime = "";
	%clientId.lastFireTime = "";
	%clientId.lastItemPickupTime = "";
	%clientId.MusicTicksLeft = "";
	%clientId.doExport = "";
	%clientId.TryingToSteal = "";
	%clientId.lastGetWeight = "";
	%clientId.echoOff = "";
	%clientId.lastMissMessage = "";
	%clientId.lastMinePos = "";
	%clientId.bulkNum = "";
	%clientId.zoneLastPos = "";
	%clientId.roll = "";
	%clientId.lbnum = "";
	$numMessage[%clientId, 1] = "";
	$numMessage[%clientId, 2] = "";
	$numMessage[%clientId, 3] = "";
	$numMessage[%clientId, 4] = "";
	$numMessage[%clientId, 5] = "";
	$numMessage[%clientId, 6] = "";
	$numMessage[%clientId, 7] = "";
	$numMessage[%clientId, 8] = "";
	$numMessage[%clientId, 9] = "";
	$numMessage[%clientId, 0] = "";
	$numMessage[%clientId, numpad0] = "";
	$numMessage[%clientId, numpad1] = "";
	$numMessage[%clientId, numpad2] = "";
	$numMessage[%clientId, numpad3] = "";
	$numMessage[%clientId, numpad4] = "";
	$numMessage[%clientId, numpad5] = "";
	$numMessage[%clientId, numpad6] = "";
	$numMessage[%clientId, numpad7] = "";
	$numMessage[%clientId, numpad8] = "";
	$numMessage[%clientId, numpad9] = "";
	$numMessage[%clientId, "numpad/"] = "";
	$numMessage[%clientId, "numpad*"] = "";
	$numMessage[%clientId, "numpad-"] = "";
	$numMessage[%clientId, "numpad+"] = "";
	$numMessage[%clientId, numpadenter] = "";
	$numMessage[%clientId, b] = "";
	$numMessage[%clientId, g] = "";
	$numMessage[%clientId, h] = "";
	$numMessage[%clientId, m] = "";
	$numMessage[%clientId, c] = "";

	for(%i = 0; (%id = GetWord($TownBotList, %i)) != -1; %i++)
	{
		$state[%id, %clientId] = "";
		if($QuestCounter[%name, %id.name] != "")
			$QuestCounter[%name, %id.name] = "";
	}

	for(%i = 1; %i <= $maxDamagedBy; %i++)
		$damagedBy[%name, %i] = "";

	SetAllSkills(%clientId, "");

	ClearEvents(%clientId);

	deleteVariables("BonusState" @ %clientId @ "*");
	deleteVariables("BonusStateCnt" @ %clientId @ "*");

	deleteVariables("ClientData" @ %clientId @ "*");
}

function SaveCharacter(%clientId, %silent)
{
	dbecho($dbechoMode2, "SaveCharacter(" @ %clientId @ ")");

	//first pass check
	if(%clientId.isInvalid || !fetchData(%clientId, "HasLoadedAndSpawned"))
		return False;

	//second pass check, will cause 4 line flood if the client is invalid
	//only do this as a "last resort" test.  if the player is detected to be dead, then there shouldn't be a problem
	if(!IsDead(%clientId))
	{
		Player::incItemCount(%clientId, Tool);
		%x = Player::getItemCount(%clientId, Tool);
		Player::decItemCount(%clientId, Tool);
		%y = Player::getItemCount(%clientId, Tool);
		if(%x == %y)
			return False;
	}
	if(IsDead(%clientId))
		return;
	//third check for bots, they won't have passwords.
	if(fetchData(%clientId, "password") == "")
		return false;

	%name = Client::getName(%clientId);

	if(!IsDead(%clientId) && !IsInRoster(%clientId) && !IsInArenaDueler(%clientId))
	{
		storeData(%clientId, "campPos", GameBase::getPosition(%clientId));
		storeData(%clientId, "campRot", GameBase::getRotation(%clientId));
	}

	deleteVariables("CharacterSaveData*");

	Player::SetItemCount(%clientId, BeltItemTool, 0);

	//the second identifier in the array is either 0, 1, or 2
	//0: regular player variable
	//1: weapon/item
	//2: quest counters

	//the third identifier is simply for identifying what we're saving.

	if(!%silent)
		pecho("Saving character " @ %name @ " (" @ %clientId @ ")...");
	$CharacterSaveData[0, 1] = fetchData(%clientId, "RACE");
	$CharacterSaveData[0, 2] = fetchData(%clientId, "EXP");
	$CharacterSaveData[0, 3] = fetchData(%clientId, "campPos");
	$CharacterSaveData[0, 4] = fetchData(%clientId, "COINS");
	$CharacterSaveData[0, 5] = fetchData(%clientId, "isMimic");
	$CharacterSaveData[0, 6] = fetchData(%clientId, "BANK");
	$CharacterSaveData[0, 7] = Client::getName(%clientId);
	$CharacterSaveData[0, 8] = fetchData(%clientId, "grouplist");
	$CharacterSaveData[0, 9] = fetchData(%clientId, "defaultTalk");
	$CharacterSaveData[0, 10] = fetchData(%clientId, "password");
	$CharacterSaveData[0, 11] = fetchData(%clientId, "bounty");
	$CharacterSaveData[0, 12] = fetchData(%clientId, "inArena");
	$CharacterSaveData[0, 13] = fetchData(%clientId, "PlayerInfo");
	$CharacterSaveData[0, 14] = fetchData(%clientId, "deathmsg");
	//15 is done lower
	$CharacterSaveData[0, 16] = fetchData(%clientId, "BankStorage");
	$CharacterSaveData[0, 17] = fetchData(%clientId, "campRot");
	$CharacterSaveData[0, 18] = fetchData(%clientId, "HP");
	$CharacterSaveData[0, 19] = fetchData(%clientId, "MANA");
	$CharacterSaveData[0, 20] = fetchData(%clientId, "FAVORmode");
	$CharacterSaveData[0, 21] = fetchData(%clientId, "RemortStep");
	$CharacterSaveData[0, 22] = fetchData(%clientId, "FAVOR");
	$CharacterSaveData[0, 23] = $rpgver;
	$CharacterSaveData[0, 26] = fetchData(%clientId, "GROUP");
	$CharacterSaveData[0, 27] = fetchData(%clientId, "CLASS");
	$CharacterSaveData[0, 28] = fetchData(%clientId, "SPcredits");
	//$CharacterSaveData[0, 29] = "";
	$CharacterSaveData[0, 30] = GetHouseNumber(fetchData(%clientId, "MyHouse"));
	$CharacterSaveData[0, 31] = fetchData(%clientId, "RankPoints");
	$CharacterSaveData[0, 32] = fetchData(%clientId, "STOLEN");
	$CharacterSaveData[0, 33] = fetchData(%clientId, "DeathWithEQ");
	$CharacterSaveData[0, 34] = fetchData(%clientId, "LoginMessage");
	$CharacterSaveData[0, 35] = fetchData(%clientId, "CyclesSurvived");
	$CharacterSaveData[0, 36] = fetchData(%clientId, "EqualizerLaunch");
	
	$CharacterSaveData[10, 0] = $TheMandateDelivery[%name];
	if($TheMandateDeliveryTarget[%name] != "")
		$CharacterSaveData[10, 1] = $TheMandateDeliveryTarget[%name].name;
	else $CharacterSaveData[10, 1] = "";
	
	if($ShakedownTarget[%name] != "") {
		$CharacterSaveData[10, 2] = $ShakedownTarget[%name].name;
		$CharacterSaveData[10, 3] = $ShakedownWinnings[%name];
	} else {
		$CharacterSaveData[10, 2] = "";
		$CharacterSaveData[10, 3] = "";
	}
	

	//Key binds
	$CharacterSaveData[7, 1] = $numMessage[%clientId, 1];
	$CharacterSaveData[7, 2] = $numMessage[%clientId, 2];
	$CharacterSaveData[7, 3] = $numMessage[%clientId, 3];
	$CharacterSaveData[7, 4] = $numMessage[%clientId, 4];
	$CharacterSaveData[7, 5] = $numMessage[%clientId, 5];
	$CharacterSaveData[7, 6] = $numMessage[%clientId, 6];
	$CharacterSaveData[7, 7] = $numMessage[%clientId, 7];
	$CharacterSaveData[7, 8] = $numMessage[%clientId, 8];
	$CharacterSaveData[7, 9] = $numMessage[%clientId, 9];
	$CharacterSaveData[7, 0] = $numMessage[%clientId, 0];
	$CharacterSaveData[7, b] = $numMessage[%clientId, b];
	$CharacterSaveData[7, g] = $numMessage[%clientId, g];
	$CharacterSaveData[7, h] = $numMessage[%clientId, h];
	$CharacterSaveData[7, m] = $numMessage[%clientId, m];
	$CharacterSaveData[7, n] = $numMessage[%clientId, n];
	$CharacterSaveData[7, c] = $numMessage[%clientId, c];
	$CharacterSaveData[7, q] = $numMessage[%clientId, q];
	$CharacterSaveData[7, numpadenter] = $numMessage[%clientId, numpadenter];
	$CharacterSaveData[7, numpad0] = $numMessage[%clientId, numpad0];
	$CharacterSaveData[7, numpad1] = $numMessage[%clientId, numpad1];
	$CharacterSaveData[7, numpad2] = $numMessage[%clientId, numpad2];
	$CharacterSaveData[7, numpad3] = $numMessage[%clientId, numpad3];
	$CharacterSaveData[7, numpad4] = $numMessage[%clientId, numpad4];
	$CharacterSaveData[7, numpad5] = $numMessage[%clientId, numpad5];
	$CharacterSaveData[7, numpad6] = $numMessage[%clientId, numpad6];
	$CharacterSaveData[7, numpad7] = $numMessage[%clientId, numpad7];
	$CharacterSaveData[7, numpad8] = $numMessage[%clientId, numpad8];
	$CharacterSaveData[7, numpad9] = $numMessage[%clientId, numpad9];
	$CharacterSaveData[7, numpadslash] = $numMessage[%clientId, "numpad/"];
	$CharacterSaveData[7, numpadstar] = $numMessage[%clientId, "numpad*"];
	$CharacterSaveData[7, numpadminus] = $numMessage[%clientId, "numpad-"];
	$CharacterSaveData[7, numpadplus] = $numMessage[%clientId, "numpad+"];

	$CharacterSaveData[8, 1] = fetchData(%clientId, "AmmoItems");
	$CharacterSaveData[8, 2] = fetchData(%clientId, "BankAmmoItems");
	$CharacterSaveData[8, 3] = fetchData(%clientId, "MiscItems");
	$CharacterSaveData[8, 4] = fetchData(%clientId, "BankMiscItems");
	$CharacterSaveData[8, 5] = fetchData(%clientId, "PotionItems");
	$CharacterSaveData[8, 6] = fetchData(%clientId, "BankPotionItems");
	$CharacterSaveData[8, 7] = fetchData(%clientId, "GlassIdioms");
	$CharacterSaveData[8, 8] = fetchData(%clientId, "BankGlassIdioms");

	//skill variables
	%cnt = 0;
	for(%i = 1; %i <= GetNumSkills(); %i++)
	{
		$CharacterSaveData[4, %cnt++] = $PlayerSkill[%clientId, %i];
		$CharacterSaveData[4, %cnt++] = $SkillCounter[%clientId, %i];
	}

	//IP dump, for server admin look-up purposes
	$CharacterSaveData[0, 666] = Client::getTransportAddress(%clientId);

	%ii = 0;

	//determine which weapons player has

	if(!IsDead(%clientId))
	{
		%s = "";
		%max = getNumItems();
		for(%i = 0; %i < %max; %i++)
		{
			%checkItem = getItemData(%i);
			%itemcount = Player::getItemCount(%clientId, %checkItem);
			if(%itemcount > $maxItem)
				%itemcount = $maxItem;
			if(%itemcount > 0)
				%s = %s @ %checkItem @ " " @ %itemcount @ " ";
		}
		$CharacterSaveData[0, 15] = %s;
	}
	else
		$CharacterSaveData[0, 15] = fetchData(%clientId, "spawnStuff");

	%cnt = 0;
	%list = GetBotIdList();
	for(%i = 0; GetWord(%list, %i) != -1; %i++)
	{
		%id = GetWord(%list, %i);
		%aiName = fetchData(%id, "BotInfoAiName");

		if($QuestCounter[%name, %aiName] != "")
		{
			%cnt++;
			$CharacterSaveData[2, %cnt] = %aiName;
			$CharacterSaveData[3, %cnt] = $QuestCounter[%name, %aiName];
		}
	}

	//bonus state variables
	for(%i = 1; %i <= $maxBonusStates; %i++)
	{
		$CharacterSaveData[5, %i] = $BonusState[%clientId, %i];
		$CharacterSaveData[6, %i] = $BonusStateCnt[%clientId, %i];
	}

	export("CharacterSaveData*", "temp\\" @ %name @ ".cs", false);
	deleteVariables("CharacterSaveData*");

	Player::SetItemCount(%clientId, BeltItemTool, 1);

	if(!%silent)
		pecho("Save for " @ %name @ " (" @ %clientId @ ") complete.");
	else
		return "complete";

	return True;
}

function LoadCharacter(%clientId, %forceLoadByName)
{
	dbecho($dbechoMode2, "LoadCharacter(" @ %clientId @ ")");

	if(!%forceLoadByName) {
		ClearVariables(%clientId);
		%name = Client::getName(%clientId);
	} else {
		%name = %clientId;
	}
	%filename = %name @ ".cs";

	$ConsoleWorld::DefaultSearchPath = $ConsoleWorld::DefaultSearchPath;	//thanks Presto

	deleteVariables("CharacterSaveData*");

	if(isFile("temp\\" @ %filename))
	{
		//load character
		if(!%forceLoadByName) echo("Loading character " @ %name @ " (" @ %clientId @ ")...");

		for(%retry = 0; %retry < 5; %retry++)		//This might not be necessary, but it's to ensure that the
		{								//exec doesn't get flakey when there's lag.
			exec(%filename);
			if($CharacterSaveData[0, 1] != "")
				break;
		}
		if(!%forceLoadByName) {
			storeData(%clientId, "RACE", $CharacterSaveData[0, 1]);
			storeData(%clientId, "EXP", $CharacterSaveData[0, 2]);
			
			storeData(%clientId, "COINS", $CharacterSaveData[0, 4]);
			storeData(%clientId, "isMimic", $CharacterSaveData[0, 5]);
			storeData(%clientId, "BANK", $CharacterSaveData[0, 6]);
			storeData(%clientId, "tmpname", $CharacterSaveData[0, 7]);
			storeData(%clientId, "grouplist", $CharacterSaveData[0, 8]);
			storeData(%clientId, "defaultTalk", $CharacterSaveData[0, 9]);
			storeData(%clientId, "password", string::getSubStr($Client::info[%clientId, 5], 0, 210));
			storeData(%clientId, "bounty", $CharacterSaveData[0, 11]);
			storeData(%clientId, "inArena", $CharacterSaveData[0, 12]);
			storeData(%clientId, "PlayerInfo", $CharacterSaveData[0, 13]);
			storeData(%clientId, "deathmsg", $CharacterSaveData[0, 14]);
			storeData(%clientId, "spawnStuff", $CharacterSaveData[0, 15]);
			storeData(%clientId, "BankStorage", $CharacterSaveData[0, 16]);
			storeData(%clientId, "campRot", $CharacterSaveData[0, 17]);
			storeData(%clientId, "tmphp", $CharacterSaveData[0, 18]);
			storeData(%clientId, "tmpmana", $CharacterSaveData[0, 19]);
			storeData(%clientId, "FAVORmode", $CharacterSaveData[0, 20]);
			storeData(%clientId, "RemortStep", $CharacterSaveData[0, 21]);
			storeData(%clientId, "FAVOR", $CharacterSaveData[0, 22]);
			storeData(%clientId, "tmpLastSaveVer", $CharacterSaveData[0, 23]);
			storeData(%clientId, "GROUP", $CharacterSaveData[0, 26]);
			storeData(%clientId, "CLASS", $CharacterSaveData[0, 27]);
			storeData(%clientId, "SPcredits", $CharacterSaveData[0, 28]);
			//$CharacterSaveData[0, 29]);
			storeData(%clientId, "MyHouse", $HouseName[$CharacterSaveData[0, 30]]);
			storeData(%clientId, "RankPoints", $CharacterSaveData[0, 31]);
			storeData(%clientId, "STOLEN", $CharacterSaveData[0, 32]);
			
			if(rpg::IsTheWorldEnding() && rpg::GetItemListCount($CharacterSaveData[0, 15], "Equalizer") > 0) {
				storeData(%clientId, "DeathWithEQ", 1);
			} else {
				storeData(%clientId, "campPos", $CharacterSaveData[0, 3]);
				storeData(%clientId, "DeathWithEQ", $CharacterSaveData[0, 33]);
			}
			
			storeData(%clientId, "LoginMessage", $CharacterSaveData[0, 34]);
			storeData(%clientId, "CyclesSurvived", $CharacterSaveData[0, 35]);
			storeData(%clientId, "EqualizerLaunch", $CharacterSaveData[0, 36]);


			$numMessage[%clientId, 1] = $CharacterSaveData[7, 1];
			$numMessage[%clientId, 2] = $CharacterSaveData[7, 2];
			$numMessage[%clientId, 3] = $CharacterSaveData[7, 3];
			$numMessage[%clientId, 4] = $CharacterSaveData[7, 4];
			$numMessage[%clientId, 5] = $CharacterSaveData[7, 5];
			$numMessage[%clientId, 6] = $CharacterSaveData[7, 6];
			$numMessage[%clientId, 7] = $CharacterSaveData[7, 7];
			$numMessage[%clientId, 8] = $CharacterSaveData[7, 8];
			$numMessage[%clientId, 9] = $CharacterSaveData[7, 9];
			$numMessage[%clientId, 0] = $CharacterSaveData[7, 0];
			$numMessage[%clientId, b] = $CharacterSaveData[7, b];
			$numMessage[%clientId, g] = $CharacterSaveData[7, g];
			$numMessage[%clientId, h] = $CharacterSaveData[7, h];
			$numMessage[%clientId, m] = $CharacterSaveData[7, m];
			$numMessage[%clientId, n] = $CharacterSaveData[7, n];
			$numMessage[%clientId, c] = $CharacterSaveData[7, c];
			$numMessage[%clientId, q] = $CharacterSaveData[7, q];
			$numMessage[%clientId, numpadenter] = $CharacterSaveData[7, numpadenter];
			$numMessage[%clientId, numpad0] = $CharacterSaveData[7, numpad0];
			$numMessage[%clientId, numpad1] = $CharacterSaveData[7, numpad1];
			$numMessage[%clientId, numpad2] = $CharacterSaveData[7, numpad2];
			$numMessage[%clientId, numpad3] = $CharacterSaveData[7, numpad3];
			$numMessage[%clientId, numpad4] = $CharacterSaveData[7, numpad4];
			$numMessage[%clientId, numpad5] = $CharacterSaveData[7, numpad5];
			$numMessage[%clientId, numpad6] = $CharacterSaveData[7, numpad6];
			$numMessage[%clientId, numpad7] = $CharacterSaveData[7, numpad7];
			$numMessage[%clientId, numpad8] = $CharacterSaveData[7, numpad8];
			$numMessage[%clientId, numpad9] = $CharacterSaveData[7, numpad9];
			$numMessage[%clientId, "numpad/"] = $CharacterSaveData[7, numpadslash];
			$numMessage[%clientId, "numpad*"] = $CharacterSaveData[7, numpadstar];
			$numMessage[%clientId, "numpad-"] = $CharacterSaveData[7, numpadminus];
			$numMessage[%clientId, "numpad+"] = $CharacterSaveData[7, numpadplus];


			storeData(%clientId, "AmmoItems", $CharacterSaveData[8, 1]);
			storeData(%clientId, "BankAmmoItems", $CharacterSaveData[8, 2]);
			storeData(%clientId, "MiscItems", $CharacterSaveData[8, 3]);
			storeData(%clientId, "BankMiscItems", $CharacterSaveData[8, 4]);
			storeData(%clientId, "PotionItems", $CharacterSaveData[8, 5]);
			storeData(%clientId, "BankPotionItems", $CharacterSaveData[8, 6]);		
			storeData(%clientId, "GlassIdioms", $CharacterSaveData[8, 7]);
			storeData(%clientId, "BankGlassIdioms", $CharacterSaveData[8, 8]);		
		}
		
		if(!%forceLoadByName) {
			Belt::BankStorageConversion(%clientid);
		}

		$TheMandateDelivery[%name] = $CharacterSaveData[10, 0];
		%targetBotName = $CharacterSaveData[10, 1];
		for(%tb=0;(%thebot=getword($TownBotList,%tb))!=-1;%tb++) {
			if(%thebot.name == %targetBotName) {
				$TheMandateDeliveryTarget[%name] = %thebot;
				break;
			}
		}

		//skill variables
		if(!%forceLoadByName) {
			%cnt = 0;
			for(%i = 1; %i <= GetNumSkills(); %i++) {
				$PlayerSkill[%clientId, %i] = $CharacterSaveData[4, %cnt++];
				$SkillCounter[%clientId, %i] = $CharacterSaveData[4, %cnt++];
			}
			for(%i = 1; %i <= $maxBonusStates; %i++) {
				$BonusState[%clientId, %i] = $CharacterSaveData[5, %i];
				$BonusStateCnt[%clientId, %i] = $CharacterSaveData[6, %i];
			}
		}

		for(%i = 1; $CharacterSaveData[3, %i] != ""; %i++)
		{
			$QuestCounter[$CharacterSaveData[2, %i]] = $CharacterSaveData[3, %i];
		}


		//== VERSION CONVERSION ROUTINES ============================
		if(!%forceLoadByName) {
			if(fetchData(%clientId, "RemortStep") == "")
				storeData(%clientId, "RemortStep", 0);
			if(fetchData(%clientId, "FAVORmode") == "")
				storeData(%clientId, "FAVORmode", "death");
			if(fetchData(%clientId, "tmphp") == "")
				storeData(%clientId, "tmphp", 1);
			if(fetchData(%clientId, "tmpmana") == "")
				storeData(%clientId, "tmpmana", 1);
			if(fetchData(%clientId, "tmpname") == "")
				storeData(%clientId, "tmpname", %name);
		}

		//===========================================================
		
		if(!%forceLoadByName) 	echo("Load complete.");
		else					return %filename;
	}
	else if(!%forceLoadByName)
	{
		//give defaults
		echo("Giving defaults to new player " @ %clientId);
		storeData(%clientId, "RACE", Client::getGender(%clientId) @ "Human");
		storeData(%clientId, "EXP", 0);
		storeData(%clientId, "campPos", "");
		storeData(%clientId, "BANK", $initbankcoins);
		storeData(%clientId, "grouplist", "");
		storeData(%clientId, "defaultTalk", "#global");
		storeData(%clientId, "password", $Client::info[%clientId, 5]);
		storeData(%clientId, "FAVOR", $initialFavor);
		storeData(%clientId, "PlayerInfo", "");
		storeData(%clientId, "ignoreGlobal", "");
		storeData(%clientId, "FAVORmode", "death");
		storeData(%clientId, "tmphp", "");
		storeData(%clientId, "tmpmana", "");
		storeData(%clientId, "RemortStep", 0);
		storeData(%clientId, "tmpname", %name);
		storeData(%clientId, "tmpLastSaveVer", $rpgver);
		storeData(%clientId, "bounty", 0);
		storeData(%clientId, "isMimic", "");
		storeData(%clientId, "MyHouse", "");
		storeData(%clientId, "RankPoints", 0);

		%clientId.choosingGroup = True;

		SetAllSkills(%clientId, 0);
	} else return "";

	if(!%forceLoadByName) {
		storeData(%clientId, "NextHitBash", 0);
		storeData(%clientId, "Leaping", "");
		storeData(%clientId, "Charging", "");
		storeData(%clientId, "Swooping", "");

		if(%clientId.repack >= 14)
			remoteeval(%clientId, RepackKeyOverride, 2);
		
		deleteVariables("CharacterSaveData*");
	}
}

function ResetPlayer(%clientId)
{
	dbecho($dbechoMode2, "ResetPlayer(" @ %clientId @ ")");

	%name = Client::getName(%clientId);
	%filename = %name @ ".cs";

	File::delete("temp\\" @ %filename);

	LoadCharacter(%clientId);

	StartStatSelection(%clientId);
}