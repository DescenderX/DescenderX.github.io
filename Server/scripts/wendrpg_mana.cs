//This file is part of Tribes RPG.
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

//__________________________________________________________________________________________________________________________________
// DescX Notes:
//		New THIRST system.
//		Over time, you get thirsty. This impacts your MANA regeneration significantly.
//		The more powerful you are, the thirstier you get. 
//		Too thirsty? NEGATIVE regen! Idlenomore... bringwater...

function setMANA(%clientId, %val) {
	dbecho($dbechoMode, "setMANA(" @ %clientId @ ", " @ %val @ ")");
	%armor = Player::getArmor(%clientId);
	if(%val == "")
		%val = fetchData(%clientId, "MaxMANA");

	%a = %val * %armor.maxEnergy;
	%b = %a / fetchData(%clientId, "MaxMANA");

	if(%b < 0)						%b = 0;
	else if(%b > %armor.maxEnergy)	%b = %armor.maxEnergy;

	GameBase::setEnergy(Client::getOwnedObject(%clientId), %b);
}

function refreshMANA(%clientId, %value)
{
	dbecho($dbechoMode, "refreshMANA(" @ %clientId @ ", " @ %value @ ")");

	if(AddBonusStatePoints(%clientId,"SCRFC") > 0) {
		GameBase::setRechargeRate(Client::getOwnedObject(%clientId), 0);
		setMANA(%clientId, 0);
	} else {
		setMANA(%clientId, (fetchData(%clientId, "MANA") - %value));
	}
}


function GetManaRegenRate(%clientId) {
	dbecho($dbechoMode, "refreshMANAREGEN(" @ %clientId @ ")");

	%regenBuff = AddBonusStatePoints(%clientId, "WARMTH") / 400;
	if(AddBonusStatePoints(%clientId,"SCRFC") > 0) {
		return 0;
	} else {
		%isAi = Player::isAiControlled(%clientId);
		if(%isAi)
			%a = GetSkillWithBonus(%clientId, $SkillWillpower) * 10;
		else %a = (GetSkillWithBonus(%clientId, $SkillWillpower) / 3250);		
		
		if(AddBonusStatePoints(%clientId,"TOTEM") > 0)
			%a += Cap(GetSkillWithBonus(%clientId, $SkillSurvival) / 123, 2, 10);
		
		if(%clientId.sleepMode == 1)			%b = 1.0 + %a;
		else if(%clientId.sleepMode == 2)		%b = 2.25 + %a;
		else									%b = %a;
		%c = AddPoints(%clientId, 11) / 800;
		%r = %b + %c + %regenBuff;
		
		if(!%isAi && %clientId.sleepMode < 1 && %regenBuff <= 0) {
			%thirst = Cap(fetchData(%clientId, "Thirst") / 300, -2, 2);
			%r *= %thirst;
		}
		return %r;
	}
}

function refreshMANAREGEN(%clientId) {
	dbecho($dbechoMode, "refreshMANAREGEN(" @ %clientId @ ")");
	GameBase::setRechargeRate(Client::getOwnedObject(%clientId), GetManaRegenRate(%clientId));
}

