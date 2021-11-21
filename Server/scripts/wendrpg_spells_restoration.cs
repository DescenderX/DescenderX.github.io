//________________________________________________________________________________________________
// RESTORATION
//
// DescX Notes:
//		Restoration magic allowed me to go nuts with the new spellcasting system. The tree
//		is massive in comparison to vanilla. I don't have any system in place for visual
//		and audio effects for buffs beyond when they're applied, but given the engine's
//		current limitations I didn't see a reason to find work-arounds.
//
//		Other than this, the restoration tree is very satisfying now. You get a huge array
//		of healing spells including regeneration; you have Smite that will damage OR heal;
//		Manatap that works differently at each skill tier; and you can temporarily buff with FAVOR.
//________________________________________________________________________________________________
SpellDefinition($SkillRestorationMagic, "heal1", 10, 5, 
				"Heal Self 1", "Heals the caster.", -10, 0, 0, 
				DeActivateWA, ActivateAR, 2, 1.5, 2, 0, False, False,
				$TargetDamageSpell, 0, 1, "SpellFXhealing");
				
SpellDefinition($SkillRestorationMagic, "shield", 10, 2, 
				"Shield Self", "A magical shield adds some DEF to the caster.", 0, 0, 0, 
				ActivateTR, ActivateTD, 2, 2, 8, 150, False, False,
				$TargetBuffAlly, "DEF 1 50", 0, 0);	
				
SpellDefinition($SkillRestorationMagic, "advheal1", 25, 12, 
				"Heal Self or Other 1", "Heals the caster or someone in the LOS.", -25, 80, 0, 
				DeActivateWA, ActivateAR, 2, 1, 4, 0, False, False,
				($TargetDamageSpell | $TargetOther), 0, 1, "SpellFXhealing");
				
SpellDefinition($SkillRestorationMagic, "smite", 15, 15, 
				"Smite", "A curious force slams the target backward. If the target is an ally, the forces are reversed.", 25, 40, 3, 
				SoundHolyLaser, SoundHolySmite, 8, 0.5, 12, 0, False, False,
				($TargetInvertAllyDamage | $TargetDamageSpell | $TargetBeamSpell | $TargetSpecial | $TargetOther), 0, 3.033, "SmiteLaser");								

SpellDefinition($SkillRestorationMagic, "protect", 20, 2, 
				"Protect Self", "A magical shield adds MDEF to the caster.", 0, 0, 0, 
				ImpactTR, ActivateTD, 2, 2, 8, 150, False, False,
				$TargetBuffAlly, "MDEF 1 50", 0, 0);
				
SpellDefinition($SkillRestorationMagic, "massheal1", 50, 25, 
				"Mass Heal 1", "Heals caster and friendlies 10 meters around.", -50, 0, 10, 
				DeActivateWA, ActivateAR, 2, 2, 6, 0, False, False,
				($TargetDamageSpell | $TargetGroup), 0, 1, "SpellFXhealing");				

SpellDefinition($SkillRestorationMagic, "regen", 20, 25, 
				"Regeneration", "Regenerate some the caster's health over 15 seconds.", 0, 0, 0, 
				DeActivateWA, ActivateTD, 2, 2, 2, 7.5, False, False,
				$TargetBuffAlly, "HPR 1 2", 0, 0);
				
SpellDefinition($SkillRestorationMagic, "manatap", 30, 1, 
				"Manatap", "Removes all energy from the caster and slowly restores 3 times the amount removed.", 1, 0, 0, 
				ActivateDE, ActivateAM, 2, 3, 15, 15, False, False,
				$TargetSpecial, 0, 0, 0);
				
SpellDefinition($SkillRestorationMagic, "heal2", 10, 15, 
				"Heal Self 2", "Heals the caster.", -25, 0, 0, 
				DeActivateWA, ActivateAR, 2, 0, 3, 0, False, False,
				$TargetDamageSpell, 0, 1, "SpellFXhealing");

SpellDefinition($SkillRestorationMagic, "guardian", 20, 10, 
				"Guardian Angel", "A curious force prevents the caster's death one time for 60 seconds.", 0, 0, 0, 
				PlaceSeal, SoundObjectHarp, 2, 1, 15, 30, False, False,
				$TargetBuffAlly, "FAVOR 1", 0, 0);
				
SpellDefinition($SkillRestorationMagic, "advheal2", 25, 25, 
				"Heal Self or Other 2", "Heals the caster or someone in the LOS.", -60, 80, 0, 
				DeActivateWA, ActivateAR, 2, 1, 4, 0, False, False,
				($TargetDamageSpell | $TargetOther), 0, 1, "SpellFXhealing");				

SpellDefinition($SkillRestorationMagic, "advshield", 11, 10, 
				"Shield Self Or Other", "A magical shield adds more DEF to the caster or target in LOS.", 0, 80, 0, 
				ActivateTR, ActivateTD, 2, 2, 8, 300, False, False,
				($TargetOther | $TargetBuffAlly), "DEF 2 100", 0, 0);	

SpellDefinition($SkillRestorationMagic, "advsmite", 45, 50, 
				"Smite From Above", "A curious force slams the target into the ground from above. If the target is an ally, the forces are reversed.", 25, 40, 3, 
				SoundHolyLaser, SoundHolySmite, 20, 2, 9, 0, False, False,
				($TargetInvertAllyDamage | $TargetDamageSpell | $TargetSpecial | $TargetOther), 0, 3.075, "SpellFXsmite");
				
SpellDefinition($SkillRestorationMagic, "massheal2", 50, 50, 
				"Mass Heal 2", "Heals caster and friendlies 20 meters around.", -125, 0, 20, 
				DeActivateWA, ActivateAR, 10, 2, 6, 0, False, False,
				($TargetDamageSpell | $TargetGroup), 0, 1, "SpellFXhealing");
				
SpellDefinition($SkillRestorationMagic, "advprotect", 21, 10, 
				"Protect Self Or Other", "A magical shield adds MDEF to the caster or target in LOS.", 0, 80, 0, 
				ImpactTR, ActivateTD, 2, 2, 8, 300, False, False,
				($TargetOther | $TargetBuffAlly), "MDEF 2 100", 0, 0);
				
SpellDefinition($SkillRestorationMagic, "advregen", 41, 15, 
				"Regenerate Self Or Other", "Regenerate more of the caster or target's health over 15 seconds.", 0, 80, 0, 
				DeActivateWA, ActivateTD, 2, 2, 3, 7.5, False, False,
				($TargetOther | $TargetBuffAlly), "HPR 2 4", 0, 0);
				
SpellDefinition($SkillRestorationMagic, "heal3", 10, 30, 
				"Heal Self 3", "Heals the caster.", -50, 0, 0, 
				DeActivateWA, ActivateAR, 2, 1.5, 2, 0, False, False,
				$TargetDamageSpell, 0, 1, "SpellFXhealing");
			
SpellDefinition($SkillRestorationMagic, "advmanatap", 31, 5, 
				"Manatap Other", "Taps the target for mana. If the spell is resisted, the caster loses the amount that would have drained.", 5, 80, 0, 
				ActivateDE, ActivateAM, 2, 1.5, 4, 0, False, False,
				$TargetSpecial, 0, 0, 0);
				
SpellDefinition($SkillRestorationMagic, "advguardian", 21, 25, 
				"Guardian Angel Self Or Other", "A curious force prevents death of the caster or a target twice for 2 minutes.", 0, 80, 0, 
				PlaceSeal, SoundObjectHarp, 2, 1, 30, 60, False, False,
				($TargetOther | $TargetBuffAlly), "FAVOR 2", 0, 0);	
				
SpellDefinition($SkillRestorationMagic, "advheal3", 25, 50, 
				"Heal Self or Other 3", "Heals the caster or someone in the LOS.", -90, 80, 0, 
				DeActivateWA, ActivateAR, 2, 1, 4, 0, False, False,
				($TargetDamageSpell | $TargetOther), 0, 1, "SpellFXhealing");
				
SpellDefinition($SkillRestorationMagic, "massshield", 15, 50, 
				"Mass Shield", "A magical shield that adds DEF to all friendlies within a 10 meter radius.", 0, 0, 10, 
				ActivateTR, ActivateTD, 2, 2, 8, 450, False, False,
				($TargetGroup | $TargetBuffAlly), "DEF 3 175", 0, 0);
				
SpellDefinition($SkillRestorationMagic, "massheal3", 50, 100, 
				"Mass Heal 3", "Heals caster and friendlies 30 meters around.", -200, 0, 30, 
				DeActivateWA, ActivateAR, 10, 2, 6, 0, False, False,
				($TargetDamageSpell | $TargetGroup), 0, 1, "SpellFXhealing");				

SpellDefinition($SkillRestorationMagic, "masssmite", 100, 150, 
				"Mass Smite", "A curious force slams every target in a random direction. Allies that are smitten received a speed boost.", 50, 0, 8, 
				SoundHolyLaser, SoundHolySmite, 2, 0, 5, 7.5, False, False,
				($TargetInvertAllyDamage | $TargetGroup | $TargetDamageSpell | $TargetSpecial | $TargetOther), "SPD ! 1", 1, "SpellFXsmite");
				
SpellDefinition($SkillRestorationMagic, "massprotect", 25, 50, 
				"Mass Protect", "A magical shield that adds MDEF to all friendlies within a 10 meter radius.", 0, 0, 10, 
				ImpactTR, ActivateTD, 2, 2, 8, 450, False, False,
				($TargetGroup | $TargetBuffAlly), "MDEF 3 175", 0, 0);
				
SpellDefinition($SkillRestorationMagic, "heal4", 10, 45, 
				"Heal Self 4", "Heals the caster.", -100, 0, 0, 
				DeActivateWA, ActivateAR, 2, 1, 1, 0, False, False,
				$TargetDamageSpell, 0, 1, "SpellFXhealing");

SpellDefinition($SkillRestorationMagic, "massregen", 45, 80, 
				"Mass Regeneration", "Regenerate the health of all allies around the caster over 15 seconds.", 0, 0, 20, 
				DeActivateWA, ActivateTD, 2, 2, 5, 7.5, False, False,
				($TargetGroup | $TargetBuffAlly), "HPR 1 8", 0, 0);

SpellDefinition($SkillRestorationMagic, "massmanatap", 35, 25, 
				"Mass Manatap", "Taps all enemies around the caster for their mana, then redistributes it to the caster and nearby allies.", 8, 0, 15, 
				ActivateDE, ActivateAM, 2, 3, 20, 0, False, False,
				($TargetGroup | $TargetSpecial), 0, 0, 0);

SpellDefinition($SkillRestorationMagic, "advheal4", 25, 75, 
				"Heal Self or Other 4", "Heals the caster or someone in the LOS.", -175, 80, 0, 
				DeActivateWA, ActivateAR, 2, 4, 4, 0, False, False,
				($TargetDamageSpell | $TargetOther), 0, 1, "SpellFXhealing");

SpellDefinition($SkillRestorationMagic, "massguardian", 25, 100, 
				"Mass Guardian Angel", "A curious force prevents all nearby friendly death three times per ally, for 3 minutes.", 0, 0, 15, 
				PlaceSeal, SoundObjectHarp, 2, 1, 90, 120, False, False,
				($TargetGroup | $TargetBuffAlly), "FAVOR 3", 0, 0);

SpellDefinition($SkillRestorationMagic, "fullheal", 9998, 25, 
				"Full Heal Self", "Fully heals the caster.", -9999, 0, 0, 
				DeActivateWA, ActivateAR, 2, 2, 60, 0, False, False,
				$TargetDamageSpell, 0, 0, 0);
				
SpellDefinition($SkillRestorationMagic, "massheal4", 50, 200, 
				"Mass Heal 4", "Heals caster and friendlies 40 meters around.", -300, 0, 40, 
				DeActivateWA, ActivateAR, 20, 2, 6, 0, False, False,
				($TargetDamageSpell | $TargetGroup), 0, 1, "SpellFXhealing");				
						
SpellDefinition($SkillRestorationMagic, "massfullheal", 9999, 50, 
				"Mass Full Heal", "Fully heals the caster and friendlies 12 meters around.", 0, 0, 12, 
				DeActivateWA, ActivateAR, 2, 2, 180, 0, False, False,
				($TargetDamageSpell | $TargetGroup), 0, 0, 0);

//________________________________________________________________________________________________
// Hardcoded Restoration magic effects
//________________________________________________________________________________________________
function CastRestorationMagic(%player, %clientId, %index, %oldpos, %castPos, %castObj, %w2, %returnFlag, %skipEndCast, %isWordSmith) {
	if(%isWordSmith)	%skillLevel = GetSkillWithBonus(%clientId, $SkillWordsmith);
	else 				%skillLevel = GetSkillWithBonus(%clientId, $SkillRestorationMagic);
	
	%id = Player::getClient(%castObj);
	if(%castObj != "" && %id > 0 && (%id != %clientId || %index == $Spell::index[masssmite])) {
		if(%index == $Spell::index[smite]) {			
			%b = GameBase::getRotation(%clientId);
			%c1 = Cap(20 + %skillLevel / 4, 0, 100);
			%c2 = (%c1 / 2) * -1.0;
			if(GameBase::getTeam(%id) == GameBase::getTeam(%clientId)) {
				%c1 *= -0.25;
				%c2 *= -0.25;
			}
			%mom = Vector::getFromRot( %b, %c1, %c2 );	
			Player::applyImpulse(%id, %mom);			
		} else if(%index == $Spell::index[advsmite] || %index == $Spell::index[masssmite]) {			
			%b = GameBase::getRotation(%clientId);
			%c1 = %c1 / 5;
			%c2 = Cap(20 + %skillLevel / 3, 0, 300);
			if(GameBase::getTeam(%id) != GameBase::getTeam(%clientId)) {
				%c1 *= -1.0;
				%c2 *= -1.0;
			} else {
				%c1 *= 0.5;
				%c2 *= 0.2;
			}
			%mom = Vector::getFromRot( %b, %c1, %c2 );	
			Player::applyImpulse(%id, %mom);
		}
		else if(%index == $Spell::index[advmanatap]) {			
			if(getObjectType(%castObj) == "Player") {
				%mana = fetchData(%id, "MANA");
				if(%mana < 1) {
					Client::sendMessage(%clientId, $MsgBeige, Client::getName(%id) @ " does not have any mana to tap.");
				} else {
					%take = Cap(getRandom() * %mana * (%skillLevel / 500), $Spell::manaCost[%index], %mana);
					refreshMANA(%id, %take * 1);
					refreshMANA(%clientId, %take * -1);				
					Client::sendMessage(%clientId, $MsgBeige, "Tapped " @ Client::getName(%id) @ " for " @ RoundToFirstDecimal(%take) @ " mana.");
				}
				%returnFlag = True;				
			} else {
				refreshMANA(%clientId, $Spell::manaCost[%index] * -1);		
				Client::sendMessage(%clientId, $MsgRed, "Your attempt to tap mana backfires without a target!");
				%returnFlag = False;
			}
		}
		else if(%index == $Spell::index[massmanatap]) { // This part of massmanatap will only run on enemies. It "collects" mana.
			if(getObjectType(%castObj) == "Player") {
				%enemyMana = fetchData(%id, "MANA");
				%take = %enemyMana * Cap(getRandom(), 0.1, 0.3) * Cap(%skillLevel / 500, 1, 3);
				if(%take > %enemyMana)
					%take = %enemyMana;
				
				// choose random ally nearby?...
				
				refreshMANA(%id, %take);
				if($MassManaTapHolder[%clientId] == "")
					$MassManaTapHolder[%clientId] = 0;
				$MassManaTapHolder[%clientId] += %take;
			}
		}
	}
	else if(%index == $Spell::index[manatap]) {	// consume all mana to restore 3x over time
		%ticks = 10;
		%manaPerTick = (fetchData(%clientId, "MANA") * 3) / %ticks;
		for(%x=1;%x<=%ticks;%x++){
			schedule("refreshMANA(" @ %clientId @ ", -" @ %manaPerTick @ ");", %x * 2, %clientId);
		}
		refreshMANA(%clientId, fetchData(%clientId,"MANA"));
		%castpos = GameBase::getPosition(%clientId);
		%returnFlag = True;	
	}
	else if(%index == $Spell::index[massmanatap]) { // Schedule the distribution of mana collected from enemies
		schedule("DistributeMassManaTap(" @ %clientId @ ");", 1, %clientId);
	}
	
	if(!%skipEndCast) return EndCast(%clientid,%index,%castpos,%returnflag);
}



//________________________________________________________________________________________________
// massmanatap | Disperse collected mana 
function ManaGiver(%object, %clientId, %mana, %extra) {
	if(GameBase::getTeam(%clientId) == GameBase::getTeam(%object)) {
		refreshMANA(%object, %mana * -1);
		$MassManaTapHolder[%clientId] -= %mana;
	}
}



//________________________________________________________________________________________________
// massmanatap | Count number of valid targets to hand out collected mana in chunks
function ManaGiverCounter(%object, %clientId, %mana, %extra) { // Count friendlies
	if(GameBase::getTeam(%clientId) == GameBase::getTeam(%id)) $MassManaTapCounter[%clientId]++;
}



//________________________________________________________________________________________________
// massmanatap
function DistributeMassManaTap(%clientId) {	// called by schedule - massmanatap "collects" mana from enemies and distributes the mana to allies
	if($MassManaTapHolder[%clientId] > 0) {
		%b 		= $Spell::radius[massmanatap] * 2;
		%set 	= newObject("set", SimSet);
		%n 		= containerBoxFillSet(%set, $SimPlayerObjectType, GameBase::getPosition(%clientId), %b, %b, %b, 0);
		
		$MassManaTapCounter[%clientId] = 0;
		Group::iterateRecursive(%set, ManaGiverCounter, %clientId, 0, 0);		// Count the number of allies to distribute mana to
		if($MassManaTapCounter[%clientId] > 0){
			%mana = $MassManaTapHolder[%clientId] / $MassManaTapCounter[%clientId];
			Group::iterateRecursive(%set, ManaGiver, %clientId, %mana, 0);		// Give each ally an equal share
		} else {
			refreshMANA(%clientId, $MassManaTapHolder[%clientId] * -1);			// If there are no allies around, give the caster all of the mana
		}
		deleteObject(%set);
		$MassManaTapHolder[%clientId] = 0;
	}
}



//________________________________________________________________________________________________
// smite | advsmite
ExplosionData SmiteShockwave {
   shapeName = "hitter.dts";
   soundId   = debrisLargeExplosion;
   faceCamera = true;
   randomSpin = false;
   hasLight   = true;
   lightRange = 6.0;
   timeZero = 0.250;
   timeOne  = 0.650;
   colors[0]  = { 0.0, 0.0, 0.0  };
   colors[1]  = { 1.0, 0.5, 0.16 };
   colors[2]  = { 1.0, 0.5, 0.16 };
   radFactors = { 0.0, 1.0, 1.0 };
};
MineData SpellFXsmite
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";
	shapeFile = "multibolt";
	shadowDetailMask = 4;
	explosionId = SmiteShockwave;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
function SpellFXsmite::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.1, %this); }



//________________________________________________________________________________________________
// smite
LaserData SmiteLaser
{
	laserBitmapName   = "gold3.bmp";
	hitName           = "hitter.dts";
	damageConversion  = 0.0;
	baseDamageType    = $LaserDamageType;
 	beamTime          = 0.25;
	lightRange        = 20.0;
	lightColor        = { 0.75, 0.75, 0.75 };
	detachFromShooter = false;
	hitSoundId        = debrisLargeExplosion;
};



//________________________________________________________________________________________________
// heal | advheal | massheal | fullheal | massfullheal
ExplosionData HealingBlastFX {
   shapeName = "plasmabolt.dts";
   soundId   = ActivateAR;
   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 6.0;
   timeZero = 0.1;
   timeOne  = 0.4;
   colors[0]  = { 0.0, 0.0, 0.0  };
   colors[1]  = { 1.0, 0.5, 0.16 };
   colors[2]  = { 1.0, 0.5, 0.16 };
   radFactors = { 1.0, 1.0, 1.0 };
};
MineData SpellFXhealing
{
	mass = 0.3;
	drag = 1.0;
	density = 2.0;
	elasticity = 0.15;
	friction = 1.0;
	className = "Handgrenade";
	description = "Handgrenade";
	shapeFile = "plasmabolt";
	shadowDetailMask = 4;
	explosionId = HealingBlastFX;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
function SpellFXhealing::onAdd(%this) { Mine::Detonate(%this); }
