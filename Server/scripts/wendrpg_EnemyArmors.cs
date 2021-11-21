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



//_________________________________________________________________________________________________________________________________________________________
// Greenskins
rpg::DefineEnemyRace("Goblin", "male", "GoblinArmor::rpgorc", "Greenskins", 1, 5, "Greenskin 1%25", "stone shove",
	"SoundGoblinDeath1",  "SoundGoblinAcquired1", "SoundGoblinTaunt1",  "SoundGoblinRandom1", "SoundGoblinHit1 SoundGoblinHit2");
rpg::DefineEnemyRace("GoblinFemale", "female", "FemaleLightArmor::rpgorc", "Greenskins", 1, 5, "Greenskin 1%25", "stone shove",
	"SoundGoblinDeath1",  "SoundGoblinAcquired1", "SoundGoblinTaunt1",  "SoundGoblinRandom1", "SoundGoblinHit1 SoundGoblinHit2");
	
rpg::DefineEnemyRace("Orc", "male", "MaleMediumArmor::rpgorc","Greenskins", 7, 25, "BlackStatue 2%25 Greenskin 2%50", "#blind charge shove stone fireball advshield heal taunt fear",
	"Soundorcdeath",  "Soundorcacquired", "Soundorctaunt",  "Soundorcidle", "Soundorchit1 Soundorchit2");	
rpg::DefineEnemyRace("OrcFemale", "female", "FemaleMediumArmor::rpgorc","Greenskins", 7, 25, "BlackStatue 2%25 Greenskin 2%50", "#blind shove stone advheal1 shield burden",
	"Soundorcfemdeath",  "Soundorcfemacquired", "Soundorcfemtaunt",  "Soundorcfemidle", "Soundorcfemhit1 Soundorcfemhit2");
	
rpg::DefineEnemyRace("Ogre", "", "HeavyArmor::rpgorc","Greenskins", 8, 0, "BlackStatue 1%75 Greenskin 4%75", "taunt petrify shatter #brace radstorm boulder",
	"SoundOgreDeath1", "SoundOgreAcquired1", "SoundOgreTaunt1", "SoundOgreRandom1", "SoundOgreHit1");
	
	
//_________________________________________________________________________________________________________________________________________________________
// Gnolls
rpg::DefineEnemyRace("Gnoll", "male", "GnollArmor::rpggnoll","Gnoll", 4, 25, "GnollHide 1%25 SmallRock 100%15 Opal 1%10 Jade 1%10", "charge burden stone shield",
	"SoundGnollDeath1", "SoundGnollAcquired1", "SoundGnollTaunt1", "SoundGnollRandom1", "SoundGnollHit1 SoundGnollHit2");		
rpg::DefineEnemyRace("GnollFemale", "female", "FemaleLightArmor::rpggnoll","Gnoll", 4, 25, "GnollHide 1%25 SmallRock 100%15 Opal 1%10 Jade 1%10", "advmanatap blink heal1",
	"Soundgnollfemdeath", "Soundgnollfemacquired", "Soundgnollfemtaunt", "Soundgnollfemidle", "Soundgnollfemhit1 Soundgnollfemhit2");
			
			
//_________________________________________________________________________________________________________________________________________________________			
// Elf-like
rpg::DefineEnemyRace("Elf", "male", "MaleLightArmor::rpgelf","Elf", 15, 75, "ElfEar 2%5 Jade 10%10 Emerald 1,1:1000 EnchantedStone 5%3", "advshield heal2 advheal2 advprotect advguardian smite beam advsmite spark teleswap advteleswap blink #swoop #leap",
	"Soundelfdeath1", "Soundelfacquired", "Soundelftaunt", "Soundelfidle", "Soundelfhit1 Soundelfhit2");		
rpg::DefineEnemyRace("ElfFemale", "female", "FemaleLightArmor::rpgelf","Elf", 15, 75, "ElfEar 2%5 Jade 10%10 Emerald 1,1:1000", "advshield heal2 advheal2 advprotect advguardian smite beam advsmite spark teleswap advteleswap blink #swoop #leap",
	"Soundelffemdeath", "Soundelffemacquired", "Soundelffemtaunt", "Soundelffemidle", "Soundelffemhit1 Soundelffemhit2");
		
rpg::DefineEnemyRace("Antanari", "male", "MaleLightArmor::antanari","Enemy", 15, 50, "AntanariMask 1%5 Gold 25%25 EnchantedStone 5%3", "smite advsmite warcry #leap advreflect advprotect advguardian",
	"Soundelfdeath1", "Soundelfacquired", "Soundelftaunt", "Soundelfidle", "Soundelfhit1 Soundelfhit2");			
rpg::DefineEnemyRace("AntanariFemale", "female", "FemaleLightArmor::antanari","Enemy", 15, 50, "AntanariMask 1%5 Gold 25%25", "smite advsmite warcry #leap advreflect advprotect advguardian",
	"Soundelffemdeath", "Soundelffemacquired", "Soundelffemtaunt", "Soundelffemidle", "Soundelffemhit1 Soundelffemhit2");

rpg::DefineEnemyRace("Jheriman", "male", "MaleLightArmor::jheriman","Enemy", 15, 90, "Effigy 1%5 Jade 3%5 EnchantedStone 5%3", "advthorns spark advshield advreflect heal1 advheal2 heal3",
	"Soundelfdeath1", "Soundelfacquired", "Soundelftaunt", "Soundelfidle", "Soundelfhit1 Soundelfhit2");			
rpg::DefineEnemyRace("JherimanFemale", "female", "FemaleLightArmor::jheriman","Enemy", 15, 90, "Effigy 1%5 Jade 5%20", "advthorns spark advshield advreflect heal1 advheal2 heal3",
	"Soundelffemdeath", "Soundelffemacquired", "Soundelffemtaunt", "Soundelffemidle", "Soundelffemhit1 Soundelffemhit2");
	
rpg::DefineEnemyRace("Craven", "male", "MaleLightArmor::craven","Enemy", 15, 0, "LettingBlade 10%3 Ruby 7%80 EnchantedStone 5%3", "#bleed #leech #mug #swoop advteleswap advmanatap",
	"Soundelfdeath1", "Soundelfacquired", "Soundelftaunt", "Soundelfidle", "Soundelfhit1 Soundelfhit2");			
rpg::DefineEnemyRace("CravenFemale", "female", "FemaleLightArmor::craven","Enemy", 15, 0, "LettingBlade 10%3 Ruby 7%80", "#bleed #leech #mug #swoop advteleswap advmanatap",
	"Soundelffemdeath", "Soundelffemacquired", "Soundelffemtaunt", "Soundelffemidle", "Soundelffemhit1 Soundelffemhit2");
	
rpg::DefineEnemyRace("Kymera", "male", "MaleLightArmor::kymera","Enemy", 15, 150, "ClippedWing 2%5 Sapphire 10%10 EnchantedStone 5%3", "ice advbarrier #leap",
	"Soundelfdeath1", "Soundelfacquired", "Soundelftaunt", "Soundelfidle", "Soundelfhit1 Soundelfhit2");			
rpg::DefineEnemyRace("KymeraFemale", "female", "FemaleLightArmor::kymera","Enemy", 15, 150, "ClippedWing 2%5 Sapphire 10%10", "ice advbarrier #leap",
	"Soundelffemdeath", "Soundelffemacquired", "Soundelffemtaunt", "Soundelffemidle", "Soundelffemhit1 Soundelffemhit2");


//_________________________________________________________________________________________________________________________________________________________
// Minotaurs	
rpg::DefineEnemyRace("Ratman", "male", "MaleLightArmor::min","Minotaur", 20, 50, "RatFur 1%10", "#swoop #bleed #leech",
	"SoundGnollDeath1", "SoundGnollAcquired1", "SoundGnollTaunt1", "SoundGnollRandom1", "SoundGnollHit1 SoundGnollHit2");		
rpg::DefineEnemyRace("Ratwoman", "female", "FemaleLightArmor::min","Minotaur", 20, 50, "RatFur 1%10", "#leech #bleed #swoop",
	"Soundgnollfemdeath", "Soundgnollfemacquired", "Soundgnollfemtaunt", "Soundgnollfemidle", "Soundgnollfemhit1 Soundgnollfemhit2");

rpg::DefineEnemyRace("Minotaur", "male", "MinotaurArmor::min","Minotaur", 20, 0, "BlackStatue 5%15 MinotaurHorn 2%75", "charge taunt firebomb boulder #leap shove",
	"Soundminotaurdeath1", "Soundminotauracquired1", "Soundminotaurtaunt", "Soundminotauridle", "Soundminotaurhit1");			
rpg::DefineEnemyRace("MinotaurFemale", "female", "FemaleMediumArmor::min","Minotaur", 20, 0, "BlackStatue 5%15 MinotaurHorn 2%50", "atkecho1 atkecho3 charge taunt firebomb boulder",
	"Soundminotaurfemdeath", "Soundminotaurfemacquired", "Soundminotaurfemtaunt", "Soundminotaurfemidle", "Soundminotaurfemhit1");

//_________________________________________________________________________________________________________________________________________________________
// Golems
rpg::DefineEnemyRace("Churl", "male", "MaleMediumArmor::fedmonster","Golem", 8, 30, "ChurlEye 1%25 Sulfur 3%50", "boulder shove teleswap",
	"SoundUberDeath1", "SoundUberAcquired1", "SoundUberTaunt", "SoundUberRandom1", "SoundUberHit1");
rpg::DefineEnemyRace("Cragspawn", "", "MaleLightArmor::fedmonster","Golem", 16, 0, "Cobalt 3%13 Turquoise 66,1:13", "ice hail cryostorm barrier #brace advblink",
	"SoundUberDeath1", "SoundUberAcquired1", "SoundUberTaunt", "SoundUberRandom1", "SoundUberHit1");
rpg::DefineEnemyRace("Golem", "", "GolemArmor::fedmonster","Golem", 24, 0, "Obsidian 13,1:666 Granite 100%50 Opal 100%20 Diamond 100%2 Keldrinite 1%3 ", "sandstorm shatter barrier reflect shield advshield massshield protect guardian",
	"SoundUberDeath1", "SoundUberAcquired1", "SoundUberTaunt", "SoundUberRandom1", "SoundUberHit1");
rpg::DefineEnemyRace("Ancient", "", "AncientArmor::ancient","Enemy", 32, 0, "Mercury 10%25 Keldrinite 1%3 EnergyCrystal 5%50", "radstorm shatter melt advsmite masssmite massshield #rally #smash #inspire atkecho4 #brace",
	"SoundUberDeath1", "SoundUberAcquired1", "SoundUberAcquired2", "SoundUberRandom1", "SoundHitPlate");


//_________________________________________________________________________________________________________________________________________________________
// Undead
rpg::DefineEnemyRace("LostSoul", "", "LostSoulArmor::lostsoul","Enemy", 1, 0, "VileSubstance 13,1:666 MeatStick 1%10 MeatSlab 1%10", "stone shove",
	"Soundlostsouldeath", "Soundlostsoulacquired", "Soundlostsoultaunt", "Soundlostsoulidle", "Soundlostsoulhit1 Soundlostsoulhit2");
	
rpg::DefineEnemyRace("Zombie", "", "ZombieArmor::undead","Undead", 15, 0, "Toxin 13,1:666", "#leech fear massfear atkecho1",
	"SoundUndeadDeath1", "SoundUndeadAcquired1", "SoundUndeadTaunt1", "SoundUndeadRandom1", "SoundUndeadHit1 SoundUndeadHit2");
	
rpg::DefineEnemyRace("Skeleton", "", "SkeletonArmor::skel","Undead", 11, 0, "SkeletonBone 5%5", "advthorns fear taunt warcry #bleed firebomb fireball melt",
	"Soundskeletondeath", "Soundskeletonacquired", "Soundskeletontaunt", "Soundskeletonidle", "Soundskeletonhit1 Soundskeletonhit2");


//_________________________________________________________________________________________________________________________________________________________
// Demons
rpg::DefineEnemyRace("Imp", "", "ImpArmor::imp","Enemy", 15, 75, "ImpClaw 12%3", "fireball #leap spark",
	"Soundimpdeath", "Soundimpacquired", "Soundimptaunt", "Soundimpidle", "Soundimphit1 Soundimphit2");
	
rpg::DefineEnemyRace("Demon", "male", "MaleMediumArmor::demon","Undead", 20, 0, "DemonRune 1%5 BloodyPentagram 1%13", "hellstorm firebomb #leech #bleed #leap #intimidate #confuse",
	"Sounddemondeath", "Sounddemonacquired", "Sounddemontaunt", "Sounddemonidle", "Sounddemonhit1 Sounddemonhit2");			
rpg::DefineEnemyRace("DemonFemale", "female", "FemaleLightArmor::demon","Undead", 20, 75, "DemonRune 1%5 BloodyPentagram 1%13", "fireball shatter #leech #bleed #leap #intimidate #confuse",
	"Sounddemonfemdeath", "Sounddemonfemacquired", "Sounddemonfemtaunt", "Sounddemonfemidle", "Sounddemonfemhit1 Sounddemonfemhit2");


//_________________________________________________________________________________________________________________________________________________________
// Humans
rpg::DefineEnemyRace("Traveller", "male", "MaleLightArmor::traveller","Enemy", 8, 50, "DragonScale 1%3 EnergyPotion 1%10 WeakHealthPotion 5%10 HealthPotion 5%10", "#brace charge #leap advshield advheal",
	"Soundoutlawdeath", "Soundoutlawtaunt", "Soundelftaunt", "Soundelfidle", "Soundelfhit1 Soundelfhit2 Soundoutlawhit1 Soundoutlawhit2");
rpg::DefineEnemyRace("TravellerFemale", "female", "FemaleLightArmor::traveller","Enemy", 8, 50, "DragonScale 1%3 EnergyPotion 1%10 WeakHealthPotion 5%10 HealthPotion 5%10", "#brace charge #leap advshield advheal",
	"Soundamazondeath", "Soundoutlawfemaleacquired", "Sounddeludedtaunt1", "Soundoutlawfemaletaunt", "Soundamazonhit1 Soundamazonhit2 Soundoutlawfemalehit1 Soundoutlawfemalehit2");

rpg::DefineEnemyRace("Amazon", "female", "FemaleLightArmor::amazon","Enemy", 8, 90, "StolenGarments 1%5 VialOfStamina 1%10 EnergyScroll 10%20", "#charm #mug burden cloud disarm thorns advheal advshield advguardian",
	"Soundamazondeath", "Soundamazonacquired", "Soundamazontaunt", "Soundamazonidle", "Soundamazonhit1 Soundamazonhit2");

rpg::DefineEnemyRace("Deluded", "male", "MaleRobedArmor::deluded","Enemy", 13, 150, "ManaStone 1/-10000 ControlCrystal 1%25", "spark shock melt heal advreflect smite advteleswap",
	"Soundoutlawdeath", "Soundoutlawacquired", "Soundoutlawtaunt", "Soundoutlawidle", "Soundoutlawhit1 Soundoutlawhit2");			
rpg::DefineEnemyRace("DeludedFemale", "female", "FemaleRobedArmor::deluded","Enemy", 13, 150, "ManaStone 1/-10000 ControlCrystal 1%25", "spark shock melt heal advreflect smite advteleswap",
	"Sounddeludeddeath", "Sounddeludedacquired", "Sounddeludedtaunt1", "Sounddeludedidle", "Sounddeludedhit1 Sounddeludedhit2");

rpg::DefineEnemyRace("Outlaw", "male", "MaleRobedArmor::outlaw","Enemy", 20, 50, "Gold 100%3 COINS 1200~5000", "#steal #swoop #hide #knockout atkecho1",
	"Soundoutlawdeath", "Soundoutlawacquired", "Soundoutlawtaunt", "Soundoutlawidle", "Soundoutlawhit1 Soundoutlawhit2");			
rpg::DefineEnemyRace("OutlawFemale", "female", "FemaleLightArmor::outlaw","Enemy", 20, 50, "Gold 100%3 COINS 1200~5000", "#steal #swoop #hide #knockout atkecho1",
	"Soundoutlawfemaledeath", "Soundoutlawfemaleacquired", "Soundoutlawfemaletaunt", "Soundoutlawfemaleidle", "Soundoutlawfemalehit1 Soundoutlawfemalehit2");

rpg::DefineEnemyRace("Savage", "male", "MaleLightArmor::savage","Enemy", 2, 50, "SavageHeart 1%5 LoinCloth 1%50", "stone shove",
	"Soundoutlawdeath", "Soundoutlawacquired", "Soundoutlawtaunt", "Soundoutlawidle", "Soundoutlawhit1 Soundoutlawhit2");			
rpg::DefineEnemyRace("SavageFemale", "female", "FemaleLightArmor::savage","Enemy", 2, 50, "SavageHeart 1%5 LoinCloth 2%50", "stone shove",
	"Soundamazondeath", "Soundamazonacquired", "Soundamazontaunt", "Soundamazonidle", "Soundamazonhit1 Soundamazonhit2");



//________________________________________________________________________________________________
// Bosses/etc
rpg::DefineEnemyRace("Mandatory", "male", "MaleLightArmor::mandate","Enemy", 8, 50, "DragonScale 1%3 EnergyPotion 1%10 WeakHealthPotion 5%10 HealthPotion 5%10", "#brace #rally #inspire advsmite heal3 advheal2 guardian",
	"Soundoutlawdeath", "Soundoutlawtaunt", "Soundelftaunt", "Soundelfidle", "Soundelfhit1 Soundelfhit2 Soundoutlawhit1 Soundoutlawhit2");
rpg::DefineEnemyRace("MandatoryFemale", "female", "FemaleLightArmor::mandate","Enemy", 8, 50, "DragonScale 1%3 EnergyPotion 1%10 WeakHealthPotion 5%10 HealthPotion 5%10", "#brace advsmite taunt heal2 advheal1 guardian",
	"Soundamazondeath", "Soundoutlawfemaleacquired", "Sounddeludedtaunt1", "Soundoutlawfemaletaunt", "Soundamazonhit1 Soundamazonhit2 Soundoutlawfemalehit1 Soundoutlawfemalehit2");

rpg::DefineEnemyRace("Hermit", "", "HeavyArmor::ancient","Golem", 24, 0, "Obsidian 100%50", "shatter rift petrify barrier reflect thorns atkecho3",
	"SoundUberDeath1", "SoundUberAcquired1", "SoundUberTaunt", "SoundUberRandom1", "SoundUberHit1");

rpg::DefineEnemyRace("PrimeEvil", "male", "MinotaurArmor::demon", "Undead", 20, 0, "DemonRune 1 BloodyPentagram 13", "hellstorm #leech #bleed #leap #intimidate #confuse",
	"Sounddemondeath", "Sounddemonacquired", "Sounddemontaunt", "Sounddemonidle", "Sounddemonhit1 Sounddemonhit2");			

rpg::DefineEnemyRace("Beastly", "male", "MinotaurArmor::min","Minotaur", 20, 0, "MinotaurHorn 2%75", "charge taunt #leap atkecho2 #sacrifice",
	"SoundOgreDeath1", "SoundOgreAcquired1", "SoundOgreTaunt1", "SoundOgreRandom1", "SoundOgreHit1");
//________________________________________________________________________________________________
// Civilian/misc bot types
rpg::DefineEnemyType("generic", 	"MaleHuman", "Bard", "1", 0, "Knife 1");
//________________________________________________________________________________________________
// Enemies
rpg::DefineEnemyType("Hethen", 		"Savage", "Druid", "1~5", 0, "Knife 1 EnergyDrink 2%10");
rpg::DefineEnemyType("Miscreant", 	"Savage", "Brawler", "3~4", 0, "Hatchet 1 HealingHerbs 2%10");

rpg::DefineEnemyType("Nymph", 		"SavageFemale", "Invoker", "2~4", 0, "Hatchet 1 EnergyDrink 2%10");
rpg::DefineEnemyType("Sinner", 		"SavageFemale", "Fighter", "2~5", 0, "Knife 1 HealingHerbs 2%10");

rpg::DefineEnemyType("Trodden", 	"LostSoul", "Fighter", "1~5", 0, "BoneClub 1 Quartz 1%15");
rpg::DefineEnemyType("Forgotten", 	"LostSoul", "Fighter", "2~3", 0, "BoneClub 1 Granite 5%20");
rpg::DefineEnemyType("Runaway", 	"LostSoul", "Thief", "1~6", 0, "Sling 1 SmallRock 333/50");
rpg::DefineEnemyType("Wanderer", 	"LostSoul", "Invoker", "3~6", 0, "");
rpg::DefineEnemyType("Neglected", 	"LostSoul", "Invoker", "4~6", 0, "CastingBlade 1");
rpg::DefineEnemyType("Virulant", 	"LostSoul", "Fighter", "5~15", 0, "Sling 1 SmallRock 500/50");
rpg::DefineEnemyType("Rabid", 		"LostSoul", "Bard", "5~15", 0, "Knife 1 MeatSlab 5%20");
rpg::DefineEnemyType("Tosser", 		"LostSoul", "Brawler", "5~15", 0, "Sling 1 SmallRock 333/50");

rpg::DefineEnemyType("Runt", 		"Goblin", "Fighter", "1~3", 0, "BoneClub 1 Granite 4%10");
rpg::DefineEnemyType("Thief", 		"Goblin", "Thief", "3~7", 0, "Sling 1 SmallRock 333/50");
rpg::DefineEnemyType("Wizard", 		"Goblin", "Mage", "1~4", 0, "Quartz 10,1:50");
rpg::DefineEnemyType("Raider", 		"Goblin", "Fighter", "3~6", 0, "Knife 1");

rpg::DefineEnemyType("Slunt", 		"GoblinFemale", "Fighter", "1~3", 0, "BoneClub 1 Granite 8%5");
rpg::DefineEnemyType("Tick", 		"GoblinFemale", "Thief", "3~6", 0, "Sling 1 SmallRock 333/50");
rpg::DefineEnemyType("Witch", 		"GoblinFemale", "Mage", "1~4", 1, "CastingBlade 1 Quartz 10,1:50");
rpg::DefineEnemyType("Digger",		"GoblinFemale", "Fighter", "5~7", 0, "Knife 1 Quartz 10,1:50 Granite 10,1:50 Opal 3,1:300");

rpg::DefineEnemyType("Pup", 		"Gnoll", "Fighter", "1~3", 0, "BoneClub 1 DriedBerries 1 WoodChip 5%10");
rpg::DefineEnemyType("Shaman", 		"Gnoll", "Mage", "1~10", 0, "Opal 5,1:1000");
rpg::DefineEnemyType("Scavenger", 	"Gnoll", "Ranger", "1~5", 0, "Hatchet 1 Ruby 2/-5000 Opal 1,1:1000 TreeSap 1%3");
rpg::DefineEnemyType("Hunter", 		"Gnoll", "Ranger", "1~7", 0, "Knife 1 CheetaursPaws 1,1:50000");

rpg::DefineEnemyType("Shrike", 		"GnollFemale", "Thief", "1~7", 1, "Sling 1 SmallRock 500/50");
rpg::DefineEnemyType("Gatherer", 	"GnollFemale", "Ranger", "1~5", 0, "Knife 1");
rpg::DefineEnemyType("Pathmaker", 	"GnollFemale", "Ranger", "1~10", 3, "Sling 1 TreeSap 1%3");

rpg::DefineEnemyType("Warlock", 	"Orc", "Invoker", "15~20", 0, "QuarterStaff 1");
rpg::DefineEnemyType("Berserker", 	"Orc", "Fighter", "8-14", 0, "BroadSword 1 Topaz 4/-500");
rpg::DefineEnemyType("Ravager", 	"Orc", "Fighter", "20~25", 0, "StoneAxe 1 MeatSlab 3/30 Jade 1/-300");
rpg::DefineEnemyType("Slayer", 		"Orc", "Fighter", "10~15", 0, "OrcishAxe 1,1:100 WarAxe 1 Jade 5/-1000");
rpg::DefineEnemyType("Roughskin", 	"Orc", "Fighter", "10~15", 0, "Club 1 Jade 5/-1000");

rpg::DefineEnemyType("Breeder", 	"OrcFemale", "Merchant", "5~15", 0, "Sling 1 SmallRock 400/-50");
rpg::DefineEnemyType("Slave", 		"OrcFemale", "Merchant", "5~15", 0, "BoneClub 1 Granite 10%50");
rpg::DefineEnemyType("Healer", 		"OrcFemale", "Cleric", "20~30", 0, "QuarterStaff 1 EnergyRock 1/-50");

rpg::DefineEnemyType("Communor", 	"Jheriman", "Druid", "10~30", 1, "DartGun 1 MetalDart 550/50");
rpg::DefineEnemyType("Prospector", 	"Jheriman", "Ranger", "10~30", 0, "Spear 1");

rpg::DefineEnemyType("Treespeaker",	"JherimanFemale", "Druid", "10~30", 1, "QuarterStaff 1");
rpg::DefineEnemyType("Valuer", 		"JherimanFemale", "Ranger", "10~30", 0, "DartGun 1 BasicDart 550/50");

rpg::DefineEnemyType("Ripheart", 	"Churl", "Fighter", "6~15", 0, "Knife 1");
rpg::DefineEnemyType("Snapback", 	"Churl", "Brawler", "6~15", 0, "Hammer 1");
rpg::DefineEnemyType("Breakneck", 	"Churl", "Fighter", "6~15", 0, "Hammer 1");
rpg::DefineEnemyType("Master", 		"Golem", "Enchanter", "20~30", 0, "FrozenWand 1");

rpg::DefineEnemyType("Lockjaw", 	"Churl", "Invoker", "15~25", 0, "QuarterStaff 1");
rpg::DefineEnemyType("Spinetap", 	"Churl", "Fighter", "15~25", 0, "Thrown 1 ThrowingKnife 300 Knife 1");
rpg::DefineEnemyType("Limbtore", 	"Churl", "Brawler", "15~25", 0, "Hatchet 1");
rpg::DefineEnemyType("Controller", 	"Golem", "Fighter", "25~40", 0, "StoneAxe 1");

rpg::DefineEnemyType("Ruffian", 	"Ogre", "Brawler", "25~40", 0, "StoneAxe 1 Quartz 8/-200");
rpg::DefineEnemyType("Destroyer", 	"Ogre", "Mage", "25~40", 0, "Smoker 1");
rpg::DefineEnemyType("Halberdier", 	"Ogre", "Brawler", "35~40", 0, "Halberd 1 HealthPotion 3/30");
rpg::DefineEnemyType("Dreadnought",	"Ogre", "Mage", "25~40", 0, "Smoker 1 Diamond 1/-10000");
rpg::DefineEnemyType("Magi", 		"Ogre", "Mage", "25~40", 0, "CastingBlade 1 Sapphire 10/-2000");

rpg::DefineEnemyType("Follower",	"DeludedFemale", "Cleric", "25~35", 0, "Club 1 EnergyPotion 1/-50 Ruby 5/-250");
rpg::DefineEnemyType("Sycophant", 	"Deluded", "Fighter", "25~45", 0, "ShortBow 1 PoisonArrow 300~500 Knife 1 Topaz 5/-250");
rpg::DefineEnemyType("Servant", 	"DeludedFemale", "Invoker", "30~40", 0, "FlintCaster 1 EnergyCrystal 2/-200 Topaz 1/-100");
rpg::DefineEnemyType("Chosen", 		"DeludedFemale", "Invoker", "40~45", 1, "LongStaff 1 Ruby 1/-111");
rpg::DefineEnemyType("Convert", 	"DeludedFemale", "Druid", "25~45", 0, "QuarterStaff 1 Ruby 1/-111");
rpg::DefineEnemyType("Radical", 	"Deluded", "Enchanter", "25~45", 0, "BroadSword 1 Ruby 1/-111");
rpg::DefineEnemyType("Fanatic", 	"Deluded", "Enchanter", "35~40", 0, "LongSword 1 Ruby 1/-111");

rpg::DefineEnemyType("Brigand", 	"Traveller", "Fighter", "30~40", 0, "ShortSword 1");
rpg::DefineEnemyType("Marauder", 	"Traveller", "Brawler", "30~50", 0, "Halberd 1");
rpg::DefineEnemyType("Swordsman", 	"Traveller", "Paladin", "40~50", 0, "LongSword 1");
rpg::DefineEnemyType("Medicineman",	"Traveller", "Cleric", "50~60", 1, "SpikedClub 1");
rpg::DefineEnemyType("Archer", 		"Traveller", "Fighter", "30~50", 0, "CompositeBow 1 BasicArrow 1000/50");
rpg::DefineEnemyType("Miner", 		"Traveller", "Merchant", "30~60", 0, "Knife 1 Hatchet 1 Diamond 10,1:10000 Jade 10,1:2000 Topaz 3,1:1000 Opal 10,1:100 Quartz 10,1:50");

rpg::DefineEnemyType("Spy",		 	"TravellerFemale", "Fighter", "30~40", 0, "ShortSword 1");
rpg::DefineEnemyType("Swordsong",	"TravellerFemale", "Paladin", "40~50", 0, "LongSword 1");
rpg::DefineEnemyType("Sapper", 		"TravellerFemale", "Fighter", "30~50", 0, "LightCrossbow 1 BasicBolt 1000/50");
rpg::DefineEnemyType("Guard",	 	"MandatoryFemale", "Brawler", "50~60", 1, "SpikedClub 1 ThiefWrit 1");

rpg::DefineEnemyType("Mauler", 		"Zombie", "Brawler", "50~60", 0, "Mace 1 Ruby 3/-300");
rpg::DefineEnemyType("Thrasher", 	"Zombie", "Fighter", "30~50", 0, "Club 1 Ruby 3/-300");
rpg::DefineEnemyType("Ripper", 		"Zombie", "Fighter", "20~60", 0, "WarAxe 1 Ruby 3/-300");
rpg::DefineEnemyType("Flesheater", 	"Zombie", "Thief", "20~50", 0, "Dagger 1 Ruby 3/-300");

rpg::DefineEnemyType("Seductress", 	"Amazon", "Enchanter", "50~65", 0, "ShortSword 1 EnergyScroll 1/-10 Ruby 5/-250");
rpg::DefineEnemyType("Siren", 		"Amazon", "Druid", "50~65", 0, "CastingBlade 1 EnergyVial 2/-200 Topaz 1/-100");
rpg::DefineEnemyType("Seeker", 		"Amazon", "Bard", "50~65", 0, "SpikedBoneClub 1 Topaz 5/-250");
rpg::DefineEnemyType("Sniper", 		"Amazon", "Ranger", "50~65", 0, "HeavyCrossbow 1 BasicBolt 400/50 Ruby 1/-111");

rpg::DefineEnemyType("Protector", 	"Elf", "Paladin", "45~60", 1, "ElvenSword 1 Jade 2/-500");
rpg::DefineEnemyType("Lord", 		"Elf", "Druid", "60~70", 3, "ElvenBow 1 MetalArrow 800/100 Emerald 10%10");
rpg::DefineEnemyType("Champion", 	"Elf", "Paladin", "50~60", 1, "ElvenSword 1 Jade 3/-1000");
rpg::DefineEnemyType("Historian", 	"Elf", "Bard", "40~50", 1, "ElvenSword 1 Jade 5/-500");

rpg::DefineEnemyType("Peacekeeper", "ElfFemale", "Druid", "60~70", 1, "ElvenBow 1 BasicArrow 1000 Jade 5/-500");
rpg::DefineEnemyType("Conjurer", 	"ElfFemale", "Mage", "40~70", 1, "CastingBlade 1 Jade 2/-300");

rpg::DefineEnemyType("Leper", 		"Imp", "Invoker", "66~100", 0, "FlameThrower 1 Ruby 8/-200");
rpg::DefineEnemyType("Wretch", 		"Imp", "Invoker", "66~100", 0, "FlameThrower 1");
rpg::DefineEnemyType("Spawn", 		"Imp", "Invoker", "66~100", 0, "FlameThrower 1 VialOfStamina 1/30");

rpg::DefineEnemyType("Bloodletter", "Craven", "Enchanter", "10~20", 2, "Thrown 1 ThrowingKnife 800/100 Bandages 1~10");
rpg::DefineEnemyType("Sanguine", 	"Craven", "Thief", "10~20", 3, "Thrown 1 ThrowingKnife 800/100 Bandages 1~10");

rpg::DefineEnemyType("Coagulant", 	"CravenFemale", "Thief", "10~83", 4, "Thrown 1 ThrowingKnife 800/100 Bandages 1~10");
rpg::DefineEnemyType("Cutter", 		"CravenFemale", "Enchanter", "10~20", 1, "Thrown 1 ThrowingKnife 800/100 Bandages 1~10");
rpg::DefineEnemyType("Clotmaker", 	"CravenFemale", "Enchanter", "20~30", 1, "Thrown 1 ThrowingKnife 800/100 HealthPotion 1");

rpg::DefineEnemyType("Goliath", 	"Minotaur", "Fighter", "80~100", 0, "GoliathSword 1");
rpg::DefineEnemyType("Reaper", 		"Minotaur", "Thief", "80~100", 0, "AwlPike 1 Jade 5/-500");
rpg::DefineEnemyType("Meatshield", 	"Minotaur", "Brawler", "10~20", 0, "BroadSword 1");

rpg::DefineEnemyType("Dentaur", 	"MinotaurFemale", "Fighter", "75~100", 0, "MorningStar 1 StrongHealthPotion 1/-50 SmallRock 20/50");
rpg::DefineEnemyType("Deviant", 	"MinotaurFemale", "Thief", "75~100", 0, "AwlPike 1 EnergyRock 1/-50 SmallRock 20/50");
rpg::DefineEnemyType("Fodder", 		"Minotaur", "Brawler", "10~20", 0, "BroadSword 1");

rpg::DefineEnemyType("Sniffer", 	"Ratman", "Thief", "10~20", 0, "Spear 1");
rpg::DefineEnemyType("Watcher", 	"Ratman", "Thief", "10~20", 0, "Thrown 1 ThrowingSpear 200~400 Jade 5/-500");

rpg::DefineEnemyType("Feeder", 		"Ratwoman", "Thief", "10~20", 0, "Thrown 1 ThrowingSpear 200~400");
rpg::DefineEnemyType("Stalker", 	"Ratwoman", "Thief", "10~20", 0, "Spear 1 EnergyRock 1/-50");

rpg::DefineEnemyType("Hellpoint", 	"Cragspawn", "Mage", "80~110", 0, "FrozenWand 1");
rpg::DefineEnemyType("Fearaiser", 	"Cragspawn", "Cleric", "80~110", 0, "FrozenWand 1");
rpg::DefineEnemyType("Oculus", 		"Cragspawn", "Invoker", "80~110", 0, "FrozenWand 1");
rpg::DefineEnemyType("Hengemen", 	"Cragspawn", "Druid", "80~110", 0, "FrozenWand 1");

rpg::DefineEnemyType("Bonelord", 	"Skeleton", "Fighter", "75~80", 0, "SpikedBoneClub 1");
rpg::DefineEnemyType("Necromancer", "Skeleton", "Invoker", "75~100", 0, "LongStaff 1");
rpg::DefineEnemyType("Fallen", 		"Skeleton", "Fighter", "75~100", 0, "Rapier 1");
rpg::DefineEnemyType("Marrowist", 	"Skeleton", "Druid", "75~100", 0, "FlintCaster 1");

rpg::DefineEnemyType("Hitman", 		"Outlaw", "Ranger", "75~125", 0, "RepeatingCrossbow 1 BasicBolt 400/50 Jade 5/-500");
rpg::DefineEnemyType("Thug", 		"Outlaw", "Fighter", "75~125", 0, "Mace 1");

rpg::DefineEnemyType("Mule", 		"OutlawFemale", "Ranger", "75~125", 0, "RepeatingCrossbow 1 BasicBolt 400/50 Turquoise 5/-500");
rpg::DefineEnemyType("Killer", 		"OutlawFemale", "Invoker", "75~125", 0, "ToxicDevice 1 Parchment 1,1:10 Emerald 10,1:1000");

rpg::DefineEnemyType("Farsighter", 	"Kymera", "Fighter", "100~150", 0, "SteelBow 1 MetalArrow 500~1000 Gladius 1 Soulsphere 1/-50");
rpg::DefineEnemyType("Windcaller", 	"Kymera", "Invoker", "100~150", 0, "EtheralSpear 1 Soulsphere 1/-50");

rpg::DefineEnemyType("Bainess", 	"KymeraFemale", "Invoker", "100~150", 1, "EtheralSpear 1 Soulsphere 1/-50");
rpg::DefineEnemyType("Unwinged", 	"KymeraFemale", "Bard", "100~150", 0, "Gladius 1 Soulsphere 1/-50");

rpg::DefineEnemyType("Daemon", 		"Demon", "Fighter", "99~200", 0, "Slasher 1 VigorousVial 1/-50");
rpg::DefineEnemyType("Firecaller", 	"Demon", "Mage", "125~166", 0, "FlameThrower 1 VigorousVial 1/-50");
rpg::DefineEnemyType("Handler", 	"Demon", "Brawler", "99~125", 0, "Slasher 1 Soulsphere 1/-50");

rpg::DefineEnemyType("Succubus", 	"DemonFemale", "Enchanter", "125~166", 0, "Slasher 1 Soulsphere 1/-50");
rpg::DefineEnemyType("Herder", 		"DemonFemale", "Fighter", "99~125", 0, "Slasher 1 VigorousVial 1/-50");
rpg::DefineEnemyType("Screech", 	"DemonFemale", "Invoker", "99~200", 0, "FlameThrower 1 VigorousVial 1/-50");

rpg::DefineEnemyType("Warrior", 	"Antanari", "Paladin", "80~120", 2, "Claymore 1 Soulsphere 2/-50");
rpg::DefineEnemyType("Hero", 		"Antanari", "Paladin", "75~135", 2, "Trident 1 Soulsphere 3/-50");

rpg::DefineEnemyType("Savior", 		"AntanariFemale", "Paladin", "75~120", 2, "Trident 1 Soulsphere 3/-50");
rpg::DefineEnemyType("Heroine", 	"AntanariFemale", "Paladin", "80~135", 2, "Claymore 1 Soulsphere 2/-50");

rpg::DefineEnemyType("Ieor", 		"Ancient", "Bard", "80~200", 5, "SteelBow 1 MetalArrow 1000~2000 Rapier 1");
rpg::DefineEnemyType("Eventus", 	"Ancient", "Bard", "80~200", 5, "EtheralSpear 1");
rpg::DefineEnemyType("Shaper", 		"Ancient", "Bard", "80~200", 5, "WarHammer 1");
rpg::DefineEnemyType("Indexer", 	"Ancient", "Bard", "80~200", 5, "ThunderCaller 1");

rpg::DefineEnemyType("Breaker", 	"Golem", "Brawler", "65~140", 3, "Mace 1 DragonScale 1/-3000 Gold 1/-1000");
rpg::DefineEnemyType("Sloth", 		"Golem", "Paladin", "140~200", 3, "Claymore 1 DragonScale 1/-3000 Gold 1/-1000");
rpg::DefineEnemyType("Gohort", 		"Golem", "Mage", "140~200", 4, "ThunderCaller 1 DragonScale 1/-300 Emerald 1/-1000");

// Bosses
rpg::DefineEnemyType("Creten", 		"Beastly", 		"Brawler", 		"333", 0, "TitanCrusher 1");
rpg::DefineEnemyType("Zatan", 		"PrimeEvil",	"Thief", 		"333", 0, "Lacerator 1");
rpg::DefineEnemyType("Qod", 		"Ancient", 		"Mage", 		"500", 13, "JusticeStaff 1");
rpg::DefineEnemyType("Gat", 		"Churl", 		"Enchanter", 	"333", 0, "ToxicDevice 1 KeldrinRobe 1");
rpg::DefineEnemyType("Squall", 		"Hermit", 		"Invoker", 		"500", 0, "RockLauncher 1 AntanicRobe0 1 Granite 10000");
rpg::DefineEnemyType("Pios", 		"Mandatory", 	"Paladin", 		"250", 10, "KeldriniteLS 1 KnightShield0 1 PlateMail0 1");
rpg::DefineEnemyType("Elle", 		"ElfFemale",	"Ranger", 		"250", 7, "WarBow 1 MetalArrow 5000 Spear 1 KeldrinArmor0 1 KeldrinArmor 1");

// "The hunted"
rpg::DefineEnemyType("Penant",	 	"LostSoul", "Bard", "5~50", 0, 			"Knife 1 WarAxe 1 SpikedBoneClub 1 PurgatingSoul 1");

rpg::DefineEnemyType("Writter", 	"Mandatory", 		"Brawler", "75~125", 1, "MorningStar 1 ThiefWrit 1");
rpg::DefineEnemyType("Preacher", 	"Mandatory", 		"Brawler", "90~125", 1, "WarHammer 1 ThiefWrit 1");
rpg::DefineEnemyType("Detective", 	"MandatoryFemale", 	"Brawler", "75~125", 1, "LongSword 1 ThiefWrit 1");


rpg::DefineEnemyType("Fugitive", 	"OutlawFemale", "Thief", "30~50", 0, 	"ThiefHands 1");
rpg::DefineEnemyType("Murderer", 	"Outlaw", "Thief", "30~50", 0, 			"ThiefHands 1");
rpg::DefineEnemyType("Zatanist", 	"Outlaw", "Thief", "30~50", 0, 			"ThiefHands 1");

rpg::DefineEnemyType("Roamer",	 	"Orc", "Fighter", "15~30", 0,			"OrcishAxe 1 Greenskin 1~3 BlackStatue 1~5");
rpg::DefineEnemyType("Scout",	 	"OrcFemale", "Fighter", "15~30", 0, 	"OrcishAxe 1 Greenskin 1~3 BlackStatue 1~5");
rpg::DefineEnemyType("Grunt",	 	"Orc", "Fighter", "15~30", 0, 			"OrcishAxe 1 Greenskin 1~3 BlackStatue 1~5");
rpg::DefineEnemyType("Warmaiden", 	"OrcFemale", "Fighter", "15~30", 0, 	"OrcishAxe 1 Greenskin 1~3 BlackStatue 1~5");

//_________________________________________________________________________________________________________________________________________________________
// Standard armors
//_________________________________________________________________________________________________________________________________________________________
PlayerData MaleLightArmor
{
	className = "Armor";
	shapeFile = "rpgmalehuman";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	flameShapeName = "lflame";
	shieldShapeName = "shield";
	shadowDetailMask = 1;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	canCrouch = false;
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdhigh;
	maxBackwardSpeed = $spdhigh * 0.8;
	maxSideSpeed = $spdhigh * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 60;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = $damageScale;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	animData[43] = { "celebration 1",none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, 		SoundLFootRSoft
	}; 
	lFootSounds = { SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, 		SoundLFootLSoft
	};
	footPrints = { 0, 1 };
	boxWidth = 0.5;
	boxDepth = 0.5;
	boxNormalHeight = 2.3;
	boxCrouchHeight = 1.8;
	boxNormalHeadPercentage  = 0.83;
	boxNormalTorsoPercentage = 0.53;
	boxCrouchHeadPercentage  = 0.6666;
	boxCrouchTorsoPercentage = 0.3333;
	boxHeadLeftPercentage  = 0;
	boxHeadRightPercentage = 1;
	boxHeadBackPercentage  = 0;
	boxHeadFrontPercentage = 1;
};
//_________________________________________________________________________________________________________________________________________________________
PlayerData FemaleLightArmor
{
	className = "Armor";
	shapeFile = "lfemalehuman";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	flameShapeName = "lflame";
	shieldShapeName = "shield";
	shadowDetailMask = 1;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	canCrouch = false;
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdhigh;
	maxBackwardSpeed = $spdhigh * 0.8;
	maxSideSpeed = $spdhigh * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 60;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = $damageScale;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	animData[43] = { "celebration 1",none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, 		SoundLFootRSoft
	}; 
	lFootSounds = { SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, 		SoundLFootLSoft
	};
	footPrints = { 0, 1 };
	boxWidth = 0.5;
	boxDepth = 0.5;
	boxNormalHeight = 2.3;
	boxCrouchHeight = 1.8;
	boxNormalHeadPercentage  = 0.83;
	boxNormalTorsoPercentage = 0.53;
	boxCrouchHeadPercentage  = 0.6666;
	boxCrouchTorsoPercentage = 0.3333;
	boxHeadLeftPercentage  = 0;
	boxHeadRightPercentage = 1;
	boxHeadBackPercentage  = 0;
	boxHeadFrontPercentage = 1;
};
//_________________________________________________________________________________________________________________________________________________________
PlayerData MaleRobedArmor
{
	className = "Armor";
	shapeFile = "magemale";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	flameShapeName = "lflame";
	shieldShapeName = "shield";
	shadowDetailMask = 1;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	canCrouch = false;
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdhigh;
	maxBackwardSpeed = $spdhigh * 0.8;
	maxSideSpeed = $spdhigh * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 60;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = $damageScale;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	animData[43] = { "celebration 1",none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, 		SoundLFootRSoft
	}; 
	lFootSounds = { SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, 		SoundLFootLSoft
	};
	footPrints = { 0, 1 };
	boxWidth = 0.5;
	boxDepth = 0.5;
	boxNormalHeight = 2.3;
	boxCrouchHeight = 1.8;
	boxNormalHeadPercentage  = 0.83;
	boxNormalTorsoPercentage = 0.53;
	boxCrouchHeadPercentage  = 0.6666;
	boxCrouchTorsoPercentage = 0.3333;
	boxHeadLeftPercentage  = 0;
	boxHeadRightPercentage = 1;
	boxHeadBackPercentage  = 0;
	boxHeadFrontPercentage = 1;
};
//_________________________________________________________________________________________________________________________________________________________
PlayerData FemaleRobedArmor
{
	className = "Armor";
	shapeFile = "femalemage";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	flameShapeName = "lflame";
	shieldShapeName = "shield";
	shadowDetailMask = 1;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	canCrouch = false;
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdhigh;
	maxBackwardSpeed = $spdhigh * 0.8;
	maxSideSpeed = $spdhigh * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 60;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = $damageScale;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	animData[43] = { "celebration 1",none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, 		SoundLFootRSoft
	}; 
	lFootSounds = { SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, 		SoundLFootLSoft
	};
	footPrints = { 0, 1 };
	boxWidth = 0.5;
	boxDepth = 0.5;
	boxNormalHeight = 2.3;
	boxCrouchHeight = 1.8;
	boxNormalHeadPercentage  = 0.83;
	boxNormalTorsoPercentage = 0.53;
	boxCrouchHeadPercentage  = 0.6666;
	boxCrouchTorsoPercentage = 0.3333;
	boxHeadLeftPercentage  = 0;
	boxHeadRightPercentage = 1;
	boxHeadBackPercentage  = 0;
	boxHeadFrontPercentage = 1;
};
//_________________________________________________________________________________________________________________________________________________________
PlayerData MaleMediumArmor
{
	className = "Armor";
	shapeFile = "marmor";
	flameShapeName = "mflame";
	shieldShapeName = "shield";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	shadowDetailMask = 1;
	canCrouch = false;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdmed;
	maxBackwardSpeed = $spdmed * 0.8;
	maxSideSpeed = $spdmed * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 80;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = $damageScale;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	// animation name, one shot, exclude, direction, firstPerson, chaseCam, thirdPerson, signalThread
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	// celebraton animations:
	animData[43] = { "celebration 1", none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	// taunt anmations:
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundMFootRSoft, SoundMFootRHard, SoundMFootRSoft, SoundMFootRHard, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRHard, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, 		SoundMFootRSoft
	}; 
	lFootSounds = { SoundMFootLSoft, SoundMFootLHard, SoundMFootLSoft, SoundMFootLHard, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLHard, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, 		SoundMFootLSoft
	};
	footPrints = { 2, 3 };
	boxWidth = 0.7;
	boxDepth = 0.7;
	boxNormalHeight = 2.4;
	boxNormalHeadPercentage  = 0.83;
	boxNormalTorsoPercentage = 0.49;
	boxHeadLeftPercentage  = 0;
	boxHeadRightPercentage = 1;
	boxHeadBackPercentage  = 0;
	boxHeadFrontPercentage = 1;
};
//_________________________________________________________________________________________________________________________________________________________
PlayerData FemaleMediumArmor
{
	className = "Armor";
	shapeFile = "mfemale";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	flameShapeName = "lflame";
	shieldShapeName = "shield";
	shadowDetailMask = 1;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	canCrouch = false;
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdmed;
	maxBackwardSpeed = $spdmed * 0.8;
	maxSideSpeed = $spdmed * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 60;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = $damageScale;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	animData[43] = { "celebration 1",none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, 		SoundLFootRSoft
	}; 
	lFootSounds = { SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, 		SoundLFootLSoft
	};
	footPrints = { 0, 1 };
	boxWidth = 0.5;
	boxDepth = 0.5;
	boxNormalHeight = 2.3;
	boxCrouchHeight = 1.8;
	boxNormalHeadPercentage  = 0.83;
	boxNormalTorsoPercentage = 0.53;
	boxCrouchHeadPercentage  = 0.6666;
	boxCrouchTorsoPercentage = 0.3333;
	boxHeadLeftPercentage  = 0;
	boxHeadRightPercentage = 1;
	boxHeadBackPercentage  = 0;
	boxHeadFrontPercentage = 1;
};
//_________________________________________________________________________________________________________________________________________________________
PlayerData HeavyArmor
{
	className = "Armor";
	shapeFile = "harmor";
	flameShapeName = "hflame";
	shieldShapeName = "shield";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	shadowDetailMask = 1;
	canCrouch = false;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdlow;
	maxBackwardSpeed = $spdlow * 0.5;
	maxSideSpeed = $spdlow;
	groundForce = 75 * 9.0;
	mass = 15.0;
	groundTraction = 5.0;
	maxEnergy = 110;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = 0.006;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	// animation name, one shot, exclude, direction, firstPerson, chaseCam, thirdPerson, signalThread
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	// celebraton animations:
	animData[43] = { "celebration 1", none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	// taunt anmations:
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundHFootRSoft, SoundHFootRHard, SoundHFootRSoft, SoundHFootRHard, SoundHFootRSoft, SoundHFootRSoft, SoundHFootRSoft, SoundHFootRHard, SoundHFootRSoft, SoundHFootRSoft, SoundHFootRSoft, SoundHFootRSoft, SoundHFootRSoft, SoundHFootRSoft, 		SoundHFootRSoft
	}; 
	lFootSounds = { SoundHFootLSoft, SoundHFootLHard, SoundHFootLSoft, SoundHFootLHard, SoundHFootLSoft, SoundHFootLSoft, SoundHFootLSoft, SoundHFootLHard, SoundHFootLSoft, SoundHFootLSoft, SoundHFootLSoft, SoundHFootLSoft, SoundHFootLSoft, SoundHFootLSoft, 		SoundHFootLSoft
	};
	footPrints = { 4, 5 };
	boxWidth = 0.8;
	boxDepth = 0.8;
	boxNormalHeight = 2.6;
	boxNormalHeadPercentage  = 0.70;
	boxNormalTorsoPercentage = 0.45;
	boxHeadLeftPercentage  = 0.48;
	boxHeadRightPercentage = 0.70;
	boxHeadBackPercentage  = 0.48;
	boxHeadFrontPercentage = 0.60;
};

//_________________________________________________________________________________________________________________________________________________________
// Creatures
//_________________________________________________________________________________________________________________________________________________________
PlayerData GoblinArmor
{
	className = "Armor";
	shapeFile = "goblin";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	flameShapeName = "lflame";
	shieldShapeName = "shield";
	shadowDetailMask = 1;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	canCrouch = false;
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdlowmed;
	maxBackwardSpeed = $spdlowmed * 0.8;
	maxSideSpeed = $spdlowmed * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 60;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = $damageScale;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	animData[43] = { "celebration 1",none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, 		SoundLFootRSoft
	}; 
	lFootSounds = { SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, 		SoundLFootLSoft
	};
	footPrints = { 0, 1 };
	boxWidth = 0.5;
	boxDepth = 0.5;
	boxNormalHeight = 2.3;
	boxCrouchHeight = 1.8;
	boxNormalHeadPercentage  = 0.83;
	boxNormalTorsoPercentage = 0.53;
	boxCrouchHeadPercentage  = 0.6666;
	boxCrouchTorsoPercentage = 0.3333;
	boxHeadLeftPercentage  = 0;
	boxHeadRightPercentage = 1;
	boxHeadBackPercentage  = 0;
	boxHeadFrontPercentage = 1;
};
//________________________________________________________________________________________________
PlayerData LostSoulArmor
{
	className = "Armor";
	shapeFile = "lostsoul";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	flameShapeName = "lflame";
	shieldShapeName = "shield";
	shadowDetailMask = 1;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	canCrouch = false;
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdlow;
	maxBackwardSpeed = $spdlow * 0.8;
	maxSideSpeed = $spdlow * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 60;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = $damageScale;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	animData[43] = { "celebration 1",none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, 		SoundLFootRSoft
	}; 
	lFootSounds = { SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, 		SoundLFootLSoft
	};
	footPrints = { 0, 1 };
	boxWidth = 0.5;
	boxDepth = 0.5;
	boxNormalHeight = 2.3;
	boxCrouchHeight = 1.8;
	boxNormalHeadPercentage  = 0.83;
	boxNormalTorsoPercentage = 0.53;
	boxCrouchHeadPercentage  = 0.6666;
	boxCrouchTorsoPercentage = 0.3333;
	boxHeadLeftPercentage  = 0;
	boxHeadRightPercentage = 1;
	boxHeadBackPercentage  = 0;
	boxHeadFrontPercentage = 1;
};
//_________________________________________________________________________________________________________________________________________________________
PlayerData GnollArmor
{
	className = "Armor";
	shapeFile = "marmorgnoll";
	flameShapeName = "mflame";
	shieldShapeName = "shield";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	shadowDetailMask = 1;
	canCrouch = false;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdmed;
	maxBackwardSpeed = $spdmed * 0.8;
	maxSideSpeed = $spdmed * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 80;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = $damageScale;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	// animation name, one shot, exclude, direction, firstPerson, chaseCam, thirdPerson, signalThread
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	// celebraton animations:
	animData[43] = { "celebration 1", none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	// taunt anmations:
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundMFootRSoft, SoundMFootRHard, SoundMFootRSoft, SoundMFootRHard, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRHard, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, 		SoundMFootRSoft
	}; 
	lFootSounds = { SoundMFootLSoft, SoundMFootLHard, SoundMFootLSoft, SoundMFootLHard, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLHard, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, 		SoundMFootLSoft
	};
	footPrints = { 2, 3 };
	boxWidth = 0.7;
	boxDepth = 0.7;
	boxNormalHeight = 2.4;
	boxNormalHeadPercentage  = 0.83;
	boxNormalTorsoPercentage = 0.49;
	boxHeadLeftPercentage  = 0;
	boxHeadRightPercentage = 1;
	boxHeadBackPercentage  = 0;
	boxHeadFrontPercentage = 1;
};
//_________________________________________________________________________________________________________________________________________________________
PlayerData AncientArmor
{
	className = "Armor";
	shapeFile = "ancient";
	flameShapeName = "mflame";
	shieldShapeName = "shield";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	shadowDetailMask = 1;
	canCrouch = false;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdmed;
	maxBackwardSpeed = $spdmed * 0.8;
	maxSideSpeed = $spdmed * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 80;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = $damageScale;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	// animation name, one shot, exclude, direction, firstPerson, chaseCam, thirdPerson, signalThread
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	// celebraton animations:
	animData[43] = { "celebration 1", none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	// taunt anmations:
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundMFootRSoft, SoundMFootRHard, SoundMFootRSoft, SoundMFootRHard, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRHard, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, 		SoundMFootRSoft
	}; 
	lFootSounds = { SoundMFootLSoft, SoundMFootLHard, SoundMFootLSoft, SoundMFootLHard, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLHard, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, 		SoundMFootLSoft
	};
	footPrints = { 2, 3 };
	boxWidth = 0.7;
	boxDepth = 0.7;
	boxNormalHeight = 2.4;
	boxNormalHeadPercentage  = 0.83;
	boxNormalTorsoPercentage = 0.49;
	boxHeadLeftPercentage  = 0;
	boxHeadRightPercentage = 1;
	boxHeadBackPercentage  = 0;
	boxHeadFrontPercentage = 1;
};
//_________________________________________________________________________________________________________________________________________________________
PlayerData MinotaurArmor
{
	className = "Armor";
	shapeFile = "min";
	flameShapeName = "mflame";
	shieldShapeName = "shield";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	shadowDetailMask = 1;
	canCrouch = false;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdlowmed;
	maxBackwardSpeed = $spdlowmed * 0.8;
	maxSideSpeed = $spdlowmed * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 80;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = $damageScale;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	// animation name, one shot, exclude, direction, firstPerson, chaseCam, thirdPerson, signalThread
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	// celebraton animations:
	animData[43] = { "celebration 1", none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	// taunt anmations:
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundMFootRSoft, SoundMFootRHard, SoundMFootRSoft, SoundMFootRHard, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRHard, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, SoundMFootRSoft, 		SoundMFootRSoft
	}; 
	lFootSounds = { SoundMFootLSoft, SoundMFootLHard, SoundMFootLSoft, SoundMFootLHard, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLHard, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, SoundMFootLSoft, 		SoundMFootLSoft
	};
	footPrints = { 2, 3 };
	boxWidth = 0.7;
	boxDepth = 0.7;
	boxNormalHeight = 2.4;
	boxNormalHeadPercentage  = 0.83;
	boxNormalTorsoPercentage = 0.49;
	boxHeadLeftPercentage  = 0;
	boxHeadRightPercentage = 1;
	boxHeadBackPercentage  = 0;
	boxHeadFrontPercentage = 1;
};
//_________________________________________________________________________________________________________________________________________________________
PlayerData GolemArmor
{
	className = "Armor";
	shapeFile = "fedmonster";
	flameShapeName = "hflame";
	shieldShapeName = "shield";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	shadowDetailMask = 1;
	canCrouch = false;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdfast;
	maxBackwardSpeed = $spdfast * 0.8;
	maxSideSpeed = $spdfast * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 110;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = 0.006;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	// animation name, one shot, exclude, direction, firstPerson, chaseCam, thirdPerson, signalThread
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	// celebraton animations:
	animData[43] = { "celebration 1", none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	// taunt anmations:
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundHFootRSoft, SoundHFootRHard, SoundHFootRSoft, SoundHFootRHard, SoundHFootRSoft, SoundHFootRSoft, SoundHFootRSoft, SoundHFootRHard, SoundHFootRSoft, SoundHFootRSoft, SoundHFootRSoft, SoundHFootRSoft, SoundHFootRSoft, SoundHFootRSoft, 		SoundHFootRSoft
	}; 
	lFootSounds = { SoundHFootLSoft, SoundHFootLHard, SoundHFootLSoft, SoundHFootLHard, SoundHFootLSoft, SoundHFootLSoft, SoundHFootLSoft, SoundHFootLHard, SoundHFootLSoft, SoundHFootLSoft, SoundHFootLSoft, SoundHFootLSoft, SoundHFootLSoft, SoundHFootLSoft, 		SoundHFootLSoft
	};
	footPrints = { 4, 5 };
	boxWidth = 0.8;
	boxDepth = 0.8;
	boxNormalHeight = 2.6;
	boxNormalHeadPercentage  = 0.70;
	boxNormalTorsoPercentage = 0.45;
	boxHeadLeftPercentage  = 0.48;
	boxHeadRightPercentage = 0.70;
	boxHeadBackPercentage  = 0.48;
	boxHeadFrontPercentage = 0.60;
};
//_________________________________________________________________________________________________________________________________________________________
PlayerData ZombieArmor
{
	className = "Armor";
	shapeFile = "zombie";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	flameShapeName = "lflame";
	shieldShapeName = "shield";
	shadowDetailMask = 1;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	canCrouch = false;
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdlowmed;
	maxBackwardSpeed = $spdlowmed * 0.8;
	maxSideSpeed = $spdlowmed * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 60;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = $damageScale;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	animData[43] = { "celebration 1",none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, 		SoundLFootRSoft
	}; 
	lFootSounds = { SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, 		SoundLFootLSoft
	};
	footPrints = { 0, 1 };
	boxWidth = 0.5;
	boxDepth = 0.5;
	boxNormalHeight = 2.3;
	boxCrouchHeight = 1.8;
	boxNormalHeadPercentage  = 0.83;
	boxNormalTorsoPercentage = 0.53;
	boxCrouchHeadPercentage  = 0.6666;
	boxCrouchTorsoPercentage = 0.3333;
	boxHeadLeftPercentage  = 0;
	boxHeadRightPercentage = 1;
	boxHeadBackPercentage  = 0;
	boxHeadFrontPercentage = 1;
};
//_________________________________________________________________________________________________________________________________________________________
PlayerData SkeletonArmor
{
	className = "Armor";
	shapeFile = "skel";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	flameShapeName = "lflame";
	shieldShapeName = "shield";
	shadowDetailMask = 1;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	canCrouch = false;
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdmed;
	maxBackwardSpeed = $spdmed * 0.8;
	maxSideSpeed = $spdmed * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 60;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = $damageScale;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	animData[43] = { "celebration 1",none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, 		SoundLFootRSoft
	}; 
	lFootSounds = { SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, 		SoundLFootLSoft
	};
	footPrints = { 0, 1 };
	boxWidth = 0.5;
	boxDepth = 0.5;
	boxNormalHeight = 2.3;
	boxCrouchHeight = 1.8;
	boxNormalHeadPercentage  = 0.83;
	boxNormalTorsoPercentage = 0.53;
	boxCrouchHeadPercentage  = 0.6666;
	boxCrouchTorsoPercentage = 0.3333;
	boxHeadLeftPercentage  = 0;
	boxHeadRightPercentage = 1;
	boxHeadBackPercentage  = 0;
	boxHeadFrontPercentage = 1;
};
//________________________________________________________________________________________________
PlayerData ImpArmor
{
	className = "Armor";
	shapeFile = "imp";
	damageSkinData = "armorDamageSkins";
	debrisId = playerDebris;
	flameShapeName = "lflame";
	shieldShapeName = "shield";
	shadowDetailMask = 1;
	visibleToSensor = True;
	mapFilter = 1;
	mapIcon = "M_player";
	canCrouch = false;
	maxJetSideForceFactor = 1;
	maxJetForwardVelocity = 1.0;
	minJetEnergy = 60;
	jetForce = 1;
	jetEnergyDrain = 0.0;
	maxDamage = 1.0;
	maxForwardSpeed = $spdmed;
	maxBackwardSpeed = $spdmed * 0.8;
	maxSideSpeed = $spdmed * 0.75;
	groundForce = 75 * 9.0;
	mass = 9.0;
	groundTraction = 3.0;
	maxEnergy = 60;
	drag = 1.0;
	density = 1.2;
	minDamageSpeed = 16;
	damageScale = $damageScale;
	jumpImpulse = 75;
	jumpSurfaceMinDot = $jumpSurfaceMinDot;
	animData[0]  = { "root", none, 1, true, true, true, false, 0 };
	animData[1]  = { "run", none, 1, true, false, true, false, 3 };
	animData[2]  = { "runback", none, 1, true, false, true, false, 3 };
	animData[3]  = { "side left", none, 1, true, false, true, false, 3 };
	animData[4]  = { "side left", none, -1, true, false, true, false, 3 };
	animData[5] = { "jump stand", none, 1, true, false, true, false, 3 };
	animData[6] = { "jump run", none, 1, true, false, true, false, 3 };
	animData[7] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[8] = { "crouch root", none, 1, true, true, true, false, 3 };
	animData[9] = { "crouch root", none, -1, true, true, true, false, 3 };
	animData[10] = { "crouch forward", none, 1, true, false, true, false, 3 };
	animData[11] = { "crouch forward", none, -1, true, false, true, false, 3 };
	animData[12] = { "crouch side left", none, 1, true, false, true, false, 3 };
	animData[13] = { "crouch side left", none, -1, true, false, true, false, 3 };
	animData[14]  = { "fall", none, 1, true, true, true, false, 3 };
	animData[15]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[16]  = { "landing", SoundLandOnGround, 1, true, false, false, false, 3 };
	animData[17]  = { "tumble loop", none, 1, true, false, false, false, 3 };
	animData[18]  = { "tumble end", none, 1, true, false, false, false, 3 };
	animData[19] = { "jet", none, 1, true, true, true, false, 3 };
	animData[20] = { "die back", none, 1, true, false, false, false, 0 };
	animData[21] = { "throw", none, 1, true, false, false, false, 3 };
	animData[22] = { "flyer root", none, 1, false, false, false, false, 3 };
	animData[23] = { "apc root", none, 1, true, true, true, false, 3 };
	animData[24] = { "apc pilot", none, 1, false, false, false, false, 3 };
	animData[25] = { "crouch die", none, 1, false, false, false, false, 4 };
	animData[26] = { "die chest", none, 1, false, false, false, false, 4 };
	animData[27] = { "die head", none, 1, false, false, false, false, 4 };
	animData[28] = { "die grab back", none, 1, false, false, false, false, 4 };
	animData[29] = { "die right side", none, 1, false, false, false, false, 4 };
	animData[30] = { "die left side", none, 1, false, false, false, false, 4 };
	animData[31] = { "die leg left", none, 1, false, false, false, false, 4 };
	animData[32] = { "die leg right", none, 1, false, false, false, false, 4 };
	animData[33] = { "die blown back", none, 1, false, false, false, false, 4 };
	animData[34] = { "die spin", none, 1, false, false, false, false, 4 };
	animData[35] = { "die forward", none, 1, false, false, false, false, 4 };
	animData[36] = { "die forward kneel", none, 1, false, false, false, false, 4 };
	animData[37] = { "die back", none, 1, false, false, false, false, 4 };
	animData[38] = { "sign over here",  none, 1, true, false, false, false, 2 };
	animData[39] = { "sign point", none, 1, true, false, false, false, 1 };
	animData[40] = { "sign retreat",none, 1, true, false, false, false, 2 };
	animData[41] = { "sign stop", none, 1, true, false, false, true, 1 };
	animData[42] = { "sign salut", none, 1, true, false, false, true, 1 }; 
	animData[43] = { "celebration 1",none, 1, true, false, false, false, 2 };
	animData[44] = { "celebration 2", none, 1, true, false, false, false, 2 };
	animData[45] = { "celebration 3", none, 1, true, false, false, false, 2 };
	animData[46] = { "taunt 1", none, 1, true, false, false, false, 2 };
	animData[47] = { "taunt 2", none, 1, true, false, false, false, 2 };
	animData[48] = { "pose kneel", none, 1, true, false, false, true, 1 };
	animData[49] = { "pose stand", none, 1, true, false, false, true, 1 };
	// Bonus wave
	animData[50] = { "wave", none, 1, true, false, false, true, 1 };
	jetSound = NoSound;
	rFootSounds =  { SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRHard, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, SoundLFootRSoft, 		SoundLFootRSoft
	}; 
	lFootSounds = { SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLHard, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, SoundLFootLSoft, 		SoundLFootLSoft
	};
	footPrints = { 0, 1 };
	boxWidth = 0.5;
	boxDepth = 0.5;
	boxNormalHeight = 2.3;
	boxCrouchHeight = 1.8;
	boxNormalHeadPercentage  = 0.83;
	boxNormalTorsoPercentage = 0.53;
	boxCrouchHeadPercentage  = 0.6666;
	boxCrouchTorsoPercentage = 0.3333;
	boxHeadLeftPercentage  = 0;
	boxHeadRightPercentage = 1;
	boxHeadBackPercentage  = 0;
	boxHeadFrontPercentage = 1;
};

