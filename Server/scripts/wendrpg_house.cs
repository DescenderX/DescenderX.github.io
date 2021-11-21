//____________________________________________________________________________________________________________________________________________________
// DescX Notes:
//		Houses are now first-class in TRPG. I've added MANY fetch quests, upgrades, and perks - as well as a fourth house.
//		Your Rank Points are now capped at 100,000
//		Each House is intended to be very distinct from the others
//		Each House can be joined by any class and will create different combinations of advantages
//		

$HouseName[1] = "Order Of Qod";		
$HouseName[2] = "Keldrin Mandate";
$HouseName[3] = "College of Geoastrics";
$HouseName[4] = "Luminous Dawn";
$HouseName[5] = "Wildenslayers";

$HouseIndex["Order Of Qod"] = 1;	
$HouseIndex["Keldrin Mandate"] = 2;
$HouseIndex["College of Geoastrics"] = 3;
$HouseIndex["Luminous Dawn"] = 4;
$HouseIndex["Wildenslayers"] = 5;

$HouseStartUpEq[1] = "MarkOfOrder 1 SepticSystemKey 1";
$HouseStartUpEq[2] = "TheLawfulMasses 1 PaddedShield 1";
$HouseStartUpEq[3] = "Iradnium 1 PickAxe 1 AstralFlask 1";
$HouseStartUpEq[4] = "WayLink 1";
$HouseStartUpEq[5] = "SlayerGear 1";

$HouseTitles[1] = Array::New("","OrderOfQodTitles");
$HouseTitles[2] = Array::New("","KeldrinMandateTitles");
$HouseTitles[3] = Array::New("","GeostrologistsTitles");
$HouseTitles[4] = Array::New("","LuminousDawnTitles");
$HouseTitles[5] = Array::New("","WildenslayersTitles");

Array::Fill($HouseTitles[1], "0 Ordertaker|100000 Ordergiver", "|");
Array::Fill($HouseTitles[2], "0 Enforcer|40000 Lawyer|60000 Knight|80000 Guardian|100000 Mandatory", "|");
Array::Fill($HouseTitles[3], "0 Student|25000 Pilgrim|50000 Researcher|75000 Resonator|100000 Refractor", "|");
Array::Fill($HouseTitles[4], "0 Waysetter|100000 Owner", "|");
Array::Fill($HouseTitles[5], "0 Slayer|100000 Wildenslayer", "|");

function rpg::GetHouseTitle(%clientId) {
	%h = $HouseIndex[fetchData(%clientId,"MyHouse")];
	%title = "";
	if(%h) {
		%rp = fetchData(%clientId,"RankPoints");
		for(%x=0;%x<$Array::Size[$HouseTitles[%h]];%x++) {
			if(%rp >= getword(Array::Get($HouseTitles[%h],%x), 0))
				%title = getword(Array::Get($HouseTitles[%h],%x), 1);
		}
	}
	return %title;
}

function GetHouseNumber(%n) {
	for(%i = 1; $HouseName[%i] != ""; %i++) {
		if(String::ICompare($HouseName[%i], %n) == 0)
			return %i;
	}
	return "";
}

function BootFromCurrentHouse(%clientId) {
	%currentHouse = fetchData(%clientId, "MyHouse");
	if(%currentHouse != "") {
		for(%i = 1; $HouseName[%i] != ""; %i++) {
			for(%z=0;(%item=getword($HouseStartUpEq[%i],%z))!=-1;%z+=2) {
				%amt = getword($HouseStartUpEq[%i],%z+1);
				if(isBeltItem(%item)) 								Belt::TakeThisStuff(%clientId,%item,%amt);
				else if(HasThisStuff(%clientId,%item @ " " @ %amt))	TakeThisStuff(%clientId,%item @ " " @ %amt);
				
				%amt = rpg::GetItemListCount(fetchData(%clientId, "BankStorage"), %item);
				if(%amt > 0) storeData(%clientId, "BankStorage", rpg::ModifyItemList(fetchData(%clientId, "BankStorage"), %item, -%amt));
			}
		}
		RefreshAll(%clientId);
		UnequipMountedStuff(%clientId);
		
		%name=rpg::getName(%clientId);
		$TheMandateDelivery[%name] = "";
		$TheMandateDeliveryTarget[%name] = "";
		
		// LuminousDawn links and outpost will remain, but wont be useable
		// Geostrologists' equipment requires house membership. it wont be taken but wont be usable
		// Qod #brew book wont be taken away if it was earned
		
		Client::sendMessage(%clientId, $MsgRed, "You have been booted from " @ %currentHouse @ " and have lost all rank points.");

		storeData(%clientId, "MyHouse", "");
		storeData(%clientId, "RankPoints", 0);
		return %hn;
	}
	else return -1;
}


function JoinHouse(%clientId, %newHouse) {
	%currentHouse = fetchData(%clientId, "MyHouse");
	%lostEquip = false;
	if(%currentHouse != "") {
		if(%currentHouse == $HouseName[%newHouse]) {
			%index = GetHouseNumber(%currentHouse);
			if(!HasThisStuff(%clientId,$HouseStartUpEq[%index])) {
				%item = getword($HouseStartUpEq[%index],0);
				%cnt = rpg::GetItemListCount(fetchData(%clientId, "BankStorage"), %item);
				if(%cnt <= 0) {
					%lostEquip = true;
					Client::sendMessage(%clientId, $MsgRed, "You lost your startup equipment! Replacements cost Rank Points.");			
				}
			}
		}
		else BootFromCurrentHouse(%clientId, true);
	}
	storeData(%clientId, "MyHouse", $HouseName[%newHouse]);
	%giveRP = $joinHouseRankPoints;
	if(%lostEquip == true)
		%giveRP = Cap(floor(fetchData(%clientId, "RankPoints") / $joinHouseRankPoints),1,"inf");
	storeData(%clientId, "RankPoints", %giveRP);
	
	GiveThisStuff(%clientId,$HouseStartUpEq[%newHouse], true);
	%items = "";
	for(%x=0;(%w=getword($HouseStartUpEq[%newHouse],%x)) != -1; %x += 2) {
		%w2 = getword($HouseStartUpEq[%newHouse],%x+1);
		if(%items != "")
			%items = %items @ ",";
		%name = "";
		if(isBeltItem(%w))	%name = $beltitem[%w, "Name"];
		else 				%name = %w.description;
		%items = %items @ " " @ %w2 @ " " @ %name;
	}
	if(%items != "")
		%items = %items @ " and ";
	Client::sendMessage(%clientId, $MsgBeige, "You have joined the " @ $HouseName[%newHouse] @ ". You receive" @ %items @ %giveRP @ " rank points.");
}

//____________________________________________________________________________________________________________________________________________________
function rpg::GetHouseLevel(%clientId) {
	%rp = fetchData(%clientId, "RankPoints");
	if(%rp > 100000) { %rp = 100000; storeData(%clientId, "RankPoints", %rp); }
	else if(%rp < 0) { %rp = 0; storeData(%clientId, "RankPoints", %rp); }
	return 1 + ( %rp / 1000 );
}

//____________________________________________________________________________________________________________________________________________________
function rpg::JoinHouse(%clientId, %a, %b, %c, %house) {
	JoinHouse(%clientId, $HouseIndex[%house]); return ""; 
}

//____________________________________________________________________________________________________________________________________________________
function rpg::IsMemberOfHouse(%clientId, %whichHouse) {
	return ($HouseIndex[%whichHouse] == GetHouseNumber(fetchData(%clientId, "MyHouse")));
}

//____________________________________________________________________________________________________________________________________________________
function rpg::HouseFetchQuestAwards(%clientId, %theBot, %response, %keyword, %takeStuff, %rpValueOfStuff, %takeVerb, %returnKeyword) {
	if(IsDead(%clientId))
		return -2;
	%awarded 		= 0;
	%items 			= "";
	%ifnotfound		= "";
	for(%x=0;(%item=getword(%takeStuff,%x)) != -1; %x++) {
		%ifnotfound = %ifnotfound @ %item;
		%val 	= getword(%rpValueOfStuff,%x);
		if(isBeltItem(%item))
			%count 	= Belt::HasThisStuff(%clientId, %item);
		else %count = Player::getItemCount(%clientId, %item);		
		if(%count > 0) {
			TakeThisStuff(%clientId, %item @ " " @ %count);
			%awarded = %count * %val;
			if(%items != "")
				%items = %items @ " " @ %item @ " " @ %count;
			else %items = %item @ " " @ %count;
		}
	}

	if (%rpValueOfStuff < 0 && %awarded < 0) {
		%awarded = -%awarded;
		storeData(%clientId, "COINS", %awarded, "inc");
		Client::sendMessage(%clientId, $MsgBeige, "You have earned " @ %awarded @ " coins for " @ %takeVerb @ ": " @ %items);
		return %returnKeyword;
	} else if(%awarded > 0) {
		RefreshAll(%clientId);
		storeData(%clientId, "RankPoints", %awarded, "inc");
		Client::sendMessage(%clientId, $MsgBeige, "You have earned " @ %awarded @ " rank points for " @ %takeVerb @ ": " @ %items);
		return %returnKeyword;
	}
	
	if(%returnKeyword != "")
		return "no" @ %returnKeyword;
	return "no" @ %ifnotfound;
}

//____________________________________________________________________________________________________________________________________________________
function rpg::HouseUpgradeItemAward(%clientId, %minRank, %fromThisItem, %toThisItem, %dontTakeOldItem) {
	if(fetchData(%clientId, "RankPoints") >= %minRank) {
		%hasStuff = false;
		if(HasThisStuff(%clientId, %fromThisItem @ " 1")) {
			if(!%dontTakeOldItem) {
				TakeThisStuff(%clientId, %fromThisItem @ " 1");
				RefreshAll(%clientId);
			}
			%hasStuff = true;
		}		
		%cnt = rpg::GetItemListCount(fetchData(%clientId, "BankStorage"), %fromThisItem);
		if(%cnt > 0) {
			if(!%dontTakeOldItem)
				storeData(%clientId, "BankStorage", rpg::ModifyItemList(fetchData(%clientId, "BankStorage"), %fromThisItem, -%cnt));
			%hasStuff = true;
		}
		if(%hasStuff) {
			if(!HasThisStuff(%clientId, %toThisItem @ " 1")) {
				GiveThisStuff(%clientId, %toThisItem @ " 1", true);
				RefreshAll(%clientId);
				return 1;
			}
			return 0;
		}
	}
	return 0;
}

//____________________________________________________________________________________________________________________________________________________
function rpg::HouseHuntQuest(%clientId, %theBot, %response, %keyword, %house, %enemyType, %team, %maxActiveHunted, %desiredDrop, %dropValue, %turnInVerb) {
	%turnIn = rpg::HouseFetchQuestAwards(%clientId, %theBot, %response, %keyword, %desiredDrop, %dropValue, %turnInVerb);
	if(%turnIn == "")		// We will be using SAY differently here. If we have the desired drop, go to a keyword....
		return "gave" @ %desiredDrop;
		
	%houseNumber = $HouseIndex[%house];
	$TheHuntedMax[%houseNumber] = %maxActiveHunted;
	for(%x=0;%x<%maxActiveHunted;%x++) {
		if($TheHunted[%houseNumber, %x] == "")
			break;
	}
	
	if(%x==%maxActiveHunted) {
		%x = floor(getRandom()*100) % %maxActiveHunted;		// After we spawn the MAX number of hunted, select a random active hunted creature that already exists
	} else {												
		%group = nameToID("MissionGroup\\SpawnPoints");		// Always try to spawn a new creature each time nothing is turned in
		%where = "";
		%spawnEnemy = "";
		for(%e=0;(%enemy=getword(%enemyType,%e)) != -1; %e++) {
			%spawnEnemy = %enemy;
			if(getRandom() > 0.66) break;
		}
		if(%group != -1) {
			%count = Group::objectCount(%group);
			%total=0;
			for(%m=0;%m<%count;%m++) {
				%marker = Group::getObject(%group, %m);
				
				if(%marker.hunt == %house) {
					%total++;
					if(%where == "" || getRandom() * 100 <= 5)	// Select a random Marker with a matching hunt attribute
						%where = %marker;
				}
			}
		}
		if(%where != "") {									// Found a marker? Good. Spawn. 
			$TheHunted[%houseNumber, %x] = %spawnEnemy;
			$TheHuntedSpawn[%houseNumber, %x] = Object::getName(%where);
		} else return "+Something is wrong.";		// Error.... no hunt attribute on any Marker types...
	}
	return "+" @ String::replace(String::replace(%response,		// Transform the response to show the name of the AI and a hint
			"%NAME", $TheHunted[%houseNumber, %x]),
			"%HINT", getword($TheHuntedSpawn[%houseNumber, %x],5));
}



//____________________________________________________________________________________________________________________________________________________
// [Keldrin] The Mandate
//____________________________________________________________________________________________________________________________________________________
function rpg::TheMandateShieldUpgrade(%clientId, %theBot, %response, %keyword) {
	%item = "KeldriniteShield 1";
	if(HasThisStuff(%clientId, %item @ " 1") || rpg::GetItemListCount(fetchData(%clientId, "BankStorage"), %item) > 0) 
		return "maxupgrade";	
	%upgraded =  rpg::HouseUpgradeItemAward(%clientId, 20000, 	"PaddedShield", "PlateShield");
	%upgraded += rpg::HouseUpgradeItemAward(%clientId, 40000, 	"PlateShield", 	"KnightShield");
	%upgraded += rpg::HouseUpgradeItemAward(%clientId, 60000, 	"KnightShield", "BronzeShield");
	%upgraded += rpg::HouseUpgradeItemAward(%clientId, 80000, 	"BronzeShield", "DragonShield");
	%upgraded += rpg::HouseUpgradeItemAward(%clientId, 100000, "DragonShield", "KeldriniteShield");
	if(%upgraded > 0)	{
		PlaySound(SoundLevelUp, GameBase::getPosition(%clientId));
		return "upgradeshield";	
	}
	else return "failupgrade";
}

//____________________________________________________________________________________________________________________________________________________
function rpg::TheMandateDeliverySetup(%clientId) {
	if(!rpg::IsMemberOfHouse(%clientId,"Keldrin Mandate"))
		return "";
	%name = rpg::getName(%clientId);
	%targetBot = $TheMandateDeliveryTarget[%name];
	if(%targetBot==""){
		for(%x=0;(%bot=getword($TownBotList,%x)) != -1; %x++) {
			%botname	= $BotInfo[%bot.name, NAME];
			%race 		= $BotInfo[%bot.name, RACE];
			if(%botname == "Weather Device" || %race == "Keeper" || %race == "Outlaw" || %race == "OutlawFemale" || %race == "Dancer" || %race == "DancerManager")
				continue; // never deliver to non-sentients, outlaws, or dancers
			if(%targetBot == "" || getRandom() < 0.13)
				%targetBot = %bot;
		}
		
		%amt = floor(cap(getRandom()*15,1,15));
		Belt::GiveThisStuff(%clientId, "MandateSatchel", %amt, true);
		%supplies = "MandateSatchel " @ %amt;
		%amt = floor(getRandom() * 3);
		if(%amt > 0) {Belt::GiveThisStuff(%clientId, "MandateCrate", %amt, true); %supplies = %supplies @ " MandateCrate " @ %amt; }
		%amt = floor(getRandom() * 8);
		if(%amt > 0) {Belt::GiveThisStuff(%clientId, "MandateBackpack", %amt, true); %supplies = %supplies @ " MandateBackpack " @ %amt;}
		
		$TheMandateDelivery[%name] = %supplies;
		$TheMandateDeliveryTarget[%name] = %targetBot;
		RefreshAll(%clientId);
		return "+Thanks! You'll have to track down " @ $BotInfo[%targetBot.name, NAME] @ ". Here are the supplies.";
	}
	
	if(!HasThisStuff(%clientId,$TheMandateDelivery[%name])) {
		storeData(%clientId, "RankPoints", 1000, "dec");
		if(fetchData(%clientId, "RankPoints") < 1)
			storeData(%clientId, "RankPoints", 1);
		%crates 	= Belt::HasThisStuff(%clientId,"MandateCrate");
		%packs 		= Belt::HasThisStuff(%clientId,"MandateBackpack");
		%satchels 	= Belt::HasThisStuff(%clientId,"MandateSatchel");
		if(%crates)		Belt::TakeThisStuff(%clientId,"MandateCrate",%crates);
		if(%packs)		Belt::TakeThisStuff(%clientId,"MandateBackpack",%packs);
		if(%satchels)	Belt::TakeThisStuff(%clientId,"MandateSatchel",%satchels);
		RefreshAll(%clientId);
		$TheMandateDelivery[%name] = "";
		$TheMandateDeliveryTarget[%name] = "";
		Client::sendMessage(%clientId, $MsgRed, "You failed your delivery and lost 1000 rank points!");
		return "+Where are the supples?!? Why did you come back?! Nevermind. If you want to try a new delivery, well... ok, I guess.";
	}
	
	return "+Going the wrong way? You're supposed to be delivering " @ $TheMandateDelivery[%name] @ " to " @ GameBase::getMapName($TheMandateDeliveryTarget[%name]) @ "...";	
}

//____________________________________________________________________________________________________________________________________________________
function rpg::TheMandateDeliveryComplete(%clientId) {
	%name = rpg::getName(%clientId);
	if(HasThisStuff(%clientId,$TheMandateDelivery[%name])) {
		%score = 0;
		for(%x=0;(%item=getword($TheMandateDelivery[%name],%x)) != -1; %x+=2) {
			%amt = getword($TheMandateDelivery[%name],%x+1);
			if(%item=="MandateSatchel") %score += 50 * %amt;
			if(%item=="MandateBackpack") %score += 100 * %amt;
			if(%item=="MandateCrate") %score += 500 * %amt;
			Belt::TakeThisStuff(%clientId,%item,%amt);
		}
		storeData(%clientId, "RankPoints", %score, "inc");
		Client::sendMessage(%clientId, $MsgGreen, "You found your delivery target and unloaded supplies! " @ %score @ " rank points have been awarded.");
		$TheMandateDelivery[%name] = "";
		$TheMandateDeliveryTarget[%name] = "";
		RefreshAll(%clientId);		
	}
}



//____________________________________________________________________________________________________________________________________________________
// College of Geoastrics
//____________________________________________________________________________________________________________________________________________________
function rpg::GeostrologyRewardCheck(%clientId, %theBot, %response, %keyword) {
	%item = "Reatomizer 1";
	if(HasThisStuff(%clientId, %item @ " 1") || rpg::GetItemListCount(fetchData(%clientId, "BankStorage"), %item) > 0) 
		return "";	
	%upgraded += rpg::HouseUpgradeItemAward(%clientId, 25000, 	"PickAxe", 	"HammerPick", true);
	%upgraded += rpg::HouseUpgradeItemAward(%clientId, 50000, 	"HammerPick", "Laser", true);
	%upgraded += rpg::HouseUpgradeItemAward(%clientId, 75000, 	"Laser", "Driller", true);
	%upgraded += rpg::HouseUpgradeItemAward(%clientId, 100000, "Driller", "Reatomizer", true);
	if(%upgraded > 0){
		PlaySound(SoundLevelUp, GameBase::getPosition(%clientId));
		return "upgradetool";	
	}
	else return "";
}

//____________________________________________________________________________________________________________________________________________________
// Order of Qod
//____________________________________________________________________________________________________________________________________________________
function rpg::OrderOfQodRewardCheck(%clientId, %theBot, %response, %keyword) {
	
	
	if(fetchData(%clientId, "RankPoints") >= 100000) {
		%item = "SignOfQod";
		if(!HasThisStuff(%clientId, %item @ " 1")) {
			%cnt = rpg::GetItemListCount(fetchData(%clientId, "BankStorage"), %item);
			if(%cnt <= 0) {
				GiveThisStuff(%clientId, %item @ " 1", true);
				PlaySound(SoundLevelUp, GameBase::getPosition(%clientId));
				RefreshAll(%clientId);
				return "completed2";
			} else return "alreadycompleted";
		} else return "alreadycompleted";
	}
	else if(fetchData(%clientId, "RankPoints") >= 50000) {
		%item = "BookOfConcoctions";
		if(!HasThisStuff(%clientId, %item @ " 1")) {
			%cnt = rpg::GetItemListCount(fetchData(%clientId, "BankMiscItems"), %item);
			if(%cnt <= 0) {
				GiveThisStuff(%clientId, %item @ " 1", true);
				PlaySound(SoundLevelUp, GameBase::getPosition(%clientId));
				RefreshAll(%clientId);
				return "completed1";
			} else return "alreadycompleted";
		} else return "alreadycompleted";
	}
	return "";
}

//____________________________________________________________________________________________________________________________________________________
// Luminous Dawn
//____________________________________________________________________________________________________________________________________________________
function rpg::LuminousDawnSetupOutpost(%name, %clientId, %step, %pos, %loadingFromWorldSave) {	
	%groupName 	= "Outpost" @ %name;
	%basePath 	= "MissionCleanup\\" @ %groupName;
	
	%zSkyOffset = 3000;
	
	if(%step == 1) {
		%object = newObject("building", InteriorShape, "keldinn.dis");
		addToSet(%basePath, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 6, vecy(%pos) - 9, %zSkyOffset + vecz(%pos)));
		
		%botname = "banker" @ %clientId;		
		%bot = nameToId(%basePath @ "\\" @ %botname);
		if(%bot > 0){
			$TownBotList = String::RemoveFromList($TownBotList, %bot, " ");
			deleteObject(%bot);
		}
		if(getRandom() >= 0.5) $BotInfo[%botname, RACE] = "DancerManager";
		else $BotInfo[%botname, RACE] = "OutlawMale";
		$BotInfo[%botname, NAME] = %name @ "'s Banker";
		%object = newObject("storagebot", StaticShape, DancerManagerTownBot, true);
		addToSet(%basePath, %object);
		GameBase::setMapName(%object, $BotInfo[%botname, NAME]);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 1, vecy(%pos) - 3, %zSkyOffset + vecz(%pos) + 3));
		GameBase::setRotation(%object, "0 0 1.56");
		GameBase::setTeam(%object, 0);
		GameBase::playSequence(%object, 0, "pose kneel");
		%object.name = %botname;
		
		$TownBotList = $TownBotList @ %object @ " ";
		
		if(!%loadingFromWorldSave)
			Client::sendMessage(%clientId, $MsgBeige, "You set the foundation for your #outpost in the sky. The Luminous Dawn will #fasttravel inside to build it for you over the next 5 minutes.");
	} else if(%step == 2) {
		%old = nameToId(%basePath @ "\\building");
		deleteObject(%old);
		
		%object = newObject("building", InteriorShape, "rininn.dis");
		addToSet(%basePath, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 1, vecy(%pos) + 1, %zSkyOffset + vecz(%pos) + 10));
		
		%object = newObject("sleepzone", Trigger, GroupTrigger);
		addToSet(%basePath, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos), vecy(%pos) - 11, %zSkyOffset + vecz(%pos) + 6));
		%object.boundingBox = "-5 -5 -2 5 5 2";
				
		for(%x=0;(%outpostOwner=$LuminousDawnOutpostOwner[%x]) != ""; %x++) {
			if(%outpostOwner == %name) {
				$LuminousDawnOutposts[%x] = %pos;
				%replaced = true;
			}
		}
		if(!%replaced) {				
			$LuminousDawnOutposts[%x] = %pos;
			$LuminousDawnOutpostOwner[%x] = %name;
		}
		
		if(!%loadingFromWorldSave) {
			PlaySound(SoundLevelUp, GameBase::getPosition(%clientId));
			Client::sendMessage(%clientId, $MsgBeige, "Finished setting up the #outpost. Enter it with #outpost enter. Leave with #outpost leave, then #recall or #fasttravel");
		}
	} else if(%step == "destroy") {
		%y=-1;
		for(%x=0;(%outpostOwner=$LuminousDawnOutpostOwner[%x]) != ""; %x++) {
			if(%y>-1) {
				$LuminousDawnOutposts[%x-1] 	= $LuminousDawnOutposts[%x];
				$LuminousDawnOutpostOwner[%x-1] = $LuminousDawnOutpostOwner[%x];
			}
			if(%outpostOwner == %name) {
				$LuminousDawnOutposts[%x] = "";
				$LuminousDawnOutpostOwner[%x] = "";
				%y = %x;
			}
		}
		
		if(!%loadingFromWorldSave) {
			%botname = "banker" @ %clientId;
			%bot = nameToId(%basePath @ "\\" @ %botname);
			if(%bot > 0){
				$TownBotList = String::RemoveFromList($TownBotList, %bot, " ");
				deleteObject(%bot);
			}
			RefreshAll(%clientId);
			%group = nameToId(%basePath);
			Group::iterateRecursive(%group, GameBase::setPosition, "0 0 0");
			deleteObject(%group);
		}
	}
}

//____________________________________________________________________________________________________________________________________________________
// Produce a space-less name for a given zone
function rpg::ZoneNameFromObject(%groupObject) {
	%name="";
	%location = Object::getName(%groupObject);
	for(%x=1;(%w=getword(%location,%x))!=-1;%x++)
		%name = %name @ %w;
	return %name;
}

$LuminousDawnWayLinkMidpoint = "-1795 -1872 53";		// Magic constant position ftw. Would prefer a variable position, but... no ideas...
StaticShapeData WaylinkEnergyField { shapeFile = "domefiled"; disableCollision = true;  maxDamage = 10000.0; isTranslucent = true;	description = "Force Field";};

//____________________________________________________________________________________________________________________________________________________
function rpg::LuminousDawnDeployLink(%name, %clientId, %location, %step, %pos, %loadingFromWorldSave) {	
	%groupPath = "MissionCleanup\\Waylink\\" @ %name @ %location;
	if(!%loadingFromWorldSave && %pos != "" && %step != "destroy") {
		if(Vector::getDistance(GameBase::getPosition(%clientId), %pos) > 10 || IsDead(%clientId)) {
			if(GameBase::getPosition(Group::getObject(%groupPath, 0)) != "0 0 0") {
				Client::sendMessage(%clientId, $MsgRed, "As you wander away from your #waylink, it topples over. You'll need to start a new one.");
				%step = "destroy";
			} else return;
		}
	}
	if(%step == 1) {
		%object = newObject("stand", StaticShape, FlagStand);
		addToSet(%groupPath, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 1, vecy(%pos), vecz(%pos)));
		if(!%loadingFromWorldSave)
			Client::sendMessage(%clientId, $MsgBeige, "You place the landing stand for your #waylink in " @ %location);
	} else if(%step == 2 && !%loadingFromWorldSave) {
		%object = newObject("pillar", InteriorShape, "pstone_beam_s.dis");
		addToSet(%groupPath, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 0.65, vecy(%pos), vecz(%pos) + 2));
		GameBase::setRotation(%object, "0 1.56 0");
		if(!%loadingFromWorldSave)
			Client::sendMessage(%clientId, $MsgBeige, "You place a beam to balance the #waylink on top of...");
	} else if(%step == 3) {
		%object = newObject("beacon", StaticShape, DefaultBeacon);
		addToSet(%groupPath, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 1, vecy(%pos), vecz(%pos) + 4));
		if(!%loadingFromWorldSave)
			Client::sendMessage(%clientId, $MsgBeige, "You place the #waylink on the beam...");
	} else if(%step == 4) {
		%object = newObject("dome", StaticShape, WaylinkEnergyField);
		addToSet(%groupPath, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 1, vecy(%pos), vecz(%pos) + 3));
		if(!%loadingFromWorldSave)
			Client::sendMessage(%clientId, $MsgBeige, "You activate the #waylink and begin to syncronize it with the #fasttravel network...");		
	} else if(%step == 5) {		
		%old = nameToId(%groupPath @ "\\pillar");
		if(%old > 0)
			deleteObject(%old);
		%object = newObject("elecbeam", StaticShape, ElectricalBeam);
		addToSet(%groupPath, %object);
		GameBase::setPosition(%object, NewVector(vecx(%pos) + 1, vecy(%pos), vecz(%pos)));
		if(!%loadingFromWorldSave) {
			Client::sendMessage(%clientId, $MsgBeige, "Finished setting up the #waylink. Use #waylink a second time in this zone to replace the link. Use #fasttravel " @ %location @ " to return here.");
			%replaced = false;
			for(%x=0;(%outpost=$LuminousDawnWayLinks[%x]) != ""; %x++) {
				if($LuminousDawnWayLinkLocation[%x] == %location &&
				   $LuminousDawnWayLinkOwner[%x] == %name) {
					$LuminousDawnWayLinks[%x] = %pos;
					%replaced = true;
				}
			}
			if(!%replaced) {
				$LuminousDawnWayLinks[%x] = %pos;
				$LuminousDawnWayLinkOwner[%x] = %name;
				$LuminousDawnWayLinkLocation[%x] = %location;
				
				%RPBonus = Cap(floor(Vector::getDistance(%pos, $LuminousDawnWayLinkMidpoint)), 1, 2500);
				storeData(%clientId, "RankPoints", %RPBonus, "inc");
				Client::sendMessage(%clientId, $MsgGreen, "You gain " @ %RPBonus @ " bonus Rank Points for placing your first #waylink in " @ %location @ ".");
			}
		}
	} else if(%step == "destroy") {	
		if(!%loadingFromWorldSave)
			RefreshAll(%clientId);
		Group::iterateRecursive(%groupPath, GameBase::setPosition, "0 0 0");
		deleteObject(nameToId(%groupPath));
		if(!%loadingFromWorldSave)
			Client::sendMessage(%clientId, $MsgBeige, "Your previous #waylink for " @ %location @ " has been removed.");
	}
}

//____________________________________________________________________________________________________________________________________________________
// This needs its own function to keep the schedule calls from turning into hideously long strings.
// You can easily die at any stage of #fasttravel.
function rpg::PerformFastTravel(%clientId, %clientpos, %location, %linkpos, %stage) {
	if(!IsDead(%clientId)) {
		if(%stage == 1) {
			PlaySound(Portal6, %clientpos);
			schedule("rpg::PerformFastTravel(" @ %clientId @ ", \"" @ %clientpos @ "\", \"" @ %location @ "\", \"" @ %linkpos @ "\", 2);", 1, %clientId);
		} else if(%stage == 2) {
			Player::applyImpulse(%clientId, "0 0 1000");
			schedule("rpg::PerformFastTravel(" @ %clientId @ ", \"" @ %clientpos @ "\", \"" @ %location @ "\", \"" @ %linkpos @ "\", 3);", 0.5, %clientId);
		} else if(%stage == 3) {
			PlaySound(Portal6, %linkpos);
			Player::setDamageFlash(%clientId, 1);
			PlaySound(SoundGlassBreak, GameBase::getPosition(%clientId));
			schedule("rpg::PerformFastTravel(" @ %clientId @ ", \"" @ %clientpos @ "\", \"" @ %location @ "\", \"" @ %linkpos @ "\", 4);", 0.5, %clientId);
		} else if(%stage == 4) {			
			Item::setVelocity(%clientId, "0 0 0");
			GameBase::setPosition(%clientId, %linkpos);
			schedule("rpg::PerformFastTravel(" @ %clientId @ ", \"" @ %clientpos @ "\", \"" @ %location @ "\", \"" @ %linkpos @ "\", 5);", 1, %clientId);
		} else if(%stage == 5) {
			PlaySound(SoundGlassBreak, GameBase::getPosition(%clientId));
			for(%x=1;%x<=5;%x++) {
				schedule("Player::setDamageFlash(" @ %clientId @ ", 0.66);", (%x * 2), %clientId);
			}
			Client::sendMessage(%clientId, $MsgGreen, "You #fasttravel to '" @ %location @ "' successfully.");
		}
	} else {
		Client::sendMessage(%clientId, $MsgRed, "You died attempting to #fasttravel.");
	}
}
