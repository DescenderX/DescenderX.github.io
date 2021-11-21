//This file is part of Tribes RPG.
//Tribes RPG client side scripts
//Repack RPG additions written by Jason "phantom" Daley, tribesrpg.org

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

//________________________________________________________________________________________________________________________________________________________________
// DescX Notes:
//		comchat.cs was such a bloated, upsetting mess that it caused me to become angry several times. I could not continue without cleaning it up.
//		Comchat is now split into separate files for admin, player, and skill functions.
//
//		The basic call to internalsay simply switches through functions related to each group of commands, and I've corrected the grevious error
//		of forgetting to use the "else" keyword. This cleans up the functions massively. Comchat is now readable and is 100% less likely to cause heart attacks :)


$MsgTypeSystem = 0;
$MsgTypeGame = 1;
$MsgTypeChat = 2;
$MsgTypeTeamChat = 3;
$MsgTypeCommand = 4;

$MsgWhite = 0;
$MsgRed = 1;
$MsgBeige = 2;
$MsgGreen = 3;

function remoteSay(%clientId, %team, %message, %senderName)
{
	//tribesrpg.org
	if(%sendername != ""){
		%n = Client::getName(%ClientId);
		%ip = Client::getTransportAddress(%ClientId);
		messageall(0,"Exploit attempt detected and blocked: " @ %ClientId @ ", aka " @ %n @ ", at " @ %ip @ ".");
		messageall(0,"Exploit: " @ %message);
		echo("Exploit attempt detected and blocked: " @ %ClientId @ ", aka " @ %n @ ", at " @ %ip @ ".");
		echo("Exploit: " @ %message);
		return;
	}
	internalSay(%clientId, %team, %message);
}

//________________________________________________________________________________________________________________________________________________________________
// All instances of "remotesay" in all scripts should be changed to "internalsay".
// Old comments in this code STRONGLY indicate that none of the lines above actual command processing should be changed.
// This function is entered in a variety of ways. The cluster of logic at the top looks nonsensical, but it's actually required code.
//________________________________________________________________________________________________________________________________________________________________
function internalSay(%clientId, %team, %message, %senderName)
{
	dbecho($dbechoMode, "internalSay(" @ %clientId @ ", " @ %team @ ", \"" @ %message @ "\", " @ %senderName @ ")");
	
	if(%clientId.IsInvalid)
		return;

	%TrueClientId = %clientId;
	if(%senderName == "nofloodprotect") {
		%clientId.lastSayTime=0;
		%senderName = "";
	}
	
	if(%senderName != "") {
		%clientId = 2048;
		%clientToServerAdminLevel = $BlockOwnerAdminLevel[%senderName];
	} else {
		%senderName = Client::getName(%clientId);
		%clientToServerAdminLevel = floor(%clientId.adminLevel);
	}
	
	%isai = Player::isAiControlled(%clientId);
	if(%isai)
		%clientToServerAdminLevel = 3;

	if(%TrueClientId == 2048)	%echoOff = True;
	else						%echoOff = %TrueClientId.echoOff;

	if(%TrueClientId != 2048)	%TCsenderName = Client::getName(%TrueClientId);
	else						%TCsenderName = %senderName;

	%time = getIntegerTime(true) >> 5;
	if(%time - %clientId.lastSayTime <= $sayDelay && !(%clientToServerAdminLevel >= 1))
		return;
	%clientId.lastSayTime = %time;

	%msg = %clientId @ " \"" @ escapeString(%message) @ "\"";

	if(!isOneOf(GetWord(%message, 0), "#use", "#render", "#spell", "#cast") && !Player::isAiControlled(%clientId)){
		// check for flooding if it's a broadcast OR if it's team in FFA
		if($Server::FloodProtectionEnabled && (!$Server::TourneyMode || !%team) && !(%clientToServerAdminLevel >= 1)) {
			%time = getIntegerTime(true) >> 5;
			if(%TrueClientId.floodMute) {
				%delta = %TrueClientId.muteDoneTime - %time;
				if(%delta > 0) {
					Client::sendMessage(%TrueClientId, $MSGTypeGame, "FLOOD! You cannot talk for " @ %delta @ " seconds.");
					return;
				}
				%TrueClientId.floodMute = "";
				%TrueClientId.muteDoneTime = "";
			}
			%TrueClientId.floodMessageCount++;
			schedule(%TrueClientId @ ".floodMessageCount--;", 5, %TrueClientId);
			if(%TrueClientId.floodMessageCount > 4) {
				%TrueClientId.floodMute = true;
				%TrueClientId.muteDoneTime = %time + 10;
				Client::sendMessage(%TrueClientId, $MSGTypeGame, "FLOOD! You cannot talk for 10 seconds.");
				return;
			}
		}
		// Updated exploit block thingies ported from main server
		if(string::len(%defaulttalk@" - "@%message) >= 200) {
			pecho("censor:"@%message);
			%message = "(length-censored)";
			%stringlength = string::len(%message);
		}
		if(String::findSubStr(%message, "\\n") != -1 || String::findSubStr(%message, "\\t") != -1 || String::findSubStr(%msg, "~)") != -1 || String::findSubStr(%msg, "\\x") != -1)
			%message = "(linebreak-censored)";
	}
	%isNumber = false;
		//check for a bulknum-type of message	if(string::compare(%message, floor(%message)) == 0) {
		if(%clientId.currentShop != "" || %clientId.currentBank != "") {
			if(%message < 1)	%message = 1;
			if(%message > 100)	%message = 100;
		}
		%TrueClientId.bulkNum = %message;		%isNumber = true;
	}

	//parse message
	%botTalk = False;
	%isCommand = False;

	if(String::getSubStr(%message, 0, 1) != "#") {
		if(%team)			%message = "#zone " @ %message;		else if(%isNumber)	%message = "#say " @ %message;
		else				%message = fetchData(%TrueClientId, "defaultTalk") @ " " @ %message;
	}
	if(String::getSubStr(%message, 0, 1) == "#")
		%isCommand = True;

	if($exportChat) {
		%ip = Client::getTransportAddress(%TrueClientId);
		if(%TrueClientId.doExport) {
			$log::msg["[\"" @ %TCsenderName @ "\"]"] = %message;
			export("log::msg[\"" @ %TCsenderName @ "\"*", "temp\\log$ @ " @ %TCsenderName @ ".cs", true);
		}
	}

	%w1 = GetWord(%message, 0);

	if(fetchData(%TrueClientId, "BlockInputFlag") != "" && String::ICompare(%w1, "#endblock") != 0 && %w1 != -1 && %message != "") {
		//Entering block information into memory
		%tmpBlockCnt = fetchData(%TrueClientId, "tmpBlockCnt") + 1;
		storeData(%TrueClientId, "tmpBlockCnt", %tmpBlockCnt);
		$BlockData[%TCsenderName, fetchData(%TrueClientId, "BlockInputFlag"), %tmpBlockCnt] = %message;
		return 0;
	}

	%cropped = String::NEWgetSubStr(%message, (String::len(%w1)+1), 99999);

	if(%isCommand) {
		if ( processChat(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai) )				return;	
		if ( IsDead(%TrueClientId) && %TrueClientId != 2048 )																		return;

		if ( processPlayerCommands(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai) )	return;
		if ( processMinionControls(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai) )	return;
		if ( processThievery(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai) )			return;
		
		if(fetchData(%clientId, "invisible") != 2) {	// When Shadow Walking (perm stealth), disable Survival, Wordsmith, spells, and weapons 
			if ( processSurvival(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai) )		return;
			if ( processWordsmith(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai) )	return;
		}

		if ( processPackManagement(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai) )	return;		
		if ( processAdminLevel1(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai, %clientToServerAdminLevel) )	return;			
		if ( processAdminLevel2(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai, %clientToServerAdminLevel) )	return;			
		if ( processAdminLevel3(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai, %clientToServerAdminLevel) )	return;			
		if ( processAdminLevel4(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai, %clientToServerAdminLevel) )	return;			
		if ( processAdminLevel5(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai, %clientToServerAdminLevel) )	return;		
	}
	
	if(%botTalk) {
		processbottalk(%clientId,%TrueClientId,%message,%cropped,%w1);
		return;
	}
}

function remoteIssueCommand(%commander, %cmdIcon, %command, %wayX, %wayY, %dest1, %dest2, %dest3, %dest4, %dest5, %dest6, %dest7, %dest8, %dest9, %dest10, %dest11, %dest12, %dest13, %dest14) {
	for(%i = 1; %dest[%i] != ""; %i = %i + 1)
		if(!%dest[%i].muted[%commander])
			issueCommandI(%commander, %dest[%i], %cmdIcon, %command, %wayX, %wayY);
}

function remoteIssueTargCommand(%commander, %cmdIcon, %command, %targIdx, %dest1, %dest2, %dest3, %dest4, %dest5, %dest6, %dest7, %dest8, %dest9, %dest10, %dest11, %dest12, %dest13, %dest14) {
	for(%i = 1; %dest[%i] != ""; %i = %i + 1)
		if(!%dest[%i].muted[%commander])
			issueTargCommand(%commander, %dest[%i], %cmdIcon, %command, %targIdx);
}

function remoteCStatus(%clientId, %status, %message) {
	if(setCommandStatus(%clientId, %status, %message)) {
		if($dedicated)
			echo("COMMANDSTATUS: " @ %clientId @ " \"" @ escapeString(%message) @ "\"");
	}
	else internalSay(%clientId, true, %message);
}

function teamMessages(%mtype, %team1, %message1, %team2, %message2, %message3) {
	%numPlayers = getNumClients();
	for(%i = 0; %i < %numPlayers; %i = %i + 1) {
		%id = getClientByIndex(%i);
		if(Client::getTeam(%id) == %team1)							Client::sendMessage(%id, %mtype, %message1);		
		else if(%message2 != "" && Client::getTeam(%id) == %team2)	Client::sendMessage(%id, %mtype, %message2);		
		else if(%message3 != "")									Client::sendMessage(%id, %mtype, %message3);
	}
}

function messageAll(%mtype, %message, %filter) {
	dbecho($dbechoMode, "messageAll(" @ %mtype @ ", " @ %message @ ", " @ %filter @ ")");

	if(%filter == ""){
		for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl))
			Client::sendMessage(%cl, %mtype, %message);
	} else {
		for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl)) {
			if(%cl.messageFilter & %filter)
			Client::sendMessage(%cl, %mtype, %message);
		}
	}
}

function messageAllExcept(%except, %mtype, %message) {
	dbecho($dbechoMode, "messageAllExcept(" @ %except @ ", " @ %mtype @ ", " @ %message @ ")");

	for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl)) {
		if(%cl != %except)
			Client::sendMessage(%cl, %mtype, %message);
	}
}

function radiusAllExcept(%except1, %except2, %message, %battle) {
	dbecho($dbechoMode, "radiusAllExcept(" @ %except1 @ ", " @ %except2 @ ", " @ %message @ ")");

	%epos1 = GameBase::getPosition(%except1);
	%epos2 = GameBase::getPosition(%except2);
	for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl)) {
		%clpos = GameBase::getPosition(%cl);
		%dist1 = Vector::getDistance(%clpos, %epos1);
		%dist2 = Vector::getDistance(%clpos, %epos2);
		if(%cl != %except1 && %cl != %except2 && !IsDead(%cl)) {
			if(%dist1 <= $maxSAYdistVec || %dist2 <= $maxSAYdistVec) {				if(%battle)	newprintmsg(%cl,%message, $MsgBeige);				else		Client::sendMessage(%cl, $MsgBeige, %message);			}
		}
	}
}
function newprintmsg(%cl,%message, %colour){
	if(Player::isAiControlled(%cl) || %cl < 2048)
		return;	%tag = string::printcolortag(%colour);	%message = string::replaceall(%message,"<ff>",%tag);	if(fetchData(%cl, "battlemsg") == "chathud"){		%message = String::replaceAll(%message, "<f0>", "");		%message = String::replaceAll(%message, "<f1>", "");
		%message = String::replaceAll(%message, "<f2>", "");
		%message = String::replaceAll(%message, "<f3>", "");
		%message = String::replaceAll(%message, "<f4>", "");
		%message = String::replaceAll(%message, "<f5>", "");		Client::sendMessage(%cl, %colour, %message);		return;	}	%ctime = string::getsubstr(timestamp(),11,8);	%message = %ctime @ ": " @ %message;
	if(%cl.repack < 9){		if(%cl.printlength == 1){
			%cl.lastBMessage3 = "";%cl.lastBMessage2 = "";			if(fetchData(%cl, "battlemsg") == "topprint")	remoteEval(%cl, "TP", %message, 10);			else											remoteEval(%cl, "BP", %message, 10);		}		else {			if(fetchData(%cl, "battlemsg") == "topprint")	remoteEval(%cl, "TP", %cl.lastBMessage3 @ %cl.lastBMessage2 @ %cl.lastBMessage @ %message, 10);			else											remoteEval(%cl, "BP", %cl.lastBMessage3 @ %cl.lastBMessage2 @ %cl.lastBMessage @ %message, 10);			%cl.lastBMessage3 = %cl.lastBMessage2;			%cl.lastBMessage2 = %cl.lastBMessage;		}
		%cl.lastBMessage = %message @ "\n";	}
	else {
		if(%cl.repack >= 32){			remoteEval(%cl,HUDConsolePrint,%message,10);			return;		}
		if(%cl.printlength == "")						%cl.printlength = 10;		if(fetchData(%cl, "battlemsg") == "topprint")	remoteEval(%cl,BufferedConsolePrint,%message@"\n",10,2,%cl.printlength);		else											remoteEval(%cl,BufferedConsolePrint,%message@"\n",10,1,%cl.printlength);
	}}

function FadeMsg(%txt, %dist, %max) {
	dbecho($dbechoMode, "FadeMsg(" @ %txt @ ", " @ %dist @ ", " @ %max @ ")");

	if(%dist <= %max) return %txt;
	else {
		for(%i = 0; (%z = GetWord(%txt, %i)) != -1; %i++)
			%ntxt = %ntxt @ %z;
		%lntxt = String::len(%ntxt);
		%x = %dist - %max;
		%amt = round((%x / %max) * %lntxt);
		%txt = BuildDotString(%txt, %amt);
		
		return %txt;
	}
}

function BuildDotString(%txt, %n) {
	dbecho($dbechoMode, "BuildDotString(" @ %txt @ ", " @ %n @ ")");

	%len = String::len(%txt);
	%retry = 0;
	for(%i = %n; %i > 0; %i) {
		%p = floor(getRandom() * %len);
		%a = String::getSubStr(%txt, %p, 1);
		if(%a != " " && %a != ".") {
			%txt = String::getSubStr(%txt, 0, %p) @ "." @ String::getSubStr(%txt, %p+1, 99999);
			%i--;
			%retry = 0;
		}
		else %retry++;

		if(%retry > 10)
			break;
	}
	return %txt;
}

function ClearBlockData(%name, %block) {
	dbecho($dbechoMode, "ClearBlockData(" @ %name @ ", " @ %block @ ")");
	for(%i = 1; $BlockData[%name, %block, %i] != ""; %i++)
		$BlockData[%name, %block, %i] = "";
}

function ManageBlockOwnersList(%name) {	dbecho($dbechoMode, "ManageBlockOwnersList(" @ %name @ ")");	%clientId = NEWgetClientByName(%name);	if(CountObjInCommaList($BlockList[%name]) > 0) {		if(!IsInCommaList($BlockOwnersList, %name)) {			$BlockOwnersList = AddToCommaList($BlockOwnersList, %name);			if(%name != "Server")				$BlockOwnerAdminLevel[%name] = floor(%clientId.adminLevel);		}	} else {		$BlockOwnersList = RemoveFromCommaList($BlockOwnersList, %name);		if(%name != "Server")			$BlockOwnerAdminLevel[%name] = "";	}}

function ParseBlockData(%bd, %victimId, %killerId) {
	dbecho($dbechoMode, "ParseBlockData(" @ %bd @ ", " @ %victimId @ ", " @ %killerId @ ")");

	//the passed variables MUST BE IN COMMALIST FORMAT!

	%vtype[1] = "^victimName";
	%vtype[2] = "^victimId";
	%vtype[3] = "^victimPos";
	%vtype[4] = "^victimRot";
	%vtype[5] = "^victimZoneId";
	%vtype[6] = "^victimZoneType";
	%vtype[7] = "^victimZoneDesc";
	%vtype[8] = "^victimClass";
	%vtype[9] = "^victimLevel";
	%vtype[10] = "^victimX";
	%vtype[11] = "^victimY";
	%vtype[12] = "^victimZ";
	%vtype[13] = "^victimR1";
	%vtype[14] = "^victimR2";
	%vtype[15] = "^victimR3";
	%vtype[16] = "^victimCoins";
	%vtype[17] = "^victimBank";
	%vtype[18] = "^victimVelX";
	%vtype[19] = "^victimVelY";
	%vtype[20] = "^victimVelZ";

	%vtype[21] = "^killerName";
	%vtype[22] = "^killerId";
	%vtype[23] = "^killerPos";
	%vtype[24] = "^killerRot";
	%vtype[25] = "^killerZoneId";
	%vtype[26] = "^killerZoneType";
	%vtype[27] = "^killerZoneDesc";
	%vtype[28] = "^killerClass";
	%vtype[29] = "^killerLevel";
	%vtype[30] = "^killerX";
	%vtype[31] = "^killerY";
	%vtype[32] = "^killerZ";
	%vtype[33] = "^killerR1";
	%vtype[34] = "^killerR2";
	%vtype[35] = "^killerR3";
	%vtype[36] = "^killerCoins";
	%vtype[37] = "^killerBank";
	%vtype[38] = "^killerVelX";
	%vtype[39] = "^killerVelY";
	%vtype[40] = "^killerVelZ";

	if(%victimId != "")
	{
		%vpos = GameBase::getPosition(%victimId);
		%vrot = GameBase::getRotation(%victimId);
		%vvel = Item::getVelocity(%victimId);

		%var[1] = Client::getName(%victimId);
		%var[2] = %victimId;
		%var[3] = %vpos;
		%var[4] = %vrot;
		%var[5] = fetchData(%victimId, "zone");
		%var[6] = Zone::getType(fetchData(%victimId, "zone"));
		%var[7] = Zone::getDesc(fetchData(%victimId, "zone"));
		%var[8] = fetchData(%victimId, "CLASS");
		%var[9] = fetchData(%victimId, "LVL");
		%var[10] = GetWord(%vpos, 0);
		%var[11] = GetWord(%vpos, 1);
		%var[12] = GetWord(%vpos, 2);
		%var[13] = GetWord(%vrot, 0);
		%var[14] = GetWord(%vrot, 1);
		%var[15] = GetWord(%vrot, 2);
		%var[16] = fetchData(%victimId, "COINS");
		%var[17] = fetchData(%victimId, "BANK");
		%var[18] = GetWord(%vvel, 0);
		%var[19] = GetWord(%vvel, 1);
		%var[20] = GetWord(%vvel, 2);
	}
	if(%killerId != "")
	{
		%kpos = GameBase::getPosition(%killerId);
		%krot = GameBase::getRotation(%killerId);
		%kvel = Item::getVelocity(%killerId);

		%var[21] = Client::getName(%killerId);
		%var[22] = %killerId;
		%var[23] = %kpos;
		%var[24] = %krot;
		%var[25] = fetchData(%killerId, "zone");
		%var[26] = Zone::getType(fetchData(%killerId, "zone"));
		%var[27] = Zone::getDesc(fetchData(%killerId, "zone"));
		%var[28] = fetchData(%killerId, "CLASS");
		%var[29] = fetchData(%killerId, "LVL");
		%var[30] = GetWord(%kpos, 0);
		%var[31] = GetWord(%kpos, 1);
		%var[32] = GetWord(%kpos, 2);
		%var[33] = GetWord(%krot, 0);
		%var[34] = GetWord(%krot, 1);
		%var[35] = GetWord(%krot, 2);
		%var[36] = fetchData(%killerId, "COINS");
		%var[37] = fetchData(%killerId, "BANK");
		%var[38] = GetWord(%kvel, 0);
		%var[39] = GetWord(%kvel, 1);
		%var[40] = GetWord(%kvel, 2);
	}

	for(%i = 1; %vtype[%i] != ""; %i++)
		%bd = String::replace(%bd, %vtype[%i], %var[%i], True);

	return %bd;
}

$hex["00"] = "\x00"; $hex["01"] = "\x01"; $hex["02"] = "\x02"; $hex["03"] = "\x03";$hex["04"] = "\x04"; $hex["05"] = "\x05"; $hex["06"] = "\x06"; $hex["07"] = "\x07";$hex["08"] = "\x08"; $hex["09"] = "\x09"; $hex["0A"] = "\x0A"; $hex["0B"] = "\x0B";$hex["0C"] = "\x0C"; $hex["0D"] = "\x0D"; $hex["0E"] = "\x0E"; $hex["0F"] = "\x0F";$hex["10"] = "\x10"; $hex["11"] = "\x11"; $hex["12"] = "\x12"; $hex["13"] = "\x13";$hex["14"] = "\x14"; $hex["15"] = "\x15"; $hex["16"] = "\x16"; $hex["17"] = "\x17";$hex["18"] = "\x18"; $hex["19"] = "\x19"; $hex["1A"] = "\x1A"; $hex["1B"] = "\x1B";$hex["1C"] = "\x1C"; $hex["1D"] = "\x1D"; $hex["1E"] = "\x1E"; $hex["1F"] = "\x1F";$hex["20"] = "\x20"; $hex["21"] = "\x21"; $hex["22"] = "\x22"; $hex["23"] = "\x23";$hex["24"] = "\x24"; $hex["25"] = "\x25"; $hex["26"] = "\x26"; $hex["27"] = "\x27";$hex["28"] = "\x28"; $hex["29"] = "\x29"; $hex["2A"] = "\x2A"; $hex["2B"] = "\x2B";$hex["2C"] = "\x2C"; $hex["2D"] = "\x2D"; $hex["2E"] = "\x2E"; $hex["2F"] = "\x2F";$hex["30"] = "\x30"; $hex["31"] = "\x31"; $hex["32"] = "\x32"; $hex["33"] = "\x33";$hex["34"] = "\x34"; $hex["35"] = "\x35"; $hex["36"] = "\x36"; $hex["37"] = "\x37";$hex["38"] = "\x38"; $hex["39"] = "\x39"; $hex["3A"] = "\x3A"; $hex["3B"] = "\x3B";$hex["3C"] = "\x3C"; $hex["3D"] = "\x3D"; $hex["3E"] = "\x3E"; $hex["3F"] = "\x3F";$hex["40"] = "\x40"; $hex["41"] = "\x41"; $hex["42"] = "\x42"; $hex["43"] = "\x43";$hex["44"] = "\x44"; $hex["45"] = "\x45"; $hex["46"] = "\x46"; $hex["47"] = "\x47";$hex["48"] = "\x48"; $hex["49"] = "\x49"; $hex["4A"] = "\x4A"; $hex["4B"] = "\x4B";$hex["4C"] = "\x4C"; $hex["4D"] = "\x4D"; $hex["4E"] = "\x4E"; $hex["4F"] = "\x4F";$hex["50"] = "\x50"; $hex["51"] = "\x51"; $hex["52"] = "\x52"; $hex["53"] = "\x53";$hex["54"] = "\x54"; $hex["55"] = "\x55"; $hex["56"] = "\x56"; $hex["57"] = "\x57";$hex["58"] = "\x58"; $hex["59"] = "\x59"; $hex["5A"] = "\x5A"; $hex["5B"] = "\x5B";$hex["5C"] = "\x5C"; $hex["5D"] = "\x5D"; $hex["5E"] = "\x5E"; $hex["5F"] = "\x5F";$hex["60"] = "\x60"; $hex["61"] = "\x61"; $hex["62"] = "\x62"; $hex["63"] = "\x63";$hex["64"] = "\x64"; $hex["65"] = "\x65"; $hex["66"] = "\x66"; $hex["67"] = "\x67";$hex["68"] = "\x68"; $hex["69"] = "\x69"; $hex["6A"] = "\x6A"; $hex["6B"] = "\x6B";$hex["6C"] = "\x6C"; $hex["6D"] = "\x6D"; $hex["6E"] = "\x6E"; $hex["6F"] = "\x6F";$hex["70"] = "\x70"; $hex["71"] = "\x71"; $hex["72"] = "\x72"; $hex["73"] = "\x73";$hex["74"] = "\x74"; $hex["75"] = "\x75"; $hex["76"] = "\x76"; $hex["77"] = "\x77";$hex["78"] = "\x78"; $hex["79"] = "\x79"; $hex["7A"] = "\x7A"; $hex["7B"] = "\x7B";$hex["7C"] = "\x7C"; $hex["7D"] = "\x7D"; $hex["7E"] = "\x7E"; $hex["7F"] = "\x7F";$hex["80"] = "\x80"; $hex["81"] = "\x81"; $hex["82"] = "\x82"; $hex["83"] = "\x83";$hex["84"] = "\x84"; $hex["85"] = "\x85"; $hex["86"] = "\x86"; $hex["87"] = "\x87";$hex["88"] = "\x88"; $hex["89"] = "\x89"; $hex["8A"] = "\x8A"; $hex["8B"] = "\x8B";$hex["8C"] = "\x8C"; $hex["8D"] = "\x8D"; $hex["8E"] = "\x8E"; $hex["8F"] = "\x8F";$hex["90"] = "\x90"; $hex["91"] = "\x91"; $hex["92"] = "\x92"; $hex["93"] = "\x93";$hex["94"] = "\x94"; $hex["95"] = "\x95"; $hex["96"] = "\x96"; $hex["97"] = "\x97";$hex["98"] = "\x98"; $hex["99"] = "\x99"; $hex["9A"] = "\x9A"; $hex["9B"] = "\x9B";$hex["9C"] = "\x9C"; $hex["9D"] = "\x9D"; $hex["9E"] = "\x9E"; $hex["9F"] = "\x9F";$hex["A0"] = "\xA0"; $hex["A1"] = "\xA1"; $hex["A2"] = "\xA2"; $hex["A3"] = "\xA3";$hex["A4"] = "\xA4"; $hex["A5"] = "\xA5"; $hex["A6"] = "\xA6"; $hex["A7"] = "\xA7";$hex["A8"] = "\xA8"; $hex["A9"] = "\xA9"; $hex["AA"] = "\xAA"; $hex["AB"] = "\xAB";$hex["AC"] = "\xAC"; $hex["AD"] = "\xAD"; $hex["AE"] = "\xAE"; $hex["AF"] = "\xAF";$hex["B0"] = "\xB0"; $hex["B1"] = "\xB1"; $hex["B2"] = "\xB2"; $hex["B3"] = "\xB3";$hex["B4"] = "\xB4"; $hex["B5"] = "\xB5"; $hex["B6"] = "\xB6"; $hex["B7"] = "\xB7";$hex["B8"] = "\xB8"; $hex["B9"] = "\xB9"; $hex["BA"] = "\xBA"; $hex["BB"] = "\xBB";$hex["BC"] = "\xBC"; $hex["BD"] = "\xBD"; $hex["BE"] = "\xBE"; $hex["BF"] = "\xBF";$hex["C0"] = "\xC0"; $hex["C1"] = "\xC1"; $hex["C2"] = "\xC2"; $hex["C3"] = "\xC3";$hex["C4"] = "\xC4"; $hex["C5"] = "\xC5"; $hex["C6"] = "\xC6"; $hex["C7"] = "\xC7";$hex["C8"] = "\xC8"; $hex["C9"] = "\xC9"; $hex["CA"] = "\xCA"; $hex["CB"] = "\xCB";$hex["CC"] = "\xCC"; $hex["CD"] = "\xCD"; $hex["CE"] = "\xCE"; $hex["CF"] = "\xCF";$hex["D0"] = "\xD0"; $hex["D1"] = "\xD1"; $hex["D2"] = "\xD2"; $hex["D3"] = "\xD3";$hex["D4"] = "\xD4"; $hex["D5"] = "\xD5"; $hex["D6"] = "\xD6"; $hex["D7"] = "\xD7";$hex["D8"] = "\xD8"; $hex["D9"] = "\xD9"; $hex["DA"] = "\xDA"; $hex["DB"] = "\xDB";$hex["DC"] = "\xDC"; $hex["DD"] = "\xDD"; $hex["DE"] = "\xDE"; $hex["DF"] = "\xDF";$hex["E0"] = "\xE0"; $hex["E1"] = "\xE1"; $hex["E2"] = "\xE2"; $hex["E3"] = "\xE3";$hex["E4"] = "\xE4"; $hex["E5"] = "\xE5"; $hex["E6"] = "\xE6"; $hex["E7"] = "\xE7";$hex["E8"] = "\xE8"; $hex["E9"] = "\xE9"; $hex["EA"] = "\xEA"; $hex["EB"] = "\xEB";$hex["EC"] = "\xEC"; $hex["ED"] = "\xED"; $hex["EE"] = "\xEE"; $hex["EF"] = "\xEF";$hex["F0"] = "\xF0"; $hex["F1"] = "\xF1"; $hex["F2"] = "\xF2"; $hex["F3"] = "\xF3";$hex["F4"] = "\xF4"; $hex["F5"] = "\xF5"; $hex["F6"] = "\xF6"; $hex["F7"] = "\xF7";$hex["F8"] = "\xF8"; $hex["F9"] = "\xF9"; $hex["FA"] = "\xFA"; $hex["FB"] = "\xFB";$hex["FC"] = "\xFC"; $hex["FD"] = "\xFD"; $hex["FE"] = "\xFE"; $hex["FF"] = "\xFF";function bytetohex(%decimal) {	%hex[ 0] = "0"; %hex[ 1] = "1"; %hex[ 2] = "2"; %hex[ 3] = "3";	%hex[ 4] = "4"; %hex[ 5] = "5"; %hex[ 6] = "6"; %hex[ 7] = "7";	%hex[ 8] = "8"; %hex[ 9] = "9"; %hex[10] = "A"; %hex[11] = "B";	%hex[12] = "C"; %hex[13] = "D"; %hex[14] = "E"; %hex[15] = "F";    %b = floor(%decimal / 16);    %r = %decimal % 16;    %value = %hex[%b] @ %hex[%r];    return %value;}function charCodeAt(%original, %pos){	%char = String::getSubStr(%original,%pos,1);	for(%i = 1; %i < 256; %i++) {		%h = bytetohex(%i);		if(String::Compare(%char, $hex[%h]) == 0)			return %i;	}}

//font translation stuff written by phantom//tribesrpg.orgfunction generateCharCodes(){	for(%i = 32; %i < 256; %i++){		$char[%i] = $hex[bytetohex(%i)];	}}generateCharCodes();
function charTranslate(%char){	for(%i = 33; %i < 127; %i++) {		if(String::Compare(%char, $char[%i]) == 0)			return $char[%i+94];	}	return %char;}function string::translate(%msg){	%final = "";	for(%i;(%char = String::getSubStr(%msg,%i,1))!="";%i++){		%c = charTranslate(%char);		%final = %final @ %c;	}	return %final;}
//yellow textfunction charTranslate2(%char){	for(%i = 64; %i < 100; %i++) {		if(String::Compare(%char, $char[%i]) == 0)			return $char[%i+94+62];	}	return %char;}//yellow text//converts from://ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`ab//converts to://ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]:#()-//Other characters will not work. Could only fit this many.//Only works in chat hud fonts. Other fonts are using other char sets.function string::translate2(%msg){	%final = "";	for(%i;(%char = String::getSubStr(%msg,%i,1))!="";%i++){		%c = charTranslate2(%char);		%final = %final @ %c;	}	return %final;}
//written by phantom, tribesrpg.org//allows use of <f3> through <f5> in huds like bottomprints for extra colours.//Anyone who views them requires the fonts from rpgfonts.vol//requires function string::translate(%msg)function string::newPrintFormat(%msg){	%cont = True;	%translate = False;	while(%cont){		%pos = string::findsubstr(%msg,"<f");		if(%pos == -1){			if(%translate)				%msg = string::translate(%msg);			return %finalMsg @ %msg;		}		if(%translate){			%translate = False;			%trans = string::translate(string::getsubstr(%msg,0,%pos));			%finalMsg = %finalMsg @ %trans;			%msg = string::NEWgetsubstr(%msg,%pos,5000);		}		else{			%finalMsg = %finalMsg @ string::getsubstr(%msg,0,%pos);			%n = string::getsubstr(%msg,%pos+2,1);			if(%n > 2){				%tag = "<f"@(%n-3)@">";				%translate = True;			}			else				%tag = "<f"@%n@">";			%finalMsg = %finalMsg @ %tag;			%msg = string::NEWgetsubstr(%msg,%pos+4,5000);		}		%error++;		if(%error > 100)			return %finalMsg;	}	return %finalMsg;}
//By phantom, tribesrpg.org
//added in v6.7.2
function string::printcolortag(%colour){	if(%colour == $msgOrange)	return "<f0>";	if(%colour == $msgBeige)	return "<f1>";	if(%colour == $msgWhite)	return "<f2>";	if(%colour == $msgGreen)	return "<f3>";	if(%colour == $msgBlue)		return "<f4>";	if(%colour == $msgRed)		return "<f5>";}

//By phantom, tribesrpg.org
//For use from console, not for writing into code.
function msg(%msg) {
	if(string::compare(%msg, "") == 0) {
		pecho("Allows speaking to players. ex: msg(\"Hi players!\");");
	} else {
		pecho("MSG - Console: " @ %msg);		for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl)){			if(%cl.alttext) client::sendMessage(%cl, $MsgGreen, string::translate2("[G]") @ " Console - " @ %msg, True);			else			client::sendMessage(%cl, $MsgGreen, "[G] Console - " @ %msg, True);		}
	}
}
