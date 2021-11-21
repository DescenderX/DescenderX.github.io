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
//		Over time, you get thirsty. This impacts your HP regeneration moderately.
//		Most classes won't lose much, but some like the Cleric who depend on high regen will suffer without water

function setHP(%clientId, %val, %dontSet)
{
	dbecho($dbechoMode, "setHP(" @ %clientId @ ", " @ %val @ ")");

	%armor = Player::getArmor(%clientId);

	if(%val < 0)
		%val = 0;
	if(%val == "")
		%val = fetchData(%clientId, "MaxHP");

	%a = %val * %armor.maxDamage;
	%b = %a / fetchData(%clientId, "MaxHP");
	%c = %armor.maxDamage - %b;

	if(%c < 0)
		%c = 0;
	else if(%c > %armor.maxDamage)
		%c = %armor.maxDamage;

	if(%c == %armor.maxDamage && !IsStillArenaFighting(%clientId)) {
		%guardian = AddBonusStatePoints(%clientId, "FAVOR");
		if(%guardian > 0) {
			Client::sendMessage(%clientId, $MsgRed, "A Guardian Angel saves you from death...");
			%c = GameBase::getDamageLevel(Client::getOwnedObject(%clientId));
			%val = -1;
			if(GetExactBonusValue(%clientId, "FAVOR 1") >= 1)		AddExactBonusValue(%clientId, "FAVOR 1", -1);
			else if(GetExactBonusValue(%clientId, "FAVOR 2") >= 1)	AddExactBonusValue(%clientId, "FAVOR 2", -1);
			else													AddExactBonusValue(%clientId, "FAVOR 3", -1);
			
			%bound1 = GetExactBonusValue(%clientId, "FAVOR 1");
			%bound2 = GetExactBonusValue(%clientId, "FAVOR 2");
			%bound3 = GetExactBonusValue(%clientId, "FAVOR 3");		
			
			if(%dontSet == "") {
				if(%bound1 >= 0.0 && %bound1 < 1.0 ) UpdateBonusState(%clientId, "FAVOR 1", 0, 0);
				if(%bound2 >= 0.0 && %bound2 < 1.0 ) UpdateBonusState(%clientId, "FAVOR 2", 0, 0);
				if(%bound3 >= 0.0 && %bound3 < 1.0 ) UpdateBonusState(%clientId, "FAVOR 3", 0, 0);
			}
			
		} else {
			if(!rpg::PlayerIsEqualizer(%clientId) || fetchData(%clientId, "FAVORmode") == "miss") {
				if(%dontSet == "") {				
					storeData(%clientId, "FAVOR", 1, "dec");				
					if(fetchData(%clientId, "FAVOR") >= 0) {					
						Client::sendMessage(%clientId, $MsgRed, "You have permanently lost FAVOR!");
						if(fetchData(%clientId, "FAVORmode") == "miss") 			{
							%c = GameBase::getDamageLevel(Client::getOwnedObject(%clientId));
							%val = -1;
						}
					}				
				} else {
					if(fetchData(%clientId, "FAVOR") - 1 >= 0 && fetchData(%clientId, "FAVORmode") == "miss")
						%val = -1;
				}
			}
		}		
	}

	if(%dontSet == "") {
		GameBase::setDamageLevel(Client::getOwnedObject(%clientId), %c);
		return %val;
	} else return %c;
}

function refreshHP(%clientId, %value) {
	dbecho($dbechoMode, "refreshHP(" @ %clientId @ ", " @ %value @ ")");
	return setHP(%clientId, fetchData(%clientId, "HP") - round(%value * $TribesDamageToNumericDamage));
}


function rpg::DoesHPRefreshCauseDeath(%clientId, %value) {
	dbecho($dbechoMode, "refreshHP(" @ %clientId @ ", " @ %value @ ")");
	return setHP(%clientId, fetchData(%clientId, "HP") - round(%value * $TribesDamageToNumericDamage), true);
}

function refreshHPREGEN(%clientId) {
	dbecho($dbechoMode, "refreshHPREGEN(" @ %clientId @ ")");

	%regenBuff = AddBonusStatePoints(%clientId, "HPR") / 250;
	if(AddBonusStatePoints(%clientId,"TOTEM") > 0) {
		%r = 0;
	} else {		
		%a = GetSkillWithBonus(%clientId, $SkillEndurance) / 250000;
		if(%clientId.sleepMode == 1)		%b = %a + 0.0200;
		else if(%clientId.sleepMode == 2)	%b = %a;
		else								%b = %a;
		%c = AddPoints(%clientId, 10) / 2000;
		%r = %b + %c + %regenBuff;		
	}
	
	%isAi = Player::isAiControlled(%clientId);
	if(!%isAi && %clientId.sleepMode < 1 && %regenBuff <= 0) {
		%thirst = Cap(fetchData(%clientId, "Thirst") / 300, -2, 2);
		%r *= %thirst;
	}
	
	GameBase::setAutoRepairRate(Client::getOwnedObject(%clientId), %r);
}
