//________________________________________________________________________________________________
// DISTORTION
//
//	DescX Notes:
//		Teleport. Blink. Glide. Fly. Thorns. Dimensionrift.
//		I had to stop stacking "cool stuff" in this spell tree and spread it out more ;)
//________________________________________________________________________________________________

SpellDefinition($SkillDistortionMagic, "blink", 44, 4, 
				"Blink", "The caster teleports a short distance forward", 0, 0, 0, 
				UnravelAM, UnravelAM, 20, 0, 5, 0, False, False,
				$TargetSpecial, 0, 0, 0);

SpellDefinition($SkillDistortionMagic, "teleport", 0, 5, 
				"Teleport", "Teleport the caster close to nearest zone", 0, 0, 0, 
				SoundElevatorStart, ActivateCH, 2, 3.5, 10, 0, False, True,
				$TargetSpecial, 0, 0, 0);			
	
SpellDefinition($SkillDistortionMagic, "rift", 50, 35, 
				"Rift", "Casts rift.", 35, 80, 10, 
				SoundDoorClose, debrisMediumExplosion, 2, 2, 15, 0, False, False,
				$TargetSpecial, 0, 0, 0);

SpellDefinition($SkillDistortionMagic, "thorns", 15, 10, 
				"Thorns", "A portion of melee damage the caster receives is returned to attackers for 60 seconds", 0, 0, 0, 
				SoundUseInventoryStation, SoundUseInventoryStation, 2, 2, 30, 30, False, False,
				$TargetBuffAlly, "THORN 1 10", 0, 0);

SpellDefinition($SkillDistortionMagic, "transport", 0, 12, 
				"Transport", "Transports to a specific zone", 0, 0, 0, 
				Portal11, ActivateCH, 2, 5, 20, 0, False, False,
				$TargetSpecial, 0, 0, 0);

SpellDefinition($SkillDistortionMagic, "advblink", 13, 1, 
				"Advanced Blink", "Launches a Blink orb which sticks to a surface for 30 seconds. Cast again to teleport to the orb.", 0, 0, 0, 
				UnravelAM, UnravelAM, 20, 0, 2, 0, False, False,
				$TargetSpecial, 0, 0, 0);	

SpellDefinition($SkillDistortionMagic, "glide", 0, 8, 
				"Glide", "Allows the caster to glide and avoid fall damage for 20 seconds.", 0, 0, 0, 
				ActivateCH, SoundFloatMineTarget, 2, 1, 2, 10, False, False,
				($TargetSpecial | $TargetBuffAlly), "FLIGHT ! 2", 0, 0);
				
SpellDefinition($SkillDistortionMagic, "advteleport", 0, 10, 
				"Teleport Self or Other", "Teleport the caster or target close to nearest zone", 0, 80, 0, 
				SoundElevatorStart, ActivateCH, 2, 3.5, 20, 0, True, True,
				$TargetSpecial, 0, 0, 0);

SpellDefinition($SkillDistortionMagic, "teleswap", 30, 8, 
				"Teleswap", "The caster opens a wormhole and swaps places with their target.", 0, 80, 0, 
				AbsorbABS, AbsorbABS, 30, 1, 5, 0, False, False,
				($TargetOther | $TargetSpecial), 0, 0, 0);

SpellDefinition($SkillDistortionMagic, "advglide", 0, 20, 
				"Advanced Glide", "Allows the caster to glide and avoid fall damage for 100 seconds.", 0, 0, 0, 
				ActivateCH, SoundFloatMineTarget, 2, 1, 10, 50, False, False,
				($TargetSpecial | $TargetBuffAlly), "FLIGHT ! 2", 0, 0);

SpellDefinition($SkillDistortionMagic, "massteleport", 0, 15, 
				"Mass Teleport", "Teleport all nearby allies to the nearest zone", 0, 0, 10, 
				SoundElevatorStart, ActivateCH, 2, 3.5, 30, 0, False, True,
				($TargetGroup | $TargetSpecial), 0, 0, 0);				
				
SpellDefinition($SkillDistortionMagic, "advtransport", 0, 25, 
				"Advanced Transport", "Transports self OR person in line-of-sight to a specific zone", 0, 80, 0, 
				Portal11, ActivateCH, 2, 5, 30, 0, True, False,
				$TargetSpecial, 0, 0, 0);

SpellDefinition($SkillDistortionMagic, "advthorns", 50, 10, 
				"Advanced Thorns", "Protects the caster or a target with heavy thorns for 25 seconds.", 0, 80, 0, 
				SoundUseInventoryStation, SoundUseInventoryStation, 2, 2, 12.5, 12.5, False, False,
				($TargetOther | $TargetBuffAlly), "THORN 1 20", 0, 0);

SpellDefinition($SkillDistortionMagic, "flight", 0, 8, 
				"Flight", "The caster gains flight for a brief 5 second period.", 0, 0, 0, 
				SoundFlyerDismount, ActivateCH, 2, 2.0, 15, 5, False, False,
				($TargetSpecial | $TargetBuffAlly), "FLIGHT ! 3", 0, 0);

SpellDefinition($SkillDistortionMagic, "dimensionrift", 90, 300, 
				"Dimension Rift", "Casts dimensionrift.", 320, 80, 30, 
				SoundThunder1, debrisLargeExplosion, 2, 5, 20, 0, False, False,
				$TargetSpecial, 0, 0, 0);

SpellDefinition($SkillDistortionMagic, "advteleswap", 60, 15, 
				"Advanced Teleswap", "The caster swaps places with their target and deals rift damage twice.", 50, 200, 5, 
				AbsorbABS, AbsorbABS, 15, 1, 20, 0, False, False,
				($TargetOther | $TargetSpecial), 0, 0, 0);
				
SpellDefinition($SkillDistortionMagic, "masstransport", 0, 50, 
				"Mass Transport", "Transports self and all friendlies within a 6 meter radius to a specific zone.", 0, 0, 10, 
				Portal11, ActivateCH, 2, 5, 5, 0, False, False,
				($TargetGroup | $TargetSpecial), 0, 0, 0);						

SpellDefinition($SkillDistortionMagic, "advflight", 0, 8, 
				"Advanced Flight", "Allows the caster to fly for 45 seconds.", 0, 0, 0, 
				SoundFlyerDismount, ActivateCH, 2, 2.0, 15, 25, False, False,
				($TargetSpecial | $TargetBuffAlly), "FLIGHT ! 3", 0, 0);

SpellDefinition($SkillDistortionMagic, "remort", 0, 1, 
				"Remort", "After reaching maximum level, cast remort to restart at level 1 with higher maximum statistics and faster skill gains.", 0, 0, 0, 
				RespawnA, RespawnC, 1, 5, 5, 0, False, False, 
				$TargetSpecial, 0, 0, 0);


//________________________________________________________________________________________________
// Hardcoded Distortion magic effects
//________________________________________________________________________________________________
function CastDistortionMagic(%player, %clientId, %index, %oldpos, %castPos, %castObj, %w2, %returnFlag, %skipEndCast, %isWordSmith) {
	if(%isWordSmith)	%skillLevel = GetSkillWithBonus(%clientId, $SkillWordsmith);
	else 				%skillLevel = GetSkillWithBonus(%clientId, $SkillDistortionMagic);
	
	// Teleport to rough area
	if(!rpg::IsTheWorldEnding() && isOneOf(%index,$Spell::index[teleport],$Spell::index[advteleport],$Spell::index[massteleport])) {
		%zoneId = GetNearestZone(%clientId, %w2, 3, True);
		if(%zoneId != False) {
			if($Spell::index[transport] == %index) {
				%id = %clientId;
			} else {
				if(getObjectType(%castObj) == "Player")
					%id = Player::getClient(%castObj);
				else %id = %clientId;
			}
			
			Client::sendMessage(%clientId, $MsgBeige, "Teleporting near " @ Zone::getDesc(%zoneId));
			if(%clientId != %id)
				Client::sendMessage(%id, $MsgBeige, "You are being teleported near " @ Zone::getDesc(%zoneId));

			%system = Object::getName(%zoneId);
			%type = GetWord(%system, 0);
			%desc = String::getSubStr(%system, String::len(%type)+1, 9999);

			%castPos = TeleportToMarker(%id, "Zones\\" @ %system @ "\\DropPoints", False, True);
			CheckAndBootFromArena(%id);

			if(!fetchData(%id, "invisible"))
				GameBase::startFadeIn(%id);

			Player::setDamageFlash(%id, 0.7);
			%extraDelay = 0.22;	//sometimes the endSound doesn't get played unless there is sufficient delay

			%returnFlag = True;
		} else {
			Client::sendMessage(%clientId, $MsgBeige, "Teleportation failed.");
			%returnFlag = False;
		}
	}
	// Transport to zone
	else if(!rpg::IsTheWorldEnding() && isOneOf(%index,$Spell::index[advtransport],$Spell::index[masstransport],$Spell::index[transport])) {
		%zoneId = GetZoneByKeywords(%clientId, %w2, 3);

		if(%zoneId != False) {
			if($Spell::index[transport] == %index) {
				%id = %clientId;
			} else {
				if(getObjectType(%castObj) == "Player")
					%id = Player::getClient(%castObj);
				else %id = %clientId;
			}

			%system = Object::getName(%zoneId);
			%type = GetWord(%system, 0);
			%desc = String::getSubStr(%system, String::len(%type)+1, 9999);

			%castPos = TeleportToMarker(%id, "Zones\\" @ %system @ "\\DropPoints", False, True);
			if(!%castPos) {
				Client::sendMessage(%clientId, $MsgRed, "A strange field prevents transportation to " @ Zone::getDesc(%zoneId));
				%returnFlag = false;
			} else {
				Client::sendMessage(%clientId, $MsgBeige, "Transporting to " @ Zone::getDesc(%zoneId));
				if(%clientId != %id)
					Client::sendMessage(%id, $MsgBeige, "You are being transported to " @ Zone::getDesc(%zoneId));
				CheckAndBootFromArena(%id);

				if(!fetchData(%id, "invisible"))
					GameBase::startFadeIn(%id);

				Player::setDamageFlash(%id, 0.7);
				%extraDelay = 0.22;	//sometimes the endSound doesn't get played unless there is sufficient delay

				%returnFlag = True;
			}
		} else {
			Client::sendMessage(%clientId, $MsgBeige, "Transportation failed.");
			%returnFlag = False;
		}
	}
		
	else if(isOneOf(%index,$Spell::index[rift],$Spell::index[dimensionrift])) {
		if(%castPos != "") {
			%mult = 1.5;
			if(%index == $Spell::index[rift])
				%mult = 0.5;
			%minrad = 0;
			%maxrad = 4;
			for(%i = 0; %i <= 10 * %mult; %i++) {
				%tempPos = Vector::Random(5);
				%newPos = NewVector(GetWord(%tempPos, 0) + GetWord(%castPos, 0), GetWord(%tempPos, 1) + GetWord(%castPos, 1), GetWord(%castPos, 2) + (%i / 4));
				schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXdimensionrift3\", \"" @ %newPos @ "\", True, " @ %index @ "," @ %isWordSmith @ ");", %i / 2, %player);
			}
			if(%mult < 1.0)
				schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXdimensionrift2\", \"" @ %castPos @ "\", True, " @ %index @ "," @ %isWordSmith @ ");", 1.0, %player);
			else {
				schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXdimensionrift2\", \"" @ %castPos @ "\", True, " @ %index @ "," @ %isWordSmith @ ");", 3, %player);
				schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXdimensionrift3\", \"" @ %castPos @ "\", True, " @ %index @ "," @ %isWordSmith @ ");", 2, %player);			
				schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXdimensionrift4\", \"" @ %castPos @ "\", True, " @ %index @ "," @ %isWordSmith @ ");", 1, %player);
			}
			%returnFlag = True;
		} else {
			Client::sendMessage(%clientId, $MsgBeige, "Could not find a target.");
			%returnFlag = False;
		}
	}
	else if (%index == $Spell::index[blink]) {
		
		%c1 = Cap(%skillLevel / 10, 25, 150);		
		%zoffset = 0;
		%bomb = newObject("", "Mine", "BlinkOrb");
		%bomb.owner = %clientId;
		addToSet("MissionCleanup", %bomb);
		%rot = GameBase::getRotation(%clientId);
		%newrot = (GetWord(%rot, 0) - %zoffset) @ " " @ GetWord(%rot, 1) @ " " @ GetWord(%rot, 2);
		GameBase::setRotation(%clientId, %newrot);
		GameBase::Throw(%bomb, %player, %c1, false);
		GameBase::setRotation(%bomb, %rot);
		GameBase::setRotation(%clientId, %rot);
		schedule("GameBase::setPosition(" @ %clientId @ ", GameBase::getPosition(" @ %bomb @ "));" @ "Mine::Detonate(" @ %bomb @ ");", 0.66, %player);		
		playSound($Spell::endSound[%index], %castPos);
		%returnFlag = True;
	}
	else if (%index == $Spell::index[advblink]) {
		if($ActiveAdvBlinkOrb[%clientId] != "") {
			GameBase::setPosition(%clientId, GameBase::getPosition($ActiveAdvBlinkOrb[%clientId]));
			Mine::Detonate($ActiveAdvBlinkOrb[%clientId]);
			playSound($Spell::endSound[%index], %castPos);
			$ActiveAdvBlinkOrb[%clientId] = "";
			%returnFlag = True;
		} else {
			%c1 = Cap(%skillLevel / 5, 50, 500);		
			%zoffset = 0;
			%bomb = newObject("", "Mine", "BlinkOrb2");
			%bomb.owner = %clientId;
			schedule("$ActiveAdvBlinkOrb[" @ %bomb.owner @ "] = \"\"; Mine::Detonate(" @ %bomb @ ");", 30, %bomb); 
			$ActiveAdvBlinkOrb[%clientId] = %bomb;
			addToSet("MissionCleanup", %bomb);
			%rot = GameBase::getRotation(%clientId);
			%newrot = (GetWord(%rot, 0) - %zoffset) @ " " @ GetWord(%rot, 1) @ " " @ GetWord(%rot, 2);
			GameBase::setRotation(%clientId, %newrot);
			GameBase::Throw(%bomb, %player, %c1, false);
			GameBase::setRotation(%bomb, %rot);
			GameBase::setRotation(%clientId, %rot);
			%returnFlag = False;
		}
	}
	else if(isOneOf(%index,$Spell::index[flight],$Spell::index[advflight],$Spell::index[glide],$Spell::index[advglide])) {
		RefreshWeight(%clientId);
		%returnFlag = True;
	}
	else if(isOneOf(%index,$Spell::index[teleswap],$Spell::index[advteleswap])) {
		if(%castObj > 0 && %castObj != "" && getObjectType(%castObj) == "Player") {
			%id = Player::getClient(%castObj);	
			%clientSwapSpot = GameBase::getPosition(%clientId);
			if(%index == $Spell::index[advteleswap]) {
				schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXdimensionrift1\", \"" @ %clientSwapSpot @ "\", True, " @ %index @ "," @ %isWordSmith @ ");", 0.5, %player);
			}
			schedule("GameBase::setPosition(" @ %clientId @ ", GameBase::getPosition(" @ %id @ "));", 0.25, %player);
			schedule("GameBase::setPosition(" @ %id @ ", \"" @ %clientSwapSpot @ "\");", 0.33, %player);
		} else {
			if(%index == $Spell::index[advteleswap]) {
				CreateAndDetBomb(%clientId, SpellFXdimensionrift1, %clientSwapSpot, True, %index, %isWordSmith);
			}
			Client::sendMessage(%clientId, $MsgRed, "Your teleswap attempt failed! No target was found.");
			%returnFlag = False;
		}
	} 
	else if(!rpg::IsTheWorldEnding() && $Spell::index[remort] == %index) {
		%remortLevelDistance = rpg::AllowRemort(%clientId);
		if(%remortLevelDistance >= 0) {
			%castPos = DoRemort(%clientId);	
			%extraDelay = 0.22;
			%returnFlag = True;
			Client::sendMessage(%clientId, $MsgGreen, "Your skin begins to burn away as the Hall of Souls prepares your new body...");
		} else {
			Client::sendMessage(%clientId, $MsgRed, "You must gain " @ -%remortLevelDistance @ " levels to remort." );
			%returnFlag = False;
		}
	} 
	
	if(!%skipEndCast) return EndCast(%clientid,%index,%castpos,%returnflag);
}



//________________________________________________________________________________________________
function casting::teleport(%clientId, %index, %oldpos, %castObj, %w2) {
	%zoneId = GetNearestZone(%clientId, %w2, 3, True);
	if(%zoneId != False) {
		Schedule::Add("DoCastSpell(" @ %clientId @ ", " @ %index @ ", \"" @ %oldpos @ "\", \"" @ %castObj @ "\", \"" @ %w2 @ "\");", $Spell::delay[%index], "spell"@%clientId);
	} else {
		disableOverrides(%clientId);
		%clientId.overrideKeybinds = 2;
		%clientId.keyOverride = "casting::teleportmenu_input";
		%clientId.tpmenutype = "";
		%clientId.castingmenuindex = %index;
		casting::teleportmenu(%clientid);
	}
}
function casting::advteleport(%clientId, %index, %oldpos, %castObj, %w2) {
	casting::teleport(%clientId, %index, %oldpos, %castObj, %w2);
}
function casting::massteleport(%clientId, %index, %oldpos, %castObj, %w2) {
	casting::teleport(%clientId, %index, %oldpos, %castObj, %w2);
}



//________________________________________________________________________________________________
// Teleport casting menu. Probably going to delete this junk.
function casting::teleportmenu(%clientId) {
	%msg = "<jc>Teleport to nearest:";
	%msg = %msg @ "\n\n<f2>1:<f0> Town\n<f2>2:<f0> Dungeon";
	%msg = %msg @ "\n\n<f1>0:<f0> Cancel";
	rpg::longPrint(%clientId,%msg,1,0.6);
	schedule::add("casting::teleportmenu("@%clientId@");",0.3,"transportmenu"@%clientId, %clientId);//should override other transport menu so you don't get both
}
function casting::teleportmenu_input(%clientId, %key) {
	%index = floor(%clientId.castingmenuindex);
	if(%key == 0){
		disableOverrides(%clientId);
		return;
	} else {
		if(%key == 1){
			%clientId.tpmenutype = "town";
			%chosen = True;
		}
		else if(%key == 2){
			%clientId.tpmenutype = "dungeon";
			%chosen = True;
		}
		if(%chosen){
			Schedule::Add("DoCastSpell(" @ %clientId @ ", " @ %index @ ", \"" @ GameBase::getPosition(%clientId) @ "\", 0, \"" @ %clientId.tpmenutype @ "\");", $Spell::delay[%index], "spell"@%clientId);
			%clientId.overrideKeybinds = 1;
			disableOverrides(%clientId);
			%chosen = True;
		}
	}
	if(!%chosen)
		client::sendmessage(%clientId, $msgRed,"That isn't on the menu.");
}



//________________________________________________________________________________________________
// blink
ExplosionData BlinkOrbBoom {
	faceCamera = true; randomSpin = true; shapeName = "tumult_medium.dts"; soundId = NoSound; timeZero = 0.0; timeOne  = 1.0; hasLight  = false;  lightRange = 0;
	colors[0]  = { 0.0, 0.0, 0.0  };	colors[1]  = { 1.0, 0.5, 0.16 };	colors[2]  = { 1.0, 0.5, 0.16 };	radFactors = { 0.5, 0.5, 0.25 };	
};
MineData BlinkOrb {
	className = "Mine"; description = "Casting Blade bomb"; shadowDetailMask = 4; damageValue = 0.0; maxDamage = 1.0;
	kickBackStrength = 0.2; triggerRadius = 0; damageType = $NullDamageType; 
	mass = 2.0; drag = 1.0; density = 1.0; elasticity = 0.0; friction = 1.0; 
	explosionId = BlinkOrbBoom; explosionRadius = 4; shapeFile = "goldorb";
}; 
function BlinkOrb::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 10, %this); }



//________________________________________________________________________________________________
// advblink
MineData BlinkOrb2 {
	className = "Mine"; description = "Casting Blade bomb"; shadowDetailMask = 4; damageValue = 0.0; maxDamage = 1.0;
	kickBackStrength = 0.2; triggerRadius = 0; damageType = $NullDamageType; 
	mass = 5.0; drag = 1.0; density = 2.0; elasticity = 0.0; friction = 2.0; 
	explosionId = BlinkOrbBoom; explosionRadius = 4; shapeFile = "goldorb";
};



//________________________________________________________________________________________________
// rift | dimensionrift		
ExplosionData DimensionRiftExplosion1 { 
	shapeName = "shotgunex.dts"; soundId   = NoSound; 
	faceCamera = true; randomSpin = true; hasLight   = true; 
	lightRange = 3.0; timeZero = 0.450; timeOne  = 0.750; 
	colors[0]  = { 1.0, 0.25, 0.25 }; colors[1]  = { 1.0, 0.25, 0.25 }; colors[2]  = { 1.0, 0.25, 0.25 }; 
	radFactors = { 1.0, 1.0, 1.0 }; shiftPosition = True;
};
ExplosionData DimensionRiftExplosion2 { 
	shapeName = "shockwave_large.dts"; soundId   = NoSound; 
	faceCamera = false; randomSpin = false; hasLight   = true; 
	lightRange = 10.0; timeZero = 0.100; timeOne  = 0.300;
	colors[0]  = { 1.0, 1.0, 1.0 }; colors[1]  = { 1.0, 1.0, 1.0 }; colors[2]  = { 0.0, 0.0, 0.0 }; 
	radFactors = { 1.0, 0.5, 0.0 };
};
ExplosionData DimensionRiftExplosion3 { 
	shapeName = "bluex.dts"; soundId   = NoSound; 
	faceCamera = true; randomSpin = true; hasLight   = true;
	lightRange = 8.0; timeScale = 1.5; timeZero = 0.250; timeOne  = 0.850;
	colors[0]  = { 0.4, 0.4,  1.0 }; colors[1]  = { 1.0, 1.0,  1.0 }; colors[2]  = { 1.0, 0.95, 1.0 }; 
	radFactors = { 0.5, 1.0, 1.0 };
};
ExplosionData DimensionRiftExplosion4 { 
	shapeName = "tumult_large.dts"; soundId   = debrisLargeExplosion; 
	faceCamera = true; randomSpin = true; hasLight   = true; 
	lightRange = 5.0; timeZero = 0.250; timeOne  = 0.650; 
	colors[0]  = { 0.0, 0.0, 0.0  }; colors[1]  = { 1.0, 0.5, 0.16 }; colors[2]  = { 1.0, 0.5, 0.16 }; 
	radFactors = { 0.0, 1.0, 1.0 };
};

MineData SpellFXdimensionrift1 { 
	mass = 0.3; drag = 1.0; density = 2.0; elasticity = 0.15; friction = 1.0; className = "Handgrenade"; description = "Handgrenade"; 
	shapeFile = "smoke"; shadowDetailMask = 4; explosionId = DimensionRiftExplosion1; explosionRadius = 10.0; damageValue = 1.0; damageType = $NullDamageType; 
	kickBackStrength = 0; triggerRadius = 0.5; maxDamage = 1.0; 
};
MineData SpellFXdimensionrift2 { 
	mass = 0.3; drag = 1.0; density = 2.0; elasticity = 0.15; friction = 1.0; className = "Handgrenade"; description = "Handgrenade"; 
	shapeFile = "smoke"; shadowDetailMask = 4; explosionId = DimensionRiftExplosion2; explosionRadius = 10.0; damageValue = 1.0; damageType = $NullDamageType; 
	kickBackStrength = 0; triggerRadius = 0.5; maxDamage = 1.0;
};
MineData SpellFXdimensionrift3 { 
	mass = 0.3; drag = 1.0; density = 2.0; elasticity = 0.15; friction = 1.0; className = "Handgrenade"; description = "Handgrenade"; 
	shapeFile = "smoke"; shadowDetailMask = 4; explosionId = DimensionRiftExplosion3; explosionRadius = 10.0; damageValue = 1.0; damageType = $NullDamageType; 
	kickBackStrength = 0; triggerRadius = 0.5; maxDamage = 1.0;
};
MineData SpellFXdimensionrift4 { 
	mass = 0.3; drag = 1.0; density = 2.0; elasticity = 0.15; friction = 1.0; className = "Handgrenade"; description = "Handgrenade"; 
	shapeFile = "smoke"; shadowDetailMask = 4; explosionId = DimensionRiftExplosion4; explosionRadius = 10.0; damageValue = 1.0; damageType = $NullDamageType; 
	kickBackStrength = 0; triggerRadius = 0.5; maxDamage = 1.0;
};

function SpellFXdimensionrift1::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }
function SpellFXdimensionrift2::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }
function SpellFXdimensionrift3::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }
function SpellFXdimensionrift4::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }

