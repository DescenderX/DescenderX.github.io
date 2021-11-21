//________________________________________________________________________________________________
// COMBAT ARTS
//
//	DescX Notes:
//		Spells for fighters! These are used with #cast, but their mana costs are dirt cheap and
//		the benefits they provide are typically difficult for casters to get any value out of.
//		speedX will increase base movement speed (which isnt safe if you're a mage...)
//		atkechoX causes physical attacks to repeat X times (also no good for a mage...)
//		You can shove; #sleep anywhere with rest; force targets to unequip weapons with disarm;
//		buff damage with warcry; or charge to apply a strong impulse and fly forward
//________________________________________________________________________________________________
SpellDefinition($SkillCombatArts, "shove", 20, 1, 
				"Shove", "Shove your target away from you.", 0, 3, 0,
				NoSound, SoundLandOnGround, 2, 0, 2, 0, False, False,
				$TargetSpecial, 0, 0, 0);

SpellDefinition($SkillCombatArts, "speed1", 0, 3, 
				"Movement Enhancement 1", "Accelerates movement by 50% for 10 seconds", 0, 0, 0, 
				SoundActivateMotionSensor, SoundFireSeeking, 10, 0.2, 5, 5, False, False,
				($TargetSpecial | $TargetBuffAlly), "SPD ! 1", 0, 0);

SpellDefinition($SkillCombatArts, "atkecho1", 70, 10, 
				"Attack Echo 1", "An invisible force repeats your physical attacks 1 time for 10 seconds", 0, 0, 0, 
				NoSound, SoundShieldHit, 2, 2, 10, 5, False, False,
				$TargetBuffAlly, "ASU ! 1", 0, 0);		

SpellDefinition($SkillCombatArts, "rest", 21, 1, 
				"Rest", "Sleep anywhere, anytime.", 0, 0, 0, 
				NoSound, NoSound, 2, 5, 5, 0, False, False,
				$TargetSpecial, 0, 0, 0);
				
SpellDefinition($SkillCombatArts, "speed2", 0, 8, 
				"Movement Enhancement 2", "Accelerates movement by 50% for 30 seconds", 0, 0, 0, 
				SoundActivateMotionSensor, SoundFireSeeking, 10, 0.2, 10, 15, False, False,
				($TargetSpecial | $TargetBuffAlly), "SPD ! 1", 0, 0);

SpellDefinition($SkillCombatArts, "atkecho2", 71, 30, 
				"Attack Echo 2", "An invisible force repeats your physical attacks 1 time for 30 seconds", 0, 0, 0, 
				NoSound, SoundShieldHit, 2, 2, 10, 15, False, False,
				$TargetBuffAlly, "ASU ! 1", 0, 0);

SpellDefinition($SkillCombatArts, "disarm", 22, 1, 
				"Disarm", "Attempt to knock the weapon out of the target's hand.", 0, 5, 0, 
				SoundBluntSwingLG, SoundBluntSwingSM, 2, 1, 10, 0, False, False,
				$TargetSpecial, 0, 0, 0);

SpellDefinition($SkillCombatArts, "speed3", 0, 8, 
				"Movement Enhancement 3", "Accelerates movement by 2.5x for 10 seconds", 0, 0, 0, 
				SoundActivateMotionSensor, SoundFireSeeking, 10, 0.2, 5, 5, False, False,
				($TargetSpecial | $TargetBuffAlly), "SPD ! 2", 0, 0);

SpellDefinition($SkillCombatArts, "taunt", 25, 1, 
				"Taunt", "Throws off the target, causing it stagger and lose attack power for a short time.", 0, 80, 0, 
				NoSound, NoSound, 2, 0, 12, 5, False, False,
				($TargetSpecial | $TargetOther | $TargetBuffEnemy), "ATK 2 -30", 0, 0);

SpellDefinition($SkillCombatArts, "atkecho3", 72, 20, 
				"Attack Echo 3", "An invisible force repeats your physical attacks 2 times for 10 seconds", 0, 0, 0, 
				NoSound, SoundShieldHit, 2, 2, 10, 5, False, False,
				$TargetBuffAlly, "ASU ! 2", 0, 0);

SpellDefinition($SkillCombatArts, "charge", 24, 1, 
				"Charge", "Charge forward with great speed, shielding yourself from initial impact. Impact causes damage to anything struck by the shockwave.", 0, 0, 0, 
				ActivateAB, NoSound, 20, 0.1, 5, 0, False, False,
				$TargetSpecial, 0, 0, 0);

SpellDefinition($SkillCombatArts, "speed4", 0, 16, 
				"Movement Enhancement 4", "Accelerates movement by 2.5x for 30 seconds", 0, 0, 0, 
				SoundActivateMotionSensor, SoundFireSeeking, 10, 0.2, 10, 15, False, False,
				($TargetSpecial | $TargetBuffAlly), "SPD ! 2", 0, 0);
				
SpellDefinition($SkillCombatArts, "warcry", 23, 1, 
				"Warcry", "Increase attack damage temporarily.", 0, 0, 8, 
				SoundFlagReturned, SoundFlagPickup, 2, 5, 5, 15, False, False,
				($TargetBuffAlly | $TargetGroup), "ATK 1 50", 0, 0);
				
SpellDefinition($SkillCombatArts, "atkecho4", 73, 40, 
				"Attack Echo 4", "An invisible force repeats your physical attacks 2 times for 20 seconds", 0, 0, 0, 
				NoSound, SoundShieldHit, 2, 2, 10, 10, False, False,
				$TargetBuffAlly, "ASU ! 2", 0, 0);			
				
SpellDefinition($SkillCombatArts, "speed5", 0, 16, 
				"Movement Enhancement 5", "Accelerates movement by 5x for 10 seconds", 0, 0, 0, 
				SoundActivateMotionSensor, SoundFireSeeking, 10, 1.5, 10, 5, False, False,
				($TargetSpecial | $TargetBuffAlly), "SPD ! 4", 0, 0);
				
SpellDefinition($SkillCombatArts, "atkecho5", 74, 50, 
				"Attack Echo 5", "An invisible force repeats your physical attacks 3 times for 15 seconds", 0, 0, 0, 
				NoSound, SoundShieldHit, 2, 5, 10, 5, False, False,
				$TargetBuffAlly, "ASU ! 3", 0, 0);



//________________________________________________________________________________________________
// Hardcoded Combat Arts
//________________________________________________________________________________________________
function CastCombatArts(%player, %clientId, %index, %oldpos, %castPos, %castObj, %w2, %returnFlag, %skipEndCast, %isWordSmith) {
	if(%isWordSmith)	%skillLevel = GetSkillWithBonus(%clientId, $SkillWordsmith);
	else 				%skillLevel = GetSkillWithBonus(%clientId, $SkillCombatArts);
	
	if(isOneOf(%index, $Spell::index[speed1],$Spell::index[speed2],$Spell::index[speed3],$Spell::index[speed4],$Spell::index[speed5])) {
		RefreshWeight(%clientId);
	}
	else if($Spell::index[shove] == %index) {
		if(%castObj > 0 && %castObj != "" && getObjectType(%castObj) == "Player") {
			%id = Player::getClient(%castObj);
			%b = GameBase::getRotation(%clientId);
			%c1 = Cap(20 + %skillLevel / 2, 25, 800);
			%c2 = %c1 / 1.5;
			%mom = Vector::getFromRot( %b, %c1, %c2 );	
			Player::applyImpulse(%id, %mom);
			playSound(SoundLandOnGround, GameBase::getPosition(%id));
			Client::sendMessage(%clientId, $MsgBeige, "You shoved " @ rpg::getName(%id) @ ".");			
			%returnFlag = True;
		} else Client::sendMessage(%clientId, $MsgRed, "You flail wildly and miss!");
	}
	else if($Spell::index[rest] == %index) {		
		if(!IsDead(%clientId)) {
			%clientId.sleepMode = 1;				
			Client::setControlObject(%clientId, Client::getObserverCamera(%clientId));
			Observer::setOrbitObject(%clientId, Client::getOwnedObject(%clientId), 3, 3, 3);
			refreshHPREGEN(%clientId);
			refreshMANAREGEN(%clientId);
			Client::sendMessage(%clientId, $MsgGreen, "You begin to rest. Use #wake to wake up.");
		}
	}
	else if($Spell::index[disarm] == %index) {
		if(%castObj != "" && %castObj > 0 && getObjectType(%castObj) == "Player") {
			%id = Player::getClient(%castObj);
			%weapon = Player::getMountedItem(%id, $WeaponSlot);				
			if (%weapon != -1) {
				%returnFlag = rpg::ResistSkillRoll($Spell::keyword[%index], 
										%clientId, 	%skillLevel, 	1, 0,
										%id, 		$SkillStrength, 	1.2, (fetchData(%id, "LVL") * 8));
				if(!%returnFlag) {
					Client::sendMessage(%clientId, $MsgRed, rpg::getName(%id) @ " keeps a firm grip on their " @ %weapon.description @ ".");
				} else {
					Player::unMountItem(%id,$WeaponSlot);
					Client::sendMessage(%clientId, $MsgRed, rpg::getName(%id) @ " was disarmed!");
					Client::sendMessage(%id, $MsgRed, rpg::getName(%clientId) @ " just disarmed you!");
				}
			} else {
				%returnFlag = False;
				Client::sendMessage(%clientId, $MsgRed, "You cannot literally disarm targets...");
			}
		} else {
			%returnFlag = False;
			Client::sendMessage(%clientId, $MsgRed, "You flail around and grab on to... nothing.");
		}
	}
	else if($Spell::index[charge] == %index) {
		if(fetchData(%clientId, "Charging") < 1) {
			storeData(%clientId, "Charging", 1);
			%multiplier = Cap(%skillLevel / 200, 1, 3);
			%vel = Vector::getFromRot(GameBase::getRotation(%clientId), 150 * %multiplier, 1);
			%vel = vecx(%vel) @ " " @ vecy(%vel) @ " 35";
			Client::setControlObject(%clientId, -1);
			player::applyimpulse(%clientId, %vel);
			%pos = GameBase::getPosition(%clientId);
			CreateAndDetBomb(%clientId, DustPlumeFX, NewVector(vecx(%pos),vecy(%pos),vecz(%pos)+1), false);
			playSound(SoundFireFlierRocket, GameBase::getPosition(%clientId));
			schedule("Client::setControlObject(" @ %clientId @ "," @ %clientId @ " );", 0.5, %clientId);
			schedule("storeData(" @%clientId @ ", \"Charging\", \"\");", 3, %clientId);
		}		
	}
	else if($Spell::index[taunt] == %index) {		
		%sound = RandomRaceSound(fetchData(%clientId, "RACE"), Taunt);
		PlaySound(%sound, GameBase::getPosition(%clientId));
		
		if(%castObj != "" && getObjectType(%castObj) == "Player") {
			%id 		= Player::getClient(%castObj);		
			%returnFlag = rpg::ResistSkillRoll($Spell::keyword[%index], 
									%clientId, 	%skillLevel, 	1, 0,
									%id, 		$SkillWordsmith, 	0.8, (fetchData(%id, "LVL") * 8));
			if(!%returnFlag) {
				Client::sendMessage(%clientId, $MsgRed, rpg::getName(%id) @ " laughs at your taunt.");
				Client::sendMessage(%id, $MsgGreen, "You laugh off " @ rpg::getName(%clientId) @ "'s taunt.");
				//PlaySound(RandomRaceSound(fetchData(%id, "RACE"), Taunt), GameBase::getPosition(%id));
			} else {
				%multiplier = Cap(%skillLevel / 250, 0.5, 2);	
				
				if(Player::isAiControlled(%id)) {													// TIME 0: Freeze the bot 
					storeData(%id, "frozen", True);				
					%name = fetchData(%id, "BotInfoAiName");
					AI::setVar(%name, SpotDist, 0);
					AI::newDirectiveRemove(%name, 99);
					schedule("storeData(" @ %id @ ", \"frozen\", \"\");", 1 * %multiplier);
					schedule("AI::newDirectiveFollow(\"" @ %name @ "\", " @ %id @ ", 0, 99);", 1.5 * %multiplier);
					schedule("AI::SetSpotDist(" @ %id @ ");", 4 * %multiplier);
				} else {
					%max = 4 * %multiplier;
					if(Player::isAiControlled(%clientId))
						%max = 1;
					
					for(%x=1;%x<=%max ;%x++) {
						schedule("Client::setControlObject(" @ %id @ ", -1);", %x);
						schedule("Client::setControlObject(" @ %id @ "," @ %id @ " );", %x + (0.5 * %multiplier));
					}
				}				
				player::setanimation(%clientId,43);
				%returnFlag = True;
				Client::sendMessage(%clientId, $MsgBeige, "You taunt " @ rpg::getName(%id) @ ".");
			}
		} else {
			%returnFlag = False;
			Client::sendMessage(%clientId, $MsgRed, "You taunt the empty space, looking like a fool in the process.");
		}
	}
	
	if(!%skipEndCast) return EndCast(%clientid,%index,%castpos,%returnflag);
}


ExplosionData LargeDustBoom
{
   shapeName = "shockwave_large.dts";
   soundId   = shockExplosion;
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
MineData LargeDustPlumeFX
{
	mass = 0.3;	drag = 1.0;	density = 2.0;	elasticity = 0.15;	friction = 1.0;
	className = "Handgrenade";	description = "Handgrenade";	shapeFile = "dustplume";	shadowDetailMask = 4;
	explosionId = LargeDustBoom;	explosionRadius = 5.0;	damageValue = 1.0;	damageType = $NullDamageType;	kickBackStrength = 0;
	triggerRadius = 0.5;	maxDamage = 1.0;
};
function LargeDustPlumeFX::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }
