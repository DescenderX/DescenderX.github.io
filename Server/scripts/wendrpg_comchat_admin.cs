//________________________________________________________________________________________________________________________________________________________________
// DescX Notes:
//		Comchat.cs hurt my face =X. So I fixed it and split it up.
//		There are a lot of general commands related to, uhh, messing with players that I removed.
//
//		Jailing/ganking/#takeTheseCoinsAndLaughAtThePlayer , etc -- all removed.
//		Otherwise, all of the old TRPG commands were copied over. Most were simply reformatted.
//		The only fundamental change to every function is the lack of a return. I correctly use
//		"else if", unlike the original code.... so it's not necessary for force a return in every
//		single command. Since this was guaranteed to wreck diffs, I said "screw it", and just
//		relinted the whole thing...
//________________________________________________________________________________________________________________________________________________________________
// ADMIN LEVEL 1
//________________________________________________________________________________________________________________________________________________________________
function processAdminLevel1(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai, %clientToServerAdminLevel) {
	%processed = True;
	
	if(%clientToServerAdminLevel < 1) {
		return False;
	}
	
	if(%w1 == "#createbotgroup") {			
		if(%cropped == "")
			Client::sendMessage(%TrueClientId, 0, "Please specify a one-word BotGroup name.");
		else {
			if(GetWord(%cropped, 1) == -1) {
				%g = GetWord(%cropped, 0);
				%n = AI::CountBotGroupMembers(%g);
				if(!AI::BotGroupExists(%g)) {
					if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Created BotGroup '" @ %g @ "'.");
					AI::CreateBotGroup(%g);
				} else Client::sendMessage(%TrueClientId, 0, "BotGroup already exists and contains " @ %n @ " members.  Use #discardbotgroup to delete a BotGroup.");
			} else Client::sendMessage(%TrueClientId, 0, "Please specify a ONE-WORD BotGroup name.");
		}
	}			
	else if(%w1 == "#discardbotgroup") {
		if(%cropped == "")
			Client::sendMessage(%TrueClientId, 0, "Please specify a one-word BotGroup name.");
		else {
			if(GetWord(%cropped, 1) == -1) {
				%g = GetWord(%cropped, 0);
				if(AI::BotGroupExists(%g)) {
					if(!%echoOff) 
						Client::sendMessage(%TrueClientId, 0, "Discarded BotGroup '" @ %g @ "'.");
					AI::DiscardBotGroup(%g);
				} else Client::sendMessage(%TrueClientId, 0, "BotGroup does not exist.");
			} else Client::sendMessage(%TrueClientId, 0, "Please specify a ONE-WORD BotGroup name.");
		}
	}			
	else if(%w1 == "#getbotgroupleader") {
		if(%cropped == "")
			Client::sendMessage(%TrueClientId, 0, "Please specify a one-word BotGroup name.");
		else {
			if(GetWord(%cropped, 1) == -1) {
				%g = GetWord(%cropped, 0);
				if(AI::BotGroupExists(%g)) {
					%tl = GetWord($tmpBotGroup[%g], 0);
					%tln = Client::getName(%tl);
					Client::sendMessage(%TrueClientId, 0, "BotGroup leader is " @ %tln @ " (" @ %tl @ ").");
				} else Client::sendMessage(%TrueClientId, 0, "BotGroup does not exist.");
			} else Client::sendMessage(%TrueClientId, 0, "Please specify a ONE-WORD BotGroup name.");
		}
	}			
	else if(%w1 == "#botgroup") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);
			if(%id != -1) {
				if(Player::isAiControlled(%id)) {
					if(AI::BotGroupExists(%c2)) {
						%b = AI::IsInWhichBotGroup(%id);
						if(%b == -1) {
							if(!%echoOff) 
								Client::sendMessage(%TrueClientId, 0, "Adding minion " @ %c1 @ " (" @ %id @ ") to BotGroup '" @ %c2 @ "'.");
							AI::AddBotToBotGroup(%id, %c2);
						} else Client::sendMessage(%TrueClientId, 0, "This bot already belongs to the BotGroup '" @ %b @ "'.  Use #rbotgroup to remove a bot from a BotGroup.");
					} else Client::sendMessage(%TrueClientId, 0, "BotGroup '" @ %c2 @ "' does not exist.  Use #createbotgroup to create a BotGroup.");
				} else Client::sendMessage(%TrueClientId, 0, "Name must be a bot.");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}			
	else if(%w1 == "#rbotgroup") {
		%c1 = GetWord(%cropped, 0);

		if(%c1 != -1) {
			%id = NEWgetClientByName(%c1);
			if(%id != -1) {
				if(Player::isAiControlled(%id)) {
					%b = AI::IsInWhichBotGroup(%id);
					if(%b != -1) {
						if(!%echoOff) 
							Client::sendMessage(%TrueClientId, 0, "Removing minion " @ %c1 @ " (" @ %id @ ") from BotGroup '" @ %b @ "'.");
						AI::RemoveBotFromBotGroup(%id, %b);
					} else Client::sendMessage(%TrueClientId, 0, "This bot does not belong to a BotGroup.");
				} else Client::sendMessage(%TrueClientId, 0, "Name must be a bot.");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}			
	else if(%w1 == "#listbotgroups") {
		Client::sendMessage(%TrueClientId, 0, $BotGroups);				
	}			
	else if(%w1 == "#getadmin") {
		if(%cropped == "")
		Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
		else {
			%id = NEWgetClientByName(%cropped);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				%a = floor(%id.adminLevel);
				Client::sendMessage(%TrueClientId, 0, %cropped @ "'s Admin Clearance Level: " @ %a);
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		}				
	}			
	else if(%w1 == "#setadmin") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				%a = floor(%c2);
				if(%a < 0)							%a = 0;
				if(%a > %clientToServerAdminLevel)	%a = %clientToServerAdminLevel;

				%id.adminLevel = %a;
				Game::refreshClientScore(%id);		//so the ping and PL are shown properly

				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Changed " @ %c1 @ " (" @ %id @ ") Admin Clearance Level to " @ %id.adminLevel @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}			
	else if(%w1 == "#follow") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id1 = NEWgetClientByName(%c1);
			%id2 = NEWgetClientByName(%c2);
			if(%id1 != -1 && %id2 != -1) {
				if(Player::isAiControlled(%id1)) {
					if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Making " @ %c1 @ " (" @ %id1 @ ") follow " @ %c2 @ " (" @ %id2 @ ").");

					%event = String::findSubStr(%cropped, ">");
					if(%event != -1) {
						%cmd = String::NEWgetSubStr(%cropped, %event, 99999);
						AddEventCommand(%id1, %senderName, "onIdCloseEnough " @ %id2, %cmd);
					}

					storeData(%id1, "tmpbotdata", %id2);
					storeData(%id1, "botAttackMode", 2);
				} else Client::sendMessage(%TrueClientId, 0, "First name must be a bot.");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name(s).");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");				
	}			
	else if(%w1 == "#cancelfollow") {
		if(%cropped != -1) {
			%id = NEWgetClientByName(%cropped);
			if(%id != -1) {
				if(Player::isAiControlled(%id)) {
					AI::newDirectiveRemove(fetchData(%id, "BotInfoAiName"), 99);
					if(!%echoOff) Client::sendMessage(%TrueClientId, 0, %cropped @ " (" @ %id @ ") has stopped following its target.");
				} else Client::sendMessage(%TrueClientId, 0, "Player must be a bot.");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
	}				  
	else if(%w1 == "#getitemcount") {
		%id = NEWgetClientByName(GetWord(%cropped, 0));
		if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
		else if(%id != -1) {
			%c = Player::getItemCount(%id, GetWord(%cropped, 1));
			Client::sendMessage(%TrueClientId, 0, "Item count for (" @ %id @ ") " @ GetWord(%cropped, 1) @ " is " @ %c);
		} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
	}
	else if(%w1 == "#arenacutshort") {
		$IsABotMatch = True;
		$ArenaBotMatchTicker = $ArenaBotMatchLengthInTicks;
	}
	else if(%w1 == "#getbank") {
		if(%cropped != -1) {
			%id = NEWgetClientByName(%cropped);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1)
				Client::sendMessage(%TrueClientId, 0, %cropped @ " (" @ %id @ ") BANK is " @ fetchData(%id, "BANK") @ ".");
			else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
	}
	else if(%w1 == "#getteam") {
		if(%cropped != -1) {
			%id = NEWgetClientByName(%cropped);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1)
				Client::sendMessage(%TrueClientId, 0, %cropped @ " (" @ %id @ ") team is " @ GameBase::getTeam(%id) @ ".");
			else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
	}			
	else if(%w1 == "#getclientid") {
		if(%cropped != -1) {
			%id = NEWgetClientByName(%cropped);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1)
				Client::sendMessage(%TrueClientId, 0, %cropped @ " clientId is " @ %id @ ".");
			else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
	}
	else if(%w1 == "#getplayerid") {
		if(%cropped != -1) {
			%id = NEWgetClientByName(%cropped);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1)
				Client::sendMessage(%TrueClientId, 0, %cropped @ " clientId is " @ Client::getOwnedObject(%id) @ ".");
			else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
	}
	else if(%w1 == "#getstorage") {
		if(%cropped == "") Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
		else {
			%id = NEWgetClientByName(%cropped);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1)
				Client::sendMessage(%TrueClientId, 0, %id @ ": " @ fetchData(%id, "BankStorage"));					
			else 
				Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		}
	}	
	else if(%w1 == "#getexp") {
		if(%cropped != -1) {
			%cl = NEWgetClientByName(%cropped);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1)
				Client::sendMessage(%TrueClientId, 0, %cropped @ " (" @ %cl @ ") EXP is " @ fetchData(%cl, "EXP") @ ".");
			else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");			
	}	
	else if(%w1 == "#listdis") {
		Client::sendMessage(%TrueClientId, $MsgBeige, $DISlist);				
	}
	else if(%w1 == "#listpacks") {
		Client::sendMessage(%TrueClientId, $MsgBeige, $SpawnPackList);
	}
	
	else {
		%processed = False;
	}
	
	return %processed;
}



//________________________________________________________________________________________________________________________________________________________________
// ADMIN LEVEL 2
//________________________________________________________________________________________________________________________________________________________________
function processAdminLevel2(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai, %clientToServerAdminLevel) {
	%processed = True;
	
	if(%clientToServerAdminLevel < 2) {
		return False;
	}

	if(%w1 == "#kick") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);
		if(%c2 == -1)
			 %c2 = False;
		else {
			%start = String::len(%c1)+1;
			%c2 = String::getSubStr(%cropped, %start, String::len(%cropped)-%start);
		}

		if(%c1 != -1) {
			%id = NEWgetClientByName(%c1);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1)	Admin::Kick(%TrueClientId, %id, %c2);
			else				Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");		
	}			
	else if(%w1 == "#kickid") {
		%id = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);
		if(%c2 == -1) 
			%c2 = False;	
		if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
		else if(%id != -1) 	Admin::Kick(%TrueClientId, %id, %c2);
		else				Client::sendMessage(%TrueClientId, 0, "Please specify clientId & data.");
	}
	else if(%w1 == "#fell") {	          
		if(%cropped == "") Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
		else {
			%id = NEWgetClientByName(%cropped);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				if(!%echoOff) 
					Client::sendMessage(%TrueClientId, 0, "Processing fell-off-map for " @ %cropped @ " (" @ %id @ ")");
				FellOffMap(%id);
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		}	          
	}			
	else if(%w1 == "#item") {
		%name = GetWord(%cropped, 0);
		%id = NEWgetClientByName(%name);
		
		if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
		else if(%id != -1) {
			%item = GetWord(%cropped, 1);
			%amt = GetWord(%cropped, 2);
			if(!IsDead(%id)) {
				if(isBeltItem(%item))	Belt::GiveThisStuff(%id, %item, %amt);
				else					Player::setItemCount(%id, %item, %amt);
				RefreshAll(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Set " @ %name @ " (" @ %id @ ") " @ GetWord(%cropped, 1) @ " count to " @ GetWord(%cropped, 2));
			}
		} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
	}
	else if(%w1 == "#myitem") {
		%item = GetWord(%cropped, 0);
		%amt = GetWord(%cropped, 1);
		if(!IsDead(%clientId)){
			if(isBeltItem(%item))	Belt::GiveThisStuff(%TrueClientId, %item, %amt);
			else					Player::setItemCount(%TrueClientId, %item, %amt);
			RefreshAll(%TrueClientId);
			if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Set " @ %TCsenderName @ " (" @ %TrueClientId @ ") " @ GetWord(%cropped, 0) @ " count to " @ GetWord(%cropped, 1));
		}
	}
	else if(%w1 == "#teleport") {				
		if(%cropped == "") Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
		else {
			%id = NEWgetClientByName(%cropped);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				%player = Client::getOwnedObject(%TrueClientId);
				GameBase::getLOSinfo(%player, 50000);
				GameBase::setPosition(%id, $los::position);

				CheckAndBootFromArena(%id);

				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Teleporting " @ %cropped @ " (" @ %id @ ") to " @ $los::position @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		}
	}
	else if(%w1 == "#teleport2") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id1 = NEWgetClientByName(%c1);
			%id2 = NEWgetClientByName(%c2);

			if(floor(%id1.adminLevel) >= floor(%clientToServerAdminLevel) && %id1 != %TrueClientId)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id1 != -1 && %id2 != -1) {
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Teleporting " @ %c1 @ " (" @ %id1 @ ") to " @ %c2 @ " (" @ %id2 @ ").");
				GameBase::setPosition(%id1, GameBase::getPosition(%id2));
				CheckAndBootFromArena(%id1);
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name(s).");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");				
	}
	else if(%w1 == "#kill") {
		if(%cropped != -1) {
			%id = NEWgetClientByName(%cropped);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				playNextAnim(%id);
				Player::Kill(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, %cropped @ " (" @ %id @ ") was executed.");
			} 
			else if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");					
	}			
	else if(%w1 == "#sethp") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);
		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				%max = fetchData(%id, "MaxHP");
				if(%c2 < 1)			%c2 = 1;
				else if(%c2 > %max)	%c2 = %max;

				setHP(%id, %c2);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Setting " @ %c1 @ " (" @ %id @ ") HP to " @ fetchData(%id, "HP") @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");			
	}			
	else if(%w1 == "#setmana") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				%max = fetchData(%id, "MaxMANA");
				if(%c2 < 0)			%c2 = 0;
				else if(%c2 > %max)	%c2 = %max;
				setMANA(%id, %c2);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Setting " @ %c1 @ " (" @ %id @ ") MANA to " @ fetchData(%id, "MANA") @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");				
	}
	else if(%w1 == "#addcoins") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				storeData(%id, "COINS", %c2, "inc");
				RefreshAll(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Setting " @ %c1 @ " (" @ %id @ ") COINS to " @ fetchData(%id, "COINS") @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}			
	else if(%w1 == "#addbank") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				storeData(%id, "BANK", %c2, "inc");
				RefreshAll(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Setting " @ %c1 @ " (" @ %id @ ") BANK to " @ fetchData(%id, "BANK") @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");			
	}			
	else if(%w1 == "#setteam") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				GameBase::setTeam(%id, %c2);
				RefreshAll(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Setting " @ %c1 @ " (" @ %id @ ") team to " @ GameBase::getTeam(%id) @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}
	else if(%w1 == "#setinvis") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				if(%c2 == 0) {
					if(fetchData(%id, "invisible"))
						UnHide(%id);
				}
				else if(%c2 == 1) {
					if(!fetchData(%id, "invisible"))
					GameBase::startFadeOut(%id);
					storeData(%id, "invisible", True);
				}
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Changed " @ %c1 @ " (" @ %id @ ") invisible state to " @ %c2 @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}				
	else if(%w1 == "#dumbai") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				if(%c2 == 0)		storeData(%id, "dumbAIflag", "");
				else if(%c2 == 1)	storeData(%id, "dumbAIflag", True);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Changed " @ %c1 @ " (" @ %id @ ") dumb AI flag state to '" @ fetchData(%id, "dumbAIflag") @ "'.");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}
	else if(%w1 == "#getvelocity") {
		%name = GetWord(%cropped, 0);
		%id = NEWgetClientByName(%name);
		
		if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
		else if(%id != -1) {
			%vel = Item::getVelocity(%id);
			Client::sendMessage(%TrueClientId, 0, %name @ " (" @ %id @ ") velocity: " @ %vel);
		} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");		
	}
	else if(%w1 == "#doexport") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);
		
		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				if(%c2 == 0)		%id.doExport = False;
				else if(%c2 == 1)	%id.doExport = True;
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Changed " @ %c1 @ " (" @ %id @ ") doExport to " @ %id.doExport @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}
	else if(%w1 == "#givethisstuff") {
		%c1 = GetWord(%cropped, 0);
		%stuff = String::NEWgetSubStr(%cropped, (String::len(%c1)+1), 99999);
		
		if(%c1 != -1) {
			%id = NEWgetClientByName(%c1);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				GiveThisStuff(%id, %stuff, True);
				if(Player::isAiControlled(%id))
					HardcodeAIskills(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Gave " @ %c1 @ " (" @ %id @ "): " @ %stuff);
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}
	else if(%w1 == "#takethisstuff") {
		%c1 = GetWord(%cropped, 0);
		%stuff = String::NEWgetSubStr(%cropped, (String::len(%c1)+1), 99999);

		if(%c1 != -1) {
			%id = NEWgetClientByName(%c1);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				if(HasThisStuff(%id, %stuff)) {
					TakeThisStuff(%id, %stuff);
					if(Player::isAiControlled(%id))
						HardcodeAIskills(%id);
					RefreshAll(%id);
					if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Took " @ %c1 @ " (" @ %id @ "): " @ %stuff);
				} else if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Could not take stuff.");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}
	else if(%w1 == "#refreshbotskills") {
		if(%cropped == "")
			Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
		else {
			%id = NEWgetClientByName(%cropped);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				HardcodeAIskills(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Refreshed skills for " @ %cropped @ " (" @ %id @ ").");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		}
	}
	else if(%w1 == "#nodroppack") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				if(%c2 == 0)		storeData(%id, "noDropLootbagFlag", "");
				else if(%c2 == 1)	storeData(%id, "noDropLootbagFlag", True);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Changed " @ %c1 @ " (" @ %id @ ") noDropLootbagFlag to '" @ fetchData(%id, "noDropLootbagFlag") @ "'.");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}
	else if(%w1 == "#playsound") {
		%c1 = GetWord(%cropped, 0);
		%pos = String::NEWgetSubStr(%cropped, (String::len(%c1)+1), 99999);

		if(%c1 != -1) {
			if(GetWord(%pos, 0) == -1) {
				if(GameBase::getLOSinfo(Client::getOwnedObject(%TrueClientId), 50000))
					%pos = $los::position;
				else %pos = GameBase::getPosition(%TrueClientId);
			}
			playSound(%c1, %pos);
			if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Playing sound " @ %c1 @ " at pos " @ %pos);
		} else Client::sendMessage(%TrueClientId, 0, "Please specify nsound & position.");			
	}	   
	else if(%w1 == "#getskill") {
		%name = GetWord(%cropped, 0);
		%id = NEWgetClientByName(%name);

		if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
		else if(%id != -1) {
			%sid = GetWord(%cropped, 1);
			if($SkillDesc[%sid] != "")
			Client::sendMessage(%TrueClientId, 0, %name @ " (" @ %id @ ") " @ $SkillDesc[%sid] @ " is " @ $PlayerSkill[%id, %sid]);
		} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");		
	}			  
	else if(%w1 == "#setvelocity") {
		%name = GetWord(%cropped, 0);
		%id = NEWgetClientByName(%name);

		if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
		else if(%id != -1) {
			%max = 5000;
			%x = Cap(floor(GetWord(%cropped, 1)), -%max, %max);
			%y = Cap(floor(GetWord(%cropped, 2)), -%max, %max);
			%z = Cap(floor(GetWord(%cropped, 3)), -%max, %max);

			%vel = %x @ " " @ %y @ " " @ %z;
			Item::setVelocity(%id, %vel);

			if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Set " @ %name @ " (" @ %id @ ") velocity to " @ %vel);
		} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
	} 
	else {
		%processed = False;
	}

	return %processed;
}



//________________________________________________________________________________________________________________________________________________________________
// ADMIN LEVEL 3
//________________________________________________________________________________________________________________________________________________________________
function processAdminLevel3(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai, %clientToServerAdminLevel) {
	%processed = True;
	
	if(%clientToServerAdminLevel < 3) {
		return False;
	}

	if(%w1 == "#spawn") {
		if(%cropped == "") Client::sendMessage(%TrueClientId, 0, "syntax: #spawn botType displayName loadout [team] [x] [y] [z]");
		else {
			%event = String::findSubStr(%cropped, ">");
			if(%event != -1) {
				%info = String::NEWgetSubStr(%cropped, 0, %event);
				%cmd = String::NEWgetSubStr(%cropped, %event, 99999);
			}
			else %info = %cropped;

			%c1 = GetWord(%info, 0);
			%c2 = GetWord(%info, 1);
			%loadout = GetWord(%info, 2);
			%team = GetWord(%info, 3);
			%ox = GetWord(%info, 4);
			%oy = GetWord(%info, 5);
			%oz = GetWord(%info, 6);

			if(%c1 != -1 && %c2 != -1 && %loadout != -1) {
				if(NEWgetClientByName(%c2) == -1) {
					if(%ox == -1 && %oy == -1 && %oz == -1) {
						%player = Client::getOwnedObject(%TrueClientId);
						GameBase::getLOSinfo(%player, 50000);
						%lospos = $los::position;
					} else %lospos = %ox @ " " @ %oy @ " " @ %oz;

					if(%team == -1) %team = 0;
					%n = AI::helper(%c1, %c2, "TempSpawn " @ %lospos @ " " @ %team, %loadout);
					%id = AI::getId(%n);

					if(%event != -1)
					AddEventCommand(%id, %senderName, "onkill", %cmd);

					if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Spawned " @ %n @ " (" @ %id @ ") at " @ %lospos @ ".");
				} else if(!%echoOff) Client::sendMessage(%TrueClientId, 0, %c2 @ " already exists.");
			} else Client::sendMessage(%TrueClientId, 0, "syntax: #spawn botType displayName loadout [team] [x] [y] [z]");
		}
	}
	else if(%w1 == "#onconsider") {
		if(%cropped != "") {
			%event = String::findSubStr(%cropped, ">");
			if(%event != -1) {
				%info = String::NEWgetSubStr(%cropped, 0, %event);
				%cmd = String::NEWgetSubStr(%cropped, %event, 99999);
			}
			else
			%info	= %cropped;

			%tag = GetWord(%info, 0);
			%object = $tagToObjectId[%tag];

			if(%object != "") {
				%radius = GetWord(%info, 1);
				%keep = GetWord(%info, 2);

				if(%keep == "true" || %keep == "false") {
					%targetname = GetWord(%info, 3);
					%tid = NEWgetClientByName(%targetname);
					if(String::ICompare(%targetname, "all") == 0 || %tid != -1) {
						if(%event != -1) {
							AddEventCommand(%tag, %senderName, "onConsider " @ %radius @ " " @ %keep @ " " @ %targetname, %cmd);
							if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "onConsider event set for tagname " @ %tag @ "(" @ %object @ ") for radius " @ %radius);
						} else Client::sendMessage(%TrueClientId, 0, "onConsider event definition failed.");
					} else Client::sendMessage(%TrueClientId, 0, "Invalid name. Please specify 'all' or target's name.");
				} else Client::sendMessage(%TrueClientId, 0, "Specify 'true' or 'false'. 'true' means that the onConsider event won't be deleted after use.");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid tagname.");
		} else Client::sendMessage(%TrueClientId, 0, "#onconsider tagname radius keep all/targetname");		
	}
	else if(%w1 == "#listonconsider") {
		if(%cropped != "") {
			%tag = GetWord(%cropped, 0);
			%object = $tagToObjectId[%tag];

			if(%object != "") {
				%index = GetEventCommandIndex(%object, "onConsider");
				if(%index != -1) {
					for(%i2 = 0; (%index2 = GetWord(%index, %i2)) != -1; %i2++)
					Client::sendMessage(%TrueClientId, 0, %tag @ " (" @ %object @ ") onConsider " @ %index2 @ ": " @ $EventCommand[%object, %index2]);
				}
			} else Client::sendMessage(%TrueClientId, 0, "Invalid tagname.");
		} else {
			%list = $DISlist;
			for(%p = String::findSubStr(%list, ","); (%p = String::findSubStr(%list, ",")) != -1; %list = String::NEWgetSubStr(%list, %p+1, 99999)) {
				%w = String::NEWgetSubStr(%list, 0, %p);
				%object = $tagToObjectId[%w];
				%index = GetEventCommandIndex(%object, "onConsider");
				if(%index != -1)
					Client::sendMessage(%TrueClientId, 0, %w @ ": " @ %index);
			}
		}
	}
	else if(%w1 == "#clearonconsider") {
		%tag = GetWord(%cropped, 0);
		%object = $tagToObjectId[%tag];

		if(%object != "") {
			%oindex = GetWord(%cropped, 1);
			%index = GetEventCommandIndex(%object, "onConsider");
			if(%index != -1) {
				for(%i2 = 0; (%index2 = GetWord(%index, %i2)) != -1; %i2++) {
					if(floor(%index2) == floor(%oindex) || %oindex == -1) {
						$EventCommand[%object.tag, %index2] = "";
						if(!%echoOff) Client::sendMessage(%TrueClientId, 0, %tag @ " (" @ %object @ ") onConsider " @ %index2 @ " cleared.");
					}
				}
			}
		} else Client::sendMessage(%TrueClientId, 0, "Incorrect tagname for #clearonconsider tagname [index]. If index is missing or -1, all onConsiders for name are cleared.");
	}
	else if(%w1 == "#exp") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				storeData(%id, "EXP", %c2, "inc");
				if(Player::isAiControlled(%id))
					HardcodeAIskills(%id);
				Game::refreshClientScore(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Setting " @ %c1 @ " (" @ %id @ ") EXP to " @ fetchData(%id, "EXP") @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");				
	}
	else if(%w1 == "#addsp") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				storeData(%id, "SPcredits", %c2, "inc");
				RefreshAll(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Setting " @ %c1 @ " (" @ %id @ ") SP credits to " @ fetchData(%id, "SPcredits") @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}
	else if(%w1 == "#addrank") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				storeData(%id, "RankPoints", %c2, "inc");
				RefreshAll(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Setting " @ %c1 @ " (" @ %id @ ") RankPoints to " @ fetchData(%id, "RankPoints") @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}
	else if(%w1 == "#setsp") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				storeData(%id, "SPcredits", %c2);
				RefreshAll(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Setting " @ %c1 @ " (" @ %id @ ") SP credits to " @ fetchData(%id, "SPcredits") @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}
	else if(%w1 == "#addfavor") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				storeData(%id, "FAVOR", %c2, "inc");
				RefreshAll(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Setting " @ %c1 @ " (" @ %id @ ") base FAVOR to " @ fetchData(%id, "FAVOR") @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}
	else if(%w1 == "#addexp") {				
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				storeData(%id, "EXP", %c2, "inc");
				if(Player::isAiControlled(%id))
					HardcodeAIskills(%id);
				Game::refreshClientScore(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Setting " @ %c1 @ " (" @ %id @ ") EXP to " @ fetchData(%id, "EXP") @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");				
	}
	else if(%w1 == "#setexp") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				storeData(%id, "EXP", %c2);
				Game::refreshClientScore(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Setting " @ %c1 @ " (" @ %id @ ") EXP to " @ fetchData(%id, "EXP") @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");				
	}			
	else if(%w1 == "#setrace") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				if(%c2 == "DeathKnight" && %clientToServerAdminLevel >= 4 || %c2 != "DeathKnight")
				ChangeRace(%id, %c2, %clientToServerAdminLevel);

				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Changed " @ %c1 @ " (" @ %id @ ") race to " @ fetchData(%id, "RACE") @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");			
	}
	else if(%w1 == "#getip") {
		if(%cropped != "") {
			%id = NEWgetClientByName(%cropped);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1)
				Client::sendMessage(%TrueClientId, 0, %cropped @ " (" @ %id @ ") IP is " @ Client::getTransportAddress(%id));
			else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");			
	}
	else if(%w1 == "#spawnpack") {
		if(%cropped != "") {
			%event = String::findSubStr(%cropped, ">");
			if(%event != -1) {
				%info = String::NEWgetSubStr(%cropped, 0, %event);
				%cmd = String::NEWgetSubStr(%cropped, %event, 99999);
			} else %info	= %cropped;

			%div = String::findSubStr(%info, "|");

			if(%div != -1) {
				%a = String::NEWgetSubStr(%info, 0, %div-1);
				%tag = GetWord(%a, 0);
				%ox = GetWord(%a, 1);
				%oy = GetWord(%a, 2);
				%oz = GetWord(%a, 3);
				if(%ox == -1 && %oy == -1 && %oz == -1) {
					//didn't enter coordinates.
					GameBase::getLOSinfo(Client::getOwnedObject(%TrueClientId), 50000);
					%pos = $los::position;
				} else %pos = %ox @ " " @ %oy @ " " @ %oz;

				if(!IsInCommaList($SpawnPackList, %tag)) {
					%pack = String::NEWgetSubStr(%info, %div+1, 99999);
					%pid = DeployLootbag(%pos, "0 0 0", %pack);
					$SpawnPackList = AddToCommaList($SpawnPackList, %tag);
					$tagToObjectId[%tag] = %pid;
					%pid.tag = %tag;

					if(%event != -1)
						AddEventCommand(%pid, %senderName, "onpickup", %cmd);
					if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Spawned pack (" @ %pid @ ") at position " @ %pos @ ".");
				} else Client::sendMessage(%TrueClientId, 0, "Tagname " @ %tag @ " already exists.");
			} else Client::sendMessage(%TrueClientId, 0, "Divider not found. Type #spawnpack with no parameters to get a quick overview.");
		} else Client::sendMessage(%TrueClientId, 0, "#spawnpack tagname [x] [y] [z] | packstring. Use this command only if you know what you're doing.");		
	}
	else if(%w1 == "#delpack") {			
		%tag = GetWord(%cropped, 0);
		if(%cropped != -1) {
			if($tagToObjectId[%tag] != "") {
				%object = $tagToObjectId[%tag];
				ClearEvents(%object);
				deleteObject(%object);
				$tagToObjectId[%tag] = "";
				$SpawnPackList = RemoveFromCommaList($SpawnPackList, %tag);

				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Deleted " @ %tag @ " (" @ %object @ ")");
			} else if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Invalid tagname.");
		} else Client::sendMessage(%TrueClientId, 0, "#delpack tagname.");		
	}			
	else if(%w1 == "#spawndis" || %w1 == "#sdis") {
		if(%cropped != "") {
			%f = GetWord(%cropped, 0);
			%tag = GetWord(%cropped, 1);
			%x = GetWord(%cropped, 2);
			%y = GetWord(%cropped, 3);
			%z = GetWord(%cropped, 4);
			%r1 = GetWord(%cropped, 5);
			%r2 = GetWord(%cropped, 6);
			%r3 = GetWord(%cropped, 7);

			%rotate = False;
			if(%f == "r" || %f == "R") {
				%rotate = True;
			}			
			%nudge = False;
			if(%f == "n" || %f == "N") {
				%nudge = True;
			}

			if(%x == -1 && %y == -1 && %z == -1) {
				GameBase::getLOSinfo(Client::getOwnedObject(%TrueClientId), 50000);
				%pos = $los::position;
			} else %pos = %x @ " " @ %y @ " " @ %z;

			if(%r1 == -1 && %r2 == -1 && %r3 == -1)	%rot = -1;
			else									%rot = %r1 @ " " @ %r2 @ " " @ %r3;

			if(!%rotate && !%nudge) {
				%fname = %f @ ".dis";
				%object = newObject(%tag, InteriorShape, %fname);
			} else if(IsInCommaList($DISlist, %tag)) {
				%object = $tagToObjectId[%tag];
			}				

			if(%object != 0 && %tag != -1) {
				if(IsInCommaList($DISlist, %tag)) {
					%o = $tagToObjectId[%tag];
					if(%nudge) {
						%p = GameBase::getPosition(%o);
						%pos = GetWord(%p,0) + %x @ " " @ GetWord(%p,1) + %y @ " " @ GetWord(%p,2) + %z;
						%w = "Nudged";
					} else if (%rotate) {
						%pos = GameBase::getPosition(%o);
						%rot = %x @ " " @ %y @ " " @ %z;
						%w = "Rotated";
					} else {						
						deleteObject(%o);
						$tagToObjectId[%tag] = "";
						%w = "Replaced";
					}
				} else {
					$DISlist = AddToCommaList($DISlist, %tag);
					%w = "Spawned";
				}

				addToSet("MissionCleanup", %object);
				$tagToObjectId[%tag] = %object;
				%object.tag = %tag;
				GameBase::setPosition(%object, %pos);
				if(!%nudge && !%rotate)	%object.shapeFile = %fname;
				else					%fname = %object.shapeFile;
		//		%fname = %object.fileName;
				if(%rot != -1)
					GameBase::setRotation(%object, %rot);

				if(!%echoOff) {
					Client::sendMessage(%TrueClientId, 0, %w @ " " @ %tag @ " (" @ %object @ ") at pos " @ %pos);
					if(%w1 != "#sdis") {
					%misdata = "instant InteriorShape \"" @ %tag @ "\" {\nfileName = \"" @ %fname @ "\";\nisContainer = \"1\";\nlightParams = \"0\";\nposition = \"" @ %pos @ "\";\nrotation = ";
						if(%rot == -1) 	%misdata = %misdata @ "\"" @ GameBase::getRotation(%object) @ "\";\n};";
						else 			%misdata = %misdata @ "\"" @ %rot @ "\";\n};";
					} else {
						%misdata = "rpg::DefineManagedObject(%zone, \"" @ %tag @ "\", \"InteriorShape\", \"" @ %fname @ "\", \"" @ %pos @ "\", ";
						if(%rot == -1) 	%misdata = %misdata @ "\"" @ GameBase::getRotation(%object) @ "\");";
						else 			%misdata = %misdata @ "\"" @ %rot @ "\");";
					}
					echo(%misdata);
				}
			} else Client::sendMessage(%TrueClientId, 0, "Invalid DIS filename or tagname.");
		} else Client::sendMessage(%TrueClientId, 0, "#spawndis filename tagname [x] [y] [z] [r1] [r2] [r3]. Do not specify .dis, this will automatically be added.");
	}
	else if(%w1 == "#spawndts") {
		if(%cropped != "") {
			%f = GetWord(%cropped, 0);
			%tag = GetWord(%cropped, 1);
			%x = GetWord(%cropped, 2);
			%y = GetWord(%cropped, 3);
			%z = GetWord(%cropped, 4);
			%r1 = GetWord(%cropped, 5);
			%r2 = GetWord(%cropped, 6);
			%r3 = GetWord(%cropped, 7);

			if(%x == -1 && %y == -1 && %z == -1) {
				GameBase::getLOSinfo(Client::getOwnedObject(%TrueClientId), 50000);
				%pos = $los::position;
			} else %pos = %x @ " " @ %y @ " " @ %z;

			if(%r1 == -1 && %r2 == -1 && %r3 == -1)	%rot = -1;
			else									%rot = %r1 @ " " @ %r2 @ " " @ %r3;

			%fname = %f; // @ ".dts";
			%object = newObject(%tag, "StaticShape", %fname, true);

			if(%object != 0 && %tag != -1) {
				if(IsInCommaList($DISlist, %tag)) {
					%o = $tagToObjectId[%tag];
					deleteObject(%o);
					$tagToObjectId[%tag] = "";
					%w = "Replaced";
				} else {
					$DISlist = AddToCommaList($DISlist, %tag);
					%w = "Spawned";
				}

				addToSet("MissionCleanup", %object);
				$tagToObjectId[%tag] = %object;
				%object.tag = %tag;
				GameBase::setPosition(%object, %pos);
				
				if(%rot != -1)
					GameBase::setRotation(%object, %rot);

				if(!%echoOff) {
					Client::sendMessage(%TrueClientId, 0, %w @ " " @ %tag @ " (" @ %object @ ") at pos " @ %pos);
					
					
						
						%misdata = "instant StaticShape \"" @ %tag @ "\" {\nname = \"\";\ndataBlock = \""@ %fname @"\";\ndestroyable = \"True\";\ndeleteOnDestroy = \"False\";\nposition = \"" @ %pos @"\";\nrotation = ";
						if(%rot == -1) 	%misdata = %misdata @ "\"0 0 0\";\n};";
						else 			%misdata = %misdata @ "\"" @ %rot @ "\";\n};";
						
					
					echo(%misdata);
				}
			} else Client::sendMessage(%TrueClientId, 0, "Invalid DTS filename or tagname.");
		} else Client::sendMessage(%TrueClientId, 0, "#spawndts filename tagname [x] [y] [z] [r1] [r2] [r3]. Do not specify .dis, this will automatically be added.");
	}
	else if(%w1 == "#deldts") {
		%tag = GetWord(%cropped, 0);
		if(%cropped != -1) {
			if($tagToObjectId[%tag] != "") {
				%object = $tagToObjectId[%tag];
				ClearEvents(%object);
				deleteObject(%object);
				$tagToObjectId[%tag] = "";
				$DISlist = RemoveFromCommaList($DISlist, %tag);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Deleted " @ %tag @ " (" @ %object @ ")");
			}
			else if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Invalid tagname.");
		} else Client::sendMessage(%TrueClientId, 0, "#deldis tagname.");
	}
	else if(%w1 == "#deldis") {
		%tag = GetWord(%cropped, 0);
		if(%cropped != -1) {
			if($tagToObjectId[%tag] != "") {
				%object = $tagToObjectId[%tag];
				ClearEvents(%object);
				deleteObject(%object);
				$tagToObjectId[%tag] = "";
				$DISlist = RemoveFromCommaList($DISlist, %tag);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Deleted " @ %tag @ " (" @ %object @ ")");
			} else if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Invalid tagname.");
		} else Client::sendMessage(%TrueClientId, 0, "#deldis tagname.");
	}								
	else if(%w1 == "#scheduleblock") {
		%bname = GetWord(%cropped, 0);
		if(%bname != -1) {
			if(IsInCommaList($BlockList[%senderName], %bname)) {
				%delay = GetWord(%cropped, 1);
				if(%delay >= 0.05) {
					%repeat = floor(GetWord(%cropped, 2));
					if(%repeat >= 0) {
						%rp = (%repeat+1);
						%arglist = String::NEWgetSubStr(%cropped, (String::len(%bname @ %delay @ %repeat @ "  ")+1), 99999);
						if(GetWord(%arglist, 0) != -1)	%txt = "#call " @ %bname @ " " @ %arglist;
						else							%txt = "#call " @ %bname;
						for(%sbi = 1; %sbi <= %rp; %sbi++)
							schedule("internalSay(" @ %clientId @ ", 0, \"" @ %txt @ "\", \"" @ %senderName @ "\");", %delay * %sbi);
						if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Block " @ %bname @ " scheduled for " @ %repeat @ " repeats at " @ %delay @ " second intervals.");
					} else Client::sendMessage(%TrueClientId, 0, "Schedule repeat too low, minimum is 0");
				} else Client::sendMessage(%TrueClientId, 0, "Schedule delay too low, minimum is 0.05");
			} else if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Block does not exist!");
		} else Client::sendMessage(%TrueClientId, 0, "Incorrect syntax for #scheduleblock blockName delay numRepeat");
	}
	else if(%w1 == "#listonhear") {
		if(%cropped != "") {
			%id = NEWgetClientByName(%cropped);
			if(%id != -1) {
				%index = GetEventCommandIndex(%id, "onHear");
				if(%index != -1) {
					for(%i2 = 0; (%index2 = GetWord(%index, %i2)) != -1; %i2++)
						Client::sendMessage(%TrueClientId, 0, Client::getName(%id) @ " onHear " @ %index2 @ ": " @ $EventCommand[%id, %index2]);
					}
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
	}
	else if(%w1 == "#clearonhear") {
		%name = GetWord(%cropped, 0);
		%oindex = GetWord(%cropped, 1);

		if(%name != -1) {
			%id = NEWgetClientByName(%name);
			if(%id != -1) {
				%index = GetEventCommandIndex(%id, "onHear");
				if(%index != -1) {
					for(%i2 = 0; (%index2 = GetWord(%index, %i2)) != -1; %i2++) {
						if(floor(%index2) == floor(%oindex) || %oindex == -1) {
							$EventCommand[%id, %index2] = "";
							if(!%echoOff) Client::sendMessage(%TrueClientId, 0, Client::getName(%id) @ " onHear " @ %index2 @ " cleared.");
						}
					}
				}
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Incorrect syntax for #clearonhear name [index]. If index is missing or -1, all onHears for name are cleared.");			
	}						
	else if(%w1 == "#delbot") {
		if(%cropped != -1) {
			%id = NEWgetClientByName(%cropped);

			if(%id != -1) {
				if(Player::isAiControlled(%id)) {
					storeData(%id, "noDropLootbagFlag", True);
					ClearEvents(%id);
					Player::Kill(%id);
					if(!%echoOff) Client::sendMessage(%TrueClientId, 0, %cropped @ " (" @ %id @ ") was deleted.");
				} else Client::sendMessage(%TrueClientId, 0, "This command only works on bots.");
			} else if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");		
	}
	else if(%w1 == "#loadout") {
		%c1 = GetWord(%cropped, 0);
		%stuff = String::NEWgetSubStr(%cropped, (String::len(%c1)+1), 99999);

		if(%c1 != -1) {
			if(!IsInCommaList($LoadOutList, %c1)) {
				$LoadOutList = AddToCommaList($LoadOutList, %c1);
				$LoadOut[%c1] = %stuff;
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Loadout " @ %c1 @ " defined.");
			} else if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Loadout tagname already exists.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify tagname & data.");
	}								
	else if(%w1 == "#delloadout") {
		%c1 = GetWord(%cropped, 0);
		
		if(%c1 != -1) {
			if(IsInCommaList($LoadOutList, %c1)) {
				$LoadOutList = RemoveFromCommaList($LoadOutList, %c1);
				$LoadOut[%c1] = "";
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Loadout " @ %c1 @ " deleted.");
			} else if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Loadout tagname does not exist.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify tagname.");
	}			
	else if(%w1 == "#showloadout") {
		%c1 = GetWord(%cropped, 0);
		
		if(%c1 != -1) {
			if(IsInCommaList($LoadOutList, %c1))
				Client::sendMessage(%TrueClientId, 0, %c1 @ ": " @ $LoadOut[%c1]);
			else if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Loadout tagname does not exist.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify tagname.");
	}
	else if(%w1 == "#listloadouts") {
		%list = $LoadOutList;
		for(%p = String::findSubStr(%list, ","); (%p = String::findSubStr(%list, ",")) != -1; %list = String::NEWgetSubStr(%list, %p+1, 99999)) {
			%w = String::NEWgetSubStr(%list, 0, %p);
			Client::sendMessage(%TrueClientId, 0, %w @ ": " @ $LoadOut[%w]);
		}
	}
	else if(%w1 == "#nobotsniff") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				if(%c2 == 0) 		storeData(%id, "noBotSniff", "");
				else if(%c2 == 1)	storeData(%id, "noBotSniff", True);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Changed " @ %c1 @ " (" @ %id @ ") noBotSniff flag to " @ %c2 @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}
	else if(%w1 == "#addrankpoints") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				storeData(%id, "RankPoints", %c2, "inc");
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Setting " @ %c1 @ " (" @ %id @ ") RankPoints to " @ fetchData(%id, "RankPoints") @ ".");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & data.");
	}
	else if(%w1 == "#sethouse") {
		%c1 = GetWord(%cropped, 0);
		%c2 = GetWord(%cropped, 1);

		if(%c1 != -1 && %c2 != -1) {
			%id = NEWgetClientByName(%c1);

			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				%hn = "";
				if(String::ICompare(%c2, "null") == 0)
					%hn = 0;
				else {
					for(%i = 1; $HouseName[%i] != ""; %i++) {
						if(String::findSubStr($HouseName[%i], %c2) != -1)
						%hn = %i;
					}
				}

				if(%hn != "") {
					%hname = $HouseName[%hn];
					storeData(%id, "MyHouse", %hname);
					if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Setting " @ %c1 @ " (" @ %id @ ") House to " @ fetchData(%id, "MyHouse") @ ".");
				} else Client::sendMessage(%TrueClientId, 0, "Invalid House.");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name & house (to clear house, use: #sethouse name NULL).");
	}
	else if(%w1 == "#onhear") {
		if(%cropped != "") {
			%event = String::findSubStr(%cropped, ">");
			if(%event != -1) {
				%info = String::NEWgetSubStr(%cropped, 0, %event);
				%cmd = String::NEWgetSubStr(%cropped, %event, 99999);
			} else %info = %cropped;

			%var = GetWord(%info, 4);
			if(String::ICompare(%var, "var") == 0)
				%var = "var";
			else {
				%var = "";
				%quote1 = String::findSubStr(%info, "\"");
				%quote2 = String::ofindSubStr(%info, "\"", %quote1+1);
			}
			
			if(%quote1 != -1 && %quote2 != -1 || %var != "") {
				%pname = GetWord(%info, 0);
				%id = NEWgetClientByName(%pname);

				if(%id != -1) {
					%pname = Client::getName(%id);	//properly capitalize name
					%radius = GetWord(%info, 1);
					%keep = GetWord(%info, 2);

					if(%keep == "true" || %keep == "false") {
						%targetname = GetWord(%info, 3);
						%tid = NEWgetClientByName(%targetname);
						if(String::ICompare(%targetname, "all") == 0 || %tid != -1) {
							if(%var != "") {
								%vtxt = %var;
								%text = "var";
							} else {
								%text = String::NEWgetSubStr(%info, %quote1+1, %quote2);
								%vtxt = "|" @ %text @ "|";
							}

							if(%text != "") {
								if(%event != -1) {
									AddEventCommand(%id, %senderName, "onHear " @ %pname @ " " @ %radius @ " " @ %keep @ " " @ %targetname @ " " @ %vtxt, %cmd);
									if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "onHear event set for " @ %pname @ "(" @ %id @ ") with text: \"" @ %text @ "\"");
								} else Client::sendMessage(%TrueClientId, 0, "onHear event definition failed.");
							} else Client::sendMessage(%TrueClientId, 0, "Invalid text.");
						} else Client::sendMessage(%TrueClientId, 0, "Invalid name. Please specify 'all' or target's name.");
					} else Client::sendMessage(%TrueClientId, 0, "Specify 'true' or 'false'. 'true' means that the onHear event won't be deleted after use. 'false' is recommended to keep things clean.");
				} else Client::sendMessage(%TrueClientId, 0, "Invalid name.");
			} else Client::sendMessage(%TrueClientId, 0, "Quotes for text not found.");
		} else Client::sendMessage(%TrueClientId, 0, "#onhear name radius keep all/targetname \"text\"/var.");									
	}								
	else if(%w1 == "#addskill") {
		%name = GetWord(%cropped, 0);
		%id = NEWgetClientByName(%name);

		if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
		else if(%id != -1) {
			%sid = GetWord(%cropped, 1);
			if($SkillDesc[%sid] != "") {
				%sn = floor(GetWord(%cropped, 2));
				if(%sn != 0) {
					$PlayerSkill[%id, %sid] += %sn;
					RefreshAll(%id);
					if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Set " @ %name @ " (" @ %id @ ") " @ $SkillDesc[%sid] @ " to " @ $PlayerSkill[%id, %sid]);
				}
			}
		} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
	}					
	else if(%w1 == "#block") {
		%bname = GetWord(%cropped, 0);
		if(%bname != -1) {
			ClearBlockData(%senderName, %bname);

			if(!IsInCommaList($BlockList[%senderName], %bname))
			$BlockList[%senderName] = AddToCommaList($BlockList[%senderName], %bname);

			storeData(%TrueClientId, "BlockInputFlag", %bname);
			storeData(%TrueClientId, "tmpBlockCnt", "");

			ManageBlockOwnersList(%senderName);
		} else Client::sendMessage(%TrueClientId, 0, "Incorrect syntax for #block [blockname]");
	}
	else if(%w1 == "#endblock") {
		if(fetchData(%TrueClientId, "BlockInputFlag") != "") {
			storeData(%TrueClientId, "BlockInputFlag", "");
			storeData(%TrueClientId, "tmpBlockCnt", "");
		} else Client::sendMessage(%TrueClientId, 0, "No block to end!");
	}						
	else if(%w1 == "#delblock") {
		%bname = GetWord(%cropped, 0);
		if(%bname != -1) {
			if(IsInCommaList($BlockList[%senderName], %bname)) {
				ClearBlockData(%senderName, %bname);
				$BlockList[%senderName] = RemoveFromCommaList($BlockList[%senderName], %bname);
				ManageBlockOwnersList(%senderName);

				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Block " @ %bname @ " deleted.");
			} else if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Block does not exist!");
		} else Client::sendMessage(%TrueClientId, 0, "Incorrect syntax for #delblock [blockname]");
	}
	else if(%w1 == "#call") {
		%bname = GetWord(%cropped, 0);
		if(%bname != -1) {
			%list = String::NEWgetSubStr(%cropped, (String::len(%bname)+1), 99999);
			for(%p = String::findSubStr(%list, ","); (%p = String::findSubStr(%list, ",")) != -1; %list = String::NEWgetSubStr(%list, %p+1, 99999))
			%a[%c++] = String::NEWgetSubStr(%list, 0, %p);

			if(%c <= 8) {
				if(IsInCommaList($BlockList[%senderName], %bname)) {
					%TrueClientId.echoOff = True;
					for(%i = 1; (%bd = $BlockData[%senderName, %bname, %i]) != ""; %i++) {
						if(%a[1] != "")
							%bd = nsprintf(%bd, %a[1], %a[2], %a[3], %a[4], %a[5], %a[6], %a[7], %a[8]);
						internalSay(%clientId, 0, %bd, %senderName);
					}
					%TrueClientId.echoOff = "";
				} else Client::sendMessage(%TrueClientId, 0, "Block does not exist!");
			} else Client::sendMessage(%TrueClientId, 0, "Too many parameters for #call (max of 8)");
		} else Client::sendMessage(%TrueClientId, 0, "Incorrect syntax for #call [blockname]");
	}
	else {
		%processed = False;
	}
	
	return %processed;
}



//________________________________________________________________________________________________________________________________________________________________
// ADMIN LEVEL 4
//________________________________________________________________________________________________________________________________________________________________
function processAdminLevel4(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai, %clientToServerAdminLevel) {
	%processed = True;

	if(%clientToServerAdminLevel < 4) {
		return False;
	}

	if(%w1 == "#fixspellflag") {
		if(%cropped == "") Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
		else {
			%id = NEWgetClientByName(%cropped);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				storeData(%id, "SpellCastStep", "");
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Spell flag reset.");
			}
			else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		}
	}		
	else if(%w1 == "#movetome") {
		%id = NEWgetClientByName(%cropped);
		if(%id > 2048)
			GameBase::setPosition(%id,GameBase::getPosition(%TrueClientId));
	}		
	else if(%w1 == "#human") {
		ChangeRace(%TrueClientId, "Human");
	}	
	else if(%w1 == "#loadworld") {
		if(%cropped == "") 	LoadWorld();
		else				Client::sendMessage(%TrueClientId, 0, "Do not use parameters for this function call.");
	}			
	else if(%w1 == "#saveworld") {
		if(%cropped == "")	SaveWorld();
		else				Client::sendMessage(%TrueClientId, 0, "Do not use parameters for this function call.");			
	}
	else if(%w1 == "#clearstorage") {
		if(%cropped == "")
			Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
		else {
			%id = NEWgetClientByName(%cropped);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				storeData(%id, "BankStorage", "");
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, %id @ " bank storage cleared.");
			} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		}
	}
	else if(%w1 == "#setstorage") {
		%name = GetWord(%cropped, 0);				
		%id = NEWgetClientByName(%name);

		if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
		else if(%id != -1) {
			storeData(%id, "BankStorage", rpg::ModifyItemList(fetchData(%id, "BankStorage"), GetWord(%cropped, 1), GetWord(%cropped, 2)));
			if(!%echoOff) Client::sendMessage(%TrueClientId, 0, %id @ " bank storage modified. Use #getstorage [name] to view.");
		} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
	}
	else if(%w1 == "#clearloadouts") {
		%list = $LoadOutList;
		$LoadOutList = "";
		for(%p = String::findSubStr(%list, ","); (%p = String::findSubStr(%list, ",")) != -1; %list = String::NEWgetSubStr(%list, %p+1, 99999)) {
			%w = String::NEWgetSubStr(%list, 0, %p);
			$LoadOut[%w] = "";
		}
		if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Deleted ALL loadouts.");			
	}
	else {
		%processed = False;
	}
	
	return %processed;
}



//________________________________________________________________________________________________________________________________________________________________
// ADMIN LEVEL 5
//________________________________________________________________________________________________________________________________________________________________
function processAdminLevel5(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai, %clientToServerAdminLevel) {
	%processed = True;
	
	if(%clientToServerAdminLevel < 5) {
		return False;
	}
	
	if(%w1 == "#setupai") {
		for(%i = 0; (%id = GetWord($TownBotList, %i)) != -1; %i++)
			deleteObject(%id);
		InitTownBots(true);
	}
	else if(%w1 == "#markhere") {
		%objpos = GameBase::getPosition(%TrueClientId);
		%msg = %cropped @ ": " @ %objpos;
		Client::sendMessage(%TrueClientId, $MsgWhite, %msg);
		echo(%msg);
	}
	else if(%w1 == "#markthere") {
		GameBase::getLOSinfo(Client::getOwnedObject(%TrueClientId), 25, "0 0 " @ %i);
		%dist = Vector::getDistance(GameBase::getPosition(%TrueClientId), $los::position);
		%msg = %cropped @ ": " @ $los::position @ " (dist: " @ %dist @ ")";
		Client::sendMessage(%TrueClientId, $MsgWhite, %msg);
		echo(%msg);
	}
	else if(%w1 == "#mark4spawn") {
		%objpos = GameBase::getPosition(%TrueClientId);		
		%msg = "instant Marker \".4 1 2 20 50 16\" {\n";
		%msg = %msg @ "position = \"" @ %objpos @ "\"; name = \"" @ %cropped @ "\"; dataBlock = \"PathMarker\"; rotation = \"0 0 0\";\n};";
		Client::sendMessage(%TrueClientId, $MsgWhite, "marked " @ %cropped );
		echo(%msg);
	}
	else if(%w1 == "#mark4bot") {
		%pos = GameBase::getPosition(%TrueClientId);
		%rot = GameBase::getRotation(%TrueClientId);
		%name = GetWord(%cropped, 0);
		%chatname = GetWord(%cropped, 1);
		%moarname = GetWord(%cropped, 2);
		
		if(%name == "merchant") {
			%msg = "instant SimGroup \"merchant\" {";
			%msg = %msg @ "\ninstant SimGroup \"NAME " @ %chatname @ "\"; instant SimGroup \"SHOP \"; instant SimGroup \"RACE MaleHuman\"; instant SimGroup \"ITEMS CLASS Ranger LVL 10 FAVOR 0 Dagger 1\";";		
		} else if(%name == "banker") {
			%msg = "instant SimGroup \"banker\" {";
			%msg = %msg @ "\ninstant SimGroup \"NAME " @ %chatname @ "\"; instant SimGroup \"RACE MaleHuman\"; instant SimGroup \"ITEMS CLASS Ranger LVL 10 FAVOR 0 Dagger 1\";";
		} else {
			%msg = "instant SimGroup \"info\" {";
			%msg = %msg @ "\ninstant SimGroup \"NAME " @ %moarname @ "\"; instant SimGroup \"CUE1 " @ %chatname @ "\"; instant SimGroup \"RACE MaleHumanRobed\"; instant SimGroup \"ITEMS CLASS Ranger LVL 10 FAVOR 0 Dagger 1\";";
		}
		%msg = %msg @ "\ninstant Marker \"Marker1\" { position = \"" @ %pos @ "\"; rotation = \"" @ %rot @ "\"; dataBlock = \"PathMarker\"; name = \"\"; };\n};";
		
		Client::sendMessage(%TrueClientId, $MsgWhite, "marked " @ %cropped );
		echo(%msg);
	}
	else if(%w1 == "#shapeinfo") {
		%tag = GetWord(%cropped, 0);
		if(%cropped != -1) {
			if($tagToObjectId[%tag] != "") {
				%object = $tagToObjectId[%tag];
				%msg = GameBase::getPosition(%object) @ "\n" @ GameBase::getRotation(%object);
				Client::sendMessage(%TrueClientId, 0, %msg);
				echo(%msg);
			}
			else if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Invalid tagname.");
		} else Client::sendMessage(%TrueClientId, 0, "#shapeinfo tagname.");
	}
	else if(%w1 == "#setpos") {
		GameBase::setPosition(%TrueClientId, %cropped);
	}
	else if(%w1 == "#clearchar") {				
		if(%cropped != -1) {
			%id = NEWgetClientByName(%cropped);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
			Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1) {
				playNextAnim(%id);
				Player::Kill(%id);
				ResetPlayer(%id);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, %cropped @ " (" @ %id @ ") profile was RESET.");
			}
			else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		}
		else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
	}
	else if(%w1 == "#exportchat") {
		if(%cropped != "") {
			if(%cropped == "0")			$exportChat = False;
			else if(%cropped == "1")	$exportChat = True;
			if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "exportChat set to " @ $exportChat @ ".");
		}
		else Client::sendMessage(%TrueClientId, 0, "Specify 1 or 0 (1 = True, 0 = False).");
	}
	else if(%w1 == "#deleteobject") {
		%c1 = GetWord(%cropped, 0);
		if(%c1 != -1) {
			if(%c1.tag != "") {
				$tagToObjectId[%c1.tag] = "";
				if(IsInCommaList($DISlist, %c1.tag))
					$DISlist = RemoveFromCommaList($DISlist, %c1.tag);
				else if(IsInCommaList($SpawnPackList, %c1.tag))
					$SpawnPackList = RemoveFromCommaList($SpawnPackList, %c1.tag);
			}
			deleteObject(%c1);
			ClearEvents(%c1);

			if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Attempted to deleteObject(" @ %c1 @ ")");
		} else Client::sendMessage(%TrueClientId, 0, "#deleteobject [objectId].  Be careful with this command.");
	}
	else if(%w1 == "#clearblocks") {
		%targetName = GetWord(%cropped, 0);
		%id = NEWgetClientByName(%targetName);

		if(%id != -1) {
			if($BlockList[%targetName] != "") {
				%list = $BlockList[%targetName];
				$BlockList[%targetName] = "";
				for(%p = String::findSubStr(%list, ","); (%p = String::findSubStr(%list, ",")) != -1; %list = String::NEWgetSubStr(%list, %p+1, 99999)) {
					%w = String::NEWgetSubStr(%list, 0, %p);
					ClearBlockData(%targetName, %w);
				}
				ManageBlockOwnersList(%targetName);
				if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Deleted ALL of " @ %targetName @ "'s blocks.");
			}
		} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
	}
	else if(%w1 == "#clearallblocks") {
		%bname = GetWord(%cropped, 0);
		if(%bname == "confirm") {
			%blist = $BlockOwnersList;
			for(%bp = String::findSubStr(%blist, ","); (%bp = String::findSubStr(%blist, ",")) != -1; %blist = String::NEWgetSubStr(%blist, %bp+1, 99999)) {
				%name = String::NEWgetSubStr(%blist, 0, %bp);

				if($BlockList[%name] != "") {
					%list = $BlockList[%name];
					$BlockList[%name] = "";
					for(%p = String::findSubStr(%list, ","); (%p = String::findSubStr(%list, ",")) != -1; %list = String::NEWgetSubStr(%list, %p+1, 99999)) {
						%w = String::NEWgetSubStr(%list, 0, %p);
						ClearBlockData(%name, %w);
					}
				}
				ManageBlockOwnersList(%name);
			}
			if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "Deleted EVERYONE's blocks.");
		} else Client::sendMessage(%TrueClientId, 0, "Type #clearallblocks confirm to clear EVERYONE's blocks.");
	}
	else if(%w1 == "#listblocks") {
		if(%cropped != "") {
			if(IsInCommaList($BlockOwnersList, %cropped))
				Client::sendMessage(%TrueClientId, 0, %cropped @ "'s BlockList: " @ $BlockList[%cropped]);
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
	}
	else if(%w1 == "#getotherinfo") {
		if(%cropped != -1) {
			%id = NEWgetClientByName(%cropped);
			if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && Client::getName(%id) != %senderName)
				Client::sendMessage(%TrueClientId, 0, "Could not process command: Target admin clearance level too high.");
			else if(%id != -1)
				Client::sendMessage(%TrueClientId, 0, %cropped @ " $Client::info[" @ %id @ ", 5] is " @ $Client::info[%id, 5] @ ".");
			else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify player name.");
	}
	else if(%w1 == "#if") {
		if(%cropped != "") {
			%info	= %cropped;
			%para1 = String::findSubStr(%info, "{");
			%para2 = String::ofindSubStr(%info, "}", %para1+1);
			if(%para1 != -1 && %para2 != -1) {
				%expression = String::NEWgetSubStr(%info, %para1+1, %para2);
				if((%pw = CheckForProtectedWords(%expression)) == "") {
					%command = String::NEWgetSubStr(%info, %para1+%para2+3, 99999);
					%retval = eval("%x = (" @ %expression @ ");");

					if(%retval == 0)	%r = false;
					else				%r = true;
					if(!%echoOff) 					Client::sendMessage(%TrueClientId, 0, "(" @ %expression @ ") = " @ %r);
					if(%retval && %command != "")	internalSay(%clientId, 0, %command, %senderName);
				} else Client::sendMessage(%TrueClientId, 0, "Protected word '" @ %pw @ "' can't be used in the #if statement.");
			} else Client::sendMessage(%TrueClientId, 0, "{ and } found.");
		} else Client::sendMessage(%TrueClientId, 0, "#if {expression} command");
	}
	else if(%w1 == "#setspawnmultiplier") {
		%c1 = GetWord(%cropped, 0);
		if(%c1 != -1) {
			$spawnMultiplier = Cap(%c1, 0, "inf");
			if(!%echoOff) Client::sendMessage(%TrueClientId, 0, "spawnMultiplier set to " @ $spawnMultiplier @ ".");
		} else Client::sendMessage(%TrueClientId, 0, "Please specify a number (normal should be 1. 0 will cease spawning.)");
	}
	else if(%w1 == "#listblockowners") {
		Client::sendMessage(%TrueClientId, $MsgBeige, $BlockOwnersList);
	} 
	else {
		%processed = False;
	}
	
	return %processed;
}
