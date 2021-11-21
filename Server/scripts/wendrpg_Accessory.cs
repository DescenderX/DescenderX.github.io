//This file is part of Tribes RPG.
//Tribes RPG server side scripts
//General RPG scripts written by Jason "phantom" Daley,  Matthiew "JeremyIrons" Bouchard, and more (yet undetermined)

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

//________________________________________________________________________________________________________________________________________________
// DescX Notes:
// 		Accessories should only be used for VERY odd item types now. The Belt system works well enough for most other cases.

$SpecialVarDesc[1] = "";
$SpecialVarDesc[2] = "";
$SpecialVarDesc[3] = "Resistance";
$SpecialVarDesc[4] = "Health";
$SpecialVarDesc[5] = "Mana";
$SpecialVarDesc[6] = "Attack";
$SpecialVarDesc[7] = "Armor";
$SpecialVarDesc[8] = "[Internal]";
$SpecialVarDesc[9] = "";
$SpecialVarDesc[10] = "Health Regen";
$SpecialVarDesc[11] = "Mana Regen";

$RingAccessoryType = 1;
$BodyAccessoryType = 2;
$BootsAccessoryType = 3;
$BackAccessoryType = 4;
$ShieldAccessoryType = 5;
$TalismanAccessoryType = 6;
$SwordAccessoryType = 7;
$AxeAccessoryType = 8;
$PolearmAccessoryType = 9;
$BludgeonAccessoryType = 10;
$RangedAccessoryType = 11;
$ProjectileAccessoryType = 12;

$LocationDesc[$RingAccessoryType] = "Ring";
$LocationDesc[$BodyAccessoryType] = "Body";
$LocationDesc[$BootsAccessoryType] = "Feet";
$LocationDesc[$BackAccessoryType] = "Back";
$LocationDesc[$ShieldAccessoryType] = "Shield";
$LocationDesc[$TalismanAccessoryType] = "Talisman";
$LocationDesc[$SwordAccessoryType] = "Sword";
$LocationDesc[$AxeAccessoryType] = "Axe";
$LocationDesc[$PolearmAccessoryType] = "Polearm";
$LocationDesc[$BludgeonAccessoryType] = "Bludgeon";
$LocationDesc[$RangedAccessoryType] = "Ranged";
$LocationDesc[$ProjectileAccessoryType] = "Projectile";

$maxAccessory[$RingAccessoryType] = 2;
$maxAccessory[$BodyAccessoryType] = 1;
$maxAccessory[$BootsAccessoryType] = 1;
$maxAccessory[$BackAccessoryType] = 1;
$maxAccessory[$ShieldAccessoryType] = 1;
$maxAccessory[$TalismanAccessoryType] = 1;

//these are used for $AccessoryVar
$AccessoryType = 1;				//(used in item.cs)
$SpecialVar = 2;				//(used in player.cs)
$Weight = 3;					//(used in rpgfunk.cs)
$ShopIndex = 4;					// Deleted all references for RPGext. Shop lists are defined with item NAMES now, not INDICIES.
$MiscInfo = 5;

$RingWeight = 1;

//________________________________________________________________________________________________________________________________________________
// Drop flags
$ItemDropStealProof		= 1;
$ItemDropNever 			= 2;
$ItemDropIgnoreLuck 	= 4;

//________________________________________________________________________________________________________________________________________________
//  Mining stuff
$AccessoryVar[Granite, $Weight] = 0.2;
$AccessoryVar[Copper, $Weight] = 0.3;
$AccessoryVar[Quartz, $Weight] = 0.2;
$AccessoryVar[Opal, $Weight] = 0.4;
$AccessoryVar[Iron, $Weight] = 0.4;
$AccessoryVar[Jade, $Weight] = 0.3;
$AccessoryVar[Aluminum, $Weight] = 0.1;
$AccessoryVar[Turquoise, $Weight] = 0.2;
$AccessoryVar[Ruby, $Weight] = 0.3;
$AccessoryVar[Topaz, $Weight] = 0.3;
$AccessoryVar[Sapphire, $Weight] = 0.3;
$AccessoryVar[Silver, $Weight] = 0.1;
$AccessoryVar[Emerald, $Weight] = 0.3;
$AccessoryVar[Gold, $Weight] = 0.1;
$AccessoryVar[Diamond, $Weight] = 0.1;
$AccessoryVar[Platinum, $Weight] = 0.2;
$AccessoryVar[Radium, $Weight] = 0.3;
$AccessoryVar[Keldrinite, $Weight] = 5.0;

$AccessoryVar[Quartz, $MiscInfo] = "Quartz";
$AccessoryVar[Granite, $MiscInfo] = "Granite";
$AccessoryVar[Opal, $MiscInfo] = "Opal";
$AccessoryVar[Jade, $MiscInfo] = "Jade";
$AccessoryVar[Turquoise, $MiscInfo] = "Turquoise";
$AccessoryVar[Ruby, $MiscInfo] = "Ruby";
$AccessoryVar[Topaz, $MiscInfo] = "Topaz";
$AccessoryVar[Sapphire, $MiscInfo] = "Sapphire";
$AccessoryVar[Gold, $MiscInfo] = "Gold";
$AccessoryVar[Emerald, $MiscInfo] = "Emerald";
$AccessoryVar[Diamond, $MiscInfo] = "Diamond";
$AccessoryVar[Keldrinite, $MiscInfo] = "Keldrinite is a very rare magical gem that, when in the hands of a skilled blacksmith, can give items magical properties.";
$AccessoryVar[Copper, $MiscInfo] = "Copper";
$AccessoryVar[Aluminum, $MiscInfo] = "Aluminum";
$AccessoryVar[Iron, $MiscInfo] = "Iron";
$AccessoryVar[Silver, $MiscInfo] = "Silver";
$AccessoryVar[Platinum, $MiscInfo] = "Platinum";
$AccessoryVar[Radium, $MiscInfo] = "Radium";

$HardcodedItemCost[Granite] = 50;
$HardcodedItemCost[Copper] = 100;
$HardcodedItemCost[Quartz] = 200;
$HardcodedItemCost[Opal] = 300;
$HardcodedItemCost[Iron] = 400;
$HardcodedItemCost[Jade] = 500;
$HardcodedItemCost[Aluminum] = 550;
$HardcodedItemCost[Turquoise] = 700;
$HardcodedItemCost[Ruby] = 800;
$HardcodedItemCost[Topaz] = 1100;
$HardcodedItemCost[Sapphire] = 1250;
$HardcodedItemCost[Silver] = 1500;
$HardcodedItemCost[Emerald] = 2500;
$HardcodedItemCost[Gold] = 5000;
$HardcodedItemCost[Diamond] = 6500;
$HardcodedItemCost[Platinum] = 9999;
$HardcodedItemCost[Radium] = 12500;
$HardcodedItemCost[Keldrinite] = 25000;

%f = 43;
$ItemList[Mining, 1] = "SmallRock " @ round($HardcodedItemCost[SmallRock] / %f)+2;
$ItemList[Mining, 2] = "Granite " @ round($HardcodedItemCost[Granite] / %f)+2;
$ItemList[Mining, 3] = "Copper " @ round($HardcodedItemCost[Copper] / %f)+2;
$ItemList[Mining, 4] = "Quartz " @ round($HardcodedItemCost[Quartz] / %f)+2;
$ItemList[Mining, 5] = "Iron " @ round($HardcodedItemCost[Opal] / %f)+2;
$ItemList[Mining, 6] = "Opal " @ round($HardcodedItemCost[Opal] / %f)+2;
$ItemList[Mining, 7] = "Jade " @ round($HardcodedItemCost[Jade] / %f)+2;
$ItemList[Mining, 8] = "Aluminum " @ round($HardcodedItemCost[Aluminum] / %f)+2;
$ItemList[Mining, 9] = "Turquoise " @ round($HardcodedItemCost[Turquoise] / %f)+2;
$ItemList[Mining, 10] = "Ruby " @ round($HardcodedItemCost[Ruby] / %f)+2;
$ItemList[Mining, 11] = "Topaz " @ round($HardcodedItemCost[Topaz] / %f)+2;
$ItemList[Mining, 12] = "Sapphire " @ round($HardcodedItemCost[Sapphire] / %f)+2;
$ItemList[Mining, 13] = "Silver " @ round($HardcodedItemCost[Silver] / %f)+2;
$ItemList[Mining, 14] = "Emerald " @ round($HardcodedItemCost[Emerald] / %f)+2;
$ItemList[Mining, 15] = "Gold " @ round($HardcodedItemCost[Gold] / %f)+2;
$ItemList[Mining, 16] = "Diamond " @ round($HardcodedItemCost[Diamond] / %f)+2;
$ItemList[Mining, 17] = "Platinum " @ round($HardcodedItemCost[Platinum] / %f)+2;
$ItemList[Mining, 18] = "Radium " @ round($HardcodedItemCost[Radium] / %f)+2;
$ItemList[Mining, 19] = "Keldrinite " @ round($HardcodedItemCost[Keldrinite] / %f)+2;

%f = 30;
$ItemList[Survival, 1] = "WoodChip " @ round($HardcodedItemCost[SmallRock] / %f)+2;
$ItemList[Survival, 2] = "TreeSap " @ round($HardcodedItemCost[Quartz] / %f)+2;
$ItemList[Survival, 3] = "FreshFruit " @ round($HardcodedItemCost[Granite] / %f)+2;
$ItemList[Survival, 4] = "WoodChunk " @ round($HardcodedItemCost[Opal] / %f)+2;
$ItemList[Survival, 5] = "TreeSap " @ round($HardcodedItemCost[Jade] / %f)+2;
$ItemList[Survival, 6] = "LargeFruit " @ round($HardcodedItemCost[Turquoise] / %f)+2;
$ItemList[Survival, 7] = "Wood " @ round($HardcodedItemCost[Ruby] / %f)+2;
$ItemList[Survival, 8] = "TreeSap " @ round($HardcodedItemCost[Topaz] / %f)+2;
$ItemList[Survival, 9] = "Wood " @ round($HardcodedItemCost[Sapphire] / %f)+2;
$ItemList[Survival, 10] = "Gold " @ round($HardcodedItemCost[Diamond] / %f)+2;



//________________________________________________________________________________________________________________________________________________
// ItemList for other types
$ItemList[Orb, 1] = "OrbOfBreath";
$ItemList[Orb, 2] = "OrbOfLuminance";
$ItemList[Badge, 1] = "ManaStone";
$ItemList[Badge, 2] = "VileSubstance";
$ItemList[Badge, 3] = "DemonRune";
$ItemList[Badge, 4] = "SignOfQod";
$ItemList[Badge, 5] = "Radium";



//________________________________________________________________________________________________________________________________________________
// CheetaursPaws
$HardcodedItemCost[CheetaursPaws] = 5000;
$ItemCost[CheetaursPaws] = $HardcodedItemCost[CheetaursPaws];
$AccessoryVar[CheetaursPaws, $AccessoryType] = $BootsAccessoryType;
$AccessoryVar[CheetaursPaws, $SpecialVar] = "8 1";
$AccessoryVar[CheetaursPaws, $Weight] = 3;
$AccessoryVar[CheetaursPaws, $MiscInfo] = "Cheetaur's Paws increase speed and jump power";
ItemData CheetaursPaws { description = "Cheetaur's Paws";className = "Accessory";shapeFile = "discammo";heading = "eMiscellany";price = 0;};
ItemData CheetaursPaws0 {description = "Cheetaur's Paws";className = "Equipped";shapeFile = "discammo";heading = "aArmor";};



//________________________________________________________________________________________________________________________________________________
// BootsOfGliding
$HardcodedItemCost[BootsOfGliding] = 75000;
$ItemCost[BootsOfGliding] = $HardcodedItemCost[BootsOfGliding];
$AccessoryVar[BootsOfGliding, $AccessoryType] = $BootsAccessoryType;
$AccessoryVar[BootsOfGliding, $SpecialVar] = "8 2";
$AccessoryVar[BootsOfGliding, $Weight] = 3;
$AccessoryVar[BootsOfGliding, $MiscInfo] = "Boots Of Gliding let you glide";
ItemData BootsOfGliding {description = "Boots Of Gliding";className = "Accessory";shapeFile = "discammo";heading = "eMiscellany";price = 0;};
ItemData BootsOfGliding0 {description = "Boots Of Gliding";className = "Equipped";shapeFile = "discammo";heading = "aArmor";};



//________________________________________________________________________________________________________________________________________________
// WindWalkers
$HardcodedItemCost[WindWalkers] = 650000;
$ItemCost[WindWalkers] = $HardcodedItemCost[WindWalkers];
$AccessoryVar[WindWalkers, $AccessoryType] = $BootsAccessoryType;
$AccessoryVar[WindWalkers, $SpecialVar] = "8 3";
$AccessoryVar[WindWalkers, $Weight] = 3;
$AccessoryVar[WindWalkers, $MiscInfo] = "Wind Walkers let you fly!";
ItemData WindWalkers {description = "Wind Walkers";className = "Accessory";shapeFile = "discammo";heading = "eMiscellany";price = 0;};
ItemData WindWalkers0 {description = "Wind Walkers";className = "Equipped";shapeFile = "discammo";heading = "aArmor";};



//________________________________________________________________________________________________________________________________________________
// Equalizer
$HardcodedItemCost[Equalizer] = 0;
$ItemDropFlags[Equalizer] = ($ItemDropNever | $ItemDropStealProof);
$AccessoryVar[Equalizer, $AccessoryType] = $RingAccessoryType;
$AccessoryVar[Equalizer, $SpecialVar] = "8 5";
$AccessoryVar[Equalizer, $Weight] = 0;
$AccessoryVar[Equalizer, $MiscInfo] = "What is this thing? What does it do?";
ItemData Equalizer {description = "The Equalizer";className = "Accessory";shapeFile = "discammo";heading = "eMiscellany";price = 0;};
ItemData Equalizer0 {description = "The Equalizer";className = "Equipped";shapeFile = "discammo";heading = "aArmor";};

function Equalizer::onDrop(%player, %item) {
	Client::sendMessage(Player::getClient(%player), $MsgRed, "You cannot drop The Equalizer.");
}

function Equalizer::onUse(%player, %item) {
	if($UsedEQ[%player] == ""){
		%cl = Player::getClient(%player);
		$UsedEQ[%player] = true;
		schedule("Client::sendMessage(" @ %cl @ ", " @ $MsgRed @ ", \"As your insides begin to heat up rapidly, you realize it probably wasn't a great idea to Use the Equalizer this way...\");", 2);
		for(%x=1;%x<=10;%x++) {
			schedule(	"CreateSpellBomb(" @ %cl @ ", \"SpellFXfirebomb\", GameBase::getPosition(" @ %cl @ "));" @
						"Player::applyImpulse(" @ %cl @ ", Vector::Random(50));", %x);
		}
		schedule("$UsedEQ[" @ %player @ "] = \"\"; Player::kill(" @ %cl @ ");", 10.5);
	}
}


//________________________________________________________________________________________________________________________________________________
// Zonechecked Bonus Items
$BonusItem[Radium] 					= "DMG 226 1000 1";			// Radium hits you with its atomic mass
$AccessoryVar[Radium, $MiscInfo] 	= "A rare material that slowly kills";
$BonusItemMessage[Radium]			= "The Radium you are carrying damages your health!";
$BonusItemMessageColor[Radium]		= $MsgGreen;

$HardcodedItemCost[DemonRune] 		= 13666;
$AccessoryVar[DemonRune, $Weight] 	= 1;
$AccessoryVar[DemonRune, $MiscInfo] = "A demonic rune that randomly increases the wearer's speed for short periods of time.";
$BonusItem[DemonRune] 				= "SPD 1 69 6.5";			//a chance in 69 every ZoneCheck that SPD 1 will be buffed for 13 seconds
$BonusItemMessage[DemonRune]		= "The Demon Rune you are carrying self-ignites and speeds you up!";
$BonusItemMessageColor[DemonRune]	= $MsgRed;
ItemData DemonRune {description 	= "Demon Rune";className = "Accessory";shapeFile = "grenade";heading = "eMiscellany";shadowDetailMask = 4;price = 0;};


$HardcodedItemCost[VileSubstance] = 1;
$AccessoryVar[VileSubstance, $Weight] = 1;
$AccessoryVar[VileSubstance, $MiscInfo] = "A terrible, awful substance that produces Toxin.";
$BonusItem[VileSubstance] = "Toxin 1 300";		// put Toxin in the player's inventory randomly (poison arrow smithing EZ mode)
ItemData VileSubstance {description = "Vile Substance";className = "Accessory";shapeFile = "grenade";heading = "eMiscellany";shadowDetailMask = 4;price = 0;};

$HardcodedItemCost[ManaStone] = 250000;
$AccessoryVar[ManaStone, $Weight] = 1;
$AccessoryVar[ManaStone, $MiscInfo] = "This stone randomly vibrates with energy. It might give the holder energy every 2 seconds.";
$BonusItem[ManaStone] = "MANA 0.5 5";
$ItemDropFlags[ManaStone] = $ItemDropStealProof;
ItemData ManaStone {description = "Mana Stone";className = "Accessory";shapeFile = "grenade";heading = "eMiscellany";shadowDetailMask = 4;price = 0;};

$HardcodedItemCost[SignOfQod] = 0;
$AccessoryVar[SignOfQod, $Weight] = 0;
$AccessoryVar[SignOfQod, $MiscInfo] = "A Sign of Qod. A chance in 357 every two seconds that FAVOR will be awarded.";
$BonusItem[SignOfQod] = "FAVOR 1 180";
$ItemDropFlags[SignOfQod] = ($ItemDropNever | $ItemDropStealProof);
ItemData SignOfQod {description = "Sign of Qod";className = "Accessory";shapeFile = "grenade";heading = "eMiscellany";shadowDetailMask = 4;price = 0;};
function SignOfQod::onDrop(%player, %item) {
	Client::sendMessage(Player::getClient(%player), $MsgRed, "You cannot drop the Sign of Qod.");
}


//________________________________________________________________________________________________________________________________________________
// Orb of Luminance
$HardcodedItemCost[OrbOfLuminance] = 5000;
$AccessoryVar[OrbOfLuminance, $AccessoryType] = $ShieldAccessoryType;
$AccessoryVar[OrbOfLuminance, $Weight] = 1.0;
$AccessoryVar[OrbOfLuminance, $MiscInfo] = "The Orb Of Luminance provides you with temporary illumination.";
$OverrideMountPoint[OrbOfLuminance] = 2;
$BurnOut[OrbOfLuminance] = 150;
$BurnOutInRain[OrbOfLuminance] = 5;
$ProtectFromWater[OrbOfLuminance] = "";
ItemImageData OrbOfLuminanceImage
{
	shapeFile = "orb";
	mountPoint = $OverrideMountPoint[OrbOfLuminance];
	mountOffset = {0.0, 0.0, 1.8};
	mountRotation = {5, 3, 3};
	lightType = 2;
	lightRadius = 13;
	lightTime = 9999;
	lightColor = { 0.95, 0.85, 0.55 };
};
ItemData OrbOfLuminance {description = "Orb Of Luminance";className = "Accessory";shapeFile = "orb";imageType = OrbOfLuminanceImage;heading = "eMiscellany";price = 0;};
ItemData OrbOfLuminance0 {description = "Lit Orb Of Luminance";className = "Equipped";shapeFile = "orb";imageType = OrbOfLuminanceImage;heading = "aArmor";};



//________________________________________________________________________________________________________________________________________________
//Orb of Breath
$HardcodedItemCost[OrbOfBreath] = 10000;
$AccessoryVar[OrbOfBreath, $AccessoryType] = $ShieldAccessoryType;
$AccessoryVar[OrbOfBreath, $Weight] = 0.8;
$AccessoryVar[OrbOfBreath, $MiscInfo] = "The Orb Of Breath provides you with a temporary ability to breathe underwater.";
$OverrideMountPoint[OrbOfBreath] = 2;
$BurnOut[OrbOfBreath] = 300;
$BurnOutInRain[OrbOfBreath] = 0;
$ProtectFromWater[OrbOfBreath] = True;
ItemImageData OrbOfBreathImage {shapeFile = "orb";mountPoint = $OverrideMountPoint[OrbOfBreath];mountOffset = {0.0, 0.0, 1.4};mountRotation = {5, 3, 3};};
ItemData OrbOfBreath {description = "Orb Of Breath";className = "Accessory";shapeFile = "orb";imageType = OrbOfBreathImage;heading = "eMiscellany";price = 0;};
ItemData OrbOfBreath0 {description = "Orb Of Breath in use";className = "Equipped";shapeFile = "orb";imageType = OrbOfBreathImage;heading = "aArmor";};



//________________________________________________________________________________________________________________________________________________
// "The Shocker" :(
$HardcodedItemCost[TheShocker] 			= 0;
$AccessoryVar[TheShocker, $Weight] 		= 10;
$AccessoryVar[TheShocker, $MiscInfo] 	= "Single-use, disposable device used to convince certain debtors to pay up...";
ItemData TheShocker {description = "'The Shocker'";className = "Accessory";shapeFile = "force";heading = "eMiscellany";shadowDetailMask = 4;price = 0;};
function TheShocker::onUse(%player, %item) {
	%clientId = Player::getClient(%player);
	GameBase::getLOSinfo(Client::getOwnedObject(%clientId), 5);
	if($los::object > 0) {
		if(GameBase::getMapName($los::object) == "Ealejnor") {
			for(%x=1;%x<=3;%x++) schedule("CreateSpellBomb(" @ %clientId @ ", \"SpellFXshock\", GameBase::getPosition(" @ $los::object @ "));", %x);
			%coin = floor(getRandom() * 15000) + 5000;
			storeData(%clientId, "COINS", %coin, "inc");
			Client::sendMessage(%clientId, $MsgBeige, "You used The Shocker. Ealejnor recoils in horror and hands over all " @ %coin @ " of her coins.");
			TakeThisStuff(%clientId, "TheShocker 1");
		} else if(GameBase::getMapName($los::object) == "Jorn") {
			for(%x=1;%x<=3;%x++) schedule("CreateSpellBomb(" @ %clientId @ ", \"SpellFXshock\", GameBase::getPosition(" @ $los::object @ "));", %x);
			%coin = floor(getRandom() * 50000);
			storeData(%clientId, "COINS", %coin, "inc");
			Client::sendMessage(%clientId, $MsgBeige, "You used The Shocker. Jorn shrieks and hands over all " @ %coin @ " of his coins.");
			TakeThisStuff(%clientId, "TheShocker 1");
		} else if(Player::isAiControlled($los::object)) {
			for(%x=1;%x<=3;%x++) schedule("CreateSpellBomb(" @ %clientId @ ", \"SpellFXshock\", GameBase::getPosition(" @ $los::object @ "));", %x);
			%coin = floor(getRandom() * fetchData(%id,"EXP"));
			Player::Kill($los::object);
			%id =  Player::getClient($los::object);
			storeData(%clientId, "COINS", %coin, "inc");
			Player::setAnimation(%id, 48);
			Client::sendMessage(%clientId, $MsgBeige, "You used The Shocker. Your target... squirms. Then dies suddenly. You... find... " @ %coin @ " coins.");
			TakeThisStuff(%clientId, "TheShocker 1");
		}
	} else {
		for(%x=1;%x<=3;%x++) schedule("CreateSpellBomb(" @ %clientId @ ", \"SpellFXshock\", GameBase::getPosition(" @ %clientId @ "));", %x);
		Client::sendMessage(%clientId, $MsgBeige, "You discover the price of curiosity as you use The Shocker on... yourself...");
		Item::setVelocity(%clientId, "0 0 0");
		Player::applyImpulse(%clientId, "0 0 500");
	}
}

//________________________________________________________________________________________________________________________________________________
// Misc ItemData
ItemData Lootbag {description = "Backpack";className = "Lootbag";shapeFile = "ammo2";heading = "eMiscellany";shadowDetailMask = 4;price = 0;};
ItemData Grenade {description = "Grenade";shapeFile = "grenade";heading = "eMiscellany";shadowDetailMask = 4;price = 5;className = "HandAmmo";};
ItemData MineAmmo {description = "Mine";shapeFile = "mineammo";heading = "eMiscellany";shadowDetailMask = 4;price = 10;className = "HandAmmo";};
ItemData RepairKit {description = "Repair Kit";shapeFile = "armorKit";heading = "eMiscellany";shadowDetailMask = 4;price = 35;};
ItemData BeltItemTool {description = "Belt Items...";shapeFile = "longbow";heading = "eMiscellany";shadowDetailMask = 4;price = 0;className = "Accessory";};
$ItemDropFlags["BeltItemTool"] = ($ItemDropStealProof | ItemDropNever);


//________________________________________________________________________________________________________________________________________________
// ACCESSORY FUNCTIONS
function GetAccessoryVar(%item, %type) {
	%nitem = getCroppedItem(%item);
	return $AccessoryVar[%nitem, %type];
}

function getCroppedItem(%item) {
	%zitem = %item @ "xx";
	%p = String::findSubStr(%zitem, "0xx");
	if(%p != -1)		%nitem = String::getSubStr(%item, 0, %p);
	else				%nitem = %item;
	return %nitem;
}

//________________________________________________________________________________________________________________________________________________
function GetAccessoryList(%clientId, %type, %filter) {
	dbecho($dbechoMode, "GetAccessoryList(" @ %clientId @ ", " @ %type @ ", " @ %filter @ ")");

	if(IsDead(%clientId) || !fetchData(%clientId, "HasLoadedAndSpawned") || %clientId.IsInvalid || %clientId.choosingGroup || %clientId.choosingClass)
		return "";

	%list = "";
	if(%type == 10)
	{
		%max = $BeltItemTypeCount[AmmoItems];
		for(%i = 0; %i < %max; %i++)
		{
			%item = $beltitem[%i+1, "Num", "AmmoItems"];
			%count = belt::hasthisstuff(%clientId, %item);
			if(%count)
			{
				if(%filter != -1)
				{
					%flag2 = False;
					%av = GetAccessoryVar(%item, $SpecialVar);
					for(%j = 0; GetWord(%av, %j) != -1; %j+=2)
					{
						%w = GetWord(%av, %j);
						if(String::findSubStr(%filter, %w) != -1)
							%flag2 = True;
					}
				}
				if(%filter == -1 || %flag2)
					%list = %list @ %item @ " ";
			}
		}
		return %list;
	}
	%max = getNumItems();
	for(%i = 0; %i < %max; %i++)
	{
		%count = Player::getItemCount(%clientId, %i);

		if(%count)
		{
			%item = getItemData(%i);

			%flag = "";
			if(%type == 1)
			{
				if(%item.className == "Accessory")
					%flag = True;
			}
			else if(%type == 2)
			{
				if(%item.className == "Equipped")
					%flag = True;
			}
			else if(%type == 3)
			{
				if(%item.className == "Accessory" || %item.className == "Equipped")
					%flag = True;
			}
			else if(%type == 4)
			{
				if(%item.className == "Equipped" || %item.className == "Weapon" || %item.className == "Backpack")
				{
					if(%item.className == "Weapon")
					{
						if(Player::getMountedItem(%clientId, $WeaponSlot) == %item)
							%flag = True;
					}
					else if(%item.className == "Backpack")
					{
						if(Player::getMountedItem(%clientId, $BackpackSlot) == %item)
							%flag = True;
					}
					else
						%flag = True;
				}
			}
			else if(%type == 5)
			{
				if($AccessoryVar[%item, $AccessoryType] == $SwordAccessoryType)
					%flag = True;
			}
			else if(%type == 6)
			{
				if($AccessoryVar[%item, $AccessoryType] == $AxeAccessoryType)
					%flag = True;
			}
			else if(%type == 7)
			{
				if($AccessoryVar[%item, $AccessoryType] == $PolearmAccessoryType)
					%flag = True;
			}
			else if(%type == 8)
			{
				if($AccessoryVar[%item, $AccessoryType] == $BludgeonAccessoryType)
					%flag = True;
			}
			else if(%type == 9)
			{
				if($AccessoryVar[%item, $AccessoryType] == $RangedAccessoryType)
					%flag = True;
			}
			else if(%type == -1)
				%flag = True;

			if(%flag)
			{
				if(%filter != -1)
				{
					%flag2 = False;
					%av = GetAccessoryVar(%item, $SpecialVar);
					for(%j = 0; GetWord(%av, %j) != -1; %j+=2)
					{
						%w = GetWord(%av, %j);
						if(String::findSubStr(%filter, %w) != -1)
							%flag2 = True;
					}
				}
				if(%filter == -1 || %flag2)
					%list = %list @ %item @ " ";
			}
		}
	}
	return %list;
}

function AddPoints(%clientId, %char)
{
	dbecho($dbechoMode, "AddPoints(" @ %clientId @ ", " @ %char @ ")");

	%add = 0;
	if(IsDead(%clientId)) return %add;
	
	%list = GetAccessoryList(%clientId, 4, %char);
	for(%i = 0; GetWord(%list, %i) != -1; %i++)
	{
		%w = GetWord(%list, %i);

		%slot = "";
		if(%w.className == Weapon)
			%slot = $WeaponSlot;
		else if(%w.className == Backpack)
			%slot = $BackpackSlot;

		if(%slot != "")
		{
			if(Player::getMountedItem(%clientId, %slot) == %w)
				%count = 1;
			else
				%count = 0;
		}
		else
			%count = Player::getItemCount(%clientId, %w);

		%tmp = GetAccessoryVar(%w, $SpecialVar);

		for(%j = 0; GetWord(%tmp, %j) != -1; %j+=2)
		{
			%e = GetWord(%tmp, %j);
			if(String::findSubStr(%char, %e) != -1)
				%add += GetWord(%tmp, %j+1) * %count;
		}
	}

	return %add;
}

function AddItemSpecificPoints(%item, %char) {
	%tmp = GetAccessoryVar(%item, $SpecialVar);

	for(%j = 0; GetWord(%tmp, %j) != -1; %j+=2) {
		%e = GetWord(%tmp, %j);
		if(%e == %char) {
			%info = GetWord(%tmp, %j+1);
			break;
		}
	}

	return %info;
}

function WhatSpecialVars(%thing) {
	%tmp = GetAccessoryVar(%thing, $SpecialVar);

	%t = "";
	for(%i = 0; GetWord(%tmp, %i) != -1; %i+=2) {
		%s = GetWord(%tmp, %i);
		%n = GetWord(%tmp, %i+1);
		%t = %t @ $SpecialVarDesc[%s] @ ": " @ %n @ ", ";
	}
	if(%t == "") 	%t = "None";
	else			%t = String::getSubStr(%t, 0, String::len(%t)-2);	
	return %t;
}

function NullItemList(%clientId, %type, %msgcolor, %msg)
{
	dbecho($dbechoMode, "NullItemList(" @ %clientId @ ", " @ %type @ ", " @ %msgcolor @ ", " @ %msg @ ")");
	if(IsDead(%clientId))
		return;
	for(%z = 1; $ItemList[%type, %z] != ""; %z++)
	{
		%item = $ItemList[%type, %z];

		if(isBeltItem(%item))
		{
			%amnt = Belt::HasThisStuff(%clientid,%item);
			if(%amnt > 0)
			{
				%item = $beltitem[%item, "Item"];
				%name = $beltitem[%item, "Name"];
				%newmsg = nsprintf(%msg, %name);
				Client::sendMessage(%clientId, %msgcolor, %newmsg);
				Belt::TakeThisStuff(%clientid,%item,%amnt);
			}
		}
		else if(Player::getItemCount(%clientId, %item))
		{
			Player::setItemCount(%clientId, %item, 0);

			%newmsg = nsprintf(%msg, %item.description);
			Client::sendMessage(%clientId, %msgcolor, %newmsg);
		}		
	}
}

function GetCurrentlyWearingArmor(%clientId) {
	if(!IsDead(%clientId)){
		for(%i = 1; $ArmorList[%i] != ""; %i++) {
			if(Player::getItemCount(%clientId, $ArmorList[%i] @ "0"))
				return $ArmorList[%i];
		}
	}
	return "";
}
