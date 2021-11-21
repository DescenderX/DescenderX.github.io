//________________________________________________________________________________________________________________________________________________________________
// DescX Notes:
//		See comchat_admin.cs. Same notes ;)
//________________________________________________________________________________________________________________________________________________________________
// TALK / CHAT COMMANDS
//________________________________________________________________________________________________________________________________________________________________
function processChat(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai) {
	%botTalk 	= False;
	%processed	= False;
	
	if(%w1 == "#say" || %w1 == "#s") {
		%processed = True;
		for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
		{
			%talkingPos = GameBase::getPosition(%TrueClientId);
			%receivingPos = GameBase::getPosition(%cl);
			%distVec = Vector::getDistance(%talkingPos, %receivingPos);
			if(%distVec <= $maxSAYdistVec)
			{
				//%newmsg = FadeMsg(%cropped, %distVec, $maxSAYdistVec);
				%newmsg = %cropped;

				if(!%cl.muted[%TrueClientId] && %cl != %TrueClientId)
				Client::sendMessage(%cl, $MsgWhite, %TCsenderName @ " says, \"" @ %newmsg @ "\"");
			}
		}
		Client::sendMessage(%TrueClientId, $MsgWhite, "You say, \"" @ %cropped @ "\"");
		%botTalk = True;
	}
	else if(%w1 == "#shout") {
		%processed = True;
		for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
		{
			%talkingPos = GameBase::getPosition(%TrueClientId);
			%receivingPos = GameBase::getPosition(%cl);
			%distVec = Vector::getDistance(%talkingPos, %receivingPos);
			if(%distVec <= $maxSHOUTdistVec)
			{
				%newmsg = %cropped;
				if(!%cl.muted[%TrueClientId] && %cl != %TrueClientId)
				Client::sendMessage(%cl, $MsgWhite, %TCsenderName @ " shouts, \"" @ %newmsg @ "\"");
			}
		}
		Client::sendMessage(%TrueClientId, $MsgWhite, "You shouted, \"" @ %cropped @ "\"");
		%botTalk = True;			
	}
	else if(%w1 == "#whisper") {
		%processed = True;
		for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
		{
			%talkingPos = GameBase::getPosition(%TrueClientId);
			%receivingPos = GameBase::getPosition(%cl);
			%distVec = Vector::getDistance(%talkingPos, %receivingPos);
			if(%distVec <= $maxWHISPERdistVec)
			{
				%newmsg = %cropped;
				if(!%cl.muted[%TrueClientId] && %cl != %TrueClientId)
				Client::sendMessage(%cl, $MsgWhite, %TCsenderName @ " whispers, \"" @ %newmsg @ "\"");
			}
		}
		Client::sendMessage(%TrueClientId, $MsgWhite, "You whisper, \"" @ %cropped @ "\"");
		%botTalk = True;
	}
	else if(%w1 == "#tell") {
		%processed = True;
		if(%cropped == "") {
			Client::sendMessage(%TrueClientId, 0, "syntax: #tell whoever, message");
		} else {
			%pos1 = 0;
			%pos2 = String::findSubStr(%cropped, ",");
			%name = String::getSubStr(%cropped, %pos1, %pos2-%pos1);
			%final = String::getSubStr(%cropped, %pos2 + 2, String::len(%cropped)-%pos2-2);
			%cl = NEWgetClientByName(%name);

			if(%cl != -1)
			{
				%n = Client::getName(%cl);	//capitalize the name properly
				if(!%cl.muted[%TrueClientId])
				{
					Client::sendMessage(%cl, $MsgBeige, %TCsenderName @ " tells you, \"" @ %final @ "\"");
					if(%cl != %TrueClientId)
						Client::sendMessage(%TrueClientId, $MsgBeige, "You tell " @ %n @ ", \"" @ %final @ "\"");
					%cl.replyTo = %TCsenderName;
				}
				else Client::sendMessage(%TrueClientId, $MsgRed, %n @ " has muted you.");
			}
			else Client::sendMessage(%TrueClientId, $MsgWhite, "Invalid player name.");
		}
		%botTalk = True;
	}
	else if(%w1 == "#r") {
		%processed = True;
		if(%cropped == "") {
			Client::sendMessage(%TrueClientId, 0, "syntax: #r message");
		} else {
			%name = %TrueClientId.replyTo;
			if(%name != "")
			{
				%cl = NEWgetClientByName(%name);

				if(%cl != -1)
				{
					if(!%cl.muted[%TrueClientId])
					{
						Client::sendMessage(%cl, $MsgBeige, %TCsenderName @ " tells you, \"" @ %cropped @ "\"");
						if(%cl != %TrueClientId)
							Client::sendMessage(%TrueClientId, $MsgBeige, "You tell " @ %name @ ", \"" @ %cropped @ "\"");
						%cl.replyTo = %TCsenderName;
					}
				}
				else Client::sendMessage(%TrueClientId, $MsgWhite, "Invalid player name.");
				%botTalk = True;
			}
			else Client::sendMessage(%TrueClientId, $MsgWhite, "You haven't received a #tell to reply to yet.");
		}
	}
	else if(%w1 == "#zone") {
		%processed = True;
		for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl)) {
			if(!%cl.muted[%TrueClientId] && %cl != %TrueClientId && fetchData(%cl, "zone") == fetchData(%TrueClientId, "zone"))
			{
				if(%cl.alttext)	Client::sendMessage(%cl, $MsgGreen, string::translate2("[ZONE] ") @ %TCsenderName @ " - " @ %cropped);
				else			Client::sendMessage(%cl, $MsgGreen, "[ZONE] " @ %TCsenderName @ " - " @ %cropped);
			}
		}
		if(%TrueClientId.alttext)	Client::sendMessage(%TrueClientId, $MsgGreen, string::translate2("[ZONE] ") @ %cropped);
		else						Client::sendMessage(%TrueClientId, $MsgGreen, "[ZONE] " @ %cropped);
	}
	else if(%w1 == "#group") {
		%processed = True;
		for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
		{
			if(!%cl.muted[%TrueClientId] && %cl != %TrueClientId && IsInCommaList(fetchData(%TrueClientId, "grouplist"), Client::getName(%cl)))
			{
				if(IsInCommaList(fetchData(%cl, "grouplist"), %TCsenderName)){
					if(%cl.alttext)	Client::sendMessage(%cl, $MsgBeige, string::translate2("[GRP] ") @ %TCsenderName @ " \"" @ %cropped @ "\"");
					else			Client::sendMessage(%cl, $MsgBeige, "[GRP] " @ %TCsenderName @ " \"" @ %cropped @ "\"");
				}
				else Client::sendMessage(%TrueClientId, $MsgRed, Client::getName(%cl) @ " does not have you on his/her group-list.");
			}
		}
		if(%TrueClientId.alttext)	Client::sendMessage(%TrueClientId, $MsgBeige, string::translate2("[GRP]")@" \"" @ %cropped @ "\"");
		else						Client::sendMessage(%TrueClientId, $MsgBeige, "[GRP] \"" @ %cropped @ "\"");
	}
	else if(%w1 == "#party" || %w1 == "#p") {
		%processed = True;
		%list = GetPartyListIAmIn(%TrueClientId);
		for(%p = String::findSubStr(%list, ","); (%p = String::findSubStr(%list, ",")) != -1; %list = String::NEWgetSubStr(%list, %p+1, 99999))
		{
			%cl = NEWgetClientByName(String::NEWgetSubStr(%list, 0, %p));
			if(!%cl.muted[%TrueClientId] && %cl != %TrueClientId)
				Client::sendMessage(%cl, $MsgBeige, "[PRTY] " @ %TCsenderName @ " \"" @ %cropped @ "\"");
		}
		Client::sendMessage(%TrueClientId, $MsgBeige, "[PRTY] \"" @ %cropped @ "\"");			
	}
	
	if ((IsDead(%TrueClientId) == False || %TrueClientId == 2048) && %botTalk) {
		%list = GetEveryoneIdList();
		for(%i = 0; GetWord(%list, %i) != -1; %i++) {
			%oid = GetWord(%list, %i);

			%time = getIntegerTime(true) >> 5;
			if(%time - fetchData(%oid, "nextOnHear") > 0.05) {
				storeData(%oid, "nextOnHear", %time);

				%oname = Client::getName(%oid);

				%index = GetEventCommandIndex(%oid, "onHear");
				if(%index != -1) {
					for(%i2 = 0; (%index2 = GetWord(%index, %i2)) != -1; %i2++) {
						%ec = $EventCommand[%oid, %index2];
	
						%hearName = GetWord(%ec, 2);
						%radius = GetWord(%ec, 3);
						if(Vector::getDistance(GameBase::getPosition(%oid), GameBase::getPosition(%TrueClientId)) <= %radius) {
							%targetname = GetWord(%ec, 5);
							if(String::ICompare(%targetname, "all") != 0)
								%targetId = NEWgetClientByName(%targetname);
	
							if(String::ICompare(%targetname, "all") == 0 || %targetId == %TrueClientId) {
								%sname = GetWord(%ec, 0);
								%type = GetWord(%ec, 1);
								%keep = GetWord(%ec, 4);
								%var = GetWord(%ec, 6);
								if(String::ICompare(%var, "var") == 0)
									%var = True;
								else {
									%div1 = String::findSubStr(%ec, "|");
									%div2 = String::ofindSubStr(%ec, "|", %div1+1);
									%text = String::NEWgetSubStr(%ec, %div1+1, %div2);
									%oec = String::NEWgetSubStr(%ec, %div1+%div2+2, 99999);
								}
	
								if(String::ICompare(%cropped, %text) == 0 || %var) {
									if((%cl = NEWgetClientByName(%sname)) == -1)
										%cl = 2048;

									%cmd = String::NEWgetSubStr($EventCommand[%oid, %index2], String::findSubStr($EventCommand[%oid, %index2], ">")+1, 99999);
									if(%var)
										%cmd = String::replace(%cmd, "^var", %cropped);
	
									%pcmd = ParseBlockData(%cmd, %TrueClientId, "");
									if(!%keep)
										$EventCommand[%oid, %index2] = "";
									internalSay(%cl, 0, %pcmd, %sname);
								}
							}
						}
					}
				}
			}
		}
	}
	
	return %processed;
}



//________________________________________________________________________________________________________________________________________________________________
// PACK MANAGEMENT COMMANDS
//________________________________________________________________________________________________________________________________________________________________
function processPackManagement(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai) {
	%processed = False;
	
	if(%w1 == "#createpack") {
		%processed = True;
		if(fetchData(%TrueClientId, "TempPack") != "") {
			if(HasThisStuff(%TrueClientId, fetchData(%TrueClientId, "TempPack"))) {
				TakeThisStuff(%TrueClientId, fetchData(%TrueClientId, "TempPack"));
				%namelist = %TCsenderName @ ",";
				TossLootbag(%TrueClientId, fetchData(%TrueClientId, "TempPack"), 5, %namelist, 0);
				RefreshAll(%TrueClientId);
				remotePlayMode(%TrueClientId);
			}
		}
	}
	else if(%w1 == "#dropcoins") {
		%processed = True;
		%cropped = GetWord(%cropped, 0);	
		if(%cropped == "all") %cropped = fetchData(%TrueClientId, "COINS");
		else				  %cropped = floor(%cropped);

		if(fetchData(%TrueClientId, "COINS") >= %cropped || %clientToServerAdminLevel >= 4) {
			if(%cropped > 0) {
				if( !(%clientToServerAdminLevel >= 4) )
					storeData(%TrueClientId, "COINS", %cropped, "dec");

				%toss = GetTypicalTossStrength(%TrueClientId);
				TossLootbag(%TrueClientId, "COINS " @ %cropped, %toss, "*", 0);
				RefreshAll(%TrueClientId);
				Client::sendMessage(%TrueClientId, 0, "You dropped " @ %cropped @ " coins.");
				playSound(SoundMoney1, GameBase::getPosition(%TrueClientId));
			}
		} else Client::sendMessage(%TrueClientId, 0, "You don't even have that many coins!");
	}
	else if(%w1 == "#sharepack") {
		%processed = True;
		%time = getIntegerTime(true) >> 5;
		if(%time - %TrueClientId.lastSharePackTime > 5) {
			%TrueClientId.lastSharePackTime = %time;

			%c1 = GetWord(%cropped, 0);
			%c2 = GetWord(%cropped, 1);

			if(%c1 != -1 && %c2 != -1) {
				%id = NEWgetClientByName(%c1);
	
				if(%id != -1 && Client::getName(%id) != %senderName) {
					%c1 = Client::getName(%id);	//properly capitalize name
					if(floor(%c2) != 0 || %c2 == "*") {
						%flag = "";
						%cnt = 0;
						%list = fetchData(%TrueClientId, "lootbaglist");
						for(%i = String::findSubStr(%list, ","); String::findSubStr(%list, ",") != -1; %list = String::NEWgetSubStr(%list, %i+1, 99999)) {
							%cnt++;
							%bid = String::NEWgetSubStr(%list, 0, %i);
	
							if(%cnt == %c2 || %c2 == "*") {
								%flag++;
	
								%nl = GetWord($loot[%bid], 1);
								if(%nl != "*") {
									$loot[%bid] = String::Replace($loot[%bid], %nl, AddToCommaList(%nl, %c1));
									Client::sendMessage(%TrueClientId, $MsgBeige, "Adding " @ %c1 @ " to backpack #" @ %cnt @ " (" @ %bid @ ")'s share list.");
									Client::sendMessage(%id, $MsgBeige, %TCsenderName @ " is sharing his/her backpack #" @ %cnt @ " with you.");
								}
								else Client::sendMessage(%TrueClientId, 0, "Backpack #" @ %cnt @ " is already publicly available.");
							}
						}
						
						if(%flag == "")
							Client::sendMessage(%TrueClientId, 0, "Invalid backpack number.");
					} else Client::sendMessage(%TrueClientId, 0, "Please specify a backpack number (1, 2, 3, etc, or * for all)");
				} else Client::sendMessage(%TrueClientId, 0, "Invalid player name or same player name.");
			} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
		}
	}
	else if(%w1 == "#unsharepack") {
		%processed = True;
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(%id != -1 && Client::getName(%id) != %senderName) {
				%c1 = Client::getName(%id);	//properly capitalize name
				if(floor(%c2) != 0 || %c2 == "*") {
					%flag = "";
					%cnt = 0;
					%list = fetchData(%TrueClientId, "lootbaglist");
					for(%i = String::findSubStr(%list, ","); String::findSubStr(%list, ",") != -1; %list = String::NEWgetSubStr(%list, %i+1, 99999)) {
						%cnt++;
						%bid = String::NEWgetSubStr(%list, 0, %i);

						if(%cnt == %c2 || %c2 == "*") {
							%flag++;

							%nl = GetWord($loot[%bid], 1);
							if(%nl != "*") {
								$loot[%bid] = String::Replace($loot[%bid], %nl, RemoveFromCommaList(%nl, %c1));
								Client::sendMessage(%TrueClientId, $MsgBeige, "Removing " @ %c1 @ " from backpack #" @ %cnt @ " (" @ %bid @ ")'s share list.");
								Client::sendMessage(%id, $MsgBeige, %TCsenderName @ " has removed you from his/her backpack #" @ %cnt @ " share list.");
							} else Client::sendMessage(%TrueClientId, 0, "Backpack #" @ %cnt @ " is already publicly available.  Its share list can not be changed.");
						}
					}						
					if(%flag == "")
						Client::sendMessage(%TrueClientId, 0, "Invalid backpack number.");
				} else Client::sendMessage(%TrueClientId, 0, "Please specify a backpack number (1, 2, 3, etc, or * for all)");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name or same player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}
	else if(%w1 == "#packsummary") {
		%processed = True;		
		if(%clientToServerAdminLevel >= 1) {
			if(%cropped != "") {
				%id = NEWgetClientByName(%cropped);
				%cropped = Client::getName(%id);
				%cnt = floor(CountObjInCommaList(fetchData(%id, "lootbaglist")));
				Client::sendMessage(%TrueClientId, 0, %cropped @ " has " @ %cnt @ " dropped backpacks.");
			} else {
				%list = GetPlayerIdList();
				for(%i = 0; (%id = GetWord(%list, %i)) != -1; %i++) {
					%cnt = CountObjInCommaList(fetchData(%id, "lootbaglist"));
					if(%cnt > 0)
						Client::sendMessage(%TrueClientId, 0, Client::getName(%id) @ " has " @ %cnt @ " dropped backpacks.");
				}
			}
		} 
		else if(%cropped == "") {
			%cnt = floor(CountObjInCommaList(fetchData(%TrueClientId, "lootbaglist")));
			Client::sendMessage(%TrueClientId, 0, "You have a total of " @ %cnt @ " currently dropped backpacks.");
		}
	}
	
	
	return %processed;
}



//________________________________________________________________________________________________________________________________________________________________
// MINION COMMANDS
//________________________________________________________________________________________________________________________________________________________________
function processMinionControls(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai) {
	%processed = False;
	
	if(%w1 == "#attacklos") {
		%processed = True;
		if(%cropped == "")
			Client::sendMessage(%TrueClientId, 0, "Please specify a bot name.");
		else {
			%event = String::findSubStr(%cropped, ">");
			if(%event != -1) {
				%info = String::NEWgetSubStr(%cropped, 0, %event);
				%cmd = String::NEWgetSubStr(%cropped, %event, 99999);
			}
			else %info = %cropped;

			%c1 = getWord(%info, 0);
			%ox = GetWord(%info, 1);
			%oy = GetWord(%info, 2);
			%oz = GetWord(%info, 3);
			%id = NEWgetClientByName(%c1);

			if(%id != -1) {
				if(IsInCommaList(fetchData(%TrueClientId, "PersonalPetList"), %id) || %clientToServerAdminLevel >= 1) {
					if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && %id != %TrueClientId && floor(%id.adminLevel) != 0)
						Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
					else if(Player::isAiControlled(%id)) {
						%player = Client::getOwnedObject(%TrueClientId);

						if(%ox == -1 && %oy == -1 && %oz == -1) {
							GameBase::getLOSinfo(%player, 50000);
							%pos = $los::position;
						}
						else %pos = %ox @ " " @ %oy @ " " @ %oz;

						if(%event != -1)
							AddEventCommand(%id, %senderName, "onPosCloseEnough " @ %pos, %cmd);

						if(!%echoOff) 
							Client::sendMessage(%TrueClientId, 0, %c1 @ " (" @ %id @ ") is attacking position " @ %pos @ ".");
						storeData(%id, "botAttackMode", 3);
						storeData(%id, "tmpbotdata", %pos);
					} else Client::sendMessage(%TrueClientId, 0, "Player must be a bot.");
				}
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		}
	}
	else if(%w1 == "#botnormal") {
		%processed = True;
		if(%cropped == "")
			Client::sendMessage(%TrueClientId, 0, "Please specify a bot name.");
		else {
			%id = NEWgetClientByName(%cropped);

			if(%id != -1) {
				if(IsInCommaList(fetchData(%TrueClientId, "PersonalPetList"), %id) || %clientToServerAdminLevel >= 1) {
					if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && %id != %TrueClientId && floor(%id.adminLevel) != 0)
						Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
					else if(Player::isAiControlled(%id)) {
						if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Bot is now in normal attack mode.");
						storeData(%id, "botAttackMode", 1);
						AI::newDirectiveRemove(fetchData(%id, "BotInfoAiName"), 99);
						storeData(%id, "tmpbotdata", "");

						if(fetchData(%id, "petowner") != "") {
							storeData(%id, "botAttackMode", 2);
							storeData(%id, "tmpbotdata", fetchData(%id, "petowner"));
						}
					} else Client::sendMessage(%TrueClientId, 0, "Player must be a bot.");
				}
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		}
	}
	else if(%w1 == "#possess") {
		%processed = True;
		if(%cropped == "")
			Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
		else {
			%id = NEWgetClientByName(%cropped);
			rpg::PossessTarget(%TrueClientId, %id, true);
		}
	}
	else if(%w1 == "#revert") {
		%processed = True;
		if(%TrueClientId.sleepMode == "") {
			rpg::RevertPossession(%TrueClientId);
		}
	}
	else if(%w1 == "#freeze") {
		%processed = True;
		if(%cropped != -1) {
			%id = NEWgetClientByName(%cropped);
			if(%id != -1) {
				if(IsInCommaList(fetchData(%TrueClientId, "PersonalPetList"), %id) || %clientToServerAdminLevel >= 1) {
					if(Player::isAiControlled(%id)) {
						if(!%echoOff) 
							Client::sendMessage(%TrueClientId, 0, "Freezing " @ %cropped @ " (" @ %id @ ").");
						storeData(%id, "frozen", True);
						AI::setVar(fetchData(%id, "BotInfoAiName"), SpotDist, 0);
						AI::newDirectiveRemove(fetchData(%id, "BotInfoAiName"), 99);
					} else Client::sendMessage(%TrueClientId, 0, "Name must be a bot.");
				}
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
	}	
	else if(%w1 == "#cancelfreeze") {
		%processed = True;
		if(%cropped != -1) {
			%id = NEWgetClientByName(%cropped);
			if(%id != -1) {
				if(IsInCommaList(fetchData(%TrueClientId, "PersonalPetList"), %id) || %clientToServerAdminLevel >= 1) {
					if(Player::isAiControlled(%id)) {
						if(!%echoOff) Client::sendMessage(%TrueClientId, 0, %cropped @ " (" @ %id @ ") is no longer frozen.");
						storeData(%id, "frozen", "");
						AI::SetSpotDist(%id);
					} else Client::sendMessage(%TrueClientId, 0, "Player must be a bot.");
				}
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
	}
	
	return %processed;
}



//________________________________________________________________________________________________________________________________________________________________
// PLAYER COMMANDS - Used during gameplay
//________________________________________________________________________________________________________________________________________________________________
function processPlayerCommands(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai) {
	%processed = False;
	
	if(%w1 == "#list") {
		%w1 = "#help";
		%cropped = "list " @ %cropped;
	}
	
	if(%w1 == "#w" || %w1 == "#help") {
		%processed = True;
		%wantsList = GetWord(%cropped, 0);
		if(string::compare(%wantsList, "list") == 0) {
			%ofWhat = GetWord(%cropped, 1);
			%msg = "";
			if(string::compare(%ofWhat, "stats") == 0) {
				%msg = "Names of Player Stats: ";
				%ns = GetNumSkills();
				for(%i=1;%i<%ns;%i++) {
					%msg = %msg @ $SkillDesc[%i];
					if(%i<%ns-1) %msg = %msg @ ", ";
				}				
			} 
			else if(string::compare(%ofWhat, "skills") == 0) {
				%skillName = GetWord(%cropped, 2);
				%index = $SkillIndex[%skillName];
				%msg = "";
				if(%index > 0 && $SkillList[%index] != "") {
					%msg = %skillname @ ": " @ $SkillList[%index];
				} else {
					Client::sendMessage(%TrueClientId, 0, "Format: #help list skills Skill Name");
				}		
			} 
			else if(string::compare(%ofWhat, "spells") == 0) {
				%skillName = GetWord(%cropped, 2);				
				%msg = "";
				if(%skillName != -1) {
					%name2 = GetWord(%cropped, 3);
					if(%name2 != -1) %skillName = %skillName @ %name2;
					%msg = %skillname @ ": ";
					%sp = 1; %kwd = $Spell::keyword[%sp];
					if($SkillIndex[%skillName] != "") {
						while(string::compare(%kwd,"") != 0){
							%kwd = $Spell::keyword[%sp];
							if($SkillType[%kwd] == $SkillIndex[%skillName]) {
								%msg = %msg @ %kwd @ ", ";
							}
							%sp++;
						}
					}
					if(string::compare(%msg, %skillname @ ": ") == 0) {
						Client::sendMessage(%TrueClientId, 0, "Format: #help list spells Skill Name");
						%msg = "";
					}
				} else {
					Client::sendMessage(%TrueClientId, 0, "Format: #help list spells Skill Name");
				}
			} else {
				Client::sendMessage(%TrueClientId, 0, "Please specify a type (skills, spells, [skillname]).");
			}
			if(%msg != "") {
				%time = floor(String::len(%msg) / 10);
				rpg::longPrint(%TrueClientId,%msg,0,%time);
				%clientId.blockPrints=True;
				schedule(%clientId@".blockPrints=\"\";",%time,%clientId@"blockp");
			}
		} else {
			%item = getCroppedItem(%cropped);
			if(%item == "")	{ 
				Client::sendMessage(%TrueClientId, 0, "#help list | #help itemName | #help spellName");
			} else {
				
				%msg = WhatIs(%TrueClientId, %item, True);
				if(%msg == "")	// help the clueless and the lazy
					%msg = WhatIs(%TrueClientId, "#" @ %item, True);
				rpg::longPrint(%TrueClientId, %msg, 1, floor(String::len(%msg) / 20));
			}
		}
	}
	else if(%w1 == "#savecharacter") {
		%processed = True;
		if(%clientToServerAdminLevel >= 4) {
			if(%cropped == "") {
				%r = SaveCharacter(%TrueClientId);
				Client::sendMessage(%TrueClientId, 0, "Saving self (" @ %TrueClientId @ "): success = " @ %r);
			} else {
				%id = NEWgetClientByName(%cropped);
				if(%id) {
					%r = SaveCharacter(%id);
					Client::sendMessage(%TrueClientId, 0, "Saving " @ Client::getName(%id) @ " (" @ %id @ "): success = " @ %r);
				} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
			}
		} else {
			%time = getIntegerTime(true) >> 5;
			if(%time - %TrueClientId.lastSaveCharTime > 10) {
				%TrueClientId.lastSaveCharTime = %time;
				%r = SaveCharacter(%TrueClientId);
				Client::sendMessage(%TrueClientId, 0, "Saving self (" @ %TrueClientId @ "): success = " @ %r);
			}
		}
	}	
	else if(%w1 == "#loadcharacter") {
		%processed = True;
		if(%clientToServerAdminLevel >= 4) {
			if(%cropped == "")	Client::sendMessage(%TrueClientId, 0, "Please specify clientId.");
			else				LoadCharacter(%cropped);
		}
	}
	else if(%w1 == "#resetcharacter") {				
		if(%cropped != -1) {
			%name = rpg::getname(%TrueClientId);
			if(string::compare(%cropped, %name) == 0) {
				playNextAnim(%TrueClientId);
				Player::Kill(%TrueClientId);
				ResetPlayer(%TrueClientId);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, %cropped @ " (" @ %id @ ") profile was RESET.");
			} else Client::sendMessage(%TrueClientId, 0, "You must specify your character name to confirm! #resetcharacter MyName");
		} else Client::sendMessage(%TrueClientId, 0, "You must specify your character name to confirm! #resetcharacter MyName");
	}
	else if(%w1 == "#defaulttalk") {
		%processed = True;
		if(%cropped != "") {
			storeData(%TrueClientId, "defaultTalk", %cropped);
			Client::sendMessage(%TrueClientId, 0, "Changed Default Talk to " @ fetchData(%TrueClientId, "defaultTalk") @ ".");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify what will be added to the beginning of each of your messages.");	
	}
	else if(%w1 == "#set") {
		%processed = True;
		%c1 = GetWord(%cropped, 0);
		
		%validKeys = "1 2 3 4 5 6 7 8 9 0 q b g h m n c pack";
		%validKey = "";
		for(%x=0;(%key = GetWord(%validKeys,%x)) != -1; %x++) {
			if(%key != %c1) continue;
			%validKey = %key;
			break;
		}
		if(%validKey != "") {
			%rest = String::getSubStr(%cropped, (String::len(%c1)+1), String::len(%cropped)-(String::len(%c1)+1));
			if(%rest != "")	client::sendmessage(%TrueClientId, 0, "Key "@%c1@" set to "@%rest);
			else			client::sendmessage(%TrueClientId, 0, "Key "@%c1@" cleared. was: "@$numMessage[%TrueClientId, %c1]);
			$numMessage[%TrueClientId, %c1] = %rest;
		}
		else if((string::getsubstr(%c1, 0, 6) == "numpad" && string::len(%c1) == 7) || %c1 == "numpadenter") {
			%rest = String::getSubStr(%cropped, (String::len(%c1)+1), String::len(%cropped)-(String::len(%c1)+1));
			if(%rest != "")	client::sendmessage(%TrueClientId, 0, "Key "@%c1@" set to "@%rest);
			else			client::sendmessage(%TrueClientId, 0, "Key "@%c1@" cleared. was: "@$numMessage[%TrueClientId, %c1]);
			$numMessage[%TrueClientId, %c1] = %rest;
		}
		else if(%TrueClientId.repack >= 4)	client::sendmessage(%TrueClientid, 0, "#set [1-0/b/g/h/m/c/numpad0-9] [a message]");
		else								client::sendmessage(%TrueClientid, 0, "#set [1-9/b/g/h/m/c] [a message]");
	}
	else if(%w1 == "#getinfo") {
		%processed = True;
		%cropped = GetWord(%cropped, 0);
		if(%cropped == "") Client::sendMessage(%TrueClientId, 0, "Please specify a name.");
		else {
			%id = NEWgetClientByName(%cropped);
			if(%id != -1)	DisplayGetInfo(%TrueClientId, %id, Client::getOwnedObject(%id));
			else			Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		}
	}
	else if(%w1 == "#setinfo") {
		%processed = True;
		if(%cropped == "") Client::sendMessage(%TrueClientId, 0, "Please specify text.");
		else {
			storeData(%TrueClientId, "PlayerInfo", %cropped);
			Client::sendMessage(%TrueClientId, 0, "Info set.  Use #getinfo [name] to retrieve this type of information.");
		}
	}		
	else if(%w1 == "#mypassword") {
		%processed = True;
		%c1 = GetWord(%cropped, 0);	
		if(%c1 != -1) {
			if(String::findSubStr(%c1, "\"") != -1) 		Client::sendMessage(%TrueClientId, 0, "Invalid password.");				
			else if(string::getSubStr(%c1, 0, 64) != %c1)	Client::sendMessage(%TrueClientId, 0, "Max password length is 64 characters.");
			else {
				storeData(%TrueClientId, "password", %c1);
				Client::sendMessage(%TrueClientId, 0, "Changed personal password to " @ fetchData(%TrueClientId, "password") @ ".");
			}
		}
		else Client::sendMessage(%TrueClientId, 0, "Please specify a one-word password.");	
	}
	else if(%w1 == "#admin") {
		%processed = True;
		%name = rpg::getname(%TrueClientId);
		%level = floor(%cropped);

		for(%i = 1; $AdminPassword[%i] != ""; %i++) {
			if(%cropped == $AdminPassword[%i]) {
				%TrueClientId.adminLevel = %i;
				if(%TrueClientId.adminLevel >= 5)
					ChangeRace(%TrueClientId, "DeathKnight");
				Game::refreshClientScore(%TrueClientId);
				if(!%echoOff) 
					Client::sendMessage(%TrueClientId, 0, "Password accepted for Admin Clearance Level " @ %TrueClientId.adminLevel @ ".");
				break;
			}
		}
	}
	else if(%w1 == "#use") {
		%processed = True;
		if(String::findSubStr(%cropped, "\"") == -1 && String::findSubStr(%cropped, "\\") == -1) {
			%belt = False;
			%item = $beltitem[%cropped, "Item"];
			if($beltitem[%item, "Name"] == ""){
				%itemString = Belt::HasItemNamed(%TrueClientId, %cropped);
				if(%itemString == False){
					for(%i = 0; $beltItemData[%i] != ""; %i++)
					{
						if(string::icompare($beltitem[$beltItemData[%i], "Name"], String::replace(%cropped, "armor", "armour")) == 0){
							%item = $beltItemData[%i];
							%belt = True;
							break;
						}
					}
				} else if(getWord(%itemString,1) > 0) {
					%item = getWord(%itemString,0);
					%belt = True;
				}
			} else %belt = True;

			if(%belt) {
				if(HasThisStuff(%TrueClientId, %item@" 1")) {
					%TrueClientId.bulkNum = 1;
					%type = $beltItem[%item, "Type"];
					if(%type == "PotionItems")	processMenuBeltDrop(%TrueClientId, "PotionItems use "@%item@" 1", 1);
					else 						Client::SendMessage(%TrueClientId,0,"You cannot use a "@$beltItem[%item, "Name"]);
				} else Client::SendMessage(%TrueClientId,0,"You do not have any "@$beltItem[%item, "Name"]);
			} else Client::SendMessage(%TrueClientId,0,"There doesn't seem to be an item by that name.");
		}
	}
	else if(%w1 == "#deathmsg") {
		%processed = True;
		storeData(%TrueClientId, "deathmsg", %cropped);
		Client::sendMessage(%TrueClientId, 0, "Changed your death message to: " @ fetchData(%TrueClientId, "deathmsg"));
	}
	else if(%w1 == "#echo") {
		%processed = True;
		if(String::ICompare(%cropped, "off") == 0)		%TrueClientId.echoOff = True;
		else if(String::ICompare(%cropped, "on") == 0)	%TrueClientId.echoOff = "";
		else											Client::sendMessage(%TrueClientId, $MsgWhite, %cropped);
	}
	else if(%w1 == "#examine") {
		%processed = True;
		%player = Client::getOwnedObject(%TrueClientId);
		$los::object = "";
		$los::position = "";
		%objData = "Failed to examine! (nothing to examine)";
		if(GameBase::getLOSinfo(%player, 500))			%examine = True;
		else if(GameBase::getLOSinfo(%player, 1000))	%examine = True;
		else if(GameBase::getLOSinfo(%player, 5000))	%examine = True;

		%msg = "<jc>LOS: "@$los::position@"\n";
		%eomID = nametoid("MissionGroup\\EndOfMap");
		if(%examine) {
			%target = $los::object;
			%obj = getObjectType(%target);
			%type = GameBase::getDataName(%target);
			if(%type == "") {
				if(%obj != "") {
					%objData = %obj;
					%msg = %msg @ "Obj Type: "@%obj@"\n";
				} else %objData = "Failed to examine! (returned blank)";
			} else if(%type == False) {
				if(%obj != "") {
					if(%obj == "InteriorShape"){
						%objData = %obj@" "@%target.fileName@" "@%target.tag;
						%msg = %msg @ "Obj Type: "@%obj@"\n";
						if(%target.tag != "")
							%msg = %msg @ "Obj Tag: "@%target.tag@"\n";
					} else {
						%objData = %obj;
						%msg = %msg @ "Obj Type: "@%obj@"\n";
					}
				}
				else %objData = "Failed to examine! (returned false/blank)";
				%msg = %msg @ "Obj ID: "@%target@"\n";
			} else {
				%objData = %obj @" "@ %type;
				%msg = %msg @ "Obj Type: "@%obj@"\n";
				%msg = %msg @ "Obj Shape: "@%type@"\n";
				%msg = %msg @ "Obj ID: "@%target@"\n";
			}
			pecho("examine %target="@%target@" %type="@%type@" %obj="@%obj);
		}
		
		%objPos = gamebase::getposition(%target);
		if(%objPos != "0 0 0"){
			%objData = %objData @" @ "@%objPos;
			%msg = %msg @ "Obj pos: "@%objPos@"\n";
		}
		
		if(%examine) {
			if(%eomID < %target){
				if(%type == "TreeShape" && %target.hp > 1)	%msg = %msg @ "<f2>This is a tree that can be cut.";
				else if(%obj != "Player") 					%msg = %msg @ "<f2>This is a dynamically loaded object.";
			}
			else %msg = %msg @ "<f1>This object is part of the map.";
		}
		%time = floor(String::len(%msg) / 20);
		rpg::longPrint(%TrueClientId,%msg,0,%time);
		%clientId.blockPrints=True;
		schedule(%clientId@".blockPrints=\"\";",%time,%clientId@"blockp");
	}
	else if(%w1 == "#sleep") {
		%processed = True;
		if(fetchData(%TrueClientId, "InSleepZone") != "" && %TrueClientId.sleepMode == "" && !IsDead(%TrueClientId))
			%flag = True;
		if(String::ICompare(fetchData(%TrueClientId, "CLASS"), "Ranger") == 0 && fetchData(%TrueClientId, "zone") == "" && %TrueClientId.sleepMode == "" && !IsDead(%TrueClientId))
			%flag = True;
		
		if(%flag) {
			%TrueClientId.sleepMode = 1;				
			Client::setControlObject(%TrueClientId, Client::getObserverCamera(%TrueClientId));
			Observer::setOrbitObject(%TrueClientId, Client::getOwnedObject(%TrueClientId), 8, 8, 8);
			refreshHPREGEN(%TrueClientId);
			refreshMANAREGEN(%TrueClientId);
			Client::sendMessage(%TrueClientId, $MsgWhite, "You fall asleep...  Use #wake to wake up.");
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't seem to fall asleep here.");	
	}
	else if(%w1 == "#meditate") {
		%processed = True;
		if(%TrueClientId.sleepMode == "" && !IsDead(%TrueClientId) && $possessedBy[%TrueClientId].possessId != %TrueClientId) {
			%TrueClientId.sleepMode = 2;			
			Client::setControlObject(%TrueClientId, Client::getObserverCamera(%TrueClientId));
			Observer::setOrbitObject(%TrueClientId, Client::getOwnedObject(%TrueClientId), 15, 15, 15);
			refreshHPREGEN(%TrueClientId);
			refreshMANAREGEN(%TrueClientId);
			Player::setAnimation(%TrueClientId, 48); // kneel
			centerprint(%TrueClientId, "You begin to meditate. Press T or use #wake to stand up.", 5);			
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't seem to meditate.");	
	}
	else if(%w1 == "#wake") {
		%processed = True;
		if(%TrueClientId.sleepMode != "") {
			%TrueClientId.sleepMode = "";
			Client::setControlObject(%TrueClientId, %TrueClientId);
			refreshHPREGEN(%TrueClientId);
			refreshMANAREGEN(%TrueClientId);
			centerprint(%TrueClientId, "You stand and awaken.", 5);
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You are not sleeping or meditating.");	
	}
	else if(%w1 == "#spell" || %w1 == "#cast" || %w1 == "#skill") {
		%processed = True;
		if(fetchData(%TrueClientId, "SpellCastStep") == 1)
			Client::sendMessage(%TrueClientId, 0, "You are already casting a spell!");
		else if(fetchData(%TrueClientId, "SpellCastStep") == 2)
			Client::sendMessage(%TrueClientId, 0, "You are still recovering from your last spell cast.");
		else if(%TrueClientId.sleepMode != "" && %TrueClientId.sleepMode != False)
			Client::sendMessage(%TrueClientId, $MsgRed, "You can not cast a spell while sleeping or meditating.");
		else if(IsDead(%TrueClientId))
			Client::sendMessage(%TrueClientId, $MsgRed, "You can not cast a spell when dead.");
		else {
			if(%cropped == "")
				Client::sendMessage(%TrueClientId, 0, "Specify a spell.");
			else {
				BeginCastSpell(%TrueClientId, escapestring(%cropped));
				if (String::findSubStr(%cropped, "\"") != -1){
					%ip = Client::getTransportAddress(%ClientId);
					echo("Exploit attempt detected and blocked: " @ %trueClientId @ ", aka " @ %TCsenderName @ ", at " @ %ip @ ".");
					echo("Exploit: " @ %message);
					messageall(0,"Exploit attempt detected and blocked: " @ %trueClientId @ ", aka " @ %TCsenderName @ ", at " @ %ip @ ".");
					schedule("delayedban(" @ %TrueClientId @ ");",1.0);
				}
			}
		}
	}
	else if(%w1 == "#brew") {
		if(SkillCanUse(%TrueClientId, "#brew") && HasThisStuff(%TrueClientId, "BookOfConcoctions 1") && rpg::IsMemberOfHouse(%TrueClientId,"Order Of Qod")) {
			%item = GetWord(%cropped, 0);
			if(%item == -1) {
				Client::sendMessage(%TrueClientId, $MsgBeige, "#brew [NameOfPotion] [Quantity]");
			} else {
				%smithnum = $smithVar[%item];
				if ($SmithAssociatedSkillType[%smithnum] != $SkillRestorationMagic) {
					Client::sendMessage(%TrueClientId, $MsgBeige, "You can't #brew entire items! Learn Survival skills to #smith instead.");
				} else {					
					%amt = floor(GetWord(%cropped, 1));
					
					if(%amt < 1) 	%amt = 1;
					if(%amt > 100)	%amt = 100;
					if(%smithnum > 0) {
						%sc = $SmithCombo[%smithnum];
						if(HasThisStuff(%TrueClientId, %sc, %amt) && !IsDead(%TrueClientId)) {
							for(%i = 0; (%w = GetWord(%sc, %i)) != -1; %i+=2) {
								%w2 = GetWord(%sc, %i+1) * %amt;
								takethisstuff(%TrueClientId, %w @ " "@%w2);
							}
							%m = multiplyItemString($SmithComboResult[%smithnum], %amt);
							givethisstuff(%trueClientId, %m);
							if(isBeltItem(%item))	%itemname = $beltitem[%item, "Name"];
							else					%itemname = %item.description;
							Client::sendMessage(%TrueClientId, $MsgGreen, "You brewed "@%m@".");
							PlaySound(SoundPotionSwish, GameBase::getPosition(%TrueClientId));
							for(%i=1; %i <= %amt; %i++)
								UseSkill(%TrueClientId, $SkillSurvival, False, True);
						} else Client::sendMessage(%TrueClientId, $MsgRed, "You do not have the items needed to #brew a " @ %item @ ". You need " @ %sc @ ".");						
					} else Client::sendMessage(%TrueClientId, $MsgRed, %item @ " is not a known concoction.");
				}
			}
		} else Client::sendMessage(%TrueClientId, $MsgRed, "Only the most devoted Ordertakers of the Order of Qod know how to #brew potions.");
	}
	else if(%w1 == "#outpost" && !rpg::IsTheWorldEnding()) {
		if(SkillCanUse(%TrueClientId, "#outpost") && rpg::IsMemberOfHouse(%TrueClientId,"Luminous Dawn") && rpg::GetHouseLevel(%TrueClientId) >= 100) {
			%name 		= rpg::getName(%TrueClientId);
			%groupName 	= "Outpost" @ %name;
			%basePath 	= "MissionCleanup\\" @ %groupName;
			
			%hasOutpost	= nameToId(%basePath);
			
			if(%cropped == "destroy" || %cropped == "delete" || %cropped == "demolish") {
				if(%hasOutpost < 0)
					Client::sendMessage(%TrueClientId, $MsgRed, "You don't have an #outpost to demolish.");
				else if(20 > Vector::getDistance(GameBase::getPosition(%TrueClientId), GameBase::getPosition(nameToId(%basePath @ "\\building"))))
					Client::sendMessage(%TrueClientId, $MsgRed, "You are too close to your #outpost to demolish it. Leave first.");
				else
					rpg::LuminousDawnSetupOutpost(%name, %TrueClientId, "destroy");
				return %processed;
			}
			else if(%cropped == "exit" || %cropped == "leave" || %cropped == "top") {
				if(%hasOutpost > 0 && 20 > Vector::getDistance(GameBase::getPosition(%TrueClientId), GameBase::getPosition(nameToId(%basePath @ "\\building"))))
					GameBase::setPosition(%TrueClientId, Vector::add("2 2 8",GameBase::getPosition(%basePath @ "\\sleepzone")));
				else Client::sendMessage(%TrueClientId, $MsgRed, "You can't leave your #outpost unless you are in it...");				
				return %processed;
			}
			else if(%cropped == "enter" || %cropped == "inside" || %cropped == "travel") {
				if(%hasOutpost <= 0) {
					Client::sendMessage(%TrueClientId, $MsgRed, "You don't have an #outpost to go to.");
				}
				if(20 > Vector::getDistance(GameBase::getPosition(%TrueClientId), GameBase::getPosition(nameToId(%basePath @ "\\building"))))
					GameBase::setPosition(%TrueClientId, Vector::add("2 2 -3",GameBase::getPosition(%basePath @ "\\sleepzone")));
				else {
					internalSay(%TrueClientId, 0, "#fasttravel outpost", "nofloodprotect");
				}
				return %processed;
			}
			else if(%cropped == "build" || %cropped == "construct" || %cropped == "create" || %cropped == "make") {
				if (VecZ(GameBase::getPosition(%TrueClientId)) > 1000) {
					Client::sendMessage(%TrueClientId, $MsgRed, "You are too high up to start a new #outpost.");
					return %processed;
				}
				else if(%hasOutpost > 0) {
					if(20 > Vector::getDistance(GameBase::getPosition(%TrueClientId), GameBase::getPosition(nameToId(%basePath @ "\\building")))) {
						Client::sendMessage(%TrueClientId, $MsgRed, "You are too close to your #outpost to demolish it. Leave first.");
						return %processed;
					}
					rpg::LuminousDawnSetupOutpost(%name, %TrueClientId, "destroy");
				}				
				
				%pos = GameBase::getPosition(%TrueClientId);
				%group = newObject(%groupName, SimGroup);
				addToSet("MissionCleanup", %group);				
				rpg::LuminousDawnSetupOutpost(%name, %TrueClientId, 1, %pos);
				schedule("rpg::LuminousDawnSetupOutpost(\"" @ %name @ "\"," @ %TrueClientId @ ", 2, \"" @ %pos @ "\");", 300, %group);				
			} else {
				Client::sendMessage(%TrueClientId, $MsgRed, "#outpost [build | demolish | enter | leave]");
			}
		} else Client::sendMessage(%TrueClientId, $MsgRed, "Only the highest ranking Luminous Dawn may build an #outpost.");
	}
	else if(%w1 == "#fasttravel" && !rpg::IsTheWorldEnding()) {
		%location = %cropped;
		if(%TrueClientId.lastDamage + 10 > getSimTime()) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #fasttravel while under attack!");
		} else if(fetchData(%TrueClientId, "NextFastTravel") > getSimTime()){
			Client::sendMessage(%TrueClientId, $MsgRed, "You are still shaking off #fasttravel sickness. Wait another " @ floor(fetchData(%TrueClientId, "NextFastTravel") - getSimTime()) @ " seconds.");
		} else if(SkillCanUse(%TrueClientId, "#fasttravel") && rpg::IsMemberOfHouse(%TrueClientId,"Luminous Dawn")) {
			%name = rpg::getName(%TrueClientId);
			if(%location != "") {
				if(%location == "outpost" && rpg::GetHouseLevel(%TrueClientId) >= 100) {			// hop inside the outpost this way
					%groupPath 	= "MissionCleanup\\Outpost" @ %name;
					%linkCompleted = nameToId(%groupPath @ "\\sleepzone");
				} else {																			// prefer our own waylink, if we have one
					%groupPath 	= "MissionCleanup\\Waylink\\" @ %name @ %location;
					%linkCompleted = nameToId(%groupPath @ "\\elecbeam");
				}
				if(%linkCompleted < 0) {															// look for another player's waylink
					%cnt = Group::objectCount("MissionCleanup\\Waylink");
					for(%i = 0; %i <= %cnt - 1; %i++) {
						%object = Group::getObject(%group, %i);
						%name 	= rpg::ZoneNameFromObject(%object);
						if (%name == %location) {
							%linkCompleted = %object;
							break;
						}
					}				
				}
				
				if(%linkCompleted > 0) {
					%linkpos 	= GameBase::getPosition(%linkCompleted);
					//%linkpos	= Vector::add(%linkpos, "1 -3 0");
					%clientpos 	= GameBase::getPosition(%TrueClientId);
					storeData(%TrueClientId, "NextFastTravel", getSimTime() + 30);
					rpg::PerformFastTravel(%TrueClientId, %clientpos, %location, %linkpos, 1);					
				} else {
					if(%location == "outpost")
						Client::sendMessage(%TrueClientId, $MsgRed, "You do not have an #outpost to #fasttravel to.");
					else
						Client::sendMessage(%TrueClientId, $MsgRed, "There is no #fasttravel link for '" @ %location @ "'.");
				}
			} else Client::sendMessage(%TrueClientId, $MsgRed, "Usage: #fasttravel [NameOfLocation]            WARNING: Use outside.");
		} else Client::sendMessage(%TrueClientId, $MsgRed, "Only the Luminous Dawn may use the #fasttravel network.");
	}
	else if(%w1 == "#waylink" && !rpg::IsTheWorldEnding()) {
		if(!IsDead(%TrueClientId) && SkillCanUse(%TrueClientId, "#waylink") && rpg::IsMemberOfHouse(%TrueClientId,"Luminous Dawn")) {
			%location = rpg::ZoneNameFromObject(fetchData(%TrueClientId,"zone"));
			if(%location == "") {
				Client::sendMessage(%TrueClientId, $MsgRed, "You cannot place a #waylink in unknown territory. Find a town or dungeon to mark instead.");
			}
			else if(GetWord(Object::getName(fetchData(%TrueClientId,"zone")),0) == "WATER") {
				Client::sendMessage(%TrueClientId, $MsgRed, "A #waylink will not function under water.");
			}
			else if(Belt::HasThisStuff(%TrueClientId,"WayLink") > 0) {
				Belt::TakeThisStuff(%TrueClientId,"WayLink", 1);
				
				%name		= rpg::getName(%TrueClientId);
				%groupPath 	= "MissionCleanup\\Waylink\\" @ %name @ %location;
				%hasLink 	= nameToId(%groupPath @ "\\stand");
				if(%hasLink > 0) {
					%hasLink = -1;
					rpg::LuminousDawnDeployLink(%name,%TrueClientId,%location,"destroy");
				}
				if(!%hasLink <= 0) {
					%group = newObject(%name @ %location, SimGroup);
					addToSet("MissionCleanup\\Waylink", %group);
					%pos = GameBase::getPosition(%TrueClientId);
					rpg::LuminousDawnDeployLink(%name, %TrueClientId, %location, 1, %pos);
					schedule("rpg::LuminousDawnDeployLink(\"" @ %name @ "\", " @ %TrueClientId @ ", " @ %location @ ", 2, \"" @ %pos @ "\");", 15, %group);
					schedule("rpg::LuminousDawnDeployLink(\"" @ %name @ "\", " @ %TrueClientId @ ", " @ %location @ ", 3, \"" @ %pos @ "\");", 30, %group);
					schedule("rpg::LuminousDawnDeployLink(\"" @ %name @ "\", " @ %TrueClientId @ ", " @ %location @ ", 4, \"" @ %pos @ "\");", 45, %group);					
					schedule("rpg::LuminousDawnDeployLink(\"" @ %name @ "\", " @ %TrueClientId @ ", " @ %location @ ", 5, \"" @ %pos @ "\");", 60, %group);
				}
			} else Client::sendMessage(%TrueClientId, $MsgRed, "You don't have a #waylink to place. Check back with the Luminous Dawn");
		} else Client::sendMessage(%TrueClientId, $MsgRed, "Only members of the Luminous Dawn may syncronize a #waylink");
	}
	
	return %processed;
}
