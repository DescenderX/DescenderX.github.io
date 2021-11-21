//____________________________________________________________________________________________________
// Archery
//
// DescX Notes:
//		All GEMS/ORE can be fired as projectiles. The Sling and DartGun will use a couple, but they are
//		mostly intended to be used with the big daddy RockLauncher.
//
//____________________________________________________________________________________________________
$WeaponRange[Sling] 			= 35;
$WeaponRange[DartGun] 			= 75;
$WeaponRange[ShortBow] 			= 120;
$WeaponRange[SteelBow] 			= 200;
$WeaponRange[ElvenBow] 			= 260;
$WeaponRange[CompositeBow] 		= 360;
$WeaponRange[LightCrossbow] 	= 300;
$WeaponRange[WarBow] 			= 400;
$WeaponRange[HeavyCrossbow] 	= 500;
$WeaponRange[RepeatingCrossbow] = 280;
$WeaponRange[RockLauncher] 		= 1000;
//____________________________________________________________________________________________________
rpg::DefineWeaponType("Sling", 				$SkillArchery, $RangedAccessoryType, 	"6 11", 2, "A sling", 1.5);
rpg::DefineWeaponType("DartGun", 			$SkillArchery, $RangedAccessoryType, 	"6 16", 2, "A dart gun that blows darts and other small objects", 1.2);
rpg::DefineWeaponType("ShortBow", 			$SkillArchery, $RangedAccessoryType, 	"6 35", 3, "A short bow", 2);
rpg::DefineWeaponType("LightCrossbow", 		$SkillArchery, $RangedAccessoryType, 	"6 90", 6, "A light crossbow with reasonable firing rate", 3);
rpg::DefineWeaponType("CompositeBow", 		$SkillArchery, $RangedAccessoryType, 	"6 100", 5, "A heavy-draw composite bow", 3);
rpg::DefineWeaponType("HeavyCrossbow", 		$SkillArchery, $RangedAccessoryType, 	"6 200", 17, "A extraordinarily heavy crossbow with immense stopping power", 5);
rpg::DefineWeaponType("SteelBow", 			$SkillArchery, $RangedAccessoryType, 	"6 100", 2, "A light-weight steel bow", 1.5);
rpg::DefineWeaponType("ElvenBow", 			$SkillArchery, $RangedAccessoryType, 	"6 125", 3, "A Elven bow", 2);
rpg::DefineWeaponType("RepeatingCrossbow", 	$SkillArchery, $RangedAccessoryType, 	"6 75", 15, "A rapid-fire crossbow", 0.8);
rpg::DefineWeaponType("WarBow", 			$SkillArchery, $RangedAccessoryType, 	"6 300", 8, "A bow with hundreds of pounds of draw strength required to pull", 7);
rpg::DefineWeaponType("RockLauncher", 		$SkillArchery, $RangedAccessoryType, 	"6 300", 12, "A mysterious device that uses Magic Dust to launch ore of any kind");
//____________________________________________________________________________________________________
Smith::addItem("Sling","WoodChunk 5 Parchment 5","Sling 1", $SkillArchery);
Smith::addItem("DartGun","","DartGun 1", $SkillArchery);
Smith::addItem("ShortBow","","ShortBow 1", $SkillArchery);
Smith::addItem("LightCrossbow","","LightCrossbow 1", $SkillArchery);
Smith::addItem("CompositeBow","","CompositeBow 1", $SkillArchery);
Smith::addItem("HeavyCrossbow","","HeavyCrossbow 1", $SkillArchery);
Smith::addItem("SteelBow","","SteelBow 1", $SkillArchery);
Smith::addItem("ElvenBow","","ElvenBow 1", $SkillArchery);
Smith::addItem("RepeatingCrossbow","","RepeatingCrossbow 1", $SkillArchery);
Smith::addItem("WarBow","Sling 15 TreeSap 75 MinotaurHorn 2 WoodChunk 10 EnergyDrink 2 WaterVial 2 Bandages 20","WarBow 1", $SkillArchery);
//____________________________________________________________________________________________________
rpg::DefineWeaponType("BasicArrow", $SkillArchery, $ProjectileAccessoryType, 	"6 12", 0.1, "A basic arrow for use with most bows");
rpg::DefineWeaponType("BasicDart", 	$SkillArchery, $ProjectileAccessoryType, 	"6 14", 0.05, "A basic dart");
rpg::DefineWeaponType("BasicBolt", 	$SkillArchery, $ProjectileAccessoryType, 	"6 16", 0.2, "A a crossbow bolt");
rpg::DefineWeaponType("MetalArrow", $SkillArchery, $ProjectileAccessoryType, 	"6 30", 0.3, "An all-metal arrow");
rpg::DefineWeaponType("MetalDart", 	$SkillArchery, $ProjectileAccessoryType, 	"6 42", 0.3, "An metallic dart");
rpg::DefineWeaponType("MetalBolt", 	$SkillArchery, $ProjectileAccessoryType, 	"6 44", 0.5, "A metallic crossbow bolt");
rpg::DefineWeaponType("PoisonArrow",$SkillArchery, $ProjectileAccessoryType, 	"6 40", 0.1, "A basic arrow tipped with poison");
rpg::DefineWeaponType("PoisonDart", $SkillArchery, $ProjectileAccessoryType, 	"6 60", 0.1, "A dart tipped with poison");
rpg::DefineWeaponType("PoisonBolt", $SkillArchery, $ProjectileAccessoryType, 	"6 80", 0.2, "A crossbow bolt tipped with poison");
rpg::DefineWeaponType("GasBomb", 	$SkillArchery, $ProjectileAccessoryType, 	"6 100", 1, "A noxious gas bomb attached to an arrow, dart, or bolt");
//____________________________________________________________________________________________________
Smith::addItem("WoodChipBasicArrow","WoodChip 3","BasicArrow 1", $SkillArchery);
Smith::addItem("WoodChunkBasicArrow","WoodChunk 1","BasicArrow 1", $SkillArchery);
Smith::addItem("MetalArrow","Knife 1 Granite 10","MetalArrow 3", $SkillArchery);
Smith::addItem("PoisonDart","SmallRock 1 Quartz 1","PoisonDart 1", $SkillArchery);
Smith::addItem("PoisonArrow","Knife 1 Quartz 1","PoisonArrow 1", $SkillArchery);
Smith::addItem("PoisonBolt","Dagger 1 Quartz 1 Granite 2","PoisonBolt 1", $SkillArchery);
Smith::addItem("GasBomb","Toxin 5 Jade 2 Quartz 4","GasBomb 1", $SkillArchery);
Smith::addItem("GasBomb2","Toxin 10 Smoker 1","GasBomb 10", $SkillArchery);
//____________________________________________________________________________________________________
rpg::DefineWeaponType("SmallRock", 	$SkillArchery, $ProjectileAccessoryType, 	"6 10", 0.2);
rpg::DefineWeaponType("Granite", 	$SkillArchery, $ProjectileAccessoryType, 	"6 15", 0.2);
rpg::DefineWeaponType("Copper", 	$SkillArchery, $ProjectileAccessoryType, 	"6 15", 0.3);
rpg::DefineWeaponType("Quartz", 	$SkillArchery, $ProjectileAccessoryType, 	"6 15", 0.2);
rpg::DefineWeaponType("Opal", 		$SkillArchery, $ProjectileAccessoryType, 	"6 10", 0.4);
rpg::DefineWeaponType("Iron", 		$SkillArchery, $ProjectileAccessoryType, 	"6 30", 0.4);
rpg::DefineWeaponType("Jade", 		$SkillArchery, $ProjectileAccessoryType, 	"6 10", 0.3);
rpg::DefineWeaponType("Aluminum", 	$SkillArchery, $ProjectileAccessoryType, 	"6 20", 0.1);
rpg::DefineWeaponType("Turquoise", 	$SkillArchery, $ProjectileAccessoryType, 	"6 10", 0.2);
rpg::DefineWeaponType("Ruby", 		$SkillArchery, $ProjectileAccessoryType, 	"6 20", 0.3);
rpg::DefineWeaponType("Topaz", 		$SkillArchery, $ProjectileAccessoryType, 	"6 40", 0.3);
rpg::DefineWeaponType("Sapphire", 	$SkillArchery, $ProjectileAccessoryType, 	"6 30", 0.3);
rpg::DefineWeaponType("Silver", 	$SkillArchery, $ProjectileAccessoryType, 	"6 10", 0.1);
rpg::DefineWeaponType("Emerald", 	$SkillArchery, $ProjectileAccessoryType, 	"6 30", 0.3);
rpg::DefineWeaponType("Gold", 		$SkillArchery, $ProjectileAccessoryType, 	"6 10", 0.1);
rpg::DefineWeaponType("Diamond", 	$SkillArchery, $ProjectileAccessoryType, 	"6 100", 0.1);
rpg::DefineWeaponType("Platinum", 	$SkillArchery, $ProjectileAccessoryType, 	"6 100", 0.2);
rpg::DefineWeaponType("Radium", 	$SkillArchery, $ProjectileAccessoryType, 	"6 300", 0.3);
rpg::DefineWeaponType("Keldrinite", $SkillArchery, $ProjectileAccessoryType, 	"6 300", 5);
//____________________________________________________________________________________________________
$ProjRestrictions[SmallRock] 	= ",Sling,DartGun,RockLauncher,";
$ProjRestrictions[Quartz] 		= ",Sling,RockLauncher,";
$ProjRestrictions[Granite] 		= ",Sling,RockLauncher,";
$ProjRestrictions[Opal] 		= ",Sling,RockLauncher,";
$ProjRestrictions[Copper] 		= ",RockLauncher,";
$ProjRestrictions[Iron] 		= ",Sling,RockLauncher,";
$ProjRestrictions[Aluminum] 	= ",RockLauncher,";
$ProjRestrictions[Radium] 		= ",RockLauncher,";
$ProjRestrictions[Silver] 		= ",RockLauncher,";
$ProjRestrictions[Platinum] 	= ",RockLauncher,";
$ProjRestrictions[Jade] 		= ",Sling,RockLauncher,";
$ProjRestrictions[Turquoise] 	= ",RockLauncher,";
$ProjRestrictions[Ruby] 		= ",RockLauncher,";
$ProjRestrictions[Topaz] 		= ",RockLauncher,";
$ProjRestrictions[Sapphire] 	= ",RockLauncher,";
$ProjRestrictions[Emerald] 		= ",RockLauncher,";
$ProjRestrictions[Diamond] 		= ",DartGun,RockLauncher,";
$ProjRestrictions[BasicArrow] 	= ",ShortBow,SteelBow,CompositeBow,ElvenBow,WarBow,";
$ProjRestrictions[MetalArrow] 	= ",SteelBow,ElvenBow,WarBow,";
$ProjRestrictions[BasicDart] 	= ",DartGun,RepeatingCrossbow,";
$ProjRestrictions[MetalDart] 	= ",DartGun,HeavyCrossbow,";
$ProjRestrictions[BasicBolt] 	= ",LightCrossbow,HeavyCrossbow,RepeatingCrossbow,";
$ProjRestrictions[MetalBolt] 	= ",LightCrossbow,HeavyCrossbow,";
$ProjRestrictions[PoisonDart] 	= ",DartGun,RepeatingCrossbow,";
$ProjRestrictions[PoisonArrow] 	= ",ShortBow,SteelBow,CompositeBow,ElvenBow,WarBow,";
$ProjRestrictions[PoisonBolt] 	= ",LightCrossbow,HeavyCrossbow,RepeatingCrossbow,";
$ProjRestrictions[GasBomb] 		= ",Sling,ElvenBow,HeavyCrossbow,WarBow,RockLauncher,";
//____________________________________________________________________________________________________
$ProjectileQuantityDropDampen[SmallRock] 	= 50;
$ProjectileQuantityDropDampen[BasicArrow] 	= 50;
$ProjectileQuantityDropDampen[MetalArrow] 	= 50;
$ProjectileQuantityDropDampen[BasicDart] 	= 50;
$ProjectileQuantityDropDampen[MetalDart] 	= 50;
$ProjectileQuantityDropDampen[BasicBolt] 	= 50;
$ProjectileQuantityDropDampen[MetalBolt] 	= 50;
$ProjectileQuantityDropDampen[PoisonDart] 	= 25;
$ProjectileQuantityDropDampen[PoisonArrow] 	= 25;
$ProjectileQuantityDropDampen[PoisonBolt] 	= 25;
$ProjectileQuantityDropDampen[GasBomb] 		= 5;

//____________________________________________________________________________________________________
// Archery projectile
//____________________________________________________________________________________________________
function ProjectileAttackEcho(%clientId, %weapon, %vel, %loadedProjectile) {
	if(fetchData(%clientId, "invisible") == 2) {
		return;
	}
	
	%zoffset = 0.44;

	%arrow = newObject("", "Item", %loadedProjectile, 1, false);
	%arrow.owner = %clientId;
	%arrow.delta = 1;
	%arrow.weapon = %weapon;

	addToSet("MissionCleanup", %arrow);
  	schedule("Item::Pop(" @ %arrow @ ");", 30, %arrow);

	//double-check stuff
	$ProjectileDoubleCheck[%arrow] = True;
	schedule("$ProjectileDoubleCheck[" @ %arrow @ "] = \"\";", 0.2, %arrow);

	%rot = GameBase::getRotation(%clientId);
	%newrot = (GetWord(%rot, 0) - %zoffset) @ " " @ GetWord(%rot, 1) @ " " @ GetWord(%rot, 2);

	GameBase::setRotation(%clientId, %newrot);
	GameBase::throw(%arrow, Client::getOwnedObject(%clientId), %vel, false);
	GameBase::setRotation(%arrow, %rot);
	GameBase::setRotation(%clientId, %rot);

	%efxpos = GameBase::getPosition(%clientId);
	playSound(%weapon.imageType.sfxFire, %efxpos);

	PostAttack(%clientId, %weapon);
}
function ProjectileAttack(%clientId, %weapon, %vel) {
	dbecho($dbechoMode, "ProjectileAttack(" @ %clientId @ ", " @ %weapon @ ", " @ %vel @ ")");

	if(fetchData(%clientId, "invisible") == 2) {
		Client::sendMessage(%clientId, $MsgRed, "You cannot attack while Shadow Walking. #shadowwalk again to unhide.");
		return;
	}


	//==== ANTI-SPAM CHECK, CAUSE FOR SPAM UNKNOWN ==========
	//%time = getIntegerTime(true) >> 5;
	//if(%time - %clientId.lastFireTime <= $fireTimeDelay)
	//	return;
	//%clientId.lastFireTime = %time;
	//=======================================================
	if(%clientId.sleepMode > 0)
		return;
	if($WeaponDelay[%weapon] != ""){
		if($justRanged[%clientId])
			return;
	}
	else
		$WeaponDelay[%weapon] = GetDelay(%weapon);

	$justRanged[%clientId] = True;
	schedule("$justRanged["@%clientId@"]=\"\";",$WeaponDelay[%weapon]-0.11);

	%loadedProjectile = fetchData(%clientId, "LoadedProjectile " @ %weapon);
	if(%loadedProjectile == ""){
		if(!Player::isAiControlled(%clientId)){
			processMenuselectrweapon(%clientId, %weapon);
		}
		return;
	}
	if(belt::hasthisstuff(%clientId, %loadedProjectile) <= 0)
		return;

	%repeats = AddBonusStatePoints(%clientId, "ASU");
	%stagger = $WeaponDelay[%weapon] / (%repeats + 1);
	for(%x = %repeats; %x > 0; %x--) {
		schedule("ProjectileAttackEcho(" @ %clientId @ ", " @ %weapon @ ", " @ %vel @ ", " @ %loadedProjectile @ ");",  %x * %stagger );
	}

	%zoffset = 0.44;

	%arrow = newObject("", "Item", %loadedProjectile, 1, false);
	%arrow.owner = %clientId;
	%arrow.delta = 1;
	%arrow.weapon = %weapon;

	addToSet("MissionCleanup", %arrow);
  	schedule("Item::Pop(" @ %arrow @ ");", 30, %arrow);

	//double-check stuff
	$ProjectileDoubleCheck[%arrow] = True;
	schedule("$ProjectileDoubleCheck[" @ %arrow @ "] = \"\";", 0.2, %arrow);

	%rot = GameBase::getRotation(%clientId);
	%newrot = (GetWord(%rot, 0) - %zoffset) @ " " @ GetWord(%rot, 1) @ " " @ GetWord(%rot, 2);

	GameBase::setRotation(%clientId, %newrot);
	GameBase::throw(%arrow, Client::getOwnedObject(%clientId), %vel, false);
	GameBase::setRotation(%arrow, %rot);
	GameBase::setRotation(%clientId, %rot);

	belt::takethisstuff(%clientId, %loadedProjectile, 1);

	PostAttack(%clientId, %weapon);
}


//____________________________________________________________________________________________________
// Ranged
//____________________________________________________________________________________________________
ItemImageData SlingImage {
	shapeFile = "Sling";	mountPoint = 0;	weaponType = 0; 
	ammoType = "";	projectileType = NoProjectile;	accuFire = false;
	reloadTime = 0;	fireTime = GetDelay(Sling);	lightType = 3;  // Weapon Fire
	lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = SoundBluntSwingSM;	sfxActivate = CrossbowSwitch1;	sfxReload = NoSound;
};
ItemData Sling {
	description = "Sling";	className = "Weapon";
	shapeFile = "Sling";	hudIcon = "grenade";	heading = "bWeapons";	shadowDetailMask = 4;
	imageType = SlingImage;	price = 0;	showWeaponBar = true;
};
function SlingImage::onFire(%player, %slot) {
	ProjectileAttack(Player::getClient(%player), Sling, 60);
}



ItemImageData DartGunImage {
	shapeFile = "sniper";	mountPoint = 0;	weaponType = 0; 
	ammoType = "";	projectileType = NoProjectile;	accuFire = false;
	reloadTime = 0;	fireTime = GetDelay(DartGun);	lightType = 3;  // Weapon Fire
	lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = CrossbowShoot1;	sfxActivate = CrossbowSwitch1;	sfxReload = NoSound;
};
ItemData DartGun {
	description = "Dart Gun";	className = "Weapon";	
	shapeFile = "sniper";	hudIcon = "grenade";	heading = "bWeapons";	shadowDetailMask = 4;	
	imageType = DartGunImage;	price = 0;	showWeaponBar = true;
};
function DartGunImage::onFire(%player, %slot) {
	ProjectileAttack(Player::getClient(%player), DartGun, 60);
}



ItemImageData ShortBowImage {
	shapeFile = "longbow";	mountPoint = 0;	weaponType = 0; 
	ammoType = "";	projectileType = NoProjectile;	accuFire = false;	reloadTime = 0;
	fireTime = GetDelay(ShortBow);	lightType = 3;  // Weapon Fire
	lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = BowShoot1;	sfxActivate = CrossbowSwitch1;	sfxReload = NoSound;
};
ItemData ShortBow {
	description = "Short Bow";	className = "Weapon";
	shapeFile = "longbow";	hudIcon = "bow";	heading = "bWeapons";	shadowDetailMask = 4;
	imageType = ShortBowImage;	price = 0;	showWeaponBar = true;
};
function ShortBowImage::onFire(%player, %slot) {
	ProjectileAttack(Player::getClient(%player), ShortBow, 100);
}



ItemImageData SteelBowImage {
	shapeFile = "steelbow";	mountPoint = 0;	weaponType = 0; 
	ammoType = "";	projectileType = NoProjectile;	accuFire = false;	reloadTime = 0;
	fireTime = GetDelay(SteelBow);	lightType = 3;  // Weapon Fire
	lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = BowShoot1;	sfxActivate = CrossbowSwitch1;	sfxReload = NoSound;
};
ItemData SteelBow {
	description = "Steel Bow";	className = "Weapon";
	shapeFile = "steelbow";	hudIcon = "bow";	heading = "bWeapons";	shadowDetailMask = 4;	
	imageType = SteelBowImage;	price = 0;	showWeaponBar = true;
};
function SteelBowImage::onFire(%player, %slot) {
	ProjectileAttack(Player::getClient(%player), SteelBow, 100);
}



ItemImageData ElvenBowImage {
	shapeFile = "marblebow";	mountPoint = 0;	weaponType = 0; 
	ammoType = "";	projectileType = NoProjectile;	accuFire = false;	reloadTime = 0;
	fireTime = GetDelay(ElvenBow);	lightType = 3;  // Weapon Fire
	lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = BowShoot1;	sfxActivate = CrossbowSwitch1;	sfxReload = NoSound;
};
ItemData ElvenBow {
	description = "Elven Bow";	className = "Weapon";
	shapeFile = "marblebow";	hudIcon = "bow";	heading = "bWeapons";	shadowDetailMask = 4;
	imageType = ElvenBowImage;	price = 0;	showWeaponBar = true;
};
function ElvenBowImage::onFire(%player, %slot) {
	ProjectileAttack(Player::getClient(%player), ElvenBow, 100);
}



ItemImageData CompositeBowImage {
	shapeFile = "comp_bow"; mountPoint = 0;	weaponType = 0; 
	ammoType = "";	projectileType = NoProjectile;	accuFire = false;
	reloadTime = 0;	fireTime = GetDelay(CompositeBow);	lightType = 3;  // Weapon Fire
	lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = BowShoot1;	sfxActivate = CrossbowSwitch1;	sfxReload = NoSound;
};
ItemData CompositeBow {
	description = "Composite Bow";	className = "Weapon";
	shapeFile = "comp_bow";	hudIcon = "bow";	heading = "bWeapons";	shadowDetailMask = 4;
	imageType = CompositeBowImage;	price = 0;	showWeaponBar = true;
};
function CompositeBowImage::onFire(%player, %slot) {
	ProjectileAttack(Player::getClient(%player), CompositeBow, 100);
}



ItemImageData WarBowImage {
	shapeFile = "goldenbow";	mountPoint = 0;	weaponType = 0; 
	ammoType = "";	projectileType = NoProjectile;	accuFire = false;
	reloadTime = 0;	fireTime = GetDelay(WarBow);	lightType = 3;  // Weapon Fire
	lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = BowShoot1;	sfxActivate = CrossbowSwitch1;	sfxReload = NoSound;
};
ItemData WarBow {
	description = "War Bow";	className = "Weapon";	
	shapeFile = "goldenbow";	hudIcon = "bow";	heading = "bWeapons";	shadowDetailMask = 4;
	imageType = WarBowImage;	price = 0;	showWeaponBar = true;
};
function WarBowImage::onFire(%player, %slot) {
	ProjectileAttack(Player::getClient(%player), WarBow, 100);
}



ItemImageData RockLauncherImage {
	shapeFile = "mortargun";	mountPoint = 0;	weaponType = 0; 
	ammoType = "";	projectileType = NoProjectile;	accuFire = false;
	reloadTime = 0;	fireTime = GetDelay(RockLauncher);	lightType = 3;  // Weapon Fire
	lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = RockLauncherFire;	sfxActivate = CrossbowSwitch1;	sfxReload = NoSound;
};
ItemData RockLauncher {
	description = "Rock Launcher";	className = "Weapon";	
	shapeFile = "mortargun";	hudIcon = "bow";	heading = "bWeapons";	shadowDetailMask = 4;
	imageType = RockLauncherImage;	price = 0;	showWeaponBar = true;
};
function RockLauncherImage::onFire(%player, %slot) {
	ProjectileAttack(Player::getClient(%player), RockLauncher, 120);
}



ItemImageData LightCrossbowImage {
	shapeFile = "crossbow";	mountPoint = 0;	weaponType = 0; 
	ammoType = "";	projectileType = NoProjectile;	accuFire = false;
	reloadTime = 0;	fireTime = GetDelay(LightCrossbow);	lightType = 3;  // Weapon Fire
	lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = CrossbowShoot1;	sfxActivate = CrossbowSwitch1;	sfxReload = NoSound;
};
ItemData LightCrossbow {
	description = "Light Crossbow";	className = "Weapon";
	shapeFile = "crossbow";	hudIcon = "grenade";	heading = "bWeapons";	shadowDetailMask = 4;
	imageType = LightCrossbowImage;	price = 0;	showWeaponBar = true;
};
function LightCrossbowImage::onFire(%player, %slot) {
	ProjectileAttack(Player::getClient(%player), LightCrossbow, 80);
}



ItemImageData HeavyCrossbowImage {
	shapeFile = "Crossbowsteel";	mountPoint = 0;	weaponType = 0; 
	ammoType = "";	projectileType = NoProjectile;	accuFire = false;	reloadTime = 0;
	fireTime = GetDelay(HeavyCrossbow);	lightType = 3;  // Weapon Fire
	lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = CrossbowShoot1;	sfxActivate = CrossbowSwitch1;	sfxReload = NoSound;
};
ItemData HeavyCrossbow {
	description = "Heavy Crossbow";	className = "Weapon";	
	shapeFile = "Crossbowsteel";	hudIcon = "grenade";	heading = "bWeapons";	shadowDetailMask = 4;
	imageType = HeavyCrossbowImage;	price = 0;	showWeaponBar = true;
};
function HeavyCrossbowImage::onFire(%player, %slot) {
	ProjectileAttack(Player::getClient(%player), HeavyCrossbow, 100);
}



ItemImageData RepeatingCrossbowImage {
	shapeFile = "shotgun";	mountPoint = 0;	weaponType = 0; 
	ammoType = "";	projectileType = NoProjectile;	accuFire = false;	reloadTime = 0;
	fireTime = GetDelay(RepeatingCrossbow);	lightType = 3;  // Weapon Fire
	lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = CrossbowShoot1;	sfxActivate = CrossbowSwitch1;	sfxReload = NoSound;
};
ItemData RepeatingCrossbow {
	description = "Repeating Crossbow";	className = "Weapon";
	shapeFile = "shotgun";	hudIcon = "grenade";	heading = "bWeapons";	shadowDetailMask = 4;
	imageType = RepeatingCrossbowImage;	price = 0;	showWeaponBar = true;
};
function RepeatingCrossbowImage::onFire(%player, %slot) {
	ProjectileAttack(Player::getClient(%player), RepeatingCrossbow, 100);
}



//____________________________________________________________________________________________________
ItemData SmallRock 	{	description = "Small Rock";		className = "Projectile";	shapeFile = "little_rock";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData BasicArrow {	description = "Basic Arrow";	className = "Projectile";	shapeFile = "tracer";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData MetalArrow {	description = "Metal Arrow";	className = "Projectile";	shapeFile = "bullet";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData MetalDart 	{	description = "Metal Dart";	className = "Projectile";		shapeFile = "bullet";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData BasicBolt 	{	description = "Basic Bolt";	className = "Projectile";		shapeFile = "tracer";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData MetalBolt 	{	description = "Metal Bolt";	className = "Projectile";		shapeFile = "bullet";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData BasicDart 	{	description = "Basic Dart";	className = "Projectile";		shapeFile = "tracer";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData PoisonDart {	description = "Poison Dart";className = "Projectile";		shapeFile = "mortar";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData PoisonArrow{	description = "Poison Arrow";className = "Projectile";		shapeFile = "mortar";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData PoisonBolt {	description = "Poison Bolt";className = "Projectile";		shapeFile = "mortar";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData GasBomb 	{	description = "Gas Bomb";className = "Projectile";			shapeFile = "mortar";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Granite 	{	description = "Granite";className = "Projectile";			shapeFile = "granite";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Quartz 	{	description = "Quartz";className = "Projectile";			shapeFile = "quartz";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Diamond 	{	description = "Diamond";className = "Projectile";			shapeFile = "diamond";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Emerald 	{	description = "Emerald";className = "Projectile";			shapeFile = "emerald";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Topaz 		{	description = "Topaz";className = "Projectile";				shapeFile = "topaz";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Sapphire 	{	description = "Sapphire";className = "Projectile";			shapeFile = "saphire";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Ruby 		{	description = "Ruby";className = "Projectile";				shapeFile = "ruby";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Turquoise 	{	description = "Turquoise";className = "Projectile";			shapeFile = "turquoise";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Jade 		{	description = "Jade";className = "Projectile";				shapeFile = "jade";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Opal 		{	description = "Opal";className = "Projectile";				shapeFile = "opal";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Keldrinite {	description = "Keldrinite";className = "Projectile";		shapeFile = "keldrinite";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Gold 		{	description = "Gold";className = "Projectile";				shapeFile = "gold";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Iron 		{	description = "Iron";className = "Projectile";				shapeFile = "keldrinite";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Silver 	{	description = "Silver";className = "Projectile";			shapeFile = "diamond";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Platinum 	{	description = "Platinum";className = "Projectile";			shapeFile = "diamond";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Aluminum 	{	description = "Aluminum";className = "Projectile";			shapeFile = "granite";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Copper 	{	description = "Copper";className = "Projectile";			shapeFile = "gold";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};
ItemData Radium 	{	description = "Radium";className = "Projectile";			shapeFile = "emerald";	heading = "xAmmunition";	shadowDetailMask = 4;	price = 0;};



