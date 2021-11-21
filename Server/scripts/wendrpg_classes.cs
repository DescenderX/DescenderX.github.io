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


$initcoins[Priest] 	= "3d6x10";
$initcoins[Rogue] 	= "2d6x10";
$initcoins[Warrior] = "5d4x10";
$initcoins[Wizard] 	= "1d4+1x10";

$MinHP[MaleHuman] 	= 12;
$MinHP[FemaleHuman] = 12;
$MinHP[DeathKnight] = 5000;
$MinHP[DeathKnight] = 12;

$ClassName[1] 	= "Cleric";
$ClassName[2] 	= "Druid";
$ClassName[3] 	= "Thief";
$ClassName[4] 	= "Bard";
$ClassName[5] 	= "Fighter";
$ClassName[6] 	= "Paladin";
$ClassName[7] 	= "Ranger";
$ClassName[8] 	= "Mage";
$ClassName[9] 	= "Merchant";
$ClassName[10] 	= "Brawler";
$ClassName[11] 	= "Enchanter";
$ClassName[12] 	= "Invoker";

$ClassGroup[Cleric] 	= "Priest";
$ClassGroup[Druid] 		= "Priest";
$ClassGroup[Paladin] 	= "Priest";

$ClassGroup[Thief] 		= "Rogue";
$ClassGroup[Bard] 		= "Rogue";
$ClassGroup[Merchant] 	= "Rogue";

$ClassGroup[Fighter] 	= "Warrior";
$ClassGroup[Ranger] 	= "Warrior";
$ClassGroup[Brawler] 	= "Warrior";

$ClassGroup[Mage] 		= "Wizard";
$ClassGroup[Enchanter] 	= "Wizard";
$ClassGroup[Invoker] 	= "Wizard";

$NumericSuffix = "th";
$NumericSuffix[1] = "st";
$NumericSuffix[2] = "nd";
$NumericSuffix[3] = "rd";

function rpg::NumericSuffix(%i) {
	if($NumericSuffix[%i % 10] == "") return %i @ $NumericSuffix;
	return %i @ $NumericSuffix[%i % 10];
}

function getFinalCLASS(%clientId) {	
	%remortLevel = fetchData(%clientId, "RemortStep");
	if(%remortLevel > 0)
		%remortLevel = ", " @ rpg::NumericSuffix(%remortLevel);
	else %remortLevel = "";
	
	%houseTitle = rpg::GetHouseTitle(%clientId);
	if(%houseTitle != "")
		%houseTitle = " " @ %houseTitle;
	
	%survivor = (fetchData(%clientId, "CyclesSurvived") > 0);
	if(%survivor > 0)
		%survivor = ", Reborn";
	else %survivor = "";
	
	// 104th Reborn Druid Slayer
	// 3rd Fighter Ordertaker
	// Reborn Mage Waysetter
	return fetchData(%clientId, "CLASS") @ %houseTitle @ %remortLevel @ %survivor;
}

function IsAClass(%class)
{
	dbecho($dbechoMode, "IsAClass(" @ %class @ ")");
	for(%i = 1; $ClassName[%i] != ""; %i++)	{
		if(String::ICompare(%class, $ClassName[%i]) == 0)
			return True;
	}
	return False;
}
