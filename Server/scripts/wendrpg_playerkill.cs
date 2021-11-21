//This file is part of Tribes RPG.
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

function Game::clientKilled(%playerId, %killerId) {
	%set = nameToID("MissionCleanup/ObjectivesSet");
	for(%i = 0; (%obj = Group::getObject(%set, %i)) != -1; %i++)
		GameBase::virtual(%obj, "clientKilled", %playerId, %killerId);
}

//_________________________________________________________________________________________________________________________________________________________________
// At this point, the client can still be queried for getItemCounts, and is also still an object
// Player::Kill calls this function
function Player::onKilled(%this) { 
	%clientId = Player::getClient(%this);
	%killerId = fetchData(%clientId, "tmpkillerid");
	storeData(%clientId, "tmpkillerid", "");
	%botCleanup = fetchData(%clientId, "BotCleanupNoEXP");
	%clientId.guiLock = true;
	Client::setGuiMode(%clientId, $GuiModePlay);
	UnHide(%clientId);

	if(%killerId > 0 && !%botCleanup) {
		if(%killerId != %clientId) {
			%n = Client::getName(%killerId);
			Client::sendMessage(%clientId, 0, "You were killed by " @ %n @ "!");
		}
		else if(%killerId == %clientId) {
			Client::sendMessage(%clientId, 0, "You killed yourself!");
		}
		
		DistributeExpForKilling(%clientId);

		//The player with the killshot gets the official "kill"
		if(!IsInCommaList(fetchData(%killerId, "TempKillList"), Client::getName(%clientId)))
			storeData(%killerId, "TempKillList", AddToCommaList(fetchData(%killerId, "TempKillList"), Client::getName(%clientId)));

		if(%killerId > 0 && Player::isAIcontrolled(%clientId) && %clientId != %killerId && !Player::isAIcontrolled(%killerId)) {
			for(%h="";%h<=5;%h++) {
				for(%x=0;%x<$TheHuntedMax[%h];%x++) {
					%botname = clipTrailingNumbers(fetchData(%clientId,"BotInfoAiName"));
					if ($TheHunted[%h, %x] == %botname) {
						$TheHuntedSpawn[%h, %x] = "";
						$TheHunted[%h, %x] = "";
						Client::sendMessage(%killerId, $MsgGreen, "You have killed your target, " @ rpg::getName(%clientId) );
						break;
					}
				}
			}
		}

		%stickyfingers = fetchData(%clientId, "STOLEN");
		if(%stickyfingers > 0 && %killerId != %clientId && !Player::isAIcontrolled(%clientId)) {
			%killedByVictim = false;
			%list=GetPlayerIdList();
			for(%i = 0; (%id = GetWord(%list, %i)) != -1; %i++) {
				for(%x=0;(%v=getword(%victims,%x)) != -1; %x++){ 
					if(rpg::getName(%id) == %v && %id != %clientId) {
						%killedByVictim = true;
						break;
					}
				}
			}
			%killedByLawman = (GetHouseNumber(fetchData(%killerId,"MyHouse")) == $HouseIndex["Keldrin Mandate"]);
			if(%killedByVictim || %killedByLawman) {
				storeData(%clientId, "COINS", %stickyfingers, "dec");
				if(fetchData(%clientId, "COINS") < 0)
					storeData(%clientId, "COINS", 0);	
				
				if(%killedByLawman) {			
					storeData(%clientId, "DeathByLawman", 1);	
					%stealerName 	= rpg::getName(%clientId);
					%victims 		= String::replace($VictimsOfTheft[%stealerName],","," ");
					%totalVictims 	= 0;			
					%list = GetPlayerIdList();
					for(%i = 0; (%id = GetWord(%list, %i)) != -1; %i++) {
						for(%x=0;(%v=getword(%victims,%x)) != -1; %x++){ 
							if(rpg::getName(%id) == %v && %id != %clientId) {
								%totalVictims++;
							}
						}
					}
					if(%totalVictims > 0) {
						%stickyfingers /= %totalVictims;		
						for(%i = 0; (%id = GetWord(%list, %i)) != -1; %i++) {
							for(%x=0;(%v=getword(%victims,%x)) != -1; %x++){
								if(rpg::getName(%id) == %v && %id != %clientId) {
									storeData(%id, "BANK", %stickyfingers, "inc");
									Client::sendMessage(%id, $MsgGreen, 
									"Your assailant '" @ rpg::getName(%clientId) @ "' has been brought to justice. The Mandate has deposited [ " @ %stickyfingers @ " ] COINS into your bank as compensation for losses.");
									break;
								}
							}
						}
					}
					if(Player::isAIcontrolled(%killerId))
						Player::Kill(%killerId);
				} else if(%killedByVictim && !Player::isAIcontrolled(%killerId)) {
					storeData(%clientId, "COINS", %stickyfingers, "dec");
					if(fetchData(%clientId, "COINS") < 0)
						storeData(%clientId, "COINS", 0);
					%pocketTheDifference = %stickyfingers * Cap(getRandom(), 0.25, 1);
					storeData(%killerId, "COINS", %pocketTheDifference, "inc");
					Client::sendMessage(%id, $MsgGreen, 
								"You've killed your assailant '" @ rpg::getName(%clientId) @ "'. You pick through their belongings and pocket [ " @ %pocketTheDifference @ " ] COINS.");
				}
				if(%killedByLawman || !Player::isAIcontrolled(%killerId))
					storeData(%clientId, "STOLEN", 0);
			}
		}
	}

	if(HasThisStuff(%clientId, "Equalizer 1"))
		storeData(%clientId, "DeathWithEQ", 1);

	%iscp = Player::isAIcontrolled(%clientId);	if(%iscp)	{	
		if(String::findSubStr(rpg::getName(%clientId),"MandatoryExec") != -1){
			$MandatoryLawmanChase[$MandatoryLawmanTarget[%clientId]] = "";
			$MandatoryLawmanTarget[%clientId] = "";
		}
		%AIname = fetchData(%clientId, "BotInfoAiName");
		storeData(%clientId, "frozen", True);		AI::setVar(%AIname, SpotDist, 0);
		AI::setVar(%aiName, attackMode, 0);		AI::newDirectiveRemove(%AIname, 99);		ai::callbackPeriodic(%aiName, 0, AI::Periodic);	} else if($MandatoryLawmanChase[rpg::getName(%clientId)] > 0) {
		Player::Kill($MandatoryLawmanChase[rpg::getName(%clientId)]);
	}

	//revert
	Client::setControlObject(%clientId.possessId, %clientId.possessId);
	Client::setControlObject(%clientId, %clientId);
	storeData(%clientId.possessId, "dumbAIflag", "");
	$possessedBy[%clientId.possessId] = "";

	if(IsStillArenaFighting(%clientId))
	{
		//player's dueling flag is still at ALIVE, make him DEAD

		%a = GetArenaDuelerIndex(%clientId);
		$ArenaDueler[%a] = GetWord($ArenaDueler[%a], 0) @ " DEAD";

		if(!Player::IsAiControlled(%clientId))
			%clientId.RespawnMeInArena = True;
	}
	else if(IsInRoster(%clientId))
	{
		//player was in the waiting room
		//the only way someone could have died in there is if a player was added
		//to the roster, and an AI was killed to make way for this player.
		//so don't drop lootbag

		if(Player::isAiControlled(%clientId)) //if it was an AI, remove him right away, the same AI never spawns back
			RemoveFromRoster(%clientId);
	}
	else if(fetchData(%clientId, "noDropLootbagFlag") || fetchData(%clientId, "FAVOR") >= 0) {		//do nothing
		Client::sendMessage(%clientId, $MsgGreen, "With FAVOR, your items remain.");
	}
	else if (rpg::PlayerIsEqualizer(%clientId)) {
		Client::sendMessage(%clientId, $MsgGreen, "As an Equalizer, your items remain.");
	}
	else if(!%botCleanup && !(Player::isAIcontrolled(%clientId) && Player::isAIcontrolled(%killerId) && 		// NEVER drop loot when BOTH players are AI...
				(GameBase::getTeam(%clientId) == 0 ||											// AI kills player summon 	= never drop loot
					(GameBase::getTeam(%clientId) > 0 && GameBase::getTeam(%killerId) > 0))		// AI vs AI on enemy teams 	= never drop loot
	)) {
		%tmploot 	= "";
		%luckfactor = Cap((GetSkillWithBonus(%killerid,$SkillLuck) + 75) / 10, 1, 75);

		if(fetchData(%clientId, "COINS") > 0)
			%tmploot = %tmploot @ "COINS " @ floor(fetchData(%clientId, "COINS")) @ " ";
		storeData(%clientId, "COINS", 0);

		%max = getNumItems();
		for (%i = 0; %i < %max; %i++)
		{
			%a = getItemData(%i);
			%itemcount = Player::getItemCount(%clientId, %a);

			// AI getting the last hit on another AI will yield no drop in most cases
			if(Player::isAiControlled(%killerid) && Player::isAiControlled(%clientId)){
				if(getRandom() * 100 > 10)
					%itemcount = 0;
			}

			if(%itemcount)
			{
				%flag = True;
				
				if(($ItemDropFlags[%a] & $ItemDropNever))
					%flag = False;

				if(%flag)
				{
					%b = %a;
					if(%b.className == "Equipped")
						%b = String::getSubStr(%b, 0, String::len(%b)-1);
					%randroll = getRandom() * 100;
					%witholdItem = false;
					if(Player::isAiControlled(%clientId)) {					
						if(!($ItemDropFlags[%b] & $ItemDropIgnoreLuck)) {
							%witholdItem = (Player::isAiControlled(%killerid) || (%luckfactor < %randroll));
						}
					}
					
					if(!%witholdItem) {
						if(Player::getMountedItem(%clientId, $WeaponSlot) == %a)
						{
							//special handling for currently held weapon
							%tmploot = %tmploot @ %b @ " 1 ";
							Player::decItemCount(%clientId, %a);
						}
						else
						{
							%tmploot = %tmploot @ %b @ " " @ Player::getItemCount(%clientId, %a) @ " ";
							Player::setItemCount(%clientId, %a, 0);
						}
					}
					
					if(!Player::isAIcontrolled(%clientId)) {
						%amt = rpg::GetItemListCount(fetchData(%clientId, "BankStorage"), %b);
						storeData(%clientId, "BankStorage", rpg::ModifyItemList(fetchData(%clientId, "BankStorage"), %b, %amt + %itemcount));
						%tmploot = "";
					}					
					else if(Player::isAIcontrolled(%clientId) && String::len(%tmploot) > 200) {
						TossLootbag(%clientId, %tmploot, 1, "*", 300);
						%tmploot = "";
					}
				}
			}
		}
		%weapon = Player::getMountedItem(%clientId,$WeaponSlot);		if(%weapon != -1) {			Player::unMountItem(%clientId,$WeaponSlot);		}
				%beltstuff 	= Belt::GetDeathItems(%clientid, %killerId);
		%tmploot	= rpg::ApplyLuckFactorToDropList(%clientId, %killerid, %tmploot);
		%beltstuff	= rpg::ApplyLuckFactorToDropList(%clientId, %killerid, %beltstuff);
				if((String::len(%tmploot) + String::len(%beltstuff)) < 200)			%tmploot = %tmploot @ %beltstuff;		else {			if(Player::isAiControlled(%clientId))				TossLootbag(%clientId, %beltstuff, 1, "*", 300);			else if(fetchData(%clientId, "FAVOR") <= 0)				Client::sendMessage(%clientId, $MsgRed, "You have no FAVOR. Your items are sealed in the bank.");			%beltstuff = "";		}

		if(%tmploot != "" && %tmploot != " ")
		{
			if(Player::isAiControlled(%clientId))
				TossLootbag(%clientId, %tmploot, 1, "*", 300);
			else
			{
				%namelist = Client::getName(%clientId) @ ",";
				if(fetchData(%clientId, "FAVOR") >= 0)
					TossLootbag(%clientId, %tmploot, 5, %namelist, Cap(fetchData(%clientId, "LVL") * 300, 300, 3600));
				else
					TossLootbag(%clientId, %tmploot, 5, %namelist, Cap(fetchData(%clientId, "LVL") * 0.2, 5, "inf"));
			}
		}			
	}
	updateSpawnStuff(%clientId);	
	
	if(!%botCleanup && %killerId > 0) {
		//house stuff
		%victimH = fetchData(%clientId, "MyHouse");
		%killerH = fetchData(%killerId, "MyHouse");
		%vhn = GetHouseNumber(%victimH);
		%khn = GetHouseNumber(%killerH);
		if(%vhn != "")
		{
			//victim loses two rank points
			Client::sendMessage(%clientId, $MsgWhite, "You lost 2 Rank Points.");
			storeData(%clientId, "RankPoints", 2, "dec");

			if(%khn != "")
			{
				if(%khn != %vhn)
				{
					//both contenders are in a house, different from each other
					Client::sendMessage(%killerId, $MsgWhite, "You gained 1 Rank Point!");
					storeData(%killerId, "RankPoints", 1, "inc");
				}
				else
				{
					//both contenders are in the same house, happens if one target-lists the other.
					Client::sendMessage(%killerId, $MsgWhite, "You lost 1 Rank Point.");
					storeData(%killerId, "RankPoints", 1, "dec");
				}
			}
		}
	}
	//CLEAR!!!!
	if(!IsInArenaDueler(%clientId) && !Player::isAiControlled(%clientId) && fetchData(%clientId, "FAVOR") < 0)
		storeData(%clientId, "zone", "");	//so the player spawns back at start points

	if(fetchData(%clientId, "deathmsg") != "")
	{
		%kitem = Player::getMountedItem(%killerId, $WeaponSlot);
		%msg = nsprintf(fetchData(%clientId, "deathmsg"), Client::getName(%killerId), Client::getName(%clientId), %kitem.description);
		internalSay(%clientId, 0, %msg);
	}

	if(Player::isAiControlled(%clientId))
	{
		//event stuff
		%i = GetEventCommandIndex(%clientId, "onkill");
		if(%i != -1)
		{
			%name = GetWord($EventCommand[%clientId, %i], 0);
			%type = GetWord($EventCommand[%clientId, %i], 1);
			%cl = NEWgetClientByName(%name);
			if(%cl == -1)
				%cl = 2048;

			%cmd = String::NEWgetSubStr($EventCommand[%clientId, %i], String::findSubStr($EventCommand[%clientId, %i], ">")+1, 99999);
			%pcmd = ParseBlockData(%cmd, %clientId, %killerid);
			$EventCommand[%clientId, %i] = "";
			schedule("internalSay(" @ %cl @ ", 0, \"" @ %pcmd @ "\", \"" @ %name @ "\");", 2);
		}
		ClearEvents(%clientId);
	}

	storeData(%clientId, "noDropLootbagFlag", "");

	storeData(%clientId, "SpellCastStep", "");
	%clientId.sleepMode = "";
	refreshHPREGEN(%clientId);
	refreshMANAREGEN(%clientId);

	Client::setControlObject(%clientId, %clientId);
	storeData(%clientId, "dumbAIflag", "");

	//remember the last zone the player was in.
	storeData(%clientId, "lastzone", fetchData(%clientId, "zone"));
	
	PlaySound(RandomRaceSound(fetchData(%clientId, "RACE"), Death), GameBase::getPosition(%clientId));

	if(%clientId != -1) {
		if(%this.vehicle != "") {
			if(%this.driver != "") {
				%this.driver = "";
				Client::setControlObject(Player::getClient(%this), %this);
				Player::setMountObject(%this, -1, 0);
			} else {
				%this.vehicle.Seat[%this.vehicleSlot-2] = "";
				%this.vehicleSlot = "";
			}
			%this.vehicle = "";
		}
		if(!Player::isAiControlled(%clientId)){
			%clientId.dead = 1;
			if($AutoRespawn > 0)
				schedule("Game::autoRespawn(" @ %clientId @ ");",$AutoRespawn,%clientId);

			Player::setDamageFlash(%this,0.75);
			Client::setOwnedObject(%clientId, -1);
			Client::setControlObject(%clientId, Client::getObserverCamera(%clientId));
			Observer::setOrbitObject(%clientId, %this, 15, 15, 15);
			//.properties can't be set on AI
			%clientId.observerMode = "dead";
			%clientId.dieTime = getSimTime();
		}
		storeData(%clientId, "isDead", True);
		schedule("GameBase::startFadeOut(" @ %this @ ");", $CorpseTimeoutValue, %this);
		schedule("deleteObject(" @ %this @ ");", $CorpseTimeoutValue + 2.5, %this);
	}
}

function remoteKill(%clientId)
{

	if(!$matchStarted)
		return;

	%player = Client::getOwnedObject(%clientId);
	if(%player != -1 && getObjectType(%player) == "Player" && !IsDead(%clientId) && IsInRoster(%clientId) == False)
	{
		if(!rpg::PlayerIsEqualizer(%clientId)) {
			storeData(%clientId, "FAVOR", 1, "dec");
			if(fetchData(%clientId, "FAVOR") >= 0)
				Client::sendMessage(%clientId, $MsgRed, "You have permanently lost FAVOR!");
		}
		playNextAnim(%clientId);
		Player::kill(%clientId);
	}
}
