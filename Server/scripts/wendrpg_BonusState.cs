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

//____________________________________________________________________________________________________________________________________
// DescX Notes:
//		Bonus States have become a first-class citizen; proper "buffs"
//		A Bonus NAME has two parts:		Name	ID		where ID can be different, allowing one buff type to "stack" if desired
//		Buffs will be randomly removed to make way for new ones.
//		Buffs print nothing when they are applied, but always show a cleanup message when removed.
//
$maxBonusStates = 10;

$BonusStateName[ASU] 		= "Echo";
$BonusStateName[MDEF] 		= "Resistance";
$BonusStateName[DEF] 		= "Armor";
$BonusStateName[BLEED] 		= "Bleeding";
$BonusStateName[DMG] 		= "Damage";
$BonusStateName[ATK] 		= "Attack";
$BonusStateName[SPD] 		= "Movement";
$BonusStateName[FLIGHT] 	= "Flying";
$BonusStateName[Evasion] 	= "Evasion";
$BonusStateName[Strength] 	= "Strength";
$BonusStateName[Energy] 	= "Energy";
$BonusStateName[Endurance] 	= "Endurance";
$BonusStateName[Focus] 		= "Focus";
$BonusStateName[Luck] 		= "Luck";
$BonusStateName[CHARM] 		= "Charm";
$BonusStateName[THORN] 		= "Thorn";
$BonusStateName[HPR] 		= "Regeneration";
$BonusStateName[WARMTH] 	= "Warmth";
$BonusStateName[FAVOR] 		= "Guardian";
$BonusStateName[ACRO] 		= "Acrobatics";
$BonusStateName[XPLT] 		= "Exploit";
$BonusStateName[TOTEM] 		= "Totem";
$BonusStateName[SCRFC] 		= "Sacrifice";
$BonusStateName[BRACE] 		= "Perfect Block";
$BonusStateName[WEIGHT] 	= "Weight";
$BonusStateName[PTFY] 		= "Petrification";
$BonusStateName[BREATH] 	= "Aqualung";
$BonusStateName[REFLECT] 	= "Reflect";
$BonusStateName[EXP] 		= "Experience";



function CleanupBonus(%clientId, %b, %dontEchoCleanup) {
	%name = GetWord($BonusState[%clientId, %b], 0);

	$BonusStateCnt[%clientId, %b] = "";
	$BonusStateVal[%clientId, %b] = "";
	$BonusState[%clientId, %b] = "";
	
	if(string::compare(%name, "SPD") == 0 ||
	   string::compare(%name, "PTFY") == 0 ||
	   string::compare(%name, "FLIGHT") == 0 ||
	   string::compare(%name, "WEIGHT") == 0) {
		RefreshWeight(%clientId);
	} else if(string::compare(%name, "HPR") == 0) {
		refreshHPREGEN(%clientId);
	} else if(string::compare(%name, "SCRFC") == 0) {
		refreshMANAREGEN(%clientId);
		refreshHP(%clientId);
	} else if(string::compare(%name, "WARMTH") == 0) {
		refreshMANAREGEN(%clientId);
	}
	
	if(%dontEchoCleanup != true) {
		Client::sendMessage(%clientId, $MsgBeige, $BonusStateName[%name] @ " effects have worn off...");			
		playSound(BonusStateExpire, GameBase::getPosition(%clientId));
	}
}

function DecreaseBonusStateTicks(%clientId, %b) {
	if(%b != "") {
		$BonusStateCnt[%clientId, %b]--;
		if($BonusStateCnt[%clientId, %b] <= 0) {	
			CleanupBonus(%clientId, %b);
		}
	} else {
		%totalbcnt = 0;
		%truebcnt = 0;

		//Decrease all ticks for that player
		for(%i = 1; %i <= $maxBonusStates; %i++) {
			if($BonusStateCnt[%clientId, %i] > 0) {
				$BonusStateCnt[%clientId, %i]--;
				if($BonusStateCnt[%clientId, %i] <= 0) {
					CleanupBonus(%clientId, %i);
				} else {
					%totalbcnt++;					
					%truebcnt++;
				}
			}
		}

		if(%truebcnt > 0)	storeData(%clientId, "isBonused", True);
		else				storeData(%clientId, "isBonused", "");			
	}
}

function AddBonusStatePoints(%clientId, %filter)
{
	%add = 0;
	for(%i = 1; %i <= $maxBonusStates; %i++) {
		if($BonusStateCnt[%clientId, %i] > 0) {
			for(%z = 0; (%p1 = GetWord($BonusState[%clientId, %i], %z)) != -1; %z+=2) {
				%p2 = GetWord($BonusState[%clientId, %i], %z+1);
				if(String::ICompare(%p1, %filter) == 0) {
					%add += $BonusStateVal[%clientId, %i];
				}
			}
		}
	}

	return %add;
}

function UpdateBonusState(%clientId, %type, %ticks, %value, %dontEchoCleanup) {
	//look thru the current bonus states and attempt to update
	for(%i = 1; %i <= $maxBonusStates; %i++) {
		if($BonusStateCnt[%clientId, %i] > 0) {
			if(String::ICompare($BonusState[%clientId, %i], %type) == 0) {
				if(%ticks <= 0) {
					CleanupBonus(%clientId, %i, %dontEchoCleanup);
				} else {
					$BonusStateCnt[%clientId, %i] = %ticks;
					$BonusStateVal[%clientId, %i] = %value;
				}
				return True;
			}
		}
	}
	//couldn't find a current entry to update, so make a new entry
	for(%i = 1; %i <= $maxBonusStates; %i++) {
		if( !($BonusStateCnt[%clientId, %i] > 0) ) {
			$BonusState[%clientId, %i] = %type;
			$BonusStateCnt[%clientId, %i] = %ticks;
			$BonusStateVal[%clientId, %i] = %value;
			return True;
		}
	}
	
	// still couldn't add the buff? evict an old one at random
	%evictState = (floor(getRandom() * 1000) % $maxBonusStates);
	CleanupBonus(%clientId, %evictState, %dontEchoCleanup);
	$BonusState[%clientId, %evictState]    = %type;
	$BonusStateCnt[%clientId, %evictState] = %ticks;
	$BonusStateVal[%clientId, %evictState] = %value;
	return True;
}


function GetBonusStateTicks(%clientId, %filter) {
	%add = 0;
	for(%i = 1; %i <= $maxBonusStates; %i++) {
		if($BonusStateCnt[%clientId, %i] > 0) {
			for(%z = 0; (%p1 = GetWord($BonusState[%clientId, %i], %z)) != -1; %z+=2) {
				%p2 = GetWord($BonusState[%clientId, %i], %z+1);
				if(String::ICompare(%p1, %filter) == 0) {
					%add += $BonusStateCnt[%clientId, %i];
				}
			}
		}
	}
	return %add;
}

function GetExactBonusValue(%clientId, %fullBonusID) {
	%add = 0;
	for(%i = 1; %i <= $maxBonusStates; %i++) {
		if($BonusStateCnt[%clientId, %i] > 0) {
			for(%z = 0; (%p1 = GetWord($BonusState[%clientId, %i], %z)) != -1; %z+=2) {
				%bonusID = %p1 @ " " @ GetWord($BonusState[%clientId, %i], %z+1);				
				if(String::ICompare(%bonusID, %fullBonusID) == 0) {
					%add += $BonusStateVal[%clientId, %i];
				}
			}
		}
	}
	return %add;
}


function GetExactBonusTicks(%clientId, %fullBonusID) {
	%add = 0;
	for(%i = 1; %i <= $maxBonusStates; %i++) {
		if($BonusStateCnt[%clientId, %i] > 0) {
			for(%z = 0; (%p1 = GetWord($BonusState[%clientId, %i], %z)) != -1; %z+=2) {
				%bonusID = %p1 @ " " @ GetWord($BonusState[%clientId, %i], %z+1);				
				if(String::ICompare(%bonusID, %fullBonusID) == 0) {
					%add += $BonusStateCnt[%clientId, %i];
				}
			}
		}
	}
	return %add;
}


function AddExactBonusValue(%clientId, %fullBonusID, %amount) {
	%add = 0;
	for(%i = 1; %i <= $maxBonusStates; %i++) {
		if($BonusStateCnt[%clientId, %i] > 0) {
			for(%z = 0; (%p1 = GetWord($BonusState[%clientId, %i], %z)) != -1; %z+=2) {
				%bonusID = %p1 @ " " @ GetWord($BonusState[%clientId, %i], %z+1);				
				if(String::ICompare(%bonusID, %fullBonusID) == 0) {
					$BonusStateVal[%clientId, %i] += %amount;
				}
			}
		}
	}
	return %add;
}


