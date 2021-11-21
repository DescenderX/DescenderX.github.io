//________________________________________________________________________________________________________________________________________________________________
// WORDSMITH SKILLS
//
//	DescX Notes:
//		All-new D&D "resting" caster. You #inscribe spells into Glass Idioms, which are items in your belt. You then 
//		#render the idioms to cast them, or #smash to shatter them for explosive damage.
//		The Wordsmith also gets the most powerful radial buffs and abilities to disable targets.
//________________________________________________________________________________________________________________________________________________________________
$SkillType["#global"] 		= $SkillWordsmith;		// Working
$SkillType["#inscribe"] 	= $SkillWordsmith;		// Working			but Ill have to playtest it properly, balance probably not great
$SkillType["#render"] 		= $SkillWordsmith;		// Working			Im fucking shocked and dont believe this code path actually works...
$SkillType["#charm"] 		= $SkillWordsmith;		// Working
$SkillType["#fool"] 		= $SkillWordsmith;		// Working			but it sends a team change message globally... not exactly ideal...
$SkillType["#intimidate"] 	= $SkillWordsmith;		// Working			almost certainly too powerful, need a resist dampening thingy..?
$SkillType["#rally"] 		= $SkillWordsmith;		// Working
$SkillType["#confuse"] 		= $SkillWordsmith;		// Working			might be too powerful?
$SkillType["#smash"] 		= $SkillWordsmith;		// Working
$SkillType["#inspire"] 		= $SkillWordsmith;		// Working
$SkillType["#coerce"] 		= $SkillWordsmith;		// Working 			but I think there's a bug with returning control....?	

$AccessoryVar["#global", $MiscInfo] = "Spread the word. #global allows a Wordsmith to speak to everyone in Keldrin at once.";
$AccessoryVar["#inscribe", $MiscInfo] = "#inscribe spells into crystals. If etching results in resonance, a Glass Idiom is created.";
$AccessoryVar["#render", $MiscInfo] = "Clarify a Glass Idiom with #render and the spell contained within will be released.";
$AccessoryVar["#charm", $MiscInfo] = "Pacify the target with words of mutual understanding. Prevents non-spell damage both to and from the target for a short time.";
$AccessoryVar["#fool", $MiscInfo] = "Disguises aren't always necessary. #fool the enemy by temporarily joining their faction.";
$AccessoryVar["#intimidate", $MiscInfo] = "#intimidate a target in their native tongue, reducing their combat effectiveness overall.";
$AccessoryVar["#rally", $MiscInfo] = "Boost the health regeneration and speed of nearby allies with a #rally. Also grants one free #brace.";
$AccessoryVar["#confuse", $MiscInfo] = "Make targets fall on their own swords. #confuse enemies to make them attack themselves or their allies.";
$AccessoryVar["#smash", $MiscInfo] = "Knowledge is power. Use #smash to shatter a random Idiom and deal radial spell damage based on its value.";
$AccessoryVar["#inspire", $MiscInfo] = "#inspire a target to increase their mana regenerate, maximum focus and weight capacity for a short time.";
$AccessoryVar["#coerce", $MiscInfo] = "The Wordsmith is the shepherd; all others are sheep. #coerce a target into following you for a short time.";

$SkillList[$SkillWordsmith] = "#global #inscribe #render #charm #fool #intimidate #rally #confuse #smash #inspire #coerce";
$SkillListAICombat[$SkillWordsmith] = "#render #charm #intimidate #rally #confuse #coerce #smash #inspire";

function processWordsmith(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai) {
	%processed = True;	
	%time = getIntegerTime(true) >> 5;
	%lastSkillTime = fetchData(%TrueClientId, "LastWordsmithTime");
	if(%time - %lastSkillTime < 0) {	// fix.		
		%lastSkillTime = 0;
		storeData(%TrueClientId, "LastWordsmithTime", %lastSkillTime);
	}
	%timeBetweenSkillUse = %time - %lastSkillTime;
	
	if(%w1 == "#global" || %w1 == "#g") {
		if(SkillCanUse(%TrueClientId, "#global")) {
			if(IsDead(%TrueClientId))
				Client::sendMessage(%TrueClientId, $MsgRed, "It's hard to speak, let alone loudly, while dead.");
			else if(!fetchData(%TrueClientId, "ignoreGlobal")) {
				for(%cl = Client::getFirst(); %cl != -1; %cl = Client::getNext(%cl)) {
					if(!%cl.muted[%TrueClientId] && %cl != %TrueClientId && !fetchData(%cl, "ignoreGlobal")) {
						if(%cl.alttext)	Client::sendMessage(%cl, $MsgGreen, string::translate2("[G] ") @ %TCsenderName @ " - " @ %cropped);
						else 			Client::sendMessage(%cl, $MsgGreen, "[G] " @ %TCsenderName @ " - " @ %cropped);
					}
					if(%TrueClientId.alttext)	Client::sendMessage(%TrueClientId, $MsgGreen, string::translate2("[G] ") @ %cropped);
					else						Client::sendMessage(%TrueClientId, $MsgGreen, "[G] " @ %cropped);
					
					UseSkill(%TrueClientId, $SkillWordsmith, False, True);
				}
			} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't send a Global message when ignoring other Global messages.");	
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You skill is too low to send #global messages.");	
	}
	else if(%w1 == "#inscribe") {
		if(!SkillCanUse(%TrueClientId, "#inscribe")) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You skill is too low to #inscribe words in crystal.");
			return %processed;
		}
		%quantityToInscribe = Cap(getword(%cropped, 1), 1, Cap(floor(GetSkillWithBonus(%TrueClientId,$SkillWordsmith) / 10), 10, 100));
		%recovTime = 4 + (%quantityToInscribe * 2);
		if(%timeBetweenSkillUse < %recovTime) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #inscribe for another " @ floor(%recovTime - %timeBetweenSkillUse) @ " seconds.");
		} else {
			%whatSpell = getword(%cropped, 0);
			if(%cropped == "" || %whatSpell == -1) {
				Client::sendMessage(%TrueClientId, $MsgWhite, "#inscribe [spellname] [quantity]");
				return %processed;
			}
			
			%spellIndex = $Spell::index[%whatSpell];
			if(!%spellIndex) {
				Client::sendMessage(%TrueClientId, $MsgRed, "You've never heard of a \"" @ %whatSpell @ "\".");
				return %processed;
			}
			%restictSkill 	= $SkillType[%whatSpell];
			%restictAmount 	= rpg::GetAdjustedSkillRestriction(%clientId, %whatSpell);
			if(!SkillCanUse(%TrueClientId, %whatSpell, $SkillWordsmith)) {
				Client::sendMessage(%TrueClientId, $MsgRed, "You don't know how to pronounce \"" @ %whatSpell @ "\", let alone #inscribe it.");
				return %processed;
			}
			if(%restictSkill == $SkillElementalMagic && !SkillCanUse(%TrueClientId, %whatSpell, $SkillMining)) {
				Client::sendMessage(%TrueClientId, $MsgRed, "You haven't yet discovered the etymology of \"" @ %whatSpell @ "\" while Mining.");
				return %processed;
			}
			if(%restictSkill == $SkillRestorationMagic && !SkillCanUse(%TrueClientId, %whatSpell, $SkillVitality)) {
				Client::sendMessage(%TrueClientId, $MsgRed, "Your lack of Vitality prevents you from fully understanding \"" @ %whatSpell @ "\".");
				return %processed;
			}
			if(%restictSkill == $SkillDistortionMagic && !SkillCanUse(%TrueClientId, %whatSpell, $SkillLuck)) {
				Client::sendMessage(%TrueClientId, $MsgRed, "Without a lot of Luck, guessing the meaning of \"" @ %whatSpell @ "\" will be impossible.");
				return %processed;
			}
			if(%restictSkill == $SkillIllusionMagic && !SkillCanUse(%TrueClientId, %whatSpell, $SkillEvasion)) {
				Client::sendMessage(%TrueClientId, $MsgRed, "Evasion is the key to interpreting \"" @ %whatSpell @ "\".");
				return %processed;
			}
			if(%restictSkill == $SkillCombatArts && !SkillCanUse(%TrueClientId, %whatSpell, $SkillEnergy)) {
				Client::sendMessage(%TrueClientId, $MsgRed, "Reciting the \"" @ %whatSpell @ "\" Combat Art requires more Energy than you have.");
				return %processed;
			}
			%idiomName = $Spell::keyword[%spellIndex] @ "idiom";
			%cost = floor($ItemCost[%idiomName]);
			%restrict = rpg::GetAdjustedSkillRestriction(%clientId, %idiomName);
			if(%restrict < 500)		%requiredGemIndex = round(Cap(%restrict / 35, 1, 18));
			else					%requiredGemIndex = round(Cap(%restrict / 50, 1, 18));
			%requiredGem = getword($ItemList[Mining,%requiredGemIndex], 0);
			%gemQuantity = round(Cap($ItemCost[%idiomName] / rpg::GetAdjustedSkillRestriction(%clientId, %idiomName),1,"inf"));
			%gemQuantity *= %quantityToInscribe;
			
			
			%altReqParchment 	= floor( Cap($ItemCost[%requiredGem] / 500 + Math::log(%requiredGemIndex), 1, "inf") * %quantityToInscribe);
			%altReqDust 		= floor( Cap(%requiredGemIndex / (1 + Math::log(%requiredGemIndex)), 1, "inf") * %quantityToInscribe);
			%altRequirements	= "Parchment " @ %altReqParchment @ " MagicDust " @ %altReqDust;
			
			if( !HasThisStuff(%TrueClientId, %requiredGem @ " " @ %gemQuantity) &&
				!HasThisStuff(%TrueClientId, %altRequirements)) {
				Client::sendMessage(%TrueClientId, $MsgRed, "To #inscribe \"" @ %whatSpell @ "\", you will need:");
				Client::sendMessage(%TrueClientId, $MsgRed, "[ " @ %gemQuantity @ " " @ %requiredGem @ " ] " @
															"OR [ " @ %altRequirements @ " ] ");
				return %processed;
			}
			
			storeData(%TrueClientId, "LastWordsmithTime", %time);
			if(HasThisStuff(%TrueClientId, %requiredGem @ " " @ %gemQuantity)) {
				TakeThisStuff(%TrueClientId, %requiredGem @ " " @ %gemQuantity);
				Client::sendMessage(%TrueClientId, $MsgBeige, "You begin to #inscribe all you know about \"" @ %whatSpell @ "\" into " @ %gemQuantity @ " " @ %requiredGem @ ".");
			} else {
				TakeThisStuff(%TrueClientId, %altRequirements);
				Client::sendMessage(%TrueClientId, $MsgBeige, "You begin to #inscribe all you know about \"" @ %whatSpell @ "\" using " @ %altRequirements @ ".");
			}
			
			
			playSound(SoundHitore, GameBase::getPosition(%TrueClientId));
			
			// some time passes
			storeData(%TrueClientId, "SpellCastStep", 1);		
			storeData(%TrueClientId, "SpellRecovTime", %recovTime);
			for(%x=2;%x<%recovTime;%x+=2){
				schedule("playSound(SoundHitore, GameBase::getPosition(" @ %TrueClientId @ "));", %x);
			}
			remoteEval(%TrueClientId, "rpgbarhud", %recovTime, 4, 2, "||");
			
			// some random checks pass or fail the idiom creation
			schedule("EndIdiomCreation(" @ %TrueClientId @ ",\"" @ %whatSpell @ "idiom " @ %quantityToInscribe @ "\"," @ %spellIndex @ "," @ %restictAmount @ ", " @ %gemQuantity @ ");",
						%recovTime);
		}
	}
	else if(%w1 == "#render") {
		if(!SkillCanUse(%TrueClientId, "#render")) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You skill is too low to #render words from crystal.");
			return %processed;
		}
		if(fetchData(%TrueClientId, "SpellCastStep") > 0) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You are still recovering from magic and cannot yet #render ore.");
			return;
		}
		if(fetchData(%TrueClientId, "invisible") == 2) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You cannot #render ore if it is invisible. Unhide yourself first.");
			return;
		}
		%whatSpell = getword(%cropped, 0);
		if(%cropped == "" || %whatSpell == -1) {
			Client::sendMessage(%TrueClientId, $MsgWhite, "#render [spellname]");
			return %processed;
		}
		%includedSuffix = string::findSubStr(%whatSpell,"idiom");
		if(%includedSuffix > 0) {
			%whatSpell = string::newGetSubStr(%whatSpell,0,%includedSuffix);
		}
		%i = $Spell::index[%whatSpell];		
		if(!%i) {
			Client::sendMessage(%TrueClientId, $MsgRed, "There is no idiom for \"" @ %whatSpell @ "\".");
			return %processed;
		}
		%whichIdiom = %whatSpell @ "idiom";
		if(!HasThisStuff(%TrueClientId, %whichIdiom @ " 1")) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You don't have any \"" @ %whatSpell @ "\" idioms to #render.");
			return;
		}
		TakeThisStuff(%TrueClientId, %whichIdiom @ " 1");
		
		
		Client::sendMessage(%TrueClientId, $MsgBeige, "Rendering " @ $Spell::name[%i] @ ".");
		storeData(%TrueClientId, "SpellCastStep", 1);
		storeData(%TrueClientId, "SpellRecovTime", $Spell::recoveryTime[%i]);	
		%losobj = 0;
		if(GameBase::getLOSinfo(%player, $Spell::LOSrange[%i]))
			%losobj = $los::object;
		%w2 = String::getSubStr(%cropped, String::len(%whatSpell)+1, 99999);
		
		PlaySound(SoundFloatMineTarget,GameBase::getPosition(%TrueClientId));
		schedule("PlaySound(\"" @ $Spell::startSound[%i] @ "\", GameBase::getPosition(" @ %TrueClientId @ "));", 0.5);
		
		if($spell::menu[%i])
			eval("casting::"@ %whatSpell @ "(" @ %TrueClientId @ ", " @ %i @ ", \"" @ GameBase::getPosition(%TrueClientId) @ "\", \"" @ %losobj @ "\", \"" @ %w2 @ "\");");
		else 
			schedule("CastSpell(" @ %TrueClientId @ "," @ %i @ ", GameBase::getPosition(" @ %TrueClientId @ ")," @ %losobj @ ", \"" @ %w2 @ "\", 0, 1);", 1);
		UseSkill(%TrueClientId, $SkillWordsmith, True, True);
	}
	else if(%w1 == "#charm") {
		if(!SkillCanUse(%TrueClientId, "#charm")) {
			Client::sendMessage(%TrueClientId, $MsgRed, "Your skill is too low to #charm.");
			return %processed;
		}
		%ticks = Cap($PlayerSkill[%TrueClientId,$SkillWordsmith] / 150, 2, 8);
		%asSeconds = RoundToFirstDecimal(%ticks * 2);
		if(%timeBetweenSkillUse < (%asSeconds*3)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #charm for another " @ floor((%asSeconds*3) - %timeBetweenSkillUse) @ " seconds.");
		} else {			
			if((%id = GetClientInLOS(%TrueClientId,10)) != -1) {
				%charming = AddBonusStatePoints(%TrueClientId, "CHARM");
				if(%charming) { // If we are currently charming a target, and that target is still actually charmed by this player, remove the charm on the old target
					%charmedBy = AddBonusStatePoints(%charming, "CHARM");
					if(%charmedBy == %TrueClientId) {
						UpdateBonusState(%charming, "CHARM 1", 0, 0);
						Client::sendMessage(%TrueClientId, $MsgBeige, "You turn your attention away from " @ rpg::getName(%charming) @ ".");
					}
				}
				
				UpdateBonusState(%TrueClientId, "CHARM 1", %ticks, %id);
				UpdateBonusState(%id, "CHARM 1", %ticks, %TrueClientId);
				Client::sendMessage(%TrueClientId, $MsgGreen, "You have charmed " @ rpg::getName(%id) @ ", preventing damage for the next " @ %asSeconds @ " seconds.");
				Client::sendMessage(%id, $MsgGreen, "You have been charmed by " @ rpg::getName(%TrueClientId) @ " and will refuse to hurt them for " @ %asSeconds @ " seconds.");
				UseSkill(%TrueClientId, $SkillWordsmith, True, True);
			} else Client::sendMessage(%TrueClientId, $MsgRed, "You #charm is wasted on thin air.");
			storeData(%TrueClientId, "LastWordsmithTime", %time);
		}
	}
	else if(%w1 == "#fool") {
		if(!SkillCanUse(%TrueClientId, "#fool") || Player::isAIControlled(%TrueClientId)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You skill is too low to #fool anyone.");
			return %processed;
		}
		%asSeconds = Cap($PlayerSkill[%TrueClientId,$SkillWordsmith] / 150, 2, 8);
		if(%timeBetweenSkillUse < (%asSeconds * 2)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #fool for another " @ floor((%asSeconds*2) - %timeBetweenSkillUse) @ " seconds.");
		} else {			
			if((%id = GetClientInLOS(%TrueClientId,10)) != -1) {
				%enemyTeam = GameBase::getTeam(%id);
				%race = fetchData(%TrueClientId,"RACE");
				GameBase::setTeam(%TrueClientId, %enemyTeam);
				ChangeRace(%TrueClientId, %race);
				Client::sendMessage(%id, $MsgGreen, "You trick " @ rpg::getName(%id) @ " into letting you join the " @ $Server::teamNamePlural[%enemyTeam] @ " for " @ %asSeconds @ " seconds.");
				schedule("GameBase::setTeam(" @ %TrueClientId @ ", " @ $TeamForRace[%race] @ ");", %asSeconds);
				PlaySound(RandomRaceSound(fetchData(%TrueClientId, "RACE"), RandomWait), GameBase::getPosition(%TrueClientId));
			} else Client::sendMessage(%TrueClientId, $MsgRed, "The only #fool you see is... yourself.");
			storeData(%TrueClientId, "LastWordsmithTime", %time);
		}
	}
	else if(%w1 == "#intimidate") {
		if(!SkillCanUse(%TrueClientId, "#intimidate")) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You skill is too low to #intimidate.");
			return %processed;
		}
		%ticks = Cap($PlayerSkill[%TrueClientId,$SkillWordsmith] / 100, 3, 10);
		%asSeconds = RoundToFirstDecimal(%ticks * 2);
		if(%timeBetweenSkillUse < (%asSeconds * 2) && !Player::isAIControlled(%TrueClientId)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #intimidate for another " @ floor((%asSeconds * 2) - %timeBetweenSkillUse) @ " seconds.");
		} else {
			// apply reduction to str, evade, endur, energy
			if((%id = GetClientInLOS(%TrueClientId,10)) != -1) {	
				%success = rpg::ResistSkillRoll("#intimidate", 
												%TrueClientId, 	$SkillWordsmith, 	1, 0,
												%id, 			$SkillStrength, 	0.8, (fetchData(%id, "MDEF")));
				if(!%success) %success = rpg::ResistSkillRoll("#intimidate", 
												%TrueClientId, 	$SkillWordsmith, 	1, 0,
												%id, 			$SkillWordsmith, 	0.8, 0);
				if(!%success) {
					Client::sendMessage(%TrueClientId, $MsgRed, rpg::getName(%TrueClientId) @ " laughs at you!");
					Client::sendMessage(%id, $MsgRed, rpg::getName(%TrueClientId) @ "'s intimidation has no effect on you.");
				} else {
					%killStats = Cap($PlayerSkill[%TrueClientId,$SkillWordsmith] / 3, 1, 500);
					
					UpdateBonusState(%id, "Strength 13", %ticks, -%killStats, true);
					UpdateBonusState(%id, "Focus 13", %ticks, -%killStats, true);
					UpdateBonusState(%id, "Evasion 13", %ticks, -%killStats, true);
					UpdateBonusState(%id, "Energy 13", %ticks, -%killStats, true);
					
					PlaySound(RandomRaceSound(fetchData(%TrueClientId, "RACE"), Taunt), GameBase::getPosition(%TrueClientId));
					
					Client::sendMessage(%TrueClientId, $MsgGreen, "You #intimidate " @ rpg::getName(%id) @ ", sapping " @ floor(%killStats) @ " points from their Strength, Focus, Evasion and Energy for " @ %asSeconds @ " seconds.");
					Client::sendMessage(%id, $MsgRed, rpg::getName(%TrueClientId) @ " destroys your morale for " @ floor(%asSeconds) @ " seconds.");
					UseSkill(%TrueClientId, $SkillWordsmith, True, True);
				}
			} else Client::sendMessage(%TrueClientId, $MsgRed, "You hurl insults and anger at nothing.");
			storeData(%TrueClientId, "LastWordsmithTime", %time);
		}	
	}
	else if(%w1 == "#rally") {
		if(!SkillCanUse(%TrueClientId, "#rally")) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You skill is too low to #rally allies.");
			return %processed;
		}
		if(%timeBetweenSkillUse < 10 && !Player::isAIControlled(%TrueClientId)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #rally for another " @ floor(10 - %timeBetweenSkillUse) @ " seconds.");
		} else {
			%range = Cap($PlayerSkill[%TrueClientId,$SkillWordsmith] / 50, 10, 25);
			%power = Cap($PlayerSkill[%TrueClientId,$SkillWordsmith] / 200, 1, 8);
			%set = newObject("set", SimSet);
			%n = containerBoxFillSet(%set, $SimPlayerObjectType, GameBase::getPosition(%TrueClientId), %range, %range, %range, 0);
			Group::iterateRecursive(%set, DoRadialRally, %TrueClientId, %power);
			storeData(%TrueClientId, "LastWordsmithTime", %time);
			PlaySound(SoundWordsmithGeneric, GameBase::getPosition(%TrueClientId));			
		}
	}
	else if(%w1 == "#confuse") {
		if(!SkillCanUse(%TrueClientId, "#confuse")) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You skill is too low to #confuse.");
			return %processed;
		}
		if(%timeBetweenSkillUse < 5 && !Player::isAIControlled(%TrueClientId)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #confuse for another " @ floor(5 - %timeBetweenSkillUse) @ " seconds.");
		} else {
			if((%id = GetClientInLOS(%TrueClientId,10)) == -1) {
				%id = %TrueClientId;
			}
			%success = rpg::ResistSkillRoll("#confuse", 
												%TrueClientId, 	$SkillWordsmith, 	1, 0,
												%id, 			$SkillSurvival, 	2, 0);
			%weapon = Player::getMountedItem(%id,$WeaponSlot);
			PlaySound(RandomRaceSound(fetchData(%TrueClientId, "RACE"), Acquired), GameBase::getPosition(%id));
			if(%success && %weapon != -1) {
				if(%id == %TrueClientId) {
					Client::sendMessage(%TrueClientId, $MsgRed, "With no target, you #confuse yourself!");
				} else {					
					Client::sendMessage(%TrueClientId, $MsgBeige, "You #confuse " @ rpg::getName(%id) @ "!" );
					UseSkill(%TrueClientId, $SkillWordsmith, True, True);
				}
				GameBase::virtual(%id, "onDamage", "", 1.0, "0 0 0", "0 0 0", "0 0 0", "torso", %weapon, %id);
			} else if(%id != %TrueClientId) {
				Client::sendMessage(%TrueClientId, $MsgRed, "You #confuse " @ rpg::getName(%id) @ ", but your effort is wasted.");
				UseSkill(%TrueClientId, $SkillWordsmith, False, True);
			} else Client::sendMessage(%TrueClientId, $MsgRed, "With no target, you #confuse yourself... and nothing comes of it.");
			storeData(%TrueClientId, "LastWordsmithTime", %time);
		}
	}
	else if(%w1 == "#smash") {
		if(!SkillCanUse(%TrueClientId, "#smash")) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You skill is too low to #smash idioms.");
			return %processed;
		}
		if(%timeBetweenSkillUse < 3 && !Player::isAIControlled(%TrueClientId)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #smash for another " @ floor(2 - %timeBetweenSkillUse) @ " seconds.");
		} else {
			%items = fetchData(%TrueClientId, "GlassIdioms");
			
			%found = "";
			for(%x=0;(%item=getword(%items,%x)) != -1; %x+=2) {										// select a random idiom				
				if(%found != "" && getRandom() < 0.5) break;
				%found = %item;
			}

			if(%found != "") {
				TakeThisStuff(%TrueClientId, %found @ " 1");														// take stuff				
				%basePower 	= ((($ItemCost[%found] / 2) % 1200) + 5);
				%points 	= Cap(%basePower * getRandom(), 1, "inf");								// determine damage
				%diameter	= 20;
				%set 		= newObject("set", SimSet);
				%pos 		= GameBase::getPosition(%TrueClientId);
				containerBoxFillSet(%set, $SimPlayerObjectType, %pos, %diameter, %diameter, %diameter, 0);
				Group::iterateRecursive(%set, DoRadialSmash, %TrueClientId, %diameter, %points);			// deal damage
				deleteObject(%set);								
								
				PlaySound(SoundInscribeFail, GameBase::getPosition(%TrueClientId));
				%iterations = cap(%points / 10, 2, 26);
				%player = Client::getOwnedObject(%TrueClientId);
				for(%i = 8; %i <= %iterations+8; %i++) {												// show FX
					%newPos = vecx(%pos) - (cos(%i) * 5) @ " " @ vecy(%pos) - (sin(%i) * 5) @ " " @ vecz(%pos)+2;
					%fx = floor((getRandom() * 1000) % 4);
					if(%fx==0) %whichFX = "SpellFXhail";
					else if(%fx==1) %whichFX = "SpellFXcloud";
					else if(%fx==2) %whichFX = "SpellFXcryostorm2";
					else %whichFX = "SpellFXfireball";					
					schedule("CreateSpellBomb(" @ %TrueClientId @ ", \"" @ %whichFX @ "\", \"" @ %newpos @ "\");", (%i-7) / 8, %player);
				}
				UseSkill(%TrueClientId, $SkillWordsmith, True, True);
				Client::sendMessage(%TrueClientId, $MsgGreen, "You #smash " @ %found @ " to unleash " @ floor(%points) @ " damage!" );
				storeData(%TrueClientId, "LastWordsmithTime", %time);
			} else {
				Client::sendMessage(%TrueClientId, $MsgRed, "You have no glass idioms to #smash!");
				UseSkill(%TrueClientId, $SkillWordsmith, False, True);
			}			
		}
	}
	else if(%w1 == "#inspire") {
		if(!SkillCanUse(%TrueClientId, "#inspire")) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You skill is too low to #inspire allies.");
			return %processed;
		}
		if(%timeBetweenSkillUse < 25 && !Player::isAIControlled(%TrueClientId)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #inspire for another " @ floor(25 - %timeBetweenSkillUse) @ " seconds.");
		} else {			
			%range = 8;
			%set = newObject("set", SimSet);
			%n = containerBoxFillSet(%set, $SimPlayerObjectType, GameBase::getPosition(%TrueClientId), %range, %range, %range, 0);
			Group::iterateRecursive(%set, DoRadialInspire, %TrueClientId, $PlayerSkill[%TrueClientId,$SkillWordsmith] / 2);
			storeData(%TrueClientId, "LastWordsmithTime", %time);
			PlaySound(SoundWordsmithGeneric, GameBase::getPosition(%TrueClientId));
		}
	}
	else if(%w1 == "#coerce") {
		if(!SkillCanUse(%TrueClientId, "#coerce")) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You skill is too low to #coerce anyone into following you.");
			return %processed;
		}
		if(%timeBetweenSkillUse < 15 || Player::isAIControlled(%TrueClientId)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #coerce for another " @ floor(15 - %timeBetweenSkillUse) @ " seconds.");
		} else {			
			if((%id = GetClientInLOS(%TrueClientId,10)) != -1) {
				%defenderTimeScale = 1 + ((	GetSkillWithBonus(%id,$SkillWordsmith) + 
											GetSkillWithBonus(%id,$SkillWillpower) + fetchData(%id,"MDEF")) * getRandom());
				%attackerTimeScale = GetSkillWithBonus(%TrueClientId,$SkillWordsmith) * getRandom();
				%controlTime = Cap((%attackerTimeScale / %defenderTimeScale) * (GetSkillWithBonus(%TrueClientId,$SkillWordsmith) / 150), 0, 10);
				if(%controlTime > 2) {				
					PlaySound(SoundWordsmithGeneric, GameBase::getPosition(%id));
					rpg::PossessTarget(%TrueClientId, %id, false, true);
					UseSkill(%TrueClientId, $SkillWordsmith, True, True);
					schedule("if(!IsDead(" @ %id @ ")) rpg::RevertPossession(" @ %TrueClientId @ ");", %controlTime);
					Client::sendMessage(%TrueClientId, $MsgGreen, rpg::getName(%id) @ " will follow your orders for " @ %controlTime @ " seconds.");
					Client::sendMessage(%id, $MsgRed, "You are compelled to do " @ rpg::getName(%TrueClientId) @ "'s bidding for " @ %controlTime @ " seconds.");
				}  else {
					Client::sendMessage(%TrueClientId, $MsgRed, "You fail to #coerce " @ rpg::getName(%id) @ ".");
					UseSkill(%TrueClientId, $SkillWordsmith, False, True);
				}
			} else Client::sendMessage(%TrueClientId, $MsgRed, "You don't need to #coerce yourself into doing anything...");
			storeData(%TrueClientId, "LastWordsmithTime", %time);
		}
	}
	else {
		%processed = False;
	}
	
	return %processed;
}

//________________________________________________________________________________________________________________________________________________________________
function DoRadialSmash(%object, %clientId, %diameter, %smashValue) {
	%id = Player::getClient(%object);
	if(%clientId != %id) {		
		%isPet 				= IsInCommaList(fetchData(%clientId, "PersonalPetList"), %id);
		%isInCasterGroup 	= IsInCommaList(fetchData(%clientId, "grouplist"), Client::getName(%id));
		if(!%isPet && !%isInCasterGroup) {
			%dist = Vector::getDistance(GameBase::getPosition(%clientId), GameBase::getPosition(%id));			
			%newDamage = rpg::GetSplashDamage(%dist, %diameter/2, %smashValue, 5, 100);
			GameBase::virtual(%id, "onDamage", $WordsmithDamageType, %newDamage, "0 0 0", "0 0 0", "0 0 0", "torso", "front_right", %clientId, "#smash");
			PlaySound(SoundGlassBreak, GameBase::getPosition(%id));
		}
	}
}

//________________________________________________________________________________________________________________________________________________________________
function DoRadialRally(%object, %clientId, %power) {
	%id = Player::getClient(%object);
	if(GameBase::getTeam(%id) == GameBase::getTeam(%clientId)) {
		%isPet 				= IsInCommaList(fetchData(%clientId, "PersonalPetList"), %id);
		%isInCasterGroup 	= IsInCommaList(fetchData(%clientId, "grouplist"), Client::getName(%id));
		if(%id==%clientId || %isPet || %isInCasterGroup) {
			UseSkill(%clientId, $SkillWordsmith, False, True);
			UpdateBonusState(%id, "HPR 3", 5, %power, true);
			UpdateBonusState(%id, "SPD !", 3, 1, true);
			UpdateBonusState(%id, "BRACE 2", 2, 1, true);
			RefreshWeight(%id);
			Client::sendMessage(%clientId, $MsgGreen, "Rallying " @ rpg::getName(%id));
			if(%id!=%clientId)
				Client::sendMessage(%id, $MsgGreen, rpg::getName(%clientId) @ " rallies you to fight!");
		}
	}
}

function DoRadialInspire(%object, %clientId, %power) {
	%id = Player::getClient(%object);
	if(GameBase::getTeam(%id) == GameBase::getTeam(%clientId)) {
		if(%id==%clientId || IsInCommaList(fetchData(%clientId, "partylist"), Client::getName(%id))) {
			if(%id==%clientId) {
				UseSkill(%clientId, $SkillWordsmith, True, True);
				%div = 2;
			}
			else {
				UseSkill(%clientId, $SkillWordsmith, False, True);
				%div = 1;
			}
			UpdateBonusState(%id, "WARMTH 3", 7, (%power) / %div, true);
			UpdateBonusState(%id, "Focus 5", 8, (%power * 0.8) / %div, true);
			UpdateBonusState(%id, "WEIGHT 5", 9, (-%power) / %div, true);
			RefreshWeight(%id);
			Client::sendMessage(%clientId, $MsgGreen, "Inspiring " @ rpg::getName(%id));
			if(%id!=%clientId)
				Client::sendMessage(%id, $MsgGreen, rpg::getName(%clientId) @ " inspires you!");
		}
	}
}

function EndIdiomCreation(%id, %give, %spellIndex, %skillReqAmt, %gemQuantity) {
	%skillReqRoll = getRandom() * %skillReqAmt;
	%skillRoll = getRandom() * (GetSkillWithBonus(%id, $SkillWordsmith) + GetSkillWithBonus(%id, $SkillLuck));
	if(%skillRoll > %skillReqRoll) {
		GiveThisStuff(%id, %give);
		Client::sendMessage(%id, $MsgGreen, "The ore responds, resonating with \"" @ $Spell::name[%spellIndex] @ "\".");
		UseSkill(%id, $SkillWordsmith, True, True);
		playSound(SoundInscribeOK, GameBase::getPosition(%id));
	} else {
		playSound(SoundInscribeFail, GameBase::getPosition(%id));
		%toDust = "";
		if(%gemQuantity >= 1) {
			GiveThisStuff(%id, "MagicDust " @ %gemQuantity);
			%toDust = " Your ore turns to dust.";
		}
		Client::sendMessage(%id, $MsgRed, "Your inscription fails to create resonance." @ %toDust);
	}
	
	storeData(%id, "SpellCastStep", "");
	storeData(%id, "SpellRecovTime", $Spell::recoveryTime[%i]);
}

function ClearCoercionInfluence(%id) {
	if(Player::isAIControlled(%id)) {
		GameBase::setTeam(fetchData(%id, "botorigteam"));
		storeData(%id, "botorigteam", "");
		storeData(%id, "tmpbotdata", "");
		storeData(%id, "botAttackMode", 1);
		AI::SetSpotDist(%id);
		AI::Periodic(fetchData(%id, "BotInfoAiName"));
	}
}
