//This file is part of Tribes RPG.
//Tribes RPG server side scripts
//Written by Jason "phantom" Daley,  Matthiew "JeremyIrons" Bouchard, and more (yet undetermined)

//	Copyright (C) 2014  Jason Daley

//	This program is free software: you can redistribute it and/or modify
//	it under the terms of the GNU General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.

//	This program is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//	GNU General Public License for more details.

//	You should have received a copy of the GNU General Public License
//	along with this program.  If not, see <http://www.gnu.org/licenses/>.

//You may contact the author at beatme101@gmail.com or www.tribesrpg.org/contact.php

//This GPL does not apply to Starsiege: Tribes or any non-RPG related files included.
//Starsiege: Tribes, including the engine, retains a proprietary license forbidding resale.

//______________________________________________________________________________________________________________________________________________
// DescX Note:
	// Everything about casting has changed, but nothing about casting has changed. I rewrote the face of the
	// casting API, fixed a pile of bugs, split up some functions, and then sorted out which code paths might
	// be useful to generalize. Spells now use a bitfield of options(see below) to determine their "type"
	// when being #casted, priced, idiomized, and when being selected by AI. However, the underlying
	// globals used for spells haven't changed. You can easily snap-in or adapt all kinds of older custom code.
	//
	// It is possible to create the types of spells used in base TRPG with a single call to SpellDefinition().
	// Several new types of standard spells are also possible. Negative damage implies healing, and it's
	// possible to create spells that buff, damage, and target groups with no extra code beyond a definition.
	//
	// When something more complicated is required, set (%targetType | $TargetSpecial). The spell will re-route
	// to a Cast<x>Magic() function, where you can detect the index and carry on writing a hard-coded spell
	// exactly as it was done in TRPG.

$TargetDamageSpell 		= 1;
$TargetBeamSpell		= 2;
$TargetSpecial			= 4;
$TargetOther			= 8;
$TargetGroup			= 16;
$TargetBuffAlly			= 32;
$TargetBuffEnemy		= 64;
$TargetInvertAllyDamage	= 128;
$TargetProjectileSpell	= 256;


// ALL YOUR PARAMETERS ARE BELONG TO US BWAAAAAHAHAHA
// This is nasty. I can has constructor? No? Fuk.
function SpellDefinition(%skilltype, %keyword, %priority, %mana, 
						 %name, %description, %damageValue, %range, %radius, 
						 %sndStart, %sndEnd, %gracedist, %delay, %recovery, %duration, %groupOnly, %hasMenu,
						 %targetType, %bonuses,
						 %numDetonations, %fxDetonate) {
	if(!$SpellDefIndex)
		$SpellDefIndex = 0;
	%whichIndex = $Spell::index[%keyword];
	if(%whichIndex == "" || %whichIndex <= 0) {
		$SpellDefIndex++;
		%whichIndex = $SpellDefIndex;
	}
	
	$Spell::keyword[%whichIndex] = %keyword;
	$Spell::index[%keyword] = %whichIndex;
	$Spell::name[%whichIndex] = %name;
	$Spell::description[%whichIndex] = %description;
	$Spell::delay[%whichIndex] = %delay;
	$Spell::ticks[%whichIndex] = %duration;
	$Spell::recoveryTime[%whichIndex] = %recovery;
	$Spell::radius[%whichIndex] = %radius;
	$Spell::damageValue[%whichIndex] = %damageValue;
	$Spell::LOSrange[%whichIndex] = %range;
	$Spell::manaCost[%whichIndex] = %mana;
	$Spell::startSound[%whichIndex] = %sndStart;
	$Spell::endSound[%whichIndex] = %sndEnd;
	$Spell::groupListCheck[%whichIndex] = %groupOnly;
	$Spell::refVal[%whichIndex] = %priority;
	$Spell::graceDistance[%whichIndex] = %gracedist;
	$Spell::targetType[%whichIndex] = %targetType;
	$Spell::bonuses[%whichIndex] = %bonuses;
	$Spell::FXDetonate[%whichIndex] = %fxDetonate;
	$Spell::FXDetonateCount[%whichIndex] = %numDetonations;
	
	$SkillType[%keyword] = %skilltype;
	$Spell::menu[%whichIndex] = %hasMenu;
}

//________________________________________________________________________________________________
// Can we cast? If there's enough mana and a potential target, DoCastSpell
//________________________________________________________________________________________________
function BeginCastSpell(%clientId, %keyword) {
	if(fetchData(%clientId, "invisible") == 2) {
		Client::sendMessage(%clientId, $MsgRed, "You cannot cast spells use skills while Shadow Walking. #shadowwalk again to unhide.");
		return;
	}

	%w1 = GetWord(%keyword, 0);
	%w2 = String::getSubStr(%keyword, String::len(%w1)+1, 99999);
	%i = $Spell::index[%w1];	
	if(!%i) {
		Client::sendMessage(%clientId, $MsgWhite, "This spell seems unfamiliar to you.");
		return False;
	}

	if(SkillCanUse(%clientId, $Spell::keyword[%i])) {
		if(fetchData(%clientId, "MANA") >= $Spell::manaCost[%i]) {
			%skt = $SkillType[$Spell::keyword[%i]];
			
			%verb = "Casting";
			if(%skt == $SkillCombatArts)
				%verb = "Using";
			
			Client::sendMessage(%clientId, $MsgBeige, %verb @ " " @ $Spell::name[%i] @ ".");

			%player = Client::getOwnedObject(%clientId);
			if(GameBase::getLOSinfo(%player, $Spell::LOSrange[%i])) {
				%losobj = $los::object;
			} else {
				%losobj = 0;
			}

			storeData(%clientId, "SpellCastStep", 1);

			%tempManaCost = floor($Spell::manaCost[%i] / 2);
			refreshMANA(%clientId, %tempManaCost);
			if($Spell::startSound[%i] != "")
				playSound($Spell::startSound[%i], GameBase::getPosition(%clientId));

			
			%sk1 = GetSkillWithBonus(%clientId, %skt);
			%gsa = rpg::GetAdjustedSkillRestriction(%clientId, $Spell::keyword[%i], %skt);
			%sk2 = %sk1 - %gsa;
			%sk = Cap(%sk2, 0, "inf");
			%rt = $Spell::recoveryTime[%i];
			%a = %rt / 2;
			%b = (1000 - %sk) / 1000;
			%c = %b * %a;
			//recovery time is never smaller than half of the original and never bigger than the original.
			%recovTime = Cap(%a + %c, %a, %rt);
			storeData(%clientId, "SpellRecovTime", %recovTime);
			
			
			%modifier = Cap(GetSkillWithBonus(%clientId, $SkillFocus) / 250, 1, 4);
			%delay = $Spell::delay[%i] / %modifier;
			
			if(rpg::IsMemberOfHouse(%clientId, "Order Of Qod")) {
				%reductionFactor = 1 + Cap(rpg::GetHouseLevel(%clientId) / 20, 0, 4);
				%recovTime 	/= %reductionFactor;
				%delay 		/= %reductionFactor;
				if(%recovTime < 0.2) 	%recovTime = 0;
				if(%delay < 0.2) 		%delay = 0;
			}
			
			if($spell::menu[%i])
				eval("casting::"@$spell::keyword[%i]@"(" @ %clientId @ ", " @ %i @ ", \"" @ GameBase::getPosition(%clientId) @ "\", \"" @ %losobj @ "\", \"" @ %w2 @ "\");");
			else if(%delay > 0)
				schedule("DoCastSpell(" @ %clientId @ ", " @ %i @ ", \"" @ GameBase::getPosition(%clientId) @ "\", \"" @ %losobj @ "\", \"" @ %w2 @ "\");", %delay);
			else
				DoCastSpell(%clientId, %i, GameBase::getPosition(%clientId), %losobj, %w2);
			return True;
		} else Client::sendMessage(%clientId, $MsgWhite, "Insufficient mana to cast this spell.");
	} else Client::sendMessage(%clientId, $MsgRed, "You can't cast " @ $Spell::name[%i] @ " because you lack the necessary skills.");

	return False;
}


//________________________________________________________________________________________________
// Called by CastSpell when LOSrange is 0 and radius > 0. Generates new CastSpell call for each
// valid entity in a box to perform mass spell casts.
//________________________________________________________________________________________________
function DoRadialCast(%object, %clientId, %index, %extra) {
	%id = Player::getClient(%object);
	%cast = False;
	
	if(isOneOf(%index, $Spell::index[masssmite])) {						
		%cast = True;
	}
	
	if(%clientId != %id) {		
		%isPet 				= IsInCommaList(fetchData(%clientId, "PersonalPetList"), %id);
		%isInCasterGroup 	= IsInCommaList(fetchData(%clientId, "grouplist"), Client::getName(%id));
		%isInTargetGroup 	= IsInCommaList(fetchData(%id, "grouplist"), Client::getName(%clientId));
		
		//only teammates
		if(GameBase::getTeam(%clientId) == GameBase::getTeam(%id)) {	
			if(isOneOf(%index, 	$Spell::index[massheal],$Spell::index[massfullheal],$Spell::index[massshield],$Spell::index[massguardian],
								$Spell::index[massprotect],$Spell::index[massregen],$Spell::index[massreflect])) {		
				%cast = True;
			}
		} 
		// only enemies
		else if(isOneOf(%index, $Spell::index[massmanatap], $Spell::index[massfear])) {		
			%cast = True;
		}	
		// Only Grouplisted
		if(%isPet || %isInCasterGroup || %isInTargetGroup) {
			if(isOneOf(%index, $Spell::index[masstransport], $Spell::index[massteleport])) {
				%cast = True;
			}
		}		
	}
	
	if(%cast) {
		CastSpell(%clientId, %index, GameBase::getPosition(%clientId), %object, %extra, True);
	}
}


//________________________________________________________________________________________________
// The caster has enough mana and a valid target - DoCastSpell
//________________________________________________________________________________________________
function DoCastSpell(%clientId, %index, %oldpos, %castObj, %w2, %isWordSmith) {
	return CastSpell(%clientId, %index, %oldpos, %castObj, %w2, False);
}

//________________________________________________________________________________________________
// The actual spell cast, called either by DoCastSpell or DoRadialSpell
//________________________________________________________________________________________________
function CastSpell(%clientId, %index, %oldpos, %castObj, %w2, %targetSpecified, %isWordSmith) {
	if(IsDead(%clientId))
		return;

	if(fetchData(%clientId, "invisible") == 2) {
		Client::sendMessage(%clientId, $MsgRed, "You cannot cast spells or use skills while Shadow Walking. #shadowwalk again to unhide.");
		return;
	}

	if(Player::isAiControlled(%clientId))
		rpg::FaceEnemy(%clientId, fetchData(%clientId, "AILastTarget"));

	%player = Client::getOwnedObject(%clientId);
	
	if(%targetSpecified == False || %targetSpecified > 10) {		if($Spell::graceDistance[%index] == "")			$Spell::graceDistance[%index] = 0.25;		$los::position = "";
		$los::object = "";		if(GameBase::getLOSinfo(%player, $Spell::LOSrange[%index])) {			%castPos = $los::position;			%castObj = $los::object;
			if(%castObj < 1)
				%castObj = %clientId;		}
		
		if(Vector::getDistance(%oldpos, GameBase::getPosition(%clientId)) > $Spell::graceDistance[%index]) {
			Client::sendMessage(%clientId, $MsgBeige, "Your casting was interrupted.");
			%returnflag = False;
			return EndCast(%clientid,%index,%castpos,%returnflag);
		}

		//group-list check
		if($Spell::groupListCheck[%index]) {
			%cl = Player::getClient(%castObj);
			%mutalGroupListed = IsInCommaList(fetchData(%clientId, "grouplist"), Client::getName(%cl)) &&
								IsInCommaList(fetchData(%cl, "grouplist"), Client::getName(%clientId));
			%isPet = IsInCommaList(fetchData(%clientId, "PersonalPetList"), %cl);
			if( !(%isPet || %mutalGroupListed) && %cl != %clientId && %cl != -1) {
				Client::sendMessage(%clientId, $MsgBeige, "You are not part of the target's group.");
				%returnflag = False;
				return EndCast(%clientid,%index,%castpos,%returnflag);
			}
		}
	}
	if(%castObj == 0)
		%castObj = "";
	%returnFlag = False;
	
	if(%targetSpecified == False && ($Spell::targetType[%index] & $TargetGroup)) {
		%b = $Spell::radius[%index] * 2;
		%set = newObject("set", SimSet);
		%n = containerBoxFillSet(%set, $SimPlayerObjectType, GameBase::getPosition(%clientId), %b, %b, %b, 0);
		Group::iterateRecursive(%set, DoRadialCast, %clientId, %index, %w2);
		deleteObject(%set);
	}
	
	if(%isWordSmith) 	%skillType = $SkillWordsmith;
	else 				%skillType = $SkillType[%spellname];
	
	%skillLevel = GetSkillWithBonus(%clientId, %skillType);	
	%spellname 	= $Spell::keyword[%index];
	%divisor 	= Cap(rpg::GetAdjustedSkillRestriction(%clientId, %spellname, %skillType), 100, 500);
	%multiplier = Cap(%skillLevel / %divisor, 0.5, 5);
	%r 			= $Spell::damageValue[%index];
	if(%isWordSmith){
		%damageType = $WordsmithDamageType;
	} else {
		%damageType = $SpellDamageType;
	}
	if(%castObj != "") {
		if(getObjectType(%castObj) == "Player" && GameBase::getTeam(%clientId) == GameBase::getTeam(Player::getClient(%castObj))) {
			if($Spell::targetType[%index] & $TargetInvertAllyDamage) {
				%r *= -0.5;
			}
		}
	}
	
	if(rpg::IsMemberOfHouse(%clientId, "Order Of Qod")) {
		if($SkillType[%spellname] == $SkillRestorationMagic)
			%r *= 1 + Cap(rpg::GetHouseLevel(%clientId) / 30, 0, 5);
	}
	
	// Direct damage spells
	if($Spell::targetType[%index] & $TargetDamageSpell) {		
		if($Spell::targetType[%index] & $TargetBeamSpell) {
			if(%castObj != "") {
				if(getObjectType(%castObj) == "Player")
					%id = Player::getClient(%castObj);
			}
			%trans = Gamebase::getEyeTransform(%clientId);
			%p = Projectile::spawnProjectile($Spell::FXDetonate[%index], %trans, %player, "0 0 0", 1);
			%mom1 = Vector::getFromRot( GameBase::getRotation(%clientId), -60, 1 );
			Player::applyImpulse(%clientId, %mom1);		
			if(%id != "") {
				if(%r > 0) {
					GameBase::virtual(%id, "onDamage", %damageType, $Spell::damageValue[%index], "0 0 0", "0 0 0", "0 0 0", "torso", "front_right", %clientId, %spellname);
					%mom2 = Vector::getFromRot( GameBase::getRotation(%clientId), 50, 1 );
					Player::applyImpulse(%id, %mom2);
				} else if (%r < 0) {
					%mom2 = Vector::getFromRot( GameBase::getRotation(%clientId), -50, 1 );
					Player::applyImpulse(%id, %mom2);
				}
			}
			%castPos = GameBase::getPosition(%clientId);
			%returnFlag = True;
		} else if($Spell::targetType[%index] & $TargetProjectileSpell) {	// DescX Note: I can't hook RocketData events. AI IDs are blank when firing RocketData.
			%bomb = newObject("", "Mine", $Spell::FXDetonate[%index]);
			%bomb.owner = %clientId;
			addToSet("MissionCleanup", %bomb);
			%rot = GameBase::getRotation(%clientId);				// Is there a workaround? I don't give a shit. This is stupid.
			%newrot = NewVector(VecX(%rot) + $ProjectileSpellZOffset[%spellname], VecY(%rot), VecZ(%rot));
			GameBase::setRotation(%clientId, %newrot);
			GameBase::Throw(%bomb, Client::getOwnedObject(%clientId), $ProjectileSpellVelocity[%spellname], false);
			GameBase::setRotation(%clientId, %rot);
			$MineTracker[%bomb] = GameBase::getPosition(%bomb);
			$MineExploded[%bomb] = 0;
			$MineTrackerTicks[%bomb] = $ProjectileSpellLifetime[%spellname];
			schedule("WandProjectileThinker(" @ %bomb @ ", \"" @ %spellname @ "\", \"\", 1, " @ 
													($ProjectileSpellLifetime[%spellname] < 0) @ ", " @ 
													$ProjectileSpellRadius[%spellname] @ " );", 0.1);
			%castPos = GameBase::getPosition(%clientId);
			%returnFlag = True;		
		} else {
			if($Spell::FXDetonateCount[%index] > 0) {
				
				if($Spell::LOSrange[%index] == 0) {
					%castPos = GameBase::getPosition(%clientId);
				}
				
				%missingReqTarget = ((%castObj > 0 && %castObj != "") && (($Spell::targetType[%index] & $TargetOther) && getObjectType(%castObj) != "Player"));
				if(%r < 0 && !IsInCommaList(fetchData(%clientId, "grouplist"), Client::getName(%id)))
					%castPos = GameBase::getPosition(%clientId);
				
				if(%castPos != "" && !%missingReqTarget) {
					CreateAndDetBomb(%clientId, $Spell::FXDetonate[%index], %castPos, %r > 0, %index, (%damageType == $WordsmithDamageType));
					%returnFlag = True;
				} else if(%r > 0 && !IsInCommaList(fetchData(%clientId, "grouplist"), Client::getName(%id))) {
					Client::sendMessage(%clientId, $MsgBeige, "Could not find a target.");
					%returnFlag = False;
				}
			}
		}
		
		// Healing spells
		if(%r < 0) {
			if((%castObj > 0 && %castObj != "") && ($Spell::targetType[%index] & $TargetOther) && getObjectType(%castObj) == "Player")
				%id = Player::getClient(%castObj);
			else %id = %clientId;
			
			if(!IsInCommaList(fetchData(%clientId, "grouplist"), Client::getName(%id))){
				%id = %clientId;
			}
			
			if(%clientId != %id)
				Client::sendMessage(%id, $MsgBeige, Client::getName(%clientId) @ " is casting " @ $Spell::name[%index] @ " on you.");

			if($Spell::damageValue[%index] <= -9999) {
				%r = fetchData(%id, "MaxHP");
				setHP(%id, %r);
				%r /= $TribesDamageToNumericDamage * -1;
			} else {
				%r = %r * %multiplier / $TribesDamageToNumericDamage;
				refreshHP(%id, %r);
			}
			%castPos = GameBase::getPosition(%id);
			
			if($Spell::FXDetonateCount[%index] > 0 && !($Spell::targetType[%index] & $TargetBeamSpell)) {
				CreateAndDetBomb(%clientId, $Spell::FXDetonate[%index], %castPos, False, %index, (%damageType == $WordsmithDamageType));
			}
			
			Client::sendMessage(%clientId, $MsgBeige, "Healing " @ Client::getName(%id) @ " for " @ RoundToFirstDecimal((-1 * %r) * 100) @ " HP" );

			%returnFlag = True;
		}
	}
	
	%buffAlly 	= ($Spell::targetType[%index] & $TargetBuffAlly);
	%buffEnemy 	= ($Spell::targetType[%index] & $TargetBuffEnemy);
	
	// Bonuses over time
	if((%buffAlly || %buffEnemy) && $Spell::ticks[%index] > 0) {
		if((%castObj > 0 && %castObj != "") && ($Spell::targetType[%index] & $TargetOther) && getObjectType(%castObj) == "Player")
			%id = Player::getClient(%castObj);
		else %id = %clientId;
		
		%sameTeam 	= (GameBase::getTeam(%id) == GameBase::getTeam(%clientId));
		
		if((%sameTeam && %buffAlly) && !IsInCommaList(fetchData(%clientId, "grouplist"), Client::getName(%id))){
			%id = %clientId;
		}
		if((!%sameTeam && %buffAlly)){
			%id = %clientId;
		}
		else if(!(%sameTeam && %buffAlly) && !(!%sameTeam && %buffEnemy)) {			
			%id = %clientId;
		} else {
			%lastParse = "";
			%lastParseTicks = 0;
			for(%i=0;%i<99;%i+=3) {
				%bonus = GetWord($Spell::bonuses[%index],%i);
				if(%bonus == -1) break;				
				
				%buffid  = GetWord($Spell::bonuses[%index], %i + 1);
				if(%buffid == -1) break;	
				%val = GetWord($Spell::bonuses[%index], %i + 2);
				if(%val == -1) { // No third word? Use the second word as both an ID and a Value
					%val = %buffid;
					%i -= 1;
				}				
				
				if(%buffid == "!")	%multiplier = 1;
				else				%multiplier = Cap(%skillLevel / %divisor, 0.5, 5);

				%lastParse = %bonus @ " " @ %buffid;
				%asMessage = $BonusStateName[%bonus];
				%val *= %multiplier;
				UpdateBonusState(%id, %lastParse, $Spell::ticks[%index], %val);
				RefreshAll(%id);
				
				%msg2 = " with " @ %asMessage @ " (" @ RoundToFirstDecimal(%val) @ ")";
				if(%val < 0) %msgcolour = $MsgRed;
				else if(%val > 0) %msgcolour = $MsgGreen;
				else %msgcolour = $MsgBeige;
				if(%id != %clientId) {
					Client::sendMessage(%id, %msgcolour, "Buffed by " @ Client::getName(%clientId) @ %msg2);
					Client::sendMessage(%clientId, %msgcolour, "Buffing " @ Client::getName(%id) @ %msg2);
				} else {
					Client::sendMessage(%clientId, %msgcolour, "Buffing " @ Client::getName(%clientId) @ %msg2);
				}
				%returnFlag = True;
				%castPos = GameBase::getPosition(%id);				
			}			
		}
		
		if(!%returnFlag) {
			if(%buffAlly)		Client::sendMessage(%clientId, $MsgBeige, "You cannot target enemies with this spell.");
			else if(%buffEnemy) Client::sendMessage(%clientId, $MsgBeige, "You must target enemies with this spell.");
		}
	}
	
	if(!%targetSpecified && %returnFlag) {
		for(%x=1; %x<floor($Spell::FXDetonateCount[%index]);%x++) {
			%timeBetween = ($Spell::FXDetonateCount[%index] - floor($Spell::FXDetonateCount[%index])) * 10;
			%isWordSmith = (%damageType == $WordsmithDamageType);
			schedule("CastSpell(" @ %clientId @ ", " @ %index @ ", \"" @ %oldpos @ "\", \"\", \"" @ %w2 @ "\", 999, " @ %isWordSmith @ ");",
						%x * %timeBetween);
		}
	}

	if($Spell::targetType[%index] & $TargetSpecial) {
		%skt = $SkillType[$Spell::keyword[%index]];
		if(%skt == $SkillElementalMagic) 		return CastElementalMagic(%player, %clientId, %index, %oldpos, %castPos, %castObj, %w2, %returnFlag, %targetSpecified, %isWordSmith);
		else if(%skt == $SkillRestorationMagic) return CastRestorationMagic(%player, %clientId, %index, %oldpos, %castPos, %castObj, %w2, %returnFlag, %targetSpecified, %isWordSmith);
		else if(%skt == $SkillDistortionMagic) 	return CastDistortionMagic(%player, %clientId, %index, %oldpos, %castPos, %castObj, %w2, %returnFlag, %targetSpecified, %isWordSmith);
		else if(%skt == $SkillIllusionMagic) 	return CastIllusionMagic(%player, %clientId, %index, %oldpos, %castPos, %castObj, %w2, %returnFlag, %targetSpecified, %isWordSmith);
		else if(%skt == $SkillCombatArts) 		return CastCombatArts(%player, %clientId, %index, %oldpos, %castPos, %castObj, %w2, %returnFlag, %targetSpecified, %isWordSmith);
	}
	
	if(!%targetSpecified) return EndCast(%clientid,%index,%castpos,%returnflag);
}


//________________________________________________________________________________________________
// Finish casting - consume other half of mana, upgrade skills, and reset flags
//________________________________________________________________________________________________
function EndCast(%clientid,%index,%castpos,%returnflag) {
	%raceTeam = $TeamForRace[fetchData(%clientId,"RACE")];
		
	if(%raceTeam == 1)		Player::setAnimation(%clientId, 41);
	else if(%raceTeam == 2)	Player::setAnimation(%clientId, 38);
	else if(%raceTeam == 3)	Player::setAnimation(%clientId, 40);
	else if(%raceTeam == 4)	Player::setAnimation(%clientId, 45);
	else if(%raceTeam == 5)	Player::setAnimation(%clientId, 50);
	else if(%raceTeam == 6)	Player::setAnimation(%clientId, 46);
	else if(%raceTeam == 7)	Player::setAnimation(%clientId, 47);	else 					Player::setAnimation(%clientId, 39);	if(%returnflag) {		playSound($Spell::endSound[%index], %castPos);	}
		storeData(%clientId, "SpellCastStep", 2);	%recovTime = fetchData(%clientId, "SpellRecovTime");	%skilltype = $SkillType[$Spell::keyword[%index]];
		if(%returnFlag == True) {		UseSkill(%clientId, %skilltype, True, True);
		UseSkill(%clientId, %skilltype, True, True);		UseSkill(%clientId, $SkillFocus, True, True);		%tempManaCost2 = $Spell::manaCost[%index] / 2;		%tempManaCost = floor($Spell::manaCost[%index] / 2);		if(%tempManaCost2 != %tempManaCost)			%tempManaCost += 1;		refreshMANA(%clientId, %tempManaCost);	} else if(%returnFlag == False) {		UseSkill(%clientId, %skilltype, False, True);		UseSkill(%clientId, $SkillFocus, False, True);		%recovTime = %recovTime * 0.5;	}
		
	schedule("storeData(" @ %clientId @ ", \"SpellCastStep\", \"\");", %recovTime);
	if(!Player::isAiControlled(%clientId)){		if(%clientId.repack > 32) {			remoteEval(%clientId, "rpgbarhud", %recovTime, 4, 2, "||");				} else schedule("sendDoneRecovMsg(" @ %clientId @ ");", %recovTime);
	}
	
	if(Player::isAiControlled(%clientId)) {
		%botname = fetchData(%clientId, "BotInfoAiName");
		%target = fetchData(%clientId, "AILastTarget");
		if(!IsDead(%target) && (GameBase::getTeam(%target) != GameBase::getTeam(%clientId))) {
			AI::newDirectiveFollow(%botname, %target, 0, 99);
		}
	}
		return %returnFlag;}


//________________________________________________________________________________________________
function sendDoneRecovMsg(%clientId) {
	Client::sendMessage(%clientId, $MsgBeige, "You are ready to cast.");
}

//________________________________________________________________________________________________
function CreateSpellBomb(%clientId, %whichFx, %pos, %repeats) {
	if(!IsDead(%clientid)){
		%player = Client::getOwnedObject(%clientId);
		%bomb = newObject("", "Mine", %whichFX);
		addToSet("MissionCleanup", %bomb);
		GameBase::Throw(%bomb, %player, 0, false);
		GameBase::setPosition(%bomb, %pos);
		return %bomb;
	}
	return -1;
}

//________________________________________________________________________________________________
function CreateAndDetBomb(%clientId, %b, %castPos, %doDamage, %index, %isWordSmith) {
	if(%castPos != "") {
		if(!IsDead(%clientid)){
			CreateSpellBomb(%clientId, %b, %castPos);
			if(%doDamage) {
				%b = $Spell::radius[%index] * 2;
				%set = newObject("set", SimSet);
				
				%n = containerBoxFillSet(%set, $SimPlayerObjectType, %castPos, %b, %b, %b, 0);
				Group::iterateRecursive(%set, DoSpellDamage, %clientId, %castPos, %index, %isWordSmith);
				deleteObject(%set);
			}	
		}
		if($Spell::endSound[%index] != "" && $Spell::endSound[%index] != 0)
			playSound($Spell::endSound[%index], %castPos);	
	}
}

//________________________________________________________________________________________________
function DoSpellDamage(%object, %clientId, %pos, %index, %isWordSmith) {
	%id = Player::getClient(%object);
	%dist = Vector::getDistance(%pos, GameBase::getPosition(%id));	
	if(%dist <= $Spell::radius[%index]) {	
		if(%isWordSmith){
			%damageType = $WordsmithDamageType;
		} else {
			%damageType = $SpellDamageType;
		}
		%newDamage = rpg::GetSplashDamage(%dist, $Spell::radius[%index], $Spell::damageValue[%index], 15, 100);
		GameBase::virtual(%id, "onDamage", %damageType, %newDamage, "0 0 0", "0 0 0", "0 0 0", "torso", "front_right", %clientId, $Spell::keyword[%index]);
	}
}

//________________________________________________________________________________________________
function CalcSpellMiss(%clientId, %targetId, %index) {
	%range = $Spell::LOSrange[%index];
	%dist = Vector::getDistance(GameBase::getPosition(%clientId), GameBase::getPosition(%targetId));
	%m = floor((getRandom() * %range)) + (%range / 6);
	if(%m > %dist)	return False;
	else			return True;
}

//________________________________________________________________________________________________
function SpellCanCast(%clientId, %keyword) {
	for(%i = 1; $Spell::keyword[%i] != ""; %i++) {
		if(String::ICompare($Spell::keyword[%i], %keyword) == 0) {
			if(SkillCanUse(%clientId, $Spell::keyword[%i])) {
				if(fetchData(%clientId, "MaxMANA") >= $Spell::manaCost[%i])
					return True;
			}
		}
	}
	return False;
}

//________________________________________________________________________________________________
function SpellCanCastNow(%clientId, %keyword) {
	for(%i = 1; $Spell::keyword[%i] != ""; %i++) {
		if(String::ICompare($Spell::keyword[%i], %keyword) == 0) {
			if(SkillCanUse(%clientId, $Spell::keyword[%i])) {
				if(fetchData(%clientId, "MANA") >= $Spell::manaCost[%i])
					return True;
			}
		}
	}
	return False;
}
//________________________________________________________________________________________________
function Mine::onDamage(%this,%type,%value,%pos,%vec,%mom,%object) {
   if (%type == $MineDamageType) %value = %value * 0.25;
	%damageLevel = GameBase::getDamageLevel(%this);
	GameBase::setDamageLevel(%this,%damageLevel + %value);
}
function Mine::Detonate(%this) {
	%data = GameBase::getDataName(%this);
	GameBase::setDamageLevel(%this, %data.maxDamage);
}
