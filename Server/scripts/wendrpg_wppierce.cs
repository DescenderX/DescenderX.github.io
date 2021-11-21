//____________________________________________________________________________________________________
// Piercing
//
// DescX Notes:
//		Now includes throwing weapons!
//		Backstabbing is always active, so piercing weapons get the distinct advantage of massive damage output
//
//____________________________________________________________________________________________________
rpg::DefineWeaponType("Knife", 			$SkillPiercing, $SwordAccessoryType, 		"6 12", 1, "A dull knife", 2);
rpg::DefineWeaponType("Spear", 			$SkillPiercing, $PolearmAccessoryType, 		"6 24", 2, "A simple spear", 1);
rpg::DefineWeaponType("Thrown", 		$SkillPiercing, $RangedAccessoryType, 		"6 50", 0, "A special sling used for throwing knives and spears", 1.5);
rpg::DefineWeaponType("Dagger", 		$SkillPiercing, $SwordAccessoryType, 		"6 40", 3, "A sharp dagger", 0.8);
rpg::DefineWeaponType("ShortSword", 	$SkillPiercing, $SwordAccessoryType, 		"6 50", 5, "A short sword");
rpg::DefineWeaponType("Gladius", 		$SkillPiercing, $SwordAccessoryType, 		"6 66", 5, "A versatile sword");
rpg::DefineWeaponType("Trident", 		$SkillPiercing, $PolearmAccessoryType, 		"6 90", 12, "A 3-pronged trident");
rpg::DefineWeaponType("Rapier", 		$SkillPiercing, $SwordAccessoryType, 		"6 75", 5, "A razer-sharp blade");
rpg::DefineWeaponType("AwlPike", 		$SkillPiercing, $PolearmAccessoryType, 		"6 200", 15, "A long spear");
rpg::DefineWeaponType("Lance", 			$SkillPiercing, $PolearmAccessoryType, 		"6 300", 3, "A lance with incredible reach");
rpg::DefineWeaponType("Lacerator", 		$SkillPiercing, $PolearmAccessoryType, 		"6 50", 1, "The sharpest knife in the known world");
//____________________________________________________________________________________________________
rpg::DefineWeaponType("ThrowingKnife", 	$SkillPiercing, $ProjectileAccessoryType, 	"6 12", 0.25, "A knife with the hilt re-weighted for throwing");
rpg::DefineWeaponType("ThrowingSpear", 	$SkillPiercing, $ProjectileAccessoryType, 	"6 12", 0.25, "A spear properly weighted for throwing");
$ProjectileQuantityDropDampen[ThrowingKnife] 	= 30;
$ProjectileQuantityDropDampen[ThrowingSpear] 	= 50;
//____________________________________________________________________________________________________
Smith::addItem("Knife","LettingBlade 2","Knife 1", $SkillPiercing);
Smith::addItem("Spear","Knife 1 SmallRock 5 Wood 1 TreeSap 5","Spear 1", $SkillPiercing);
Smith::addItem("Thrown","Sling 1","Thrown 1", $SkillPiercing);
Smith::addItem("Dagger","","Dagger 1", $SkillPiercing);
Smith::addItem("ShortSword","","ShortSword 1", $SkillPiercing);
Smith::addItem("Gladius","","Gladius 1", $SkillPiercing);
Smith::addItem("Trident","Spear 3","Trident 1", $SkillPiercing);
Smith::addItem("Rapier","","Rapier 1", $SkillPiercing);
Smith::addItem("AwlPike","","AwlPike 1", $SkillPiercing);
Smith::addItem("Lance","","Lance 1", $SkillPiercing);

Smith::addItem("ThrowingKnife","LettingBlade 1 WoodChip 1 TreeSap 1","ThrowingKnife 1", $SkillPiercing);
Smith::addItem("ThrowingSpear","MetalArrow 10 Wood 1 TreeSap 10","ThrowingSpear 10", $SkillPiercing);
Smith::addItem("CastingBladeReweight","CastingBlade 1","ThrowingKnife 1", $SkillPiercing);
Smith::addItem("KnifeReweight","Knife 1","ThrowingKnife 1", $SkillPiercing);
Smith::addItem("SpearReweight","Spear 1","ThrowingSpear 1", $SkillPiercing);
//____________________________________________________________________________________________________
$WeaponRange[Thrown] 				= 50;
$ProjRestrictions[ThrowingKnife] 	= ",Thrown,";
$ProjRestrictions[ThrowingSpear] 	= ",Thrown,";

//____________________________________________________________________________________________________
ItemImageData KnifeImage {
	mountPoint = 0;	weaponType = 0; reloadTime = 0;	minEnergy = 0; maxEnergy = 0; accuFire = true;
	fireTime = GetDelay(Knife); shapeFile = "dagger"; sfxFire = SoundBladeSwingSM; sfxActivate = SoundAxeSwingSM;
};
ItemData Knife {
	heading = "bWeapons";	description = "Knife";	className = "Weapon";
	shapeFile  = "dagger";	hudIcon = "dagger";	shadowDetailMask = 4;
	imageType = KnifeImage;	price = 0;	showWeaponBar = true;
};
function KnifeImage::onFire(%player, %slot) {
	PickAxeSwing(%player, GetRange(Knife), Knife);
}



ItemImageData SpearImage {
	shapeFile  = "spear";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(Spear);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundPoleSwingSM;	sfxActivate = SoundAxeSwingSM;
};
ItemData Spear {
	heading = "bWeapons";	description = "Spear";	className = "Weapon";
	shapeFile  = "spear";	hudIcon = "spear";	shadowDetailMask = 4;
	imageType = SpearImage;	price = 0;	showWeaponBar = true;
};
function SpearImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Spear), Spear);
}


ItemData ThrowingKnife {
	description = "Throwing Knife";	className = "Projectile";	shapeFile = "dagger";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;
};
ItemData ThrowingSpear {
	description = "Throwing Spear";	className = "Projectile";	shapeFile = "spear";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;
};
ItemImageData ThrownImage {
	mountPoint = 0;	weaponType = 0; reloadTime = 0;	minEnergy = 0; maxEnergy = 0; accuFire = true;
	fireTime = GetDelay(Dagger); shapeFile = "Sling"; sfxFire = SoundBladeSwingSM; sfxActivate = SoundAxeSwingSM;
};
ItemData Thrown {
	heading = "bWeapons";	description = "Thrown";	className = "Weapon";
	shapeFile  = "Sling";	hudIcon = "bow";	shadowDetailMask = 4;
	imageType = ThrownImage;	price = 0;	showWeaponBar = true;
};
function ThrownImage::onFire(%player, %slot) {
	%clientId = Player::getClient(%player);
	%loadedProjectile = fetchData(%clientId, "LoadedProjectile Thrown");
	if(%loadedProjectile == "") %force = 0;
	else if(%loadedProjectile == "ThrowingKnife") %force = 45;
	else if(%loadedProjectile == "ThrowingSpear") %force = 75;
	ProjectileAttack(%clientId, Thrown, %force);
}




ItemImageData DaggerImage {
	mountPoint = 0;	weaponType = 0; reloadTime = 0;	minEnergy = 0; maxEnergy = 0; accuFire = true;
	fireTime = GetDelay(Dagger); shapeFile = "dagger"; sfxFire = SoundBladeSwingSM; sfxActivate = SoundAxeSwingSM;
};
ItemData Dagger {
	heading = "bWeapons";	description = "Dagger";	className = "Weapon";
	shapeFile  = "dagger";	hudIcon = "dagger";	shadowDetailMask = 4;
	imageType = DaggerImage;	price = 0;	showWeaponBar = true;
};
function DaggerImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Dagger), Dagger);
}



ItemImageData GladiusImage {
	mountPoint = 0;	weaponType = 0; reloadTime = 0;	minEnergy = 0; maxEnergy = 0; accuFire = true;
	fireTime = GetDelay(Gladius); shapeFile = "gladius"; sfxFire = SoundBladeSwingSM; sfxActivate = SoundAxeSwingSM;
};
ItemData Gladius {
	heading = "bWeapons";	description = "Gladius";	className = "Weapon";
	shapeFile  = "gladius";	hudIcon = "blaster";	shadowDetailMask = 4;	imageType = GladiusImage;
	price = 0;	showWeaponBar = true;
};
function GladiusImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Gladius), Gladius);
}



ItemImageData ShortswordImage {
	shapeFile  = "short_sword";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(Shortsword);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundBladeSwingSM;	sfxActivate = SoundAxeSwingSM;
};
ItemData Shortsword {
	heading = "bWeapons";	description = "Short Sword";	className = "Weapon";
	shapeFile  = "short_sword";	hudIcon = "blaster";	shadowDetailMask = 4;
	imageType = ShortswordImage;	price = 0;	showWeaponBar = true;
};
function ShortswordImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Shortsword), Shortsword);
}



ItemImageData RapierImage {
	shapeFile  = "katana";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(Rapier);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundBladeSwingSM;	sfxActivate = SoundAxeSwingSM;
};
ItemData Rapier {
	heading = "bWeapons";	description = "Rapier";	className = "Weapon";
	shapeFile  = "katana";	hudIcon = "katana";	shadowDetailMask = 4;
	imageType = RapierImage;	price = 0;	showWeaponBar = true;
};
function RapierImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Rapier), Rapier);
}





ItemImageData AwlPikeImage {
	shapeFile  = "spear2";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(AwlPike);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundPoleSwingLG;	sfxActivate = SoundAxeSwingSM;
};
ItemData AwlPike {
	heading = "bWeapons";	description = "Awl Pike";	className = "Weapon";
	shapeFile  = "spear2";	hudIcon = "trident";	shadowDetailMask = 4;
	imageType = AwlPikeImage;	price = 0;	showWeaponBar = true;
};
function AwlPikeImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(AwlPike), AwlPike);
}



ItemImageData LanceImage {
	shapeFile  = "phenssword";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(Lance);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundPoleSwingLG;	sfxActivate = SoundAxeSwingSM;
};
ItemData Lance {
	heading = "bWeapons";	description = "Lance";	className = "Weapon";
	shapeFile  = "phenssword";	hudIcon = "trident";	shadowDetailMask = 4;
	imageType = LanceImage;	price = 0;	showWeaponBar = true;
};
function LanceImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Lance), Lance);
}



ItemImageData LaceratorImage {
	shapeFile  = "knife";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(Lacerator);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundBladeSwingLG;	sfxActivate = SoundAxeSwingSM;
};
ItemData Lacerator {
	heading = "bWeapons";	description = "Lacerator";	className = "Weapon";
	shapeFile  = "knife";	hudIcon = "trident";	shadowDetailMask = 4;
	imageType = LaceratorImage;	price = 0;	showWeaponBar = true;
};
function LaceratorImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Lacerator), Lacerator);
}


ItemImageData TridentImage {
	shapeFile  = "trident"; mountPoint = 0;	weaponType = 0; 
	reloadTime = 0; fireTime = GetDelay(Trident);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundPoleSwingLG;	sfxActivate = SoundAxeSwingSM;
};
ItemData Trident {
	heading = "bWeapons";	description = "Trident";	className = "Weapon";
	shapeFile  = "trident";	hudIcon = "trident";	shadowDetailMask = 4;
	imageType = TridentImage;	price = 0;	showWeaponBar = true;
};
function TridentImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(Trident), Trident);
}

