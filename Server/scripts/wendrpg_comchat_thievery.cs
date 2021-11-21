//________________________________________________________________________________________________________________________________________________________________
// THIEVERY SKILLS
//
// DescX Notes:
//		I could never stand how the original Thief class in this game was just pointless. I decided to make it a first class citizen :).
//		#swoop will give you iframes until it ends, and makes up for low health later on
//		#steal works any time, and AI have an infinite amount to steal
//		#mug PULLS the target toward you while stealing
//		#shadowwalk lets you be invisible for as long as you want, but you can't hurt anything or use skills until you stop
//		#spy is the old admin command #eyes, except you can use it on anything or anyone as a normal skill
//		#bleed stabs a target short range and applies the equivalent of uninterruptable poison damage
//		#knockout seems cheap, but you can immediately #wake from it if prepared
//		#leech chucks a dagger that steals health
//________________________________________________________________________________________________________________________________________________________________
$SkillType["#steal"] 		= $SkillThievery;			// Working
$SkillType["#swoop"] 		= $SkillThievery;			// Working
$SkillType["#blind"] 		= $SkillThievery;			// Working		but has no special impact on AI... hmm hmm... what 2 do...
$SkillType["#mug"] 			= $SkillThievery;			// Working
$SkillType["#hide"] 		= $SkillThievery;			// Working
$SkillType["#bleed"] 		= $SkillThievery;			// Working
$SkillType["#pickpocket"] 	= $SkillThievery;			// Working		
$SkillType["#shadowwalk"] 	= $SkillThievery;			// Working
$SkillType["#leech"] 		= $SkillThievery;			// Working		
$SkillType["#knockout"] 	= $SkillThievery;			// Working		but, balance... and, my code... is shit...
$SkillType["#spy"] 			= $SkillThievery;			// Working

$AccessoryVar["#steal", $MiscInfo] = "Steal a small amount of coins from the target, but only if they aren't facing you.";
$AccessoryVar["#swoop", $MiscInfo] = "Dodge blows with fast reflexes.";
$AccessoryVar["#blind", $MiscInfo] = "Throw crystal dust at the target's eyes, blinding them for a short time.";
$AccessoryVar["#mug", $MiscInfo] = "Pulls the target towards you and shakes them down for coins.";
$AccessoryVar["#hide", $MiscInfo] = "Hide in the shadows. Hug walls to stay hidden. Attacking will remove stealth.";
$AccessoryVar["#bleed", $MiscInfo] = "Bleeds the target over time. Two free hands allow for more precise cuts.";
$AccessoryVar["#pickpocket", $MiscInfo] = "Take items from a target. Impacted by stealth.";
$AccessoryVar["#shadowwalk", $MiscInfo] = "Permanent invisibility. While Shadow Walking, attacks and skills cannot be used. Use twice to become visible.";
$AccessoryVar["#leech", $MiscInfo] = "Throws a knife on a chain laced with an alchemical substance that turns blood into wine.";
$AccessoryVar["#knockout", $MiscInfo] = "A concussive blow that deals no damage but puts targets to sleep instantly.";
$AccessoryVar["#spy", $MiscInfo] = "See what they see. Masterful Thieves learn to project their minds as a third eye.";

$SkillList[$SkillThievery] = "#steal #swoop #blind #mug #hide #bleed #pickpocket #shadowwalk #leech #knockout #spy";
$SkillListAICombat[$SkillThievery] = "#steal #swoop #blind #mug #hide #bleed #leech #knockout";

$DamageType["#leech"] = $LeechDamageType;
$AccessoryVar["#leech", $SpecialVar] = "6 200";

//________________________________________________________________________________________________________________________________________________________________
function processThievery(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai) {
	%processed = True;
	%time = getIntegerTime(true) >> 5;
	%lastSkillTime = fetchData(%TrueClientId, "LastThiefSkillTime");
	if(%time - %lastSkillTime < 0) {	// fix.		
		%lastSkillTime = 0;
		storeData(%TrueClientId, "LastThiefSkillTime", %lastSkillTime);
	}
	%timeBetweenSkillUse = %time - %lastSkillTime;
	
	if(%w1 == "#steal") {		
		if(%timeBetweenSkillUse < 3 && !Player::isAiControlled(%TrueClientId)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #steal for another " @ 3 - %timeBetweenSkillUse @ " seconds.");
		} else {
			if(SkillCanUse(%TrueClientId, "#steal")) {
				%TrueClientId.lastStealTime = %time;
				storeData(%TrueClientId, "LastThiefSkillTime",  %time);
				%stealFailureMessage = TrySteal(%trueClientId, 3, Cap($PlayerSkill[%TrueClientId,$SkillThievery] / 500, 0.5, 2), "back");
				if(%stealFailureMessage != "")
					Client::sendMessage(%TrueClientId, $MsgRed, %stealFailureMessage);
			} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #steal because you lack the necessary skills.");
		}
	} 
	else if(%w1 == "#mug") {		
		if(%timeBetweenSkillUse < 8 && !Player::isAiControlled(%TrueClientId)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #mug for another " @ 8 - %timeBetweenSkillUse @ " seconds.");
		} else {
			if(SkillCanUse(%TrueClientId, "#mug")) {
				%TrueClientId.lastStealTime = %time;
				storeData(%TrueClientId, "LastThiefSkillTime",  %time);
				%stealFailureMessage = TrySteal(%trueClientId, 6, Cap($PlayerSkill[%TrueClientId,$SkillThievery] / 500, 2, 5), "pull");
				if(%stealFailureMessage != "") {
					Client::sendMessage(%TrueClientId, $MsgRed, %stealFailureMessage);
				}
			} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #mug because you lack the necessary skills.");
		}
	}
	else if(%w1 == "#pickpocket") {
		if(%timeBetweenSkillUse < 15) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #pickpocket for another " @ 15 - %timeBetweenSkillUse @ " seconds.");
		} else if(!SkillCanUse(%TrueClientId, "#pickpocket")) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't pickpocket because you lack the necessary skills.");
		} else {
			%TrueClientId.lastStealTime = %time;
			storeData(%TrueClientId, "LastThiefSkillTime",  %time);			
			if((%reason = AllowedToSteal(%TrueClientId)) != "True") {
				Client::sendMessage(%TrueClientId, $MsgRed, %reason);
			} else if(GameBase::getLOSinfo(Client::getOwnedObject(%TrueClientId), 3) && getObjectType($los::object) == "Player") {
				%TrueClientId.stealType = 1;
				SetupInvSteal(%TrueClientId, Player::getClient($los::object));
			}
		}
	}	
	else if(%w1 == "#shadowwalk") {
		if(SkillCanUse(%TrueClientId, "#shadowwalk") && !Player::isAiControlled(%TrueClientId)) {
			if(!fetchData(%TrueClientId, "invisible") && !fetchData(%TrueClientId, "blockHide")) {
				Client::sendMessage(%TrueClientId, $MsgBeige, "You are successful at Shadow Walk.");
				GameBase::startFadeOut(%TrueClientId);
				storeData(%TrueClientId, "invisible", 2);
				UseSkill(%TrueClientId, $SkillThievery, True, True);
			} else {
				UnHide(%clientId);
				Client::sendMessage(%TrueClientId, $MsgBeige, "You drop out of Shadow Walk.");
			}
			storeData(%TrueClientId, "LastThiefSkillTime",  %time);
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #shadowwalk because you lack the necessary skills.");
	}
	else if(%w1 == "#hide") {
		if(SkillCanUse(%TrueClientId, "#hide")) {
			if(!fetchData(%TrueClientId, "invisible") && !fetchData(%TrueClientId, "blockHide")) {
				%closeEnoughToWall = Cap($PlayerSkill[%TrueClientId, $SkillThievery] / 125, 3.5, 8);
				%pos = GameBase::getPosition(%TrueClientId);
				%closest = 10000;
				for(%i = 0; %i <= 6.283; %i+= 0.52) {
					GameBase::getLOSinfo(Client::getOwnedObject(%TrueClientId), 25, "0 0 " @ %i);
					%dist = Vector::getDistance(%pos, $los::position);
					if(%dist < %closest && $los::position != "0 0 0" && $los::position != "")
						%closest = %dist;
				}

				if(%closest <= %closeEnoughToWall) {
					Client::sendMessage(%TrueClientId, $MsgBeige, "You are successful at Hide In Shadows.");

					GameBase::startFadeOut(%TrueClientId);
					storeData(%TrueClientId, "invisible", True);

					%grace = Cap($PlayerSkill[%TrueClientId, $SkillThievery], 5, 2000);
					WalkSlowInvisLoop(%TrueClientId, 5, %grace);

					UseSkill(%TrueClientId, $SkillThievery, True, True);
				} else {
					Client::sendMessage(%TrueClientId, $MsgRed, "You were unsuccessful at Hide In Shadows.");
					UseSkill(%TrueClientId, $SkillThievery, False, True);
				}
				storeData(%TrueClientId, "LastThiefSkillTime",  %time);
			}
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #hide because you lack the necessary skills.");
	}
	else if(%w1 == "#blind") {		
		if(SkillCanUse(%TrueClientId, "#blind")) {
			if(%timeBetweenSkillUse < 12 && !Player::isAiControlled(%TrueClientId)) {
				Client::sendMessage(%TrueClientId, $MsgRed, "You can't #blind targets for another " @ 12 - %timeBetweenSkillUse @ " seconds.");
			} else {
				storeData(%TrueClientId, "LastThiefSkillTime",  %time);
				%iterations = Cap($PlayerSkill[%TrueClientId,$SkillThievery] / 100, 3, 9);
				if(GameBase::getLOSinfo(Client::getOwnedObject(%TrueClientId), 1) && getObjectType($los::object) == "Player") {
					%id = Player::getClient($los::object);
					%name = "#blind " @ rpg::getName(%id);
					UseSkill(%TrueClientId, $SkillThievery, True, True);
				} else {
					%id = %TrueClientId;
					%name = "miss and #blind yourself";
					%iterations = floor(%iterations / 2);
				}
				Player::setDamageFlash(%id, 1);
				for(%x=1;%x<%iterations;%x++)
					schedule("Player::setDamageFlash(" @ %id @ ", 1);", %x * 1.5);
				Client::sendMessage(%TrueClientId, $MsgGreen, "You " @ %name @ " for " @ (%iterations * 1.5 + 1.5) @ " seconds.");				
			}
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #blind targets because you lack the necessary skills.");
	}
	else if(%w1 == "#knockout") {
		if(SkillCanUse(%TrueClientId, "#knockout")) {
			if(%timeBetweenSkillUse < 15 && !Player::isAiControlled(%TrueClientId)) {
				Client::sendMessage(%TrueClientId, $MsgRed, "You can't #knockout targets for another " @ 15 - %timeBetweenSkillUse @ " seconds.");
			} else {
				%knockoutStrength = Cap($PlayerSkill[%TrueClientId,$SkillThievery] / 75, 3, 8);
				if(GameBase::getLOSinfo(Client::getOwnedObject(%TrueClientId), 10 + (%knockoutStrength * 1.5)) && getObjectType($los::object) == "Player") {
					%id = Player::getClient($los::object);					
					%evadeKnockout = Cap((fetchData(%id,"DEF") / 100) + (GetSkillWithBonus(%id,$SkillEvasion) / 75), 1, 10);
					%cpos = GameBase::getPosition(%TrueClientId);
					%opos = GameBase::getPosition(%id);
					%knockRoll = getRandom() * %knockoutStrength;
					%evadeRoll = getRandom() * %evadeKnockout;
					if (Vector::getDistance(%cpos, %opos) <= %knockoutStrength) {
						Client::sendMessage(%TrueClientId, $MsgWhite, "You are too close to perform a #knockout.");
					} else if (%knockRoll > %evadeRoll) {
						storeData(%TrueClientId, "LastThiefSkillTime",  %time);
						Player::setAnimation(%TrueClientId, 39);
						%vel = Vector::scale(Vector::normalize(Vector::sub(%cpos, %opos)), Cap(%knockoutStrength, 3, 5) * -10);
						Item::setVelocity(%TrueClientId, %vel);
						playSound(SoundBluntSwingLG, %cpos);
						if (%id.sleepMode < 1 || %id.sleepMode == "") {
							%id.sleepMode = 1;
							refreshHPREGEN(%id);
							refreshMANAREGEN(%id);
							Client::setControlObject(%id, Client::getObserverCamera(%id));
							Observer::setOrbitObject(%id, Client::getOwnedObject(%id), 8, 8, 8);
							if(Player::isAiControlled(%id)) {
								storeData(%id, "frozen", True);				
								storeData(%id, "dumbAIflag", true);
								%name = fetchData(%id, "BotInfoAiName");
								AI::setVar(%name, SpotDist, 0);
								AI::newDirectiveRemove(%name, 99);
								Player::setAnimation(%id, 6);
								schedule("PlaySound(RandomRaceSound(fetchData(" @ %id @ ", \"RACE\"), Hit), \"" @ %opos @ "\"); Player::setAnimation(" @ %id @ ", 33);", 0.3);
								schedule("Player::setAnimation(" @ %id @ ", 48);", 1);
								schedule("Player::setAnimation(" @ %id @ ", 48);", 1.2);
								schedule("Player::setAnimation(" @ %id @ ", 48);", 1.5);
								schedule("Player::setAnimation(" @ %id @ ", 48);", 1.7);
							}
							
							Client::sendMessage(%id, $MsgRed, "You were knocked out. Use #wake to shake off the blow and get up.");
							Client::sendMessage(%TrueClientId, $MsgGreen, rpg::getname(%id) @ " passes out after your #knockout blow.");
							
							schedule("KnockoutRecover(" @ %id @ ");", %knockoutStrength);
							UseSkill(%TrueClientId, $SkillThievery, True, True);
						} else Client::sendMessage(%TrueClientId, $MsgBeige, rpg::getname(%id) @ " is already passed out.");
					} else {
						storeData(%TrueClientId, "LastThiefSkillTime",  %time);
						Client::sendMessage(%TrueClientId, $MsgRed, "You try to line up a #knockout, but the opportunity evades you.");
					}
				}
			}
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #knockout targets because you lack the necessary skills.");
	}
	else if(%w1 == "#swoop") {
		if(SkillCanUse(%TrueClientId, "#swoop")) {
			%delay = 3.5 - Cap($PlayerSkill[%TrueClientId,$SkillThievery] / 500, 0, 2);
			if(%timeBetweenSkillUse < %delay && !Player::isAiControlled(%TrueClientId)) {
				Client::sendMessage(%TrueClientId, $MsgRed, "You can't #swoop for another " @ floor(%delay - %timeBetweenSkillUse) @ " seconds.");
			} else {
				%vel = Item::getVelocity(%TrueClientId);
						
				%sumspeed = Vector::getLength(%vel);
				if(%sumspeed == 0) {
					Client::sendMessage(%TrueClientId, $MsgWhite, "You must be moving to use #swoop.");
				} else if(%sumspeed > 200) {
					Client::sendMessage(%TrueClientId, $MsgRed, "You are moving too fast to #swoop!");
				} else {
					Player::setAnimation(%TrueClientId, 15);
					UseSkill(%TrueClientId, $SkillThievery, True, True);
					%multiplier = Cap($PlayerSkill[%TrueClientId,$SkillThievery] / 50, 3, 10);
					%stopTime = Cap(%multiplier / 16 , 0.25, 0.75);
					%newvel = vecx(%vel) * %multiplier @ " " @ vecy(%vel) * %multiplier @ " "; 
					if(vecz(%vel) != 0) 
						%newvel = %newvel @ vecz(%vel) + Cap(10 - %multiplier,5,10);
					else %newvel = %newvel @ vecz(%vel);
					playSound(AbsorbABS, GameBase::getPosition(%TrueClientId));
					Item::setVelocity(%TrueClientId, %newvel);		
					storeData(%TrueClientId,"Swooping", 1);
					schedule("Item::setVelocity(" @ %TrueClientId @ " , \"0 0 0\"); playSound(ricochet1, GameBase::getPosition(" @ %TrueClientId @ "));",
								%stopTime);
					schedule("storeData(" @ %TrueClientId @ " , \"Swooping\", \"\");", %stopTime+0.25);
					storeData(%TrueClientId, "LastThiefSkillTime",  %time);
				}				
			}
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #swoop because you lack the necessary skills.");
	}
	else if(%w1 == "#spy") {
		if(!SkillCanUse(%TrueClientId, "#spy") || Player::isAiControlled(%TrueClientId)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #spy because you lack the necessary skills.");
		} else {		
			%msg = "";
			if (%cropped == "" || (%id = NEWgetClientByName(%cropped)) == -1 ) {
				%msg = "Please specify player name.";
			} else if(%timeBetweenSkillUse < 20) {
				%msg = "You can't #spy for another " @ 20 - %timeBetweenSkillUse @ " seconds.";
			} else if(floor(%id.adminLevel) >= floor(%clientToServerAdminLevel) && %id != %TrueClientId && floor(%id.adminLevel) != 0) {
				%msg = "Could not process command: Target admin clearance level too high.";
				storeData(%TrueClientId, "LastThiefSkillTime",  %time);
			} else if(IsDead(%id)) {
				%msg = rpg::getName(%id) @ " is dead. Their eyes will not help you.";
				storeData(%TrueClientId, "LastThiefSkillTime",  %time);
			}
			
			if(%msg != "") Client::sendMessage(%TrueClientId, $MsgRed, %msg);
			else {
				rpg::RevertPossession(%TrueClientId);
				rpg::Eyes(%TrueClientId, %id);	
				UseSkill(%TrueClientId, $SkillThievery, True, True);				
				storeData(%TrueClientId, "LastThiefSkillTime",  %time);
			} 
		}
	}
	else if(%w1 == "#leech") {
		if(SkillCanUse(%TrueClientId, "#leech")) {
			if(%timeBetweenSkillUse < 8 && !Player::isAiControlled(%TrueClientId)) {
				Client::sendMessage(%TrueClientId, $MsgRed, "You can't throw another #leech knife for " @ 8 - %timeBetweenSkillUse @ " seconds.");
			} else {
				%mana = fetchData(%TrueClientId, "MANA");
				%maxmana = fetchData(%TrueClientId, "MaxMANA");
				if((%mana / %maxmana) > 0.1) {					
					%bomb = newObject("", "Mine", ThieveryLeechKnife);			
					%bomb.owner = %TrueClientId;								// When you spawnProjectile() on AI, they dont send "owner"
					addToSet("MissionCleanup", %bomb);							// to the damage functions, so you have no fucking clue which AI shot you.
					%rot = GameBase::getRotation(%TrueClientId);				// Is there a workaround? I don't give a shit. This is stupid.
					%newrot = NewVector(VecX(%rot) - 0.25, VecY(%rot), VecZ(%rot));
					GameBase::setRotation(%TrueClientId, %newrot);
					GameBase::Throw(%bomb, Client::getOwnedObject(%TrueClientId), 100, false);
					GameBase::setRotation(%TrueClientId, %rot);
					
					$MineTracker[%bomb] = GameBase::getPosition(%bomb);
					$MineExploded[%bomb] = 0;
					$MineTrackerTicks[%bomb] = 1;
					schedule("WandProjectileThinker(" @ %bomb @ ", \"#leech\", \"\", 1, 0, 3 );", 0.1);
					
					refreshMANA(%TrueClientId, fetchData(%TrueClientId, "MANA") * 0.1);
					UseSkill(%TrueClientId, $SkillThievery, True, True);
					storeData(%TrueClientId, "LastThiefSkillTime",  %time);
				} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #leech with less than 10% mana.");
			}
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #leech because you lack the necessary skills.");
	}
	else if(%w1 == "#bleed") {
		if(SkillCanUse(%TrueClientId, "#bleed")) {
			if(%timeBetweenSkillUse < 20 && !Player::isAiControlled(%TrueClientId)) {
				Client::sendMessage(%TrueClientId, $MsgRed, "You can't #bleed targets for another " @ 20 - %timeBetweenSkillUse @ " seconds.");
			} else {
				storeData(%TrueClientId, "LastThiefSkillTime",  %time);
				if(GameBase::getLOSinfo(Client::getOwnedObject(%TrueClientId), 2) && getObjectType($los::object) == "Player") {
					%id = Player::getClient($los::object);					
					if(Client::getTeam(%TrueClientId) != Client::getTeam(%id)) {
						%bleedDamage = Cap($PlayerSkill[%TrueClientId,$SkillThievery] / 66, 8, "inf");
						%ticks = Cap($PlayerSkill[%TrueClientId,$SkillThievery] / 100, 2, 10);					
						UpdateBonusState(%id, "BLEED " @ %TrueClientId, %ticks, %bleedDamage);						
						UpdateBonusState(%id, "DEF -100", %ticks, -100);
						playSound(SoundBleedStab, GameBase::getPosition(%id));
						Client::sendMessage(%TrueClientId, $MsgBeige, "Your target bleeds " @ RoundToFirstDecimal(%bleedDamage) @ " health every two seconds for " @ RoundToFirstDecimal(%ticks * 2) @ " seconds.");
						UseSkill(%TrueClientId, $SkillThievery, True, True);						
						Client::sprayBlood(%id, 5, "0 0 0.5", 4, 100, 0, %ticks * 4, 0.5);
					} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #bleed allies.");
				}
						
			}
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #bleed targets because you lack the necessary skills.");
	}
	else {
		%processed = False;
	}
	
	return %processed;
}

function TrySteal(%TrueClientId, %distance, %multiplier, %mode) {
	%reason = AllowedToSteal(%TrueClientId);
	if(%reason != "True") 
		return %reason;

	if(GameBase::getLOSinfo(Client::getOwnedObject(%TrueClientId), %distance)) {
		if($los::object <= 0 || $los::object == "")
			return "No valid target for theft was found.";
		%id = Player::getClient($los::object);
		if(%id <= 0 || %id == "")
			return "No valid target for theft was found.";
		if(getObjectType($los::object) != "Player")
			return "No valid target for theft was found.";
	} else return "No valid target for theft was found.";
	
	if(Player::isAiControlled(%id) && %mode == "back") {
		%mode = "";
	}
	
	%victimName = rpg::getName(%id);
	%stealerName = rpg::getName(%TrueClientId);
	%victimCoins = fetchData(%id, "COINS");
	if(Player::isAiControlled(%id)) %victimCoins = fetchData(%id,"LVL") * 10;	
	%dRot = GetWord(GameBase::getRotation(%id), 2);
	%sRot = GetWord(GameBase::getRotation(%TrueClientId), 2);
	%diff = %dRot - %sRot;
	%behind = (%diff >= -2 && %diff <= 2);		// close enough.
	if(%mode == "back" && !%behind)		return "You must be behind a target to #steal from them.";
	if(%victimCoins <= 0)				return %victimName @ " doesn't appear to be carrying any coins...";
		
	Item::setVelocity(%id, "0 0 0");
	%r1 = GetRoll("1d" @ ($PlayerSkill[%TrueClientId, $SkillThievery] / %multiplier));
	%r2 = GetRoll("1d" @ $PlayerSkill[%id, $SkillThievery]);
	%a = %r1 - %r2;
	%amount = floor(%a * getRandom() * 1.2 * %multiplier);
	if(%amount > %victimCoins) %amount = %victimCoins;
	if(%amount <= 0) {
		UseSkill(%TrueClientId, $SkillThievery, False, True);
		PostSteal(%TrueClientId, %id, False, 0, %amount);
		Client::sendMessage(%id, $MsgRed, %stealerName @ " just failed to steal from you!");
		if(%mode == "pull") {
			Item::setVelocity(%id, Vector::getFromRot(GameBase::getRotation(%TrueClientId), -10 * %multiplier, 1));
		}
		return "You failed to steal from " @ %victimName @ "!";
	}
	
	storeData(%TrueClientId, "COINS", %amount, "inc");
	storeData(%TrueClientId, "STOLEN", %amount, "inc");
	
	if(!Player::isAiControlled(%id) || fetchData(%id, "COINS") > 0)
		storeData(%id, "COINS", %amount, "dec");
	PerhapsPlayStealSound(%TrueClientId, 0);
	Client::sendMessage(%TrueClientId, $MsgTypeChat, "You successfully stole " @ %amount @ " coins from " @ %victimName @ "!");
	Client::sendMessage(%id, $MsgTypeChat, %stealerName @ " stole " @ %amount @ " coins from you!");
	RefreshAll(%TrueClientId);
	RefreshAll(%id);
	UseSkill(%TrueClientId, $SkillThievery, True, True);
	PostSteal(%TrueClientId, %id, True, 0, %amount);
	if(%mode == "pull") {
		Item::setVelocity(%id, Vector::getFromRot(GameBase::getRotation(%TrueClientId), -10 * %multiplier, 1));
	}
	return "";
}

ExplosionData LeechKnifeHit
{
   shapeName = "chainspk.dts";
   soundId   = SoundBleedStab;
   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 1.5;
   timeZero = 0.150;
   timeOne  = 0.650;
   colors[0]  = { 0.0, 0.0, 0.0  };
   colors[1]  = { 1.0, 0.5, 0.16 };
   colors[2]  = { 1.0, 0.5, 0.16 };
   radFactors = { 0.0, 1.0, 1.0 };
};
MineData ThieveryLeechKnife {
	shapeFile = "dagger"; 		explosionId = LeechKnifeHit;				explosionRadius = 0;
	mass = 5;	drag = 0.0;		density = 0;		elasticity = 0.0;	friction = 500.0;
	kickBackStrength = 0;		triggerRadius = 0;	maxDamage = 999.0;	damageValue = 1.0;	
	className = "Handgrenade";	description = "Handgrenade";	
	shadowDetailMask = 4;		damageType = $NullDamageType;
};

function KnockoutRecover(%id) {
	if(%id.sleepMode != "") {
		%id.sleepMode = "";
		Client::setControlObject(%id, %id);
		refreshHPREGEN(%id);
		refreshMANAREGEN(%id);
		Client::sendMessage(%id, $MsgGreen, "You shake off the knockout blow.");
	}
	if(Player::isAiControlled(%id)) {
		storeData(%id, "dumbAIflag", "");
		storeData(%id, "frozen", "");
		AI::SetSpotDist(%id);
	}
}
