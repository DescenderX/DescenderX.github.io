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



//rpg admin

$curVoteTopic = "";
$curVoteAction = "";
$curVoteOption = "";
$curVoteCount = 0;

function Admin::changeMissionMenu(%clientId)
{
}

function processMenuCMType(%clientId, %options)
{
}

function processMenuCMission(%clientId, %option)
{
}

function remoteAdminPassword(%clientId, %password)
{
	if($AdminPassword != "" && %password == $AdminPassword[4])
	{
		%clientId.adminLevel = 4;
	}
}


function remoteSetPassword(%clientId, %password)
{
	if(%clientId.adminLevel >= 5)
		$Server::Password = %password;
}

function remoteSetTimeLimit(%clientId, %time)
{
}

function remoteSetTeamInfo(%clientId, %team, %teamName, %skinBase)
{
}

function remoteVoteYes(%clientId)
{
   %clientId.vote = "yes";
   centerprint(%clientId, "", 0);
}

function remoteVoteNo(%clientId)
{
   %clientId.vote = "no";
   centerprint(%clientId, "", 0);
}

function Admin::startMatch(%admin)
{
}

function Admin::setTeamDamageEnable(%admin, %enabled)
{
}

function Admin::DirectKick(%admin, %clientId, %msg)
{
	%ip = Client::getTransportAddress(%clientId);
	BanList::add(%ip, 30);
	%aname = rpg::getName(%admin);
	%name = rpg::getName(%clientId);
	//MessageAll(0, %name @ " was kicked by " @ %aname @ ".");
	newKick(%clientId, %msg, True);
}

function Admin::kick(%admin, %clientId, %ban)
{
   if(%admin == -1 || %admin.adminLevel >= 4)
   {
      if(%ban && %admin.adminLevel < 5)
         return;
         
      if(%ban)
      {
         %word = "banned";
         %cmd = "BAN: ";
      }
      else
      {
         %word = "kicked";
         %cmd = "KICK: ";
      }
      if(%clientId.adminLevel >= 5)
      {
         if(%admin == -1)
            messageAll(0, "A super admin cannot be " @ %word @ ".");
         else
            Client::sendMessage(%admin, 0, "A super admin cannot be " @ %word @ ".");
         return;
      }
      %ip = Client::getTransportAddress(%clientId);

      echo(%cmd @ %admin @ " " @ %clientId @ " " @ %ip);

      if(%ip == "")
         return;
      if(%ban)
         BanList::add(%ip, 1800);
      else
         BanList::add(%ip, 180);

      %name = Client::getName(%clientId);

		if(%admin == -1)
		{
			MessageAll(0, %name @ " was " @ %word @ " from vote.");
			newKick(%clientId, "You were " @ %word @ " by  consensus.");
		}
		else
		{
			MessageAll(0, %name @ " was " @ %word @ " by " @ Client::getName(%admin) @ ".");
			newKick(%clientId, "You were " @ %word @ " by " @ Client::getName(%admin));
		}
	}
}


function Admin::setModeFFA(%clientId)
{
}

function Admin::setModeTourney(%clientId)
{
}

function Admin::voteFailed()
{
   $curVoteInitiator.numVotesFailed++;

   if($curVoteAction == "kick" || $curVoteAction == "admin")
      $curVoteOption.voteTarget = "";
}

function Admin::voteSucceded()
{
   $curVoteInitiator.numVotesFailed = "";
   if($curVoteAction == "kick")
   {
//      if($curVoteOption.voteTarget)
//         Admin::kick(-1, $curVoteOption);
   }
   else if($curVoteAction == "admin")
   {
      if($curVoteOption.voteTarget)
      {
//         $curVoteOption.adminLevel = 4;
         messageAll(0, Client::getName($curVoteOption) @ " has become an administrator.");
         if($curVoteOption.menuMode == "options")
            Game::menuRequest($curVoteOption);
      }
      $curVoteOption.voteTarget = false;
   }
}

function Admin::countVotes(%curVote)
{
   // if %end is true, cancel the vote either way
   if(%curVote != $curVoteCount)
      return;

   %votesFor = 0;
   %votesAgainst = 0;
   %votesAbstain = 0;
   %totalClients = 0;
   %totalVotes = 0;
   for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
   {
      %totalClients++;
      if(%cl.vote == "yes")
      {
         %votesFor++;
         %totalVotes++;
      }
      else if(%cl.vote == "no")
      {
         %votesAgainst++;
         %totalVotes++;
      }
      else
         %votesAbstain++;
   }
   %minVotes = floor($Server::MinVotesPct * %totalClients);
   if(%minVotes < $Server::MinVotes)
      %minVotes = $Server::MinVotes;

   if(%totalVotes < %minVotes)
   {
      %votesAgainst += %minVotes - %totalVotes;
      %totalVotes = %minVotes;
   }
   %margin = $Server::VoteWinMargin;
   if($curVoteAction == "admin")
   {
      %margin = $Server::VoteAdminWinMargin;
      %totalVotes = %votesFor + %votesAgainst + %votesAbstain;
      if(%totalVotes < %minVotes)
         %totalVotes = %minVotes;
   }
   if(%votesFor / %totalVotes >= %margin)
   {
      messageAll(0, "Vote to " @ $curVoteTopic @ " passed: " @ %votesFor @ " to " @ %votesAgainst @ " with " @ %totalClients - (%votesFor + %votesAgainst) @ " abstentions.");
      Admin::voteSucceded();
   }
   else  // special team kick option:
   {
      if($curVoteAction == "kick") // check if the team did a majority number on him:
      {
         %votesFor = 0;
         %totalVotes = 0;
         for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
         {
            if(GameBase::getTeam(%cl) == $curVoteOption.kickTeam)
            {
               %totalVotes++;
               if(%cl.vote == "yes")
                  %votesFor++;
            }
         }
         if(%totalVotes >= $Server::MinVotes && %votesFor / %totalVotes >= $Server::VoteWinMargin)
         {
            messageAll(0, "Vote to " @ $curVoteTopic @ " passed: " @ %votesFor @ " to " @ %totalVotes - %votesFor @ ".");
            Admin::voteSucceded();
            $curVoteTopic = "";
            return;
         }
      }
      messageAll(0, "Vote to " @ $curVoteTopic @ " did not pass: " @ %votesFor @ " to " @ %votesAgainst @ " with " @ %totalClients - (%votesFor + %votesAgainst) @ " abstentions.");
      Admin::voteFailed();
   }
   $curVoteTopic = "";
}

function Admin::startVote(%clientId, %topic, %action, %option)
{
   if(%clientId.lastVoteTime == "")
      %clientId.lastVoteTime = -$Server::MinVoteTime;

   // we want an absolute time here.
   %time = getIntegerTime(true) >> 5;
   %diff = %clientId.lastVoteTime + $Server::MinVoteTime - %time;

   if(%diff > 0)
   {
      Client::sendMessage(%clientId, 0, "You can't start another vote for " @ floor(%diff) @ " seconds.");
      return;
   }
   if($curVoteTopic == "")
   {
      if(%clientId.numFailedVotes)
         %time += %clientId.numFailedVotes * $Server::VoteFailTime;

      %clientId.lastVoteTime = %time;
      $curVoteInitiator = %clientId;
      $curVoteTopic = %topic;
      $curVoteAction = %action;
      $curVoteOption = %option;
      if(%action == "kick")
         $curVoteOption.kickTeam = GameBase::getTeam($curVoteOption);
      $curVoteCount++;
      bottomprintall("<jc><f1>" @ Client::getName(%clientId) @ " <f0>initiated a vote to <f1>" @ $curVoteTopic, 10);
      for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
         %cl.vote = "";
      %clientId.vote = "no"; // yes
      for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
         if(%cl.menuMode == "options")
            Game::menuRequest(%clientId);
      schedule("Admin::countVotes(" @ $curVoteCount @ ", true);", $Server::VotingTime, 35);
   }
   else
   {
      Client::sendMessage(%clientId, 0, "Voting already in progress.");
   }
}
$menuChars = "1234567890-=abcghijklmnoqrtuvwxy .";
function Game::menuRequest(%clientId)
{
	if(%clientId.IsInvalid)
		return;

	if(%clientId.choosingGroup)
	{
		MenuGroup(%clientId);
		return;
	}
	else if(%clientId.choosingClass)
	{
		MenuClass(%clientId);
		return;
	}
	else if(%clientId.currentShop != "")
	{
		%clientId.beltShop = %clientId.currentShop;
		MenuBuyBeltItem(%clientid, 1);
		return;
	}
	else if(%clientId.currentBank != "")
	{
		//Store belt items
		MenuWithdrawBelt(%clientId);
		return;
	}

	%curItem = 0;
	Client::buildMenu(%clientId, "Options", "options", true);
	if($curVoteTopic != "" && %clientId.vote == "")
	{
		Client::addMenuItem(%clientId, %curItem++ @ "Vote YES to " @ $curVoteTopic, "voteYes " @ $curVoteCount);
		Client::addMenuItem(%clientId, %curItem++ @ "Vote NO to " @ $curVoteTopic, "voteNo " @ $curVoteCount);
	}
	else
	{
		if(%clientId.selClient)
		{
			%sel = %clientId.selClient;
			%selname = Client::getName(%sel);
	
			if(%clientId != %sel && fetchData(%sel, "HasLoadedAndSpawned"))
			{
                        	if(IsInCommaList(fetchData(%clientId, "grouplist"), %selname))
						Client::addMenuItem(%clientId, %curItem++ @ "Remove from group-list", "remgroup " @ %sel);
					else
						Client::addMenuItem(%clientId, %curItem++ @ "Add to group-list", "addgroup " @ %sel);

                        	if(IsInCommaList(fetchData(%clientId, "targetlist"), %selname))
						Client::addMenuItem(%clientId, %curItem++ @ "Remove from target-list", "remtarget " @ %sel);
					else
						Client::addMenuItem(%clientId, %curItem++ @ "Add to target-list", "addtarget " @ %sel);

				if(fetchData(%clientId, "partyOwned"))
				{
					if(IsInCommaList(fetchData(%clientId, "partylist"), %selname))
						Client::addMenuItem(%clientId, %curItem++ @ "Remove from your party", "remparty " @ %sel);
					else
					{
						if(CountObjInCommaList(fetchData(%clientId, "partylist")) < $maxpartymembers)
						{
							%p = IsInWhichParty(Client::getName(%sel));
							if(%p == -1)
								Client::addMenuItem(%clientId, %curItem++ @ "Invite to your party", "addparty " @ %sel);
							else if(GetWord(%p, 1) == "i")
								Client::addMenuItem(%clientId, %curItem++ @ "Cancel invitation", "cancelinv " @ %sel);
							else
								Client::addMenuItem(%clientId, %curItem++ @ "(Can't invite, already in a party)", "");
						}
						else
							Client::addMenuItem(%clientId, %curItem++ @ "(Can't invite, too many members)", "");
					}
				}
				if(%clientId.muted[%sel])
					Client::addMenuItem(%clientId, %curItem++ @ "Unmute", "unmute " @ %sel);
				else
					Client::addMenuItem(%clientId, %curItem++ @ "Mute", "mute " @ %sel);
			}
			Client::addMenuItem(%clientId, %curItem++ @ "Info", "getinfo " @ %sel);
			if(%clientid.adminlevel >= 1)
				Client::addMenuItem(%clientId, %curItem++ @ "Admin controls", "admincontrol " @ %sel);
			
				
		}
		else
		{
			%curItem = -1;			
			if(!IsDead(%clientId)){
				Client::addMenuItem(%clientId, "------------------------------" , "clearmessage");				
				Client::addMenuItem(%clientId, "vView Stats & Tasks" , "viewstats");				
				Client::addMenuItem(%clientId, "kSkill Points" , "sp");
				%fvr = fetchData(%clientId, "FAVOR");
				if(%fvr == 0)
					Client::addMenuItem(%clientId, "f" @ "No FAVOR!" , "");
				else if(fetchData(%clientId, "FAVORmode") == "miss")
					Client::addMenuItem(%clientId, "f" @ %fvr @ " FAVOR [Life Save]" , "FAVORdeath");
				else if(fetchData(%clientId, "FAVORmode") == "death")
					Client::addMenuItem(%clientId, "f" @ %fvr @ " FAVOR [Soul Save]" , "FAVORmiss");
				Client::addMenuItem(%clientId, "oOptions" , "settings");

				Client::addMenuItem(%clientId, ":-----------------------------" , "clearmessage");
				Client::addMenuItem(%clientId, "bBelt Items",	"viewbelt");
				Client::addMenuItem(%clientId, "cCast Spells", 	"spellcast");
				Client::addMenuItem(%clientId, "xUse Skills", 	"skilluse");
				if(GetAccessoryList(%clientId, 9, -1) != "")
					Client::addMenuItem(%clientId, "pProjectiles" , "rweapons");
				Client::addMenuItem(%clientId, ";-----------------------------" , "clearmessage");		
				if(%clientId.sleepMode != "")
					Client::addMenuItem(%clientId, string::getsubstr($menuChars,%curItem++,1) @ "#wake","wake");
				else Client::addMenuItem(%clientId, string::getsubstr($menuChars,%curItem++,1) @ "#meditate","meditate");				
				Client::addMenuItem(%clientId, string::getsubstr($menuChars,%curItem++,1) @ "#recall" , "recall");
				Client::addMenuItem(%clientId, string::getsubstr($menuChars,%curItem++,1) @ "#savecharacter" , "savecharacter");
			}			
			Client::addMenuItem(%clientId, "=----------------------------" , "clearmessage");
			Client::addMenuItem(%clientId, "hHELP" , "help");
				if(%clientId.adminLevel > 0)
					Client::addMenuItem(%clientId, "nAdmin controls", "admincontrol");
			
			
			
		}
	}
}

function processMenuOptions(%clientId, %option)
{
	dbecho($dbechoMode, "processMenuOptions(" @ %clientId @ ", " @ %option @ ")");

	%opt = getWord(%option, 0);
	%cl = floor(getWord(%option, 1));
	


	//**RPG
	if(%opt == "settings")
	{
		Client::buildMenu(%clientId, "Options", "settings", true);

		Client::addMenuItem(%clientId, %curItem++ @ "Party options..." , "partyoptions");
		Client::addMenuItem(%clientId, %curItem++ @ "Set default talk..." , "defaulttalk");

		if(fetchData(%clientId, "ignoreGlobal"))
			Client::addMenuItem(%clientId, %curItem++ @ "Turn ignore global OFF" , "gignoreoff");
		else
			Client::addMenuItem(%clientId, %curItem++ @ "Turn ignore global ON" , "gignoreon");

		Client::addMenuItem(%clientId, %curItem++ @ "Battle message config..." , "battmsg");
		Client::addMenuItem(%clientId, "bBack" , "done");

	}	if(%opt == "help")
	{
		Client::buildMenu(%clientId, "Help", "help", true);
		%curItem = rpg::CreateHelpMenuItems(%clientId,%curItem);
	}
	else if(%opt == "viewstats")
	{
		%a[%tmp++] = "<f1>" @ Client::getName(%clientId);
		
		%a[%tmp++] = "\nLevel " @ fetchData(%clientId, "LVL") @ " | " @ getFinalCLASS(%clientId);
 
		if(fetchData(%clientId, "MyHouse") != "") {
			%a[%tmp++] = " | Rank " @  floor(rpg::GetHouseLevel(%clientId));
		}		
		%a[%tmp++] = "\n<f1>Next <f0>LEVEL: <f2>" @ GetExp(GetLevel(fetchData(%clientId, "EXP"), %clientId)+1, %clientId) - fetchData(%clientId, "EXP") @ "";
		if(fetchData(%clientId, "MyHouse") != "") {
			%a[%tmp++] = "    <f1>Next <f0>RANK: <f2>" @ floor(1000 - fetchData(%clientId,"RankPoints") % 1000) @ "\n";
		}
		%a[%tmp] = %a[%tmp] @ "<f0>\n";

		%a[%tmp++] = "<f1>  Attack - - - - <f0>" @ fetchData(%clientId, "ATK") @ "\n";
		%a[%tmp++] = "<f1>  Armor - - - - <f0>" @ fetchData(%clientId, "DEF") @ "\n";
		%a[%tmp++] = "<f1>  Resistance - - <f0>" @ fetchData(%clientId, "MDEF") @ "\n\n";

		%a[%tmp++] = "<f1>  Health - - - -  <f0>" @ fetchData(%clientId, "HP") @ " / " @ fetchData(%clientId, "MaxHP") @ "\n";
		%a[%tmp++] = "<f1>  Mana - - - - -  <f0>" @ fetchData(%clientId, "MANA") @ " / " @ fetchData(%clientId, "MaxMANA") @ "\n";		
		%a[%tmp++] = "<f1>  Thirst - - - - - <f0>" @ floor(100 * ((Cap(fetchData(%clientId, "Thirst"), -600, 600) - 600) / -1200)) @ "%\n\n";

		%a[%tmp++] = "<f1>  Weight - - - - <f0>" @ floor(fetchData(%clientId, "Weight")) @ " / " @ floor(fetchData(%clientId, "MaxWeight")) @ "\n";
		%a[%tmp++] = "<f1>  Coins - - - - - <f0>" @ fetchData(%clientId, "COINS") @ " (" @ fetchData(%clientId, "BANK") @ " hidden)\n";		
		%a[%tmp++] = "<f1>  FAVOR - - -  <f0>" @ fetchData(%clientId, "FAVOR") @ "\n\n";		
		
		%stl = Cap(fetchData(%clientId, "STOLEN"),0,"inf");
		%bty = fetchData(%clientId, "bounty");
	//	if(%stl > 0)
		%a[%tmp++] = "<f1>  Heat - - - - - <f0>" @ %stl @ "\n";
	//	if(%bty > 0)
		%a[%tmp++] = "<f1>  Bounty - - -  <f0>" @ %bty @ "\n";
	//	if(%stl>0 || %bty>0)
			%a[%tmp++] = "\n";

		%h = $HouseIndex[fetchData(%clientId, "MyHouse")];

		if(HasThisStuff(%clientId, "BlackMarketKey 1") && %h != $HouseIndex["Keldrin Mandate"]){
			if(($ShakedownTarget[rpg::getName(%clientId)] != "") || ($TheHunted["", 0] != "")) {
				%a[%tmp++] = "<f1>{[ Slicer's Tasks ]}\n";
				if($ShakedownTarget[rpg::getName(%clientId)] != "") {
					%target = $BotInfo[$ShakedownTarget[rpg::getName(%clientId)].name, NAME];
					%a[%tmp++] = "<f1>  Shakedown    | <f0>" @ %target @ "\n";
				}
			
				if($TheHunted["", 0] != "") {
					%a[%tmp++] = "<f0>  'Troublemakers':\n";
					for(%x=0;%x<$TheHuntedMax[""];%x++) {
						if($TheHunted["", %x] != "") {
							%a[%tmp++] = "        <f1>" @ $TheHunted["", %x] @ ", <f0>near a <f2>" @ getword($TheHuntedSpawn["",%x],5) @ "\n";
						}
					}		
				}
			}
		}
		
		if(fetchData(%clientId, "MyHouse") != "") {
			%a[%tmp] = %a[%tmp] @ "<f1>{[ " @ fetchData(%clientId, "MyHouse") @ " Tasks ]}\n";
		}
		
		if(%h == $HouseIndex["Keldrin Mandate"]){
			if($TheMandateDeliveryTarget[rpg::getName(%clientId)] != "") {
				%target = $BotInfo[$TheMandateDeliveryTarget[rpg::getName(%clientId)].name, NAME];
				%a[%tmp++] = "\n<f1>  Delivery    | <f0>" @ %target @ "\n";
			}
			%a[%tmp++] = "<f0>  Execute writs:\n";
			for(%x=0;%x<$TheHuntedMax[%h];%x++) {
				if($TheHunted[%h, %x] != "") {
					%a[%tmp++] = "        <f1>" @ $TheHunted[%h, %x] @ ", <f0>near a <f2>" @ getword($TheHuntedSpawn[%h,%x],5) @ "\n";
				}
			}
		}
		else if(%h == $HouseIndex["Wildenslayers"]){
			%a[%tmp++] = "<f0>  GET ORC AXES:\n";
			for(%x=0;%x<$TheHuntedMax[%h];%x++) {
				if($TheHunted[%h, %x] != "") {
					%a[%tmp++] = "        <f1>" @ $TheHunted[%h, %x] @ ", <f0>near a <f2>" @ getword($TheHuntedSpawn[%h,%x],5) @ "\n";
				}
			}
		}
		else if(%h == $HouseIndex["Order Of Qod"]){
			%a[%tmp++] = "<f0>  Souls to purge:\n";
			for(%x=0;%x<$TheHuntedMax[%h];%x++) {
				if($TheHunted[%h, %x] != "") {
					%a[%tmp++] = "        <f1>" @ $TheHunted[%h, %x] @ ", <f0>near a <f2>" @ getword($TheHuntedSpawn[%h,%x],5) @ "\n";
				}
				if(%x>=10)break;
			}
		}		
		

		for(%i = 1; %a[%i] != ""; %i++)
			%f = %f @ %a[%i];

		rpg::longprint(%clientId, %f, 1, floor(String::len(%f) / 18));

		return;
	}
	else if(%opt == "addgroup")
	{
		if(countObjInCommaList(fetchData(%clientId, "grouplist")) <= 30)
		{
			%name = Client::getName(%cl);
			storeData(%clientId, "grouplist", AddToCommaList(fetchData(%clientId, "grouplist"), %name));

			Client::sendMessage(%cl, $MsgBeige, Client::getName(%clientId) @ " has added you to his/her group-list.");
			Client::sendMessage(%clientId, $MsgBeige, %name @ " is now on your group-list.");
		}
		else
			Client::sendMessage(%clientId, $MsgRed, "You have too many people on your group-list.");
	}
	else if(%opt == "remgroup")
	{
		%name = Client::getName(%cl);
		storeData(%clientId, "grouplist", RemoveFromCommaList(fetchData(%clientId, "grouplist"), %name));

		Client::sendMessage(%cl, $MsgBeige, Client::getName(%clientId) @ " has removed you from his/her group-list.");
		Client::sendMessage(%clientId, $MsgBeige, %name @ " is no longer on your group-list.");
	}
	else if(%opt == "addtarget")
	{
		if(countObjInCommaList(fetchData(%clientId, "targetlist")) <= 30)
		{
			%delay = 20;
			%name = Client::getName(%cl);
			Client::sendMessage(%clientId, $MsgRed, %name @ " will be added to your target-list in " @ %delay @ " seconds.");
			Client::sendMessage(%cl, $MsgRed, Client::getName(%clientId) @ " is thinking about killing you.");

			schedule("AddToTargetList(" @ %clientId @ ", " @ %cl @ ");", %delay, %cl);
		}
		else
			Client::sendMessage(%clientId, $MsgRed, "You have too many people on your target-list.");
	}
	else if(%opt == "remtarget")
	{
		%name = Client::getName(%cl);
		storeData(%clientId, "targetlist", RemoveFromCommaList(fetchData(%clientId, "targetlist"), %name));

		Client::sendMessage(%cl, $MsgBeige, Client::getName(%clientId) @ " has declared a truce.");
		Client::sendMessage(%clientId, $MsgBeige, %name @ " is no longer on your target-list.");
	}
	else if(%opt == "addparty")
	{
		%clientId.invitee[%cl] = True;
		Client::sendMessage(%cl, $MsgBeige, Client::getName(%clientId) @ " has invited you to join his/her party.");
		Client::sendMessage(%clientId, $MsgBeige, "You have invited " @ Client::getName(%cl) @ " to join your party.");
	}
	else if(%opt == "remparty")
	{
		%name = Client::getName(%cl);
		RemoveFromParty(%clientId, %name);
	}
	else if(%opt == "cancelinv")
	{
		%clientId.invitee[%cl] = "";
		Client::sendMessage(%cl, $MsgRed, Client::getName(%clientId) @ " has cancelled his invitation.");
		Client::sendMessage(%clientId, $MsgBeige, "You cancelled your invitation to " @ Client::getName(%cl) @ ".");
	}
	else if(%opt == "mute")
	      %clientId.muted[%cl] = True;
	else if(%opt == "unmute")
		%clientId.muted[%cl] = "";
	else if(%opt == "sp")
	{
		MenuSP(%clientId, 1);
		return;
	}
	else if(%opt == "rweapons")
	{
		%list = GetAccessoryList(%clientId, 9, -1);

		Client::buildMenu(%clientId, "Ranged weapons:", "selectrweapon", true);
		for(%i = 0; GetWord(%list, %i) != -1; %i++)
		{
			%item = GetWord(%list, %i);

			Client::addMenuItem(%clientId, %curitem++ @ %item.description, %item);
		}
		return;
	}
	else if(%opt == "viewbelt")
	{
		MenuViewBelt(%clientid, 1);
		return;
	}
	else if(%opt == "meditate")
	{
		internalSay(%clientId, 0, "#meditate");
		return;
	}
	else if(%opt == "savecharacter")
	{
		internalSay(%clientId, 0, "#savecharacter");
		return;
	}	
	else if(%opt == "recall")
	{
		rpg::RecallPlayer(%clientId);
		return;
	}
	else if(%opt == "wake")
	{
		internalSay(%clientId, 0, "#wake");
		return;
	}
	else if(%opt == "getinfo")
	{
		if(%cl != -1){
			internalSay(%clientId, 0, "#getinfo " @ rpg::getName(%cl));
		}
		return;
	}
	
	else if(%opt == "admincontrol"){
		if(%clientId.adminLevel < 1)
			return;
		%title = "Admin controls";
		if(%cl != -1){
			%title = %title @ " for " @ rpg::getname(%cl);
		}
		
		%curItem = -1;
		%losClient ="";
		%player=Client::getOwnedObject(%clientId);
		if(GameBase::getLOSinfo(%player, 6000) && getObjectType($los::object) == "Player") {
			%losClient = Player::getClient($los::object);
			%title = %title @ " for " @ rpg::getname(%losClient);
			Client::buildMenu(%clientId, %title, "admincontrol", true);
			if(%clientId.adminlevel >= 2){
				Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Teleport" , "teleport "@%losClient);
				Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Teleport to" , "teleportto "@%losClient);
				Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Kick" , "kick "@%losClient);
			}
			if(%clientId.adminlevel >= 3) {
				Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Set race" , "setrace "@%losClient);
				Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Kill" , "kill "@%losClient);
			}
			return;
		}
		else {		
			Client::buildMenu(%clientId, %title, "admincontrol", true);
			if(%cl != -1){
				Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Observe" , "eyes "@%cl);
				if(%clientId.adminlevel >= 2){
					Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Teleport" , "teleport "@%cl);
					Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Teleport to" , "teleportto "@%cl);
					Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Force recall" , "forcerecall "@%cl);
					Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Kick" , "kick "@%cl);
				}
				if(%clientId.adminlevel >= 3)
					Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Set race" , "setrace "@%cl);
				return;
			}
		}
		Client::addMenuItem(%clientId, %curItem++ @ "Admin level" , "adminlevel");
		if(%clientId.adminlevel >= 3) {
			Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Set race" , "setrace "@%clientId);
			Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Max hp, mana, thirst" , "maxcurstat "@%cl);
		}
	}
	else if(%opt == "spellcast") {
		%curItem = -1;
		Client::buildMenu(%clientId, "Cast Spells", "spellcasting", true);
		Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Combat Arts" , %clientId @ " " @ $SkillCombatArts);
		Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Elemental Magic" , %clientId @ " " @ $SkillElementalMagic);
		Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Distortion Magic" , %clientId @ " " @ $SkillDistortionMagic);
		Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Illusion Magic" , %clientId @ " " @ $SkillIllusionMagic);
		Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Restoration Magic" , %clientId @ " " @ $SkillRestorationMagic);
	}
	else if(%opt == "skilluse") {
		%curItem = -1;
		Client::buildMenu(%clientId, "Use Skills", "skillusage", true);
		Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Thievery" , %clientId @ " " @ $SkillThievery);
		Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Survival" , %clientId @ " " @ $SkillSurvival);
		Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ "Wordsmith" , %clientId @ " " @ $SkillWordsmith);
	} 	
	else if(%opt == "FAVORmiss")
	{
		storeData(%clientId, "FAVORmode", "miss");
		Game::menuRequest(%clientId);
	}
	else if(%opt == "FAVORdeath")
	{
		storeData(%clientId, "FAVORmode", "death");
		Game::menuRequest(%clientId);
	}
	else {
		rpg::longprint(%clientId, "", 1, 0.1);
		return;
	}
}


function processMenuSkillUsage(%clientId, %option) {	
	%cl 		= floor(getWord(%option, 0));
	%skillName	= getWord(%option, 1);
	%curItem	= -1;	
	Client::buildMenu(%clientId, $SkillDesc[%skillName], "useskill", true);		
	for(%i=0;(%skill=getword($SkillList[%skillName],%i)) != -1; %i++){		
		if(SkillCanUse(%clientId, %skill))
			Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ %skill, %cl @ " " @ %skill);
	}
}
function processMenuUseSkill(%clientId, %option) {
	%cl = floor(getWord(%option, 0));
	%useThis = getWord(%option, 1);
	if(%useThis != "") internalSay(%cl, 0, %useThis);
}

function processMenuSpellCasting(%clientId, %option) {	
	%cl 		= floor(getWord(%option, 0));
	%skillName	= getWord(%option, 1);
	%sp 		= 1; 
	%kwd 		= $Spell::keyword[%sp];
	%curItem	= -1;
	
	Client::buildMenu(%clientId, $SkillDesc[%skillName], "castspell", true);		
	while(string::compare(%kwd,"") != 0) {
		if($SkillType[%kwd] == %skillName && SkillCanUse(%clientId, %kwd)) {
			Client::addMenuItem(%clientId,string::getsubstr($menuChars,%curItem++,1) @ $Spell::name[%sp] , %cl @ " " @ %kwd);
		}
		%sp++;
		%kwd = $Spell::keyword[%sp];
	}
}

function processMenuCastSpell(%clientId, %option) {
	%cl = floor(getWord(%option, 0));
	%castThis = getWord(%option, 1);
	if(%castThis != "")
		internalSay(%cl, 0, "#cast " @ %castThis);
}

function admin::hasPermissionToMod(%client, %victim){
	if(%client == %victim)
		return True;
	if(%client.adminlevel > %victim.adminlevel)
		return True;
	return False;
}

function rpg::CreateHelpMenuItems(%clientId, %curItem) {
	Client::addMenuItem(%clientId, "---------------------------------", "help");
	Client::addMenuItem(%clientId, %curItem++ @ "Where to go" , "wheretogo");
	Client::addMenuItem(%clientId, %curItem++ @ "Leveling up" , "levels");
	Client::addMenuItem(%clientId, %curItem++ @ "Tips & Commands" , "tips");
	Client::addMenuItem(%clientId, %curItem++ @ "Using #skills" , "skills");
	Client::addMenuItem(%clientId, %curItem++ @ "Casting spells" , "spells");
	Client::addMenuItem(%clientId, "[--------------------------------", "help");
	Client::addMenuItem(%clientId, "tThievery" , "thievery");
	Client::addMenuItem(%clientId, "wWordsmith" , "wordsmith");
	Client::addMenuItem(%clientId, "sSurvival" , "survival");
	Client::addMenuItem(%clientId, "]--------------------------------", "help");
	
	Client::addMenuItem(%clientId, "cCombat Arts" , "combatarts");
	Client::addMenuItem(%clientId, "dDistortion Magic" , "distortion");
	Client::addMenuItem(%clientId, "eElemental Magic" , "elemental");
	Client::addMenuItem(%clientId, "iIllusion Magic" , "illusion");
	Client::addMenuItem(%clientId, "rRestoration Magic" , "restoration");
	Client::addMenuItem(%clientId, "|--------------------------------", "help");
	Client::addMenuItem(%clientId, %curItem++ @ "PROTECTED Zones" , "zonesp");
	Client::addMenuItem(%clientId, %curItem++ @ "DUNGEON Zones" , "zonesd");
	
	return %curItem;
}

function ProcessMenuhelp(%clientId, %option, %sendMessageWithoutMenu) {
	%opt = getWord(%option, 0);
	
	if(%sendMessageWithoutMenu == "") {
		Client::buildMenu(%clientId, "Help", "help", true);
		%curItem = rpg::CreateHelpMenuItems(%clientId,%curItem);
	}
	%timeDiv = 15;
	if(%opt == "wheretogo")
	{
		%timeDiv = 15;
		%class = fetchData(%clientId, "CLASS");
		if (%class == "Druid" || %class == "Invoker") {
			%dungeon 	= "Altar of Shame";
			%dungeon2 	= "Abandoned Dig Site";
			%hometown 	= "Oasis";
			%faction 	= "Wildenslayers  or  Order of Qod";
			%skills 	=  $SkillDesc[$SkillWands] @ " | " @ $SkillDesc[$SkillIllusionMagic] 
						@ " " @ $SkillDesc[$SkillDistortionMagic] @ " | " @ $SkillDesc[$SkillEnergy] @ " | " @ $SkillDesc[$SkillEvasion];
			%tips 		= "* Invokers and Druids are Wand users with magic skills and quirks.\n* Druids should wear robes, carry backup weapons, and use Restoration magic.\n* Invokers are pure casters that won't be able to wear equipment easily but CAN use Combat Arts.\n* Don't forget about energy. Offensive casting will quickly drain unprepared Druids and Invokers.";
		} else if (%class == "Mage" || %class == "Paladin") {
			%dungeon 	= "Altar of Shame";
			%dungeon2 	= "Abandoned Dig Site";
			%hometown 	= "Wyzanhyde Priory";
			%faction 	= "Luminous Dawn";
			%skills 	=  $SkillDesc[$SkillVitality] @ " | " @ $SkillDesc[$SkillRestorationMagic] @ " | " @ $SkillDesc[$SkillFocus] @ " | " @ $SkillDesc[$SkillEnergy];
			%tips 		= "* Focus purely on class strengths.\n* The Luminous Dawn will compliment your class, but they are far away.\n* You can never have enough mana. Keep your Energy high at all times.\n* Don't run out of water later on. Thirst will begin to impact your regen rates in mid-game.";
		} else if (%class == "Thief" || %class == "Ranger") {
			%dungeon 	= "Old Ethren";
			%dungeon2 	= "Cravv Keep";
			%hometown 	= "Jaten";
			%faction 	= "Wildenslayers";
			%skills 	=  $SkillDesc[$SkillThievery] @ " | " @ $SkillDesc[$SkillSurvival] @ " | " @ $SkillDesc[$SkillEndurance] @ " | " @ $SkillDesc[$SkillCombatArts] @ " | " @ $SkillDesc[$SkillVitality];
			%tips 		= "* Don't talk to The Mandate! They will kill Thieves.\n* Don't forget about Survival.\n* Invest in a backup weapon.\n* Keep your Endurance high and wear armor.\n* #steal from everything\n* Hotkey your combat skills such as #set 1 #charge or #set 2 #swoop";
		} else if (%class == "Fighter" || %class == "Bard") {
			%dungeon 	= "Keldrin Mine";
			%dungeon2 	= "Stronghold Yolanda";
			%hometown 	= "Keldrin Town";
			%faction 	= "Keldrin Mandate";
			%skills 	=  "Any";
			%tips = "* Your class will handle most weapons with ease.\n* Don't spread points. Invest in ONE secondary skill such as Survival or Wordsmith.\n* Don't forget about support stats such as Strength and Focus.";
		} else if (%class == "Enchanter" || %class == "Merchant") {
			%dungeon 	= "Sail a raft to Keldrin Mine";
			%dungeon2 	= "Collapsed Castle";
			%hometown 	= "Mercator";
			%faction 	= "College of Geoastrics  or   Keldrin Mandate";
			%skills 	=  $SkillDesc[$SkillWordsmith] @ " | " @ $SkillDesc[$SkillEnergy] @ " | " @ $SkillDesc[$SkillVitality] @ " | " @ $SkillDesc[$SkillIllusionMagic];
			%tips = "* Your class is NOT designed for combat early on and starts with disadvantages that become strengths later on.\n* The Luminous Dawn are nearby, but they won't give you the most benefit.\n* You are surrounded by powerful enemies in Mercator. Sailing to Keldrin is the only safe option until level 20.\n* Invest in Wordsmith to become a spellcaster later.\n* Invest in as many support and secondary skills as possible, as they govern your ability to #inscribe idioms.";
		}  else if (%class == "Cleric" || %class == "Brawler") {
			%dungeon 	= "Septic System";
			%dungeon2 	= "Jherigo Pass";
			%hometown 	= "Hazard";
			%faction 	= "Order of Qod";
			%skills 	=  $SkillDesc[$SkillBludgeoning] @ " | " @ $SkillDesc[$SkillVitality] @ " | " @ $SkillDesc[$SkillEnergy];
			%tips = "* Surprising magical abilities are available to you.\n* Joining the Order of Qod early on is a good idea. You can always quit later.\n* Keep one spell type and one weapon type high at all times.\n* Your class excels at tanking and combat. Keep a high Strength to wear the best armor and deal the best damage.";
		}
		
		if(%sendMessageWithoutMenu == "") {
			Client::addMenuItem(%clientId, %curItem++ @ "Casting spells" , "spells");
		}
		%a[%tmp++] = "<f1>HOME: <f0>" @ %hometown @ "\n";
		%a[%tmp++] = "<f1>STARTER DUNGEONS: <f0>" @ %dungeon @ ", <f1>then <f0>" @ %dungeon2 @ "\n";
		%a[%tmp++] = "<f1>IMPORTANT SKILLS: <f0>" @ %skills @ "\n";
		%a[%tmp++] = "<f1>SUGGESTED HOUSE TO SEEK: <f0>" @ %faction @ "\n";
		%a[%tmp++] = "<f2>" @ %tips @ "";
	}
	else if(%opt == "skills")
	{
		%a[%tmp++] = "<f1>USING SKILLS<f0>\n\nType #[skill] as a chat message.\n\n\n";
		%a[%tmp++] = "<f1>HOTKEYING SKILLS<f0>\n\nType #set [key] #[skill] as a chat message. The skill will be used when pressing the given key.\n\n\n";
		%a[%tmp++] = "<f1>LIST SKILLS<f0>\n\nSelect option on the menu above, or, type #list skills [type] as a chat message.\n\n\n";
		%a[%tmp++] = "<f1>DETAILED INFORMATION<f0>\n\nType #help [skill] as a chat message.\n\n\n";
	}
	else if(%opt == "thievery")
	{
		%timeDiv = 5;
		%a[%tmp++] = "<f1>Thievery SKILLS<f0>\n\n" @ $SkillList[$SkillThievery] @ "\n";
	}
	else if(%opt == "wordsmith")
	{
		%timeDiv = 5;
		%a[%tmp++] = "<f1>Wordsmith SKILLS<f0>\n\n" @ $SkillList[$SkillWordsmith] @ "\n";
	}
	else if(%opt == "survival")
	{
		%timeDiv = 5;
		%a[%tmp++] = "<f1>Survival SKILLS<f0>\n\n" @ $SkillList[$SkillSurvival] @ "\n";
	}	
	else if(%opt == "zonesp")
	{
		%timeDiv = 20;
		%a[%tmp++] = "<f1>PROTECTED Zones<f0>\n\n";				
		%a[%tmp++] = "<f2>Hall of Souls <f0>A place where dead souls collect before being sent back to Keldrin.\n";
		%a[%tmp++] = "<f2>Keldrin Town <f0>Fighters and Bards begin here. The Mandate occupies land here.\n";
		%a[%tmp++] = "<f2>Jaten <f0>Rangers and Thieves begin here\n";
		%a[%tmp++] = "<f2>Oasis <f0>Druids and Invokers begin here\n";
		%a[%tmp++] = "<f2>Wyzanhyde Priory <f0>Mages and Paladins begin here\n";
		%a[%tmp++] = "<f2>Mercator <f0>Enchanters and Merchants begin here. Home of the Luminous Dawn.\n";
		%a[%tmp++] = "<f2>Hazard <f0>Brawlers and Clerics begin here\n";
		%a[%tmp++] = "<f2>College of Geoastrics <f0>A place where Geostrologists gather to study\n";
		%a[%tmp++] = "<f2>Fort of Ethren <f0>The Mandate occupies land here\n";
		%a[%tmp++] = "<f2>Wellsprings <f0>Once occupied by Druids - now, it's dry\n";
		%a[%tmp++] = "<f2>Restaurant at the End of the World <f0>???\n";
		%a[%tmp++] = "<f2>Fight Club <f0>???\n";
	} 
	else if(%opt == "zonesd")
	{
		%timeDiv = 30;
		%a[%tmp++] = "<f1>DUNGEONS, Enemies, and Level Ranges<f0>\n\n";
		%a[%tmp++] = "<f1>{([ 1   to   10 ])}\n";
		%a[%tmp++] = "<f2>Altar of Shame    <f0>Savages\n";
		%a[%tmp++] = "<f2>Keldrin Mine        <f0>Goblins\n";
		%a[%tmp++] = "<f2>Old Ethren          <f0>Gnolls\n";
		%a[%tmp++] = "<f2>Septic System      <f0>Lost Souls\n";
		
		%a[%tmp++] = "\n<f1>{([ 10   to   30 ])}\n";
		%a[%tmp++] = "<f2>Abandoned Dig Site   <f0>Churls\n";
		%a[%tmp++] = "<f2>Collapsed Castle       <f0>Ratmen and Minotaurs\n";
		%a[%tmp++] = "<f2>Orc Base              <f0>Orcs\n";
		%a[%tmp++] = "<f2>Stronghold Yolanda   <f0>Orcs\n";
		%a[%tmp++] = "<f2>Jherigo Pass           <f0>Jheriman\n";
		%a[%tmp++] = "<f2>Cravv Keep           <f0>Craven\n";
		
		%a[%tmp++] = "\n<f1>{([ 30   to   50 ])}\n";
		%a[%tmp++] = "<f2>The Wastes            <f0>Ogres\n";
		%a[%tmp++] = "<f2>Tainted Sewer         <f0>Churls\n";
		%a[%tmp++] = "<f2>Graveyard             <f0>Zombies\n";
		%a[%tmp++] = "<f2>Temple Of Delusion    <f0>Deluded\n";
		
		%a[%tmp++] = "\n<f1>{([ 50   to   70 ])}\n";
		%a[%tmp++] = "<f2>Traveller's Canyon    <f0>Travellers\n";
		%a[%tmp++] = "<f2>Elven Outpost         <f0>Elves\n";
		%a[%tmp++] = "<f2>Amazon Forest        <f0>Amazons\n";
		
		%a[%tmp++] = "\n<f1>{([ 70   to   90 ])}\n";
		%a[%tmp++] = "<f2>Cavern Of Torment    <f0>Imps\n";
		%a[%tmp++] = "<f2>Damned Crypt          <f0>Skeletons\n";
		%a[%tmp++] = "<f2>Antaris Remains        <f0>Antanari\n";
		%a[%tmp++] = "<f2>Palace Safeway         <f0>Minotaurs\n";
		%a[%tmp++] = "<f2>Black Market           <f0>Outlaws\n";
		
		%a[%tmp++] = "\n<f1>{([   90+   ])}\n";
		%a[%tmp++] = "<f2>Kymer Deadwood       <f0>Kymera\n";
		%a[%tmp++] = "<f2>Overville                <f0>Cragspawn\n";
		%a[%tmp++] = "<f2>Chamber of Animation  <f0>Golems\n";		
		%a[%tmp++] = "<f2>Undercity                <f0>Demons and fodder\n";
		%a[%tmp++] = "<f2>Ancient Sanctuary       <f0>Ancients\n";
		
		
		%a[%tmp++] = "\n<f1>{([   ELDERS   ])}\n";
		%a[%tmp++] = "<f2>Mylehi Watch      <f0>Elle\n";
		%a[%tmp++] = "<f2>Solitude            <f0>Pios\n";
		%a[%tmp++] = "<f2>Icon of Delusion    <f0>Gat\n";
		%a[%tmp++] = "<f2>Secluded Cave      <f0>Squall\n";
		%a[%tmp++] = "<f2>Creten's Arena     <f0>Creten\n";
		%a[%tmp++] = "<f2>Syncronicon        <f0>Qod\n";
		%a[%tmp++] = "<f2>Eviscera            <f0>Zatan\n";
	}	
	else if(%opt == "spells")
	{
		%timeDiv = 10;
		%a[%tmp++] = "<f1>CASTING SPELLS<f0>\n\nType #cast [spell] as a chat message.\n\n\n";
		%a[%tmp++] = "<f1>HOTKEYING SPELLS<f0>\n\nType #set [key] #cast [spell] as a chat message. The spell will be used when pressing the given key.\n\n\n";
		%a[%tmp++] = "<f1>LIST SPELLS<f0>\n\nSelect option on the menu above, or, type #list spells [type]Magic as a chat message.\n\n\n";
		%a[%tmp++] = "<f1>DETAILED INFORMATION<f0>\n\nType #help [spell] as a chat message.\n\n\n";
	}
	else if(%opt == "combatarts")
	{
		%timeDiv = 15;
		%skillName = "CombatArts"; %sp = 1; %kwd = $Spell::keyword[%sp];
		%a[%tmp++] = "<f1>" @ %skillName @ " Name | Minimum Skill Required\n\n";
		if($SkillIndex[%skillName] != "") {
			while(string::compare(%kwd,"") != 0){
				%kwd = $Spell::keyword[%sp];
				if($SkillType[%kwd] == $SkillIndex[%skillName])
					%a[%tmp++] = "<f2>" @ %kwd @ "<f0> ......... <f1>" @ rpg::GetAdjustedSkillRestriction(%clientId, %kwd, $SkillType[%kwd]) @ "\n";
				%sp++;
			}
		}
	}
	else if(%opt == "distortion")
	{
		%timeDiv = 15;
		%skillName = "DistortionMagic"; %sp = 1; %kwd = $Spell::keyword[%sp];
		%a[%tmp++] = "<f1>" @ %skillName @ " Spell Name | Minimum Skill Required\n\n";
		if($SkillIndex[%skillName] != "") {
			while(string::compare(%kwd,"") != 0){
				%kwd = $Spell::keyword[%sp];
				if($SkillType[%kwd] == $SkillIndex[%skillName])
					%a[%tmp++] = "<f2>" @ %kwd @ "<f0> ......... <f1>" @ rpg::GetAdjustedSkillRestriction(%clientId, %kwd, $SkillType[%kwd]) @ "\n";
				%sp++;
			}
		}
	}
	else if(%opt == "elemental")
	{
		%timeDiv = 15;
		%skillName = "ElementalMagic"; %sp = 1; %kwd = $Spell::keyword[%sp];
		%a[%tmp++] = "<f1>" @ %skillName @ " Spell Name | Minimum Skill Required\n\n";
		if($SkillIndex[%skillName] != "") {
			while(string::compare(%kwd,"") != 0){
				%kwd = $Spell::keyword[%sp];
				if($SkillType[%kwd] == $SkillIndex[%skillName])
					%a[%tmp++] = "<f2>" @ %kwd @ "<f0> ......... <f1>" @ rpg::GetAdjustedSkillRestriction(%clientId, %kwd, $SkillType[%kwd]) @ "\n";
				%sp++;
			}
		}
	}
	else if(%opt == "illusion")
	{
		%timeDiv = 15;
		%skillName = "IllusionMagic"; %sp = 1; %kwd = $Spell::keyword[%sp];
		%a[%tmp++] = "<f1>" @ %skillName @ " Spell Name | Minimum Skill Required\n\n";
		if($SkillIndex[%skillName] != "") {
			while(string::compare(%kwd,"") != 0){
				%kwd = $Spell::keyword[%sp];
				if($SkillType[%kwd] == $SkillIndex[%skillName])
					%a[%tmp++] = "<f2>" @ %kwd @ "<f0> ......... <f1>" @ rpg::GetAdjustedSkillRestriction(%clientId, %kwd, $SkillType[%kwd]) @ "\n";
				%sp++;
			}
		}
	}
	else if(%opt == "restoration")
	{
		%timeDiv = 40;
		%skillName = "RestorationMagic"; %sp = 1; %kwd = $Spell::keyword[%sp];
		%a[%tmp++] = "<f1>" @ %skillName @ " Spell Name | Minimum Skill Required\n\n";
		if($SkillIndex[%skillName] != "") {
			while(string::compare(%kwd,"") != 0){
				%kwd = $Spell::keyword[%sp];
				if($SkillType[%kwd] == $SkillIndex[%skillName])
					%a[%tmp++] = "<f2>" @ %kwd @ "<f0> ......... <f1>" @ rpg::GetAdjustedSkillRestriction(%clientId, %kwd, $SkillType[%kwd]) @ "\n";
				%sp++;
			}
		}
	}
	else if(%opt == "tips")
	{
		%a[%tmp++] = "<f1>SAVE: <f0>#savecharacter\n";
		%a[%tmp++] = "<f1>Set a hotkey: <f0>#set [key] #[command]\n";
		%a[%tmp++] = "<f1>Meditate: <f0>#meditate\n";
		%a[%tmp++] = "<f1>Wake Up (during sleep, meditation, or knockout): <f0>#wake\n";
		%a[%tmp++] = "<f1>Smith new items: <f0>#smith when pointing at any anvil or tent -- or anywhere, with enough Survival\n";
		%a[%tmp++] = "<f1>Erase your character: <f0>#resetcharacter [YourName]\n";
	}
	else if(%opt == "levels")
	{
		%a[%tmp++] = "<f1>Level up\n<f0>Kill enemies to gain experience. Each level requires 1000 experience.\n\n";
		%a[%tmp++] = "<f1>Check enemy levels\n<f0>Press T when looking at an enemy to display their level.\n\n";
		%a[%tmp++] = "<f1>Stay alive\n<f0>Buy healing items. Use healing spells. Scout enemy levels in an area before attacking.\n\n";
		%a[%tmp++] = "<f1>Join a House\n<f0>Join a House to earn additional experience called Rank. Each House has different tasks to acquire Rank.\n\n";
	}
	
	for(%i = 1; %a[%i] != ""; %i++)
		%f = %f @ %a[%i];

	%pos = 0;
	if(%sendMessageWithoutMenu == "" && %timeDiv <= 10)
		%pos = 1;
	rpg::longPrint(%clientId, %f, %pos, floor(String::len(%f) / %timeDiv));
	return;
}

function processMenuadmincontrol(%clientId, %option){
	%opt = getWord(%option, 0);
	if(%opt == "adminlevel"){
		if(%clientId.adminLevel < 1)
			return;
		Client::buildMenu(%clientId, "Your admin level (cur:"@%clientId.adminlevel@")", "adminlevel", true);
		%curItem = -1;
		for(%i = 0; %i <= %clientId.adminLevel; %i++){
			Client::addMenuItem(%clientId, string::getsubstr($menuChars,%curItem++,1) @ "Level "@%i , %i);
		}
	}
	if(%clientId.adminlevel < 1)
		return;
	%cl = getWord(%option, 1);
	if(%opt == "eyes"){
		if(admin::hasPermissionToMod(%clientId, %cl))
			rpg::eyes(%clientId, %cl);
		else
			Client::sendMessage(%clientId, 0, "Could not process command: Target admin clearance level too high.");
	}
	if(%clientId.adminlevel < 2)
		return;
	if(%opt == "teleport"){
		admin::teleport(%clientId, %cl);
	}
	if(%opt == "teleportto"){
		admin::teleportto(%clientId, %cl);
	}
	if(%opt == "forcerecall"){
		admin::forcerecall(%clientId, %cl);
	}
	if(%opt == "kick"){		Client::buildMenu(%clientId, "Confirm kick?", "acconfirm", true);		Client::addMenuItem(%clientId, "1Confirm kick" , "confirm "@%opt@" "@%cl);		Client::addMenuItem(%clientId, "2Cancel" , "cancel");
	}
	if(%opt == "kill"){
		Player::kill(%cl);
	}
	if(%opt == "maxcurstat"){
		setHP(%clientId, fetchData(%clientId, "MaxHP"));
		setMANA(%clientId, fetchData(%clientId, "MaxMANA"));
		storeData(%clientId, "Thirst", 600);
	}	
	if(%opt == "setrace"){
		Client::buildMenu(%clientId, "Set race for "@rpg::getname(%cl), "setrace", true);
		Client::addMenuItem(%clientId, "1Male Human" , "MaleHuman "@%cl);
		Client::addMenuItem(%clientId, "2Female Human" , "FemaleHuman "@%cl);
		Client::addMenuItem(%clientId, "3Death Knight" , "Orc "@%cl);
	}
}
function processMenuacconfirm(%clientId, %option){	%c = getWord(%option,0);	%opt = getWord(%option,1);	%cl = getWord(%option,2);	if(%c == "confirm"){		if(%opt == "kick"){			if(admin::hasPermissionToMod(%clientId, %cl))				Admin::DirectKick(%clientId, %cl, "You have been kicked by the administrator.");			else				Client::sendMessage(%clientId, 0, "Could not process command: Target admin clearance level too high.");		}	}}

function processMenusetrace(%clientId, %option){
	if(%clientId.adminlevel < 3) return;
	%cl = getWord(%option,1);
	if(!admin::hasPermissionToMod(%clientId, %cl)){
		Client::sendMessage(%clientId, 0, "Could not process command: Target admin clearance level too high.");
		return;
	}
	%race = getWord(%option,0);
	ChangeRace(%cl, %race);
}

function processMenuadminlevel(%clientId, %option){
	%name = rpg::getname(%clientId);
	if(%option > %clientId.adminLevel)
		return;
	%clientId.adminlevel = %option;
	pecho("Setting "@%name@" admin level to "@%option);
	Client::sendMessage(%clientId, $msgWhite, "Your admin level is now "@%option);
}

function processMenusettings(%clientId, %option)
{

	%opt = getWord(%option, 0);
	%cl = getWord(%option, 1);

	if(%opt == "defaulttalk")
	{

		Client::buildMenu(%clientId, "Default Talk", "deftalk", true);
		%defaulttalks[1] = "#group";
		%defaulttalks[2] = "#global";
		%defaulttalks[3] = "#say";
		for(%i = 1; %defaulttalks[%i] != ""; %i++)
			Client::addMenuItem(%clientId, %i@%defaulttalks[%i], %defaulttalks[%i]);
	}
	else if(%opt == "gignoreon")
	{
		storeData(%clientId, "ignoreGlobal", True);
	}
	else if(%opt == "gignoreoff")
	{
		storeData(%clientId, "ignoreGlobal", "");
	}
	else if(%opt == "battmsg")
	{
		if(!Player::isAiControlled(%clientId) && %clientId > 2048) {
			Client::buildMenu(%clientId, "Battle message config:", "battmsgcfg", true);			%cur = fetchData(%clientId, "battlemsg");			if(%cur == "")				%cur = "auto";			%choices = "chathud topprint bottomprint auto";			%eng["chathud"] = "Chat HUD";			%eng["topprint"] = "Top Print";			%eng["bottomprint"] = "Bottom Print";			%eng["auto"] = "Automatic";			if(%clientId.repack >= 32){				%choices = "chathud topleft topprint bottomleft bottomprint auto";				%eng["topleft"] = "Top Left";				%eng["topprint"] = "Top Right";				%eng["bottomleft"] = "Bottom Left";				%eng["bottomprint"] = "Bottom Right";			}						for(%i = 0; getword(%choices, %i) != -1; %i++)			{				%w = getword(%choices, %i);				%eng = %eng[%w];				if(%cur == %w)					%eng = string::translate(%eng);				Client::addMenuItem(%clientId, %curitem++ @ %eng, %w);			}			if(%clientId.repack < 32)				Client::addMenuItem(%clientId, %curitem++ @ "Print # of lines..", "printlength");
		}
		return;
	}
	else if(%opt == "partyoptions")
	{
		Client::buildMenu(%clientId, "Party options", "partyopt", true);

		if(fetchData(%clientId, "partyOwned"))
			Client::addMenuItem(%clientId, "xDisband party", "disbandparty");
		else
			Client::addMenuItem(%clientId, "cCreate party", "createparty");

		%name = Client::getName(%clientId);
		if( (%p = IsInWhichParty(%name)) != -1)
		{
			%id = GetWord(%p, 0);
			%inv = GetWord(%p, 1);
			if(%inv == -1)
			{
				//this player is in the party
				Client::addMenuItem(%clientId, "pLeave current party", "leaveparty " @ %id);
			}
			else if(%inv == "i")
			{
				//this player is being invited
				Client::addMenuItem(%clientId, "pAccept " @ Client::getName(%id) @ "'s party invitation", "acceptinv " @ %id);
			}
		}

		%list = fetchData(%clientId, "partylist");
		for(%p = String::findSubStr(%list, ","); (%p = String::findSubStr(%list, ",")) != -1; %list = String::NEWgetSubStr(%list, %p+1, 99999))
		{
			%w = String::NEWgetSubStr(%list, 0, %p);
			Client::addMenuItem(%clientId, %curitem++ @ "Remove " @ %w, "remparty " @ %w);
		}
	} else Game::menuRequest(%clientId);
}
function processMenudeftalk(%clientId, %option)
{
	%defaulttalks[1] = "#group";
	%defaulttalks[2] = "#global";
	%defaulttalks[3] = "#say";
	for(%i = 1; %defaulttalks[%i] != ""; %i++){
		if(%option == %defaulttalks[%i])
			storeData(%clientId, "defaultTalk", %option);
	}
}
function processMenubattmsgcfg(%clientId, %option)
{
	dbecho($dbechoMode, "processMenuselectctrlk(" @ %clientId @ ", " @ %option @ ")");

	%opt = GetWord(%option, 0);
	%cl = GetWord(%option, 1);

	if(%opt == "printlength"){
		Client::buildMenu(%clientId, "Print # of lines...", "battmsglines", true);
		Client::addMenuItem(%clientId, %curItem++ @ "1" , "1");
		Client::addMenuItem(%clientId, %curItem++ @ "4" , "4");
		if(%clientId.repack >= 9){
			Client::addMenuItem(%clientId, %curItem++ @ "5" , "5");
			Client::addMenuItem(%clientId, %curItem++ @ "8" , "8");
			Client::addMenuItem(%clientId, %curItem++ @ "10" , "10");
			Client::addMenuItem(%clientId, %curItem++ @ "15" , "15");
			Client::addMenuItem(%clientId, %curItem++ @ "20" , "20");
		}
		return;
	}

	storeData(%clientId, "battlemsg", %opt);

	%eng["chathud"] = "Chat HUD";	%eng["topprint"] = "Top Print";	%eng["bottomprint"] = "Bottom Print";	%eng["auto"] = "Automatic";	if(%clientId.repack >= 32){		%eng["topleft"] = "Top Left";		%eng["topprint"] = "Top Right";		%eng["bottomleft"] = "Bottom Left";		%eng["bottomprint"] = "Bottom Right";	}

	if(%clientId.repack >= 32)		refreshBattleHudPos(%clientId);	processMenusettings(%clientId, "battmsg");

//	if(%opt == "chathud")
//		Client::sendMessage(%ClientId, 0, "Battle messages now appear in your chat hud.");
//	else if(%opt == "bottomprint")
//		Client::sendMessage(%ClientId, 0, "Battle messages now appear at the bottom of your screen.");
//	else if(%opt == "topprint")
//		Client::sendMessage(%ClientId, 0, "Battle messages now appear at the top of your screen.");
}

function processMenupartyopt(%clientId, %option)
{
	dbecho($dbechoMode, "processMenupartyopt(" @ %clientId @ ", " @ %option @ ")");

	%opt = getWord(%option, 0);
	%cl = getWord(%option, 1);

	if(%opt == "disbandparty")
	{
		DisbandParty(%clientId);
	}
	else if(%opt == "createparty")
	{
		CreateParty(%clientId);
	}
	else if(%opt == "remparty")
	{
		RemoveFromParty(%clientId, %cl);
	}
	else if(%opt == "acceptinv")
	{
		%name = Client::getName(%clientId);
		if( (%p = IsInWhichParty(%name)) != -1)
		{
			%id = GetWord(%p, 0);
			%inv = GetWord(%p, 1);
			if(%inv == "i")
				AddToParty(%id, %name);
		}
	}
	else if(%opt == "leaveparty")
	{
		RemoveFromParty(%cl, Client::getName(%clientId));
	}

	return;
}

function processMenuselectspell(%clientId, %option)
{
	dbecho($dbechoMode, "processMenuselectspell(" @ %clientId @ ", " @ %option @ ")");

	%name = Client::getName(%clientId);

	$playerCurrentSpell[%clientId] = $spellShell[%option];
}
function processMenuselectrweapon(%clientId, %item)
{
	%list = GetAccessoryList(%clientId, 10, -1);

	Client::buildMenu(%clientId, "Projectiles:", "selectproj", true);
	for(%i = 0; GetWord(%list, %i) != -1; %i++)
	{
		%proj = GetWord(%list, %i);

		if(String::findSubStr($ProjRestrictions[%proj], "," @ %item @ ",") != -1)
			Client::addMenuItem(%clientId, %curitem++ @ $beltitem[%proj, "Name"], %item @ " " @ %proj);
	}
	return;
}
function processMenuselectproj(%clientId, %itemandproj)
{
	%item = GetWord(%itemandproj, 0);
	%proj = GetWord(%itemandproj, 1);

	storeData(%clientId, "LoadedProjectile " @ %item, %proj);
}

function processMenuOtheropt(%clientId, %option)
{
	dbecho($dbechoMode, "processMenuOtheropt(" @ %clientId @ ", " @ %option @ ")");

	%opt = GetWord(%option, 0);
	%cl = GetWord(%option, 1);
	if(%opt == "mute")
	      %clientId.muted[%cl] = true;
	else if(%opt == "unmute")
		%clientId.muted[%cl] = "";
	else if(%opt == "voteYes" && %cl == $curVoteCount)
	{
	      %clientId.vote = "yes";
	 	centerprint(%clientId, "", 0);
	}
	else if(%opt == "voteNo" && %cl == $curVoteCount)
	{
	      %clientId.vote = "no";
	      centerprint(%clientId, "", 0);
	}
	else if(%opt == "kick")
	{
	      Client::buildMenu(%clientId, "Confirm kick:", "kaffirm", true);
	      Client::addMenuItem(%clientId, "1Kick " @ Client::getName(%cl), "yes " @ %cl);
	      Client::addMenuItem(%clientId, "2Don't kick " @ Client::getName(%cl), "no " @ %cl);
	      return;
	}
	else if(%opt == "admin")
	{
	      Client::buildMenu(%clientId, "Confirm admim:", "aaffirm", true);
	      Client::addMenuItem(%clientId, "1Admin " @ Client::getName(%cl), "yes " @ %cl);
	      Client::addMenuItem(%clientId, "2Don't admin " @ Client::getName(%cl), "no " @ %cl);
	      return;
	}
	else if(%opt == "ban")
	{
	      Client::buildMenu(%clientId, "Confirm Ban:", "baffirm", true);
	      Client::addMenuItem(%clientId, "1Ban " @ Client::getName(%cl), "yes " @ %cl);
	      Client::addMenuItem(%clientId, "2Don't ban " @ Client::getName(%cl), "no " @ %cl);
		return;
	}
	Game::menuRequest(%clientId);
}

function remoteSelectClient(%clientId, %selId)
{
	dbecho($dbechoMode, "remoteSelectClient(" @ %clientId @ ", " @ %selId @ ")");

   if(%clientId.selClient != %selId)
   {
      %clientId.selClient = %selId;
      if(%clientId.menuMode == "options")
         Game::menuRequest(%clientId);
      remoteEval(%clientId, "setInfoLine", 1, "Player Info for " @ Client::getName(%selId) @ ":");
      remoteEval(%clientId, "setInfoLine", 2, "Real Name: " @ $Client::info[%selId, 1]);
      remoteEval(%clientId, "setInfoLine", 3, "Email Addr: " @ $Client::info[%selId, 2]);
      remoteEval(%clientId, "setInfoLine", 5, "URL: " @ $Client::info[%selId, 4]);
   }
}


function processMenuPickTeam(%clientId, %team, %adminClient)
{
	dbecho($dbechoMode, "processMenuPickTeam(" @ %clientId @ ", " @ %team @ ", " @ %adminClient @ ")");

   if(%team != -1 && %team == Client::getTeam(%clientId))
      return;

   if(%clientId.observerMode == "justJoined")
   {
      %clientId.observerMode = "";
      centerprint(%clientId, "");
   }

   if((!$matchStarted || !$Server::TourneyMode || %adminClient) && %team == -2)
   {
      if(Observer::enterObserverMode(%clientId))
      {
         %clientId.notready = "";
         if(%adminClient == "") 
            messageAll(0, Client::getName(%clientId) @ " became an observer.");
         else
            messageAll(0, Client::getName(%clientId) @ " was forced into observer mode by " @ Client::getName(%adminClient) @ ".");
		   Game::refreshClientScore(%clientId);
		}
      return;
   }

   %player = Client::getOwnedObject(%clientId);
   %clientId.observerMode = "";

   if(%team == -1)
   {
      UpdateTeam(%clientId);
      %team = Client::getTeam(%clientId);
   }
   GameBase::setTeam(%clientId, %team);
   %clientId.teamEnergy = 0;
	Client::clearItemShopping(%clientId);
	if(Client::getGuiMode(%clientId) != 1)
		Client::setGuiMode(%clientId,1);		
	Client::setControlObject(%clientId, -1);

   Game::playerSpawn(%clientId, false);
	%team = Client::getTeam(%clientId);
	if($TeamEnergy[%team] != "Infinite")
		$TeamEnergy[%team] += $InitialPlayerEnergy;
}


function admin::teleport(%clientId, %id){
	if(!admin::hasPermissionToMod(%clientId, %id)){
		Client::sendMessage(%clientId, 0, "Could not process command: Target admin clearance level too high.");
		return;
	}
	%player = Client::getOwnedObject(%clientId);
	if(GameBase::getLOSinfo(%player, 1000)){
		GameBase::setPosition(%id, $los::position);
		CheckAndBootFromArena(%id);
	}
	else if(GameBase::getLOSinfo(%player, 2000)){
		GameBase::setPosition(%id, $los::position);
		CheckAndBootFromArena(%id);
	}
	else if(GameBase::getLOSinfo(%player, 6000)){
		GameBase::setPosition(%id, $los::position);
		CheckAndBootFromArena(%id);
	}
	else{
		Client::sendMessage(%TrueClientId, 0, "Need to look at something.");
		return;
	}
	Client::sendMessage(%clientId, 0, "Teleporting " @ rpg::getname(%id) @ " to " @ $los::position @ ".");
}


function admin::teleportto(%clientId, %id){
	if(!admin::hasPermissionToMod(%clientId, %id))
		Client::sendMessage(%clientId, 0, "Could not process command: Target admin clearance level too high.");
	else
	{
		Client::sendMessage(%clientId, 0, "Teleporting " @ rpg::getname(%clientId) @ " (" @ %clientId @ ") to " @ rpg::getname(%id) @ " (" @ %id @ ").");
		GameBase::setPosition(%clientId, GameBase::getPosition(%id));
		CheckAndBootFromArena(%clientId);
	}
}
function admin::forcerecall(%clientId, %id){
	if(!admin::hasPermissionToMod(%clientId, %id)){
		Client::sendMessage(%clientId, 0, "Could not process command: Target admin clearance level too high.");
		return;
	}
	Client::sendMessage(%clientId, 0, "Teleporting " @ rpg::getname(%id) @ " to the recall point.");
	FellOffMap(%id);
}