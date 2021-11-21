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


//____________________________________________________________________________________________________________________________________________________________
// DescX Note:
//		The entire skill tree has been redone for World Ender, but my hope is that I've managed to do so in a way that
//		"extends" the base game. Almost no functionality has been removed, but most of it has been shuffled around.
//		For example, "Sense Heading" became "Survival", which allows for a host of new skills that "make sense" without
//		removing anything from the base game. There are now 5 spells trees instead of 3, and combat skills are all backed
//		up by a secondary attribute - Slashing governs chance to hit, while Strength governs damage. Etc,etc.
//
//		I did NOT want to make a cheesy amateur mod :). I wanted to take what JI and the community had built up over the years,
//		make it all fit together, put some finishing touches on it, and do my best to leave as much as possible in place.

$SkillSlashing = 1;					// Parry (always on)										backed by Strength
$SkillPiercing = 2;					// Backstabbing (always on)									backed by Focus
$SkillBludgeoning = 3;				// Bashing (always on)										backed by Strength
$SkillArchery = 4;					// Access to Poison projectiles								backed by Endurance
$SkillWands = 5;					// Variety of ranged attack types with no ammo required		backed by Willpower
$SkillCombatArts = 6;				// Spells for fighter types
$SkillElementalMagic = 7;			// Spells that deal damage (or are otherwise "elemental")
$SkillRestorationMagic = 8;			// Spells that heal or protect
$SkillDistortionMagic = 9;			// Spells that distort time or space
$SkillIllusionMagic = 10;			// Spells that play tricks or alter reality
$SkillEvasion = 11;					// Your chance to evade physical attacks.
$SkillThievery = 12;				// New #skills including combat skills #swoop #leech #bleed
$SkillMining = 13;					// Added weapons and several types of ore; sped up with Attack Echos and other effects
$SkillWordsmith = 14;				// Spells that work like old D&D "resting" casters. Prepare idioms using ore in advance of casting. Entirely new tree
$SkillSurvival = 15;				// Takes the old Sense Heading and adds a bunch of combat skills; wood cutting; and more
$SkillEndurance = 16;				// Governs your ability to wear robes; your health regeneration; and archery damage. Crosses many classes.
$SkillStrength = 17;				// Governs your ability to wear armor and your damage with blunt/slashing weapons
$SkillWillpower = 18;				// Governs your mana recharge rate, base spell resistance, and wand damage
$SkillFocus = 19;					// Governs your spell channeling/recovery times, piercing damage, and various misc Thievery/Wordsmith skills 
$SkillVitality = 20;				// Governs your maximum health. Increased by eating or drinking stuff.
$SkillEnergy = 21;					// Governs your maximum mana. Increased by eating or drinking stuff.
$SkillLuck = 22;					// Governs drop chances for items.

$MinLevel = "L";
$MinGroup = "G";
$MinClass = "C";
$MinRemort = "R";
$MinAdmin = "A";
$MinHouse = "H";

$SkillDesc[1] = "Slashing";				// increase by using Slashing
$SkillDesc[2] = "Piercing";				// increase by using Piercing
$SkillDesc[3] = "Bludgeoning";			// increase by using Bludgeoning
$SkillDesc[4] = "Archery";				// increase by using Archery
$SkillDesc[5] = "Wands";				// increase by using Wands
$SkillDesc[6] = "Combat Arts";			// increase by casting Combat Arts
$SkillDesc[7] = "Elemental Magic";		// increase by casting Elemental magic
$SkillDesc[8] = "Restoration Magic";	// increase by casting Restoration magic
$SkillDesc[9] = "Distortion Magic";		// increase by casting Distortion magic
$SkillDesc[10] = "Illusion Magic";		// increase by casting Illusion magic
$SkillDesc[11] = "Evasion";				// increase by evading physical attacks
$SkillDesc[12] = "Thievery";			// increase by using Thievery skills
$SkillDesc[13] = "Mining";				// increase by mining
$SkillDesc[14] = "Wordsmith";			// increase by using Wordsmith skills
$SkillDesc[15] = "Survival";			// increase by using Survival skills
$SkillDesc[16] = "Endurance";			// increase by moving while weighed down, getting hit, or landing Archery hits
$SkillDesc[17] = "Strength";			// increase by hitting stuff with Slashing or Bludgeoning
$SkillDesc[18] = "Willpower";			// increase by taking spell damage or hitting stuff with Wands
$SkillDesc[19] = "Focus";				// increase by successfully casting spells or hitting stuff with Piercing
$SkillDesc[20] = "Vitality";			// increase by eating stuff
$SkillDesc[21] = "Energy";				// increase by eating stuff
$SkillDesc[22] = "Luck";				// manual increase only, needs SP


$SkillIndex[Slashing] = 1;
$SkillIndex[Piercing] = 2;
$SkillIndex[Bludgeoning] = 3;
$SkillIndex[Archery] = 4;
$SkillIndex[Wands] = 5;
$SkillIndex[CombatArts] = 6;
$SkillIndex[ElementalMagic] = 7;
$SkillIndex[RestorationMagic] = 8;
$SkillIndex[DistortionMagic] = 9;
$SkillIndex[IllusionMagic] = 10;
$SkillIndex[Evasion] = 11;
$SkillIndex[Thievery] = 12;
$SkillIndex[Mining] = 13;
$SkillIndex[Wordsmith] = 14;
$SkillIndex[Survival] = 15;
$SkillIndex[Endurance] = 16;
$SkillIndex[Strength] = 17;
$SkillIndex[Willpower] = 18;
$SkillIndex[Focus] = 19;
$SkillIndex[Vitality] = 20;
$SkillIndex[Energy] = 21;
$SkillIndex[Luck] = 22;

$SkillDesc[L] = "Level";
$SkillDesc[G] = "Group";
$SkillDesc[C] = "Class";
$SkillDesc[R] = "Remort";
$SkillDesc[A] = "Admin Level";
$SkillDesc[H] = "House";

//________________________________________________________________________________________________________________________________________________
// Cleric -> Restoration, uses clubs; average survival and haggling; high AC, VIT, WILL; bad stealing, o-casting
$SkillMultiplier[Cleric, $SkillSlashing] = 0.4;
$SkillMultiplier[Cleric, $SkillPiercing] = 0.4;
$SkillMultiplier[Cleric, $SkillBludgeoning] = 1.5;
$SkillMultiplier[Cleric, $SkillArchery] = 0.5;
$SkillMultiplier[Cleric, $SkillWands] = 1.0;
$SkillMultiplier[Cleric, $SkillCombatArts] = 0.5;
$SkillMultiplier[Cleric, $SkillElementalMagic] = 0.5;
$SkillMultiplier[Cleric, $SkillRestorationMagic] = 2.0;
$SkillMultiplier[Cleric, $SkillDistortionMagic] = 0.5;
$SkillMultiplier[Cleric, $SkillIllusionMagic] = 0.5;
$SkillMultiplier[Cleric, $SkillEvasion] = 0.8;
$SkillMultiplier[Cleric, $SkillThievery] = 0.2;
$SkillMultiplier[Cleric, $SkillMining] = 0.5;
$SkillMultiplier[Cleric, $SkillWordsmith] = 1.5;
$SkillMultiplier[Cleric, $SkillSurvival] = 1.0;
$SkillMultiplier[Cleric, $SkillEndurance] = 0.5;
$SkillMultiplier[Cleric, $SkillStrength] = 1.5;
$SkillMultiplier[Cleric, $SkillWillpower] = 1.5;
$SkillMultiplier[Cleric, $SkillFocus] = 1.0;
$SkillMultiplier[Cleric, $SkillVitality] = 2.0;
$SkillMultiplier[Cleric, $SkillEnergy] = 1.0;
$SkillMultiplier[Cleric, $SkillLuck] = 0.5;
$EXPmultiplier[Cleric] = 1.0;

//________________________________________________________________________________________________________________________________________________
// Druid -> range fighter, high evade + recovery + energy; average magic; terrible AC
$SkillMultiplier[Druid, $SkillSlashing] = 0.4;
$SkillMultiplier[Druid, $SkillPiercing] = 0.4;
$SkillMultiplier[Druid, $SkillBludgeoning] = 0.4;
$SkillMultiplier[Druid, $SkillArchery] = 1.3;
$SkillMultiplier[Druid, $SkillWands] = 1.3;
$SkillMultiplier[Druid, $SkillCombatArts] = 0.2;
$SkillMultiplier[Druid, $SkillElementalMagic] = 0.8;
$SkillMultiplier[Druid, $SkillRestorationMagic] = 1.5;
$SkillMultiplier[Druid, $SkillDistortionMagic] = 1.0;
$SkillMultiplier[Druid, $SkillIllusionMagic] = 1.0;
$SkillMultiplier[Druid, $SkillEvasion] = 1.5;
$SkillMultiplier[Druid, $SkillThievery] = 0.2;
$SkillMultiplier[Druid, $SkillMining] = 0.6;
$SkillMultiplier[Druid, $SkillWordsmith] = 0.2;
$SkillMultiplier[Druid, $SkillSurvival] = 0.5;
$SkillMultiplier[Druid, $SkillEndurance] = 2.0;
$SkillMultiplier[Druid, $SkillStrength] = 0.2;
$SkillMultiplier[Druid, $SkillWillpower] = 2.0;
$SkillMultiplier[Druid, $SkillFocus] = 1.0;
$SkillMultiplier[Druid, $SkillVitality] = 0.4;
$SkillMultiplier[Druid, $SkillEnergy] = 2.0;
$SkillMultiplier[Druid, $SkillLuck] = 1.0;
$EXPmultiplier[Druid] = 1.0;

//________________________________________________________________________________________________________________________________________________
// Paladin -> Any melee weapon, high AC, mining, combat arts, restorage; no other magic, average vit/nrg, cant steal, no distance weapon
$SkillMultiplier[Paladin, $SkillSlashing] = 1.3;
$SkillMultiplier[Paladin, $SkillPiercing] = 1.3;
$SkillMultiplier[Paladin, $SkillBludgeoning] = 1.3;
$SkillMultiplier[Paladin, $SkillArchery] = 0.4;
$SkillMultiplier[Paladin, $SkillWands] = 0.4;
$SkillMultiplier[Paladin, $SkillCombatArts] = 1.5;
$SkillMultiplier[Paladin, $SkillElementalMagic] = 0.3;
$SkillMultiplier[Paladin, $SkillRestorationMagic] = 1.5;
$SkillMultiplier[Paladin, $SkillDistortionMagic] = 0.3;
$SkillMultiplier[Paladin, $SkillIllusionMagic] = 0.3;
$SkillMultiplier[Paladin, $SkillEvasion] = 1.0;
$SkillMultiplier[Paladin, $SkillThievery] = 0.1;
$SkillMultiplier[Paladin, $SkillMining] = 1.5;
$SkillMultiplier[Paladin, $SkillWordsmith] = 0.5;
$SkillMultiplier[Paladin, $SkillSurvival] = 1.3;
$SkillMultiplier[Paladin, $SkillEndurance] = 1.5;
$SkillMultiplier[Paladin, $SkillStrength] = 2.0;
$SkillMultiplier[Paladin, $SkillWillpower] = 0.5;
$SkillMultiplier[Paladin, $SkillFocus] = 1.5;
$SkillMultiplier[Paladin, $SkillVitality] = 1.0;
$SkillMultiplier[Paladin, $SkillEnergy] = 1.0;
$SkillMultiplier[Paladin, $SkillLuck] = 0.5;
$EXPmultiplier[Paladin] = 1.0;

//________________________________________________________________________________________________________________________________________________
// Thief -> Extreme thievery, evasion, survival; decent combat arts, piercing, archery, luck; terrible vit, str, endur, magic
$SkillMultiplier[Thief, $SkillSlashing] = 0.8;
$SkillMultiplier[Thief, $SkillPiercing] = 1.8;
$SkillMultiplier[Thief, $SkillBludgeoning] = 0.8;
$SkillMultiplier[Thief, $SkillArchery] = 1.5;
$SkillMultiplier[Thief, $SkillWands] = 0.2;
$SkillMultiplier[Thief, $SkillCombatArts] = 1.8;
$SkillMultiplier[Thief, $SkillElementalMagic] = 0.2;
$SkillMultiplier[Thief, $SkillRestorationMagic] = 0.2;
$SkillMultiplier[Thief, $SkillDistortionMagic] = 0.2;
$SkillMultiplier[Thief, $SkillIllusionMagic] = 0.2;
$SkillMultiplier[Thief, $SkillEvasion] = 2.0;
$SkillMultiplier[Thief, $SkillThievery] = 3.0;
$SkillMultiplier[Thief, $SkillMining] = 0.2;
$SkillMultiplier[Thief, $SkillWordsmith] = 1.5;
$SkillMultiplier[Thief, $SkillSurvival] = 2.0;
$SkillMultiplier[Thief, $SkillEndurance] = 0.5;
$SkillMultiplier[Thief, $SkillStrength] = 0.5;
$SkillMultiplier[Thief, $SkillWillpower] = 0.5;
$SkillMultiplier[Thief, $SkillFocus] = 1.0;
$SkillMultiplier[Thief, $SkillVitality] = 0.6;
$SkillMultiplier[Thief, $SkillEnergy] = 0.2;
$SkillMultiplier[Thief, $SkillLuck] = 3.0;
$EXPmultiplier[Thief] = 1.0;

//________________________________________________________________________________________________________________________________________________
// Bard -> Can do anything, but gains 25% less XP
$SkillMultiplier[Bard, $SkillSlashing] = 1.0;
$SkillMultiplier[Bard, $SkillPiercing] = 1.0;
$SkillMultiplier[Bard, $SkillBludgeoning] = 1.0;
$SkillMultiplier[Bard, $SkillArchery] = 1.0;
$SkillMultiplier[Bard, $SkillWands] = 1.0;
$SkillMultiplier[Bard, $SkillCombatArts] = 1.0;
$SkillMultiplier[Bard, $SkillElementalMagic] = 1.0;
$SkillMultiplier[Bard, $SkillRestorationMagic] = 1.0;
$SkillMultiplier[Bard, $SkillDistortionMagic] = 1.0;
$SkillMultiplier[Bard, $SkillIllusionMagic] = 1.0;
$SkillMultiplier[Bard, $SkillEvasion] = 1.0;
$SkillMultiplier[Bard, $SkillThievery] = 1.0;
$SkillMultiplier[Bard, $SkillMining] = 1.0;
$SkillMultiplier[Bard, $SkillWordsmith] = 1.0;
$SkillMultiplier[Bard, $SkillSurvival] = 1.0;
$SkillMultiplier[Bard, $SkillEndurance] = 1.0;
$SkillMultiplier[Bard, $SkillStrength] = 1.0;
$SkillMultiplier[Bard, $SkillWillpower] = 1.0;
$SkillMultiplier[Bard, $SkillFocus] = 1.0;
$SkillMultiplier[Bard, $SkillVitality] = 1.0;
$SkillMultiplier[Bard, $SkillEnergy] = 1.0;
$SkillMultiplier[Bard, $SkillLuck] = 1.0;
$EXPmultiplier[Bard] = 0.75;

//________________________________________________________________________________________________________________________________________________
// Merchant -> Extreme haggle, mining, will, focus, luck, evasion; average thief, illusionist; terrible at everything else
$SkillMultiplier[Merchant, $SkillSlashing] = 0.2;
$SkillMultiplier[Merchant, $SkillPiercing] = 0.2;
$SkillMultiplier[Merchant, $SkillBludgeoning] = 0.2;
$SkillMultiplier[Merchant, $SkillArchery] = 0.2;
$SkillMultiplier[Merchant, $SkillWands] = 0.2;
$SkillMultiplier[Merchant, $SkillCombatArts] = 0.2;
$SkillMultiplier[Merchant, $SkillElementalMagic] = 0.2;
$SkillMultiplier[Merchant, $SkillRestorationMagic] = 0.2;
$SkillMultiplier[Merchant, $SkillDistortionMagic] = 0.2;
$SkillMultiplier[Merchant, $SkillIllusionMagic] = 0.8;
$SkillMultiplier[Merchant, $SkillEvasion] = 2.0;
$SkillMultiplier[Merchant, $SkillThievery] = 1.5;
$SkillMultiplier[Merchant, $SkillMining] = 3.0;
$SkillMultiplier[Merchant, $SkillWordsmith] = 3.0;
$SkillMultiplier[Merchant, $SkillSurvival] = 1.0;
$SkillMultiplier[Merchant, $SkillEndurance] = 1.0;
$SkillMultiplier[Merchant, $SkillStrength] = 0.2;
$SkillMultiplier[Merchant, $SkillWillpower] = 2.0;
$SkillMultiplier[Merchant, $SkillFocus] = 2.0;
$SkillMultiplier[Merchant, $SkillVitality] = 1.0;
$SkillMultiplier[Merchant, $SkillEnergy] = 1.0;
$SkillMultiplier[Merchant, $SkillLuck] = 2.0;
$EXPmultiplier[Merchant] = 1.0;

//________________________________________________________________________________________________________________________________________________
// Fighter -> Can use any weapon with high damage and accuracy; worthless magic, haggling, and combat arts are hard to use with low nrg
$SkillMultiplier[Fighter, $SkillSlashing] = 2.0;
$SkillMultiplier[Fighter, $SkillPiercing] = 2.0;
$SkillMultiplier[Fighter, $SkillBludgeoning] = 2.0;
$SkillMultiplier[Fighter, $SkillArchery] = 2.0;
$SkillMultiplier[Fighter, $SkillWands] = 0.1;
$SkillMultiplier[Fighter, $SkillCombatArts] = 1.0;
$SkillMultiplier[Fighter, $SkillElementalMagic] = 0.1;
$SkillMultiplier[Fighter, $SkillRestorationMagic] = 0.1;
$SkillMultiplier[Fighter, $SkillDistortionMagic] = 0.1;
$SkillMultiplier[Fighter, $SkillIllusionMagic] = 0.1;
$SkillMultiplier[Fighter, $SkillEvasion] = 1.0;
$SkillMultiplier[Fighter, $SkillThievery] = 0.3;
$SkillMultiplier[Fighter, $SkillMining] = 0.1;
$SkillMultiplier[Fighter, $SkillWordsmith] = 0.3;
$SkillMultiplier[Fighter, $SkillSurvival] = 2.0;
$SkillMultiplier[Fighter, $SkillEndurance] = 2.0;
$SkillMultiplier[Fighter, $SkillStrength] = 3.0;
$SkillMultiplier[Fighter, $SkillWillpower] = 2.0;
$SkillMultiplier[Fighter, $SkillFocus] = 2.0;
$SkillMultiplier[Fighter, $SkillVitality] = 2.0;
$SkillMultiplier[Fighter, $SkillEnergy] = 0.1;
$SkillMultiplier[Fighter, $SkillLuck] = 1.0;
$EXPmultiplier[Fighter] = 1.0;

//________________________________________________________________________________________________________________________________________________
// Brawler ->  high damage slash/club, survival, mining, strength, vit and energy; can use combat arts and distortion magic; cant carry much, terrible luck, no ranged stuff
$SkillMultiplier[Brawler, $SkillSlashing] = 2.0;
$SkillMultiplier[Brawler, $SkillPiercing] = 0.3;
$SkillMultiplier[Brawler, $SkillBludgeoning] = 2.0;
$SkillMultiplier[Brawler, $SkillArchery] = 0.1;
$SkillMultiplier[Brawler, $SkillWands] = 0.1;
$SkillMultiplier[Brawler, $SkillCombatArts] = 2.5;
$SkillMultiplier[Brawler, $SkillElementalMagic] = 0.1;
$SkillMultiplier[Brawler, $SkillRestorationMagic] = 0.1;
$SkillMultiplier[Brawler, $SkillDistortionMagic] = 1.5;
$SkillMultiplier[Brawler, $SkillIllusionMagic] = 0.1;
$SkillMultiplier[Brawler, $SkillEvasion] = 1.5;
$SkillMultiplier[Brawler, $SkillThievery] = 0.3;
$SkillMultiplier[Brawler, $SkillMining] = 1.0;
$SkillMultiplier[Brawler, $SkillWordsmith] = 0.3;
$SkillMultiplier[Brawler, $SkillSurvival] = 1.0;
$SkillMultiplier[Brawler, $SkillEndurance] = 0.4;
$SkillMultiplier[Brawler, $SkillStrength] = 2.0;
$SkillMultiplier[Brawler, $SkillWillpower] = 0.5;
$SkillMultiplier[Brawler, $SkillFocus] = 0.5;
$SkillMultiplier[Brawler, $SkillVitality] = 1.5;
$SkillMultiplier[Brawler, $SkillEnergy] = 2.0;
$SkillMultiplier[Brawler, $SkillLuck] = 0.2;
$EXPmultiplier[Brawler] = 1.0;

//________________________________________________________________________________________________________________________________________________
// Ranger ->  all range all the time; possible to add some magic; huge carry capacity; terrible melee
$SkillMultiplier[Ranger, $SkillSlashing] = 0.2;
$SkillMultiplier[Ranger, $SkillPiercing] = 1.0;
$SkillMultiplier[Ranger, $SkillBludgeoning] = 0.2;
$SkillMultiplier[Ranger, $SkillArchery] = 2.1;
$SkillMultiplier[Ranger, $SkillWands] = 0.1;
$SkillMultiplier[Ranger, $SkillCombatArts] = 1.7;
$SkillMultiplier[Ranger, $SkillElementalMagic] = 0.3;
$SkillMultiplier[Ranger, $SkillRestorationMagic] = 0.8;
$SkillMultiplier[Ranger, $SkillDistortionMagic] = 0.8;
$SkillMultiplier[Ranger, $SkillIllusionMagic] = 0.6;
$SkillMultiplier[Ranger, $SkillEvasion] = 2.0;
$SkillMultiplier[Ranger, $SkillThievery] = 0.6;
$SkillMultiplier[Ranger, $SkillMining] = 1.3;
$SkillMultiplier[Ranger, $SkillWordsmith] = 1.3;
$SkillMultiplier[Ranger, $SkillSurvival] = 3.0;
$SkillMultiplier[Ranger, $SkillEndurance] = 2.0;
$SkillMultiplier[Ranger, $SkillStrength] = 0.8;
$SkillMultiplier[Ranger, $SkillWillpower] = 1.0;
$SkillMultiplier[Ranger, $SkillFocus] = 1.0;
$SkillMultiplier[Ranger, $SkillVitality] = 1.0;
$SkillMultiplier[Ranger, $SkillEnergy] = 0.8;
$SkillMultiplier[Ranger, $SkillLuck] = 1.0;
$EXPmultiplier[Ranger] = 1.0;

//________________________________________________________________________________________________________________________________________________
// Enchanter -> slasher with high mining, restoration, and distortion; low on the rest
$SkillMultiplier[Enchanter, $SkillSlashing] = 0.9;
$SkillMultiplier[Enchanter, $SkillPiercing] = 1.3;
$SkillMultiplier[Enchanter, $SkillBludgeoning] = 0.3;
$SkillMultiplier[Enchanter, $SkillArchery] = 0.3;
$SkillMultiplier[Enchanter, $SkillWands] = 1.3;
$SkillMultiplier[Enchanter, $SkillCombatArts] = 0.3;
$SkillMultiplier[Enchanter, $SkillElementalMagic] = 0.3;
$SkillMultiplier[Enchanter, $SkillRestorationMagic] = 2.0;
$SkillMultiplier[Enchanter, $SkillDistortionMagic] = 2.0;
$SkillMultiplier[Enchanter, $SkillIllusionMagic] = 0.3;
$SkillMultiplier[Enchanter, $SkillEvasion] = 1.0;
$SkillMultiplier[Enchanter, $SkillThievery] = 1.0;
$SkillMultiplier[Enchanter, $SkillMining] = 2.5;
$SkillMultiplier[Enchanter, $SkillWordsmith] = 1.0;
$SkillMultiplier[Enchanter, $SkillSurvival] = 0.3;
$SkillMultiplier[Enchanter, $SkillEndurance] = 0.5;
$SkillMultiplier[Enchanter, $SkillStrength] = 0.5;
$SkillMultiplier[Enchanter, $SkillWillpower] = 1.0;
$SkillMultiplier[Enchanter, $SkillFocus] = 1.0;
$SkillMultiplier[Enchanter, $SkillVitality] = 0.8;
$SkillMultiplier[Enchanter, $SkillEnergy] = 1.3;
$SkillMultiplier[Enchanter, $SkillLuck] = 1.5;
$EXPmultiplier[Enchanter] = 1.0;

//________________________________________________________________________________________________________________________________________________
// Invoker -> wand user with illusion magic, evasion, focus, and will; can access combat arts and elemental magic; terrible soft stats + haggle
$SkillMultiplier[Invoker, $SkillSlashing] = 0.3;
$SkillMultiplier[Invoker, $SkillPiercing] = 0.3;
$SkillMultiplier[Invoker, $SkillBludgeoning] = 0.3;
$SkillMultiplier[Invoker, $SkillArchery] = 1.0;
$SkillMultiplier[Invoker, $SkillWands] = 1.8;
$SkillMultiplier[Invoker, $SkillCombatArts] = 1.0;
$SkillMultiplier[Invoker, $SkillElementalMagic] = 1.0;
$SkillMultiplier[Invoker, $SkillRestorationMagic] = 0.3;
$SkillMultiplier[Invoker, $SkillDistortionMagic] = 2.0;
$SkillMultiplier[Invoker, $SkillIllusionMagic] = 3.0;
$SkillMultiplier[Invoker, $SkillEvasion] = 2.0;
$SkillMultiplier[Invoker, $SkillThievery] = 0.8;
$SkillMultiplier[Invoker, $SkillMining] = 0.3;
$SkillMultiplier[Invoker, $SkillWordsmith] = 0.3;
$SkillMultiplier[Invoker, $SkillSurvival] = 1.3;
$SkillMultiplier[Invoker, $SkillEndurance] = 0.3;
$SkillMultiplier[Invoker, $SkillStrength] = 0.3;
$SkillMultiplier[Invoker, $SkillWillpower] = 2.0;
$SkillMultiplier[Invoker, $SkillFocus] = 2.0;
$SkillMultiplier[Invoker, $SkillVitality] = 1.5;
$SkillMultiplier[Invoker, $SkillEnergy] = 1.5;
$SkillMultiplier[Invoker, $SkillLuck] = 1.0;
$EXPmultiplier[Invoker] = 1.0;

//________________________________________________________________________________________________________________________________________________
// Mage -> pure offensive magic; can use wands; very frail
$SkillMultiplier[Mage, $SkillSlashing] = 0.1;
$SkillMultiplier[Mage, $SkillPiercing] = 0.1;
$SkillMultiplier[Mage, $SkillBludgeoning] = 0.1;
$SkillMultiplier[Mage, $SkillArchery] = 0.1;
$SkillMultiplier[Mage, $SkillWands] = 1.5;
$SkillMultiplier[Mage, $SkillCombatArts] = 0.1;
$SkillMultiplier[Mage, $SkillElementalMagic] = 2.5;
$SkillMultiplier[Mage, $SkillRestorationMagic] = 1.5;
$SkillMultiplier[Mage, $SkillDistortionMagic] = 2.0;
$SkillMultiplier[Mage, $SkillIllusionMagic] = 2.0;
$SkillMultiplier[Mage, $SkillEvasion] = 0.2;
$SkillMultiplier[Mage, $SkillThievery] = 0.2;
$SkillMultiplier[Mage, $SkillMining] = 0.2;
$SkillMultiplier[Mage, $SkillWordsmith] = 1.5;
$SkillMultiplier[Mage, $SkillSurvival] = 0.5;
$SkillMultiplier[Mage, $SkillEndurance] = 0.2;
$SkillMultiplier[Mage, $SkillStrength] = 0.2;
$SkillMultiplier[Mage, $SkillWillpower] = 3.0;
$SkillMultiplier[Mage, $SkillFocus] = 3.0;
$SkillMultiplier[Mage, $SkillVitality] = 0.2;
$SkillMultiplier[Mage, $SkillEnergy] = 3.0;
$SkillMultiplier[Mage, $SkillLuck] = 1.0;
$EXPmultiplier[Mage] = 1.0;



//________________________________________________________________________________________________________________________________________________
$SkillRestriction[ApprenticeRobe] 	= $SkillEndurance @ " 0";
$SkillRestriction[LightRobe] 		= $SkillEndurance @ " 75";
$SkillRestriction[RobeOfOrder] 		= $SkillEndurance @ " 150";
$SkillRestriction[FineRobe] 		= $SkillEndurance @ " 225";
$SkillRestriction[AdvisorRobe] 		= $SkillEndurance @ " 300";
$SkillRestriction[BloodRobe] 		= $SkillEndurance @ " 375";
$SkillRestriction[FrostRobe] 		= $SkillEndurance @ " 450";
$SkillRestriction[ElvenRobe] 		= $SkillEndurance @ " 525";
$SkillRestriction[MasterRobe] 		= $SkillEndurance @ " 600";
$SkillRestriction[RobeOfVenjance] 	= $SkillEndurance @ " 675";
$SkillRestriction[AntanicRobe] 		= $SkillEndurance @ " 750";
$SkillRestriction[PhensRobe] 		= $SkillEndurance @ " 825";
$SkillRestriction[KeldrinRobe] 		= $SkillEndurance @ " 900";
//________________________________________________________________________________________________________________________________________________
$SkillRestriction[PaddedArmor] 		= $SkillStrength @ " 0";
$SkillRestriction[LeatherArmor] 	= $SkillStrength @ " 60";
$SkillRestriction[StuddedLeather] 	= $SkillStrength @ " 120";
$SkillRestriction[SpikedLeather] 	= $SkillStrength @ " 180";
$SkillRestriction[HideArmor] 		= $SkillStrength @ " 240";
$SkillRestriction[ScaleMail] 		= $SkillStrength @ " 300";
$SkillRestriction[BrigandineArmor] 	= $SkillStrength @ " 360";
$SkillRestriction[ChainMail] 		= $SkillStrength @ " 420";
$SkillRestriction[RingMail] 		= $SkillStrength @ " 480";
$SkillRestriction[BandedMail] 		= $SkillStrength @ " 540";
$SkillRestriction[SplintMail] 		= $SkillStrength @ " 600";
$SkillRestriction[BronzePlateMail] 	= $SkillStrength @ " 660";
$SkillRestriction[PlateMail] 		= $SkillStrength @ " 720";
$SkillRestriction[FieldPlateArmor] 	= $SkillStrength @ " 780";
$SkillRestriction[DragonMail] 		= $SkillStrength @ " 840";
$SkillRestriction[FullPlateArmor] 	= $SkillStrength @ " 900";
$SkillRestriction[KeldrinArmor] 	= $SkillStrength @ " 960";
//________________________________________________________________________________________________________________________________________________
$SkillRestriction[CheetaursPaws] 	= $MinLevel @ " 8";
$SkillRestriction[BootsOfGliding] 	= $MinLevel @ " 25";
$SkillRestriction[WindWalkers] 		= $MinLevel @ " 60";
//________________________________________________________________________________________________________________________________________________
// Slashing
$SkillRestriction[Hatchet] 		= $SkillSlashing @ " 0";		// hatchet
$SkillRestriction[BroadSword] 	= $SkillSlashing @ " 60";		// long_sword2
$SkillRestriction[WarAxe] 		= $SkillSlashing @ " 100";		// CRIMAXE2
$SkillRestriction[LongSword] 	= $SkillSlashing @ " 200";		// long_sword
$SkillRestriction[Halberd] 		= $SkillSlashing @ " 300";		// battleaxe2
$SkillRestriction[ElvenSword] 	= $SkillSlashing @ " 400";		// greensword
$SkillRestriction[BattleAxe] 	= $SkillSlashing @ " 500";		// BattleAxe
$SkillRestriction[Claymore] 	= $SkillSlashing @ " 600";		// sword
$SkillRestriction[Slasher] 		= $SkillSlashing @ " 700";		// slasher
$SkillRestriction[GoliathSword] = $SkillSlashing @ " 800";		// goliathsword
$SkillRestriction[KeldriniteLS] = $SkillSlashing @ " 900";		// elfinblade

$SkillRestriction[OrcishAxe] 	= $SkillSlashing @ " 30";		// OrcishAxe
//________________________________________________________________________________________________________________________________________________
// Bludgeoning
$SkillRestriction[BoneClub] 		= $SkillBludgeoning @ " 0";		// pboneclub
$SkillRestriction[Hammer] 			= $SkillBludgeoning @ " 60";	// pick
$SkillRestriction[Club] 			= $SkillBludgeoning @ " 100";	// club
$SkillRestriction[StoneAxe]			= $SkillBludgeoning @ " 200";	// axe
$SkillRestriction[SpikedBoneClub] 	= $SkillBludgeoning @ " 300";	// pboneclub
$SkillRestriction[SpikedClub] 		= $SkillBludgeoning @ " 400";	// spikedclub
$SkillRestriction[Mace] 			= $SkillBludgeoning @ " 500";	// mace
$SkillRestriction[MorningStar] 		= $SkillBludgeoning @ " 600";	// mace2
$SkillRestriction[WarHammer] 		= $SkillBludgeoning @ " 700";	// hammer_bronze
$SkillRestriction[WarMaul] 			= $SkillBludgeoning @ " 800";	// hammer
$SkillRestriction[TitanCrusher] 	= $SkillBludgeoning @ " 900";	// hammer_ice
//________________________________________________________________________________________________________________________________________________
// Wands
$SkillRestriction[CastingBlade] 	= $SkillWands @ " 0";		// dagger
$SkillRestriction[QuarterStaff] 	= $SkillWands @ " 60";		// quarterstaff
$SkillRestriction[Smoker] 			= $SkillWands @ " 100";		// grenadeL
$SkillRestriction[FrozenWand]		= $SkillWands @ " 200";		// same as prj?...
$SkillRestriction[FlintCaster] 		= $SkillWands @ " 300";		// energygun
$SkillRestriction[LongStaff] 		= $SkillWands @ " 400";		// longstaff
$SkillRestriction[FlameThrower] 	= $SkillWands @ " 500";		// repairgun
$SkillRestriction[ThunderCaller] 	= $SkillWands @ " 600";		// disc :(
$SkillRestriction[ToxicDevice] 		= $SkillWands @ " 700";		// paintgun
$SkillRestriction[EtheralSpear] 	= $SkillWands @ " 800";		// spearether
$SkillRestriction[JusticeStaff] 	= $SkillWands @ " 900";		// cannon
//________________________________________________________________________________________________________________________________________________
// Piercing
$SkillRestriction[Knife] 		= $SkillPiercing @ " 0";	// pick
$SkillRestriction[Spear]		= $SkillPiercing @ " 60";	// dagger
$SkillRestriction[Thrown] 		= $SkillPiercing @ " 100";	// spear
$SkillRestriction[Dagger] 		= $SkillPiercing @ " 200";	// knife
$SkillRestriction[ShortSword] 	= $SkillPiercing @ " 300";	// short_sword
$SkillRestriction[Gladius] 		= $SkillPiercing @ " 400";	// Gladius
$SkillRestriction[AwlPike] 		= $SkillPiercing @ " 500";	// spear2
$SkillRestriction[Rapier] 		= $SkillPiercing @ " 600";	// katana
$SkillRestriction[Trident] 		= $SkillPiercing @ " 700";	// trident
$SkillRestriction[Lance] 		= $SkillPiercing @ " 800";	// phenssword
$SkillRestriction[Lacerator]	= $SkillPiercing @ " 900";	// knife
//________________________________________________________________________________________________________________________________________________
// Archery
$SkillRestriction[Sling] 				= $SkillArchery @ " 0";			// ""
$SkillRestriction[DartGun] 				= $SkillArchery @ " 60";		// sniper
$SkillRestriction[ShortBow] 			= $SkillArchery @ " 100";		// longbow
$SkillRestriction[LightCrossbow] 		= $SkillArchery @ " 200";		// crossbow
$SkillRestriction[CompositeBow] 		= $SkillArchery @ " 300";		// comp_bow
$SkillRestriction[HeavyCrossbow] 		= $SkillArchery @ " 400";		// Crossbowsteel
$SkillRestriction[SteelBow] 			= $SkillArchery @ " 500";		// steelbow
$SkillRestriction[ElvenBow] 			= $SkillArchery @ " 600";		// marblebow
$SkillRestriction[RepeatingCrossbow] 	= $SkillArchery @ " 700";		// shotgun
$SkillRestriction[WarBow] 				= $SkillArchery @ " 800";		// glassbow
$SkillRestriction[RockLauncher] 		= $SkillArchery @ " 900";		// mortar
//________________________________________________________________________________________________________________________________________________
// Ammo
$SkillRestriction[SmallRock] 			= $SkillArchery @ " 0";
$SkillRestriction[Quartz] 				= $SkillArchery @ " 0";
$SkillRestriction[Granite] 				= $SkillArchery @ " 0";
$SkillRestriction[Opal] 				= $SkillArchery @ " 0";
$SkillRestriction[Jade] 				= $SkillArchery @ " 0";
$SkillRestriction[Turquoise] 			= $SkillArchery @ " 0";
$SkillRestriction[Ruby] 				= $SkillArchery @ " 0";
$SkillRestriction[Topaz] 				= $SkillArchery @ " 0";
$SkillRestriction[Sapphire] 			= $SkillArchery @ " 0";
$SkillRestriction[Emerald] 				= $SkillArchery @ " 0";
$SkillRestriction[Diamond] 				= $SkillArchery @ " 0";

$SkillRestriction[BasicArrow] 		= $SkillArchery @ " 0";
$SkillRestriction[MetalArrow] 		= $SkillArchery @ " 0";
$SkillRestriction[BasicDart] 		= $SkillArchery @ " 0";
$SkillRestriction[MetalDart] 		= $SkillArchery @ " 0";
$SkillRestriction[BasicBolt] 		= $SkillArchery @ " 0";
$SkillRestriction[MetalBolt] 		= $SkillArchery @ " 0";
$SkillRestriction[PoisonDart] 		= $SkillArchery @ " 0";
$SkillRestriction[PoisonArrow] 		= $SkillArchery @ " 0";
$SkillRestriction[PoisonBolt] 		= $SkillArchery @ " 0";
$SkillRestriction[GasBomb] 			= $SkillArchery @ " 0";


//________________________________________________________________________________________________________________________________________________
// Wordsmith
//    Restriction[HAGGLING]			= ALWAYS ON
$SkillRestriction["#global"] 		= $SkillWordsmith @ " 0";
$SkillRestriction["#inscribe"] 		= $SkillWordsmith @ " 60";
$SkillRestriction["#render"] 		= $SkillWordsmith @ " 100";	
$SkillRestriction["#charm"] 		= $SkillWordsmith @ " 200";	
$SkillRestriction["#fool"] 			= $SkillWordsmith @ " 300";	
$SkillRestriction["#intimidate"] 	= $SkillWordsmith @ " 400";
$SkillRestriction["#rally"] 		= $SkillWordsmith @ " 500";
$SkillRestriction["#confuse"] 		= $SkillWordsmith @ " 600";
$SkillRestriction["#smash"] 		= $SkillWordsmith @ " 700";
$SkillRestriction["#inspire"] 		= $SkillWordsmith @ " 800";
$SkillRestriction["#coerce"] 		= $SkillWordsmith @ " 900";
//________________________________________________________________________________________________________________________________________________
// Thievery
$SkillRestriction["#steal"] 		= $SkillThievery @ " 20";	
$SkillRestriction["#swoop"] 		= $SkillThievery @ " 60";
$SkillRestriction["#blind"] 		= $SkillThievery @ " 100";
$SkillRestriction["#mug"] 			= $SkillThievery @ " 200";
$SkillRestriction["#hide"] 			= $SkillThievery @ " 300";
$SkillRestriction["#bleed"] 		= $SkillThievery @ " 400";
$SkillRestriction["#pickpocket"] 	= $SkillThievery @ " 500";
$SkillRestriction["#shadowwalk"] 	= $SkillThievery @ " 600";
$SkillRestriction["#leech"] 		= $SkillThievery @ " 700";
$SkillRestriction["#knockout"] 		= $SkillThievery @ " 800";
$SkillRestriction["#spy"] 			= $SkillThievery @ " 900";
//________________________________________________________________________________________________________________________________________________
// Survival
$SkillRestriction["#pvp"] 			= $SkillSurvival @ " 0";
$SkillRestriction["#compass"] 		= $SkillSurvival @ " 25";
$SkillRestriction["#recall"] 		= $SkillSurvival @ " 75";
$SkillRestriction["#trackpack"] 	= $SkillSurvival @ " 150";
$SkillRestriction["#track"] 		= $SkillSurvival @ " 225";
$SkillRestriction["#camp"] 			= $SkillSurvival @ " 300";
$SkillRestriction["#inspect"]		= $SkillSurvival @ " 375";
$SkillRestriction["#smith"] 		= $SkillSurvival @ " 450";
$SkillRestriction["#acrobatics"] 	= $SkillSurvival @ " 525";
$SkillRestriction["#leap"] 			= $SkillSurvival @ " 600";
$SkillRestriction["#brace"] 		= $SkillSurvival @ " 675";
$SkillRestriction["#exploit"] 		= $SkillSurvival @ " 750";
$SkillRestriction["#totem"]			= $SkillSurvival @ " 825";
$SkillRestriction["#sacrifice"] 	= $SkillSurvival @ " 900";


//________________________________________________________________________________________________________________________________________________
// Combat Arts 
$SkillRestriction[shove] 			= $SkillCombatArts @ " 20";
$SkillRestriction[speed1] 			= $SkillCombatArts @ " 60";
$SkillRestriction[atkecho1] 		= $SkillCombatArts @ " 120";
$SkillRestriction[rest] 			= $SkillCombatArts @ " 180";
$SkillRestriction[speed2] 			= $SkillCombatArts @ " 240";
$SkillRestriction[atkecho2] 		= $SkillCombatArts @ " 300";
$SkillRestriction[disarm]	 		= $SkillCombatArts @ " 360";
$SkillRestriction[speed3] 			= $SkillCombatArts @ " 420";
$SkillRestriction[taunt] 			= $SkillCombatArts @ " 480";
$SkillRestriction[atkecho3] 		= $SkillCombatArts @ " 540";
$SkillRestriction[charge]	 		= $SkillCombatArts @ " 600";
$SkillRestriction[speed4] 			= $SkillCombatArts @ " 660";
$SkillRestriction[warcry] 			= $SkillCombatArts @ " 720";
$SkillRestriction[atkecho4] 		= $SkillCombatArts @ " 780";
$SkillRestriction[speed5] 			= $SkillCombatArts @ " 840";
$SkillRestriction[atkecho5] 		= $SkillCombatArts @ " 900";
//________________________________________________________________________________________________________________________________________________
// Elemental spells
$SkillRestriction[stone] 		= $SkillElementalMagic @ " 20";
$SkillRestriction[ice] 			= $SkillElementalMagic @ " 60";
$SkillRestriction[spark] 		= $SkillElementalMagic @ " 120";
$SkillRestriction[fireball] 	= $SkillElementalMagic @ " 180";
$SkillRestriction[hail] 		= $SkillElementalMagic @ " 240";
$SkillRestriction[beam] 		= $SkillElementalMagic @ " 300";
$SkillRestriction[firebomb] 	= $SkillElementalMagic @ " 360";
$SkillRestriction[boulder] 		= $SkillElementalMagic @ " 420";
$SkillRestriction[shock] 		= $SkillElementalMagic @ " 480";
$SkillRestriction[petrify]		= $SkillElementalMagic @ " 540";
$SkillRestriction[melt] 		= $SkillElementalMagic @ " 600";
$SkillRestriction[shatter] 		= $SkillElementalMagic @ " 660";
$SkillRestriction[hellstorm] 	= $SkillElementalMagic @ " 720";
$SkillRestriction[sandstorm]	= $SkillElementalMagic @ " 780";
$SkillRestriction[cryostorm]	= $SkillElementalMagic @ " 840";
$SkillRestriction[radstorm]		= $SkillElementalMagic @ " 900";
//________________________________________________________________________________________________________________________________________________
// Illusion magic
$SkillRestriction[burden] 			= $SkillIllusionMagic @ " 20";
$SkillRestriction[aqualung]			= $SkillIllusionMagic @ " 60";
$SkillRestriction[barrier] 			= $SkillIllusionMagic @ " 120";
$SkillRestriction[mirage] 			= $SkillIllusionMagic @ " 180";
$SkillRestriction[illuminate]		= $SkillIllusionMagic @ " 240";
$SkillRestriction[cloud] 			= $SkillIllusionMagic @ " 300";
$SkillRestriction[fear]				= $SkillIllusionMagic @ " 360";
$SkillRestriction[reflect]			= $SkillIllusionMagic @ " 420";
$SkillRestriction[ironmaiden]		= $SkillIllusionMagic @ " 480";
$SkillRestriction[advbarrier]		= $SkillIllusionMagic @ " 540";
$SkillRestriction[massfear] 		= $SkillIllusionMagic @ " 600";
$SkillRestriction[crystalmaiden]	= $SkillIllusionMagic @ " 660";
$SkillRestriction[advcloud]			= $SkillIllusionMagic @ " 720";
$SkillRestriction[advreflect]		= $SkillIllusionMagic @ " 780";
$SkillRestriction[massreflect]		= $SkillIllusionMagic @ " 840";
$SkillRestriction[mimic] 			= $SkillIllusionMagic @ " 900";
//________________________________________________________________________________________________________________________________________________
// Distortion magic
$SkillRestriction[blink] 			= $SkillDistortionMagic @ " 30";	
$SkillRestriction[teleport] 		= $SkillDistortionMagic @ " 60";	
$SkillRestriction[rift] 			= $SkillDistortionMagic @ " 120";
$SkillRestriction[thorns] 			= $SkillDistortionMagic @ " 180";
$SkillRestriction[transport] 		= $SkillDistortionMagic @ " 240";
$SkillRestriction[advblink] 		= $SkillDistortionMagic @ " 300";
$SkillRestriction[glide] 			= $SkillDistortionMagic @ " 360";
$SkillRestriction[advteleport] 		= $SkillDistortionMagic @ " 420";
$SkillRestriction[teleswap]			= $SkillDistortionMagic @ " 480";
$SkillRestriction[advglide] 		= $SkillDistortionMagic @ " 540";
$SkillRestriction[massteleport] 	= $SkillDistortionMagic @ " 540";
$SkillRestriction[advtransport] 	= $SkillDistortionMagic @ " 600";
$SkillRestriction[advthorns]		= $SkillDistortionMagic @ " 660";
$SkillRestriction[flight] 			= $SkillDistortionMagic @ " 720";
$SkillRestriction[dimensionrift] 	= $SkillDistortionMagic @ " 780";
$SkillRestriction[advteleswap]		= $SkillDistortionMagic @ " 840";
$SkillRestriction[masstransport] 	= $SkillDistortionMagic @ " 840";
$SkillRestriction[advflight] 		= $SkillDistortionMagic @ " 900";
//________________________________________________________________________________________________________________________________________________
// Restoration magic
$SkillRestriction[shield] 		= $SkillRestorationMagic @ " 10";
$SkillRestriction[heal1] 		= $SkillRestorationMagic @ " 40";
$SkillRestriction[advheal1]		= $SkillRestorationMagic @ " 70";
$SkillRestriction[smite] 		= $SkillRestorationMagic @ " 100";
$SkillRestriction[protect] 		= $SkillRestorationMagic @ " 130";
$SkillRestriction[massheal1]	= $SkillRestorationMagic @ " 160";
$SkillRestriction[regen] 		= $SkillRestorationMagic @ " 190";
$SkillRestriction[manatap] 		= $SkillRestorationMagic @ " 220";
$SkillRestriction[heal2] 		= $SkillRestorationMagic @ " 250";
$SkillRestriction[guardian] 	= $SkillRestorationMagic @ " 280";
$SkillRestriction[advheal2]		= $SkillRestorationMagic @ " 310";
$SkillRestriction[advshield] 	= $SkillRestorationMagic @ " 370";
$SkillRestriction[advsmite] 	= $SkillRestorationMagic @ " 400";
$SkillRestriction[massheal2] 	= $SkillRestorationMagic @ " 430";
$SkillRestriction[advprotect]	= $SkillRestorationMagic @ " 460";
$SkillRestriction[advregen] 	= $SkillRestorationMagic @ " 490";
$SkillRestriction[heal3]		= $SkillRestorationMagic @ " 520";
$SkillRestriction[advmanatap] 	= $SkillRestorationMagic @ " 550";
$SkillRestriction[advguardian]	= $SkillRestorationMagic @ " 580";
$SkillRestriction[advheal3]		= $SkillRestorationMagic @ " 640";
$SkillRestriction[massshield] 	= $SkillRestorationMagic @ " 670";
$SkillRestriction[massheal3] 	= $SkillRestorationMagic @ " 700";
$SkillRestriction[masssmite] 	= $SkillRestorationMagic @ " 730";
$SkillRestriction[massprotect]	= $SkillRestorationMagic @ " 760";
$SkillRestriction[heal4]		= $SkillRestorationMagic @ " 790";
$SkillRestriction[massregen] 	= $SkillRestorationMagic @ " 820";
$SkillRestriction[massmanatap] 	= $SkillRestorationMagic @ " 850";
$SkillRestriction[advheal4]		= $SkillRestorationMagic @ " 880";
$SkillRestriction[massguardian]	= $SkillRestorationMagic @ " 910";
$SkillRestriction[fullheal]		= $SkillRestorationMagic @ " 940";
$SkillRestriction[massheal4] 	= $SkillRestorationMagic @ " 970";
$SkillRestriction[massfullheal]	= $SkillRestorationMagic @ " 1000";

$SkillRestriction[remort] 			= $SkillDistortionMagic @ " 0 " @ $MinLevel @ " 101";

//________________________________________________________________________________________________________________________________________________
// Geoastrics
$SkillRestriction[PickAxe] 			= $MinHouse @ " " @ $HouseIndex["College of Geoastrics"] @ " " @ $SkillMining @ " 0";
$SkillRestriction[HammerPick] 		= $MinHouse @ " " @ $HouseIndex["College of Geoastrics"] @ " " @ $SkillEndurance @ " 100";
$SkillRestriction[Laser] 			= $MinHouse @ " " @ $HouseIndex["College of Geoastrics"] @ " " @ $SkillFocus @ " 200";
$SkillRestriction[Driller] 			= $MinHouse @ " " @ $HouseIndex["College of Geoastrics"] @ " " @ $SkillStrength @ " 300";
$SkillRestriction[Reatomizer] 		= $MinHouse @ " " @ $HouseIndex["College of Geoastrics"] @ " " @ $SkillMining @ " 400";
//________________________________________________________________________________________________________________________________________________
// Order of Qod
$SkillRestriction["#brew"] 				= $MinHouse @ " " @ $HouseIndex["Order Of Qod"];
$SkillType["#brew"] 					= $SkillEnergy;
$AccessoryVar["#brew", $MiscInfo] 		= "The most devoted Ordertakers in the Order of Qod learn to #brew potions";
//________________________________________________________________________________________________________________________________________________
// Luminous Dawn
$SkillRestriction["#outpost"] 			= $MinHouse @ " " @ $HouseIndex["Luminous Dawn"];
$SkillType["#outpost"] 					= $SkillSurvival;
$AccessoryVar["#outpost", $MiscInfo]	= "Fully enlightened Luminous Dawn can construct an #outpost anywhere in the wilds";

$SkillRestriction["#waylink"] 			= $MinHouse @ " " @ $HouseIndex["Luminous Dawn"];
$SkillType["#waylink"] 					= $SkillWillpower;
$AccessoryVar["#waylink", $MiscInfo]	= "Luminous Dawn members have created a #fasttravel network using cystals syncronized with a #waylink";

$SkillRestriction["#fasttravel"] 		= $MinHouse @ " " @ $HouseIndex["Luminous Dawn"];
$SkillType["#fasttravel"] 				= $SkillFocus;
$AccessoryVar["#fasttravel", $MiscInfo]	= "The Luminous Dawn can #fasttravel between all #waylink crystals placed by any member";
//________________________________________________________________________________________________________________________________________________
// Wildenslayers
$SkillRestriction[SlayerGear] 		= $MinHouse @ " " @ $HouseIndex[Wildenslayers];
//________________________________________________________________________________________________________________________________________________
// The Keldrin Mandate
$SkillRestriction[PaddedShield] 	= $MinHouse @ " " @ $HouseIndex["Keldrin Mandate"] @ " " @ $SkillStrength @ " 0";
$SkillRestriction[PlateShield] 		= $MinHouse @ " " @ $HouseIndex["Keldrin Mandate"] @ " " @ $SkillStrength @ " 100";
$SkillRestriction[KnightShield] 	= $MinHouse @ " " @ $HouseIndex["Keldrin Mandate"] @ " " @ $SkillStrength @ " 200";
$SkillRestriction[BronzeShield] 	= $MinHouse @ " " @ $HouseIndex["Keldrin Mandate"] @ " " @ $SkillStrength @ " 300";
$SkillRestriction[DragonShield] 	= $MinHouse @ " " @ $HouseIndex["Keldrin Mandate"] @ " " @ $SkillStrength @ " 400";
$SkillRestriction[KeldriniteShield] = $MinHouse @ " " @ $HouseIndex["Keldrin Mandate"] @ " " @ $SkillStrength @ " 500";


//######################################################################################
// Skill functions
//######################################################################################

function GetNumSkills()
{
	dbecho($dbechoMode, "GetNumSkills()");

	for(%i = 1; $SkillDesc[%i] != ""; %i++){}
	return %i-1;
}

function GetCurrentSkillCap(%clientId) {
	%remorts 	= fetchData(%clientId, "RemortStep");
	%cycles 	= fetchData(%clientId, "CyclesSurvived");
	return (($skillRangePerLevel + %cycles + %remorts) * fetchData(%clientId, "LVL")) + 20 + %remorts + %cycles;
}

function AddSkillPoint(%clientId, %skill, %delta)
{
	dbecho($dbechoMode, "AddSkillPoint(" @ %clientId @ ", " @ %skill @ ", " @ %delta @ ")");

	if(%delta == "")
		%delta = 1;

	%ub = GetCurrentSkillCap(%clientId);
	if($PlayerSkill[%clientId, %skill] >= %ub)
		return False;

	%a = GetSkillMultiplier(%clientId, %skill) * %delta;
	%b = $PlayerSkill[%clientId, %skill];
	%c = %a + %b;
	%d = round(%c * 10);
	%e = (%d / 10) * 1.000001;

	$PlayerSkill[%clientId, %skill] = RoundToFirstDecimal(%e);
	return True;
}

function GetPlayerSkill(%clientId, %skill)
{
	return $PlayerSkill[%clientId, %skill];
}

function GetSkillMultiplier(%clientId, %skill) {
	%a = $SkillMultiplier[fetchData(%clientId, "CLASS"), %skill];
	%remorts 	= fetchData(%clientId, "RemortStep");
	%cycles 	= fetchData(%clientId, "CyclesSurvived");
	if(%remorts>0) {
		if(%cycles > 0) %cycles *= %remorts;
		else			%cycles = %remorts;
	}
	
	%c = Cap(%a + (%cycles * 0.1), "inf", 30.0);

	return RoundToFirstDecimal(%c);
}

function GetEXPmultiplier(%clientId) {
	%a = $EXPmultiplier[fetchData(%clientId, "CLASS")];
	%b = (fetchData(%clientId, "RemortStep") + fetchData(%clientId, "CyclesSurvived")) / 8;
	%c = %a + %b;
	return RoundToFirstDecimal(%c);
}

function SetAllSkills(%clientId, %n)
{
	dbecho($dbechoMode, "SetAllSkills(" @ %clientId @ ", " @ %n @ ")");

	for(%i = 1; $SkillDesc[%i] != ""; %i++)
		$PlayerSkill[%clientId, %i] = %n;
}

function SkillCanUse(%clientId, %thing, %replaceSkillToCheck)
{
	dbecho($dbechoMode, "SkillCanUse(" @ %clientId @ ", " @ %thing @ ")");

	if(%clientId.adminLevel >= 3)
		return True;
	if(Player::isAiControlled(%clientId)) {
		if(AI::HasLoadoutSkill(fetchData(%clientId,"RACE"), %thing)) {
			return true;		// All skills should be built to work below their minimum level. This way, AI can be given "skill kits".
		}
	}

	%flag = 0;
	%gc = 0;
	%gcflag = 0;
	for(%i = 0; GetWord($SkillRestriction[%thing], %i) != -1; %i+=2)
	{
		%s = GetWord($SkillRestriction[%thing], %i);
		%n = GetWord($SkillRestriction[%thing], %i+1);

		if(%replaceSkillToCheck) {
			%s = %replaceSkillToCheck;
		}

		if(%s == "L")
		{
			if(fetchData(%clientId, "LVL") < %n)
				%flag = 1;
		}
		else if(%s == "R")
		{
			if(fetchData(%clientId, "RemortStep") < %n)
				%flag = 1;
		}
		else if(%s == "A")
		{
			if(%clientId.adminLevel < %n)
				%flag = 1;
		}
		else if(%s == "G")
		{
			%gcflag++;
			if(String::ICompare(fetchData(%clientId, "GROUP"), %n) == 0)
				%gc = 1;
		}
		else if(%s == "C")
		{
			%gcflag++;
			if(String::ICompare(fetchData(%clientId, "CLASS"), %n) == 0)
				%gc = 1;
		}
		else if(%s == "H")
		{
			%hflag++;
			if(String::ICompare(fetchData(%clientId, "MyHouse"), $HouseName[%n]) == 0)
				%hh = 1;
		}
		else
		{
			if(GetSkillWithBonus(%clientId, %s) < %n)
				%flag = 1;
		}
	}

	//First, if there are any class/group restrictions, house restrictions, check these first.
	if(%gcflag > 0)
	{
		if(%gc == 0)
			%flag = 1;
	}
	if(%hflag > 0)
	{
		if(%hh == 0)
			%flag = 1;
	}

	
	if(%flag != 1)
		return True;
	else
		return False;
}

function UseSkill(%clientId, %skilltype, %successful, %showmsg, %multiplier)
{
	if(%skilltype == "" || %skilltype == 0) {
		return;
	}

	if(%multiplier <= 0 || %multiplier == "") %multiplier = 1;
	%multiplier = Cap(%multiplier,0.1,3);
	
	%base = 35;

	%ub = GetCurrentSkillCap(%clientId);
	if($PlayerSkill[%clientId, %skilltype] < %ub)
	{
		if(%successful)
			$SkillCounter[%clientId, %skilltype] += %multiplier;
		else
			$SkillCounter[%clientId, %skilltype] += (%multiplier * 0.25);

		%p = 1 - ($PlayerSkill[%clientId, %skilltype] / 1150);
		%e = round( (%base / Cap(GetSkillMultiplier(%clientId, %skilltype), 1, "inf") ) * %p );

		if($SkillCounter[%clientId, %skilltype] >= %e)
		{
			$SkillCounter[%clientId, %skilltype] = 0;
			%retval = AddSkillPoint(%clientId, %skilltype, 1);

			if(%retval)
			{
				if(%showmsg)
					Client::sendMessage(%clientId, $MsgBeige, "You have increased your skill in " @ $SkillDesc[%skilltype] @ " (" @ $PlayerSkill[%clientId, %skilltype] @ ")");
				if(%skilltype == $SkillEndurance)
					RefreshAll(%clientId);
			}
		}
	}
}


function rpg::SkillAdvancementProgress(%clientId, %skilltype) {
	if(%skilltype == "" || %skilltype == 0) {
		return;
	}
	%base = 35;
	%ub = GetCurrentSkillCap(%clientId);
	if($PlayerSkill[%clientId, %skilltype] < %ub) {
		%p = 1 - ($PlayerSkill[%clientId, %skilltype] / 1150);
		%e = round( (%base / Cap(GetSkillMultiplier(%clientId, %skilltype), 1, "inf") ) * %p );
		
		return Cap(($SkillCounter[%clientId, %skilltype] / %e) * 100, 0, 100);
	} else return 0;
}

//________________________________________________________________________________________________________________________________________________
function rpg::GetAdjustedSkillRestriction(%clientId, %thing, %skill) {
	if(%skill=="")
		%skill = $SkillType[%thing];
	%reduction = fetchData(%clientId, "remortStep") * 5;
	for(%i = 0; GetWord($SkillRestriction[%thing], %i) != -1; %i+=2) {
		%s = GetWord($SkillRestriction[%thing], %i);
		if(%s == %skill)
			return Cap( GetWord($SkillRestriction[%thing], %i+1) - %reduction, 0, "inf");
	}
	return 0;
}

//________________________________________________________________________________________________________________________________________________
function rpg::ListAdjustedSkillRestrictions(%clientId, %thing) {
	%t = "";
	%reduction = fetchData(%clientId, "remortStep") * 5;
	for(%i = 0; GetWord($SkillRestriction[%thing], %i) != -1; %i+=2) {
		%s = GetWord($SkillRestriction[%thing], %i);
		%n = GetWord($SkillRestriction[%thing], %i+1);
		if(%s == "H")	%t = %t @ $HouseName[%n] @ ", ";
		else 			%t = %t @ $SkillDesc[%s] @ ": " @ Cap(%n - %reduction, 0, "inf") @ ", ";
	}
	if(%t == "")	%t = "None";
	else			%t = String::getSubStr(%t, 0, String::len(%t)-2);
	
	return %t;
}
