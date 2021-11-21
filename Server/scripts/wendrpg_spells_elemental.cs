//________________________________________________________________________________________________
// ELEMENTAL
//
//	DescX Notes:
//		This is the old "Offensive Magic" tree from TRPG with twists. I intend to double the number
//		of spells here, at least, but for now the basics are covered.
//		Petrify is the only non-damage spell.
//________________________________________________________________________________________________
SpellDefinition($SkillElementalMagic, "stone", 5, 5, 
				"Stone", "Teleports two stones to the target and launches them at close range.", 18, 80, 2, 
				SoundSpinUp, SoundFlierCrash, 50, 1, 2, 0, False, False,
				$TargetDamageSpell, 0, 2.1, "SpellFXstone");

SpellDefinition($SkillElementalMagic, "ice", 10, 5, 
				"Ice", "Generates a damaging spike of ice on the target.", 50, 150, 3, 
				ActivateAS, NoSound, 2, 2, 2, 0, False, False,
				$TargetDamageSpell, 0, 1, "SpellFXicespike");				

SpellDefinition($SkillElementalMagic, "spark", 15, 10, 
				"Spark", "Instantly shocks the target with an electrical spark.", 80, 50, 5, 
				SoundLaserHit, DeflectAS, 10, 0, 6.66, 0, False, False,
				$TargetDamageSpell, 0, 1, "SpellFXSpark");

SpellDefinition($SkillElementalMagic, "fireball", 20, 15, 
				"Fireball", "Casts a fireball.", 60, 80, 6, 
				ActivateAB, NoSound, 2, 1, 1, 0, False, False,
				$TargetDamageSpell, 0, 1, "SpellFXfireball");

SpellDefinition($SkillElementalMagic, "hail", 25, 50, 
				"Hail", "Causes moisture in the air to freeze and explode into ice shards.", 25, 80, 4, 
				SoundPlasmaTurretFire, HitPawnDT, 10, 1.5, 9, 0, False, False,
				($TargetDamageSpell | $TargetSpecial), 0, 1, "SpellFXhail");

SpellDefinition($SkillElementalMagic, "beam", 30, 50, 
				"Beam", "Electricity gathers into a concentrated beam and causes intense damage to the target.", 125, 750, 0, 
				SoundHitBF, SoundFloatMineTarget, 5, 0, 5, 0, False, False,
				($TargetDamageSpell | $TargetBeamSpell), 0, 1, "BeamLaserFX");

SpellDefinition($SkillElementalMagic, "firebomb", 35, 66,
				"Fire Bomb From Hell", "Casts an explosive.", 145, 80, 10, 
				ActivateBF, NoSound, 2, 1.5, 4, 0, False, False,
				$TargetDamageSpell, 0, 1, "SpellFXfirebomb");

SpellDefinition($SkillElementalMagic, "boulder", 40, 75, 
				"Boulder", "Launches a boulder made of fire and wind. Explodes on contact.", 80, 80, 6, 
				SoundMortarReload, mineExplosion, 20, 0.25, 1.5, 0, False, False,
				($TargetDamageSpell | $TargetProjectileSpell), 0, 1, "ProjectileBoulder");

SpellDefinition($SkillElementalMagic, "shock", 45, 66, 
				"Shock", "Casts a shocking burst of electricity.", 20, 30, 3, 
				SoundELFFire, NoSound, 2, 0, 7.5, 0, False, False,
				$TargetDamageSpell, 0, 10.015, "SpellFXshock");		

SpellDefinition($SkillElementalMagic, "petrify", 50, 50, 
				"Petrify", "Freezes the target's joints causing them to stagger randomly for a short time.", 0, 80, 0, 
				SoundSpellcast, SoundSpawn2, 2, 5, 1, 10, False, False,
				($TargetOther | $TargetBuffEnemy | $TargetSpecial), "PTFY 1", 0, 0);		

SpellDefinition($SkillElementalMagic, "melt", 55, 150, 
				"Melt Bomb Attack", "Casts a firey explosive that melts away all matter.", 145, 80, 10, 
				BonusStateExpire, ExplodeLM, 2, 3, 6, 0, False, False,
				$TargetDamageSpell, 0, 3.05, "SpellFXmelt");			

SpellDefinition($SkillElementalMagic, "shatter", 60, 100, 
				"Shatter", "Causes the earth to rupture in a straight line in front of the caster.", 100, 80, 7, 
				ImpactTR, floatMineExplosion, 2, 0.1, 13.5, 0, False, False,
				($TargetDamageSpell | $TargetSpecial), 0, 1, "SpellFXshatter");
				
SpellDefinition($SkillElementalMagic, "hellstorm", 65, 150, 
				"Hellstorm", "A massive explosion ignites the air above it and starts a second chain reaction of explosions.", 50, 80, 10, 
				LoopLS, debrisLargeExplosion, 2, 9, 18, 0, False, False,
				($TargetDamageSpell | $TargetSpecial), 0, 1, "SpellFXfirebomb");

SpellDefinition($SkillElementalMagic, "sandstorm", 75, 250, 
				"Sandstorm", "Crushes the earth to dust and launches it upward. Requires a great deal of Focus to use effectively.", 250, 80, 10, 
				RespawnB, LaunchET, 2, 6, 15, 0, False, False,
				($TargetDamageSpell | $TargetSpecial), 0, 1, "SpellFXsandstorm");

SpellDefinition($SkillElementalMagic, "cryostorm", 80, 500, 
				"Cryostorm", "Causes violent expansion and contraction, eliminating all sources of heat from the area.", 75, 80, 11, 
				RespawnA, debrisLargeExplosion, 2, 5, 15, 0, False, False,
				($TargetDamageSpell | $TargetSpecial), 0, 1, "SpellFXcryostorm");
				
SpellDefinition($SkillElementalMagic, "radstorm", 70, 200, 
				"Radstorm", "A wide area is decimated with supercharged electrical energy.", 75, 80, 20, 
				LaunchLS, SoundELFFire, 2, 9, 5, 0, False, False,
				($TargetDamageSpell | $TargetSpecial), 0, 1, "SpellFXradstorm");

//________________________________________________________________________________________________
// Hardcoded Elemental magic effects
//________________________________________________________________________________________________
function CastElementalMagic(%player, %clientId, %index, %oldpos, %castPos, %castObj, %w2, %returnFlag, %skipEndCast, %isWordSmith) {
	if(%isWordSmith)	%skillLevel = GetSkillWithBonus(%clientId, $SkillWordsmith);
	else 				%skillLevel = GetSkillWithBonus(%clientId, $SkillElementalMagic);
	
	if(%castPos == "") {
		if(!%skipEndCast)	return EndCast(%clientid,%index,%castpos,%returnflag);
		else 				return;
	}
	
	if($Spell::index[radstorm] == %index) { // 32 electrical storms of random sizes appear at random positions in quick succession
		%minrad = 0;
		%maxrad = $Spell::radius[%index] * 0.8;			

		for(%i = 1; %i <= 16; %i++) {
			%tempPos = RandomPositionXY(%minrad, %maxrad);
			%newPos = NewVector(vecx(%tempPos) + vecx(%castPos),
								vecy(%tempPos) + vecy(%castPos),
								vecz(%castPos));
			schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXradstorm\", \"" @ %newPos @ "\", True, " @ %index @ "," @ %isWordSmith @ ");", %i / 4);		
		}
		for(%i = 3; %i <= 18; %i++) {
			%tempPos = RandomPositionXY(%minrad, %maxrad);
			%newPos = NewVector(vecx(%tempPos) + vecx(%castPos),
								vecy(%tempPos) + vecy(%castPos),
								vecz(%castPos) + ((getRandom() * 1000) % 8));
			schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXvertlightningrings\", \"" @ %newPos @ "\", True, " @ %index @ "," @ %isWordSmith @ ");", %i / 4);
		}
	}
	else if($Spell::index[petrify] == %index) {
		if(getObjectType(%castObj) == "Player"){
			%id = Player::getClient(%castObj);
			for(%x=1;%x<=10;%x++)
				schedule("RefreshWeight(" @ %id @ ");", %x, %castObj);
		}
	}
	else if($Spell::index[shatter] == %index) {	// a straight line of 8 crushing spells impacts
		%clientPos = GameBase::getPosition(%clientId);
		for(%i = 1; %i <= 8; %i++) {
			%newPos = Vector::getFromRot(GameBase::getRotation(%clientId), %i * 2, 1);
			%newPos = NewVector(vecx(%newpos) + vecx(%clientPos), vecy(%newpos) + vecy(%clientPos), vecz(%clientPos));
			schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXshatter\", \"" @ %newPos @ "\", True, " @ %index @ "," @ %isWordSmith @ ");", %i / 4);
		}		
	}	
	else if($Spell::index[hail] == %index) {	// creates a horizontal field of ice that erupts randomly
		for(%i = 0; %i < 7; %i++) {
			%vec = Vector::Random(1.5);
			if(VecZ(%vec) < 0) %vec = NewVector(VecX(%vec), VecY(%vec), VecZ(%vec) * -1);			
			%newPos = Vector::add(%castPos, %vec);
			schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXhail\", \"" @ %newPos @ "\", True, " @ %index @ "," @ %isWordSmith @ ");", (%i + 1) * 0.25);
		}
	}
	else if($Spell::index[hellstorm] == %index) { // 8 random columns of 4 fireballs descend from the sky
		for(%x = 0; %x <= 20; %x++) {
			%tempPos = Vector::Random(5);
			%xPos = GetWord(%tempPos, 0) + GetWord(%castPos, 0);
			%yPos = GetWord(%tempPos, 1) + GetWord(%castPos, 1);			
			%zPos = GetWord(%castPos, 2) + (3 * getRandom());	
			%newPos = %xPos @ " " @ %yPos @ " " @ %zPos;
			schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXfireball\", \"" @ %newPos @ "\", True, " @ %index @ "," @ %isWordSmith @ ");", %x/4);			
		}
	}
	else if($Spell::index[cryostorm] == %index) {	// creates a horizontal field of ice that erupts randomly
		%minrad = 1;
		%maxrad = 5;
		for(%i = 0; %i <= 16; %i++) {
			%tempPos = RandomPositionXY(%minrad, %maxrad);
			%xPos = GetWord(%tempPos, 0) + GetWord(%castPos, 0);
			%yPos = GetWord(%tempPos, 1) + GetWord(%castPos, 1);
			%zPos = GetWord(%castPos, 2);	
			%newPos = %xPos @ " " @ %yPos @ " " @ %zPos;
			schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXcryostorm1\", \"" @ %newPos @ "\", True, " @ %index @ "," @ %isWordSmith @ ");", 0.5 + (%i / 4) + getRandom() * 5);
			schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXcryostorm2\", \"" @ %newPos @ "\", True, " @ %index @ "," @ %isWordSmith @ ");", 0.5 + (%i / 4) + getRandom() * 5);
		}		
	}
	else if($Spell::index[sandstorm] == %index) {	// 48 boulders rise from the earth and explode over a short time
		for(%i = 0; %i <= 24; %i++) {
			%tempPos = Vector::Random(6);
			%xPos = GetWord(%tempPos, 0) + GetWord(%castPos, 0);
			%yPos = GetWord(%tempPos, 1) + GetWord(%castPos, 1);
			%zPos = GetWord(%castPos, 2) + (%i * 0.25);	
			%newPos = %xPos @ " " @ %yPos @ " " @ %zPos;
			%dmg = False;
			if(%x % 3 == 0)
				%dmg = True;
			schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXsandstorm\", \"" @ %newPos @ "\", " @ %dmg @ ", " @ %index @ "," @ %isWordSmith @ ");", %i/3);
		}
	}
	
	if(!%skipEndCast) return EndCast(%clientid,%index,%castpos,%returnflag);
}



//________________________________________________________________________________________________
// Various
ExplosionData flashExpLarge
{
   shapeName = "flash_large.dts";
   soundId   = debrisLargeExplosion;
   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 6.0;
   timeZero = 0.250;
   timeOne  = 0.650;
   colors[0]  = { 0.0, 0.0, 0.0  };
   colors[1]  = { 1.0, 0.5, 0.16 };
   colors[2]  = { 1.0, 0.5, 0.16 };
   radFactors = { 0.0, 1.0, 1.0 };
};
ExplosionData debrisExpSmall
{
   shapeName = "tumult_small.dts";
   soundId   = debrisSmallExplosion;

   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 2.5;

   timeZero = 0.250;
   timeOne  = 0.650;

   colors[0]  = { 0.0, 0.0, 0.0  };
   colors[1]  = { 1.0, 0.5, 0.16 };
   colors[2]  = { 1.0, 0.5, 0.16 };
   radFactors = { 0.0, 1.0, 1.0 };
};
ExplosionData debrisExpMedium
{
   shapeName = "tumult_medium.dts";
   soundId   = debrisMediumExplosion;

   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 3.5;

   timeZero = 0.250;
   timeOne  = 0.650;

   colors[0]  = { 0.0, 0.0, 0.0  };
   colors[1]  = { 1.0, 0.5, 0.16 };
   colors[2]  = { 1.0, 0.5, 0.16 };
   radFactors = { 0.0, 1.0, 1.0 };
};


//________________________________________________________________________________________________
// firebomb
ExplosionData FirebombExplosion 
{
	shapeName = "BigRed.dts";
	soundId   = ExplodeLM;
	faceCamera = true;
	randomSpin = false;
	hasLight   = true;
	lightRange = 8.0;
	timeScale = 1.5;
	timeZero = 0.0;
	timeOne  = 0.500;
	colors[0]  = { 0.0, 0.0, 0.0 };
	colors[1]  = { 1.0, 1.0, 1.0 };
	colors[2]  = { 1.0, 1.0, 1.0 };
	radFactors = { 0.0, 1.0, 1.0 };
};
MineData SpellFXfirebomb
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";
	shapeFile = "smoke";
	shadowDetailMask = 4;
	explosionId = FirebombExplosion;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
function SpellFXfirebomb::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// melt
ExplosionData MeltExplosion
{
	shapeName = "BigRed.dts";
	soundId   = NoSound;
	faceCamera = true;
	randomSpin = true;
	hasLight   = true;
	lightRange = 10.0;
	timeScale = 1.5;
	timeZero = 0.150;
	timeOne  = 0.500;
	colors[0]  = { 0.0, 0.0,  0.0 };
	colors[1]  = { 1.0, 0.63, 0.0 };
	colors[2]  = { 1.0, 0.63, 0.0 };
	radFactors = { 0.0, 1.0, 0.9 };
};
MineData SpellFXmelt
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";
	shapeFile = "smoke";
	shadowDetailMask = 4;
	explosionId = MeltExplosion;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
function SpellFXmelt::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// boulder
ExplosionData BoulderExplosion 
{
	shapeName = "mortarex.dts";
	soundId   = NoSound;
	faceCamera = true;
	randomSpin = false;
	hasLight   = true;
	lightRange = 8.0;
	timeScale = 1.5;
	timeZero = 0.0;
	timeOne  = 0.500;
	colors[0]  = { 0.0, 0.0, 0.0 };
	colors[1]  = { 1.0, 1.0, 1.0 };
	colors[2]  = { 1.0, 1.0, 1.0 };
	radFactors = { 0.0, 1.0, 1.0 };
};
MineData SpellFXboulder
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";
	shapeFile = "boulder";
	shadowDetailMask = 4;
	explosionId = BoulderExplosion;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
function SpellFXboulder::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// stone
ExplosionData StoneExplosion
{
   shapeName = "chainspk.dts";
   soundId   = ricochet1;
   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 1.0;
   timeZero = 0.100;
   timeOne  = 0.900;
   colors[0]  = { 0.0, 0.0, 0.0 };
   colors[1]  = { 1.0, 1.0, 1.0 };
   colors[2]  = { 1.0, 1.0, 1.0 };
   radFactors = { 0.0, 1.0, 0.0 };

   shiftPosition = True;
};
MineData SpellFXstone
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";
	shapeFile = "med_rock";
	shadowDetailMask = 4;
	explosionId = StoneExplosion;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
function SpellFXstone::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// sandstorm
ExplosionData SandstormExplosion {
   shapeName = "tumult_large.dts";
   soundId   = debrisLargeExplosion;
   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 5.0;
   timeZero = 0.250;
   timeOne  = 0.650;
   colors[0]  = { 0.0, 0.0, 0.0  };
   colors[1]  = { 1.0, 0.5, 0.16 };
   colors[2]  = { 1.0, 0.5, 0.16 };
   radFactors = { 0.0, 1.0, 1.0 };
};
MineData SpellFXsandstorm
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";
	shapeFile = "boulder";
	shadowDetailMask = 4;
	explosionId = SandstormExplosion;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
function SpellFXsandstorm::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// fireball
ExplosionData SpellExplodeFireball {
   shapeName = "plasmaex.dts";
   soundId   = LaunchFB;
   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 4.0;
   timeZero = 0.200;
   timeOne  = 0.950;
   colors[0]  = { 1.0, 1.0,  0.0 };
   colors[1]  = { 1.0, 1.0, 0.75 };
   colors[2]  = { 1.0, 1.0, 0.75 };
   radFactors = { 0.375, 1.0, 0.9 };
};
MineData SpellFXfireball
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";
	shapeFile = "smoke";
	shadowDetailMask = 4;
	explosionId = SpellExplodeFireball;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
function SpellFXfireball::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// cryostorm
ExplosionData CryospikeExplode {
	faceCamera = true; randomSpin = true; shapeName = "cheefist.dts"; soundId   = Reflected;
	timeZero = 0.0; timeOne  = 1.0; hasLight   = true;  lightRange = 6.0;
	colors[0]  = { 0.0, 0.0, 0.0  };	colors[1]  = { 1.0, 0.5, 0.16 };	colors[2]  = { 1.0, 0.5, 0.16 };	radFactors = { 0.5, 0.5, 0.25 };	
};
ExplosionData CryospikeExplode2 {
	shapeName = "fusionex.dts"; soundId   = Reflected;
	faceCamera = true; randomSpin = true; hasLight   = true;
	lightRange = 3.0; timeZero = 0.450; timeOne  = 0.750;
	colors[0]  = { 0.25, 0.25, 1.0 }; colors[1]  = { 0.25, 0.25, 1.0 }; colors[2]  = { 1.0,  1.0,  1.0 }; radFactors = { 1.0, 1.0, 1.0 };
};
MineData SpellFXcryostorm
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";	
	shadowDetailMask = 4;
	explosionId = flashExpLarge;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
	shapeFile = "iceshield";
};
MineData SpellFXcryostorm1 {
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";
	shapeFile = "smoke";
	shadowDetailMask = 4;
	explosionId = CryospikeExplode;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
MineData SpellFXcryostorm2 {
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";
	shapeFile = "smoke";
	shadowDetailMask = 4;
	explosionId = CryospikeExplode2;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
function SpellFXcryostorm::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.5, %this); }
function SpellFXcryostorm1::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }
function SpellFXcryostorm2::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// spark
ExplosionData SparkExplosion {
	shapeName = "chainspk.dts"; soundId   = ricochet1;
	faceCamera = true; randomSpin = true; hasLight   = true;
	lightRange = 1.0; timeZero = 0.100; timeOne  = 0.900;
	colors[0]  = { 0.0, 0.0, 0.0 }; colors[1]  = { 1.0, 1.0, 0.5 }; colors[2]  = { 1.0, 1.0, 0.5 }; 
	radFactors = { 0.0, 1.0, 0.0 }; shiftPosition = True;
};
MineData SpellFXSpark {
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";
	shapeFile = "smoke";
	shadowDetailMask = 4;
	explosionId = SparkExplosion;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
function SpellFXSpark::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// shatter
MineData SpellFXshatter
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";
	shapeFile = "smoke";
	shadowDetailMask = 4;
	explosionId = debrisExpSmall;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
function SpellFXshatter::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// shock
ExplosionData ShockSpellExplosion {
	faceCamera = true; randomSpin = true; shapeName = "AURA_CHARGE.dts"; soundId = SoundShockTarget;
	timeZero = 0.0; timeOne  = 1.0; hasLight   = true;  lightRange = 6.0;
	colors[0]  = { 0.0, 0.0, 0.0  };	colors[1]  = { 0.5, 0.5, 0.5 };	colors[2]  = { 0.0, 0.1, 0.3 };	radFactors = { 0.5, 0.5, 0.5 };	
};
MineData SpellFXshock
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";	
	shadowDetailMask = 4;
	explosionId = ShockSpellExplosion;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
	shapeFile = "AURA_ENERGY";
};
function SpellFXshock::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// radstorm
MineData SpellFXradstorm
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";	
	shadowDetailMask = 4;
	explosionId = flashExpLarge;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
	shapeFile = "AIL_SHOCK";
};
ExplosionData ShockSpellExplosion3 {
	faceCamera = true; randomSpin = true; shapeName = "electrical.dts"; soundId = SoundShockTarget;
	timeZero = 0.0; timeOne  = 1.0; hasLight   = true;  lightRange = 6.0;
	colors[0]  = { 0.0, 0.0, 0.0  };	colors[1]  = { 0.5, 0.5, 0.5 };	colors[2]  = { 0.0, 0.1, 0.3 };	radFactors = { 1, 1, 1 };	
};
MineData SpellFXvertlightningrings
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";	
	shadowDetailMask = 4;
	explosionId = ShockSpellExplosion3;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
	shapeFile = "AURA_CHARGE";
};
function SpellFXradstorm::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }
function SpellFXvertlightningrings::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// hail
MineData SpellFXhail
{
	mass = 1.0;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.0;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";	
	shadowDetailMask = 4;
	explosionId = CryospikeExplode2;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
	shapeFile = "smoke";
};
function SpellFXhail::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// ice
ExplosionData SpellExplodeIce {
   shapeName = "enex.dts";
   soundId   = HitPawnDT;
   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 3.0;
   timeZero = 0.450;
   timeOne  = 0.750;
   colors[0]  = { 0.25, 0.25, 1.0 };
   colors[1]  = { 0.25, 0.25, 1.0 };
   colors[2]  = { 1.0, 1.0,  1.0 };
   radFactors = { 1.0, 1.0,  1.0 };
   shiftPosition = True;
};
MineData SpellFXicespike
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";
	shapeFile = "smoke";
	shadowDetailMask = 4;
	explosionId = SpellExplodeIce;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
function SpellFXicespike::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// beam
LaserData BeamLaserFX
{
	laserBitmapName   = "iceice.bmp";
	hitName           = "cheefist.dts";
	damageConversion  = 0.0;
	baseDamageType    = $LaserDamageType;
 	beamTime          = 0.5;
	lightRange        = 10.0;
	lightColor        = { 0.2, 0.2, 1.0 };
	detachFromShooter = false;
	hitSoundId        = NoSound;
};



//________________________________________________________________________________________________
// boulder
$ProjectileSpellVelocity["boulder"] = 10;
$ProjectileSpellZOffset["boulder"] = -0.4;
$ProjectileSpellLifetime["boulder"] = 1;
$ProjectileSpellRadius["boulder"] = 15;
MineData ProjectileBoulder
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";
	shapeFile = "boulder";
	shadowDetailMask = 4;
	explosionId = debrisExpMedium;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};



//_______________________________________________________________________________________________________________________________
MineData SpellFXMeteor {
	mass = 10;
	drag = 1.0;
	density = 1.0;
	elasticity = 0.01;
	friction = 1.0;
	shapeFile = "fire_xl";
	//shapeFile = "plasmaex";
	explosionId = FirebombExplosion;
	explosionRadius = 0.0;	damageValue = 0;	damageType = $NullDamageType;	kickBackStrength = 0;	triggerRadius = 0;	maxDamage = 1.0;
	shadowDetailMask = 4; className = "Handgrenade"; description = "Handgrenade";
};
function SpellFXMeteor::onAdd(%this) { schedule("rpg::MeteorExplosion(" @ %this @ ");", 8, %this); }
