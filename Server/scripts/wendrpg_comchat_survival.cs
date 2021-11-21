//________________________________________________________________________________________________________________________________________________________________
// SURVIVAL SKILLS
//
// DescX Notes:
//		Survival skills are way more than just the compass now. You start off earning basic tracking skills, but later on,
//		this turns into learning combat skills. Arguably the biggest "cheap skill" exists here, #brace, which allows you
//		to shrug off damage even if it lands. #leap is loads of fun, and if you get far enough to use #totem or
//		#sacrifice, you'll be wildly powerful.
//________________________________________________________________________________________________________________________________________________________________
$SkillType["#pvp"] 			= $SkillSurvival;		// Working		(tested without an actual player ;))
$SkillType["#compass"] 		= $SkillSurvival;		// Working
$SkillType["#recall"] 		= $SkillSurvival;		// Working
$SkillType["#trackpack"] 	= $SkillSurvival;		// Working
$SkillType["#track"] 		= $SkillSurvival;		// Working
$SkillType["#camp"] 		= $SkillSurvival;		// Working
$SkillType["#inspect"]		= $SkillSurvival;		// Working
$SkillType["#smith"] 		= $SkillSurvival;		// Working		but I need some kind of smithable item listing... ahh, NPCs, duh
$SkillType["#acrobatics"] 	= $SkillSurvival;		// Working
$SkillType["#leap"] 		= $SkillSurvival;		// Working
$SkillType["#brace"] 		= $SkillSurvival;		// Working
$SkillType["#exploit"] 		= $SkillSurvival;		// Working
$SkillType["#totem"]		= $SkillSurvival;		// Working		but the actual bonus it gives is probably overboard, would be too crazy as a team skill...
$SkillType["#sacrifice"] 	= $SkillSurvival;		// Working		FX need to be obvious to offset the chance of suicide

$AccessoryVar["#pvp", $MiscInfo] 		= "Allow other players to damage you when outside of PROTECTED zones. Disabled by default.";
$AccessoryVar["#compass", $MiscInfo] 	= "The Wizards of Keldrin maintain magnetic fields in each zone that can be pinpointed by #compass.";
$AccessoryVar["#recall", $MiscInfo] 	= "Returns the player to their home town. #recall works without skill. Training causes #recall to work instantly.";
$AccessoryVar["#trackpack", $MiscInfo] 	= "Pinpoint the location of the last package dropped by something or someone.";
$AccessoryVar["#track", $MiscInfo] 		= "Superior wilderness skills allow you to follow targets by name over great distances.";
$AccessoryVar["#camp", $MiscInfo] 		= "Set up a camp that can be used for sleeping.";
$AccessoryVar["#inspect", $MiscInfo] 	= "Your experience allows you to assess targets from anywhere in the world, or survey entire zones at a glance.";
$AccessoryVar["#smith", $MiscInfo] 		= "Why buy when you can make your own? #smith can be used anywhere with enough training.";
$AccessoryVar["#acrobatics", $MiscInfo] = "A superior Survivalist knows how to fall. #acrobatics will increase jump height and eliminate fall damage for a moment.";
$AccessoryVar["#leap", $MiscInfo] 		= "Death from above. #leap into the air and crash into the ground, dealing damage to everything around.";
$AccessoryVar["#brace", $MiscInfo] 		= "Experience begets insight. Anticipate attacks and #brace for impact with a perfect block. If timed well, the next physical attack will deal no damage.";
$AccessoryVar["#exploit", $MiscInfo] 	= "Who needs luck when you have skill? #exploit will temporarily lend your Survival skills to Luck.";
$AccessoryVar["#totem", $MiscInfo] 		= "Attract energy with a #totem. After construction, the #totem will boost mana regeneration at the cost of health regeneration.";
$AccessoryVar["#sacrifice", $MiscInfo] 	= "Survivalists lern to balance extremes. With less than 10% mana, #sacrifice results in suicide. Otherwise, all mana is consumed to boost maximum health. Mana regen is stunted while #sacrifice is active.";

$SkillList[$SkillSurvival] 				= "#pvp #compass #recall #trackpack #track #camp #inspect #smith #acrobatics #leap #brace #exploit #totem #sacrifice";
$SkillListAICombat[$SkillSurvival] 		= "#acrobatics #leap #brace #sacrifice";

function processSurvival(%clientId, %team, %message, %senderName, %w1, %cropped, %TrueClientId, %TCsenderName, %isai) {
	%processed = True;	
	%time = getIntegerTime(true) >> 5;
	%lastSkillTime = fetchData(%TrueClientId, "LastSurvivalSkillTime");
	if(%time - %lastSkillTime < 0) {	// fix.		
		%lastSkillTime = 0;
		storeData(%TrueClientId, "LastSurvivalSkillTime", %lastSkillTime);
	}
	%timeBetweenSkillUse = %time - %lastSkillTime;
	%skillLevel = GetSkillWithBonus(%TrueClientId, $SkillSurvival);
	
	if(%w1 == "#pvp" && !rpg::IsTheWorldEnding()) {
		if(!Player::isAIControlled(%TrueClientId)){
			%pvp = fetchData(%clientId,"PVP");
			if(%pvp!=1)	{
				storeData(%clientId,"PVP",1);
				Client::sendMessage(%TrueClientId, $MsgRed, "Player-vs-Player enabled.");
			} else {
				storeData(%clientId,"PVP",0);
				Client::sendMessage(%TrueClientId, $MsgBeige, "Player-vs-Player disabled.");
			}
		}
	}
	else if(%w1 == "#recall" && !rpg::IsTheWorldEnding()) {
		if(%timeBetweenSkillUse < 25 && !Player::isAIControlled(%TrueClientId)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #recall for another " @ 25 - %timeBetweenSkillUse @ " seconds.");
		} else {
			storeData(%TrueClientId, "LastSurvivalSkillTime", %time);
			rpg::RecallPlayer(%TrueClientId);
		}
	}
	else if(%w1 == "#smith") {		
		%atAnvil = False;
		if(GameBase::getLOSinfo(Client::getOwnedObject(%TrueClientId), 5)) {
			%target = $los::object;
			%obj = getObjectType(%target);
			%oname = object::getname(%target);
			%type = Getword(%oname, 1);
			%atAnvil = 	(%type == "Anvil" || %type == "anvil") || 
						(%oname == "Tent" || %oname == "woodfire") || 
						(SkillCanUse(%TrueClientId, "#smith")) ||
						(cliptrailingnumbers(Getword(%oname, 0)) == "anvil");
			if (%atAnvil) {
				%item = GetWord(%cropped, 0);
				if(%item == -1 || Player::isAIControlled(%TrueClientId)) {
					Client::sendMessage(%TrueClientId, $MsgBeige, "#smith itemname(one word) # | ex: #smith KeldriniteLS 2");
				} else {
					%smithnum = $smithVar[%item];
					if ($SmithAssociatedSkillType[%smithnum] == $SkillRestorationMagic) {
						Client::sendMessage(%TrueClientId, $MsgBeige, "You can't #smith potions, elixers and other liquids. Join the Order Of Qod to #brew concoctions.");
					} else {
						%amt = floor(GetWord(%cropped, 1));
						
						if(%amt < 1) 	%amt = 1;
						if(%amt > 100)	%amt = 100;
						if(%smithnum > 0) {
							%sc = $SmithCombo[%smithnum];
							if(HasThisStuff(%TrueClientId, %sc, %amt) && !IsDead(%TrueClientId)) {
								for(%i = 0; (%w = GetWord(%sc, %i)) != -1; %i+=2) {
									%w2 = GetWord(%sc, %i+1) * %amt;
									takethisstuff(%TrueClientId, %w @ " "@%w2);
								}
								%m = multiplyItemString($SmithComboResult[%smithnum], %amt);
								givethisstuff(%trueClientId, %m);
								if(isBeltItem(%item))	%itemname = $beltitem[%item, "Name"];
								else					%itemname = %item.description;
								Client::sendMessage(%TrueClientId, $MsgGreen, "You smithed "@%m@".");
								
								for(%i=1; %i <= %amt; %i++)
									UseSkill(%TrueClientId, $SkillSurvival, False, True);
							} else Client::sendMessage(%TrueClientId, $MsgRed, "You do not have the items needed to smith a " @ %item @ ". You need " @ %sc @ ".");						
						} else Client::sendMessage(%TrueClientId, $MsgRed, %item @ " is not smithable.");
					}
				}
			}
		}
		if(!%atAnvil) {
			%minskill = rpg::GetAdjustedSkillRestriction(%clientId, "#camp", $SkillType["#camp"]);
			Client::sendMessage(%TrueClientId, $MsgRed, "You can only #smith if you have " @ %minskill @ " Survival, or are near an anvil, woodfire or tent.");
		}
	}
	else if(%w1 == "#camp" && !rpg::IsTheWorldEnding()) {
		if(SkillCanUse(%TrueClientId, "#camp") || Player::isAIControlled(%TrueClientId)) {
			if(fetchData(%TrueClientId, "zone") == "") {
				%camp = nameToId("MissionCleanup\\Camp" @ %TrueClientId);
				if(%camp != -1) {
					DoCampSetup(%TrueClientId, 5);
					Client::sendMessage(%TrueClientId, $MsgBeige, "Camp has been packed up.");
				} else {
					Client::sendMessage(%TrueClientId, $MsgBeige, "Setting up a new camp...");
		
					%pos = GameBase::getPosition(%TrueClientId);
		
					RefreshAll(%TrueClientId);
					%group = newObject("Camp" @ %TrueClientId, SimGroup);
					addToSet("MissionCleanup", %group);

					schedule("DoCampSetup(" @ %TrueClientId @ ", 1, \"" @ %pos @ "\");", 2, %group);
					schedule("DoCampSetup(" @ %TrueClientId @ ", 2, \"" @ %pos @ "\");", 10, %group);
					schedule("DoCampSetup(" @ %TrueClientId @ ", 3, \"" @ %pos @ "\");", 17, %group);
					schedule("DoCampSetup(" @ %TrueClientId @ ", 4, \"" @ %pos @ "\");", 20, %group);
				}
			} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't set up a camp here.");
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You don't have enough skill to set up a camp yet.");
	}
	else if(%w1 == "#compass") {
		if(%cropped == "" || Player::isAIControlled(%TrueClientId))
			Client::sendMessage(%TrueClientId, 0, "Use #compass town | dungeon | SpecificName");
		else {
			if(SkillCanUse(%TrueClientId, "#compass")) {
				if(string::compare(%cropped,"town") == 0 || string::compare(%cropped,"dungeon") == 0){
					%mpos = GetNearestZone(%TrueClientId, %cropped, 4);
					
					if(%mpos != False) {
						%d = GetNESW(GameBase::getPosition(%TrueClientId), %mpos);
						UseSkill(%TrueClientId, $SkillSurvival, False, True);
						Client::sendMessage(%TrueClientId, 0, "The nearest " @ %cropped @ " is " @ %d @ " of here.");
						arrowTowards(gamebase::getposition(%TrueClientId), %mpos, rpg::getName(%TrueClientId) @ "'s Compass");
					} else Client::sendMessage(%TrueClientId, 1, "Error finding a zone!");
				} else {
					%obj = GetZoneByKeywords(%TrueClientId, %cropped, 3);

					if(%obj != False) {
						%mpos = Zone::getMarker(%obj);
						%d = GetNESW(GameBase::getPosition(%TrueClientId), %mpos);
						UseSkill(%TrueClientId, $SkillSurvival, False, True);
						Client::sendMessage(%TrueClientId, 0, Zone::getDesc(%obj) @ " is " @ %d @ " of here.");
						arrowTowards(gamebase::getposition(%TrueClientId), %mpos, rpg::getName(%TrueClientId) @ "'s Compass");
					} else Client::sendMessage(%TrueClientId, 1, "Couldn't fine a zone to match those keywords.");
				}
			} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't use your compass because you lack the necessary skills.");
		}
	}
	else if(%w1 == "#track") {
		%cropped = GetWord(%cropped, 0);

		if(%cropped == "" || Player::isAIControlled(%TrueClientId))
			Client::sendMessage(%TrueClientId, 0, "Please specify a name.");
		else {
			if(SkillCanUse(%TrueClientId, "#track")) {
				%id = NEWgetClientByName(%cropped);
				%cropped = Client::getName(%id);
				if(%id != -1) {
					%clientIdpos = GameBase::getPosition(%TrueClientId);
					%idpos = fetchData(%id, "lastScent");

					if(%idpos != "") {
						%dist = round(Vector::getDistance(%clientIdpos, %idpos));

						if(Cap(%skillLevel * 7.5, 100, "inf") >= %dist) {
							%d = GetNESW(%clientIdpos, %idpos);
							Client::sendMessage(%TrueClientId, $MsgGreen, "You sense that " @ %cropped @ " is " @ %d @ " of here, " @ %dist @ " meters away.");
							UseSkill(%TrueClientId, $SkillSurvival, False, True);
						} else {
							Client::sendMessage(%TrueClientId, $MsgWhite, "You have no idea where " @ %cropped @ " could be.");
							UseSkill(%TrueClientId, $SkillSurvival, False, True);
						}
					} else {
						Client::sendMessage(%TrueClientId, $MsgWhite, "You have no idea where " @ %cropped @ " could be.");
						UseSkill(%TrueClientId, $SkillSurvival, False, True);
					}
				} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
			} else {
				Client::sendMessage(%TrueClientId, $MsgRed, "You can't track because you lack the necessary skills.");
				UseSkill(%TrueClientId, $SkillSurvival, False, True);
			}
		}
	}
	else if(%w1 == "#trackpack") {
		%cropped = GetWord(%cropped, 0);

		if(%cropped == "" || Player::isAIControlled(%TrueClientId))
			Client::sendMessage(%TrueClientId, 0, "Please specify a name.");
		else {
			if(SkillCanUse(%TrueClientId, "#trackpack")) {
				%id = NEWgetClientByName(%cropped);
				if(%id != -1) {
					%cropped = Client::getName(%id);	//properly capitalize name

					%closest = 5000000;
					%closestId = -1;
					%clientIdpos = GameBase::getPosition(%TrueClientId);
					%list = fetchData(%id, "lootbaglist");
					for(%i = String::findSubStr(%list, ","); String::findSubStr(%list, ",") != -1; %list = String::NEWgetSubStr(%list, %i+1, 99999)) {
						%id = String::NEWgetSubStr(%list, 0, %i);
						%idpos = GameBase::getPosition(%id);
						%dist = round(Vector::getDistance(%clientIdpos, %idpos));
						if(%dist < %closest) {
							%closest = %dist;
							%closestId = %id;
						}
					}
					
					if(%closestId != -1) {
						%idpos = GameBase::getPosition(%closestId);

						if(Cap(%skillLevel * 15, 100, "inf") >= %closest) {
							%d = GetNESW(%clientIdpos, %idpos);
							Client::sendMessage(%TrueClientId, $MsgGreen, %cropped @ "'s nearest backpack is " @ %d @ " of here, " @ %closest @ " meters away.");
							UseSkill(%TrueClientId, $SkillSurvival, False, True);
						} else {
							Client::sendMessage(%TrueClientId, $MsgWhite, %cropped @ "'s nearest backpack is too far from you to track with your current sense heading skills.");
							UseSkill(%TrueClientId, $SkillSurvival, False, True);
						}
					} else {
						Client::sendMessage(%TrueClientId, $MsgBeige, %cropped @ " doesn't have any dropped backpacks.");
						UseSkill(%TrueClientId, $SkillSurvival, False, True);
					}
				} else Client::sendMessage(%TrueClientId, 0, "Invalid player name.");
			} else {
				Client::sendMessage(%TrueClientId, $MsgRed, "You can't track a backpack because you lack the necessary skills.");
				UseSkill(%TrueClientId, $SkillSurvival, False, True);
			}
		}
	}
	else if(%w1 == "#inspect") {
		if(SkillCanUse(%TrueClientId, "#inspect") && !Player::isAIControlled(%TrueClientId)) {
			%c1 = GetWord(%cropped, 0);

			if(%c1 != -1) {
				if(String::ICompare(%c1, "zone") == 0) {
					%t = 1;
					%word2 = GetWord(%cropped, 1);
					if(String::ICompare(%word2, "players") == 0)		%t = 2;
					else if(String::ICompare(%word2, "enemies") == 0)	%t = 3;
					%list = Zone::getPlayerList(fetchData(%TrueClientId, "zone"), %t);				
					if(%list != "") {
						for(%i = 0; (%id = GetWord(%list, %i)) != -1; %i++)
							Client::sendMessage(%TrueClientId, $MsgBeige, Client::getName(%id));
						UseSkill(%TrueClientId, $SkillSurvival, False, True);
					} else Client::sendMessage(%TrueClientId, $MsgRed, "[none]");
				} else {
					if(%timeBetweenSkillUse < 5) {
						Client::sendMessage(%TrueClientId, $MsgRed, "You can't #inspect named targets for another " @ 5 - %timeBetweenSkillUse @ " seconds.");
						return %processed;
					}
					storeData(%TrueClientId, "LastSurvivalSkillTime", %time);
					%id = NEWgetClientByName(%cropped);
					if(%id != "" && %id > 2048) {
						%msg = "<f2>" @ Client::getName(%id) @ "\n";
						%msg = %msg @ "<f0>LVL " @ fetchData(%id, "LVL") @ " ";
						%msg = %msg @ fetchData(%id, "CLASS") @ " ";
						%msg = %msg @ fetchData(%id, "RACE") @ " ";
						if(fetchData(%id, "invisible"))
							%msg = %msg @ " (Invisible)";
						%pos = GameBase::getPosition(%id);
						%msg = %msg @ "<f1> @ <f0>" @ floor(vecx(%pos)) @ ", " @ floor(vecy(%pos)) @ ", " @ floor(vecz(%pos)) @ "\n";
						%msg = %msg @ "<f1>Attack<f2>     " @ fetchData(%id, "ATK") @ "\n";
						%msg = %msg @ "<f1>Armor<f2>      " @ fetchData(%id, "DEF") @ "\n";
						%msg = %msg @ "<f1>Resistance<f2>  " @ fetchData(%id, "MDEF") @ "\n";						
						%msg = %msg @ "<f1>Health<f2>      " @ floor(fetchData(%id, "HP")) @ " | " @ floor(fetchData(%id, "MaxHP")) @ "\n";
						%msg = %msg @ "<f1>Mana <f2>      " @ floor(fetchData(%id, "MANA")) @ " | " @ floor(fetchData(%id, "MaxMANA")) @ "\n";
						%msg = %msg @ "<f1>Weight <f2>    " @ floor(fetchData(%id, "Weight")) @ " | " @ floor(fetchData(%id, "MaxWeight")) @ "\n";
						%msg = %msg @ "<f1>Coins <f2>      " @ floor(fetchData(%id, "COINS")) @ "\n";
						%msg = %msg @ "<f1>Bounty <f2>    " @ floor(fetchData(%id, "bounty")) @ "\n";
						%msg = %msg @ "<f1>Favor <f2>     " @ floor(fetchData(%id, "FAVOR")) @ "\n";
						%wielding = Player::getMountedItem(%id, $WeaponSlot);
						%wearing = Player::getMountedItem(%id, $WeaponSlot);											
						%wearing = -1;
						%list = GetAccessoryList(%id, 2, "3 7");
						for(%i = 0; (%w = getCroppedItem(GetWord(%list, %i))) != -1; %i++) {
							if($AccessoryVar[%w, $AccessoryType] == $BodyAccessoryType)
								%wearing = %w;
						}
						if(%wielding != -1 || %wearing != -1) {
							%msg = %msg @ "<f1>Equipped:<f2>   ";
							if (%wielding != -1) 		%msg = %msg @ %wielding @ " ";
							else if(%wearing != -1)		%msg = %msg @ %wearing;
						}
						rpg::longPrint(%TrueClientId, %msg, 0, floor(String::len(%msg) / 20));
						UseSkill(%TrueClientId, $SkillSurvival, False, True);
					} else Client::sendMessage(%TrueClientId, 0, "Could not find anything to inspect.");
				}
			} else Client::sendMessage(%TrueClientId, 0, "You can #inspect zone | zone players | zone enemies | SpecificName");
		} else {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #inspect because you lack the necessary skills.");
		}
	}
	else if (%w1 == "#acrobatics") {
		%ticks = 5 * Cap(%skillLevel / 250, 1, 5);
		if(%timeBetweenSkillUse < (%ticks * 0.75) && !Player::isAIControlled(%TrueClientId)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't use #acrobatics for another " @ floor((%ticks * 0.75) - %timeBetweenSkillUse) @ " seconds.");
			return %processed;
		}
		if(SkillCanUse(%TrueClientId, "#acrobatics")) {
			storeData(%TrueClientId, "LastSurvivalSkillTime", %time);
			if(AddBonusStatePoints(%TrueClientId,"ACRO") <= 0)
				UseSkill(%clientId, $SkillSurvival, False, True);
			
			UpdateBonusState(%TrueClientId, "ACRO 1", %ticks, 1);
			playSound(ActivateAR, GameBase::getPosition(%TrueClientId));
			Client::sendMessage(%TrueClientId, $MsgGreen, "You ignore impact damage for the next " @ (%ticks * 2) @ " seconds.");
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't use #acrobatics because you lack the necessary skills.");
	}
	else if (%w1 == "#leap") {
		if(%timeBetweenSkillUse < 4 && !Player::isAIControlled(%TrueClientId)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #leap for another " @ floor(4 - %timeBetweenSkillUse) @ " seconds.");
			return %processed;
		}
		if(SkillCanUse(%TrueClientId, "#leap")) {			
			if(fetchData(%TrueClientId, "Leaping") < 1) {
				storeData(%TrueClientId, "LastSurvivalSkillTime", %time);
				UseSkill(%clientId, $SkillSurvival, False, True);
				storeData(%TrueClientId, "Leaping", 2);
				
				%upvel = Cap(%skillLevel / 3, 96, 256);
				%downvel = -(%upvel*2);
				rpg::ProcessLeapSkill(%TrueClientId, %upvel, %downvel, 0);
			}
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #leap because you lack the necessary skills.");
	}
	else if (%w1 == "#exploit") {		
		if(%timeBetweenSkillUse < 30) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #exploit for another " @ floor(30 - %timeBetweenSkillUse) @ " seconds.");
			return %processed;
		}
		if(SkillCanUse(%TrueClientId, "#exploit")) {
			if (AddBonusStatePoints(%TrueClientId,"Luck") > 0) {
				Client::sendMessage(%TrueClientId, $MsgBeige, "You are currently using #exploit.");
			} else {
				storeData(%TrueClientId, "LastSurvivalSkillTime", %time);
				UseSkill(%clientId, $SkillSurvival, False, True);
				%survivalSkills = %skillLevel;
				%ticks = floor(Cap(10 * (1.0 - cos(Cap((%survivalSkills - 500) / 10, 20, 60))), 3, 10));
				UpdateBonusState(%TrueClientId, "Luck 1", %ticks, %survivalSkills);
				playSound(ActivateAR, GameBase::getPosition(%TrueClientId));
				Client::sendMessage(%TrueClientId, $MsgGreen, "Your " @ $SkillDesc[$SkillSurvival] @ " skills have been added to your " @ $SkillDesc[$SkillLuck] @ " for " @ floor(%ticks * 2) @ " seconds.");
			}
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #exploit your " @ $SkillDesc[$SkillSurvival] @ " skills for extra " @ $SkillDesc[$SkillLuck] @ " because you lack the necessary skills.");
	}
	else if (%w1 == "#brace") {
		%recovery = 12 - Cap(%skillLevel / 150, 1, 6);
		if(%timeBetweenSkillUse < %recovery && !Player::isAIControlled(%TrueClientId)) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #brace for impact for another " @ floor(%recovery - %timeBetweenSkillUse) @ " seconds.");
			return %processed;
		}
		if(SkillCanUse(%TrueClientId, "#brace")) {
			if (AddBonusStatePoints(%TrueClientId,"BRACE") <= 0) {
				storeData(%TrueClientId, "LastSurvivalSkillTime", %time);				
				UpdateBonusState(%TrueClientId, "BRACE 1", 1 + (%recovery / 5), 2);
				playSound(ActivateTD, GameBase::getPosition(%TrueClientId));
				Client::sendMessage(%TrueClientId, $MsgGreen, "You #brace for impact...");
			}
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #brace for impact because you lack the necessary skills.");
	}
	else if (%w1 == "#totem") {
		if(%timeBetweenSkillUse < 30) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't construct another #totem for " @ floor(30 - %timeBetweenSkillUse) @ " seconds.");
			return %processed;
		}
		if(SkillCanUse(%TrueClientId, "#totem") && !Player::isAIControlled(%TrueClientId)) {
			if (AddBonusStatePoints(%TrueClientId,"TOTEM") > 0) {
				Client::sendMessage(%TrueClientId, $MsgBeige, "You are currently benefiting from your #totem.");
			} else if(fetchData(%TrueClientId, "TOTEMInProg") == 1) {
				Client::sendMessage(%TrueClientId, $MsgBeige, "You are already constructing a #totem.");
			} else {
				storeData(%TrueClientId, "LastSurvivalSkillTime", %time);
				storeData(%TrueClientId, "TOTEMInProg", 1);
				%pos = GameBase::getPosition(%TrueClientId);
				%group = newObject("SurvivalTotem" @ %TrueClientId, SimGroup);
				addToSet("MissionCleanup", %group);
				
				Client::setControlObject(%TrueClientId, Client::getObserverCamera(%TrueClientId));
				Observer::setOrbitObject(%TrueClientId, Client::getOwnedObject(%TrueClientId), 4, 4, 4);
				Player::setAnimation(%TrueClientId, 42);
				
				Client::sendMessage(%TrueClientId, $MsgBeige, "You begin to construct a #totem.");
				schedule("CreateTotem(" @ %TrueClientId @ ", 1, \"" @ %pos @ "\");", 1, %group);
				schedule("CreateTotem(" @ %TrueClientId @ ", 2, \"" @ %pos @ "\");", 4, %group);
				schedule("Client::setControlObject(" @ %TrueClientId @ ", " @ %TrueClientId @ ");", 8);
				schedule("CreateTotem(" @ %TrueClientId @ ", 3, \"" @ %pos @ "\");", 9, %group);
				schedule("CreateTotem(" @ %TrueClientId @ ", 4, \"" @ %pos @ "\");", 39, %group);
				
				schedule("Player::applyImpulse(" @ %TrueClientId @ ", \"0 0 10\"); GameBase::setRotation(" @ %TrueClientId @ ", \"0 -0 1\");", 2);
				schedule("Player::applyImpulse(" @ %TrueClientId @ ", \"0 0 20\"); GameBase::setRotation(" @ %TrueClientId @ ", \"0 -0 2\");", 3);
				schedule("Player::applyImpulse(" @ %TrueClientId @ ", \"0 0 30\"); GameBase::setRotation(" @ %TrueClientId @ ", \"0 -0 3\");", 4);
				schedule("Player::applyImpulse(" @ %TrueClientId @ ", \"0 0 40\"); GameBase::setRotation(" @ %TrueClientId @ ", \"0 -0 4\");", 5);
				schedule("Player::applyImpulse(" @ %TrueClientId @ ", \"0 0 50\"); GameBase::setRotation(" @ %TrueClientId @ ", \"0 -0 3\");", 6);
				schedule("Player::applyImpulse(" @ %TrueClientId @ ", \"0 0 60\"); GameBase::setRotation(" @ %TrueClientId @ ", \"0 -0 2\");", 7);
				schedule("Player::applyImpulse(" @ %TrueClientId @ ", \"0 0 70\"); GameBase::setRotation(" @ %TrueClientId @ ", \"0 -0 1\");", 8);
				
			}
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't construct a #totem because you lack the necessary skills.");
	}
	else if (%w1 == "#sacrifice") {
		if(%timeBetweenSkillUse < 5) {
			Client::sendMessage(%TrueClientId, $MsgRed, "You can't #sacrifice for another " @ floor(5 - %timeBetweenSkillUse) @ " seconds.");
			return %processed;
		}		
		if(SkillCanUse(%TrueClientId, "#sacrifice")) {
			if (AddBonusStatePoints(%TrueClientId,"SCRFC") > 0) {
				Client::sendMessage(%TrueClientId, $MsgBeige, "Your #sacrifice is still effective.");
			} else {
				UseSkill(%clientId, $SkillSurvival, False, True);
				Client::sendMessage(%TrueClientId, $MsgBeige, "You #sacrifice your mana for health...");
				%multiplier = fetchData(%TrueClientId, "MANA") / fetchData(%TrueClientId, "MaxMANA");
				if(%multiplier <= 0.1) {
					playNextAnim(%TrueClientId);
					Player::Kill(%TrueClientId);
					Client::sendMessage(%TrueClientId, $MsgRed, "...and kill yourself in the process.");
				} else {					
					%multiplier = ((Math::log(%multiplier) + 1) * 2) + 1;
					storeData(%TrueClientId, "LastSurvivalSkillTime", %time);
					Client::sendMessage(%TrueClientId, $MsgGreen, "...and gain +" @ floor((%multiplier - 1) * 100) @ "% maximum health multiplier.");
					UpdateBonusState(%TrueClientId, "SCRFC 1", 15, %multiplier);
					playSound(ActivateAR, GameBase::getPosition(%TrueClientId));
					refreshMANA(%clientId, 0);
					refreshMANAREGEN(%clientId);
					refreshHP(%clientId);
				}
			}
		} else Client::sendMessage(%TrueClientId, $MsgRed, "You can't #sacrifice your mana because you lack the necessary skills.");
	}	
	else {
		%processed = False;
	}
	
	return %processed;
}

//________________________________________________________________________________________________________________________________________________________________
function rpg::ProcessLeapSkill(%cl, %upvel, %downvel, %stage) {
	if(!IsDead(%cl)) {
		if(fetchData(%cl, "Leaping") > 0) {
			if(%stage == 0) {
				%pos = GameBase::getPosition(%cl);
				playSound(SoundMortarFire, %pos);				
				CreateAndDetBomb(%cl, DustPlumeFX, NewVector(vecx(%pos),vecy(%pos),vecz(%pos)+1), false);
				Player::applyImpulse(%cl, "0 0 " @ %upvel );
				schedule("rpg::ProcessLeapSkill(" @ %cl @ ", \"" @ %upvel @ "\", \"" @ %downvel @ "\", 1);", 0.5);
				return;
			}
			else if(%stage == 1) {
				Player::applyImpulse(%cl,"0 0 10");
				schedule("rpg::ProcessLeapSkill(" @ %cl @ ", \"" @ %upvel @ "\", \"" @ %downvel @ "\", 2);", 0.15);
				schedule("rpg::ProcessLeapSkill(" @ %cl @ ", \"" @ %upvel @ "\", \"" @ %downvel @ "\", 2);", 0.30);
				schedule("rpg::ProcessLeapSkill(" @ %cl @ ", \"" @ %upvel @ "\", \"" @ %downvel @ "\", 3);", 0.45);
				return;
			}
			else if(%stage == 2) {
				Player::applyImpulse(%cl,"0 0 10");
				return;
			}
			else if(%stage == 3) {
				%pos = GameBase::getPosition(%cl);
				if(!getLOSinfo(%pos, vector::add(%pos, "0 0 -4"), 0xFF)) {
					storeData(%cl, "Leaping", 1);
					playSound(ActivateAB, %pos);
					Player::applyImpulse(%cl, "0 0 " @ %downvel);
					schedule("rpg::ProcessLeapSkill(" @ %cl @ ", \"" @ %upvel @ "\", \"" @ %downvel @ "\", 3);", 2);
				} else {
					storeData(%cl, "Leaping", "");
				}
				return;
			} 
		}
	}
	%player = Client::getOwnedObject(%cl);
	if(%player != -1)
		storeData(%cl, "Leaping", "");
}

function DoCampSetup(%clientId, %step, %pos)
{

	if(%pos != "" && %step != 5) {
		if(Vector::getDistance(GameBase::getPosition(%clientId), %pos) > 10) {
			if(GameBase::getPosition(Group::getObject("MissionCleanup/Camp" @ %clientId, 0)) != "0 0 0") {
				Client::sendMessage(%clientId, $MsgRed, "You have wandered too far from your camp while setting it up.");
				%step = 5;
			} else return;
		}
	}

	if(%step == 1) {
		%object = newObject("wood", InteriorShape, "wood.dis");
		addToSet("MissionCleanup\\Camp" @ %clientId, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 1, vecy(%pos), vecz(%pos)));
	}
	else if(%step == 2) {
		%old = nameToId("MissionCleanup\\Camp" @ %clientId @ "\\wood");
		deleteObject(%old);
		%object = newObject("woodfire", InteriorShape, "woodfire.dis");
		addToSet("MissionCleanup\\Camp" @ %clientId, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 1, vecy(%pos), vecz(%pos)));
	}
	else if(%step == 3) {
		%object = newObject("tent", InteriorShape, "tent.dis");
		addToSet("MissionCleanup\\Camp" @ %clientId, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 5, vecy(%pos), vecz(%pos)));
	}
	else if(%step == 4) {
		%object = newObject("sleepzone", Trigger, GroupTrigger);
		addToSet("MissionCleanup\\Camp" @ %clientId, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 8, vecy(%pos), vecz(%pos) + 2));
		UseSkill(%clientId, $SkillSurvival, True, True);
		Client::sendMessage(%clientId, $MsgBeige, "Finished setting up camp. Use #camp a second time to pack up.");
	}
	else if(%step == 5) {
		%g = "MissionCleanup/Camp" @ %clientId;		
		RefreshAll(%clientId);
		Group::iterateRecursive(%g, GameBase::setPosition, "0 0 0");
		%gg = nameToId(%g);
		schedule("deleteObject(" @ %gg @ ");", 5);
	}
}

ExplosionData DustBoom {
	shapeName = "dustplume.dts";
	soundId   = shockExplosion;
	faceCamera = true;
	randomSpin = true;
	hasLight   = false;
	timeZero = 0.150;
	timeOne  = 0.750;
	radFactors = { 0.0, 1.0, 0.0 };
	shiftPosition = True;
};
MineData DustPlumeFX
{
	mass = 0.3;	drag = 1.0;	density = 2.0;	elasticity = 0.15;	friction = 1.0;
	className = "Handgrenade";	description = "Handgrenade";	shapeFile = "dustplume";	shadowDetailMask = 4;
	explosionId = DustBoom;	explosionRadius = 5.0;	damageValue = 1.0;	damageType = $NullDamageType;	kickBackStrength = 0;
	triggerRadius = 0.5;	maxDamage = 1.0;
};
function DustPlumeFX::onAdd(%this) { schedule("Mine::Detonate(" @ %this @ ");", 0.22, %this); }


function CreateTotem(%clientId, %step, %pos) {
	if(%pos != "" && %step != 4) {
		if(Vector::getDistance(GameBase::getPosition(%clientId), %pos) > 10) {
			if(GameBase::getPosition(Group::getObject("MissionCleanup/SurvivalTotem" @ %clientId, 0)) != "0 0 0") {
				Client::sendMessage(%clientId, $MsgRed, "You have wandered too far from your #totem site.");
				%step = 4;
			} else return;
		}
	}

	if(%step == 1) {		
		%object = newObject("pwood_spike_s", InteriorShape, "pwood_spike_s.dis");
		addToSet("MissionCleanup\\SurvivalTotem" @ %clientId, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 1, vecy(%pos), vecz(%pos)));
		
	} else if(%step == 2) {
		%object = newObject("panelstand", StaticShape, VerticalPanel);
		addToSet("MissionCleanup\\SurvivalTotem" @ %clientId, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 1, vecy(%pos), vecz(%pos) + 1));
	} else if(%step == 3) {
		%object = newObject("flag", StaticShape, BaseTribesFlag);
		addToSet("MissionCleanup\\SurvivalTotem" @ %clientId, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 1, vecy(%pos), vecz(%pos) + 2));
		
		playSound(ActivateAR, GameBase::getPosition(%clientId));
		
		%oldRegenRate = GetManaRegenRate(%clientId) * 100;
		UpdateBonusState(%clientId, "TOTEM 1", 15, %skillLevel);
		RefreshAll(%clientId);
		%newRegenRate = GetManaRegenRate(%clientId) * 100;
		storeData(%clientId, "TOTEMInProg", 0);
		UseSkill(%clientId, $SkillSurvival, True, True);
		Client::sendMessage(%clientId, $MsgGreen, "The #totem is complete. Your mana regenerates " @ RoundToFirstDecimal(%newRegenRate / %oldRegenRate) @ "x faster.");
	} else if(%step == 4) {
		UpdateBonusState(%clientId, "TOTEM 1", 0, 0);
		RefreshAll(%clientId);		
		deleteObject(nameToId("MissionCleanup/SurvivalTotem" @ %clientId));
	}
}
