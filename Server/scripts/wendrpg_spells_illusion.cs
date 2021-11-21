//________________________________________________________________________________________________
// ILLUSION
//
//	DescX Notes:
//		Spawn Maiden helpers! Look at them, press T to control them.
//		Gains the old Cloud spell, which now casts on the player and buffs Evasion
//		Burden gives weight to players / forces the AI to stop moving
//		Aqualung shows up early to let Druids play around in Oasis
//		Illuminate blows up a harmless spell to create light, making the Orb Of Luminance still useful
//		Fear forces the target to jump around and spaz out
//		Reflect returns RANGED physical damage; barrier is WYSIWYG
//________________________________________________________________________________________________
SpellDefinition($SkillIllusionMagic, "burden", 15, 3, 
				"Burden", "Attaches a heavy force to the target, slowing them down.", 0, 80, 0, 
				ActivateDE, SoundCapturedTower, 25, 2, 6, 6, False, False,
				($TargetOther | $TargetBuffEnemy | $TargetSpecial), "WEIGHT 100", 0, 0);

SpellDefinition($SkillIllusionMagic, "aqualung", 55, 8, 
				"Aqualung", "Grants the caster the ability to breath under water.", 0, 0, 0, 
				SoundFishWalk, NoSound, 2, 2.0, 1.0, 20, False, False,
				$TargetBuffAlly, "BREATH ! 1", 0, 0);

SpellDefinition($SkillIllusionMagic, "barrier", 10, 75, 
				"Barrier", "Spawns an immovable flat barrier wall that disappears after a short time.", 0, 80, 0, 
				ActivateBF, SoundCapturedTower, 2, 2.0, 2.0, 0, False, False,
				$TargetSpecial, 0, 0, 0);

SpellDefinition($SkillIllusionMagic, "mirage", 0, 8, 
				"Mirage", "Duplicates the caster. The mirage deals no damage and expires quickly.", 0, 120, 0, 
				SoundMedic4Spell, SoundMedic4Spell, 2, 2.0, 1.0, 0, False, False,
				($TargetOther | $TargetBuffEnemy | $TargetSpecial), 0, 0, 0);

SpellDefinition($SkillIllusionMagic, "illuminate", 35, 8, 
				"Illuminate", "Illuminates the area around the caster with bursts of light.", 0, 0, 0, 
				RespawnC, NoSound, 30, 2.5, 1.0, 0, False, False,
				$TargetSpecial, 0, 0, 0);

SpellDefinition($SkillIllusionMagic, "cloud", 20, 10, 
				"Cloud Attack", "Casts an explosive and raises evasion.", 25, 0, 10, 
				ActivateBF, SoundSmokerAttack, 2, 0, 10, 3, False, False,
				($TargetDamageSpell | $TargetBuffAlly), $SkillDesc[$SkillEvasion] @ " 1 300", 1, "SpellFXcloud");				

SpellDefinition($SkillIllusionMagic, "fear", 25, 8, 
				"Fear", "Causes the target to jump around with losses to attack and magic defense.", 0, 80, 0, 
				Portal6, NoSound, 2, 1.0, 8, 3, False, False,
				($TargetOther | $TargetSpecial | $TargetBuffEnemy), "ATK 3 -10 MDEF 3 -50", 0, 0);

SpellDefinition($SkillIllusionMagic, "reflect", 40, 8, 
				"Reflect", "Reflects a portion of ranged, wand or spell damage back at the attacker.", 0, 0, 0, 
				SoundElevatorBlocked, SoundCanSmith, 2, 1.5, 20, 60, False, False,
				$TargetBuffAlly, "DEFLECT 1 15", 0, 0);

SpellDefinition($SkillIllusionMagic, "ironmaiden", 0, 8, 
				"Iron Maiden", "Spawns an Iron Maiden that reflects damage.", 0, 120, 0, 
				LoopLG, ExplodeLM, 2, 5.0, 10.0, 0, False, False,
				$TargetSpecial, 0, 0, 0);

SpellDefinition($SkillIllusionMagic, "advbarrier", 11, 125, 
				"Advanced Barrier", "Spawns an impenetrable, concave force field over the target.", 0, 80, 0, 
				ActivateBF, SoundCapturedTower, 2, 2.0, 3.0, 0, False, False,
				$TargetSpecial, 0, 0, 0);				

SpellDefinition($SkillIllusionMagic, "massfear", 0, 8, 
				"Mass Fear", "Causes all enemies around the caster to experience fear.", 0, 80, 10, 
				Portal6, NoSound, 2, 1.0, 8, 3, False, False,
				($TargetSpecial | $TargetBuffEnemy | $TargetGroup), "ATK -15 MDEF -100", 0, 0);

SpellDefinition($SkillIllusionMagic, "crystalmaiden", 0, 8, 
				"Crystal Maiden", "Spawns a Crystal Maiden with strong attacks.", 0, 120, 0, 
				LoopLT, SoundGlassBreak, 2, 3.0, 10.0, 0, False, False,
				$TargetSpecial, 0, 0, 0);

SpellDefinition($SkillIllusionMagic, "advcloud", 50, 10, 
				"Power Cloud Attack", "Casts three explosives and raises evasion.", 25, 0, 10, 
				ActivateBF, SoundSmokerAttack, 50, 2, 15, 7, False, False,
				($TargetDamageSpell | $TargetBuffAlly), $SkillDesc[$SkillEvasion] @ " 1 800", 3.033, "SpellFXcloud");

SpellDefinition($SkillIllusionMagic, "advreflect", 70, 8, 
				"Advanced Reflect", "Grants additional ranged damage reflection to one ally or the caster.", 0, 80, 0, 
				SoundElevatorBlocked, SoundCanSmith, 2, 1.5, 20, 60, False, False,
				($TargetOther | $TargetBuffAlly), "REFLECT 2 15", 0, 0);

SpellDefinition($SkillIllusionMagic, "massreflect", 100, 8, 
				"Mass Reflect", "All allies around the caster will reflect a portion of ranged damage for a short time.", 0, 0, 10, 
				SoundElevatorBlocked, SoundCanSmith, 2, 1.5, 20, 60, False, False,
				($TargetOther | $TargetGroup | $TargetBuffAlly), "REFLECT 3 15", 0, 0);
				
SpellDefinition($SkillIllusionMagic, "mimic", 0, 80, 
				"Mimic", "A very dangerous spell that transforms the caster into the creature in his/her LOS.", 0, 80, 0, 
				LoopSP, AbsorbABS, 2, 4, 60, 0, False, False,
				$TargetSpecial, 0, 0, 0);

//________________________________________________________________________________________________
// Hardcoded Illusion magic effects
//________________________________________________________________________________________________
function CastIllusionMagic(%player, %clientId, %index, %oldpos, %castPos, %castObj, %w2, %returnFlag, %skipEndCast, %isWordSmith) {
	if(%isWordSmith)	%skillLevel = GetSkillWithBonus(%clientId, $SkillWordsmith);
	else 				%skillLevel = GetSkillWithBonus(%clientId, $SkillIllusionMagic);
	
	if($Spell::index[mimic] == %index) {
		if(Zone::getType(fetchData(%clientId, "zone")) == "PROTECTED") {
			Client::sendMessage(%clientId, $MsgRed, "You can't cast mimic in protected territory.");
			%overrideEndsound = True;
			%returnFlag = False;
		} else {
			%id = Player::getClient(%castObj);
			if(getObjectType(%castObj) == "Player")
			{
				%skilltype = $SkillType[$Spell::keyword[%index]];
				%troll = fetchData(%id, "LVL") + floor(getRandom() * (GetSkillWithBonus(%id, %skilltype) + (GetSkillWithBonus(%id, $SkillWillpower) * (1/2)) ));
				%yroll = fetchData(%clientId, "LVL") + floor(getRandom() * %skillLevel);

				if(%yroll > %troll) {
					storeData(%clientId, "RACE", fetchData(%id, "RACE"));
					storeData(%clientId, "isMimic", True);
				
					UpdateTeam(%clientId);
					RefreshAll(%clientId);
				
					%castPos = GameBase::getPosition(%clientId);
					%returnFlag = True;
				} else {
					Client::sendMessage(%clientId, $MsgBeige, "Mimic failed.");
					%overrideEndsound = True;
					%returnFlag = False;
				}
			} else {
				Client::sendMessage(%clientId, $MsgBeige, "Could not find a target.");
				%overrideEndsound = True;
				%returnFlag = False;
			}
		}
	}
	else if($Spell::index[mirage] == %index) {
		if(getObjectType(%castObj) == "Player") {
			%id = Player::getClient(%castObj);
		} else %id = %clientId;
		
		if(%id == %clientId) 	%selfProjection = True;
		else 					%selfProjection = False;
		
		if(%castPos != "")	%pos = %castPos;
		else 				%pos = Vector::getFromRot(GameBase::getPosition(%id), 5, 1);
		
		%weapon = Player::getMountedItem(%id, $WeaponSlot);	
		
		if(%clientId != %id) %level = Cap(fetchData(%id,"LVL") / 5, 1, 25);
		else				 %level = Cap(fetchData(%id,"LVL") / 10, 1, 10);
		
		if (%weapon) 	%equipment = "CLASS " @ fetchData(%id,"CLASS") @ " LVL " @ %level @ " " @ %weapon @ " 1";
		else 			%equipment = "CLASS " @ fetchData(%id,"CLASS") @ " LVL " @ %level;
		
		%targetRace = fetchData(%id,"RACE");		
		if(%clientId != %id) %name = String::replace(rpg::getname(%id), %targetRace, "Mirage");
		else				 %name = rpg::getname(%id);
		
		%team = GameBase::getTeam(%id);
		
		%id = SpawnIllusionHelper(%clientId, %name, %pos, %equipment, %targetRace, 20);
		if(%id != False) {
			Player::mountItem(%id,%weapon,$WeaponSlot);
			if(%selfProjection)	Client::sendMessage(%clientId, $MsgBeige, "A projection of yourself emerges from the ether.");
			else				Client::sendMessage(%clientId, $MsgBeige, "Your projection of " @ rpg::getname(%id) @ " appears from the ether.");
			if(%team > 1 && $Server::teamSkin[%team] != "") {
				Client::setSkin(%id, $Server::teamSkin[%team]);
			}
			%returnFlag = True;
		} else {
			Client::sendMessage(%clientId, $MsgRed, "Your Mirage was unable to spawn.");
			%overrideEndsound = True;
			%returnFlag = False;
		}
	}
	else if($Spell::index[ironmaiden] == %index) {
		if(%castPos != "") {
			%armor								= "BronzePlateMail";
			if((getRandom() * 100) % 2 == 1)	%weapon = "BroadSword";				
			else								%weapon = "SpikedClub";		
			%equipment = "CLASS Fighter " @ %armor @ "0 1 LVL " @ floor(fetchData(%clientId,"LVL") / 1.75) @ " " @ %weapon @ " 1";
			%id = SpawnIllusionHelper(%clientId, "IronMaiden", %castPos, %equipment, "FemaleHuman", 60);
			if(%id != False) {
				UpdateBonusState(%id, "THORN 5", 30, 80);
				Player::mountItem(%id,%weapon,$WeaponSlot);
				Client::setSkin(%id, $ArmorSkin[%armor]);
				Client::sendMessage(%clientId, $MsgBeige, "Your Iron Maiden readies its Thorns and " @ %weapon @ " for battle.");
			}
			%returnFlag = True;
		} else {
			Client::sendMessage(%clientId, $MsgRed, "The Maiden cannot break from the ether at that distance.");
			%overrideEndsound = True;
			%returnFlag = False;
		}
	}
	else if($Spell::index[crystalmaiden] == %index) {
		if(%castPos != "") {
			%armor								= "AdvisorRobe";
			if((getRandom() * 100) % 2 == 1)	%weapon = "FrozenWand";				
			else								%weapon = "Spear";		
			%equipment = "CLASS Enchanter " @ %armor @ "0 1 LVL " @ floor(fetchData(%clientId,"LVL") * 2) @ " " @ %weapon @ " 1";			
			%id = SpawnIllusionHelper(%clientId, "CrystalMaiden", %castPos, %equipment, "FemaleHumanRobed", 120);
			if(%id != False) {
				UpdateBonusState(%id, "ATK 5", 30, 200);
				Player::mountItem(%id,%weapon,$WeaponSlot);
				Client::setSkin(%id, $ArmorSkin[%armor]);
				Client::sendMessage(%clientId, $MsgBeige, "Your Crystal Maiden breaks free of the ether with its " @ %weapon @ " at the ready.");
			}
			%returnFlag = True;
		} else {
			Client::sendMessage(%clientId, $MsgRed, "The Maiden cannot break from the ether at that distance.");
			%overrideEndsound = True;
			%returnFlag = False;
		}
	}
	else if(isOneOf(%index,$Spell::index[barrier],$Spell::index[advbarrier])) {
		if(%castPos != "") {
			if(getObjectType(%castObj) == "Player") {
				%returnFlag = True;
				Client::sendMessage(%clientId, $MsgGreen, "You imagine a barrier appearing on " @ rpg::getName(Player::getClient(%castObj)) @ ".");
			} else {
				Client::sendMessage(%clientId, $MsgGreen, "You imagine a wall into existence.");
			}
			if(%index == $Spell::index[barrier]) {			
				%object = newObject("", StaticShape, SpellFXvertforceshield);
				schedule("deleteObject(" @ %object @ ");", Cap(%skillLevel / 25, 5, 25));
			} else {
				%object = newObject("", StaticShape, SpellFXdomesmall);
				schedule("deleteObject(" @ %object @ ");", Cap(%skillLevel / 75, 5, 15));
			}
			addToSet("MissionCleanup", %object);
			GameBase::setPosition(%object, %castPos);	
			GameBase::setRotation(%object, GameBase::getRotation(%clientId));		
			playSound($Spell::endSound[%index], %castPos);
		} else {
			%returnFlag = False;
			%overrideEndsound = True;
			Client::sendMessage(%clientId, $MsgRed, "You can't imagine a wall being that far away...");
		}
			
	}
	else if(isOneOf(%index,$Spell::index[fear],$Spell::index[massfear])) {
		%id = Player::getClient(%castObj);
		if(getObjectType(%castObj) == "Player" && %id != %clientId) {
			%iterations = Cap(%skillLevel / 80, 2, 8);
			for(%x=1;%x<=%iterations;%x++){
				%multiplier = Cap((%skillLevel / 10), 35, 80);
				%vel = Vector::Random(%multiplier);
				%vel = getword(%vel,0) @ " " @ getword(%vel,1) @ " " @ (%multiplier * 2);
				schedule("Player::applyImpulse(" @ %id @ ", \"" @ %vel @ "\");", %x);
			}
		}
	}
	else if(%index == $Spell::index[burden]) {
		if(getObjectType(%castObj) == "Player") {
			%id = Player::getClient(%castObj);
			if(Player::isAiControlled(%id)) {
				%iterations = Cap(%skillLevel / 80, 2, 15);
				FreezeAI(%id);
				for(%x=1; %x<=%iterations; %x++) {
					schedule("FreezeAI(" @ %id @ ");", %x );
				}
			}
			%returnFlag = True;
		} else %returnFlag = False;
	}
	else if($Spell::index[illuminate] == %index) {
		%pos = GameBase::getPosition(%clientId);
		CreateAndDetBomb(%clientId, "SpellFXilluminate", %pos, 0, 0);
		%iterations = Cap(%skillLevel / 50, 1, 20);
		for(%x=1;%x<=%iterations;%x++) {
			//if(%x % 2 == 1)
				schedule("PlaySound(\"SoundLaserIdle\", GameBase::getPosition(" @ %clientId @ "));", %x + 0.3);
			schedule("CreateAndDetBomb(" @ %clientId @ ", \"SpellFXilluminate\", GameBase::getPosition(" @ %clientId @ "), 0, 0);", %x);
		}
	}
	
	if(!%skipEndCast) return EndCast(%clientid,%index,%castpos,%returnflag);
}



//________________________________________________________________________________________________
// Absolutely force-stop an AI
function FreezeAI(%id) {
	if(!IsDead(%id) && Player::isAiControlled(%id)){
		storeData(%id, "frozen", True);		
		%name = fetchData(%id, "BotInfoAiName");
		AI::setVar(%name, SpotDist, 0);
		AI::newDirectiveRemove(%name, 99);
		schedule("UnfreezeAI(" @ %id @ ");", Cap(getRandom(), 0.3, 0.8) );
	}
}



//________________________________________________________________________________________________
// Ask AI to return to its main loop (doesnt work worth a damn in most cases, but works here)
function UnfreezeAI(%id) {
	if(!IsDead(%id) && Player::isAiControlled(%id)){
		storeData(%id, "frozen", "");
		AI::SetSpotDist(%id);
	}
}



//________________________________________________________________________________________________
// mirage | ironmaiden | crystalmaiden
function SpawnIllusionHelper(%clientId, %name, %pos, %equipment, %race, %lifetime) {
	if(CountObjInCommaList(fetchData(%clientId, "PersonalPetList")) >= $maxPetsPerPlayer) {
		return False;
	}
	for(%i = 0; %i < 99; %i++) {
		if(NEWgetClientByName(%name @ %i) == -1) {
			%name = %name @ %i;
			break;
		}
	}	
	$BotEquipment[generic] = %equipment;
	%an = AI::helper("generic", %name, "TempSpawn " @ %pos @ " " @ GameBase::getTeam(%clientId));
	%id = AI::getId(%an);
	ChangeRace(%id, %race);
	storeData(%id, "BotInfoAiName", %an);
	storeData(%id, "tmpbotdata", %clientId);
	storeData(%id, "botAttackMode", 2);
	AI::SetSpotDist(%id);
	$PetList = AddToCommaList($PetList, %id);
	storeData(%clientId, "PersonalPetList", AddToCommaList(fetchData(%clientId, "PersonalPetList"), %id));
	storeData(%id, "petowner", %clientId);
	storeData(%id, "OwnerID", %clientId);	
	storeData(%id, "noDropLootbagFlag", True);
	AI::SelectMovement(%an);
	schedule("Player::kill(" @ %id @ ");", %lifetime);
	return %id;
}



//________________________________________________________________________________________________
// cloud | advcloud
MineData SpellFXcloud
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
	explosionId = flashExpLarge;
	explosionRadius = 10.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
function SpellFXcloud::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// illuminate
ExplosionData IlluminateExplosion 
{
   shapeName = "smoke.dts";
   soundId   = NoSound;
   faceCamera = false;
   randomSpin = false;
   hasLight   = true;
   lightRange = 20.0;
   timeZero = 0.0;
   timeOne  = 2.0;
   colors[0]  = { 1,1,1 };
   colors[1]  = { 1,1,1 };
   colors[2]  = { 1,1,1 };
   radFactors = { 0.0, 1.0, 1.0 };
};
MineData SpellFXilluminate
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
	explosionId = IlluminateExplosion;
	explosionRadius = 15.0;
	damageValue = 1.0;
	damageType = $NullDamageType;
	kickBackStrength = 0;
	triggerRadius = 0.5;
	maxDamage = 1.0;
};
function SpellFXilluminate::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.2, %this); }



//________________________________________________________________________________________________
// barrier | advbarrier
DebrisData ForceFieldDebris 
{
   type      = 0;
   imageType = 0;   
   mass       = 100.0;
   elasticity = 0.25;
   friction   = 0.5;
   center     = { 0, 0, 0 };
   animationSequence = -1;
   minTimeout = 3.0;
   maxTimeout = 6.0;
   explodeOnBounce = 0.3;
   damage          = 1000.0;
   damageThreshold = 100.0;
   spawnedDebrisMask     = 1;
   spawnedDebrisStrength = 90;
   spawnedDebrisRadius   = 0.2;
   spawnedExplosionID = debrisExpSmall;
   p = 1;
   explodeOnRest   = True;
   collisionDetail = 0;
};
StaticShapeData SpellFXvertforceshield
{
	shapeFile = "shieldshield";
	debrisId = ForceFieldDebris;
	maxDamage = 10000.0;
	visibleToSensor = false;
	isTranslucent = true;
   	description = "Door Force Field";
};
StaticShapeData SpellFXdomesmall 
{
	shapeFile = "domefiled";
	debrisId = ForceFieldDebris;
	maxDamage = 10000.0;
	visibleToSensor = false;
	isTranslucent = true;
   	description = "Door Force Field";
};
