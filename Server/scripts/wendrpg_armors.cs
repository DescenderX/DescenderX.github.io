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


//________________________________________________________________________________________________________________________________________________________________
// DescX Notes:
//		Armor definitions simplified to a couple hundred lines
//________________________________________________________________________________________________________________________________________________________________
rpg::DefineArmorType("PaddedArmor", "rpgpadded", "", "Padded Armor", "7 25 4 5", 10, SoundHitLeather);
rpg::DefineArmorType("LeatherArmor", "rpgleather", "", "Leather armor", "7 100 4 7", 15, SoundHitLeather);
rpg::DefineArmorType("StuddedLeather", "rpgstudleather", "", "Studded leather armor", "7 150 4 9", 25, SoundHitLeather);
rpg::DefineArmorType("SpikedLeather", "rpgspiked", "", "Leather armor with spikes", "7 200 4 13", 25, SoundHitLeather);
rpg::DefineArmorType("HideArmor", "rpghide", "", "Hide armor", "7 250 4 18", 30, SoundHitLeather);
rpg::DefineArmorType("ScaleMail", "rpgscalemail", "", "Scalemail", "7 300 4 22", 40, SoundHitChain);
rpg::DefineArmorType("BrigandineArmor", "rpgbrigandine", "", "Brigandine metal armor", "7 400 4 28", 35, SoundHitChain);
rpg::DefineArmorType("ChainMail", "rpgchainmail", "", "Chain link mail", "7 450 4 33", 40, SoundHitChain);
rpg::DefineArmorType("RingMail", "rpgringmail", "", "Heavy chain mail", "7 500 4 38", 30, SoundHitChain);
rpg::DefineArmorType("BandedMail", "rpgbandedmail", "", "Banded steel armor", "7 600 4 43", 35, SoundHitChain);
rpg::DefineArmorType("SplintMail", "rpgsplintmail", "", "Splint mail", "7 650 4 47", 40, SoundHitChain);
rpg::DefineArmorType("BronzePlateMail", "rpgbronzeplate", "", "Bronze plated armor", "7 750 4 53", 45, SoundHitPlate);
rpg::DefineArmorType("PlateMail", "rpgplatemail", "", "Plated chain armor", "7 800 4 58", 50, SoundHitPlate);
rpg::DefineArmorType("FieldPlateArmor", "rpgfieldplate", "", "Plated armor", "7 850 4 64", 60, SoundHitPlate);
rpg::DefineArmorType("FullPlateArmor", "rpgfullplate", "", "Full suit of armor", "7 900 4 75", 70, SoundHitPlate);
rpg::DefineArmorType("DragonMail", "rpghuman7", "", "Suitable armor for combat with dragons of ancient times", "7 1000 4 70", 80, SoundHitChain);
rpg::DefineArmorType("KeldrinArmor", "rpghuman9", "", "The strongest and lightest armor in the known world", "7 1350 3 350 4 110", 5, SoundHitPlate);
//________________________________________________________________________________________________________________________________________________________________
rpg::DefineArmorType("SlayerGear", "slay", "", "A Wildenslayer's combat gear. Useless to non-slayers.", "7 69", 80, SoundHitChain, 0);
//________________________________________________________________________________________________________________________________________________________________
Smith::addItem("PaddedArmor","","PaddedArmor 1", $SkillStrength);
Smith::addItem("LeatherArmor","","LeatherArmor 1", $SkillStrength);
Smith::addItem("StuddedLeather","","StuddedLeather 1", $SkillStrength);
Smith::addItem("SpikedLeather","","SpikedLeather 1", $SkillStrength);
Smith::addItem("HideArmor","","HideArmor 1", $SkillStrength);
Smith::addItem("ScaleMail","","ScaleMail 1", $SkillStrength);
Smith::addItem("BrigandineArmor","","BrigandineArmor 1", $SkillStrength);
Smith::addItem("ChainMail","","ChainMail 1", $SkillStrength);
Smith::addItem("RingMail","","RingMail 1", $SkillStrength);
Smith::addItem("BandedMail","","BandedMail 1", $SkillStrength);
Smith::addItem("SplintMail","","SplintMail 1", $SkillStrength);
Smith::addItem("BronzePlateMail","","BronzePlateMail 1", $SkillStrength);
Smith::addItem("PlateMail","","PlateMail 1", $SkillStrength);
Smith::addItem("FieldPlateArmor","","FieldPlateArmor 1", $SkillStrength);
Smith::addItem("FullPlateArmor","","FullPlateArmor 1", $SkillStrength);
Smith::addItem("DragonMail","DragonScale 15 Diamond 5 Ruby 3","DragonMail 1", $SkillStrength);
//________________________________________________________________________________________________________________________________________________________________
rpg::DefineArmorType("ApprenticeRobe", "robepink", "Robed", "A mage-in-training's robe", "3 50 4 10", 15);
rpg::DefineArmorType("LightRobe", "robepurple", "Robed", "A light robe", "3 100 4 20", 10);
rpg::DefineArmorType("RobeOfOrder", "robegray", "Robed", "A robe made for the Order of Qod", "3 200 4 70", 15);
rpg::DefineArmorType("FineRobe", "robebrown", "Robed", "A fine robe", "3 300 4 30", 5);
rpg::DefineArmorType("AdvisorRobe", "robeblue", "Robed", "A second-tier mage Advisor's robe", "3 400 4 40", 15);
rpg::DefineArmorType("BloodRobe", "robered", "Robed", "A robe used by blood magicians", "3 500 4 50", 15);
rpg::DefineArmorType("FrostRobe", "robecyan", "Robed", "A robe made of frost threads", "3 600 4 70", 15);
rpg::DefineArmorType("ElvenRobe", "robegreen", "Robed", "An Elven robe", "3 700 4 60", 15);
rpg::DefineArmorType("MasterRobe", "master", "Robed", "A master mage's robe", "3 800 4 70", 15);
rpg::DefineArmorType("RobeOfVenjance", "robeblack", "Robed", "A powerful robe", "3 900 4 80", 15);
rpg::DefineArmorType("AntanicRobe", "robeyellow", "Robed", "An ancient hero's robe", "3 1000 4 70", 15);
rpg::DefineArmorType("PhensRobe", "robewhite", "Robed", "A robe designed by the legendary mage of ages past", "3 1200 4 90", 15);
rpg::DefineArmorType("KeldrinRobe", "robeorange", "Robed", "The most powerful robe in the known world", "3 1500 4 100", 5);
//________________________________________________________________________________________________________________________________________________________________
Smith::addItem("ApprenticeRobe","","ApprenticeRobe 1", $SkillEndurance);
Smith::addItem("LightRobe","","LightRobe 1", $SkillEndurance);
Smith::addItem("RobeOfOrder","","RobeOfOrder 1", $SkillEndurance);
Smith::addItem("FineRobe","LightRobe 1 ApprenticeRobe 1 EnchantedStone 5","FineRobe 1", $SkillEndurance);
Smith::addItem("AdvisorRobe","","AdvisorRobe 1", $SkillEndurance);
Smith::addItem("BloodRobe","","BloodRobe 1", $SkillEndurance);
Smith::addItem("FrostRobe","","FrostRobe 1", $SkillEndurance);
Smith::addItem("ElvenRobe","AdvisorRobe 1 Topaz 2 EnchantedStone 4","ElvenRobe 1", $SkillEndurance);
Smith::addItem("MasterRobe","","MasterRobe 1", $SkillEndurance);
Smith::addItem("RobeOfVenjance","","RobeOfVenjance 1", $SkillEndurance);
Smith::addItem("AntanicRobe","","AntanicRobe 1", $SkillEndurance);
Smith::addItem("PhensRobe","","PhensRobe 1", $SkillEndurance);
//________________________________________________________________________________________________________________________________________________________________
rpg::DefineShieldType("PaddedShield", 		"7 100 3 10",	8, 	"A simple padded shield", 0);
rpg::DefineShieldType("PlateShield", 		"7 200 3 25",	20, "A shield made of sturdy plates", 0);
rpg::DefineShieldType("KnightShield", 		"7 300 3 50",	12, "A mid-tier shield with excellent defense", 0);
rpg::DefineShieldType("BronzeShield", 		"7 400 3 100", 	15, "A simple padded shield", 0);
rpg::DefineShieldType("DragonShield", 		"7 500 3 200", 	20,	"A shield made from the scales of a dragon", 0);
rpg::DefineShieldType("KeldriniteShield", 	"7 600 3 300", 	5, 	"The sturdiest shield in the known world", 0);


//________________________________________________________________________________________________________________________________________________________________
// ARMOR
//________________________________________________________________________________________________________________________________________________________________
ItemData PaddedArmor { description = "Padded Armor"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData PaddedArmor0 { description = "Padded Armor"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData LeatherArmor { description = "Leather Armor"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData LeatherArmor0 { description = "Leather Armor"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData StuddedLeather { description = "Studded Leather Armor"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData StuddedLeather0 { description = "Studded Leather Armor"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData SpikedLeather { description = "Spiked Leather Armor"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData SpikedLeather0 { description = "Spiked Leather Armor"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData SlayerGear { description = "Wildenslayer Gear"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData SlayerGear0 { description = "Wildenslayer Gear"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData HideArmor { description = "Hide Armor"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData HideArmor0 { description = "Hide Armor"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData ScaleMail { description = "Scale Mail"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData ScaleMail0 { description = "Scale Mail"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData BrigandineArmor { description = "Brigandine Armor"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData BrigandineArmor0 { description = "Brigandine Armor"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData ChainMail { description = "Chain Mail"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData ChainMail0 { description = "Chain Mail"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData RingMail { description = "Ring Mail"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData RingMail0 { description = "Ring Mail"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData BandedMail { description = "Banded Mail"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData BandedMail0 { description = "Banded Mail"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData SplintMail { description = "Splint Mail"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData SplintMail0 { description = "Splint Mail"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData DragonMail { description = "Dragon Mail"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData DragonMail0 { description = "Dragon Mail"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData BronzePlateMail { description = "Bronze Plate Mail"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData BronzePlateMail0 { description = "Bronze Plate Mail"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData PlateMail { description = "Plate Mail"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData PlateMail0 { description = "Plate Mail"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData FieldPlateArmor { description = "Field Plate Armor"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData FieldPlateArmor0 { description = "Field Plate Armor"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData FullPlateArmor { description = "Full Plate Armor"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData FullPlateArmor0 { description = "Full Plate Armor"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData KeldrinArmor { description = "Keldrin Armor"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData KeldrinArmor0 { description = "Keldrin Armor"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };


//________________________________________________________________________________________________________________________________________________________________
// ROBES
//________________________________________________________________________________________________________________________________________________________________
ItemData ApprenticeRobe { description = "Apprentice Robe"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData ApprenticeRobe0 { description = "Apprentice Robe"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData LightRobe { description = "Light Robe"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData LightRobe0 { description = "Light Robe"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData FineRobe { description = "Fine Robe"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData FineRobe0 { description = "Fine Robe"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData BloodRobe { description = "Blood Robe"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData BloodRobe0 { description = "Blood Robe"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData AdvisorRobe { description = "Advisor Robe"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData AdvisorRobe0 { description = "Advisor Robe"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData ElvenRobe { description = "Elven Robe"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData ElvenRobe0 { description = "Elven Robe"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData RobeOfVenjance { description = "Robe Of Venjance"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData RobeOfVenjance0 { description = "Robe Of Venjance"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData PhensRobe { description = "Phen's Robe"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData PhensRobe0 { description = "Phen's Robe"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData KeldrinRobe { description = "Keldrin Robe"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData KeldrinRobe0 { description = "Keldrin Robe"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData MasterRobe { description = "Master Robe"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData MasterRobe0 { description = "Master Robe"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData RobeOfOrder { description = "Robe of Order"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData RobeOfOrder0 { description = "Robe of Order"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData FrostRobe { description = "Frost Robe"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData FrostRobe0 { description = "Frost Robe"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };

ItemData AntanicRobe { description = "Antanic Robe"; className = "Accessory"; shapeFile = "discammo"; heading = "eMiscellany"; price = 0; };
ItemData AntanicRobe0 { description = "Antanic Robe"; className = "Equipped"; shapeFile = "discammo"; heading = "aArmor"; };


//________________________________________________________________________________________________________________________________________________________________
// SHIELDS
//________________________________________________________________________________________________________________________________________________________________
ItemImageData PaddedShieldImage 	{ shapeFile = "shieldpadded"; mountPoint = 2; mountOffset = {0.0, -0.1, -0.3}; mountRotation = {-0.1, 0, 0.5};};
ItemData PaddedShield				{ description = "Padded Shield";className = "Accessory"; shapeFile = "shieldpadded";imageType = PaddedShieldImage; heading = "eMiscellany";price = 0;};
ItemData PaddedShield0				{ description = "Padded Shield";	className = "Equipped";	shapeFile = "shieldpadded";	heading = "aArmor";};

ItemImageData PlateShieldImage 		{ shapeFile = "shield3"; mountPoint = 2; mountOffset = {0.0, -0.12, -0.3}; mountRotation = {-0.1, 0, 0.5};};
ItemData PlateShield				{ description = "Plate Shield";className = "Accessory"; shapeFile = "shield3";imageType = PlateShieldImage; heading = "eMiscellany";price = 0;};
ItemData PlateShield0				{ description = "Plate Shield";	className = "Equipped";	shapeFile = "shield3";	heading = "aArmor";};

ItemImageData KnightShieldImage 	{ shapeFile = "shieldknight"; mountPoint = 2; mountOffset = {0.0, -0.1, -0.3}; mountRotation = {-0.1, 0, 0.5};};
ItemData KnightShield				{ description = "Knight Shield";className = "Accessory"; shapeFile = "shieldknight";imageType = KnightShieldImage; heading = "eMiscellany";price = 0;};
ItemData KnightShield0				{ description = "Knight Shield";	className = "Equipped";	shapeFile = "shieldknight";	heading = "aArmor";};

ItemImageData BronzeShieldImage 	{ shapeFile = "shieldbronze"; mountPoint = 2; mountOffset = {0.0, -0.1, -0.3}; mountRotation = {-0.1, 0, 0.5};};
ItemData BronzeShield				{ description = "Bronze Shield";className = "Accessory"; shapeFile = "shieldbronze";imageType = BronzeShieldImage; heading = "eMiscellany";price = 0;};
ItemData BronzeShield0				{ description = "Bronze Shield";	className = "Equipped";	shapeFile = "shieldbronze";	heading = "aArmor";};

ItemImageData KeldriniteShieldImage	{shapeFile = "shieldkeldrin";mountPoint = 2;mountOffset = {0.0, -0.1, -0.35};mountRotation = {-0.1, 0, 0.5};};
ItemData KeldriniteShield			{description = "Keldrinite Shield";className = "Accessory";shapeFile = "shieldkeldrin";imageType = KeldriniteShieldImage;heading = "eMiscellany";price = 0;};
ItemData KeldriniteShield0			{description = "Keldrinite Shield";className = "Equipped";shapeFile = "shieldkeldrin";heading = "aArmor";};

ItemImageData DragonShieldImage		{shapeFile = "shielddragon";mountPoint = 2;mountOffset = {0.0, -0.1, -0.35};mountRotation = {-0.1, 0, 0.5};};
ItemData DragonShield				{description = "Dragon Shield";className = "Accessory";shapeFile = "shielddragon";imageType = DragonShieldImage;heading = "eMiscellany";price = 0;};
ItemData DragonShield0				{description = "Dragon Shield";className = "Equipped";shapeFile = "shielddragon";heading = "aArmor";};

