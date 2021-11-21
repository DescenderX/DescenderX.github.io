//____________________________________________________________________________________________________
// Wands
//
// DescX Notes:
//		More like "guns", but mostly like wands.
//		Includes a wide mix of ranged, melee, and explosive weapons.
//		Less damage than archery, but no ammo or mana is required.
//
//____________________________________________________________________________________________________
$WeaponRange[CastingBlade] 	= 20;
$WeaponRange[Smoker] 		= 10;
$WeaponRange[FrozenWand] 	= 20;
$WeaponRange[FlintCaster] 	= 70;
$WeaponRange[FlameThrower] 	= 20;
$WeaponRange[ThunderCaller] = 80;
$WeaponRange[ToxicDevice] 	= 40;
$WeaponRange[EtheralSpear] 	= 80;
$WeaponRange[JusticeStaff] 	= 100;
//____________________________________________________________________________________________________
rpg::DefineWeaponType("CastingBlade", 		$SkillWands, $SwordAccessoryType, 		"6 35", 3, "Launches a random elemental orb that explodes after a short duration", 2);
rpg::DefineWeaponType("QuarterStaff", 		$SkillWands, $BludgeonAccessoryType, 	"6 50", 8, "A quarter staff");
rpg::DefineWeaponType("Smoker", 			$SkillWands, $SwordAccessoryType, 		"6 30", 3, "Blows thick smog over a short range", 0.33);
rpg::DefineWeaponType("FrozenWand", 		$SkillWands, $SwordAccessoryType, 		"6 75", 6, "Slings a volatile mixture of dry ice and metal", 1.0);
rpg::DefineWeaponType("FlintCaster", 		$SkillWands, $SwordAccessoryType, 		"6 175", 2, "Uses Magic Dust to propel flint at hight speed", 3);
rpg::DefineWeaponType("LongStaff", 			$SkillWands, $BludgeonAccessoryType, 	"6 100", 3, "A long staff", 1.0);
rpg::DefineWeaponType("FlameThrower", 		$SkillWands, $SwordAccessoryType, 		"6 80", 8, "Ignites alcohol and propels it using Magic Dust", 0.5);
rpg::DefineWeaponType("ThunderCaller", 		$SkillWands, $SwordAccessoryType, 		"6 275", 10, "Launches a disc that attracts thunder", 5);
rpg::DefineWeaponType("ToxicDevice", 		$SkillWands, $SwordAccessoryType, 		"6 100", 1, "Rapidly releases deadly fumes that damage any living thing", 0.66);
rpg::DefineWeaponType("EtheralSpear", 		$SkillWands, $PolearmAccessoryType, 	"6 250", 0, "A magical throwing spear. Creates a physical copy of itself when thrown", 1.25);
rpg::DefineWeaponType("JusticeStaff", 		$SkillWands, $BludgeonAccessoryType, 	"6 300", 5, "Deliver Justice with this 'long staff'");
function rpg::CheatGiveAllWands(%c){
	GiveThisStuff(%c,
	"CastingBlade 1 EtheralSpear 1 ToxicDevice 1 ThunderCaller 1 FlameThrower 1 LongStaff 1 FlintCaster 1 FrozenWand 1 Smoker 1 JusticeStaff 1 QuarterStaff 1"); 
}
//____________________________________________________________________________________________________
Smith::addItem("CastingBlade","Knife 1 EnergyRock 1","CastingBlade 1", $SkillWands);
Smith::addItem("QuarterStaff","Knife 1 EnergyRock 10 WoodChunk 5","QuarterStaff 1", $SkillWands);
Smith::addItem("Smoker","Hammer 2 MagicDust 10 WaterJug 1","Smoker 1", $SkillWands);
Smith::addItem("FrozenWand","Sling 1 MagicDust 10 Sapphire 1 Turquoise 1","FrozenWand 1", $SkillWands);
Smith::addItem("FlintCaster","Hammer 1 Smoker 1 Sulfur 5 MagicDust 50 Ruby 10","FlintCaster 1", $SkillWands);
Smith::addItem("LongStaff","Knife 1 QuarterStaff 1 EnergyRock 20 TreeSap 10 Wood 1","LongStaff 1", $SkillWands);
Smith::addItem("FlameThrower","Hammer 5 Smoker 2 FlintCaster 1 Sulfur 20 Ruby 10","FlameThrower 1", $SkillWands);
Smith::addItem("ThunderCaller","Smoker 1 Hammer 5 EnchantedStone 5 ClippedWing 1 Radium 1","ThunderCaller 1", $SkillWands);				// no shops
Smith::addItem("ToxicDevice","Smoker 1 Hammer 2 TreeSap 100 Toxin 100 Sulfur 100 Jade 50 Emerald 10","ToxicDevice 1", $SkillWands);		// no shops
Smith::addItem("EtheralSpear","Spear 1 ThrowingSpear 1 ImpClaw 10 MagicDust 100 EnergyScroll 100","EtheralSpear 1", $SkillWands);
//____________________________________________________________________________________________________
function DoWandDamage(%object, %clientId, %pos, %weapon, %radius) {
	%id = Player::getClient(%object);
	%dist = Vector::getDistance(%pos, GameBase::getPosition(%id));
	if(%dist <= %radius) {
		%damage = getword($AccessoryVar[%weapon, $SpecialVar], 1);		
		if(%damage>0){
			%damage = rpg::GetSplashDamage(%dist, %radius, %damage, 1, 100);
			GameBase::virtual(%id, "onDamage", $WandDamageType, %damage, "0 0 0", "0 0 0", "0 0 0", "torso", "front_right", %clientId, %weapon);
		} else {
			%trySpell = $Spell::keyword[$Spell::index[%weapon]];
			if(%trySpell != "") {
				DoSpellDamage(%object, %clientId, %pos, $Spell::index[%weapon]);
			}
		}
	}
}
//____________________________________________________________________________________________________
function WandProjectileCollision(%projectile, %clientId, %weapon, %radius) {
	if($MineExploded[%projectile] == 0) {
		$MineTracker[%projectile] = "";
		$MineExploded[%projectile] = 1;
		%b = %radius * 2;
		%set = newObject("set", SimSet);		
		%n = containerBoxFillSet(%set, $SimPlayerObjectType, GameBase::getPosition(%projectile), %b, %b, %b, 0);
		Group::iterateRecursive(%set, DoWandDamage, %clientId, GameBase::getPosition(%projectile), %weapon, %radius);
		Mine::Detonate(%projectile);
		deleteObject(%set);
	}
}



//____________________________________________________________________________________________________
function WandProjectileLaunch(%player,%weapon,%mine,%velocity,%radius,%lifetime,%matchClientRotation,%dontRest,%trail,%zOffset,%trailTime){
	%clientId = Player::getClient(%player);
	if(Player::isAiControlled(%clientId)) {
		%closestId = fetchData(%clientId, "AILastTarget");
		%dist = vector::getDistance(GameBase::getPosition(%clientId), GameBase::getPosition(%closestId));
		%range = GetRange(%weapon);
		if(%closestId == "")
			return;
		if(%range < %dist)
			return;
	}
	
	if(fetchData(%clientId, "invisible") == 2) {
		Client::sendMessage(%clientId, $MsgRed, "You cannot use Wands while Shadow Walking. #shadowwalk again to unhide.");
		return;
	}

	%bomb = newObject("", "Mine", %mine);
	%bomb.owner = %clientId;	
	addToSet("MissionCleanup", %bomb);
	
	
	%rot = GameBase::getRotation(%clientId);
	%newrot = NewVector(VecX(%rot) + %zOffset, VecY(%rot), VecZ(%rot));
	GameBase::setRotation(%clientId, %newrot);
	GameBase::Throw(%bomb, %player, %velocity, false);
	GameBase::setRotation(%clientId, %rot);
	%vel = Item::getVelocity(%bomb);
	if(%matchClientRotation)
		GameBase::setRotation(%bomb, %rot);
	$MineTracker[%bomb] = GameBase::getPosition(%bomb);
	$MineExploded[%bomb] = 0;
	//playSound(%weapon.imageType.sfxFire, GameBase::getPosition(%clientId));
	$MineTrackerTicks[%bomb] = 1;
	schedule("WandProjectileThinker(" @ %bomb @ "," @ %weapon @ ", \"" @ %trail @ "\", " @ %dontRest @ ", " @ vecz(%vel) @ ", " @ %radius @ " );", 0.06);
	//WandProjectileThinker(%bomb, %weapon, %trail, %dontRest, vecz(%vel));
	if(%lifetime > 0)
		schedule("WandProjectileCollision(" @ %bomb @ ", " @ %bomb.owner @ ", " @ %weapon @ ", " @ %radius @ ");", %lifetime, %bomb);
}
function WandTrailFixZ(%trail, %bomb, %initialZ, %trailTime) {	
	%vel = Item::getVelocity(%bomb);
	%pos = GameBase::getPosition(%bomb);
	GameBase::setPosition(%trail, NewVector(vecx(%pos), vecy(%pos), vecz(%pos) ) );
	if(%trailTime <= 0 || %trailTime == "") 
		%trailTime = 0.09;
	schedule("GameBase::setPosition(" @ %trail @ ", GameBase::getPosition(" @ %bomb @ ")); Mine::Detonate(" @ %trail @ ");", %trailTime);
}
//____________________________________________________________________________________________________
function WandProjectileThinker(%bomb, %weapon, %trail, %dontRest, %initialZ, %radius, %trailTime) {
	%posnow = GameBase::getPosition(%bomb);
	$MineTrackerTicks[%bomb]++;
	
	if($MineTrackerTicks[%bomb] >= 0) {
		if($MineTracker[%bomb] != %posnow) {			
			if(%trail != "" && $MineTrackerTicks[%bomb] > 3) {
				%newtrail = newObject("", "Mine", %trail);
				addToSet("MissionCleanup", %newtrail);
				GameBase::Throw(%newtrail, %bomb, 0, false);
				%rot = GameBase::getRotation(%bomb);
				GameBase::setRotation(%newtrail, %rot);
				WandTrailFixZ(%newtrail, %bomb , %initialZ, %trailTime );
			}
			schedule("WandProjectileThinker(" @ %bomb @ ",\"" @ %weapon @ "\", \"" @ %trail @ "\", " @ %dontRest @ ", " @ %initialZ @ ", " @ %radius @ ", \"" @ %trailTime @ "\" );", 0.06);
		}
	}
	
	if(%dontRest && $MineTracker[%bomb] == %posnow) {
		if($MineTracker[%bomb] != "") {			
			$MineTrackerTicks[%bomb] = -1;
			WandProjectileCollision(%bomb, %bomb.owner, %weapon, %radius);			
			return;
		}
	}
	$MineTracker[%bomb] = %posnow;	
}


//____________________________________________________________________________________________________
// Quarter Staff
ItemImageData QuarterStaffImage {
	shapeFile  = "quarterstaff";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(QuarterStaff);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundPoleSwingLG;	sfxActivate = CrossbowShoot1;
};
ItemData QuarterStaff {
	heading = "bWeapons";	description = "Quarter Staff";	className = "Weapon";
	shapeFile  = "quarterstaff";	hudIcon = "spear";	shadowDetailMask = 4;
	imageType = QuarterStaffImage;	price = 0;	showWeaponBar = true;
};
function QuarterStaffImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(QuarterStaff), QuarterStaff);
}



//____________________________________________________________________________________________________
// Long Staff
ItemImageData LongStaffImage {
	shapeFile  = "longstaff";	mountPoint = 0;	weaponType = 0; 
	reloadTime = 0;	fireTime = GetDelay(LongStaff);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = SoundPoleSwingLG;	sfxActivate = CrossbowShoot1;
};
ItemData LongStaff {
	heading = "bWeapons";	description = "Long Staff";	className = "Weapon";
	shapeFile  = "longstaff";	hudIcon = "spear";	shadowDetailMask = 4;
	imageType = LongStaffImage;	price = 0;	showWeaponBar = true;
};
function LongStaffImage::onFire(%player, %slot) {
	MeleeAttack(%player, GetRange(LongStaff), LongStaff);
}




//____________________________________________________________________________________________________
// Smoker
ExplosionData SmokerTrailExplode {
	faceCamera = true; randomSpin = true; shapeName = "smoke.dts"; soundId   = NoSound;
	timeZero = 0.0; timeOne  = 1.0; hasLight   = true;  lightRange = 6.0;
	colors[0]  = { 0.0, 0.0, 0.0  };	colors[1]  = { 1.0, 0.5, 0.16 };	colors[2]  = { 1.0, 0.5, 0.16 };	radFactors = { 1.0, 1.0, 1.0 };	
};
MineData SmokerTrail {
	shapeFile = "rsmoke"; 		explosionId = SmokerTrailExplode;	explosionRadius = 0;
	mass = 0.15;	drag = 0.0;		density = 1;		elasticity = 0.0;	friction = 5.0;
	kickBackStrength = 0;		triggerRadius = 0;	maxDamage = 1.0;	damageValue = 0.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
};
ExplosionData ProjectileSmokerSmoke {
	faceCamera = true; randomSpin = true; shapeName = "smoke.dts"; soundId   = debrisSmallExplosion;
	timeZero = 0.0; timeOne  = 1.0; hasLight   = true;  lightRange = 6.0;
	colors[0]  = { 0.0, 0.0, 0.0  };	colors[1]  = { 1.0, 0.5, 0.16 };	colors[2]  = { 1.0, 0.5, 0.16 };	radFactors = { 0.5, 0.5, 0.25 };	
};
MineData ProjectileSmoker {
	shapeFile = "rsmoke"; 		explosionId = ProjectileSmokerSmoke;	explosionRadius = 0;
	mass = 0.15;	drag = 990.0;		density = 999;		elasticity = 0.0;	friction = 0.0;
	kickBackStrength = 10;		triggerRadius = 0;	maxDamage = 1.0;	damageValue = 1.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
};
ItemImageData SmokerImage {
	shapeFile = "grenadeL"; projectileType = ""; ammoType = ""; fireTime = GetDelay(Smoker);
	mountPoint = 0;	weaponType = 0; accuFire = false; reloadTime = 0;
	lightType = 3; lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = SoundSmokerAttack; sfxActivate = SoundEquipWand;	sfxReload = NoSound;
	minEnergy = 0.2; maxEnergy = 0.2;
};
ItemData Smoker {
	description = "Smoker";	className = "Weapon";
	shapeFile = "grenadeL";	hudIcon = "grenade";	heading = "bWeapons";	shadowDetailMask = 4;
	imageType = SmokerImage;	price = 0;	showWeaponBar = true;
};
function SmokerImage::onFire(%player, %slot) {
	WandProjectileLaunch(%player, Smoker, ProjectileSmoker, 3, 2, 0.5, True, True, SmokerTrail, -0.2);
}



//____________________________________________________________________________________________________
// Justice Staff
ExplosionData Shockwave {
   shapeName = "spikeshot.dts";
   soundId   = NoSound;
   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 6.0;
   timeZero = 0.250;
   timeOne  = 0.650;
   colors[0]  = { 0.0, 0.0, 0.0  };
   colors[1]  = { 1.0, 0.5, 0.16 };
   colors[2]  = { 1.0, 0.5, 0.16 };
   radFactors = { 1.0, 1.0, 1.0 };
};
MineData JusticeStaffSmokeTrail {
	shapeFile = "spikeshot"; 	explosionId = Shockwave;				explosionRadius = 1.0;
	mass = 1;	drag = 0.0;		density = 0;		elasticity = 0.0;	friction = 5.0;
	kickBackStrength = 0;		triggerRadius = 0;	maxDamage = 1.0;	damageValue = 1.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
};
MineData ProjectileJusticeStaff {
	shapeFile = "multibolt"; 	explosionId = flashExpLarge;				explosionRadius = 0;
	mass = 1;	drag = 0.0;		density = 0;		elasticity = 0.0;	friction = 5.0;
	kickBackStrength = 0;		triggerRadius = 0;	maxDamage = 1.0;	damageValue = 1.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
};
ItemImageData JusticeStaffImage {
	shapeFile = "cannon"; projectileType = ""; ammoType = ""; fireTime = GetDelay(JusticeStaff);
	mountPoint = 0;	weaponType = 0; accuFire = true; reloadTime = 0;
	lightType = 3; lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = NoSound; sfxActivate = SoundEquipWand;	sfxReload = NoSound;
	minEnergy = 0.2; maxEnergy = 1;
};
ItemData JusticeStaff {
	heading = "bWeapons";	description = "Justice Staff";	className = "Weapon";
	shapeFile  = "cannon";	hudIcon = "spear";	shadowDetailMask = 4;
	imageType = JusticeStaffImage;	price = 0;	showWeaponBar = true;
};
function JusticeStaffImage::onFire(%player, %slot) {
	playSound(JusticeStaffFire, GameBase::getPosition(Player::getClient(%player)));
	schedule("WandProjectileLaunch(" @ %player @ ", JusticeStaff, ProjectileJusticeStaff, 100, 7, 10, True, True, JusticeStaffSmokeTrail, -0.45);", 0.75);
}



//____________________________________________________________________________________________________
// Etheral Spear
ExplosionData ProjectileEtherSpearHit {
	faceCamera = true; randomSpin = false;
	timeZero = 0.0; timeOne  = 1.0;
	hasLight   = true; lightRange = 6.0;
	colors[0]  = { 0.0, 0.0, 0.0  };	colors[1]  = { 1.0, 0.5, 0.16 };	colors[2]  = { 1.0, 0.5, 0.16 };	radFactors = { 1.0, 0.0, 0.0 };
	shapeName = "spearether.dts";
	soundId   = SoundHitChain;
};
MineData ProjectileEtherSpear {
	shapeFile = "spearether"; 	explosionId = ProjectileEtherSpearHit;	explosionRadius = 0;
	mass = 1;	drag = 1.0;		density = 1;		elasticity = 0.0;	friction = 5.0;
	kickBackStrength = 0;		triggerRadius = 0;	maxDamage = 1.0;	damageValue = 1.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
};
ItemImageData EtheralSpearImage {
	shapeFile = "spearether"; projectileType = ""; ammoType = ""; fireTime = GetDelay(EtheralSpear);
	mountPoint = 0;	weaponType = 0; accuFire = true; reloadTime = 0;
	lightType = 3; lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = SoundPoleSwingSM; sfxActivate = SoundEquipWand;	sfxReload = NoSound;
	minEnergy = 0.2; maxEnergy = 1;
};
ItemData EtheralSpear {
	heading = "bWeapons";	description = "Etheral Spear";	className = "Weapon";
	shapeFile  = "spearether";	hudIcon = "spear";	shadowDetailMask = 4;
	imageType = EtheralSpearImage;	price = 0;	showWeaponBar = true;
};
function EtheralSpearImage::onFire(%player, %slot) {
	WandProjectileLaunch(%player, EtheralSpear, ProjectileEtherSpear, 50, 2, 50, True, True, "", -0.33);
}



//____________________________________________________________________________________________________
// Toxic Device
ExplosionData PoisonTrailExplode {
   shapeName = "paint.dts";
   soundId   = NoSound;
   faceCamera = true; randomSpin = true; hasLight   = true;
   lightRange = 8.0; timeScale = 1; timeZero = 0; timeOne  = 1.0;
   colors[0]  = { 0.4, 0.4,  1.0 }; colors[1]  = { 1.0, 1.0,  1.0 }; colors[2]  = { 1.0, 0.95, 1.0 };
   radFactors = { 0.5, 1.0, 1.0 };
};
MineData PoisonTrail {
	shapeFile = "mortartrail"; 		explosionId = PoisonTrailExplode;		explosionRadius = 0;
	mass = 1.0;	drag = 1.0;		density = 1.0;		elasticity = 0.8;	friction = 5.0;
	kickBackStrength = 0;		triggerRadius = 0;	maxDamage = 1.0;	damageValue = 0.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
};
ExplosionData ProjectilePoisonExplode {
   shapeName = "paint.dts";
   soundId   = SoundToxinBoom;
   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 3.0;
   timeZero = 0.450;
   timeOne  = 0.750;
   colors[0]  = { 0.25, 1, 0.25 };
   colors[1]  = { 0.25, 1, 0.25 };
   colors[2]  = { 0.25, 1, 0.25 };
   radFactors = { 1.0, 1.0, 0.5 };
   shiftPosition = True;
};
MineData ProjectilePoison {
	shapeFile = "mortar"; 	explosionId = ProjectilePoisonExplode;	explosionRadius = 0;
	mass = 1.0;	drag = 1.0;		density = 1.0;		elasticity = 1;	friction = 50.0;
	kickBackStrength = 0;		triggerRadius = 0;	maxDamage = 999.0;	damageValue = 1.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
};
ItemImageData ToxicDeviceImage {
	shapeFile = "paintgun"; projectileType = ""; ammoType = ""; fireTime = GetDelay(ToxicDevice);
	mountPoint = 0;	weaponType = 0; accuFire = true; reloadTime = 0;
	lightType = 3; lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = SoundToxinFire; sfxActivate = SoundEquipWand;	sfxReload = NoSound;
	minEnergy = 0.2; maxEnergy = 0.2;
};
ItemData ToxicDevice {
	heading = "bWeapons";	description = "Toxic Device";	className = "Weapon";
	shapeFile  = "paintgun";	hudIcon = "spear";	shadowDetailMask = 4;
	imageType = ToxicDeviceImage;	price = 0;	showWeaponBar = true;
};
function ToxicDeviceImage::onFire(%player, %slot) {WandProjectileLaunch(%player, ToxicDevice, ProjectilePoison, 65, 4, 0.5, True, True, PoisonTrail, -0.42, 0.10);}



//____________________________________________________________________________________________________
// Thunder Caller
ExplosionData ThunderCallerTrailExplode {
   shapeName = "blueball.dts";
   soundId   = NoSound;
   faceCamera = true; randomSpin = true; hasLight   = true;
   lightRange = 8.0; timeScale = 1.5; timeZero = 0.250; timeOne  = 0.850;
   colors[0]  = { 0.4, 0.4,  1.0 }; colors[1]  = { 1.0, 1.0,  1.0 }; colors[2]  = { 1.0, 0.95, 1.0 };
   radFactors = { 0.5, 1.0, 1.0 };
};
MineData ThunderCallerTrail {
	shapeFile = "blueball"; 	explosionId = ThunderCallerTrailExplode;		explosionRadius = 0;
	mass = 1.0;	drag = 1.0;		density = 1.0;		elasticity = 0.0;	friction = 5.0;
	kickBackStrength = 0;		triggerRadius = 0;	maxDamage = 1.0;	damageValue = 0.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
};
ExplosionData LargeShockwave {
   shapeName = "shockwave_large.dts";
   soundId   = rocketExplosion;
   faceCamera = false;
   randomSpin = false;
   hasLight   = true;
   lightRange = 10.0;
   timeZero = 0.100;
   timeOne  = 0.300;
   colors[0]  = { 1.0, 1.0, 1.0 };
   colors[1]  = { 1.0, 1.0, 1.0 };
   colors[2]  = { 0.0, 0.0, 0.0 };
   radFactors = { 1.0, 0.5, 0.0 };
};
MineData ProjectileThunderCaller {
	shapeFile = "discb"; 		explosionId = LargeShockwave;			explosionRadius = 0;
	mass = 10;	drag = 0.0;		density = 0;		elasticity = 0.0;	friction = 5.0;
	kickBackStrength = 0;		triggerRadius = 0;	maxDamage = 999.0;	damageValue = 1.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
};
ItemImageData ThunderCallerImage {
	shapeFile = "disc"; projectileType = ""; ammoType = ""; fireTime = GetDelay(ThunderCaller);
	mountPoint = 0;	weaponType = 0; accuFire = true; reloadTime = 0;
	lightType = 3; lightRadius = 3;	lightTime = 1;	lightColor = { 0.2, 0.5, 1.0 };
	sfxFire = shockExplosion; sfxActivate = SoundEquipWand;	sfxReload = NoSound;
	minEnergy = 0.2; maxEnergy = 1;
};
ItemData ThunderCaller {
	heading = "bWeapons";	description = "Thunder Caller";	className = "Weapon";
	shapeFile  = "disc";	hudIcon = "spear";	shadowDetailMask = 4;
	imageType = ThunderCallerImage;	price = 0;	showWeaponBar = true;
};
function ThunderCallerImage::onFire(%player, %slot) {
	WandProjectileLaunch(%player, ThunderCaller, ProjectileThunderCaller, 400, 20, 1.5, False, False, ThunderCallerTrail, -0.35);
}



//____________________________________________________________________________________________________
// Flint Caster
ExplosionData FlintTrailExplode {
   shapeName = "chainspk.dts";
   soundId   = NoSound;
   faceCamera = true; randomSpin = true; hasLight   = true;
   lightRange = 8.0; timeScale = 0.5; timeZero = 0.0; timeOne  = 1.5;
   colors[0]  = { 0.4, 0.4,  1.0 }; colors[1]  = { 1.0, 1.0,  1.0 }; colors[2]  = { 1.0, 0.95, 1.0 };
   radFactors = { 0.5, 1.0, 1.0 };
};
MineData FlintTrail {
	shapeFile = "chainspk"; 	explosionId = FlintTrailExplode;		explosionRadius = 0;
	mass = 1.0;	drag = 1.0;		density = 1.0;		elasticity = 0.0;	friction = 5.0;
	kickBackStrength = 0;		triggerRadius = 0;	maxDamage = 1.0;	damageValue = 0.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
};
ExplosionData FlintExplode
{
   shapeName = "shotgunex.dts";
   soundId   = DeflectAS;
   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 2.0;
   timeZero = 0.150;
   timeOne  = 0.450;
   colors[0]  = { 1.0, 0.25, 0.25 };
   colors[1]  = { 1.0, 0.25, 0.25 };
   colors[2]  = { 1.0, 0.25, 0.25 };
   radFactors = { 1.0, 1.0, 1.0 };
   shiftPosition = True;
};
MineData ProjectileFlint {
	shapeFile = "chainspk"; 	explosionId = FlintExplode;				explosionRadius = 0;
	mass = 5;	drag = 0.0;		density = 0;		elasticity = 0.0;	friction = 500.0;
	kickBackStrength = 0;		triggerRadius = 0;	maxDamage = 999.0;	damageValue = 1.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
};
ItemImageData FlintCasterImage {
	shapeFile = "energygun"; projectileType = ""; ammoType = ""; fireTime = GetDelay(FlintCaster);
	mountPoint = 0;	weaponType = 0; accuFire = true; reloadTime = 0;
	lightType = 3; lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1.0, 1.0 };
	sfxFire = SoundFlintCasterFire; sfxActivate = SoundEquipWand;	sfxReload = NoSound;
	minEnergy = 0.2; maxEnergy = 0.3;
};
ItemData FlintCaster {
	heading = "bWeapons";	description = "Flint Caster";	className = "Weapon";
	shapeFile  = "energygun";	hudIcon = "spear";	shadowDetailMask = 4;
	imageType = FlintCasterImage;	price = 0;	showWeaponBar = true;
};
function FlintCasterImage::onFire(%player, %slot) {
	WandProjectileLaunch(%player, FlintCaster, ProjectileFlint, 250, 3, 50, True, True, FlintTrail, -0.4);
}



//____________________________________________________________________________________________________
// Flame Thrower
ExplosionData ProjectileFlamesExplode {
	faceCamera = true; randomSpin = false;
	timeZero = 0.0; timeOne  = 1.0;
	hasLight   = true; lightRange = 6.0;
	colors[0]  = { 0.0, 0.0, 0.0  };	colors[1]  = { 1.0, 0.5, 0.16 };	colors[2]  = { 1.0, 0.5, 0.16 };	radFactors = { 0.0, 0.0, 1.0 };
	shapeName = "fire_large.dts";
	soundId   = LaunchFB;
};
MineData ProjectileFlames {
	shapeFile = "fire_medium"; 	explosionId = ProjectileFlamesExplode;	explosionRadius = 0;
	mass = 0.5;	drag = 999.0;		density = 999;		elasticity = 0.0;	friction = 999.0;
	kickBackStrength = 0;		triggerRadius = 0;	maxDamage = 1.0;	damageValue = 1.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
};
ItemImageData FlameThrowerImage {
	shapeFile = "FlameThrower"; projectileType = ""; ammoType = ""; fireTime = GetDelay(FlameThrower);
	mountPoint = 0;	weaponType = 0; accuFire = true; reloadTime = 0;
	lightType = 3; lightRadius = 3;	lightTime = 1;	lightColor = { 1.0, 0.8, 0 };
	sfxFire = SoundPickupAmmo; sfxActivate = SoundEquipWand;	sfxReload = NoSound;
	minEnergy	= 0.0;	 maxEnergy	= 0.3;
};
ItemData FlameThrower {
	heading = "bWeapons";	description = "Flame Thrower";	className = "Weapon";
	shapeFile  = "FlameThrower";	hudIcon = "spear";	shadowDetailMask = 4;
	imageType = FlameThrowerImage;	price = 0;	showWeaponBar = true;
};
function FlameThrowerImage::onFire(%player, %slot) {
	WandProjectileLaunch(%player, FlameThrower, ProjectileFlames, 15, 4, 0.33, True, True, "", -0.3);
}



//____________________________________________________________________________________________________
// Frozen Wand

ExplosionData CryoShardTrailBoom {
	shapeName = "enex.dts"; soundId   = NoSound;
	faceCamera = true; randomSpin = true; hasLight   = true;
	lightRange = 3.0; timeZero = 0.450; timeOne  = 0.750;
	colors[0]  = { 0.25, 0.25, 1.0 }; colors[1]  = { 0.25, 0.25, 1.0 }; colors[2]  = { 1.0,  1.0,  1.0 }; radFactors = { 1.0, 1.0, 1.0 };
};
MineData CryoShardTrail {
	shapeFile = "enex"; 	explosionId = CryoShardTrailBoom;		explosionRadius = 0;
	mass = 1.0;	drag = 100.0;	density = 100;			elasticity = 0.0;	friction = 15.0;
	kickBackStrength = 0;		triggerRadius = 0.5;	maxDamage = 1.0;	damageValue = 1.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
};


ExplosionData CryoShardExplode {
	shapeName = "fusionex.dts"; soundId   = HitPawnDT;
	faceCamera = true; randomSpin = true; hasLight   = true;
	lightRange = 3.0; timeZero = 0.450; timeOne  = 0.750;
	colors[0]  = { 0.25, 0.25, 1.0 }; colors[1]  = { 0.25, 0.25, 1.0 }; colors[2]  = { 1.0,  1.0,  1.0 }; radFactors = { 1.0, 1.0, 1.0 };
};
MineData ProjectileCryoShard {
	shapeFile = "boltbolt1"; 	explosionId = CryoShardExplode;		explosionRadius = 0;
	mass = 1.0;	drag = 100.0;	density = 100;			elasticity = 0.0;	friction = 15.0;
	kickBackStrength = 0;		triggerRadius = 0.5;	maxDamage = 1.0;	damageValue = 1.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
	isTranslucent = true;
};
ItemImageData FrozenWandImage {
	shapeFile = "boltbolt1"; projectileType = ""; ammoType = ""; fireTime = GetDelay(FrozenWand);
	mountPoint = 0;	weaponType = 0; accuFire = true; reloadTime = 0;
	lightType = 3; lightRadius = 3;	lightTime = 1;	lightColor = { 0.6, 1, 1.0 };
	sfxFire = Reflected; sfxActivate = SoundEquipWand;	sfxReload = NoSound;
	minEnergy = 0.2; maxEnergy = 0.2;
};
ItemData FrozenWand {
	heading = "bWeapons";	description = "Frozen Wand";	className = "Weapon";
	shapeFile  = "boltbolt1";	hudIcon = "spear";	shadowDetailMask = 4;
	imageType = FrozenWandImage;	price = 0;	showWeaponBar = true;
};
function FrozenWandImage::onFire(%player, %slot) {	WandProjectileLaunch(%player, FrozenWand, ProjectileCryoShard, 35, 3, 2, True, True, CryoShardTrail, -0.35);}





//____________________________________________________________________________________________________
// Casting Blade
ExplosionData CastingBladeExplosion {
   shapeName = "shockwave.dts"; soundId   = debrisSmallExplosion;
   faceCamera = true; randomSpin = true; hasLight   = true;
   lightRange = 8.0; timeScale = 1.5; timeZero = 0.0; timeOne  = 0.500;
   colors[0]  = { 0.0, 0.0, 0.0 }; colors[1]  = { 1.0, 1.0, 1.0 }; colors[2]  = { 1.0, 1.0, 1.0 }; radFactors = { 0.0, 1.0, 1.0 };
};
MineData ProjectileCastingBlade1 {
	className = "Mine"; description = "Casting Blade bomb"; shadowDetailMask = 4; damageValue = 1.0; maxDamage = 1.0;
	kickBackStrength = 0.2; triggerRadius = 2; damageType = 28; 
	mass = 5; drag = 2.0; density = 6.0; elasticity = 0.0; friction = 5.0; 
	explosionId = SpellExplodeIce; explosionRadius = 0; shapeFile = "blueorb";
}; 
MineData ProjectileCastingBlade2 {
	className = "Mine"; description = "Casting Blade bomb"; shadowDetailMask = 4; damageValue = 1.0; maxDamage = 1.0;
	kickBackStrength = 0.2; triggerRadius = 2; damageType = 28; 
	mass = 5; drag = 1.0; density = 1.0; elasticity = 0.25; friction = 2.5; 
	explosionId = SpellExplodeFireball; explosionRadius = 0; shapeFile = "redorb";
}; 
MineData ProjectileCastingBlade3 {
	className = "Mine"; description = "Casting Blade bomb"; shadowDetailMask = 4; damageValue = 1.0; maxDamage = 1.0;
	kickBackStrength = 0.2; triggerRadius = 2; damageType = 28; 
	mass = 5.0; drag = 0.5; density = 4.0; elasticity = 1.0; friction = 2.0; 
	explosionId = ProjectilePoisonExplode; explosionRadius = 0; shapeFile = "greenorb";
}; 
MineData ProjectileCastingBlade4 {
	className = "Mine"; description = "Casting Blade bomb"; shadowDetailMask = 4; damageValue = 1.0; maxDamage = 1.0;
	kickBackStrength = 0.2; triggerRadius = 2; damageType = 28; 
	mass = 5; drag = 0.5; density = 4.0; elasticity = 0.0; friction = 2.0; 
	explosionId = CastingBladeExplosion; explosionRadius = 0; shapeFile = "orb";
}; 
ItemImageData CastingBladeImage {
	shapeFile  = "dagger";	mountPoint = 0;	weaponType = 0;
	reloadTime = 0;	fireTime = GetDelay(CastingBlade);	minEnergy = 0;	maxEnergy = 0;
	accuFire = true;	sfxFire = ActivateFK;	sfxActivate = SoundEquipWand;
};
ItemData CastingBlade {
	heading = "bWeapons";	description = "Casting Blade";	className = "Weapon";
	shapeFile  = "dagger";	hudIcon = "dagger";	shadowDetailMask = 4;
	imageType = CastingBladeImage;	price = 0;	showWeaponBar = true;
};
function CastingBladeImage::onFire(%player, %slot) {
	%projectile = "ProjectileCastingBlade" @ floor(Cap(getRandom() * 5, 1, 4));
	if(%projectile == "ProjectileCastingBlade1") { %zoff = -0.3; %push = 120; }
	else { %zoff = -0.1; %push = 80; }
	WandProjectileLaunch(%player, CastingBlade, %projectile, 90, 3, 0.8 + getRandom(), False, (%projectile == "ProjectileCastingBlade2"), "", %zoff	);
}
