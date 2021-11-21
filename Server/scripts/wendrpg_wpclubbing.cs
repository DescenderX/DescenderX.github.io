//____________________________________________________________________________________________________
// Bludgeoning
//
// DescX Notes:
//		Bludgeons do the least base damage overall and swing the slowest
//		Bashing is free! To make up for the damage output, Bludgeons will always try to bash on every swing.
//
//____________________________________________________________________________________________________
rpg::DefineWeaponType("BoneClub", 		$SkillBludgeoning, $BludgeonAccessoryType, 	"6 8", 2, "A sturdy bone", 1.5);
rpg::DefineWeaponType("Hammer", 		$SkillBludgeoning, $BludgeonAccessoryType, 	"6 40", 6, "A hammer for break ore and attacking", 2);
rpg::DefineWeaponType("Club", 			$SkillBludgeoning, $BludgeonAccessoryType, 	"6 30", 4, "A simple club", 1);
rpg::DefineWeaponType("StoneAxe", 		$SkillBludgeoning, $BludgeonAccessoryType, 	"6 60", 3, "A blunted stone axe", 2);
rpg::DefineWeaponType("SpikedBoneClub", $SkillBludgeoning, $BludgeonAccessoryType, 	"6 48", 5, "A bone club with spikes", 1.5);
rpg::DefineWeaponType("SpikedClub", 	$SkillBludgeoning, $BludgeonAccessoryType, 	"6 90", 8, "A spiked club", 2);
rpg::DefineWeaponType("Mace", 			$SkillBludgeoning, $BludgeonAccessoryType, 	"6 125", 12, "A smooth, heavy mace", 3.5);
rpg::DefineWeaponType("MorningStar", 	$SkillBludgeoning, $BludgeonAccessoryType, 	"6 65", 5, "A light-weight, sharpened mace", 1);
rpg::DefineWeaponType("WarHammer", 		$SkillBludgeoning, $AxeAccessoryType, 		"6 200", 18, "An enormous war hammer", 5);
rpg::DefineWeaponType("WarMaul", 		$SkillBludgeoning, $BludgeonAccessoryType, 	"6 225", 20, "A spiked mace that absorbs shock, allowing for heavier hits.", 3);
rpg::DefineWeaponType("TitanCrusher", 	$SkillBludgeoning, $BludgeonAccessoryType, 	"6 500", 30, "An ancient hammer used to defeat the giants of old. Requires unearthly strength to wield.", 4);
//____________________________________________________________________________________________________
Smith::addItem("BoneClub","SkeletonBone 2","BoneClub 1", $SkillBludgeoning);
Smith::addItem("Hammer","BoneClub 1 Copper 1 Iron 1 Granite 1","Hammer 1", $SkillBludgeoning);
Smith::addItem("Club","BoneClub 1 Hammer 1 Knife 1 Granite 5 TreeSap 5","Club 1", $SkillBludgeoning);
Smith::addItem("StoneAxe","Hatchet 1 SmallRock 20 Granite 10 WaterFlask 1","StoneAxe 1", $SkillBludgeoning);
Smith::addItem("SpikedBoneClub","BoneClub 1 SkeletonBone 2 WoodChip 20 Granite 5 TreeSap 10","SpikedBoneClub 1", $SkillBludgeoning);
Smith::addItem("SpikedClub","Club 1 MinotaurHorn 5 TreeSap 20","SpikedClub 1", $SkillBludgeoning);
Smith::addItem("Mace","Club 1 Iron 5 TreeSap 20 WaterFlask 1", "Mace 1", $SkillBludgeoning);
Smith::addItem("MorningStar","Mace 1 BlackStatue 1 Aluminum 10 WaterFlask 1","MorningStar 1", $SkillBludgeoning);
Smith::addItem("WarHammer","Hammer 2 Club 2 StoneAxe 2 TreeSap 50 WaterCask 1","WarHammer 1", $SkillBludgeoning);
Smith::addItem("WarMaul","Mace 2 Obsidian 2 Iron 20 Opal 20 WaterCask 1","WarMaul 1", $SkillBludgeoning);
//____________________________________________________________________________________________________
ItemImageData ClubImage {
	shapeFile  = "club";	mountPoint = 0; 	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(Club);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundBluntSwingSM;	sfxActivate = SoundBladeSwingLG;
};
ItemData Club {
	heading = "bWeapons";	description = "Club";	className = "Weapon";
	shapeFile  = "club";	hudIcon = "club";	shadowDetailMask = 4;
	imageType = ClubImage;	price = 0;	showWeaponBar = true;
};
function ClubImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Club), Club);
}



ItemImageData HammerImage {
	shapeFile = "Pick";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(Hammer);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundBluntSwingSM;	sfxActivate = SoundBladeSwingLG;
};
ItemData Hammer {
	heading = "bWeapons";	description = "Hammer Pick";	className = "Weapon";
	shapeFile = "Pick";	hudIcon = "pick";	shadowDetailMask = 4;
	imageType = HammerImage;	price = 0;	showWeaponBar = true;
};
function HammerImage::onFire(%player, %slot) {
	PickAxeSwing(%player, GetRange(Hammer), Hammer);
}



ItemImageData SpikedClubImage {
	shapeFile  = "spikedclub";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(SpikedClub); minEnergy = 0; maxEnergy = 0;
	accuFire = true;	sfxFire = SoundBluntSwingSM;	sfxActivate = SoundBladeSwingLG;
};
ItemData SpikedClub {
	heading = "bWeapons";	description = "Spiked Club";	className = "Weapon";
	shapeFile  = "spikedclub";	hudIcon = "sclub";	shadowDetailMask = 4;
	imageType = SpikedClubImage;	price = 0;	showWeaponBar = true;
};
function SpikedClubImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(SpikedClub), SpikedClub);
}



ItemImageData MaceImage {
	shapeFile  = "mace";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(Mace);	minEnergy = 0; maxEnergy = 0;
	accuFire = true;	sfxFire = SoundBluntSwingLG;	sfxActivate = SoundBladeSwingLG;
};
ItemData Mace {
	heading = "bWeapons";	description = "Mace";	className = "Weapon";
	shapeFile  = "mace";	hudIcon = "mace";	shadowDetailMask = 4;
	imageType = MaceImage;	price = 0;	showWeaponBar = true;
};
function MaceImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Mace), Mace);
}



ItemImageData StoneAxeImage {
	shapeFile  = "axe";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(StoneAxe);	minEnergy = 0;
	maxEnergy = 0;	accuFire = true;	sfxFire = SoundAxeSwingSM;	sfxActivate = SoundBladeSwingLG;
};
ItemData StoneAxe {
	heading = "bWeapons";	description = "Stone Axe";	className = "Weapon";
	shapeFile  = "axe";	hudIcon = "axe";	shadowDetailMask = 4;
	imageType = StoneAxeImage;	price = 0;	showWeaponBar = true;
};
function StoneAxeImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(StoneAxe), StoneAxe);
}



ItemImageData MorningStarImage {
	shapeFile  = "mace2";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(MorningStar);	minEnergy = 0;
	maxEnergy = 0;	accuFire = true;	sfxFire = SoundBluntSwingSM;	sfxActivate = SoundBladeSwingLG;
};
ItemData MorningStar {
	heading = "bWeapons";	description = "Morning Star";	className = "Weapon";
	shapeFile  = "mace2";	hudIcon = "mace";	shadowDetailMask = 4;
	imageType = MorningStarImage;	price = 0;	showWeaponBar = true;
};
function MorningStarImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(MorningStar), MorningStar);
}



ItemImageData WarHammerImage {
	shapeFile  = "hammer";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(WarHammer);	minEnergy = 0;
	maxEnergy = 0;	accuFire = true;	sfxFire = SoundBluntSwingLG;	sfxActivate = SoundBladeSwingLG;
};
ItemData WarHammer {
	heading = "bWeapons";	description = "War Hammer";	className = "Weapon";
	shapeFile  = "hammer";	hudIcon = "hammer";	shadowDetailMask = 4;
	imageType = WarHammerImage;	price = 0;	showWeaponBar = true;
};
function WarHammerImage::onFire(%player, %slot) {
	//MeleeAttack(%player, GetRange(WarHammer), WarHammer);
	PickAxeSwing(%player, GetRange(WarHammer), WarHammer);
}


ItemImageData TitanCrusherImage {
	shapeFile  = "hammer_ice";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(TitanCrusher);	minEnergy = 0;
	maxEnergy = 0;	accuFire = true;	sfxFire = SoundBluntSwingLG;	sfxActivate = SoundBladeSwingLG;
};
ItemData TitanCrusher {
	heading = "bWeapons";	description = "Titan Crusher";	className = "Weapon";
	shapeFile  = "hammer_ice";	hudIcon = "hammer";	shadowDetailMask = 4;
	imageType = TitanCrusherImage;	price = 0;	showWeaponBar = true;
};
function TitanCrusherImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(TitanCrusher), TitanCrusher);
}



ItemImageData WarMaulImage {
	shapeFile  = "hammer_bronze";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(WarMaul);	minEnergy = 0;
	maxEnergy = 0;	accuFire = true;	sfxFire = SoundBluntSwingLG;	sfxActivate = SoundBladeSwingLG;
};
ItemData WarMaul {
	heading = "bWeapons";	description = "War Maul";	className = "Weapon";
	shapeFile  = "hammer_bronze";	hudIcon = "hammer";	shadowDetailMask = 4;
	imageType = WarMaulImage;	price = 0;	showWeaponBar = true;
};
function WarMaulImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(WarMaul), WarMaul);
}



ItemImageData BoneClubImage {
	shapeFile  = "PBONESWORD";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(BoneClub);	minEnergy = 0;
	maxEnergy = 0;	accuFire = true;	sfxFire = SoundBluntSwingSM;	sfxActivate = SoundBladeSwingLG;
};
ItemData BoneClub {
	heading = "bWeapons";	description = "Bone Club";	className = "Weapon";
	shapeFile  = "PBONESWORD";	hudIcon = "club";	shadowDetailMask = 4;
	imageType = BoneClubImage; price = 0;	showWeaponBar = true;
};
function BoneClubImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(BoneClub), BoneClub);
}



ItemImageData SpikedBoneClubImage {
	shapeFile  = "PBONESWORD";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(SpikedBoneClub);	minEnergy = 0;
	maxEnergy = 0;	accuFire = true;	sfxFire = SoundBluntSwingSM;	sfxActivate = SoundBladeSwingLG;
};
ItemData SpikedBoneClub {
	heading = "bWeapons";	description = "Spiked Bone Club";	className = "Weapon";
	shapeFile  = "PBONESWORD";	hudIcon = "sclub";	shadowDetailMask = 4;
	imageType = SpikedBoneClubImage;	price = 0;	showWeaponBar = true;
};
function SpikedBoneClubImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(SpikedBoneClub), SpikedBoneClub);
}

