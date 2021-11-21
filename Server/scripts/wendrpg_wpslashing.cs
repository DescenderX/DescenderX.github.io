//____________________________________________________________________________________________________
// Slashing
//
// DescX Notes:
//		The Orcish Axe is a rare instance of a quest item that is a weapon.
//		Slashing weapons automatically "parry", which essentially gives you an extra evade roll based on Slashing for free.
//
//____________________________________________________________________________________________________
rpg::DefineWeaponType("Hatchet", 		$SkillSlashing, $AxeAccessoryType, 		"6 20", 5, "A hatchet. Chops wood. Or enemies.", 2);
rpg::DefineWeaponType("BroadSword", 	$SkillSlashing, $SwordAccessoryType, 	"6 35", 5, "A sturdy sword");
rpg::DefineWeaponType("WarAxe", 		$SkillSlashing, $AxeAccessoryType, 		"6 70", 7, "A short axe");
rpg::DefineWeaponType("LongSword", 		$SkillSlashing, $SwordAccessoryType, 	"6 65", 5, "A long sword");
rpg::DefineWeaponType("BattleAxe", 		$SkillSlashing, $AxeAccessoryType, 		"6 100", 9, "A black battle axe");
rpg::DefineWeaponType("ElvenSword", 	$SkillSlashing, $SwordAccessoryType, 	"6 135", 7, "A green Elven sword");
rpg::DefineWeaponType("Halberd", 		$SkillSlashing, $AxeAccessoryType, 		"6 175", 8, "A sharp halberd");
rpg::DefineWeaponType("Claymore", 		$SkillSlashing, $SwordAccessoryType, 	"6 200", 8, "A large claymore");
rpg::DefineWeaponType("GoliathSword", 	$SkillSlashing, $SwordAccessoryType, 	"6 100", 12, "An enormous sword");
rpg::DefineWeaponType("Slasher", 		$SkillSlashing, $SwordAccessoryType, 	"6 225", 9, "A terrible blade forged in hellfire");
rpg::DefineWeaponType("KeldriniteLS", 	$SkillSlashing, $SwordAccessoryType, 	"6 150", 3, "The sword of legends. Some say it doesn't exist.");
//____________________________________________________________________________________________________
rpg::DefineWeaponType("OrcishAxe", 		$SkillSlashing, $AxeAccessoryType, 		"6 50", 15, "An Orcish Axe. Powerful, but weighted strangely.", 5);
$ItemDropFlags[OrcishAxe] = $ItemDropIgnoreLuck;
//____________________________________________________________________________________________________
Smith::addItem("Hatchet","WoodChunk 1 Granite 1 SmallRock 5","Hatchet 1", $SkillSlashing);
Smith::addItem("BroadSword","WoodChunk 1 Bandages 5 Iron 3","BroadSword 1", $SkillSlashing);
Smith::addItem("WarAxe","Hatchet 1 Wood 1 Bandages 10 Copper 10 Iron 5","WarAxe 1", $SkillSlashing);
Smith::addItem("LongSword","BroadSword 1 Hammer 1 SmallRock 100","LongSword 1", $SkillSlashing);
Smith::addItem("BattleAxe","OrcishAxe 1 Aluminum 1","BattleAxe 1", $SkillSlashing);
Smith::addItem("ElvenSword","LongSword 1 Jade 25 Emerald 2 MagicDust 75","ElvenSword 1", $SkillSlashing);
Smith::addItem("Halberd","WarAxe 1 Hammer 1 Iron 15 WaterJug 1","Halberd 1", $SkillSlashing);
Smith::addItem("Claymore","Hammer 2 WaterFlask 3 Bandages 10 Iron 40 WoodChunk 1 Aluminum 20","Claymore 1", $SkillSlashing);
Smith::addItem("GoliathSword","FireballIdiom 1 Claymore 1 Silver 10 Gold 5 DragonScale 1","GoliathSword 1", $SkillSlashing);
Smith::addItem("Slasher","FlameThrower 1 Dagger 1 Radium 1","Slasher 1", $SkillSlashing);
//____________________________________________________________________________________________________
ItemImageData BroadswordImage {
	shapeFile  = "long_sword2";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(Broadsword);	minEnergy = 0;
	maxEnergy = 0;	accuFire = true;	sfxFire = SoundBladeSwingLG;	sfxActivate = SoundPoleSwingSM;
};
ItemData Broadsword {
	heading = "bWeapons";	description = "Broad Sword";	className = "Weapon";
	shapeFile  = "long_sword2";	hudIcon = "blaster";	shadowDetailMask = 4;
	imageType = BroadswordImage;	price = 0;	showWeaponBar = true;
};
function BroadswordImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Broadsword), Broadsword);
}



ItemImageData LongswordImage {
	shapeFile  = "long_sword";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(Longsword);	minEnergy = 0;
	maxEnergy = 0;	accuFire = true;	sfxFire = SoundBladeSwingLG;	sfxActivate = SoundPoleSwingSM;
};
ItemData Longsword {
	heading = "bWeapons";	description = "Long Sword";	className = "Weapon";
	shapeFile  = "long_sword";	hudIcon = "blaster";	shadowDetailMask = 4;
	imageType = LongswordImage;	price = 0;	showWeaponBar = true;
};
function LongswordImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Longsword), Longsword);
}



ItemImageData KeldriniteLSImage {
	shapeFile  = "elfinblade";	mountPoint = 0; 	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(KeldriniteLS);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundBladeSwingLG;	sfxActivate = SoundPoleSwingSM;
};
ItemData KeldriniteLS {
	heading = "bWeapons";	description = "Keldrinite Long Sword";	className = "Weapon";
	shapeFile  = "elfinblade"; hudIcon = "blaster";	shadowDetailMask = 4;
	imageType = KeldriniteLSImage;	price = 0;	showWeaponBar = true;
};
function KeldriniteLSImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(KeldriniteLS), KeldriniteLS);
}



ItemImageData ElvenSwordImage {
	shapeFile  = "greensword";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(ElvenSword);	minEnergy = 0;
	maxEnergy = 0;	accuFire = true;	sfxFire = SoundBladeSwingLG;	sfxActivate = SoundPoleSwingSM;
};
ItemData ElvenSword {
	heading = "bWeapons";	description = "Elven Sword";	className = "Weapon";
	shapeFile  = "greensword";	hudIcon = "blaster";	shadowDetailMask = 4;
	imageType = ElvenSwordImage;	price = 0;	showWeaponBar = true;
};
function ElvenSwordImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(ElvenSword), ElvenSword);
}



ItemImageData ClaymoreImage {
	shapeFile  = "sword"; sfxFire = SoundBladeSwingLG; sfxActivate = SoundPoleSwingSM; fireTime = GetDelay(Claymore);
	mountPoint = 0; weaponType = 0; reloadTime = 0; minEnergy = 0; maxEnergy = 0; accuFire = true;
};
ItemData Claymore {
	heading = "bWeapons"; description = "Claymore";	 className = "Weapon";
	shapeFile  = "sword"; hudIcon = "katana"; shadowDetailMask = 4; imageType = ClaymoreImage;
	price = 0; showWeaponBar = true;
};
function ClaymoreImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Claymore), Claymore);
}



ItemImageData GoliathswordImage {
	shapeFile  = "goliathsword"; sfxFire = SoundBladeSwingLG; sfxActivate = SoundPoleSwingSM; fireTime = GetDelay(Goliathsword);
	mountPoint = 0; weaponType = 0; reloadTime = 0; minEnergy = 0; maxEnergy = 0; accuFire = true;
};
ItemData Goliathsword {
	heading = "bWeapons"; description = "Goliath Sword";	 className = "Weapon";
	shapeFile  = "goliathsword"; hudIcon = "katana"; shadowDetailMask = 4; imageType = GoliathswordImage;
	price = 0; showWeaponBar = true;
};
function GoliathswordImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Goliathsword), Goliathsword);
}



ItemImageData SlasherImage {
	shapeFile  = "Slasher"; sfxFire = SoundBladeSwingLG; sfxActivate = SoundPoleSwingSM; fireTime = GetDelay(Slasher);
	mountPoint = 0; weaponType = 0; reloadTime = 0; minEnergy = 0; maxEnergy = 0; accuFire = true;
};
ItemData Slasher {
	heading = "bWeapons"; description = "Slasher";	 className = "Weapon";
	shapeFile  = "Slasher"; hudIcon = "katana"; shadowDetailMask = 4; imageType = SlasherImage;
	price = 0; showWeaponBar = true;
};
function SlasherImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Slasher), Slasher);
}



ItemImageData HatchetImage {
	shapeFile  = "hatchet";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(Hatchet);	minEnergy = 0; maxEnergy = 0;
	accuFire = true;	sfxFire = SoundAxeSwingSM;	sfxActivate = SoundPoleSwingSM;
};
ItemData Hatchet {
	heading = "bWeapons";	description = "Hatchet";	className = "Weapon";
	shapeFile  = "hatchet";	hudIcon = "axe";	shadowDetailMask = 4;	imageType = HatchetImage;
	price = 0;	showWeaponBar = true;
};
function HatchetImage::onFire(%player, %slot) {
	HatchetSwing(%player, GetRange(Hatchet), Hatchet);
	//MeleeAttack(%player, GetRange(Hatchet), Hatchet);
}



ItemImageData WarAxeImage {
	shapeFile  = "CRIMAXE2";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(WarAxe);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundAxeSwingSM;	sfxActivate = SoundPoleSwingSM;
};
ItemData WarAxe {
	heading = "bWeapons";	description = "War Axe";	className = "Weapon";
	shapeFile  = "CRIMAXE2";	hudIcon = "axe";	shadowDetailMask = 4;
	imageType = WarAxeImage;	price = 0;	showWeaponBar = true;
};
function WarAxeImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(WarAxe), WarAxe);
}



ItemImageData BattleAxeImage {
	shapeFile  = "BattleAxe2";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(BattleAxe);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundAxeSwingLG;	sfxActivate = SoundPoleSwingSM;
};
ItemData BattleAxe {
	heading = "bWeapons";	description = "Battle Axe";	className = "Weapon";
	shapeFile  = "BattleAxe2";	hudIcon = "axe";	shadowDetailMask = 4;
	imageType = BattleAxeImage;	price = 0;	showWeaponBar = true;
};
function BattleAxeImage::onFire(%player, %slot) {
	HatchetSwing(%player, GetRange(BattleAxe), BattleAxe);
	//MeleeAttack(%player, GetRange(BattleAxe), BattleAxe);
}



ItemImageData HalberdImage {
	shapeFile  = "BattleAxe"; 	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(Halberd);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundAxeSwingLG;	sfxActivate = SoundPoleSwingSM;
};
ItemData Halberd {
	heading = "bWeapons";	description = "Halberd";	className = "Weapon";
	shapeFile  = "BattleAxe";	hudIcon = "spear";	shadowDetailMask = 4;
	imageType = HalberdImage;	price = 0;	showWeaponBar = true;
};
function HalberdImage::onFire(%player, %slot) {
	HatchetSwing(%player, GetRange(Halberd), Halberd);
	//MeleeAttack(%player, GetRange(Halberd), Halberd);
}



ItemImageData OrcishAxeImage {
	shapeFile  = "BattleAxe2";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(OrcishAxe);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundAxeSwingLG;	sfxActivate = SoundPoleSwingSM;
};
ItemData OrcishAxe {
	heading = "bWeapons";	description = "Orcish Axe";	className = "Weapon";
	shapeFile  = "BattleAxe2";	hudIcon = "axe";	shadowDetailMask = 4;
	imageType = OrcishAxeImage;	price = 0;	showWeaponBar = true;
};
function OrcishAxeImage::onFire(%player, %slot) {
	HatchetSwing(%player, GetRange(OrcishAxe), OrcishAxe);
}
