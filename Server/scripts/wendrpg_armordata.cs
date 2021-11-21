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

//___________________________________________________________________________________________________________________________
// DescX Notes:
//		Contains various enemy/race/armor related helper functions.
//___________________________________________________________________________________________________________________________
function rpg::DefineEnemyRace(%race, %gender, %armorAndskin, %team, %minHP, %jumpHeight, %itemKit, %skillKit,
								%sndDeath, %sndAcquired, %sndTaunt, %sndIdle, %sndHit) {
	if( String::findSubStr(%gender,"Fe") != -1 || 
		String::findSubStr(%gender,"fe") != -1)			$GenderForRace[%race] = 3;
	else if(String::findSubStr(%gender,"ale") != -1)	$GenderForRace[%race] = 2;
	else												$GenderForRace[%race] = 1;	
	
	$TeamForRace[%race] = 1;	
	for(%x=0;%x<8;%x++) {
		if(String::ICompare($Server::teamName[%x], %team) == 0) {
			$TeamForRace[%race] = %x;
			break;
		}
	}
	
	%split	= String::findSubStr(%armorAndskin, "::");
	%armor 	= String::newGetSubStr(%armorAndskin, 0, %split);
	%skin 	= String::newGetSubStr(%armorAndskin, %split + 2, 9999);
	
	$MinHP[%race] 								= %minHP;
	$AIJumpHeight[%race] 						= %jumpHeight;
	$AISkillKit[%race]							= %skillKit;
	$AIRaceItemKit[%race]						= %itemKit;
	$RaceArmorType[%race] 						= %armor;
	$RaceSkin[%race] 							= %skin;
	for(%x=0;(%w=GetWord(%sndDeath,%x)) != -1; %x++)	$RaceSound[%race, Death, %x+1] = %w;
	for(%x=0;(%w=GetWord(%sndAcquired,%x)) != -1; %x++)	$RaceSound[%race, Acquired, %x+1] = %w;
	for(%x=0;(%w=GetWord(%sndTaunt,%x)) != -1; %x++)	$RaceSound[%race, Taunt, %x+1] = %w;
	for(%x=0;(%w=GetWord(%sndIdle,%x)) != -1; %x++)		$RaceSound[%race, RandomWait, %x+1] = %w;
	for(%x=0;(%w=GetWord(%sndHit,%x)) != -1; %x++)		$RaceSound[%race, Hit, %x+1] = %w;
}

//________________________________________________________________________________________________
$spawnIndexCounter = 0;
function rpg::DefineEnemyType(%name, %race, %class, %levelRange, %favor, %items) {
	if($NameForRace[%name] == "") { // not yet defined, give it a spawn index for shits and giggles
		$spawnIndexCounter++;
		$spawnIndex[$spawnIndexCounter] = %name;
	}
		
	%fem = string::findSubStr(%race,"Female");
	%displayName = %race;
	if(%fem > -1) %displayName = string::newGetSubStr(%race,0,%fem);

	$NameForRace[%name] 		= %displayName;
	$AIRaceName[%name] 			= %race;
	$BotEquipment[%name] 		= "CLASS " @ %class @ " LVL " @ %levelRange @ " FAVOR " @ %favor;
	if(%items != "") 
		$BotEquipment[%name] 	= $BotEquipment[%name] @ " " @ %items;	
	if($AIRaceItemKit[%race] != "") 
		$BotEquipment[%name] = $BotEquipment[%name] @ " " @ $AIRaceItemKit[%race];
}

//___________________________________________________________________________________________________________________________
function rpg::DefineShieldType(%name, %specialVars, %weight, %info, %cost) {
	$AccessoryVar[%name, $AccessoryType] = $ShieldAccessoryType;
	$AccessoryVar[%name, $SpecialVar] = %specialVars;
	$AccessoryVar[%name, $Weight] = %weight;
	$AccessoryVar[%name, $MiscInfo] = %info;
	$HardcodedItemCost[%name] = %cost;
}

//___________________________________________________________________________________________________________________________
$ArmorListCount = 0;
function rpg::DefineArmorType(%name, %skin, %modelSuffix, %info, %specialvars, %weight, %hitsound, %forceCost) {
	%index = 0;
	if($ArmorSkin[%name] == "") {
		$ArmorListCount++;
		%index = $ArmorListCount;
	} else {
		%index = $ArmorIndexList[%name];
	}
	$ArmorList[%index] 						= %name;
	$ArmorIndexList[%name] 					= %index;	
	$AccessoryVar[%name, $AccessoryType] 	= $BodyAccessoryType;
	$AccessoryVar[%name, $SpecialVar] 		= %specialvars;
	$AccessoryVar[%name, $Weight] 			= %weight;
	$AccessoryVar[%name, $MiscInfo] 		= %info;
	$ArmorSkin[%name] 						= %skin;
	$ArmorModelSuffix[%name] 				= %modelSuffix;
	if(%forceCost != ""){
		$ItemCost[%name] 			= %forceCost;
		$HardcodedItemCost[%name] 	= %forceCost;
	} else {
		$ItemCost[%name] 			= GenerateItemCost(%name);
	}
	if(%hitsound == 0 || %hitsound == "" || !%hitsound)
		$ArmorHitSound[%name] 				= SoundHitFlesh;
	else
		$ArmorHitSound[%name] 				= %hitsound;
}


//___________________________________________________________________________________________________________________________
DamageSkinData armorDamageSkins {
	bmpName[0] = "dskin1_armor";bmpName[1] = "dskin2_armor";bmpName[2] = "dskin3_armor";bmpName[3] = "dskin4_armor";bmpName[4] = "dskin5_armor";
	bmpName[5] = "dskin6_armor";bmpName[6] = "dskin7_armor";bmpName[7] = "dskin8_armor";bmpName[8] = "dskin9_armor";bmpName[9] = "dskin10_armor";
};
DebrisData playerDebris {
	type = 0; imageType = 0; mass = 100.0; elasticity = 0.25; friction = 0.5; center = { 0, 0, 0 }; 
	animationSequence = -1; minTimeout = 3.0; maxTimeout = 6.0; explodeOnBounce = 0.3; damage = 1000.0; 
	damageThreshold = 100.0; spawnedDebrisMask = 1; spawnedDebrisStrength = 90; spawnedDebrisRadius = 0.2; 
	p = 1; explodeOnRest = True; collisionDetail = 0;
};

//___________________________________________________________________________________________________________________________
$Server::teamName[0] = "Citizen";
$Server::teamSkin[0] = "rpgbase";
$Server::teamName[1] = "Enemy";
$Server::teamSkin[1] = "";
$Server::teamName[2] = "Greenskins";
$Server::teamSkin[2] = "rpgorc";
$Server::teamName[3] = "Gnoll";
$Server::teamSkin[3] = "rpggnoll";
$Server::teamName[4] = "Undead";
$Server::teamSkin[4] = "undead";
$Server::teamName[5] = "Elf";
$Server::teamSkin[5] = "rpgelf";
$Server::teamName[6] = "Minotaur";
$Server::teamSkin[6] = "min";
$Server::teamName[7] = "Golem";
$Server::teamSkin[7] = "fedmonster";

//___________________________________________________________________________________________________________________________
$Server::teamNamePlural[0] = "Citizens";
$Server::teamNamePlural[1] = "Enemies";
$Server::teamNamePlural[2] = "Greenskins";
$Server::teamNamePlural[3] = "Gnolls";
$Server::teamNamePlural[4] = "Undead";
$Server::teamNamePlural[5] = "Elves";
$Server::teamNamePlural[6] = "Minotaurs";
$Server::teamNamePlural[7] = "Golems";

//___________________________________________________________________________________________________________________________
// Special cases...
$TeamForRace[Keeper] = 0;

//_________________________________________________________________________________________________________________________________________________________
$spdlower = 5;
$spdlow = 6;
$spdlowmed = 7;
$spdmed = 8;
$spdmedhigh = 9;
$spdhigh = 10;
$spdhigher = 11;
$spdfast = 16;
$spdveryfast = 20;
$spdgm = 50;
