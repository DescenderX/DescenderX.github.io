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

//_________________________________________________________________________________________________________________________________
// DescX Notes:
//		Moved several variables bits exec'd by the engine here,
//		in order to delete files that were small, relevant to Server, or not in use.
if($pref::lastMission == "")
   $pref::lastMission = rpgext;
$console::logmode = 1;
$Server::TeamDamageScale = 1;
$Server::JoinMOTD = "";
$Server::AutoAssignTeams = "true";
$Server::CurrentMaster = "0";
$Server::FileURL = "https://tribesrpg.org/worldender/";
$Server::FloodProtectionEnabled = "true";
$Server::HostPublicGame = "false";
$Server::Info = "https://tribesrpg.org/worldender/\nServer Admin: DescX\nBased on phantom's 'LTS' @ TribesRPG.org";
$Server::MasterAddressN0 = "t1m1.pu.net:28000 tribes.lock-load.org:28000 t1m1.kigen.co:28000 t1m2.kigen.co:28000 t1m1.tribesmasterserver.com:28000 t1m1.tribes0.com:28000 t1m1.masters.dynamix.com:28000 t1m2.masters.dynamix.com:28000 t1m3.masters.dynamix.com:28000";
$Server::MasterAddressN1 = "t1m1.pu.net:28000 tribes.lock-load.org:28000 t1m1.kigen.co:28000 t1m2.kigen.co:28000 t1m1.tribesmasterserver.com:28000 t1m1.tribes0.com:28000 t1ukm1.masters.dynamix.com:28000 t1ukm2.masters.dynamix.com:28000 t1ukm3.masters.dynamix.com:28000";
$Server::MasterAddressN2 = "t1m1.pu.net:28000 tribes.lock-load.org:28000 t1m1.kigen.co:28000 t1m2.kigen.co:28000 t1m1.tribesmasterserver.com:28000 t1m1.tribes0.com:28000 t1aum1.masters.dynamix.com:28000 t1aum2.masters.dynamix.com:28000 t1aum3.masters.dynamix.com:28000";
$Server::MasterName0 = "US Tribes Master";
$Server::MasterName1 = "UK Tribes Master";
$Server::MasterName2 = "Australian Tribes Master";
$Server::MaxPlayers = 13;
$Server::MinVotes = "1";
$Server::MinVotesPct = "0.5";
$Server::MinVoteTime = "45";
$server::modinfo = $Server::Info;
$Server::numMasters = "9";
$Server::Password = "password";
$Server::Port = "28002";
$Server::respawnTime = "0";
$Server::TeamDamageScale = "1";
$Server::teamName0 = "Citizen";
$Server::teamName1 = "Enemy";
$Server::teamName2 = "Greenskins";
$Server::teamName3 = "Gnoll";
$Server::teamName4 = "Undead";
$Server::teamName5 = "Elf";
$Server::teamName6 = "Minotaur";
$Server::teamName7 = "Golem";
$Server::teamNamePlural0 = "Citizens";
$Server::teamNamePlural1 = "Enemies";
$Server::teamNamePlural2 = "Greenskins";
$Server::teamNamePlural3 = "Gnolls";
$Server::teamNamePlural4 = "Undead";
$Server::teamNamePlural5 = "Elves";
$Server::teamNamePlural6 = "Minotaurs";
$Server::teamNamePlural7 = "Golems";
$Server::teamSkin0 = "rpgbase";
$Server::teamSkin2 = "rpgorc";
$Server::teamSkin3 = "rpggnoll";
$Server::teamSkin4 = "undead";
$Server::teamSkin5 = "rpgelf";
$Server::teamSkin6 = "min";
$Server::teamSkin7 = "fedmonster";
$Server::timeLimit = "0";
$Server::TourneyMode = "false";
$Server::VoteAdminWinMargin = "0.659999";
$Server::VoteFailTime = "30";
$Server::VoteWinMargin = "0.549999";
$Server::VotingTime = "20";
$Server::warmupTime = "10";
$Server::XLMasterN0 = "IP:18.218.30.7:28000";
$Server::XLMasterN1 = "IP:66.39.167.52:28000";
$Server::XLMasterN2 = "IP:97.105.140.115:28000";
$Server::XLMasterN3 = "IP:97.105.140.115:28000";
$Server::XLMasterN4 = "IP:66.39.167.52:28000";
$Server::XLMasterN5 = "IP:173.27.47.107:28000";
$Server::XLMasterN6 = "IP:216.249.100.66:28000";
$Server::XLMasterN7 = "IP:209.223.236.114:28000";


function dbecho(%mode, %s){
	//echo(%s);
}
function pecho(%m) {	echo(String::getSubStr(%m, 0, 250));}

function rp_include(%file){
	if(!$fileLoaded[%file])
		exec(%file);
	$fileLoaded[%file] = True;
}

function createTrainingServer()
{
	dbecho($dbechoMode, "createTrainingServer()");

	$SinglePlayer = true;
	createServer($pref::lastTrainingMission, false);
}

function remoteSetCLInfo(%clientId, %skin, %name, %email, %tribe, %url, %info, %autowp, %enterInv, %msgMask, %rpv)
{
	dbecho($dbechoMode, "remoteSetCLInfo(" @ %clientId @ ", " @ %skin @ ", " @ %name @ ", " @ %email @ ", " @ %tribe @ ", " @ %url @ ", " @ %info @ ", " @ %autowp @ ", " @ %enterInv @ ", " @ %msgMask @ ")");

   $Client::info[%clientId, 0] = %skin;
   $Client::info[%clientId, 1] = %name;
   $Client::info[%clientId, 2] = %email;
   $Client::info[%clientId, 3] = %tribe;
   $Client::info[%clientId, 4] = %url;
   $Client::info[%clientId, 5] = %info;
   if(%autowp)
      %clientId.autoWaypoint = true;
   if(%enterInv)
      %clientId.noEnterInventory = true;
   if(%msgMask != "")
      %clientId.messageFilter = %msgMask;

	if(%rpv == ""){		if(%info == ""){//Reasonably certain they don't have it.			newKick(%clientId, "You must download RPG mod to play here. Download site: www.TribesRPG.org");		}		else{			remoteEval(%clientId, MODInfo, "Your copy of RPG may be out of date. Please visit www.TribesRPG.org to update.");		}	}	%flag = False;	%list = GetPlayerIdList();	for(%i = 0; (%id = GetWord(%list, %i)) != -1; %i++){		%n = rpg::getName(%id);		if(String::Compare(%n, %clname) == 0 && %id != %clientId){			%eflag = True;			if(string::compare(fetchData(%id,"Password"), %info) == 0){				%flag = True;				break;			}		}	}	if(%flag){
		newKick(%id);
		newKick(%clientId, "Ghosted player dropped, please reconnect.");	}	else if(%eflag)	{		//This only happens if the player connecting a duplicate name doesn't		//share a password with the existing player.		//Makes sense to give them a little time out for bad behaviour.		%hisip = Client::getTransportAddress(%clientId);		BanList::add(%hisip, 30);	}
}

function Server::storeData()
{
	dbecho($dbechoMode, "Server::storeData()");

   $ServerDataFile = "serverTempData" @ $Server::Port @ ".cs";

   export("Server::*", "temp\\" @ $ServerDataFile, False);
   export("pref::lastMission", "temp\\" @ $ServerDataFile, true);
   EvalSearchPath();
}

function Server::refreshData()
{
	dbecho($dbechoMode, "Server::refreshData()");

   exec($ServerDataFile);  // reload prefs.
   checkMasterTranslation();
   Server::loadMission($pref::lastMission, false);
}

function Game::EndFrame()
{
}


function translateMasters()
{
   for(%i = 0; (%word = getWord($Server::MasterAddressN[$Server::CurrentMaster], %i)) != -1; %i++)
      %mlist[%i] = %word;

   $Server::numMasters = DNet::resolveMasters(%mlist0, %mlist1, %mlist2, %mlist3, %mlist4, %mlist5, %mlist6, %mlist7, %mlist8, %mlist9);
}


$Console::LastLineTimeout = 0;
$Console::updateMetrics = false;

function createServer(%mission, %dedicated)
{
	dbecho($dbechoMode2, "createServer(" @ %mission @ ", " @ %dedicated @ ")");

	deleteVariables("tmpBotGroup*");
	deleteVariables("aidirectiveTable*");
	deleteVariables("aiNumTable*");
	deleteVariables("tmpbotn*");
	deleteVariables("funk*");
	deleteVariables("Skill*");
	deleteVariables("world*");
	deleteVariables("Quest*");
	deleteVariables("loot*");
	deleteVariables("BotInfo*");
	deleteVariables("Merchant*");
	deleteVariables("NameForRace*");
	deleteVariables("BlockData*");
	deleteVariables("EventCommand*");
	deleteVariables("LoadOut*");
	$PetList = "";
	$DISlist = "";
	$SpawnPackList = "";
	$LoadOutList = "";
	$isRaining = "";

	$loadingMission = false;
	$ME::Loaded = false;
	exec(wendrpg_server_config);	if(%mission == "")		%mission = $pref::lastMission;	if(%mission == "")	{		%printlevel = $console::printlevel;		$console::printlevel = 1;		echo("Error: no mission provided.");		$console::printlevel = %printlevel;		return "False";	}

	%ms = String::GetSubStr(timestamp(), 20, 03);
	%ms = %ms * 4;	for(%i=0; %i <= %ms; %i++)	{		getrandom();	}
	if(!$SinglePlayer)		$pref::lastMission = %mission;

	$MODInfo = "www.tribesrpg.org/worldender\n";
	if(!$dedicated){
		//display the "loading" screen
		cursorOn(MainWindow);
		GuiLoadContentCtrl(MainWindow, "gui\\Loading.gui");
		renderCanvas(MainWindow);
	}

	if(!%dedicated)
	{
		deleteServer();
		purgeResources();
		newServer();
		focusServer();
	}
	if($SinglePlayer)
		newObject(serverDelegate, FearCSDelegate, true, "LOOPBACK", $Server::Port);
	else
		newObject(serverDelegate, FearCSDelegate, true, "IP", $Server::Port, "IPX", 	$Server::Port, "LOOPBACK", $Server::Port);

	rp_include(strings);	
	exec(array);						// DescX Note: Index-based arrays. Needs extension, but works well enough for now.
	
	exec(wendrpg_rpgfunk);
	exec(wendrpg_worldender);
	
	rpg::DisableAllWeatherDevices();	// Start disabled. LoadWorld and SaveWorld take care of things from here.
	
	exec(wendrpg_charfunk);
	exec(wendrpg_connectivity);

	exec(wendrpg_version);
	setWindowTitle( "0/" @ $server::maxplayers @ " TribesRPG [" @ $rpgver @ "]" );

	exec(wendrpg_house);
	exec(wendrpg_skills);
	
	exec(wendrpg_rpgarena);
	exec(wendrpg_sleep);
	exec(wendrpg_game);
	exec(wendrpg_admin);
	exec(wendrpg_Marker);
	exec(wendrpg_Trigger);
	exec(wendrpg_zone);
	exec(wendrpg_zone_objmgr);
	
	exec(wendrpg_NSound);
	
	exec(wendrpg_spells);
	exec(wendrpg_spells_combatarts);
	exec(wendrpg_spells_elemental);
	exec(wendrpg_spells_distortion);
	exec(wendrpg_spells_restoration);
	exec(wendrpg_spells_illusion);
	
	exec(wendrpg_classes);
	exec(wendrpg_party);	
	
	exec(wendrpg_ArmorData);	
	exec(wendrpg_HumanArmors);
	exec(wendrpg_HumanArmorsSpeedSkill);
	exec(wendrpg_HumanArmorsSpeedSkill2);
	exec(wendrpg_HumanArmorsSpeedSkill3);
	exec(wendrpg_EnemyArmors);
	exec(wendrpg_SpecialArmors);
	
	exec(wendrpg_Item);
	exec(wendrpg_rpgStaticShape);
	exec(wendrpg_Accessory);
	exec(wendrpg_smithing);
	
	exec(wendrpg_weapons);
	exec(wendrpg_wparchery);
	exec(wendrpg_wpclubbing);
	exec(wendrpg_wpslashing);
	exec(wendrpg_wppierce);
	exec(wendrpg_wpwand);
	exec(wendrpg_wpgeoastrics);
	
	exec(wendrpg_armors);
	exec(wendrpg_Spawn);
	exec(wendrpg_gameevents);
	exec(wendrpg_shopping);
	exec(wendrpg_weight);
	exec(wendrpg_mana);
	exec(wendrpg_hp);
	exec(wendrpg_rpgstats);
	exec(wendrpg_playerdamage);
	exec(wendrpg_playerkill);
	exec(wendrpg_playerspawn);
	exec(wendrpg_itemevents);
	exec(wendrpg_economy);
	exec(wendrpg_remote);
	exec(wendrpg_weaponHandling);
	exec(wendrpg_BonusState);
	exec(wendrpg_Player);
	exec(wendrpg_AI);
	
	exec(wendrpg_InteriorLight);
	
	exec(wendrpg_comchat);
	exec(wendrpg_comchat_all);
	exec(wendrpg_comchat_survival);
	exec(wendrpg_comchat_thievery);
	exec(wendrpg_comchat_wordsmith);
	exec(wendrpg_comchat_admin);

	exec(wendrpg_bottalk);
	exec(wendrpg_bottalk_strings);
	exec(wendrpg_belt);
	
	Belt::Initialize();						// Only call Belt::Initialize() once so that objects can be exec'd into existence after launch
	
	exec(wendrpg_belt_items);
	
	rpg::GenerateSmithingBookHelpText();
	rpg::GenerateGlassIdiomsFromSpells();
		
	$Server::Info = $Server::Info @ $extrainfo;
	$server::modinfo = $Server::Info;

	Server::storeData();

	// NOTE!! You must have declared all data blocks BEFORE you call
	// preloadServerDataBlocks.

	preloadServerDataBlocks();

	Server::loadMission( ($missionName = %mission), true );

	//**RPG	
	LoadWorld();
	
	
	rpg::UpdateServerName();
	
	InitStaticShapes();
	InitZones();
	InitFerry();
	InitTownBots();
	if(!$NoSpawn) {
		InitSpawnPoints();
		InitArena();
	}
	
	InitObjectives();
	
	if(!%dedicated) {
		focusClient();
		if($IRC::DisconnectInSim == "")
			$IRC::DisconnectInSim = true;
		if($IRC::DisconnectInSim == true) {
			ircDisconnect();
			$IRCConnected = FALSE;
			$IRCJoinedRoom = FALSE;
		}
		// join up to the server
		$Server::Address = "LOOPBACK:" @ $Server::Port;
		$Server::JoinPassword = $Server::Password;
		connect($Server::Address);
	}

	$Pref::OverrideSetSkin = true;
	return "True";
}

function rpg::UpdateServerName() {
	$Server::BaseHostName = "World Ender RPG";
	
	if($EndOfTheWorld >= 7) 		%stateName = "ENDING";
	else if($EndOfTheWorld >= 6) 	%stateName = "Clear Skies";
	else if($EndOfTheWorld >= 3) 	%stateName = "Parted Skies";
	else if($EndOfTheWorld >= 1) 	%stateName = "Condensing";
	else							%stateName = "Darkness";
	$Server::HostName = $Server::BaseHostName @ "| v0.4 [fixed] |Cycle #" @ ($CyclesCompleted + 1) @ ": " @ %stateName;
}

function Server::nextMission(%replay)
{
	dbecho($dbechoMode, "Server::nextMission(" @ %replay @ ")");
}

function remoteCycleMission(%clientId)
{
	dbecho($dbechoMode, "remoteCycleMission(" @ %clientId @ ")");

   if(%clientId.adminLevel >= 4)
   {
      messageAll(0, Client::getName(%playerId) @ " cycled the mission.");
      Server::nextMission();
   }
}

function remoteDataFinished(%clientId)
{
	dbecho($dbechoMode, "remoteDataFinished(" @ %clientId @ ")");

   if(%clientId.dataFinished)
      return;
   %clientId.dataFinished = true;
   Client::setDataFinished(%clientId);
   %clientId.svNoGhost = ""; // clear the data flag
   if($ghosting)
   {
      %clientId.ghostDoneFlag = true; // allow a CGA done from this dude
      startGhosting(%clientId);  // let the ghosting begin!
   }
}

function remoteCGADone(%playerId)
{
	dbecho($dbechoMode, "remoteCGADone(" @ %playerId @ ")");

   if(!%playerId.ghostDoneFlag || !$ghosting)
      return;
   %playerId.ghostDoneFlag = "";

   Game::initialMissionDrop(%playerid);

	if ($cdTrack != "")
		remoteEval (%playerId, setMusic, $cdTrack, $cdPlayMode);
   remoteEval(%playerId, MInfo, $missionName);
}

function Server::loadMission(%missionName, %immed)
{
	dbecho($dbechoMode, "Server::loadMission(" @ %missionName @ ", " @ %immed @ ")");

   if($loadingMission)
      return;

   %missionFile = "missions\\" $+ %missionName $+ ".mis";
   if(File::FindFirst(%missionFile) == "")
   {
      %missionName = $firstMission;
      %missionFile = "missions\\" $+ %missionName $+ ".mis";
      if(File::FindFirst(%missionFile) == "")
      {
         echo("invalid nextMission and firstMission...");
         echo("aborting mission load.");
         return;
      }
   }
   //echo("Notfifying players of mission change: ", getNumClients(), " in game");
   for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
   {
      Client::setGuiMode(%cl, $GuiModeVictory);
      %cl.guiLock = true;
      %cl.nospawn = true;
      remoteEval(%cl, missionChangeNotify, %missionName);
   }

   $loadingMission = true;
   $missionName = %missionName;
   $missionFile = %missionFile;
   $prevNumTeams = getNumTeams();

	if(isObject("MissionGroup"))
		deleteObject("MissionGroup");
	if(isObject("MissionCleanup"))
		deleteObject("MissionCleanup");
	if(isObject("ConsoleScheduler"))
		deleteObject("ConsoleScheduler");
   resetPlayerManager();
   resetGhostManagers();
   $matchStarted = false;
   $countdownStarted = false;
   $ghosting = false;

   resetSimTime(); // deal with time imprecision

   newObject(ConsoleScheduler, SimConsoleScheduler);
   if(!%immed)
      schedule("Server::finishMissionLoad();", 18);
   else
      Server::finishMissionLoad();      
}

function Server::finishMissionLoad()
{
	dbecho($dbechoMode, "Server::finishMissionLoad()");

	$loadingMission = false;
	$TestMissionType = "";
	// instant off of the manager
	setInstantGroup(0);
	newObject(MissionCleanup, SimGroup);

	exec($missionFile);


	$END_OF_MAP = newObject("EndOfMap", SimGroup);
	//Don't modify $END_OF_MAP in your scripts.
	//it is used by saveworld and should never change after this point.


	if($END_OF_MAP > 1)
		Mission::init();
   if($prevNumTeams != getNumTeams())
   {
      // loop thru clients and setTeam to -1;
      messageAll(0, "New teamcount - resetting teams.");
      for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
         GameBase::setTeam(%cl, -1);
   }

   $ghosting = true;
   for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
   {
      if(!%cl.svNoGhost)
      {
         %cl.ghostDoneFlag = true;
         startGhosting(%cl);
      }
   }
   if($SinglePlayer)
      Game::startMatch();
   //else if($Server::warmupTime && !$Server::TourneyMode)
   //   Server::Countdown($Server::warmupTime);
   //else if(!$Server::TourneyMode)
      Game::startMatch();

   $teamplay = (getNumTeams() != 1);
   purgeResources(true);

   // make sure the match happens within 5-10 hours.
   //schedule("Server::CheckMatchStarted();", 3600);
   //schedule("Server::nextMission();", 18000);
   
   return "True";
}

function Server::CheckMatchStarted()
{
	dbecho($dbechoMode, "Server::CheckMatchStarted()");

   // if the match hasn't started yet, just reset the map
   // timing issue.
   if(!$matchStarted)
      Server::nextMission(true);
}

function Server::Countdown(%time)
{
	dbecho($dbechoMode, "Server::Countdown(" @ %time @ ")");

   $countdownStarted = true;
   schedule("Game::startMatch();", %time);
   Game::notifyMatchStart(%time);
   if(%time > 30)
      schedule("Game::notifyMatchStart(30);", %time - 30);
   if(%time > 15)
      schedule("Game::notifyMatchStart(15);", %time - 15);
   if(%time > 10)
      schedule("Game::notifyMatchStart(10);", %time - 10);
   if(%time > 5)
      schedule("Game::notifyMatchStart(5);", %time - 5);
}

function Client::setInventoryText(%clientId, %txt)
{
	dbecho($dbechoMode, "Client::setInventoryText(" @ %clientId @ ", " @ %txt @ ")");

	remoteEval(%clientId, "ITXT", %txt);
}

function centerprint(%clientId, %msg, %timeout)
{
	dbecho($dbechoMode, "centerprint(" @ %clientId @ ", " @ %msg @ ", " @ %timeout @ ")");

   if(%timeout == "")
      %timeout = 5;
   if(%timeout == -1)
        %timeout = "";
   remoteEval(%clientId, "CP", %msg, %timeout);
}

function bottomprint(%clientId, %msg, %timeout)
{
	dbecho($dbechoMode, "bottomprint(" @ %clientId @ ", " @ %msg @ ", " @ %timeout @ ")");

   if(%timeout == "")
      %timeout = 5;
   if(%timeout == -1)
        %timeout = "";
   remoteEval(%clientId, "BP", %msg, %timeout);
}

function topprint(%clientId, %msg, %timeout)
{
	dbecho($dbechoMode, "topprint(" @ %clientId @ ", " @ %msg @ ", " @ %timeout @ ")");

   if(%timeout == "")
      %timeout = 5;
   if(%timeout == -1)
        %timeout = "";
   remoteEval(%clientId, "TP", %msg, %timeout);
}

function centerprintall(%msg, %timeout)
{
	dbecho($dbechoMode, "centerprintall(" @ %msg @ ", " @ %timeout @ ")");

   if(%timeout == "")
      %timeout = 5;
   if(%timeout == -1)
        %timeout = "";
   for(%clientId = Client::getFirst(); %clientId != -1; %clientId = Client::getNext(%clientId))
      remoteEval(%clientId, "CP", %msg, %timeout);
}

function bottomprintall(%msg, %timeout)
{
	dbecho($dbechoMode, "bottomprintall(" @ %msg @ ", " @ %timeout @ ")");

   if(%timeout == "")
      %timeout = 5;
   if(%timeout == -1)
        %timeout = "";
   for(%clientId = Client::getFirst(); %clientId != -1; %clientId = Client::getNext(%clientId))
      remoteEval(%clientId, "BP", %msg, %timeout);
}

function topprintall(%msg, %timeout)
{
	dbecho($dbechoMode, "topprintall(" @ %msg @ ", " @ %timeout @ ")");

   if(%timeout == "")
      %timeout = 5;
   if(%timeout == -1)
        %timeout = "";
   for(%clientId = Client::getFirst(); %clientId != -1; %clientId = Client::getNext(%clientId))
      remoteEval(%clientId, "TP", %msg, %timeout);
}


function Schedule::Add( %eval, %time, %tag ) {	if ( %tag == "" )		%tag = %eval;	if(String::findSubStr(%tag, "\"") != -1 || String::findSubStr(%tag, "\\") != -1){		pecho("%tag malformed: "@%tag);		return;	}	$Schedule::id[%tag]++;	$Schedule::eval[%tag] = %eval;		schedule( "Schedule::Exec(\""@%tag@"\", "@$Schedule::ID[%tag]@");", %time );}function Schedule::Exec( %tag, %id ) {	if ( $Schedule::ID[%tag] != %id )		return;	%eval = $Schedule::eval[%tag];	Schedule::Cancel(%tag);	eval(%eval);}function Schedule::Cancel( %tag ) {	if($Schedule::ID[%tag] > 900000)		$Schedule::ID[%tag] = 0;	else		$Schedule::ID[%tag]++;	$Schedule::eval[%tag] = "";}function Schedule::Check( %tag ) {	if( $Schedule::eval[%tag] != "" )		return true;	else		return false;}

$round2plugin = False;
if(round2(2) != False) $round2plugin = True;

// DescX Note:	The original FixDecimals function tries to fix floating point error... and also incorrectly converted negative values to fractions of -1.
// This replacement function is still wrong. But at least it's named closer to what it does!! :o
function RoundToFirstDecimal(%c)
{
	dbecho($dbechoMode, "RoundToFirstDecimal(" @ %c @ ")");
	if($round2plugin){
		%neg = False;
		if(%c < 0) {%neg = True; %c *= -1;}
		%val = round2(%c*10);
		%val = String::getSubStr(%val, 0, String::len(%val)-1) @ "." @ %val % 10;
		if(%val < 1 && %val > 0)
			%val = "0" @ %val;
		if(%neg) %val = "-" @ %val;
		return %val;
	}

	return FixDecimals(%c);
}

function safeDelete(%id, %caller, %instant){	if(%id < 700){		pecho("Failure 1 to safeDelete object "@%id@", called by "@%caller);		return false;	}	if(!isObject(%id)){		pecho("Failure 2 to safeDelete object "@%id@", called by "@%caller);		return false;	}	if(%instant)		deleteObject(%id);	else		schedule("finalDelete("@%id@",\"escapeString(%caller)\");",0.01,%id);	return true;}
function finalDelete(%id, %caller){	if(!isObject(%id)){		pecho("Failure 2 to finalDelete object "@%id@", called by "@%caller);		return false;	}	deleteObject(%id);	return true;}
// for player rotations only (around z axis) -plasmatic function rotateVector(%vec,%rot){	%pi = 3.1416;	%rot3= getWord(%rot,2);	for(%i = 0; %rot3 >= %pi*2; %i++) %rot3 = %rot3 - %pi*2;	if (%rot3 > %pi) %rot3 = %rot3 - %pi*2;	%vec1= getWord(%vec,0);	%vec2= getWord(%vec,1);	%vc = %vec2;	%vec3= getWord(%vec,2); 	%ray = %vec1;		%vec1 = %ray*cos(%rot3);	%vec2 = %ray*sin(%rot3);	%vec = %vec1 @" "@ %vec2 @" "@ %vec3;	%vec = Vector::add(%vec,Vector::getFromRot(%rot,%vc,0));	return %vec;}