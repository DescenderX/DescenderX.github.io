//____________________________________________________________________________________________________
// Geoastrics
//
// DescX Notes:
//		The good old Pick Axe lives here now. These are items specific to the College of Geoastrics, 
//		a house (guild) in "World Ender" focued on Mining.
//		The weapons mine incredibly fast. Some of them have dual functions that include tree cutting. 
//		The "Astral Flask" is capable of producing Water Flasks. Taken together, this means Geoastrics get the
//		best resource-gathering experience.
//
//____________________________________________________________________________________________________
$WeaponRange[Laser] 		= 1;
$WeaponRange[Reatomizer] 	= 25;
$WeaponRange[Driller] 		= 0;
//____________________________________________________________________________________________________
rpg::DefineWeaponType("PickAxe", 		$SkillMining, 	$AxeAccessoryType, 			"6 10", 5, "A pick axe used for mining or attacking", 1.0);
rpg::DefineWeaponType("HammerPick", 	$SkillMining, 	$BludgeonAccessoryType, 	"6 50", 8, "A hammer pick for mining, woodcutting, or attacking", 1.5);
rpg::DefineWeaponType("Laser", 			$SkillMining, 	$SwordAccessoryType, 		"6 25", 1, "A strange light casting device for searing ore and enemies", 0.5);
rpg::DefineWeaponType("Driller", 		$SkillMining, 	$SwordAccessoryType, 		"6 20", 4, "A mining, woodcutting and enemy slicing tool", 0.333);
rpg::DefineWeaponType("Reatomizer", 	$SkillMining, 	$RangedAccessoryType, 		"6 50", 12, "An otherwordly device that duplicates atomic structure of ore, wood, and water at a distance", 1.0);


//____________________________________________________________________________________________________
$HardcodedItemCost[AstralFlask] 		= 0;
$AccessoryVar[AstralFlask, $Weight] 	= 0.2;
$AccessoryVar[AstralFlask, $MiscInfo] 	= "A magical device that creates 'ice flasks' and preserves samples from liquid resevoirs";
ItemData AstralFlask
{
	description = "Astral Flask";
	className = "Accessory";
	shapeFile = "saphire";
	heading = "eMiscellany";
	shadowDetailMask = 4;
	price = 0;
};
function AstralFlask::onUse(%player, %item) {
	%clientId = Player::getClient(%player);
	if(fetchData(%clientId, "MyHouse") != "College of Geoastrics") {
		Client::sendMessage(%clientId, $MsgRed, "You have no idea how to use this... thing?");
		return;
	}
	if(Belt::HasThisStuff(%clientId,"MineralWater") >= 5) {
		Client::sendMessage(%clientId, $MsgBeige, "The flask is full of Mineral Water. Pryzm will need to empty it for you before it can be used again.");
	} 
	else if($FlaskUsed[%clientId] == 1) {
		Client::sendMessage(%clientId, $MsgBeige, "The flask needs a couple of seconds to separate out your last sample.");
	}
	else if(fetchData(%clientId, "InWaterZone") == true) {
		%level = rpg::GetHouseLevel(%clientId);
		$FlaskUsed[%clientId] = 1;
		
		if(getRandom() <= Cap(%level/75, 0.1, 0.75)) {
			schedule("Belt::GiveThisStuff(" @ %clientId @ ", \"MineralWater\", 1, true);", 2);
		} else {
			schedule("Belt::GiveThisStuff(" @ %clientId @ ", \"WaterFlask\", 1, true);", 2);
		}
		
		schedule("$FlaskUsed[" @ %clientId @ "] = \"\";", 3);
	} else {
		Client::sendMessage(%clientId, $MsgRed, "You need to be near or in a water source to use the Astral Flask.");
	}
}

//____________________________________________________________________________________________________
ItemImageData PickAxeImage {	
	mountPoint = 0;	weaponType = 0; reloadTime = 0;	minEnergy = 0; maxEnergy = 0; accuFire = true;
	fireTime = GetDelay(PickAxe); shapeFile = "Pick"; sfxFire = SoundPoleSwingSM;	sfxActivate = SoundAxeSwingSM;
};
ItemData PickAxe {
	heading = "bWeapons"; className = "Weapon"; price = 0;	showWeaponBar = true; shadowDetailMask = 4;
	imageType = PickAxeImage; description = "Pick Axe"; shapeFile = "Pick";	hudIcon = "pick";			
};
function PickAxeImage::onFire(%player, %slot) {
	PickAxeSwing(%player, GetRange(PickAxe), PickAxe);
	
}



//____________________________________________________________________________________________________
ItemImageData HammerPickImage {
	shapeFile = "Pick";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(HammerPick);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundBladeSwingLG;	sfxActivate = SoundBluntSwingSM;
};
ItemData HammerPick {
	heading = "bWeapons";	description = "Hammer Pick";	className = "Weapon";
	shapeFile = "Pick";	hudIcon = "pick";	shadowDetailMask = 4;
	imageType = HammerPickImage;	price = 0;	showWeaponBar = true;
};
function HammerPickImage::onFire(%player, %slot) {
	PickAxeSwing(%player, GetRange(HammerPick), HammerPick);
	$justmeleed[Player::getClient(%player)] = "";
	HatchetSwing(%player, GetRange(HammerPick), HammerPick);
}



//____________________________________________________________________________________________________
ItemImageData DrillerImage {
	shapeFile = "chaingun";	mountPoint = 0;	weaponType = 1; 
	reloadTime = 0;	fireTime = GetDelay(Driller);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundSpinUp;	sfxActivate = SoundPickupBackpack;
	reloadTime = 0;
	spinUpTime = 1;
	spinDownTime = 1;
	sfxSpinUp = SoundSpinUp;
	sfxSpinDown = SoundSpinUp;
};
ItemData Driller {
	heading = "bWeapons";	description = "Driller";	className = "Weapon";
	shapeFile = "chaingun";	hudIcon = "pick";	shadowDetailMask = 4;
	imageType = DrillerImage;	price = 0;	showWeaponBar = true;
};
function DrillerImage::onFire(%player, %slot) {	
	PickAxeSwing(%player, GetRange(Driller), Driller);	
	%clientId = Player::getClient(%player);
	playSound(SoundSpinUp, GameBase::getPosition(%clientId));
	$justmeleed[Player::getClient(%player)] = "";
	HatchetSwing(%player, GetRange(Driller), Driller);
}



//____________________________________________________________________________________________________
ItemImageData LaserImage {
	shapeFile = "repairgun";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(Laser);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundShockTarget;	sfxActivate = SoundEquipWand;
};
ItemData Laser {
	heading = "bWeapons";	description = "Laser";	className = "Weapon";
	shapeFile = "repairgun";	hudIcon = "pick";	shadowDetailMask = 4;
	imageType = LaserImage;	price = 0;	showWeaponBar = true;
};
function LaserImage::onFire(%player, %slot) {
	PickAxeSwing(%player, GetRange(Laser), Laser);
}



//____________________________________________________________________________________________________
ItemImageData ReatomizerImage {
	shapeFile = "plasma";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(Reatomizer);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundPickupHealth;	sfxActivate = SoundEquipWand;
};
ItemData Reatomizer {
	heading = "bWeapons";	description = "Reatomizer";	className = "Weapon";
	shapeFile = "plasma";	hudIcon = "pick";	shadowDetailMask = 4;
	imageType = ReatomizerImage;	price = 0;	showWeaponBar = true;
};
function ReatomizerImage::onFire(%player, %slot) {
	PickAxeSwing(%player, GetRange(Reatomizer), Reatomizer);
	$justmeleed[Player::getClient(%player)] = "";
	HatchetSwing(%player, GetRange(Reatomizer), Reatomizer);
}
