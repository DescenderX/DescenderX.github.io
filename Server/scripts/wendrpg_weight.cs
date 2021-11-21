//This file is part of Tribes RPG.
//Tribes RPG server side scripts
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

//____________________________________________________________________________________________________________________________________
// DescX Notes:
//		RefreshWeight now sets both armor and skin, ensuring weight changes don't have broken code paths.
//		Model is set first. Skin second.
//		Armor and skin are determined by rpg::DefineArmorType and rpg::DefineEnemyType. 
function RefreshWeight(%clientId) {
	dbecho($dbechoMode2, "RefreshWeight(" @ %clientId @ ")");

	%player = Client::getOwnedObject(%clientId);
	%race 	= fetchData(%clientId, "RACE");
	%armor 	= -1;
	%shield	= -1;
	%list 	= GetAccessoryList(%clientId, 2, "3 7");
	for (%i = 0; (%w = getCroppedItem(GetWord(%list, %i))) != -1; %i++) {
		if($AccessoryVar[%w, $AccessoryType] == $BodyAccessoryType)
			%armor = %w;
		else if($AccessoryVar[%w, $AccessoryType] == $ShieldAccessoryType)
			%shield = %w;
	}
	
	if(Player::isAiControlled(%clientId)) {		// AI
		%newarmor = $RaceArmorType[%race];
	} else if(%race == "DeathKnight") {			// Administrator
		%newarmor = "DeathKnightArmor22";
	} else {									// Players
		if(!fetchData(%clientId, "SlowdownHitFlag") || %flightBonus > 0) {
			%flightBonus 		= AddBonusStatePoints(%clientId,"FLIGHT");
			%weight 			= fetchData(%clientId, "Weight");		
			%changeweightstep 	= 5;
			%newarmor 			= $ArmorForSpeed[%race, 0];
			%speedskillarmor 	= 0;
			%spill 				= %weight - fetchData(%clientId, "MaxWeight");
			%num 				= floor(%spill / %changeweightstep);

			if(%num > 0 && %flightBonus <= 0) {	//overweight, select appropriate armor
				for(%i = -1; %i >= -%num; %i--) {
					if($ArmorForSpeed[%race, %i] != "") {
						%newarmor = $ArmorForSpeed[%race, %i];
						%speedskillarmor = %i;
					}
					else break;
				}
			} else {							//when not overweight, the special armor-modifying items come in
				if(%flightBonus > 0)	%x = %flightBonus;
				else 					%x = $GetWeight::ArmorMod;
				if(%x > 0) {
					%newarmor = $ArmorForSpeed[%race, %x];
					%speedskillarmor = %x;
				}
			}			
			storeData(%clientId, "OverweightStep", %num);		// give penalties to other stats for being overweight
		}
		else {
			%newarmor = $ArmorForSpeed[%race, -5];
			%speedskillarmor = -5;
		}

		// Petrification will override speed bonuses, but still takes underlying weight into account.
		if(AddBonusStatePoints(%clientId, "PTFY") > 0) {	
			%newarmor = $ArmorForSpeed[%race, Cap(floor(getRandom() * -5), -7, %speedskillarmor)];
		} else {
			if(!IsDead(%clientId)){
				if(Player::getItemCount(%clientId,SlayerGear0) && %speedskillarmor > -6 && %speedskillarmor < 2) {
					%newarmor = $ArmorForSpeed[%race, 4];
				} else {
					%speedMult = AddBonusStatePoints(%clientId, "SPD");
					if(%speedMult > 0) {		
						if (%speedMult >= 4) 		%newarmor = $ArmorForSpeedSkill3[%race, %speedskillarmor];			
						else if (%speedMult >= 2) 	%newarmor = $ArmorForSpeedSkill2[%race, %speedskillarmor];
						else if (%speedMult >= 1) 	%newarmor = $ArmorForSpeedSkill1[%race, %speedskillarmor];					
					}
				}
			}
		}
		if($ArmorModelSuffix[%armor] != "")
			%newarmor = String::replace(%newarmor, "Armor", $ArmorModelSuffix[%armor] @ "Armor");
	}
	
	%a = Player::getArmor(%clientId);
	%ae = GameBase::getEnergy(%player);

	if(%a != %newarmor && %newarmor != "") {						// Set Armor
		Player::setArmor(%clientId, %newarmor);
		GameBase::setEnergy(%player, %ae);
	}
	
	// Now, set the skin.
	if(%race == "DeathKnight") {									// Admins
		%skinbase = "cphoenix";		
	} 
	else if(%race == "MaleHuman" || %race == "FemaleHuman") {		// Players	
		%skinbase = $ArmorSkin[%armor];		
		if(%skinbase == "") {
			%skinbase = $RaceSkin[%race];
			if(%skinbase == "") %skinbase = "rpgbase";
		}
	} else {														// AI
		%skinbase = $RaceSkin[%race];		
	}
	
	if(Client::getSkinBase(%clientId) != %skinbase) {
		Client::setSkin(%clientId, %skinbase);
	}

	//=================================
	// Update shields and Orb
	//=================================
	if(!IsDead(%clientId)){
		if(%shield != -1) {
			if(Player::getMountedItem(%clientId, 2) != %shield) {
				Player::unmountItem(%clientId, 2);
				Player::mountItem(%clientId, %shield, 2);
			}
		} else {
			if(Player::getMountedItem(%clientId, 2) != -1)
				Player::unmountItem(%clientId, 2);

			for(%i = 1; $ItemList[Orb, %i] != ""; %i++) {
				if(Player::getItemCount(%clientId, $ItemList[Orb, %i] @ "0"))
					Player::mountItem(%clientId, $ItemList[Orb, %i] @ "0", 2);
			}
		}
	}
}



//____________________________________________________________________________________________________________________________________
function GetWeight(%clientId) {
	dbecho($dbechoMode, "GetWeight(" @ %clientId @ ")");

	if(IsDead(%clientId) || !fetchData(%clientId, "HasLoadedAndSpawned") || %clientId.IsInvalid)
		return 0;

	//== HELPS REDUCE LAG WHEN THERE ARE SIMULTANEOUS CALLS ======
	%time = getIntegerTime(true);
	if(%time - %clientId.lastGetWeight <= 1 && fetchData(%clientId, "tmpWeight") != "")
		return fetchData(%clientId, "tmpWeight");
	%clientId.lastGetWeight = %time;
	
	$GetWeight::ArmorMod = "";
	%total = 0;

	%max = getNumItems();
	for(%i = 0; %i < %max; %i++) {
		%checkItem = getItemData(%i);
		%itemcount = Player::getItemCount(%clientId, %checkItem);

		if(%itemcount) {
			%weight = GetAccessoryVar(%checkItem, $Weight);
			if(%weight != "" && %weight != False)
				%total += %weight * %itemcount;
			
			%specialvar = GetAccessoryVar(%checkItem, $SpecialVar);
			if(GetWord(%specialvar, 0) == 8 && %checkItem.className == Equipped)
				$GetWeight::ArmorMod = GetWord(%specialvar, 1);
		}
	}

	%total += Belt::GetWeight(%clientid);
	%total += fetchData(%clientId, "COINS") * 0.001;
	
	%falseWeight = AddBonusStatePoints(%clientId, "WEIGHT");
	if(Player::isAiControlled(%clientId))	%falseWeight *= 3;
	if(%falseWeight > 0)					%total += %falseWeight;
	
	storeData(%clientId, "tmpWeight", %total);
	return RoundToFirstDecimal(%total);
}
