//__________________________________________________________________________________________________________________________
// DescX Notes:
//		The original onDamage function in TribesRPG was awful.
//		Then, I went and made it so, so much worse! :)
//
//		Readability issues were leading to bugs. I decided it was best to fix it and wound up rewriting most of the code.
//		It doesn't fundamentally change the way the old onDamage worked -- except for the stuff related to World Ender.
//		There are differences in behaviour -- e.g. message colours are different than vanilla and bashing is automatic 
//		but otherwise, the same checks are performed.
//__________________________________________________________________________________________________________________________

$DamageSkill[$SkillSlashing]	= $SkillStrength;
$DamageSkill[$SkillBludgeoning]	= $SkillStrength;
$DamageSkill[$SkillPiercing]	= $SkillFocus;
$DamageSkill[$SkillArchery]		= $SkillEndurance;
$DamageSkill[$SkillWands]		= $SkillWillpower;
$DamageSkill[$SkillMining]		= $SkillEnergy;
if($PVPEnabled=="")				$PVPEnabled = True;

//__________________________________________________________________________________________________________________________
// "The Beast"
function Player::onDamage(%this,%type,%value,%pos,%vec,%mom,%vertPos,%rweapon,%object,%weapon,%isMiss) {
	%damagedClient = Player::getClient(%this);
	%shooterClient = %object;

	//_______________________________________
	// Early return for total immunity
	if(!rpg::IsTheWorldEnding()) {
		if(%shooterClient == 0 || !Player::isExposed(%this) || %type == $NullDamageType || IsDead(%damagedClient) || fetchData(%damagedClient, "EqualizerLaunch") > 0) {
			return;
		} else if(fetchData(%damagedClient,"Swooping") > 0) {
			return;
		} else if(%damagedClient != %shooterClient) {		
			if( !Player::isAIcontrolled(%damagedClient) && 
				!Player::isAIcontrolled(%shooterClient) && 
				getObjectType(%shooterClient) == "Player") {
				if(!$PVPEnabled || fetchData(%damagedClient,"PVP") != 1)
					return;
			} else if(	Player::isAIcontrolled(%damagedClient) && 
						Player::isAIcontrolled(%shooterClient) && 
						GameBase::getTeam(%damagedClient) == GameBase::getTeam(%shooterClient)) {
				return;
			}
		}
	} else if(rpg::IsPlayerSavedFromTheEnd(%damagedClient) || Zone::getDesc(fetchData(%damagedClient,"zone")) == "Hall of Souls") {
		if(%shooterClient > 0)
			Client::sendMessage(%shooterClient, $MsgRed, "You cannot damage players who have been saved!");
		return;
	}
	
	%damagedClientPos 	= GameBase::getPosition(%damagedClient);
	%shooterClientPos 	= GameBase::getPosition(%shooterClient);
	if(%type == $WordsmithDamageType) {
		%skilltype 		= $SkillWordsmith;
		%type 			= $SpellDamageType;
	}
	else %skilltype 	= $SkillType[%weapon];	
	
	//_______________________________________
	// Leech type always hits
	if($DamageType[%weapon] == $LeechDamageType) {		
		if(%shooterClient == %damagedClient) 
			return;
		%type 		= $LeechDamageType;
		%isMiss 	= false;
		%value 		= GetRoll(GetSkillWithBonus(%shooterClient,%skilltype)/2*getRandom());
	}
	
	//_______________________________________
	// Never hit in PROTECTED or same-House
	if(!rpg::IsTheWorldEnding() && %shooterClient != %damagedClient && !Player::isAIcontrolled(%damagedClient)) {
		if(fetchData(%damagedClient,"STOLEN") <= 0)	{			
			if(Zone::getType(fetchData(%damagedClient, "zone")) == "PROTECTED") {
				if (%shooterClient != %damagedClient) {
					%value = 0;
					%isMiss = true;					
				}
			}			
			%house = GetHouseNumber(fetchData(%damagedClient, "MyHouse"));
			if(%house > 0 && %house == GetHouseNumber(fetchData(%shooterClient, "MyHouse"))) {
				if( !(IsInCommaList(fetchData(%damagedClient, "targetlist"), Client::getName(%shooterClient)) || 
				  IsInCommaList(fetchData(%shooterClient, "targetlist"), Client::getName(%damagedClient))) ) {
					%value = 0;
					%isMiss = true;
				}
			}
		}
	}
	
	//_______________________________________
	// Limit arena damage to arena fighters
	if((IsStillArenaFighting(%damagedClient) != IsStillArenaFighting(%shooterClient)) ||
		(IsInRoster(%damagedClient) != IsInRoster(%shooterClient)) || 
		IsInRoster(%damagedClient)) {	
		if(IsStillArenaFighting(%shooterClient))CheckAndBootFromArena(%shooterClient);
		if(IsStillArenaFighting(%damagedClient))CheckAndBootFromArena(%damagedClient);
		%value = 0;						
		%isMiss = true;
	}
		
	//_______________________________________
	// Landing damage modifiers
	if(%type == $LandingDamageType) {
		%results 	= rpg::GenerateLandingDamage(%damagedClient, %value);
		%value 		= Array::Get(%results,0);
		%noimpulse 	= Array::Get(%results,1);
		Array::Delete(%results);
	}
	//_______________________________________
	// Spells - apply resistance
	else if(%type == $SpellDamageType || %type == $WandDamageType) {		
		if(%weapon == "thorns") {
			%resistance		= Cap(1.0 - (fetchData(%damagedClient, "MDEF") / 1000), 0, 1.0);
			%value 			= Cap(%value * %resistance, 0, "inf");
			%isMiss 		= True;
		} else if(%type == $WandDamageType) {			
			%value 			= round(((%value / 500) * GetSkillWithBonus(%shooterClient, %skilltype)));	
			%resistance		= (getRandom() * (fetchData(%damagedClient, "MDEF") / 10));
			%value 			= Cap(%value - %resistance, 0, "inf");
		} else {			
			%value 			= round(((%value / 750) * GetSkillWithBonus(%shooterClient, %skilltype)));	
			%resistance		= (getRandom() * (fetchData(%damagedClient, "MDEF") / 10));			
			%value 			= Cap(%value - %resistance, 0, "inf");
		}
	}	
	//_______________________________________
	// All other weapons generate damage values
	else {
		%charmedBy 	= AddBonusStatePoints(%damagedClient, "CHARM");
		%charming 	= AddBonusStatePoints(%shooterClient, "CHARM");
		if(!rpg::IsTheWorldEnding() && %charmedBy == %shooterClient && %charming == %damagedClient && %shooterClient != %damagedClient) {
			newprintmsg(%shooterClient, "You can't bring yourself to hurt <f1>" @ rpg::getname(%damagedClient) @ "<ff> while <f2>charmed<f0>", $MsgBeige);
			newprintmsg(%damagedClient, rpg::getname(%shooterClient) @ "<ff> tries to lash out, but your <f2>charm<ff> stays their hand.", $MsgBeige);
			return;
		}
		if(%isMiss != true && %type != $DrainDamageType) {
			%results 	= rpg::DoWeaponImpact(%damagedClient, %shooterClient, %type, %weapon, %rweapon, %value);		
			%maxDamage	= Array::Get(%results, 0);
			%value 		= Array::Get(%results, 1);
			%isMiss 	= Array::Get(%results, 2);
			%Backstab 	= Array::Get(%results, 3);
			%Defended 	= Array::Get(%results, 4);
			%Bash 		= Array::Get(%results, 5);
			%Parry 		= Array::Get(%results, 6);		
			Array::Delete(%results);
		}
		if(%Parry || %Defended) {
			%isMiss = true;
		}
	}
	
	//_______________________________________
	// If there's no forced hit one way or the other, TryEvadeDefend
	if(%isMiss == "")
		%isMiss = rpg::TryEvadeDefend(%damagedClient, %shooterClient, %damageType, %weapon);
	
	//_______________________________________
	// Reduce all self-Spell damage to 1/3
	if(%damagedClient == %shooterClient && isOneOf(%type,$WandDamageType,$SpellDamageType))
		%value = %value / 3;
	
	//_______________________________________
	// Nullify damage to admins
	if(%damagedClient.adminLevel >= 5)
		%value = 0;
	
	//_______________________________________
	// Sort out the attacker's name
	if(%type == $CrushDamageType)					%hitby = "moving object";
	else if(%type == $DebrisDamageType)				%hitby = "debris";
	else if(%shooterClient == %damagedClient)		%hitby = "yourself";
	else if(%shooterClient == 0)					%hitby = "an NPC";
	else if(fetchData(%shooterClient, "invisible"))	%hitby = "an unknown assailant";
	else 											%hitby = rpg::getName(%shooterClient);

	%base1 = Cap((fetchData(%damagedClient, "LVL")+1) / (fetchData(%shooterClient, "LVL")+1), 0.1, 3);
	%base2 = Cap((fetchData(%shooterClient, "LVL")+1) / (fetchData(%damagedClient, "LVL")+1), 0.1, 3);

	//_______________________________________
	// Damage was dealt
	if(!%isMiss && %value > 0) {
		%shooterMsg = " damaged";
		%damagedMsg = "were damaged by";
		storeData(%damagedClient, "tmpkillerid", %shooterClient);
		
		if(%type != $DrainDamageType && %shooterClient != %damagedClient) {
			%damageskill = $DamageSkill[$SkillType[%weapon]];
			if(%damageskill == "")
				%damageskill = %skilltype;		
			if(%skilltype != %damageskill) {
				UseSkill(%shooterClient, %damageskill, True, True, %base1);		// Train the governing "DamageSkill" on hits
			}
			UseSkill(%shooterClient, %skilltype, True, True, %base1);
			if(%type == $SpellDamageType)	UseSkill(%damagedClient, $SkillWillpower, True, True, %base2);
			else 							UseSkill(%damagedClient, $SkillEndurance, True, True, %base2);
		}
		
		//_______________________________________
		// Bashing impulse
		if(%Bash) {
			%minSkill			= (1000-Cap(GetSkillWithBonus(%shooterClient, $SkillBludgeoning),0,1000))/1000;
			%playerSkill 		= GetSkillWithBonus(%shooterClient, $SkillBludgeoning) / Cap(1000-GetWord($SkillRestriction[%weapon], 1), 1, "inf");
			%weaponBaseBash		= Cap(fetchData(%shooterClient, "ATK"), 50 * %minSkill, 100);

			%mom = Vector::getFromRot(GameBase::getRotation(%shooterClient), %playerSkill * 40, %weaponBaseBash);
			UseSkill(%shooterClient, $SkillBludgeoning, True, True);
			%shooterMsg = " bashed";
			%damagedMsg = "were bashed by";
		}
		
		//_______________________________________
		// Backstab after-effects
		if(%Backstab) {
			UseSkill(%shooterClient, $SkillPiercing, True, True);
			%shooterMsg = " backstabbed";
			%damagedMsg = "were backstabbed by";
		} 
				
		//_______________________________________
		// Thorns and Reflect		
		if(%weapon != "thorns" && %weapon != "reflect") {
			%thornReflectionPercent = 0;
			if(%skilltype != $SkillArchery && GetRange(%weapon) <= 8) {
				%thornReflectionPercent = AddBonusStatePoints(%damagedClient, "THORN") / 100.0;
				%sendWeapon = "thorns";
			} else {
				%thornReflectionPercent = AddBonusStatePoints(%damagedClient, "REFLECT") / 100.0;
				%sendWeapon = "reflect";
			}
			if(%thornReflectionPercent > 0) {
				%reflectDmg = floor(%value * %thornReflectionPercent)+1;
				if(%reflectDmg > 0)
					schedule( "GameBase::virtual(" @ %shooterClient @ ", \"onDamage\", " @ $DrainDamageType @ ", " @ %reflectDmg @ 
							  ", \"0 0 0\", \"0 0 0\", \"0 0 0\", \"torso\", \"front_right\", " @ %damagedClient @ ", \"" @ %sendWeapon @ "\");",
							  1, %damagedClient);
			}
		}
		
		//_______________________________________
		// #bleed skill
		if(%weapon == "#bleed") {
			%shooterMsg = " bled";
			%damagedMsg = "were bled by";
		}
		//_______________________________________
		// Poison application			
		else if(String::findSubStr(%weapon,"Poison") == 0 && %type != $DrainDamageType) {							
			%existingPoison 	= GetExactBonusValue(%damagedClient,"POISON " @ %shooterClient);
			%remainingTicks 	= GetExactBonusTicks(%damagedClient,"POISON " @ %shooterClient);
			%remainingPoison	= (%existingPoison * %remainingTicks);
			// Poison can never do more damage than the weapon itself did. It will 
			%newTicks 			= 4; // Force poison damage to play out over 4 ticks.
			%poisonDamage	 	= Cap(GetWord(GetAccessoryVar(%weapon, $SpecialVar), 1) * %newTicks, 0, %value);
			%poisonDamage		*= (%value+1) / (%maxDamage+1);			// scale by expected vs. actual damage
			%poisonDamage 		+= %remainingPoison;					// add back the poison that was previous applied
			UpdateBonusState(%damagedClient, "POISON " @ %shooterClient, %newTicks, %poisonDamage / %newTicks);
			%shooterMsg = " poisoned";
			%damagedMsg = "were poisoned by";
		}
		//_______________________________________
		// Leech dagger
		else if(%weapon == "#leech" && %shooterClient != %damagedClient) {	
			refreshHP(%shooterClient, %value * -1);
			UseSkill(%shooterClient, $SkillThievery, True, True);
			%shooterMsg = " leech health from";
			%damagedMsg = "were drained by";			
			if(!IsDead(%damagedClient)) Client::sprayBlood(%damagedClient, 20, "0 0 0.5", 8, 5, 8);
			if(!IsDead(%shooterClient)) Client::sprayBlood(%shooterClient, 10, "0 0 2", 3, 3, 8);
		}
		
		//_______________________________________
		// Damage from APPLIED poison, not a weapon
		else if(%weapon == "poison" && %type == $DrainDamageType) {
			%shooterMsg = "r poison";
			%damagedMsg = "suffer poison from";
		}
		
		//_______________________________________
		// Apply visual and movement effects
		if(%noimpulse != true)
			Player::applyImpulse(%this,%mom);
		%flash = %value / fetchData(%damagedClient,"HP");
		%flash += 0.05;
		if (%flash > 1) %flash = 1;
		Player::SetDamageFlash(%this,%flash);
		Client::sprayBlood(%damagedClient, floor(%flash*10), "0 0 1", 5, 5, 8);		
		if(Player::isAiControlled(%damagedClient)) {
			PlaySound(RandomRaceSound(fetchData(%damagedClient, "RACE"), Hit), %damagedClientPos);
			if(%flash * 100 * getRandom() > 8) {
				if(AI::getTarget(fetchData(%damagedClient, "BotInfoAiName")) != %shooterClient)
					AI::SelectMovement(fetchData(%damagedClient, "BotInfoAiName"));
			}
		}
		
		//_______________________________________
		// DO THE FSCKING DAMAGE ALREADY, EH? Nah. Just do a test run. Damage is done later.
		%rhp = rpg::DoesHPRefreshCauseDeath(%damagedClient, (%value / $TribesDamageToNumericDamage));
		
		//_______________________________________
		//There was a FAVOR miss
		if(%rhp == -1) {
			%value = -1;	
			newprintmsg(%shooterClient, "You try to hit <f1>" @ rpg::getname(%damagedClient) @ "<ff>, but miss! (FAVOR)", $MsgRed);
			newprintmsg(%damagedClient, "<f1>" @ %hitby @ "<ff> tries to hit you, but misses! (FAVOR)", $MsgRed);
		}
		
		//_______________________________________
		// No FAVOR was used. Print messages and play hit sounds
		else {
			if(%skilltype == $SkillArchery) {
				PlaySound(SoundArrowHit, %damagedClientPos);
			} else if($DamageSkill[%skilltype] != "") {
				%ahs = $ArmorHitSound[GetCurrentlyWearingArmor(%damagedClient)];
				if(%ahs == "") %ahs = SoundHitFlesh;
				PlaySound(%ahs, %damagedClientPos);			
			}
			
			%formatDamagePrint = floor(%value);
			if(%formatDamagePrint > 0) {
				if(%shooterClient != %damagedClient)
					newprintmsg(%shooterClient, "You" @ %shooterMsg @ " <f1>" @ rpg::getname(%damagedClient) @ "<ff> - <f2>" @ %formatDamagePrint @ "<f0> points", $MsgRed);
				newprintmsg(%damagedClient, "You " @ %damagedMsg @ " <f1>" @ %hitby @ "<ff> - <f2>" @ %formatDamagePrint @ "<ff> points", $MsgRed);
				if(%shooterClient == %damagedClient) {
					%names[1] = "itself";
					%names[2] = "himself";
					%names[3] = "herself";
					%hitby = %names[$GenderForRace[fetchData(%damagedClient,"RACE")]];
					radiusAllExcept(%damagedClient, %shooterClient, "<f1>" @ rpg::getname(%damagedClient) @ "<ff> hits " @ %hitby @ " for <f2>" @ %formatDamagePrint @ "<ff> points of damage!", true);
				} else {
					radiusAllExcept(%damagedClient, %shooterClient, "<f1>" @ %hitby @ "<ff> hits " @ rpg::getname(%damagedClient) @ " for <f2>" @ %formatDamagePrint @ "<ff> points of damage!", true);
				}
			}
		}		
		
		//_______________________________________
		// Register the hit for EXP distribution
		if(%shooterClient != 0) {
			%sname = Client::getName(%shooterClient);
			%dname = Client::getName(%damagedClient);
			if(%shooterClient != %damagedClient) {
				%index = "";
				for(%i = 1; %i <= $maxDamagedBy; %i++) {
					if($damagedBy[%dname, %i] == "" && %index == "")
						%index = %i;
				}
				if(%index != "") {
					$damagedBy[%dname, %index] = %sname @ " " @ %value;
					schedule("$damagedBy[\"" @ %dname @ "\", " @ %index @ "] = \"\";", $damagedByEraseDelay);
				}
			}
		}
		
		//_______________________________________
		// Handle death - call onKilled() and play animations
		if(%rhp == 0 || %rhp == 1) {
			if(Player::isAiControlled(%shooterClient)) {
				Player::setAnimation(%shooterClient, 43);
				PlaySound(RandomRaceSound(fetchData(%shooterClient, "RACE"), Taunt), %shooterClientPos);
			}
			
			if( Player::isCrouching(%this) )	%curDie = $PlayerAnim::Crouching;
			else								%curDie = radnomItems(3, $PlayerAnim::DieLeftSide, $PlayerAnim::DieChest, $PlayerAnim::DieForwardKneel);
			Player::setAnimation(%damagedClient, %curDie);
			if(%type == $ImpactDamageType && %object.clLastMount != "")
				%shooterClient = %object.clLastMount;

			%rhp = refreshHP(%damagedClient, (%value / $TribesDamageToNumericDamage));
			Game::clientKilled(%clientId, %shooterClient);
		}
		
		
		//___________________
		// Finally, actually do the damage.
		if(%rhp != 0) {
			%rhp = refreshHP(%damagedClient, (%value / $TribesDamageToNumericDamage));
		}
		return;
	}
	
	//_______________________________________
	// Either a miss | OR | no damage done
	if(%type != $DrainDamageType) {
		if (%shooterClient != %damagedClient) {		
			if (%Defended)	UseSkill(%damagedClient, $SkillSurvival, True, True);
			if (%Parry) 	UseSkill(%damagedClient, $SkillSlashing, True, True);

			UseSkill(%shooterClient, %skilltype, False, True);
			
			if(%type == $SpellDamageType || %type == $WandDamageType) {
				if(%value > 0)		UseSkill(%damagedClient, $SkillWillpower, True, True, %base2);
				else 				UseSkill(%damagedClient, $SkillEvasion, True, True, %base2);	

				if(%weapon == "thorns")		%spelltext = "thorns";
				if(%weapon == "reflect")	%spelltext = "damage reflection";
				else 						%spelltext = "spell";
				if(Zone::getType(fetchData(%damagedClient, "zone")) == "PROTECTED" && !Player::isAIcontrolled(%damagedClient)) {
					newprintmsg(%shooterClient,"<f1>" @ rpg::getname(%damagedClient) @ "<ff> is in PROTECTED territory.", $MsgRed);
					newprintmsg(%damagedClient,"<f1>" @ %hitby @ "<ff> can't harm you in PROTECTED territory.", $MsgWhite);
				} else {
					newprintmsg(%shooterClient, "<f1>"@rpg::getname(%damagedClient) @ "<ff> resists your " @ %spelltext @ "!", $MsgRed);
					newprintmsg(%damagedClient, "You resist <f1>" @ %hitby @ "<ff>'s " @ %spelltext @ "!", $MsgWhite);
				}
			} else {
				if(%value > 0) 		UseSkill(%damagedClient, $SkillEndurance, True, True, %base2);
				else 				UseSkill(%damagedClient, $SkillEvasion, True, True, %base2);
				
				if(%Defended || %Parry) {
					if(%Defended) {
						newprintmsg(%damagedClient, "You defended against <f1>" @ rpg::getname(%shooterClient) @ "<ff>'s attack!", $MsgGreen);
						newprintmsg(%shooterClient, "<f1>" @ Client::getName(%damagedClient) @ "<ff> blocks your attack!", $MsgRed);
					}	
					if(%Parry) {
						newprintmsg(%shooterClient, "<f1>" @ rpg::getname(%damagedClient) @ "<f2> parries <ff>your attack!", $MsgRed);			
						newprintmsg(%damagedClient,"You <f2>parry <f1>" @ %hitby @ "<ff>'s attack!", $MsgWhite);
					}
				}
				else {
					if(Zone::getType(fetchData(%damagedClient, "zone")) == "PROTECTED" && !Player::isAIcontrolled(%damagedClient)) {
						newprintmsg(%shooterClient,"<f1>" @ rpg::getname(%damagedClient) @ "<ff> is in PROTECTED territory.", $MsgRed);
						newprintmsg(%damagedClient,"<f1>" @ %hitby @ "<ff> can't harm you in PROTECTED territory.", $MsgWhite);
					} else {
						newprintmsg(%shooterClient,"You try to hit <f1>" @ rpg::getname(%damagedClient) @ "<ff>, but miss!", $MsgRed);
						newprintmsg(%damagedClient,"<f1>" @ %hitby @ "<ff> tries to hit you, but misses!", $MsgWhite);
					}
				}
			}
		}
		
		if(fetchData(%damagedClient, "isBonused")) {
			GameBase::activateShield(%this, "0 0 1.57", 1.47);
			PlaySound(SoundHitShield, %damagedClientPos);
		}
	}
}



//__________________________________________________________________________________________________________________________
// On landing from a #leap, damage everyone around except the attacker
function rpg::RadialLeapDamage(%object, %attackerId, %pos, %extra) {
	%targetId = Player::getClient(%object);
	
	if(%attackerId == %targetId) {
		//Player::applyImpulse(%targetId, "0 0 " @ -%dmg);
	}
	else if(GameBase::getTeam(%attackerId) != GameBase::getTeam(%targetId)){
		%radius 	= GetWord(%extra,0);
		%dmg 		= GetWord(%extra,1);
		%weapon 	= GetWord(%extra,2);				
		%dist 		= Vector::getDistance(GameBase::getPosition(%targetId), GameBase::getPosition(%attackerId));	
		%dmg 		= rpg::GetSplashDamage(%dist, %radius, %dmg, 10, 100);
		if(%dmg >= 1) {
			Player::applyImpulse(%targetId, "0 0 " @ %dmg/5);
			GameBase::virtual(%targetId, "onDamage", "", %dmg, "0 0 0", "0 0 0", "0 0 0", "torso", "front_right", %attackerId, %weapon);
		}
	}
}

//________________________________________________________________________________________________
function rpg::GetSplashDamage(%dist, %radius, %dmg, %percMin, %percMax) {
	if(%radius <= 0) 	return 0;
	if(%dist > %radius) %dist = %radius;
	return Cap(%dmg * ((%radius - %dist)/%radius), (%dmg * (%percMin/100)), (%dmg * (%percMax/100)));
}

//__________________________________________________________________________________________________________________________
// Calculate evasion, defense, resistance and roll against the attacker's weapon skill to determine if a hit occurs
function rpg::TryEvadeDefend(%damagedClient, %shooterClient, %damageType, %weapon) {
	%skilltype = $SkillType[%weapon];
	
	if(((%damageType != $LandingDamageType && %shooterClient != %damagedClient) || (%damageType == $WandDamageType)) && %shooterClient != 0) {
		if(%damageType == $SpellDamageType || %damageType == $WandDamageType)
			%defenseValue = (fetchData(%damagedClient, "MDEF") / 5) + (GetSkillWithBonus(%damagedClient, $SkillWillpower) / 3);
		else %defenseValue = (fetchData(%damagedClient, "DEF") / 5) + (GetSkillWithBonus(%damagedClient, $SkillEvasion) / 3);
		%attackValue = GetSkillWithBonus(%shooterClient, %skilltype) + 5;

		%attackValue 	= floor(Cap(getRandom() * %attackValue), %attackValue * 0.1, "inf");
		%defenseValue 	= floor(Cap(getRandom() * %defenseValue), %defenseValue * 0.1, "inf");

		if(%attackValue < %defenseValue)
			return true;
	}
	return false;
}



//__________________________________________________________________________________________________________________________
// Assuming %damage resulted from landing, check buffs and return the actual damage that should be applied
//		Returns array [0] = damage   [1] = bool noimpulse
function rpg::GenerateLandingDamage(%damagedClient, %damage) {
	%results = Array::New();
	Array::Insert(%results, 0, 0, 1);
	
	%damage *= 125;
	//___________________
	// #leap | charge
	%leaping = fetchData(%damagedClient, "Leaping");	
	if(%leaping > 1) {
		Array::Insert(%results, 1, 1);
		return %results;
	}
	
	%charging = fetchData(%damagedClient, "Charging");
	if(%leaping || %charging) {
		%atkVal = Cap(GetSkillWithBonus(%damagedClient,$SkillType[%weapon]) * getRandom(), fetchData(%damagedClient, "ATK"), "inf");
		%atkVal += %damage;
		%pos 	= GameBase::getPosition(%damagedClient);		
		
		if(%charging > 0) {
			storeData(%damagedClient, "Charging", "");
			%weapon = "charge";	
			%radius = 6.66;			
			%set = newObject("set", SimSet);
			%n = containerBoxFillSet(%set, $SimPlayerObjectType, %pos, %radius, %radius, %radius, 0);			
			Group::iterateRecursive(%set, rpg::RadialLeapDamage, %damagedClient, %pos, %radius @ " " @ %atkVal @ " " @ %weapon);
			deleteObject(%set);
		} 
		if(%leaping > 0) {
			storeData(%damagedClient, "Leaping", "");
			%weapon = "#leap";
			%radius = Cap(%damage / 0.04, 6, 16);
			if(%radius >= 10) 	CreateAndDetBomb(%damagedClient, LargeDustPlumeFX, NewVector(vecx(%pos),vecy(%pos),vecz(%pos)+1), false);
			else				CreateAndDetBomb(%damagedClient, DustPlumeFX, NewVector(vecx(%pos),vecy(%pos),vecz(%pos)+1), false);
			%set = newObject("set", SimSet);
			%n = containerBoxFillSet(%set, $SimPlayerObjectType, %pos, %radius, %radius, %radius, 0);			
			Group::iterateRecursive(%set, rpg::RadialLeapDamage, %damagedClient, %pos, %radius @ " " @ %atkVal @ " " @ %weapon);
			deleteObject(%set);
		}
		UseSkill(%damagedClient, $SkillSurvival, (%n > 1), True);
		Array::Insert(%results, 1, 1);
		return %results;
	}
	
	//___________________
	// #acrobatics
	if (AddBonusStatePoints(%damagedClient,"ACRO") > 0) {
		Player::setAnimation(%damagedClient,7);
		return %results;
	}
	//___________________
	// Equalizer-holder
	else if (rpg::PlayerIsEqualizer(%damagedClient)) {
		return %results;
	}
	//___________________
	// SlayerGear
	else if (rpg::GetHouseLevel(%damagedClient) >= 100 && Player::getItemCount(%damagedClient,SlayerGear0)) {
		if(fetchData(%damagedClient, "SlayerSuicideLand") > 0) {
			storeData(%damagedClient, "SlayerSuicideLand", "");
			Client::sendMessage(%damagedClient, $MsgGreen, "You discover that your maximum rank Slayer Gear has imapact compensation! You won't suffer any collision damage!");
		}
		return %results;
	}
	
	//___________________
	// Water damage damp	
	%object = "";
	for(%i = 0; %i >= -3.15; %i -= 1.57) {
		if(GameBase::getLOSInfo(Client::getOwnedObject(%damagedClient), 5, %i @ " 0 0")) {
			if(getObjectType($los::object) == "StaticShape" && String::getSubStr(Object::getName($los::object), 0, 5) == "water") {
				%object = $los::object;
				break;
			}
		}
	}
	if(%object != "") {
		%damage *= $waterDamageAmp;
		playSound(SoundSplash1, %damagedClientPos);
	}
	if(Zone::getType(fetchData(%damagedClient, "zone")) == "WATER")
		%damage *= $waterDamageAmp;
	
	Array::Insert(%results, %damage, 0);
	return %results;
}



//__________________________________________________________________________________________________________________________
// Perform a weapon impact test.
//		Return an array containing damage values and all of the impact effects that were applied
function rpg::DoWeaponImpact(%damagedClient, %shooterClient, %damageType, %weapon, %rweapon, %damage) {
	%result = Array::New();
	
	if(%damageType == $LandingDamageType || %damageType == $DrainDamageType) {
		Array::Insert(%result, 0, 0, 6);	// Landing damage isn't a weapon. Drain damage does it's own thing.
		return %result;						// Return all values/states as false
	}
	
	%isMiss 	= false;
	%Backstab 	= false;
	%Defended 	= false;	
	%Bash		= false;
	%Parry		= false;	
	%multiplier	= 1;
	%skilltype 	= $SkillType[%weapon];
	
	//___________________
	// Backstab
	%dRot = GetWord(GameBase::getRotation(%damagedClient), 2);
	%sRot = GetWord(GameBase::getRotation(%shooterClient), 2);
	%diff = %dRot - %sRot;	
	if(%diff >= -0.9 && %diff <= 0.9) {
		if(%skilltype == $SkillPiercing) {
			%multiplier += GetSkillWithBonus(%shooterClient, $SkillPiercing) / 300;
			%Backstab 	= true;
		}
	}	
	//___________________
	// #brace (not behind)
	else if(AddBonusStatePoints(%damagedClient,"BRACE") > 0) {
		UpdateBonusState(%damagedClient, "BRACE 1", 0, 0, true);
		UpdateBonusState(%damagedClient, "BRACE 2", 0, 0, true);
		playSound(SoundSwordClang, GameBase::getPosition(%damagedClient));
		%damage		= 0;
		%Defended 	= true;
		%isMiss 	= true;
	}
	
	//___________________
	// Bash
	if(%skilltype == $SkillBludgeoning) {
		%time = getIntegerTime(true) >> 5;
		if(fetchData(%shooterClient, "NextHitBash") < %time) {
			%Bash		= true;
			%multiplier += GetSkillWithBonus(%shooterClient, $SkillBludgeoning) / 500;
		}
		storeData(%shooterClient, "NextHitBash", %time + (4000 / GetSkillWithBonus(%shooterClient, $SkillBludgeoning)));
	}	
	//___________________
	// Parry
	else if(%skilltype != $SkillWands) {			
		%defenderWeapon = Player::getMountedItem(%damagedClient, $WeaponSlot);
		if(%defenderWeapon != -1 && $SkillType[%defenderWeapon] == $SkillSlashing) {		
			%parryVal 		= floor(getRandom() * (GetSkillWithBonus(%damagedClient, $SkillSlashing) / 3));
			%parryAgainst 	= floor(getRandom() * (GetSkillWithBonus(%shooterClient, %skilltype) + 5));
			if(%parryVal > %parryAgainst) {
				%isMiss = true;
				%Parry = true;
			}
		}
	}
	
	//___________________
	// Unhide
	%invisibleShooter = fetchData(%shooterClient, "invisible");
	if(%invisibleShooter) {
		if(%invisibleShooter == 2) {
			if(%damageType == $WandDamageType) {
				Client::sendMessage(%clientId, $MsgRed, "The blast from your wand forces you out of Shadow Walk!");
				UnHide(%shooterClient);
			}
		} else UnHide(%shooterClient);
	}
	if(fetchData(%damagedClient, "invisible") && %damagedClient.adminLevel < 5) {
		UnHide(%damagedClient);
	}

	%damageRange = rpg::GenerateWeaponDamage(%damagedClient, %shooterClient, %damageType, %weapon, %rweapon, %damage, %multiplier);

	Array::Insert(%result, Array::Get(%damageRange,0), 	0);
	Array::Insert(%result, Array::Get(%damageRange,1), 	1);
	Array::Insert(%result, %isMiss, 					2);
	Array::Insert(%result, %Backstab, 					3);
	Array::Insert(%result, %Defended, 					4);
	Array::Insert(%result, %Bash, 						5);
	Array::Insert(%result, %Parry, 						6);
	Array::Delete(%damageRange);
	return %result;
}



//__________________________________________________________________________________________________________________________
// Generate a pair of weapon damage values.
//		Return 	[0] the maximum damage we could have done, and 
//				[1] the damage we actually did
function rpg::GenerateWeaponDamage(%damagedClient, %shooterClient, %damageType, %weapon, %rweapon, %damage, %multiplier) {
	%skilltype = $SkillType[%weapon];
	
	%weapondamage = %damage;
	if(%damageType != $WandDamageType && %damageType != $LeechDamageType) {
		%rweapondamage = 0;
		if(%weapondamage == "") {
			if(%rweapon != "")
				%rweapondamage = GetRoll(GetWord(GetAccessoryVar(%rweapon, $SpecialVar), 1));			
			%weapondamage = GetRoll(GetWord(GetAccessoryVar(%weapon, $SpecialVar), 1));
		}
	}
	
	%damageskill 	= $DamageSkill[$SkillType[%weapon]];
	if(%damageskill == "")
		%damageskill = $SkillType[%weapon];	
	%damage 		= round((( (%weapondamage + %rweapondamage) / 1000) * GetSkillWithBonus(%shooterClient, %damageskill)) * %multiplier);
	
	%damageRange = Array::New();
	Array::Insert(%damageRange, %damage, 0);
	
	%ab 		= (getRandom() * (fetchData(%damagedClient, "DEF") / 10)) + 1;
	%damage		= Cap(%damage - %ab, 1, "inf");
	if(%skilltype == $SkillWands && %damagedClient == %shooterClient)
		%damage *= 0.25;

	%a = (%damage * 0.15);
	%r = round((getRandom() * (%a*2)) - %a);
	%damage += %r;
	if(%damage < 1)
		%damage = 1;
	Array::Insert(%damageRange, %damage, 1);
	return %damageRange;
}
