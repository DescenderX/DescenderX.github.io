//_______________________________________________________________________________________________________________________________
// DescX Notes 
// 		Misc functions & Game events added for World Ender
//_______________________________________________________________________________________________________________________________

//_______________________________________________________________________________________________________________________________
function rpg::GetItemCount(%clientId, %item) {
	%count=0;
	if(isBeltItem(%item)) {
		%type = $BeltItem[%item, "Type"];
		%count = Belt::ItemCount(%item, fetchData(%clientId,%type));
	} else {
		if(!IsDead(%clientId))
			%count = Player::getItemCount(%clientId,%item);
	}
	return %count;
}

//_______________________________________________________________________________________________________________________________
function rpg::ApplyLuckFactorToDropList(%clientId, %killerid, %lootString) {
	if(Player::isAiControlled(%clientId) && !Player::isAiControlled(%killerid)) {
		%luckfactor = Cap((GetSkillWithBonus(%killerid,$SkillLuck) + 100) / 10, 1, 200);
		for(%x=0;(%item=getword(%lootString,%x)) != -1; %x+=2){
			%count = getword(%lootString,%x+1);
			%dropCount = %count;						
			if(%dropCount > 0 && !($ItemDropFlags[%item] & $ItemDropIgnoreLuck)) {
				%random = getRandom() * 100;
				if(%luckfactor < %random) {
					%dropCount = Cap(round(%dropCount * (%luckfactor/100)), 1, Cap(floor(%dropCount * 2),1,"inf"));
				}
			}
			if($ProjectileQuantityDropDampen[%item] > 0) {
				%dropCount = Cap(floor(%count / $ProjectileQuantityDropDampen[%item]),1,"inf");
			}
			if(%dropCount != %count)
				%lootString = String::replace(%lootString, %item @ " " @ %count, %item @ " " @ %dropCount);
		}
	}
	return %lootString;
}

//_______________________________________________________________________________________________________________________________
function rpg::getGender(%id) {
	%gender = $GenderForRace[fetchData(%id,"RACE")];
	if(%gender == 2) return "Male";
	if(%gender == 3) return "Female";
	return "";
}

//_______________________________________________________________________________________________________________________________
function rpg::PossessTarget(%clientId, %target, %allowAdmin, %force) {
	if(IsInCommaList(fetchData(%target, "PersonalPetList"), %clientId)) {
		// Flip client - we are currently possessing a minion and want to switch back
		%temp = %target;
		%target = %clientId;
		%clientId = %temp;
	}
	
	if(%force || IsInCommaList(fetchData(%clientId, "PersonalPetList"), %target) || (%allowAdmin && floor(%target.adminLevel) < 5)) {
		if((!%allowAdmin && %target.adminLevel > 0) || floor(%target.adminLevel) >= floor(%clientId.adminLevel))
			Client::sendMessage(%clientId, 0, "Could not process command: Target admin clearance level too high.");
		else if(%target != -1) {
			if(!IsDead(%target)) {
				//revert
				Client::setControlObject(%clientId.possessId, %clientId.possessId);
				Client::setControlObject(%clientId, %clientId);
				storeData(%clientId.possessId, "dumbAIflag", "");
				$possessedBy[%clientId.possessId] = "";

				//possess
				if(Player::isAiControlled(%target)) {
					storeData(%target, "dumbAIflag", True);
					AI::setVar(fetchData(%target, "BotInfoAiName"), SpotDist, 0);
					AI::newDirectiveRemove(fetchData(%target, "BotInfoAiName"), 99);
				}
				%clientId.possessId = %target;
				$possessedBy[%target] = %clientId;
				Client::setControlObject(%target, -1);
				Client::setControlObject(%clientId, %target);
			} else Client::sendMessage(%clientId, 0, "Target client is dead.");
		} else Client::sendMessage(%clientId, 0, "Invalid player name.");
	}
}

//_______________________________________________________________________________________________________________________________
function rpg::RevertPossession(%clientId) {
	revertControls(%clientId);
	Client::setControlObject(%clientId.possessId, %clientId.possessId);
	Client::setControlObject(%clientId, %clientId);
	storeData(%clientId.possessId, "dumbAIflag", "");
	$possessedBy[%clientId.possessId] = "";
	if(Player::isAIcontrolled(%clientId.possessId)) {
		%an = fetchData(%clientId.possessId, "BotInfoAiName");
		%id = AI::getId(%an);
		AI::SetSpotDist(%id);
		AI::SelectMovement(%an);
	}
	%clientId.possessId = "";
	%clientId.eyesing = "";
}

//_______________________________________________________________________________________________________________________________
function rpg::AllowRemort(%clientId,%specifyBaseLevel) {
	if(Player::isAiControlled(%clientId) || %clientId < 2049)
		return -1;
	%lvl 	= GetLevel(fetchData(%clientId, "EXP"), %clientId);
	%remort = fetchData(%clientId, "RemortStep");
	%cr		= fetchData(%clientId, "currentlyRemorting");
	if(%specifyBaseLevel != "") 	%maxlvl = %specifyBaseLevel;
	else 							%maxlvl = 100;
	
	if (!%cr) {
		return %lvl - (%maxlvl + (%remort * 10));
	}
	return -1;
}

//_______________________________________________________________________________________________________________________________
function rpg::ResistSkillRoll(%skill, 	%attackerId, %attackerSkillBase, %attackerDivisorScale, %attackerSkillBonus, 
										%targetId, %targetSkillResist, %targetDivisorScale, %targetResistanceBonus) {
	if(%attackerDivisorScale == 0)	%attackerDivisorScale = 1;
	if(%targetDivisorScale == 0)	%targetDivisorScale = 1;
	%divisor			= $SkillType[%skill];
	%attackerSkill 		= (%attackerSkillBonus + GetSkillWithBonus(%attackerId, %attackerSkillBase)) / ((%divisor / %attackerDivisorScale)+1);
	%targetResistance 	= (%targetResistanceBonus + GetSkillWithBonus(%targetId, %targetResistance)) / ((%divisor / %targetDivisorScale)+1);
	
	return (getRandom() * %attackerSkill >= getRandom() * %targetResistance);
}

//_______________________________________________________________________________________________________________________________
function rpg::NPCFullHealTarget(%clientId, %theBot, %response, %keyword){
	%fullHeal = $Spell::index[fullheal];
	playSound($Spell::startSound[%fullHeal], GameBase::getPosition(%clientId));
	schedule("playSound($Spell::endSound[" @ %fullHeal @ "], GameBase::getPosition(" @ %clientId @ "));", 1, %clientId);
	setHP(%clientId, fetchData(%clientId, "MaxHP"));
	return "";
}

//_______________________________________________________________________________________________________________________________
function rpg::BotDance(%clientId, %theBot, %response, %keyword) {
	GameBase::playSequence(%theBot, 0, "celebration " @ (floor(Cap(getRandom()*5,1,3))));
	return "";
}

//_______________________________________________________________________________________________________________________________
function rpg::NPCKillAndJailTarget(%clientId, %theBot, %response, %keyword) {
	if(GameBase::getMapName(%theBot) == "Slicer") {
		Client::sendMessage(%clientId, $MsgRed, GameBase::getMapName(%theBot) @ " kills you!");
		storeData(%clientId, "DeathByThugs", 1);
	} else {
		Client::sendMessage(%clientId, $MsgRed, GameBase::getMapName(%theBot) @ " kills you for your crimes!");
		storeData(%clientId, "DeathByLawman", 1);
	}
	Player::Kill(%clientId);	
	return -2;
}


//____________________________________________________________________________________________________________________________________________________
function rpg::FindShakedownTarget(%clientId, %bot, %response, %keyword) {
	%name = rpg::getName(%clientId);
	%targetBot = $ShakedownTarget[%name];
	if(%targetBot==""){
		for(%x=0;(%bot=getword($TownBotList,%x)) != -1; %x++) {
			if($BotInfo[%bot.name, NAME] == "Weather Device" || $BotInfo[%bot.name, RACE] == "Keeper")
				continue; // never deliver shakedown non-sentients
			if(%targetBot == "" || getRandom() < 0.13)
				%targetBot = %bot;
		}
		$ShakedownWinnings[%name] = floor(getRandom() * fetchData(%clientId, "EXP"));
		$ShakedownTarget[%name] = %targetBot;
	}
	return "+" @ String::replace(%response, "%BOT%", GameBase::getMapName($ShakedownTarget[%name]));
}

//_______________________________________________________________________________________________________________________________
// Create & signal that the device is disabled with asburd requirements of 999999
function rpg::DisableAllWeatherDevices() {
	$WeatherDevices = Array::New("", "WeatherDeviceRequirements");
	Array::Fill($WeatherDevices,
				"WeatherDevice MagicDust Opal Ruby,"				@
				"WeatherDevice VigorousVial Iron Copper,"			@
				"WeatherDevice GreenSkin Jade Emerald,"				@
				"WeatherDevice ControlCrystal Aluminum Granite,"	@
				"WeatherDevice EnchantedStone Diamond Silver,"		@
				"WeatherDevice EnergyRock Turquoise Sapphire",		",");
	Array::Fill(Array::New("", $WeatherDevices @ "Enable0"), "60000,10000,10000", ",");
	Array::Fill(Array::New("", $WeatherDevices @ "Enable1"), "20000,20000,40000", ",");
	Array::Fill(Array::New("", $WeatherDevices @ "Enable2"), "40000,30000,20000", ",");
	Array::Fill(Array::New("", $WeatherDevices @ "Enable3"), "10000,40000,60000", ",");
	Array::Fill(Array::New("", $WeatherDevices @ "Enable4"), "30000,50000,50000", ",");
	Array::Fill(Array::New("", $WeatherDevices @ "Enable5"), "50000,60000,30000", ",");

	$WeatherDeviceJammerMax = 10000;
	$WeatherDeviceJammers = Array::New("", "WeatherDeviceJammers");
	Array::Fill($WeatherDeviceJammers,
					"MinotaurHorn SkeletonBone|" @
					"LoinCloth StolenGarments|" @
					"ImpClaw BloodyPentagram|" @
					"Obsidian BlackStatue|" @
					"AntanariMask DragonScale|" @
					"ClippedWing Cobalt", "|");

	for(%x=0;%x<$Array::Size[$WeatherDevices];%x++)
		Array::Insert(Array::New("", $WeatherDevices @ %x), 999999, 0, 2);
}

//_______________________________________________________________________________________________________________________________
// Enable a device by copying the required ore amounts
function rpg::WeatherDeviceEnable(%clientId, %device, %response, %keyword, %index, %whatStuff) {
	if(HasThisStuff(%clientId, %whatStuff)) {
		TakeThisStuff(%clientId, %whatStuff);
		RefreshAll(%clientId);
		PlaySound(SoundBleedStab, GameBase::getPosition(%clientId));
		schedule("PlaySound(SoundGlassBreak, GameBase::getPosition(" @ %clientId @ "));", 1.5);
		schedule("PlaySound(RockLauncherFire, GameBase::getPosition(" @ %device @ "));", 3);
		Array::Copy($WeatherDevices @ "Enable" @ %index, $WeatherDevices @ %index);
		return "";
	} return "inspect";
}

//_______________________________________________________________________________________________________________________________
// Dialog switching for weather device on "#say hello"
function rpg::WeatherDeviceGetState(%clientId, %device, %response, %keyword, %index) {
	for(%x=0;(%item=getword(Array::Get($WeatherDevices,%index),%x+1)) != -1; %x++) {
		%oreRequired = Array::Get($WeatherDevices @ %index, %x);		
		if (%oreRequired < 0)				return -2;
		else if (%oreRequired == 999999)	return "+[ This strange device clearly hasn't been used in some time... it's covered in dust... ]"; 	
		else if (%oreRequired > 0)			return "needore";
	} return "energized";
}

//_______________________________________________________________________________________________________________________________
// Check to see if the player can Equalize this device
$WeatherDeviceBoomSounds = Array::New("", "WDBoomSnd");
Array::Fill($WeatherDeviceBoomSounds, "SoundCloudExplosion rocketExplosion ExplodeLM debrisMediumExplosion mineExplosion debrisSmallExplosion SoundFlierCrash debrisLargeExplosion", " ");

$WeatherDeviceBoomFX = Array::New("", "WDBoomFX");
Array::Fill($WeatherDeviceBoomFX, "SpellFXvertlightningrings SpellFXfirebomb SpellFXfirebomb SpellFXcloud SpellFXcryostorm SpellFXmelt SpellFXboulder", " ");

function rpg::WeatherDeviceDestroy(%clientId, %device, %response, %keyword, %index) {
	if($EndOfTheWorld != 6)						return "+[ The skies aren't clear yet. ]";
	if(!HasThisStuff(%clientId, "Equalizer 1")) return "+[ ...it's probably best not to drop The Equalizer if you intend to use it... ]";
	Array::Insert($WeatherDevices @ %index, -1, 0, 2);
	rpg::longprint(%clientId,
	"\n\n[ You fire The Equalizer. The Weather Device begins to implode before your eyes as its atoms are neutralized with negative resonance. ]\n\n",
	1, 15); 
	
	MessageAll($MsgRed, "-----------------------------------------------------------------");
	MessageAll($MsgRed, rpg::getName(%clientId) @ " has destroyed a weather device!!!");
	MessageAll($MsgRed, "-----------------------------------------------------------------");
	
	%pos = vector::add("0 0 5", gamebase::getposition(%device));		
	for(%x=1;%x<=20;%x+=0.33) {
		schedule("PlaySound(Array::Get(\"WDBoomSnd\",Cap(floor(getRandom()*$Array::Size[\"WDBoomSnd\"]-1),0,\"inf\")), \"" @ %pos @ "\");", %x + 0.5);
		schedule("PlaySound(Array::Get(\"WDBoomSnd\",Cap(floor(getRandom()*$Array::Size[\"WDBoomSnd\"]-1),0,\"inf\")), \"" @ %pos @ "\");", %x);
		schedule("CreateSpellBomb(" @ %clientId @ ", Array::Get(\"WDBoomFX\",Cap(floor(getRandom()*$Array::Size[\"WDBoomFX\"]-1),0,\"inf\")), \"" @ %pos @ "\");", %x + 0.22);
	}
	schedule("deleteObject(" @ %device @ ");Array::Delete(" @ $WeatherDeviceObjects @ ", " @ %index @ ");", 19);
	
	%destroyed = 0;
	for(%z=0;%z<$Array::Size[$WeatherDevices];%z++) {
		if(Array::Get($WeatherDevices @ %z, 0) < 0)
			%destroyed++;
	}
	echo("Destroyed: " @ %destroyed);
	if(%destroyed == 6) {
		schedule("rpg::SetTheEndInMotion(666);", 21);
	}
	
	return -2;
}

//_______________________________________________________________________________________________________________________________
//
function rpg::SmithTheEqualizer(%clientId, %smith, %response, %keyword) {
	%stuff = $BotInfoChat["KeeperOfSolace", "!", "require"];
	if(!HasThisStuff(%clientId,%stuff))
		return "nohasstuff";
	TakeThisStuff(%clientId, %stuff);
	rpg::EqualizerLaunchSequence(%clientId);
	return "";
}


function rpg::EqualizerLaunchSequence(%clientId) {
	%forceLaunchPos = "-805.618 2911.15 760.5";
	%pos = gamebase::getposition(%clientId);
	%stage = fetchData(%clientId, "EqualizerLaunch");
	
	if(%stage==1) {
		PlaySound(SoundLevelUp, %pos);
	}
	else if(%stage==5) {
	//	$ForceManagedObjectCreation["Delkin Heights"] = true;
	//	rpg::UpdateManagedZoneObjects("Delkin Heights", 1);
		player::applyimpulse(%clientId, "-270 -1380 2000");
	}
	else if(%stage==13) {
		GiveThisStuff(%clientId, "Equalizer 1", true);
		PlaySound(SoundLevelUp, %pos);
	}
	else if(%stage==20) {
		Item::setVelocity(%clientId, "0 0 0");
		player::applyimpulse(%clientId, "0 0 -1000");
		//gamebase::setposition(%clientId, rpg::TraceForGround(%clientId, -3000));
		player::applyimpulse(%clientId, "50 -100 50");
	}
	else if(%stage == 25) {
	//	$ForceManagedObjectCreation["Delkin Heights"] = "";
		PlaySound(SoundLevelUp, %pos);
	}	

	if(%stage < 25) {
		if(%stage < 5) {
			gamebase::setposition(%clientId, %forceLaunchPos);
			Item::setVelocity(%clientId, "0 0 5");
		}
		%randomBoom = Array::Get("WDBoomSnd", Cap(floor(getRandom()*$Array::Size["WDBoomSnd"]-1),0,"inf"));
		PlaySound(%randomBoom, %pos);
		CreateSpellBomb(%clientId, SpellFXcloud, %pos);
		
		storeData(%clientId, "EqualizerLaunch", %stage + 1);
		schedule("rpg::EqualizerLaunchSequence(" @ %clientId @ ");", 1);
	} else {
		storeData(%clientId, "EqualizerLaunch", "");
	}
}

//_______________________________________________________________________________________________________________________________
// Destroy a Sealbroker
function rpg::BankerDestroy(%clientId, %broker, %response, %keyword) {
	if(rpg::IsTheWorldEnding() && HasThisStuff(%clientId, "Equalizer 1")) {
		rpg::longprint(%clientId, "\n\n[ You fire The Equalizer. The Sealbroker is vaporized. ]\n\n", 1, 15);		
		%pos = vector::add("0 0 5", gamebase::getposition(%broker));		
		for(%x=1;%x<=20;%x+=0.33) {
			schedule("PlaySound(Array::Get(\"WDBoomSnd\",Cap(floor(getRandom()*$Array::Size[\"WDBoomSnd\"]-1),0,\"inf\")), \"" @ %pos @ "\");", %x + 0.5);
			schedule("PlaySound(Array::Get(\"WDBoomSnd\",Cap(floor(getRandom()*$Array::Size[\"WDBoomSnd\"]-1),0,\"inf\")), \"" @ %pos @ "\");", %x);
			schedule("CreateSpellBomb(" @ %clientId @ ", Array::Get(\"WDBoomFX\",Cap(floor(getRandom()*$Array::Size[\"WDBoomFX\"]-1),0,\"inf\")), \"" @ %pos @ "\");", %x + 0.22);
		}
		$BrokersRemaining = string::replace($BrokersRemaining, %broker.name @ " ", "");
		deleteObject(%broker);
		SaveWorld();
	}
	return -2;
}

//_______________________________________________________________________________________________________________________________
// See if the device is accepting %item. If so, take all %item from the player and update the device.
function rpg::WeatherDeviceCrush(%clientId, %device, %response, %keyword, %index, %item) {
	for(%x=0;(%checkItem=getword(Array::Get($WeatherDevices,%index),%x+1)) != -1; %x++) {
		%oreRequired = Array::Get($WeatherDevices @ %index, %x);
		if (%oreRequired < 0) 
			return -2;
		else if(%item == %checkItem) {
			if (%oreRequired == 0) 
				return "+[ The device is already fully supplied with " @ %item @ ". ]";
			else break;
		}
	}
	//if(%x >= 3) { return -2;	// error
	
	%count = 0;	// Take everything the player has, even if it's too much. If they didn't query the device ahead of time, their loss!
	if((%ic=Belt::ItemCount(%item,fetchdata(%clientid,"AmmoItems"))) > 0)   { Belt::TakeThisStuff(%clientId, %item, %ic); %count += %ic; }
	if((%ic=Belt::ItemCount(%item,fetchdata(%clientid,"MiscItems"))) > 0)   { Belt::TakeThisStuff(%clientId, %item, %ic); %count += %ic; }
	if((%ic=Belt::ItemCount(%item,fetchdata(%clientid,"PotionItems"))) > 0) { Belt::TakeThisStuff(%clientId, %item, %ic); %count += %ic; }
	if((%ic=Belt::ItemCount(%item,fetchdata(%clientid,"GlassIdioms"))) > 0) { Belt::TakeThisStuff(%clientId, %item, %ic); %count += %ic; }
	if(!isBeltItem(%item) && ((%ic=Player::getItemCount(%clientId,%item)) > 0)) { TakeThisStuff(%clientId, %item @ " " @ %ic); %count += %ic; }
	if(%count > 0) {
		
		RefreshAll(%clientId);
		
		for(%i=0;(%jammer=getword(Array::Get($WeatherDeviceJammers, %index), %i)) != -1; %i++) {
			if(%jammer == %item) {
				PlaySound(ActivateBF, GameBase::getPosition(%device));
				schedule("PlaySound(SoundHitBF, GameBase::getPosition(" @ %device @ "));", 1.2);
				schedule("PlaySound(rocketExplosion, GameBase::getPosition(" @ %device @ "));", 0.7);
				schedule("PlaySound(shockExplosion, GameBase::getPosition(" @ %device @ "));", 2);
				
				if($Array::Data[$WeatherDevices @ %index, %i] < $WeatherDeviceJammerMax) {
					$Array::Data[$WeatherDevices @ %index, %i] += floor(%count);
					if($Array::Data[$WeatherDevices @ %index, %i] > $WeatherDeviceJammerMax)
						$Array::Data[$WeatherDevices @ %index, %i] = $WeatherDeviceJammerMax;
				}
				if($Array::Data[$WeatherDevices @ %index, %i+1] < $WeatherDeviceJammerMax) {
					$Array::Data[$WeatherDevices @ %index, %i+1] += floor(%count);
					if($Array::Data[$WeatherDevices @ %index, %i+1] > $WeatherDeviceJammerMax)
						$Array::Data[$WeatherDevices @ %index, %i+1] = $WeatherDeviceJammerMax;
				}
				return "+[ You feed all " @ %count @ " of the " @ %item @ " you are carrying into the device. It damages the device, which sputters and emits thick, dark smoke... ]";
			}
		}
		
		PlaySound(RockLauncherFire, GameBase::getPosition(%device));
		schedule("PlaySound(SoundInscribeFail, GameBase::getPosition(" @ %device @ "));", 1);
		$Array::Data[$WeatherDevices @ %index, %x] -= %count;
		if($Array::Data[$WeatherDevices @ %index, %x] <= 0) {
			$Array::Data[$WeatherDevices @ %index, %x] = 0;
		}

		for(%z=0;%z<$Array::Size[$WeatherDevices @ %index];%z++) {
			if(Array::Get($WeatherDevices @ %index, %z) != 0) break;
		}
		
		if(%z == 3) {
			$EndOfTheWorld++;
			if($EndOfTheWorld == 6)
				rpg::CreateKeeperOfSolace();
			rpg::UpdateWorldEndState();
			return "energized";
		} else if($Array::Data[$WeatherDevices @ %index, %x] <= 0) {
			return "+[ You feed all " @ %count @ " of the " @ %item @ " you are carrying into the device. No smoke appears. It is fully supplied with " @ %item @ ". ]";
		} else {
			return "+[ You feed all " @ %count @ " of the " @ %item @ " you are carrying into the device. A number appears as smoke: " @ Array::Get($WeatherDevices @ %index, %x) @ " ]";
		}
	}
	return "+[ You don't have any " @ %item @ " to feed into the device. ]";
}

function rpg::TestWeatherCycle(%state) {	
	if(%state==8) {
		$EndOfTheWorld = 0;
		rpg::UpdateWorldEndState();
	} else {
		$EndOfTheWorld = %state;
		rpg::UpdateWorldEndState();		
		schedule("rpg::TestWeatherCycle(" @ %state + 1 @ ");", 5);
	}
}

//_______________________________________________________________________________________________________________________________
function rpg::UpdateWorldEndState(%forceStage, %noVisChange) {
	if(%forceStage != "")
			%worldStateLevel = floor(Cap(%forceStage,0,7));
	else	%worldStateLevel = floor(Cap($EndOfTheWorld,0,7));
	
	rpg::UpdateServerName();
	
	if(isObject("weather")) deleteObject("weather");
	%intensity = Cap(getRandom(), 0.5, "inf");
	if(%worldStateLevel <= 0 || %worldStateLevel >= 7) {
		$WeatherStarField = newObject("stars", Starfield);
		//newObject("weather", Snowfall, 0, "0 0 0", 0, -1);
	} else if (%worldStateLevel <= 2) {		
		%vec = Vector::Add("0 0 " @ (-100 + (floor(getRandom() * 80))), Vector::Add("-1 -1 -1", Vector::Random(3)));
		newObject("weather", Snowfall, %intensity, %vec, 0, snow);	// Snow at first generator energizing
		if($WeatherStarField != "") 
			deleteObject($WeatherStarField);
		$WeatherStarField="";
	} else if(%worldStateLevel <= 4) {
		%vec = "-90 -50 " @ (-300 + (floor(getRandom() * 40)));
		newObject("weather", Snowfall, %intensity, %vec, 0, 1);		// "Hail" at stage 3
		if($WeatherStarField != "") 
			deleteObject($WeatherStarField);
		$WeatherStarField="";
	} else if(%worldStateLevel <= 6) {
		%vec = Vector::Add("0 0 " @ (-300 + (floor(getRandom() * 40))), Vector::Add("-1 -1 -1", Vector::Random(1.5)));
		newObject("weather", Snowfall, %intensity, %vec, 0, 1);		// "Rain" before stage 5		
		if($WeatherStarField != "") 
			deleteObject($WeatherStarField);
		$WeatherStarField="";
	}
	
	
	%group = nameToId("MissionGroup\\Landscape");
	if(%group != -1) {
		%count = Group::objectCount(%group);
		for(%i = 0; %i <= %count-1; %i++) {
			%object = Group::getObject(%group, %i);		
			if(%object != -1 && getObjectType(%object) == "Sky")
				deleteobject(%object);
		}
	}
	%newsky = newObject(Sky, Sky, 0, 0, 0, $EndWorldState[%worldStateLevel,"Sky"], 0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);
	addToSet(%group, %newsky);	
	if(%noVisChange != true) {
		setTerrainVisibility(0, $EndWorldState[%worldStateLevel,"Vis"], $EndWorldState[%worldStateLevel,"Haze"]);
		setTerrainVisibility(8, $EndWorldState[%worldStateLevel,"Vis"], $EndWorldState[%worldStateLevel,"Haze"]);
	}
	$LastWorldStateChange = $EndOfTheWorld;
	
	if(%forceStage != "")
		schedule("rpg::UpdateWorldEndState();", 1);
}

//_______________________________________________________________________________________________________________________________
function rpg::SetTheEndInMotion(%force) {
	if(%force == 666 || %force == 13) {
		$EndOfTheWorld = 7;
		rpg::UpdateWorldEndState();
	}
	
	if(!rpg::IsTheWorldEnding())
		return;
	
	%list = GetPlayerIdList();
	for(%i = 0; (%id=GetWord(%list, %i)) != -1; %i++)
		rpg::AddClientToWorldEnd(%id);		
	
	for(%i=0;(%bot=getword($TownBotList,%i)) != -1; %i++) {
		if($BotInfo[%bot.name, NAME] == "Sealbroker")
			$BrokersRemaining = %bot.name @ " " @ $BrokersRemaining;
	}
		
	
	
	if($WorldEndEventAt <= 0)
		$WorldEndEventAt = getSimTime() + 130;
									// ((60 * 60) * 4);			// default 4 hours
	
	deleteObject(nameToID("MissionGroup\\EnterBoxes"));
	deleteObject(nameToID("MissionGroup\\TeleportBoxes\\SyncroSeeQod"));

	$BlockManagedObjectCreation["Eviscera"] 			= true;
	$BlockManagedObjectCreation["Syncronicon"] 			= true;
		
	rpg::WorldEndSetZoneOnFire("Keldrin Town");
	rpg::WorldEndSetZoneOnFire("Jaten Outpost");
	rpg::WorldEndSetZoneOnFire("Fort Ethren");
	rpg::WorldEndSetZoneOnFire("Mercator");
	rpg::WorldEndSetZoneOnFire("Hazard");
	rpg::WorldEndSetZoneOnFire("Oasis");
	rpg::WorldEndSetZoneOnFire("Wellsprings");
	//rpg::WorldEndSetZoneOnFire("Wyzanhyde Priory");
	rpg::WorldEndSetZoneOnFire("Delkin Heights");

	rpg::WorldEndSetZoneOnFire("Jherigo Pass");
	rpg::WorldEndSetZoneOnFire("Orc Base");
	rpg::WorldEndSetZoneOnFire("Kymer Deadwood");
	rpg::WorldEndSetZoneOnFire("Old Ethren");
	rpg::WorldEndSetZoneOnFire("The Wastes");
	rpg::WorldEndSetZoneOnFire("Stronghold Yolanda");
	rpg::WorldEndSetZoneOnFire("Elven Outpost");
	rpg::WorldEndSetZoneOnFire("Altar Of Shame");
	rpg::WorldEndSetZoneOnFire("Overville");
	rpg::WorldEndSetZoneOnFire("Amazon Forest");
	rpg::WorldEndSetZoneOnFire("Traveller's Canyon");
	
	InitTownBots(true);	
	
	rpg::WorldEndMessage();
	rpg::WorldEndMeteor();
	
	rpg::UpdateWorldEndState("",true);
	SaveWorld();
}

//_____________________________________________________________________________________________________________________________________
// Remaining brokers to equalize
function rpg::BrokersRemaining() {
	for(%i=0;getword($BrokersRemaining,%i) != -1; %i++) { }
	return %i;
}
//_____________________________________________________________________________________________________________________________________
// After $WorldEndEventAt expires  OR  all players find a Banker  OR  all Bankers are equalized, DeterminePlayerFate
function rpg::FinishWorldEnd() {
	if($SupersonicDemondude == "")
		Player::Kill($SupersonicDemondude);
	%players = Array::New();
	Array::Fill(%players, $WorldEndParticipants, ",");
	Array::ForEach(%players, "rpg::DeterminePlayerFate");
	
	File::delete("temp\\" @ $missionName @ "_worldsave_.cs");
	deletevariables("$world::*");
	$world::CyclesCompleted = $CyclesCompleted+1;
	export("world::*", "temp\\" @ $missionName @ "_worldsave_.cs", false);
	
	schedule("quit();", 2);
}

//_____________________________________________________________________________________________________________________________________
// Determine a player's fate at the end of the world.
// Players who were not part of the event are not considered.
$EqualizerDeathNotice = "You FAILED to Equalize the world. You have been ERASED.";
function rpg::DeterminePlayerFate(%array, %index, %val) {	
	%name 				= %val;
	%id 				= newGetClientByName(%name);
	%playerIsEqualizer 	= false;
	
	if(%id<=0) {
		%saveFile = LoadCharacter(%name, true);
		%playerIsEqualizer = ((rpg::GetItemListCount($CharacterSaveData[0, 16], "Equalizer") > 0) ||
								(rpg::GetItemListCount($CharacterSaveData[0, 15], "Equalizer") > 0));
	} else {
		%playerIsEqualizer = rpg::PlayerIsEqualizer(%id);
	}
	
	%survivedThisCycle = false;

	//________________________________________________________________________________________________
	// Anyone holding The Equalizer
	if( %playerIsEqualizer ) {
		if(rpg::BrokersRemaining() > 0) {			
			%msg = $EqualizerDeathNotice;
		}
		else {
			%msg = "You SUCCEEDED. All Sealbrokers were Equalized. You remorted twice, and your skills will now advance faster!";
			%survivedThisCycle = true;
			if(%id != -1)					storeData(%id, "RemortStep", 2, "inc");				
			else if(%saveFile != "")		$CharacterSaveData[0, 21] = Cap($CharacterSaveData[0, 21] + 2, 0, "inf");
		}
	} 
	//________________________________________________________________________________________________
	// Ordinary players
	else {
		if(string::findSubStr($WorldEndParticipantsSaved, %val @ ",") != -1) {		
			%msg = "You were SAVED. Nothing was lost. You received 10 SP per level, and your skills will now advance faster!";
			%survivedThisCycle = true;
			if(%id != -1) {
				%lvl = GetLevel(fetchData(%id, "EXP"), %id);
				storeData(%id, "SPcredits", %lvl * 10, "inc");
			} else if(%saveFile != "") {
				%lvl = GetLevel($CharacterSaveData[0, 28], %id);
				$CharacterSaveData[0, 28] += (%lvl * 10);
			}
		}
		else if(rpg::BrokersRemaining() > 0) {
			%msg = "You have SURVIVED. The world ended, but Sealbrokers remained alive. You lost your equipment and coins on hand, but your skills will now advance faster!";
			%survivedThisCycle = true;
			if(%id != -1) {											
				%max = getNumItems();
				for(%i = 0; %i < %max; %i++) {
					%checkItem = getItemData(%i);
					if(Player::getItemCount(%id, %checkItem))
						Player::setItemCount(%id, %checkItem, 0);
				}
				storeData(%id, "COINS", 0);
				for(%x=0;%x<$Array::Size[$BeltItemNames];%x++)
					storeData(%id, Array::Get($BeltItemNames,%x), "");
			} else if(%saveFile != "") {
				$CharacterSaveData[8, 1] 	= "";
				$CharacterSaveData[8, 3] 	= "";
				$CharacterSaveData[8, 5] 	= "";
				$CharacterSaveData[8, 7] 	= "";
				$CharacterSaveData[0, 4] 	= "";
				$CharacterSaveData[0, 15] 	= "";
			}
		} 
		else {
			%msg = "You have DIED. The world ended. You've lost everything, except for your experience!";
			if(%id != -1) {
				%max = getNumItems();
				for(%i = 0; %i < %max; %i++) {
					%checkItem = getItemData(%i);
					if(Player::getItemCount(%id, %checkItem))
						Player::setItemCount(%id, %checkItem, 0);
				}
				storeData(%id, "RemortStep", 1, "dec");
				if(fetchData(%id, "RemortStep") < 0)
					storeData(%id, "RemortStep", 0);
				storeData(%id, "AmmoItems", "");
				storeData(%id, "MiscItems", "");
				storeData(%id, "PotionItems", "");
				storeData(%id, "GlassIdioms", "");
				storeData(%id, "EXP", 0);
			} else if(%saveFile != "") {
				$CharacterSaveData[8, 1] 	= "";
				$CharacterSaveData[8, 2] 	= "";
				$CharacterSaveData[8, 3] 	= "";
				$CharacterSaveData[8, 4] 	= "";
				$CharacterSaveData[8, 5] 	= "";
				$CharacterSaveData[8, 6] 	= "";
				$CharacterSaveData[8, 7] 	= "";
				$CharacterSaveData[8, 8] 	= "";
				$CharacterSaveData[0, 4] 	= "";
				$CharacterSaveData[0, 6] 	= "";
				$CharacterSaveData[0, 15] 	= "";
				$CharacterSaveData[0, 16] 	= "";
				$CharacterSaveData[0, 21] 	= Cap($CharacterSaveData[0, 21] - 1, 0, "inf");
			}
		}
	}
	
	if(%id != -1) {
		TakeThisStuff(%id, "Equalizer 1");
		storeData(%id, "LoginMessage", %msg);
		if(%survivedThisCycle)
			storeData(%id, "CyclesSurvived", 1, "inc");
		SaveCharacter(%id, true);
		newKick(%id, %msg);
	} else {
		if(%saveFile != "") {
			$CharacterSaveData[0, 15] 	= rpg::ModifyItemList($CharacterSaveData[0, 15], "Equalizer", "remove");
			$CharacterSaveData[0, 16] 	= rpg::ModifyItemList($CharacterSaveData[0, 16], "Equalizer", "remove");
			$CharacterSaveData[0, 34] 	= %msg;
			if(%survivedThisCycle)
				$CharacterSaveData[0, 35]++;
			export("CharacterSaveData*", "temp\\" @ %name @ ".cs", false);
		}
	}
}

//_______________________________________________________________________________________________________________________________
// A player is "saved" if they talk to a Sealbroker and choose "safety"
// ...but then, they are stuck in the Hall of Souls until the event ends
function rpg::SavePlayerFromTheEnd(%cl, %returnPos) {
	if(rpg::PlayerIsEqualizer(%cl)) {
		return "";
	}
	
	$WorldEndParticipantsSaved = rpg::getName(%cl) @ "," @ $WorldEndParticipantsSaved;
	%group 		= nameToID("MissionGroup/Teams/team0/DropPoints/OnDeath" );
	%count 		= Group::objectCount(%group);
	%spawnIdx 	= floor(getRandom() * (%count - 0.1));
	%value 		= %count;
	%best		= "";
	for(%i = %spawnIdx; %i < %value; %i++) {
		%set = newObject("set",SimSet);
		%obj = Group::getObject(%group, %i);
		if(%best == "" || %i == %spawnIdx || containerBoxFillSet(%set,$SimPlayerObjectType|$VehicleObjectType,GameBase::getPosition(%obj),2,2,4,0) == 0) {
			%best = %obj;
			if(%i != %spawnIdx) break;
		}
		deleteObject(%set);
	}
	GameBase::setPosition(%cl, GameBase::getPosition(%best));
	GameBase::setRotation(%cl, GameBase::getRotation(%best));
	SaveCharacter(%cl, true);
	rpg::longprint(%cl, "\n\n" @
						"\n  |+ You have made it to the Hall of Souls safely." @
						"\n  |+ Your level, items, and storage will be carried into the next cycle." @
						"\n  |+ You cannot leave until the next cycle begins in " @ rpg::GetRemainingEndTimeStr() @ 
						"\n\n\n",
	0, 60);
	if(%returnPos == "returnPos")
		return GameBase::getPosition(%best);
	return -2;
}

//_______________________________________________________________________________________________________________________________
// When the world end event begins, all players who spawn into the game at any point
// will be considered to be taking part in the event.
function rpg::AddClientToWorldEnd(%cl) {
	if(rpg::IsTheWorldEnding() && %cl > 0) {
		if(string::findSubStr($WorldEndParticipants, rpg::getName(%cl) @ ",") == -1)
			$WorldEndParticipants = rpg::getName(%cl) @ "," @ $WorldEndParticipants;
	}
}

//_______________________________________________________________________________________________________________________________
// See if the player chose safety at a Sealbroker
function rpg::IsPlayerSavedFromTheEnd(%cl) {
	return ((rpg::IsTheWorldEnding() && %cl > 0) && (string::findSubStr($WorldEndParticipantsSaved, rpg::getName(%cl) @ ",") != -1));
}

//_______________________________________________________________________________________________________________________________
// Convert objects in a zone to flames
function rpg::WorldEndSetZoneOnFire(%zone) {
	if(rpg::IsTheWorldEnding()) {
		function rpg::ConvertObjectsToFlames(%array, %index, %val, %zone) {
			Array::Insert(%val, "StaticShape", 	1);
			%fire[0]="GiantFlame";
			%fire[1]="MediumFlame";
			%fire[2]="MassiveFlame";
			%fire = %fire[floor(Cap(getRandom()*2.2,0,2))];
			Array::Insert(%val, %fire, 2);
			rpg::EnsureManagedCleanupExists(%zone);
			rpg::DestroyManagedObjects(%array, %index, %val);
			rpg::CreateManagedObjects(%array, %index, %val, %zone);
		}
		Array::ForEach($ManagedObjectListPrefix @ %zone, "rpg::ConvertObjectsToFlames", "\"" @ %zone @ "\"");
	}
}

//_______________________________________________________________________________________________________________________________
// Send a warning message to everyone that the world is ending, with a quick reminder about what to do!
function rpg::WorldEndMessage() {
	if(rpg::IsTheWorldEnding()) {
		%list = GetPlayerIdList();
		for(%i = 0; (%id=GetWord(%list, %i)) != -1; %i++) {
			Client::sendMessage(%id, $MsgRed, "------------------------------------------------------------------------");
			if(HasThisStuff(%id,"Equalizer 1")) {
				Client::sendMessage(%id, $MsgGreen, "The end of the world is in progress! Equalize every Sealbroker in Keldrin before the timer expires!");
			} else {
				Client::sendMessage(%id, $MsgRed, "The end of the world is in progress! Find a Soulbroker before the timer expires!");
			}
			
			if(Zone::getDesc(fetchData(%id,"zone")) == "Hall of Souls")
				Client::sendMessage(%id, $MsgGreen, "You are immune to all damage and cannot leave until the next cycle begins.");
			
			Client::sendMessage(%id, $MsgBeige, "Time remaining before the world ends: " @ rpg::GetRemainingEndTimeStr());
			Client::sendMessage(%id, $MsgRed, "------------------------------------------------------------------------");
		}
		schedule("rpg::WorldEndMessage();", Cap(150 * getRandom(),60,120));
	}
}

//_______________________________________________________________________________________________________________________________
// Return the time remaining for the world end event as a friendly string
function rpg::GetRemainingEndTimeStr() {
	%now 	= getSimTime();
	%hour 	= ($WorldEndEventAt - %now) / (60*60);
	%minute = (($WorldEndEventAt - %now) % (60*60)) /60;
	%hour = floor(%hour);
	%minute = floor(%minute);
	if(%hour != 1) %hplur = "s";
	if(%minute != 1) %mplur = "s";	
	if(%hour <= 0){
		if(%minute <= 0)
			return "!!! ANY MOMENT !!!";
		return %minute @ " minute" @ %mplur;
	}
	return %hour @ " hour" @ %hplur @ " and " @ %minute @ " minute" @ %mplur;
}


//_______________________________________________________________________________________________________________________________
// Rain fire down on players by spawning an invisible AI who teleports and launches flames! $SupersonicDemondude
function rpg::WorldEndMeteor() {
	if(rpg::IsTheWorldEnding()) {
		if($SupersonicDemondude == "" || IsDead($SupersonicDemondude)) {
			%ai = AI::Helper( "Firecaller", "Firecaller", "TempSpawn 0 0 0" );
			//echo(%ai);
			$SupersonicDemondude = AI::getId(%ai);
			Item::SetVelocity($SupersonicDemondude, "0 0 0");
			AI::SetSpotDist($SupersonicDemondude,0);
			storeData($SupersonicDemondude,"Frozen",1);
			GameBase::startFadeOut($SupersonicDemondude);
		}
		%list = GetPlayerIdList();
		Item::SetVelocity($SupersonicDemondude, "0 0 0");
		GameBase::SetPosition($SupersonicDemondude,"0 0 0");
		for(%i = 0; (%id=GetWord(%list, %i)) != -1; %i++) {
			%fireFrom	= Vector::Add(Vector::Random(42), Vector::add("0 0 200", GameBase::getPosition(%id)));
			%player 	= Client::getOwnedObject($SupersonicDemondude);
			%bomb 		= newObject("", "Mine", "SpellFXMeteor");
			addToSet("MissionCleanup", %bomb);			
			Item::SetVelocity($SupersonicDemondude, "0 0 0");
			GameBase::SetPosition($SupersonicDemondude,%fireFrom);
			GameBase::Throw(%bomb, %player, 0, false);
		}
		schedule("rpg::WorldEndMeteor();", 1);
	} else {
	//	if(!IsDead($SupersonicDemondude)) {
	//		Player::Kill($SupersonicDemondude);
	//	}
	}
}

//_______________________________________________________________________________________________________________________________
// When a $SupersonicDemondude meteor explodes, it will do direct damage over a wide area
function rpg::MeteorExplosion(%this) {
	%b 		= 30;
	%pos 	= GameBase::getPosition(%this);
	%set 	= newObject("set", SimSet);
	Mine::Detonate(%this);
	containerBoxFillSet(%set, $SimPlayerObjectType, %pos, %b, %b, %b, 0);
	Group::iterateRecursive(%set, rpg::DoMeteorDamage, %pos);
	deleteObject(%set);
}
function rpg::DoMeteorDamage(%object, %pos) {
	%id 	= Player::getClient(%object);	
	%dist 	= Vector::getDistance(%pos, GameBase::getPosition(%id));	
	%newDamage = rpg::GetSplashDamage(%dist, 15, 666, 3, 100);
	GameBase::virtual(%id, "onDamage", $DrainDamageType, %newDamage, "0 0 0", "0 0 0", "0 0 0", "torso", "front_right", $SupersonicDemondude, "hellstorm");
}

//_______________________________________________________________________________________________________________________________
// True if the world is CURRENTLY ending
function rpg::IsTheWorldEnding() { 
	return $EndOfTheWorld == 7; 
}

//_______________________________________________________________________________________________________________________________
// For bottalk::info dialogs
function rpg::IsTheWorldEndingDialog() { 
	if(rpg::IsTheWorldEnding()) 
		return "ending"; 
	return ""; 
}
function rpg::HasFavorDialog(%cl) { 
	if(fetchData(%cl,"FAVOR") <= 0) 
		return "nofavor"; 
	return ""; 
}
function rpg::PlayerIsEqualizer(%cl) {
	return (rpg::IsTheWorldEnding() && HasThisStuff(%cl, "Equalizer 1"));
}


//________________________________________________________________________________________________
// Will spawn a couple minutes behind Thieves that carry a large percentage of their STOLEN value in coins
function rpg::SpawnMandatoryLawman(%targetId) {
	%name 	= rpg::getName(%targetId);
	if($MandatoryLawmanChase[%name] > 0 || Player::isAIcontrolled(%targetId))
		return;
	
	%theft 	= fetchData(%targetId, "STOLEN");	
	%lvl 	= GetLevel(fetchData(%targetId, "EXP"), %targetId);	
	%weapon = "BroadSword";
	if(%lvl > 25)			%weapon = "LongSword";
	else if(%lvl > 50)		%weapon = "ElvenSword";
	else if(%theft > 75)	%weapon = "Claymore";
	%lvl = floor(%lvl + Cap(%theft / 1000, 1, 200));	
	
	if(getRandom() > 0.5) 	%armor = "BronzePlateMail";
	else					%armor = "PlateMail";
	$BotEquipment[generic] = "CLASS Bard LVL " @ %lvl @ " " @ %armor @ "0 1 " @ %weapon @ " 1";
	
	%an = AI::helper("generic", "MandatoryExecutor" @ %targetId, "TempSpawn " @ $ThiefPosition[%targetId] @ " 1");
	%clientId = AI::getId(%an);
	$MandatoryLawmanChase[%name] = %clientId;
	$MandatoryLawmanTarget[%clientId] = %name;
	
	
	//ChangeRace(%clientId, "Human");
	Client::setSkin(%clientId, $ArmorSkin[%armor]);	
	
	storeData(%clientId, "MyHouse", "Keldrin Mandate"); // ::onKill will pick this up as a "lawman"
	storeData(%clientId, "BotInfoAiName", %an);
	storeData(%clientId, "tmpbotdata", %targetId);
	storeData(%clientId, "botAttackMode", 2);
	storeData(%clientId, "noDropLootbagFlag", True);
	
	AI::SetSpotDist(%clientId);
	AI::setVar(%an, attackMode, $AIattackMode);	
	AI::SelectMovement(%an);
	
	schedule(	"Player::kill(" @ %clientId @ ");", 
				Cap(%lvl * 10, 30, 180));
}

function rpg::CreateKeeperOfSolace() {
	%tower = nameToID("MissionGroup\\neartheastelf");
	if(%tower>0) deleteObject(%tower);
	%tower = newObject("smithbarpillar1", InteriorShape, "pstone_rpillar_l.dis");
	GameBase::setPosition(%tower, "-803.769999999999 2908.25 700.994999999999");
	
	%tower = newObject("smithbarpillar2", InteriorShape, "pstone_rpillar_l.dis");
	GameBase::setPosition(%tower, "-803.769999999999 2908.25 728.5");
	
	%tower = newObject("smithbarpillar3", InteriorShape, "pstone_base_m.dis");
	GameBase::setPosition(%tower, "-804.75 2909 702.875");			
	
	%tower = newObject("smithstair1", InteriorShape, "pstone_tstairs_l.dis");
	GameBase::setPosition(%tower, "-820.214999999999 2908 748.595999999999");
	%tower = newObject("smithstair2", InteriorShape, "pstone_tstairs_l.dis");
	GameBase::setPosition(%tower, "-832.214999999999 2908 736.595999999999");
	%tower = newObject("smithstair3", InteriorShape, "pstone_tstairs_l.dis");
	GameBase::setPosition(%tower, "-844.214999999999 2908 724.595999999999");
	%tower = newObject("smithstair4", InteriorShape, "pstone_tstairs_l.dis");
	GameBase::setPosition(%tower, "-856.214999999999 2908 712.595999999999");
	%tower = newObject("smithstair5", InteriorShape, "pstone_tstairs_l.dis");
	GameBase::setPosition(%tower, "-868.214999999999 2908 700.595999999999");
}


//____________________________________________________________________________________________________________________________________________________
function rpg::RecallPlayer(%clientId) {
	if(rpg::IsTheWorldEnding()) 			return;	
	if(fetchData(%clientId, "tmprecall"))	return;

	if(%clientId.perks & 1){
		FellOffMap(%clientId);
		return;
	}
	
	%pos = GameBase::getPosition(%clientId);
	if(GetWord(%pos, 0) < -8000)		%offworld = True;
	if(GetWord(%pos, 1) < -3072)		%offworld = True;
	if(%offworld)						FellOffMap(%clientId);
	
	%zvel = floor(getWord(Item::getVelocity(%clientId), 2));
	if(%zvel <= -250 || %zvel >= 350) {
		FellOffMap(%clientId);
		%zv = "Falling; recalling instantly.";
		Client::sendMessage(%clientId, $MsgBeige, %zv);
		return;
	}
	
	%zv = "Not falling; recalling normally.";
	Client::sendMessage(%clientId, $MsgBeige, %zv);
	%seconds = $recallDelay;
	
	if(Zone::getType(fetchData(%clientId, "zone")) == "PROTECTED")
		%seconds = %seconds/10;
	storeData(%TrueClientId, "tmprecall", True);
	
	if(SkillCanUse(%clientId,"#recall")) {
		Client::sendMessage(%clientId, $MsgBeige, "Stay at your current position for the next 2 seconds to recall.");
		recallTimer(%clientId, 2, GameBase::getPosition(%clientId));
	} else {
		Client::sendMessage(%clientId, $MsgBeige, "Stay at your current position for the next " @ %seconds @ " seconds to recall.");
		recallTimer(%clientId, %seconds+1, GameBase::getPosition(%clientId));
	}
}
