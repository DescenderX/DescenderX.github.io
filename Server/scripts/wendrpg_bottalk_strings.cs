//================================================================================================================================================================
// Sealbrokers
//================================================================================================================================================================
function rpg::SetupBankerDialog() {
	for(%z=0;(%broker=getword("Sealbroker Soulbroker",%z)) != -1; %z++) {
		$BotInfoChat[%broker, EVAL, "hello"] = "rpg::IsTheWorldEndingDialog";
		$BotInfoChat[%broker, SAY, "hello"] = "Yes, I am a Sealbroker. I can conceal your coins from Thieves, or seal your items for safe keeping.";
		$BotInfoChat[%broker, "hello", 0] = "conceal";
		$BotInfoChat[%broker, "hello", 1] = "seal";

		$BotInfoChat[%broker, EVAL, "ending"] = "rpg::HasFavorDialog";
		$BotInfoChat[%broker, SAY, "ending"] = "Quickly, Newcomer! The world is ending! I can return coins to you, or transport you to the Hall of Souls. You will not be able to leave, but you will be safe until the next cycle begins.";
		$BotInfoChat[%broker, "ending", 0] = "return";
		$BotInfoChat[%broker, "ending", 1] = "safety";
		$BotInfoChat[%broker, SAY, "nosealing"] = "I cannot serve those without FAVOR. Find some. Or die. The Soulbroker will deal with you.";

		$BotInfoChat[%broker, EVAL, "safety"] = "rpg::SavePlayerFromTheEnd";
		$BotInfoChat[%broker, SAY, "safety"] = "You FOOL! This is YOUR fault! How dare you demand safety!";
		$BotInfoChat[%broker, "safety", 0] = "fault";
		
		$BotInfoChat[%broker, "!", "fault"] = "Equalizer 1";
		$BotInfoChat[%broker, "~", "fault"] = "hello";
		$BotInfoChat[%broker, SAY, "fault"] = "Yes, you idiot! That Equalizer you carry is tearing this world apart!";
		$BotInfoChat[%broker, "fault", 0] = "equalize";
		$BotInfoChat[%broker, "fault", 1] = "hello";
		
		$BotInfoChat[%broker, EVAL, "equalize"] = "rpg::BankerDestroy";

		$BotInfoChat[%broker, EVAL, "conceal"] = "rpg::CheckThiefBanker";
		function rpg::CheckThiefBanker(%cl) { if(fetchData(%cl, "STOLEN") > 1000) return "thief"; return ""; }
		$BotInfoChat[%broker, SAY, "conceal"] = "Ahh. Do you wish to hide coins, or have them returned to you?";
		$BotInfoChat[%broker, "conceal", 0] = "hide";
		$BotInfoChat[%broker, "conceal", 1] = "return";
		$BotInfoChat[%broker, "conceal", 2] = "hello";
		
		$BotInfoChat[%broker, SAY, "thief"] = "Thief! Conceal your own coins! We will return coins you have left with us, but we will not hide any more!";
		$BotInfoChat[%broker, "thief", 0] = "return";
		$BotInfoChat[%broker, "thief", 1] = "hello";

			$BotInfoChat[%broker, EVAL, "hide"] = "rpg::StoreCoinsDialog";
			$BotInfoChat[%broker, SAY, "hide"] = "Yes. We will hide coins. You have [ %COINS% ] to hide. We are already concealing [ %HELD% ]. The amount does not matter. What percentage should I conceal for you?";
			$BotInfoChat[%broker, "hide", 0] = "10 percent";
			$BotInfoChat[%broker, "hide", 1] = "15 percent";
			$BotInfoChat[%broker, "hide", 2] = "25 percent";
			$BotInfoChat[%broker, "hide", 3] = "50 percent";
			$BotInfoChat[%broker, "hide", 4] = "75 percent";
			$BotInfoChat[%broker, "hide", 5] = "90 percent";
			$BotInfoChat[%broker, "hide", 6] = "100 percent";

			$BotInfoChat[%broker, EVAL, "return"] = "rpg::RetrieveCoinsDialog";
			$BotInfoChat[%broker, SAY, "return"] = "Yes. We are hiding [ %COINS% ] of your coins. What percentage do you wish to have returned to you?";
			$BotInfoChat[%broker, "return", 0] = "10 percent";
			$BotInfoChat[%broker, "return", 1] = "15 percent";
			$BotInfoChat[%broker, "return", 2] = "25 percent";
			$BotInfoChat[%broker, "return", 3] = "50 percent";
			$BotInfoChat[%broker, "return", 4] = "75 percent";
			$BotInfoChat[%broker, "return", 5] = "90 percent";
			$BotInfoChat[%broker, "return", 6] = "100 percent";
			
			$BotInfoChat[%broker, EVAL, "10"] = "rpg::HandleCoinsDialog ,10";
			$BotInfoChat[%broker, EVAL, "15"] = "rpg::HandleCoinsDialog ,15";
			$BotInfoChat[%broker, EVAL, "25"] = "rpg::HandleCoinsDialog ,25";
			$BotInfoChat[%broker, EVAL, "50"] = "rpg::HandleCoinsDialog ,50";
			$BotInfoChat[%broker, EVAL, "75"] = "rpg::HandleCoinsDialog ,75";
			$BotInfoChat[%broker, EVAL, "90"] = "rpg::HandleCoinsDialog ,90";
			$BotInfoChat[%broker, EVAL, "100"] = "rpg::HandleCoinsDialog ,100";

		$BotInfoChat[%broker, EVAL, "seal"] = "rpg::SealEquipmentCheck";
		$BotInfoChat[%broker, SAY, "seal"] = "Hmm yes. You wish to seal equipment for safe keeping.";
	}
	$BotInfoChat["Soulbroker", EVAL, "seal"] = "rpg::SealEquipment";	
	$BotInfoChat["Soulbroker", EVAL, "ending"] = "";
	$BotInfoChat["Soulbroker", EVAL, "conceal"] = "";
	
	$BotInfoChat["Soulbroker", SAY, "ending"] = "Yes, Newcomer. The world is currently ending. You cannot leave this Hall until the next cycle begins. I can still coneal your coin or access your seals, if you wish.";
	$BotInfoChat["Soulbroker", "ending", 0] = "conceal";
	$BotInfoChat["Soulbroker", "ending", 1] = "seal";
} rpg::SetupBankerDialog();

function rpg::RetrieveCoinsDialog(%cl,%bot,%response) {
	$BankerBotState[%bot] = "retrievecoins";
	return "+" @ String::replace(%response, "%COINS%", fetchData(%cl,"BANK"));
}

function rpg::StoreCoinsDialog(%cl,%bot,%response) {
	$BankerBotState[%bot] = "takecoins";
	return "+" @ String::replace(String::replace(%response, "%COINS%", fetchData(%cl,"COINS")), "%HELD%", fetchData(%cl,"BANK"));
}

function rpg::SealEquipment(%cl,%bot) {
	SetupBank(%cl, %bot);
	$state[%bot, %cl] = "";
	return ""; 
}
function rpg::SealEquipmentCheck(%cl,%bot) { 
	if(rpg::HasFavorDialog(%cl,%bot) != "") 
		return "nosealing"; 
	return rpg::SealEquipment(%cl,%bot);
}

function rpg::HandleCoinsDialog(%cl,%bot,%response,%keyword,%percentage) {
	%percentage = getword(%percentage,0) / 100;
	if($BankerBotState[%bot] == "takecoins") {
		%take = floor(fetchData(%cl, "COINS") * (%percentage));
		storeData(%cl, "BANK", 	%take, "inc");
		storeData(%cl, "COINS", %take, "dec");
		return "+Yes. We will hide these [ " @ %take @ " ] coins for you.";
	}
	else if($BankerBotState[%bot] == "retrievecoins") {
		%take = floor(fetchData(%cl, "BANK") * %percentage);
		storeData(%cl, "COINS", %take, "inc");
		storeData(%cl, "BANK", 	%take, "dec");
		return "+Yes. Here are [ " @ %take @ " ] coins as requested.";
	}
	else return "hello";
}

//================================================================================================================================================================
// Jaten
//================================================================================================================================================================
$BotInfoChat["JatenGuard", SAY, "hello"] = "Keep moving.";

$BotInfoChat["JatenTanner", SAY, "hello"] = "Are you ready for the end, Newcomer? Need to buy supplies?";
$BotInfoChat["JatenTanner", "hello", 0] = "buy";
$BotInfoChat["JatenTanner", "hello", 1] = "end";
	
	$BotInfoChat["JatenTanner", EVAL, "buy"] = "bottalk::SetupShop";
	$BotInfoChat["JatenTanner", SAY, "buy"] = "Have a look";

	$BotInfoChat["JatenTanner", SAY, "end"] = "The water's drying up, in case you haven't heard. Keep your wits about you.";
	$BotInfoChat["JatenTanner", "end", 0] = "water";
	$BotInfoChat["JatenTanner", "end", 1] = "wits";
	$BotInfoChat["JatenTanner", "end", 2] = "hello";
		$BotInfoChat["JatenTanner", SAY, "water"] = "Don't ask me, but it's a real problem. Keldrin's going to run dry in less than a year.";
		$BotInfoChat["JatenTanner", "water", 0] = "hello";
		$BotInfoChat["JatenTanner", SAY, "wits"] = "Jaten is wide open and far from Ethren. Outlaws thrive here. Hell of a place to start, Newcomer.";
		$BotInfoChat["JatenTanner", "wits", 0] = "ethren";
		$BotInfoChat["JatenTanner", "wits", 0] = "hello";
		$BotInfoChat["JatenTanner", SAY, "ethren"] = "Fort Ethren originally stood North East of hear a century ago. The new fort was built on the other end of the world.";
		$BotInfoChat["JatenTanner", "ethren", 0] = "hello";
	

$BotInfoChat["JatenTamer", SAY, "hello"] = "Beasts? Hah! Gnolls are nothing compared to the bigger beasts of Keldrin.";
$BotInfoChat["JatenTamer", "hello", 0] = "gnoll";
$BotInfoChat["JatenTamer", "hello", 1] = "beasts";
	$BotInfoChat["JatenTamer", SAY, "gnoll"] = "I got Chortle here from Old Ethren around the corner. ";
	$BotInfoChat["JatenTamer", "gnoll", 0] = "chortle";
	$BotInfoChat["JatenTamer", "gnoll", 1] = "ethren";
	$BotInfoChat["JatenTamer", "gnoll", 2] = "hello";
		$BotInfoChat["JatenTamer", SAY, "chortle"] = "He doesn't do much. Carries things. But he doesn't complain. Gnolls are simple like that.";
		$BotInfoChat["JatenTamer", "chortle", 0] = "hello";
		$BotInfoChat["JatenTamer", SAY, "ethren"] = "Head North East out of Jaten to get there. It's grown over and mostly buried. Perfect home for Gnolls.";
		$BotInfoChat["JatenTamer", "ethren", 0] = "hello";
		$BotInfoChat["JatenTamer", SAY, "beasts"] = "Minotaurs! Imps! Ogres! Haha. Newcomer, tread lightly. Keldrin is full of large beasts.";
		$BotInfoChat["JatenTamer", "beasts", 0] = "hello";

$BotInfoChat["JatenGnoll", SAY, "hello"] = "(the Gnoll stares at you)";
$BotInfoChat["JatenGnoll", "hello", 0] = "poke";
$BotInfoChat["JatenGnoll", "hello", 1] = "pet";
	$BotInfoChat["JatenGnoll", SAY, "poke"] = "Gnnnruuggh!";
	$BotInfoChat["JatenGnoll", "poke", 0] = "hello";
	$BotInfoChat["JatenGnoll", SAY, "pet"] = "(the Gnoll just stands there...)";
	$BotInfoChat["JatenGnoll", "pet", 0] = "hello";
	

//================================================================================================================================================================
// Slicer, Black Market
//================================================================================================================================================================
$BotInfoChat["Slicer", EVAL, "hello"] = "rpg::IsMandatory";
function rpg::IsMandatory(%clientId,%bot) { if(rpg::IsMemberOfHouse(%clientId,"Keldrin Mandate")) rpg::NPCKillAndJailTarget(%clientId,%bot); return ""; }
$BotInfoChat["Slicer", SAY, "hello"] = "HAH, another Newcomer. Here for some work? [ Slicer slashes his dagger through the air at lightning speed, just for show ]";
$BotInfoChat["Slicer", "hello", 0] = "pet";
$BotInfoChat["Slicer", "hello", 1] = "work";

	$BotInfoChat["Slicer", EVAL, "work"] = "rpg::GiveSlicerMessage";
	function rpg::GiveSlicerMessage(%cl,%bot) {
		if(HasThisStuff(%cl,"MessageForSlicer 1")){
			TakeThisStuff(%cl,"MessageForSlicer 1");
			GiveThisStuff(%cl,"TheShocker 1", true);
			return "shocking";
		}
		return "";
	}
	$BotInfoChat["Slicer", SAY, "shocking"] = "Jorn still thinks he's gonna pay us back? How about you take this Shocker here and make sure he pays up? Or anyone with full pockets!! HAH!";
	$BotInfoChat["Slicer", "shocking", 0] = "uhh";
	$BotInfoChat["Slicer", "shocking", 1] = "work";	
		$BotInfoChat["Slicer", SAY, "uhh"] = "Don't worry. It was only used once!! HAH. HAH.";
		$BotInfoChat["Slicer", "uhh", 0] = "hello";
	
	
	$BotInfoChat["Slicer", SAY, "work"] = "Got some targets if you got skills. Good with your hands? Or maybe you know how to dig up coins?";
	$BotInfoChat["Slicer", "work", 0] = "hands";
	$BotInfoChat["Slicer", "work", 1] = "coins";
	
	$BotInfoChat["Slicer", SAY, "superheat"] = "Look who's hot. Those hands are finding too many coins. Could pay off that bounty with a little... finesse?\n[ Slicer whips his dagger around. It's... getting old. ] ";
	$BotInfoChat["Slicer", "superheat", 0] = "hands";
	$BotInfoChat["Slicer", "superheat", 1] = "coins";
	$BotInfoChat["Slicer", "superheat", 2] = "finesse";
		$BotInfoChat["Slicer", SAY, "finesse"] = "[ Slicer spins his dagger and strikes a pose. You die a little on the inside. ]\nThat's right. You give me your coins. I make your bounty go away.";
		$BotInfoChat["Slicer", "finesse", 0] = "give";		
			$BotInfoChat["Slicer", EVAL, "give"] = "rpg::NPCClearBounty";
			function rpg::NPCClearBounty(%cl,%bot,%response) {
				%coins 	= fetchData(%cl,"COINS") * 2;
				%oldbounty = fetchData(%cl,"bounty") + fetchData(%cl,"STOLEN");
				storeData(%cl, "bounty", %coins, "dec");
				storeData(%cl, "STOLEN", %coins, "dec");
				storeData(%cl, "COINS",  %coins, "dec");
				if(fetchData(%cl,"bounty") < 0)		storeData(%cl,"bounty", 0);
				if(fetchData(%cl,"STOLEN") < 0)		storeData(%cl,"STOLEN", 0);
				%newBounty = fetchData(%cl,"bounty") + fetchData(%cl,"STOLEN");
				return "+" @ string::replace(string::replace(%response, "%OLDBOUNTY%", %oldbounty), "%BOUNTY", %newBounty);
			}
			$BotInfoChat["Slicer", SAY, "give"] = "That old [ %OLDBOUNTY% ] bounty? Knocked down to [ %BOUNTY% ] coins. Just like magic. Poof. HAH.";
			$BotInfoChat["Slicer", "give", 0] = "work";

	$BotInfoChat["Slicer", "+", "hands"] = "STOLEN 10000";
	$BotInfoChat["Slicer", "-", "hands"] = "superheat";
	$BotInfoChat["Slicer", "!", "hands"] = "STOLEN 1000";
	$BotInfoChat["Slicer", "=", "hands"] = "nocred";
	$BotInfoChat["Slicer", SAY, "hands"] = "[ Slicer spins his dagger ]\nGot some Mandatories causing trouble. Need someone to... deal with 'em. You in?";
	$BotInfoChat["Slicer", "hands", 0] = "trouble";
	$BotInfoChat["Slicer", "hands", 1] = "work";
		$BotInfoChat["Slicer", EVAL, "trouble"]	= "rpg::HouseHuntQuest ,\"\",\"Guard Writter Preacher Detective\", 3, 5, \"ThiefWrit\", -10000, \"bringing back\"";
		$BotInfoChat["Slicer", SAY, "trouble"] = "%NAME cracked the case, got information out of a %HINT. Almost found our little... operation. You make sure they find the Hall of Souls instead.";
		$BotInfoChat["Slicer", "trouble", 0] = "work";
			$BotInfoChat["Slicer", SAY, "gaveThiefWrit"] = "HAH. Less trouble. You're good. Keep it up.";
			$BotInfoChat["Slicer", "gaveThiefWrit", 0] = "work";


	$BotInfoChat["Slicer", "+", "coins"] = "STOLEN 10000";
	$BotInfoChat["Slicer", "-", "coins"] = "superheat";
	$BotInfoChat["Slicer", "!", "coins"] = "STOLEN 1000";
	$BotInfoChat["Slicer", "=", "coins"] = "nocred";
	$BotInfoChat["Slicer", SAY, "coins"] = "[ Slicer vanishes and reappears ] You know how things are. Some don't. Need someone to shake the slow ones down.";
	$BotInfoChat["Slicer", "coins", 0] = "shakedown";
	$BotInfoChat["Slicer", "coins", 1] = "work";	
		$BotInfoChat["Slicer", EVAL, "shakedown"] = "rpg::FindShakedownTarget";
		$BotInfoChat["Slicer", SAY, "shakedown"] = "%BOT% owes us. Big time. Rob 'em. Broad daylight. Set an example.";
		$BotInfoChat["Slicer", "shakedown", 0] = "work";

	$BotInfoChat["Slicer", SAY, "nocred"] = "HAH! You aren't even wanted for anything! Steal, get the law on your trail. Then we talk work.";
	$BotInfoChat["Slicer", "nocred", 0] = "hello";

	$BotInfoChat["Slicer", SAY, "pet"] = "Gnnnruuggh! STOP that!";
	$BotInfoChat["Slicer", "pet", 0] = "poke";
	$BotInfoChat["Slicer", "pet", 1] = "hello";
	$BotInfoChat["Slicer", EVAL, "poke"] = "rpg::NPCKillAndJailTarget";
	





//================================================================================================================================================================
// Keldrin Town
//================================================================================================================================================================
$BotInfoChat["PilgrimBoatInfo", SAY, "hello"] = "You startled me! I'm just waiting for the next boat to Delkin Heights.";
$BotInfoChat["PilgrimBoatInfo", "hello", 0] = "boat";

$BotInfoChat["PilgrimBoatInfo", SAY, "boat"] = "Never heard of Delkin? It's a town on the water! The ship that docks here sails to Delkin Heights every few minutes.";
$BotInfoChat["PilgrimBoatInfo", "boat", 0] = "delkin";
$BotInfoChat["PilgrimBoatInfo", "boat", 1] = "ship";

$BotInfoChat["PilgrimBoatInfo", SAY, "delkin"] = "Delkin was converted into a trade town after the Wizards moved out. The old weather control devices they used are still there.";
$BotInfoChat["PilgrimBoatInfo", "delkin", 0] = "hello";

$BotInfoChat["PilgrimBoatInfo", SAY, "ship"] = "Well, we like to think of it as a ship! Nobody builds big in Keldrin since the drought.";
$BotInfoChat["PilgrimBoatInfo", "ship", 0] = "drought";

$BotInfoChat["PilgrimBoatInfo", SAY, "drought"] = "Yeah, the water started drying up. People say it's the end of the world. I don't want to talk about it.";

//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["TravellerBoatInfo", SAY, "hello"] = "Hail fellow traveller! Are you headed to Fort Ethren as well?";
$BotInfoChat["TravellerBoatInfo", "hello", 0] = "fort";
$BotInfoChat["TravellerBoatInfo", SAY, "fort"] = "Yes, Fort Ethren. Soldiers from around the world travel to join Guilds and train there.";

//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["InfoTavern1", SAY, "hello"] = "Welcome to the Dragon's End Inn! Esmeralda is inside. Enjoy your stay, and don't cause any trouble.";
$BotInfoChat["InfoTavern2", SAY, "hello"] = "Do you need to #sleep? Head right upstairs. Our beds are free for use.";

//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["InfoKCenter1", SAY, "hello"] = "Welcome to Keldrin, Newcomer. The horses just got taken out to pasture. Go to the inn if you're looking for meat.";
$BotInfoChat["InfoKCenter1", "hello", 0] = "meat";
	$BotInfoChat["InfoKCenter1", SAY, "meat"] = "...RIDE horses? Are you kidding? They're better off as meat. Won't have much to drink in a year anyway.";
	$BotInfoChat["InfoKCenter1", "meat", 0] = "drink";
		$BotInfoChat["InfoKCenter1", SAY, "drink"] = "The water's drying up. Bad for business. Keldrin Town's still has the River, but other places? No so lucky.";
		$BotInfoChat["InfoKCenter1", "drink", 0] = "water";
			$BotInfoChat["InfoKCenter1", SAY, "water"] = "Everyone wants to know why and nobody knows why. Hah! Rumor has it the world's ending. I don't care. Got work to do.";
			$BotInfoChat["InfoKCenter1", "water", 0] = "water";


//================================================================================================================================================================
// Wyzanhyde
//================================================================================================================================================================
$BotInfoChat["WyzanGeostrologist", SAY, "hello"] = "Ahh. A Newcomer. To Wyzanhyde. A rarity. Locations. You will visit some?";
$BotInfoChat["WyzanGeostrologist", "hello", 0] = "locations";
	$BotInfoChat["WyzanGeostrologist", SAY, "locations"] = "The College? Or nearby? Many. The Wastes. The Abandoned Dig Site. The Altar of Shame. Mmm.";
	$BotInfoChat["WyzanGeostrologist", "locations", 0] = "college";
	$BotInfoChat["WyzanGeostrologist", "locations", 1] = "oasis";
	$BotInfoChat["WyzanGeostrologist", "locations", 2] = "wastes";
	$BotInfoChat["WyzanGeostrologist", "locations", 3] = "dig";
	$BotInfoChat["WyzanGeostrologist", "locations", 4] = "shame";		
		$BotInfoChat["WyzanGeostrologist", SAY, "college"] = "Join the College of Geoastrics? Far east, Newcomer. At the height of land. Home of Refractor Pryzm. Seek her.";
		$BotInfoChat["WyzanGeostrologist", "college", 0] = "wellsprings";
		$BotInfoChat["WyzanGeostrologist", "college", 1] = "locations";	
		$BotInfoChat["WyzanGeostrologist", SAY, "oasis"] = "West. Do you have eyes? New home of the Druids, Invokers. Wellsprings has dried.";
		$BotInfoChat["WyzanGeostrologist", "oasis", 0] = "wellsprings";
		$BotInfoChat["WyzanGeostrologist", "oasis", 1] = "locations";
			$BotInfoChat["WyzanGeostrologist", SAY, "wellsprings"] = "Dry. Taken by Churls, Wildenslayers. Old home of the Druids. Seeking answers to the drought? Forget Wellsprings.";
			$BotInfoChat["WyzanGeostrologist", "wellsprings", 0] = "locations";
		$BotInfoChat["WyzanGeostrologist", SAY, "wastes"] = "To the east. Ogres, weapons, and death. Travelling East? Prepare.";
		$BotInfoChat["WyzanGeostrologist", "wastes", 0] = "locations";
		$BotInfoChat["WyzanGeostrologist", SAY, "dig"] = "To the north. Much to study. Abandoned to lock out Churls. A nasty place. Full of ore.";
		$BotInfoChat["WyzanGeostrologist", "dig", 0] = "locations";
		$BotInfoChat["WyzanGeostrologist", SAY, "shame"] = "Savagaes to the North! Unclean sinners and hethens! Unfit for Wizardry! Kill some! YES!";
		$BotInfoChat["WyzanGeostrologist", "shame", 0] = "locations";


//================================================================================================================================================================
// Oasis
//================================================================================================================================================================
$BotInfoChat["OasisResident", SAY, "hello"] = "Occasion of joyous heart, sighted Newcomer! Oasis. Our new home. Wellsprings again will spring, neh?";
$BotInfoChat["OasisResident", "hello", 0] = "home";
$BotInfoChat["OasisResident", "hello", 1] = "wellsprings";
	$BotInfoChat["OasisResident", SAY, "home"] = "We may, call, home, this place. Flowing is life, where the waters are. We will in time be fine.";
	$BotInfoChat["OasisResident", "home", 0] = "wellsprings";
	$BotInfoChat["OasisResident", "home", 1] = "hello";
	$BotInfoChat["OasisResident", SAY, "wellsprings"] = "To speak of my old home? Tragedy, suffering. Talk of before tomorrow - the now. We must help the flow.";
	$BotInfoChat["OasisResident", "wellsprings", 0] = "flow";
	$BotInfoChat["OasisResident", "wellsprings", 1] = "hello";
		$BotInfoChat["OasisResident", SAY, "flow"] = "Resting Churls? HAH, lies! Churls ARE where water dries and dies. The way of things will be. For us you fight, neh?";
		$BotInfoChat["OasisResident", "flow", 0] = "hello";

//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["OasisAngryRes", SAY, "hello"] = "My house is here. Your house is NOT here. Do you understand how things can be in different places? LEAVE.";


//================================================================================================================================================================
// Ancients
//================================================================================================================================================================
$BotInfoChat["AncientTender", EVAL, "hello"] = "rpg::WeatherDevicesAreFull";

$BotInfoChat["AncientTender", SAY, "allfull"] = "Hello again, Newcomer. The skies are clear. It is time the Keeper of Solace was sought.";
$BotInfoChat["AncientTender", "allfull", 0] = "solace";

$BotInfoChat["AncientTender", SAY, "solace"] = "The Keeper of Solace is the Master Smith. Obtain all of the other Keepers' knowledge. Seek Solace at the top of the largest Ancient Elven towers to the North. Equalize.";
$BotInfoChat["AncientTender", "solace", 0] = "equalize";

$BotInfoChat["AncientTender", SAY, "hello"] = "Hello again, Newcomer. A new Cycle is in progress. Before the End, you will be here again. How may I serve?";
$BotInfoChat["AncientTender", "hello", 0] = "uhh";
$BotInfoChat["AncientTender", "hello", 1] = "end";
$BotInfoChat["AncientTender", "hello", 2] = "cycle";
$BotInfoChat["AncientTender", "hello", 3] = "serve";
	$BotInfoChat["AncientTender", SAY, "uhh"] = "Yes?";
	$BotInfoChat["AncientTender", "uhh", 0] = "end";
	$BotInfoChat["AncientTender", "uhh", 1] = "cycle";
	$BotInfoChat["AncientTender", "uhh", 2] = "serve";
	$BotInfoChat["AncientTender", "uhh", 3] = "uhh";
	$BotInfoChat["AncientTender", SAY, "end"] = "The End of the World is inevitable, Newcomer. Happens all the time. You'll Equalize. Don't worry.";
	$BotInfoChat["AncientTender", "end", 0] = "equalize";
	$BotInfoChat["AncientTender", "end", 1] = "uhh";
		$BotInfoChat["AncientTender", SAY, "equalize"] = "You seek to Equalize. I seek to serve.";
		$BotInfoChat["AncientTender", "equalize", 0] = "serve";
		$BotInfoChat["AncientTender", "equalize", 1] = "uhh";	
	$BotInfoChat["AncientTender", SAY, "cycle"] = "The End comes in cycles set off by Newcomers like you. Don't tell Qod, though. He's certain this is the first run.";
	$BotInfoChat["AncientTender", "cycle", 0] = "qod";
	$BotInfoChat["AncientTender", "cycle", 1] = "zatan";
	$BotInfoChat["AncientTender", "cycle", 2] = "uhh";
		$BotInfoChat["AncientTender", SAY, "qod"] = "Qod is an Ancient, outcast before the Cycles began. He fancies wands and exists above the Hall of Souls.";
		$BotInfoChat["AncientTender", "qod", 0] = "wands";
		$BotInfoChat["AncientTender", "qod", 1] = "uhh";
		$BotInfoChat["AncientTender", SAY, "zatan"] = "Zatan's been running the Underworld for several Cycles. He's rather fond of piercing... anything, really.";
		$BotInfoChat["AncientTender", "zatan", 0] = "piercing";
		$BotInfoChat["AncientTender", "zatan", 1] = "uhh";	
	$BotInfoChat["AncientTender", SAY, "serve"] = "I can point you to the ancient ruins if you've forgotten them.";
	$BotInfoChat["AncientTender", "serve", 0] = "armor";
	$BotInfoChat["AncientTender", "serve", 1] = "robe";
	$BotInfoChat["AncientTender", "serve", 2] = "wands";
	$BotInfoChat["AncientTender", "serve", 3] = "slashing";
	$BotInfoChat["AncientTender", "serve", 4] = "piercing";
	$BotInfoChat["AncientTender", "serve", 5] = "bludgeons";
	$BotInfoChat["AncientTender", "serve", 6] = "archery";
	$BotInfoChat["AncientTender", "serve", 7] = "keldrinite";
	$BotInfoChat["AncientTender", "serve", 8] = "uhh";
		$BotInfoChat["AncientTender", SAY, "armor"] = "The Keeper of Armor can be found near a secluded holy moument at W3354S2630.";
		$BotInfoChat["AncientTender", "armor", 0] = "serve";
		$BotInfoChat["AncientTender", SAY, "robe"] = "The Keeper of Robes can be found in a rich Elven tower at W4897N2908.";
		$BotInfoChat["AncientTender", "robe", 0] = "serve";
		$BotInfoChat["AncientTender", SAY, "wands"] = "The Keeper of Justice occupies the ancient colliseum at W2850S1185.";
		$BotInfoChat["AncientTender", "wands", 0] = "serve";
		$BotInfoChat["AncientTender", SAY, "slashing"] = "The Keeper of Swords watches over an ancient blade at W3936N145.";
		$BotInfoChat["AncientTender", "slashing", 0] = "serve";
		$BotInfoChat["AncientTender", SAY, "piercing"] = "The Keeper of Daggers deifies an ancient blade at W3832S2924.";
		$BotInfoChat["AncientTender", "piercing", 0] = "serve";
		$BotInfoChat["AncientTender", SAY, "bludgeons"] = "The Keeper of Old maintains an eternal monument at E408N623.";
		$BotInfoChat["AncientTender", "bludgeons", 0] = "serve";
		$BotInfoChat["AncientTender", SAY, "archery"] = "The Keeper of Earth occupies a cliff at W2355S1302.";
		$BotInfoChat["AncientTender", "archery", 0] = "serve";
		$BotInfoChat["AncientTender", SAY, "keldrinite"] = "Deep within Keldrin Mine lies an ore deposit. It is unaffected by Cycles and yields Keldrinite ore.";
		$BotInfoChat["AncientTender", "keldrinite", 0] = "serve";

$BotInfoChat["AncientTimeKeeper", EVAL, "hello"] = "rpg::WeatherDevicesAreFull";

$BotInfoChat["AncientTimeKeeper", SAY, "allfull"] = "Hello again, Newcomer. The skies are clear. It is time the Keeper of Solace was sought.";
$BotInfoChat["AncientTimeKeeper", "allfull", 0] = "solace";
$BotInfoChat["AncientTimeKeeper", "allfull", 1] = "keepers";

$BotInfoChat["AncientTimeKeeper", SAY, "solace"] = "The Keeper of Solace is the Master Smith. Obtain all of the other Keepers' knowledge. Seek Solace at the top of the largest Ancient Elven towers to the North. Equalize.";
$BotInfoChat["AncientTimeKeeper", "solace", 0] = "keepers";
$BotInfoChat["AncientTimeKeeper", "solace", 1] = "equalize";

$BotInfoChat["AncientTimeKeeper", SAY, "hello"] = "Hello again, Newcomer. A new Cycle is in progress. Before the End, you will be here again.";
$BotInfoChat["AncientTimeKeeper", "hello", 0] = "uhh";
$BotInfoChat["AncientTimeKeeper", "hello", 1] = "end";
$BotInfoChat["AncientTimeKeeper", "hello", 2] = "cycle";
	$BotInfoChat["AncientTimeKeeper", SAY, "uhh"] = "Yes?";
	$BotInfoChat["AncientTimeKeeper", "uhh", 0] = "end";
	$BotInfoChat["AncientTimeKeeper", "uhh", 1] = "cycle";
	$BotInfoChat["AncientTimeKeeper", "uhh", 2] = "hello";
		$BotInfoChat["AncientTimeKeeper", SAY, "end"] = "The End of the World is inevitable, Newcomer. You are fortunate to have found me. I know the location of other Keepers, and how to restore the Weather Devices.";
		$BotInfoChat["AncientTimeKeeper", "end", 0] = "keepers";
		$BotInfoChat["AncientTimeKeeper", "end", 1] = "restore";
		$BotInfoChat["AncientTimeKeeper", "end", 2] = "devices";
		$BotInfoChat["AncientTimeKeeper", "end", 3] = "uhh";

		$BotInfoChat["AncientTimeKeeper", SAY, "restore"] = "Each of the Weather Devices is powered by a Windmill. These mills will require some 'repair' before they become operational.";
		$BotInfoChat["AncientTimeKeeper", "restore", 0] = "windmills";
		$BotInfoChat["AncientTimeKeeper", "restore", 1] = "devices";
		$BotInfoChat["AncientTimeKeeper", "restore", 2] = "hello";
		
		$BotInfoChat["AncientTimeKeeper", SAY, "windmills"] = "Yes. The Windmills themselves remain, but their resevoirs must be replenished with Radium - an ore known to kill those who possess it. Its strong emissions conceal it from my memory. I do not know where this Radium is.";
		$BotInfoChat["AncientTimeKeeper", "windmills", 0] = "uhh";
		$BotInfoChat["AncientTimeKeeper", "windmills", 1] = "hello";
		
		$BotInfoChat["AncientTimeKeeper", SAY, "cycle"] = "The End comes in cycles set off by Newcomers like you. Right at this very place. Have you seen the Windmills and Weather Devices?";
		$BotInfoChat["AncientTimeKeeper", "cycle", 0] = "windmills";
		$BotInfoChat["AncientTimeKeeper", "cycle", 1] = "devices";
		$BotInfoChat["AncientTimeKeeper", "cycle", 2] = "uhh";
		
		$BotInfoChat["AncientTimeKeeper", SAY, "skies"] = "It is difficult to explain to a Newcomer. The darkened skies reveal an evil while the Weather Devices present an illusion. Both conceal the End destination. Restore the Windmills. Equalize.";
		$BotInfoChat["AncientTimeKeeper", "skies", 0] = "devices";
		$BotInfoChat["AncientTimeKeeper", "skies", 1] = "end";
		$BotInfoChat["AncientTimeKeeper", "skies", 2] = "windmills";
		$BotInfoChat["AncientTimeKeeper", "skies", 3] = "hello";
		
		$BotInfoChat["AncientTimeKeeper", SAY, "devices"] = "In order for a new Cycle to begin, the skies must be clear. The Wizards of this realm have stalled the next Cycle by destroying their own Weather Devices. They must be restored to clear the sky, and then, Equalized.";
		$BotInfoChat["AncientTimeKeeper", "devices", 0] = "equalize";
		$BotInfoChat["AncientTimeKeeper", "devices", 1] = "cycle";
		$BotInfoChat["AncientTimeKeeper", "devices", 2] = "skies";
		$BotInfoChat["AncientTimeKeeper", "devices", 3] = "uhh";
		
		$BotInfoChat["AncientTimeKeeper", SAY, "equalize"] = "Yes, Newcomer. This cycle will end and a new one will begin. But only when the skies are cleared, and the Weather Devices are Equalized.";
		$BotInfoChat["AncientTimeKeeper", "equalize", 0] = "devices";
		$BotInfoChat["AncientTimeKeeper", "equalize", 1] = "skies";	
		$BotInfoChat["AncientTimeKeeper", "equalize", 2] = "cycle";	
		$BotInfoChat["AncientTimeKeeper", "equalize", 3] = "hello";	
		
		$BotInfoChat["AncientTimeKeeper", SAY, "keepers"] = "The Ancient Keepers hold the secrets to smithing the most powerful weapons in Keldrin. There are seven. Each holds a book and knowledge. Seek them if you wish to Equalize.";
		$BotInfoChat["AncientTimeKeeper", "keepers", 0] = "armor";
		$BotInfoChat["AncientTimeKeeper", "keepers", 1] = "robes";
		$BotInfoChat["AncientTimeKeeper", "keepers", 2] = "justice";
		$BotInfoChat["AncientTimeKeeper", "keepers", 3] = "swords";
		$BotInfoChat["AncientTimeKeeper", "keepers", 4] = "daggers";
		$BotInfoChat["AncientTimeKeeper", "keepers", 5] = "old";
		$BotInfoChat["AncientTimeKeeper", "keepers", 6] = "earth";
		$BotInfoChat["AncientTimeKeeper", "keepers", 7] = "hello";
		
		$BotInfoChat["AncientTimeKeeper", SAY, "armor"] = "The Keeper of Armor can be found near a secluded holy moument at W3354S2630.";
		$BotInfoChat["AncientTimeKeeper", "armor", 0] = "keepers";
		$BotInfoChat["AncientTimeKeeper", SAY, "robes"] = "The Keeper of Robes can be found in a rich Elven tower at W4897N2908.";
		$BotInfoChat["AncientTimeKeeper", "robes", 0] = "keepers";
		$BotInfoChat["AncientTimeKeeper", SAY, "justice"] = "The Keeper of Justice occupies the ancient colliseum at W2850S1185.";
		$BotInfoChat["AncientTimeKeeper", "justice", 0] = "keepers";
		$BotInfoChat["AncientTimeKeeper", SAY, "swords"] = "The Keeper of Swords watches over an ancient blade at W3936N145.";
		$BotInfoChat["AncientTimeKeeper", "swords", 0] = "keepers";
		$BotInfoChat["AncientTimeKeeper", SAY, "daggers"] = "The Keeper of Daggers deifies an ancient blade at W3832S2924.";
		$BotInfoChat["AncientTimeKeeper", "daggers", 0] = "keepers";
		$BotInfoChat["AncientTimeKeeper", SAY, "old"] = "The Keeper of Old maintains an eternal monument at E408N623.";
		$BotInfoChat["AncientTimeKeeper", "old", 0] = "keepers";
		$BotInfoChat["AncientTimeKeeper", SAY, "earth"] = "The Keeper of Earth occupies a cliff at W2355S1302.";
		$BotInfoChat["AncientTimeKeeper", "earth", 0] = "keepers";



$BotInfoChat["KeeperOfSolace", EVAL, "hello"] = "rpg::WeatherDevicesAreFull";
$BotInfoChat["KeeperOfSolace", SAY, "allfull"] = "Welcome. You seek to Equalize and begin a new cycle.";
$BotInfoChat["KeeperOfSolace", "allfull", 0] = "equalize";
	$BotInfoChat["KeeperOfSolace", SAY, "equalize"] = "Do you have what I require?";
	$BotInfoChat["KeeperOfSolace", "equalize", 0] = "require";
		$BotInfoChat["KeeperOfSolace", "!", "require"] = "KeldrinRobe 1 KeldrinArmor 1 JusticeStaff 1 RockLauncher 1 Lacerator 1 KeldriniteLS 1 TitanCrusher 1 " @
														 "BookOfArmor 1 BookOfJustice 1 BookOfEarth 1 BookOfSwords 1 BookOfDaggers 1 BookOfOld 1 BookOfRobes 1";
		$BotInfoChat["KeeperOfSolace", "=", "require"] = "nohasstuff";
		$BotInfoChat["KeeperOfSolace", SAY, "require"] = "Will you give me what I require?";
			$BotInfoChat["KeeperOfSolace", "require", 0] = "yes";
			$BotInfoChat["KeeperOfSolace", "require", 1] = "no";
				$BotInfoChat["KeeperOfSolace", EVAL, "yes"] = "rpg::SmithTheEqualizer";
				$BotInfoChat["KeeperOfSolace", SAY, "yes"] = "Be still as The Equalizer is forged and delivered.";

$BotInfoChat["KeeperOfSolace", SAY, "nohasstuff"] = "7 books. 7 items. Return with them.";


//================================================================================================================================================================
// Delkin Heights
//================================================================================================================================================================
$BotInfoChat["DelkGreeter", SAY, "hello"] = "Welkin' to Delkin'! Ha ha ha! Ah ahh... ahhh... yeah. We don't get many visitors here anymore. Bar's closed. Sorry.";
$BotInfoChat["DelkGreeter", "hello", 0] = "people";
$BotInfoChat["DelkGreeter", "hello", 1] = "bar";
$BotInfoChat["DelkGreeter", "hello", 2] = "delkin";

	$BotInfoChat["DelkGreeter", SAY, "bar"] = "There's a bar way at the top, but nobody goes there anymore. I get winded just thinking about the climb! Phew, it's enough work just getting to the first floor!";
	$BotInfoChat["DelkGreeter", "bar", 0] = "hello";
	
	$BotInfoChat["DelkGreeter", SAY, "people"] = "There's me! Jeminia's got the next floor up. Then Larcy. Then Acostus. Philo's one more up. That's all of us. Just weather devices after that.";
	$BotInfoChat["DelkGreeter", "people", 0] = "jeminia";
	$BotInfoChat["DelkGreeter", "people", 1] = "larcy";
	$BotInfoChat["DelkGreeter", "people", 2] = "acostus";
	$BotInfoChat["DelkGreeter", "people", 3] = "philo";
	$BotInfoChat["DelkGreeter", "people", 4] = "devices";
	$BotInfoChat["DelkGreeter", "people", 5] = "hello";
		$BotInfoChat["DelkGreeter", SAY, "jeminia"] = "Jeminia's our trader. She makes sure those rafts still have a reason to come here.";
		$BotInfoChat["DelkGreeter", "jeminia", 0] = "people";
		$BotInfoChat["DelkGreeter", "jeminia", 1] = "hello";
		$BotInfoChat["DelkGreeter", SAY, "larcy"] = "She's our smith. Out here on a mission for The Mandate. Takes whatever Jeminia can't use and makes it gold.";
		$BotInfoChat["DelkGreeter", "larcy", 0] = "people";
		$BotInfoChat["DelkGreeter", "larcy", 1] = "hello";
		$BotInfoChat["DelkGreeter", SAY, "acostus"] = "Acostus keeps himself locked up most of the time. He doesn't have any hands. If you can get him to talk, tell me how you did it!";
		$BotInfoChat["DelkGreeter", "acostus", 0] = "people";
		$BotInfoChat["DelkGreeter", "acostus", 1] = "hello";
		$BotInfoChat["DelkGreeter", SAY, "philo"] = "The last Wizard. He does some research at the Crypt, but is mostly here to watch over the old weather devices.";
		$BotInfoChat["DelkGreeter", "philo", 0] = "weather";
		$BotInfoChat["DelkGreeter", "philo", 1] = "devices";
		$BotInfoChat["DelkGreeter", "philo", 2] = "crypt";
		$BotInfoChat["DelkGreeter", "philo", 3] = "hello";
			$BotInfoChat["DelkGreeter", SAY, "crypt"] = "Just up the mountain -- but if I were you, I'd stay out. It's been damned ever since the weather changed.";
			$BotInfoChat["DelkGreeter", "crypt", 0] = "weather";
			$BotInfoChat["DelkGreeter", "crypt", 1] = "hello";
			$BotInfoChat["DelkGreeter", SAY, "weather"] = "The Wizards stopped using their weather devices thinking the world would ease off on the drought. Worse happened. Eviscera broke loose.";
			$BotInfoChat["DelkGreeter", "weather", 0] = "weather";
			$BotInfoChat["DelkGreeter", "weather", 1] = "eviscera";
			$BotInfoChat["DelkGreeter", "weather", 2] = "devices";			
			$BotInfoChat["DelkGreeter", "weather", 3] = "hello";
			$BotInfoChat["DelkGreeter", SAY, "eviscera"] = "Zatan is real! Churls sure are real. So are Imps. I'm ready to believe anything. I hope it's not the end of the world.";
			$BotInfoChat["DelkGreeter", "eviscera", 0] = "hello";
			$BotInfoChat["DelkGreeter", SAY, "devices"] = "There's six of them. One for each quater of a day. Philo and Hemac are the only Wizards still alive who know how to use them. Useless to everyone else.";
			$BotInfoChat["DelkGreeter", "devices", 0] = "people";
			$BotInfoChat["DelkGreeter", "devices", 1] = "hello";
	$BotInfoChat["DelkGreeter", SAY, "delkin"] = "That's where you are! The Heights used to be occupied by Wizards who controlled the weather. Now it's just us people.";
	$BotInfoChat["DelkGreeter", "delkin", 0] = "people";
	$BotInfoChat["DelkGreeter", "delkin", 1] = "weather";
	$BotInfoChat["DelkGreeter", "delkin", 2] = "hello";

//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["LarcyChats", SAY, "hello"] = "Hey there! You here to #smith? Over in the corner. I've got metals if you need them. Or, maybe you could help me with something...?";
$BotInfoChat["LarcyChats", "hello", 0] = "metals";
$BotInfoChat["LarcyChats", "hello", 1] = "help";
	$BotInfoChat["LarcyChats", EVAL, "metals"] = "bottalk::SetupShop";
	$BotInfoChat["LarcyChats", SAY, "metals"] = "Have a look.";
	
	$BotInfoChat["LarcyChats", "!", "help"] = "TheLawfulMasses 1";
	$BotInfoChat["LarcyChats", "=", "help"] = "ready";
	$BotInfoChat["LarcyChats", "~+", "help"] = "MandateCrate 1 MandateBackpack 1 MandateSatchel 1";
	$BotInfoChat["LarcyChats", "~-", "help"] = "ready";
	$BotInfoChat["LarcyChats", SAY, "help"] = "Finally, a Mandatory! You here to deliver some ore? I've got a load ready -- if you're ready?";
	$BotInfoChat["LarcyChats", "help", 0] = "ready";

	$BotInfoChat["LarcyChats", EVAL, "ready"] = "rpg::TheMandateDeliverySetup";
	$BotInfoChat["LarcyChats", SAY, "ready"] = "Ahh, sorry. I thought you were from The Mandate. Not many other people drop by here. Talk to them in Keldrin Town or Fort Ethren.";
	$BotInfoChat["LarcyChats", "ready", 0] = "hello";

//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["Acostus", SAY, "hello"] = "(Acostus looks through you, not at you)";
$BotInfoChat["Acostus", "hello", 0] = "wave";	
	$BotInfoChat["Acostus", SAY, "wave"] = "(you wave, and he stands still)";
	$BotInfoChat["Acostus", "wave", 0] = "snap";
	$BotInfoChat["Acostus", "wave", 1] = "hello";
		$BotInfoChat["Acostus", SAY, "snap"] = "(you snap your fingers, and Acostus turns his eyes to meet yours)";
		$BotInfoChat["Acostus", "snap", 0] = "slap";
		$BotInfoChat["Acostus", "snap", 1] = "hello";
			$BotInfoChat["Acostus", SAY, "slap"] = "(Acostus sighs heavily)";
			$BotInfoChat["Acostus", "slap", 0] = "again";
			$BotInfoChat["Acostus", "slap", 1] = "hello";
				$BotInfoChat["Acostus", SAY, "again"] = "(...still nothing -- but an idea hits you...)";
				$BotInfoChat["Acostus", "again", 0] = "disrobe";
				$BotInfoChat["Acostus", "again", 1] = "hello";
					$BotInfoChat["Acostus", SAY, "disrobe"] = "Oh, no! No! Please. Please, I've come to this quiet place so that... no. Please, stop!";
					$BotInfoChat["Acostus", "disrobe", 0] = "pants";
					$BotInfoChat["Acostus", "disrobe", 1] = "hello";
						$BotInfoChat["Acostus", SAY, "pants"] = "Stop this at once! Look, I'll talk if you put everything back on, alright?!";
						$BotInfoChat["Acostus", "pants", 0] = "alright";
						$BotInfoChat["Acostus", "pants", 1] = "hello";
	$BotInfoChat["Acostus", SAY, "alright"] = "No, Acostus is not my name. Yes, it was given to me after I lost my hands. No, I don't want to talk about it.";
	$BotInfoChat["Acostus", "alright", 0] = "hands";
	$BotInfoChat["Acostus", "alright", 1] = "hello";
		$BotInfoChat["Acostus", SAY, "hands"] = "I said -- ah, fine. So I touch stuff I shouldn't, alright? Can't go anywhere near Jaten or the Black Market ever again.";
		$BotInfoChat["Acostus", "hands", 0] = "market";
		$BotInfoChat["Acostus", "hands", 1] = "jaten";
		$BotInfoChat["Acostus", "hands", 2] = "alright";
			$BotInfoChat["Acostus", SAY, "market"] = "Lots of Thieves and loot. Lots of shiny things. So much to touch. I'm not telling you where it is. That's final.";
			$BotInfoChat["Acostus", "market", 0] = "alright";
			$BotInfoChat["Acostus", SAY, "jaten"] = "It's a town down the river. Lots of women. Not so much to touch. Unless you don't need your hands.";
			$BotInfoChat["Acostus", "jaten", 0] = "alright";
			$BotInfoChat["Acostus", "jaten", 1] = "slap";

//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["Philo", SAY, "hello"] = "Greetings, Newcomer. Few venture this far. Have you come to speak of the weather?";
$BotInfoChat["Philo", "hello", 0] = "weather";

	$BotInfoChat["Philo", SAY, "weather"] = "It is true. Once upon a time, Keldrin had days. Seasons. But that time has passed. The end approaches.";
	$BotInfoChat["Philo", "weather", 0] = "days";
	$BotInfoChat["Philo", "weather", 1] = "seasons";
	$BotInfoChat["Philo", "weather", 2] = "time";
	$BotInfoChat["Philo", "weather", 3] = "end";
	$BotInfoChat["Philo", "weather", 4] = "hello";
	
		$BotInfoChat["Philo", SAY, "days"] = "Stars will light the sky as the devices are energized. Without them running, the fog clouds all.";
		$BotInfoChat["Philo", "days", 0] = "fog";
		$BotInfoChat["Philo", "days", 1] = "energized";		
		$BotInfoChat["Philo", "days", 2] = "devices";
		$BotInfoChat["Philo", "days", 3] = "weather";		
		$BotInfoChat["Philo", "days", 4] = "hello";
		
		$BotInfoChat["Philo", SAY, "devices"] = "There are six, one on each floor of Delkin. I suppose the end would come quicker if we energized them again.";
		$BotInfoChat["Philo", "devices", 0] = "end";
		$BotInfoChat["Philo", "devices", 1] = "energized";
		$BotInfoChat["Philo", "devices", 2] = "weather";		
		
			$BotInfoChat["Philo", SAY, "fog"] = "So our wisdom failed us. With darkness came hell itself. Mere weeks after discharge, Eviscera erupted, and demons spilled from the cracks.";
			$BotInfoChat["Philo", "fog", 0] = "eviscera";
			$BotInfoChat["Philo", "fog", 1] = "discharge";
			$BotInfoChat["Philo", "fog", 2] = "weather";
			
				$BotInfoChat["Philo", SAY, "eviscera"] = "Zatan commands this world now. We sealed off the Dig Site and Overville too late to stop his advance.";
				$BotInfoChat["Philo", "eviscera", 0] = "dig";
				$BotInfoChat["Philo", "eviscera", 1] = "overville";
				$BotInfoChat["Philo", "eviscera", 2] = "fog";
				
					$BotInfoChat["Philo", SAY, "dig"] = "The Abandoned Dig Site near Wyzanhyde was buried in stone. Our research on ancient matters came to a close.";
					$BotInfoChat["Philo", "dig", 0] = "fog";
					$BotInfoChat["Philo", SAY, "overville"] = "Overville was once a bustling town full of life. Demons burst through their mines. We encased the town in ice. Do not go there.";
					$BotInfoChat["Philo", "overville", 0] = "fog";
				
				$BotInfoChat["Philo", SAY, "discharge"] = "The weather devices are of no use now. To start them again would require truly incredible amounts of natural and sacrificial resources.";
				$BotInfoChat["Philo", "discharge", 0] = "natural";
				$BotInfoChat["Philo", "discharge", 1] = "sacrificial";
				$BotInfoChat["Philo", "discharge", 2] = "devices";
				$BotInfoChat["Philo", "discharge", 3] = "fog";
			
			$BotInfoChat["Philo", SAY, "energized"] = "We used crystal and various elements to power the devices. Two of the devices were early experiments and need lubricants.";
			$BotInfoChat["Philo", "energized", 0] = "crystal";
			$BotInfoChat["Philo", "energized", 1] = "elements";			
			$BotInfoChat["Philo", "energized", 2] = "devices";
			$BotInfoChat["Philo", "energized", 3] = "lubricants";
		
		$BotInfoChat["Philo", SAY, "lubricants"] = "Greenskins. Vigorous Vials. Neither are particularly pleasent to handle. The rest of the devices were built with crystal resonance.";
		$BotInfoChat["Philo", "lubricants", 0] = "crystal";
		$BotInfoChat["Philo", "lubricants", 1] = "devices";
		
		$BotInfoChat["Philo", SAY, "crystal"] = "Control Crystals. Magic Dust. Mana Rocks. All of these used to be as plentiful as elements, but Gat has found other 'uses' for Control Crystals.";
		$BotInfoChat["Philo", "crystal", 0] = "elements";
		$BotInfoChat["Philo", "crystal", 1] = "gat";
		
		$BotInfoChat["Philo", SAY, "gat"] = "It is our terrible fault. This 'Gat' is a Churl. He commands an army of Deluded... using our Control Crystals to poison their minds.";
		$BotInfoChat["Philo", "gat", 0] = "hello";
		
		$BotInfoChat["Philo", SAY, "elements"] = "The devices were energized with an exhaustive list of materials. Granite, Diamond, Turquoise, Sapphire, Jade, Emerald, Ruby, Keldrinite, Topaz and Gold.";
		$BotInfoChat["Philo", "elements", 0] = "devices";
		$BotInfoChat["Philo", "elements", 1] = "energized";		
		
		$BotInfoChat["Philo", SAY, "seasons"] = "Seasons, Newcomer. Cycles. All life comes and goes. We steer the wind. You know this, neh?";
		$BotInfoChat["Philo", "seasons", 0] = "weather";
		$BotInfoChat["Philo", "seasons", 1] = "days";
		$BotInfoChat["Philo", "seasons", 2] = "hello";
		
		$BotInfoChat["Philo", SAY, "time"] = "Time is all there is, or never was. Does it matter? Will we find out?";
		$BotInfoChat["Philo", "time", 0] = "weather";
		$BotInfoChat["Philo", "time", 1] = "hello";
		
		$BotInfoChat["Philo", SAY, "end"] = "The end is in progress, Newcomer. It is only a matter of time before this world collapses.";
		$BotInfoChat["Philo", "end", 0] = "weather";
		$BotInfoChat["Philo", "end", 1] = "hello";
			
			
			
			
//================================================================================================================================================================
// Wellsprings
//================================================================================================================================================================
$BotInfoChat["WSGardener1", SAY, "hello"] = "WHAT. WHAT?!";
$BotInfoChat["WSGardener1", "hello", 0] = "what";
$BotInfoChat["WSGardener1", "hello", 1] = "wellsprings";
	$BotInfoChat["WSGardener1", SAY, "what"] = "WHY?!?!?!?! WHY... WHAT?!!";
	$BotInfoChat["WSGardener1", "what", 0] = "hello";
	$BotInfoChat["WSGardener1", SAY, "wellsprings"] = "SHUT UP. SHUT UP, SHUT UP, SHUT UP!";
	$BotInfoChat["WSGardener1", "wellsprings", 0] = "hello";
	
//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["WSWinner1", SAY, "hello"] = "They can't catch me if I'm invisible. I'm sane. Not mad like the others around here.";
$BotInfoChat["WSWinner1", "hello", 0] = "they";
$BotInfoChat["WSWinner1", "hello", 1] = "mad";
$BotInfoChat["WSWinner1", "hello", 2] = "invisible";
	$BotInfoChat["WSWinner1", SAY, "invisible"] = "Of course I'm invisible. I'm just *letting* you see me right now.";
	$BotInfoChat["WSWinner1", "invisible", 0] = "hello";
	$BotInfoChat["WSWinner1", SAY, "they"] = "You know, those ugly things that come out of the well and try to kill us every night? Yeah, them.";
	$BotInfoChat["WSWinner1", "they", 0] = "ugly";
	$BotInfoChat["WSWinner1", "they", 1] = "well";
	$BotInfoChat["WSWinner1", "they", 2] = "kill";
	$BotInfoChat["WSWinner1", "they", 3] = "night";
	$BotInfoChat["WSWinner1", "they", 4] = "hello";
		$BotInfoChat["WSWinner1", SAY, "night"] = "Bit misleading. It's always night around here now.";
		$BotInfoChat["WSWinner1", "night", 0] = "always";
		$BotInfoChat["WSWinner1", "night", 2] = "hello";
			$BotInfoChat["WSWinner1", SAY, "always"] = "The Wizards used to control weather at Delkin Heights, but they stopped, hoping to slow the drought. Just made things worse.";
			$BotInfoChat["WSWinner1", "always", 0] = "delkin";
			$BotInfoChat["WSWinner1", "always", 1] = "weather";
			$BotInfoChat["WSWinner1", "always", 2] = "hello";
			$BotInfoChat["WSWinner1", SAY, "delkin"] = "Delkin is mostly abandoned now. But the weather devices are still there. Too high up to steal.";
			$BotInfoChat["WSWinner1", "delkin", 0] = "weather";
			$BotInfoChat["WSWinner1", "delkin", 1] = "hello";
		$BotInfoChat["WSWinner1", SAY, "kill"] = "Most of us have gone mad fending them off. Not me, though! I don't know why those ugly Churls want us dead. We don't even have anything.";
		$BotInfoChat["WSWinner1", "kill", 0] = "mad";
		$BotInfoChat["WSWinner1", "kill", 1] = "ugly";
		$BotInfoChat["WSWinner1", "kill", 2] = "churls";
		$BotInfoChat["WSWinner1", "kill", 3] = "hello";
			$BotInfoChat["WSWinner1", SAY, "mad"] = "I'm smart. Not like the rest. I learn things. That's how I found out I need to be invisible!";
			$BotInfoChat["WSWinner1", "mad", 0] = "hello";
		$BotInfoChat["WSWinner1", SAY, "well"] = "Some of us stuck around and tried to make things work after the river dried up for good. Bad idea.";
		$BotInfoChat["WSWinner1", "well", 0] = "river";
		$BotInfoChat["WSWinner1", "well", 1] = "druids";
		$BotInfoChat["WSWinner1", "well", 2] = "hello";		
			$BotInfoChat["WSWinner1", SAY, "druids"] = "Yep, they moved over to 'Oasis'. It's just a pond, but it's enough for them to work with. Those canyons were rivers 10 years ago.";
			$BotInfoChat["WSWinner1", "druids", 0] = "oasis";
			$BotInfoChat["WSWinner1", "druids", 1] = "river";
			$BotInfoChat["WSWinner1", "druids", 2] = "hello";
				$BotInfoChat["WSWinner1", SAY, "oasis"] = "Druids needed a place near water. Which is hard to find now.";
				$BotInfoChat["WSWinner1", "oasis", 0] = "water";
				$BotInfoChat["WSWinner1", "oasis", 1] = "hello";
				$BotInfoChat["WSWinner1", SAY, "river"] = "Keldrina, Najette and Ehtrine still flow but the rest? Dry.";
				$BotInfoChat["WSWinner1", "river", 0] = "water";
				$BotInfoChat["WSWinner1", "river", 1] = "hello";
					$BotInfoChat["WSWinner1", SAY, "water"] = "The water just started drying up! I heard it's part of a cycle. Something about, uhh, 'Equalizing'.";
					$BotInfoChat["WSWinner1", "water", 0] = "equalizing";
					$BotInfoChat["WSWinner1", "water", 1] = "hello";
						$BotInfoChat["WSWinner1", SAY, "equalizing"] = "Don't ask me about what that means. I was REALLY drunk and wound up this placed called 'The Restaurant at the End of the World'.";
						$BotInfoChat["WSWinner1", "equalizing", 0] = "restaurant";
						$BotInfoChat["WSWinner1", "equalizing", 1] = "hello";
							$BotInfoChat["WSWinner1", SAY, "restaurant"] = "Hah, don't even ask me where it is! I barely got home that night. Probably imagined it all, anyway.";
							$BotInfoChat["WSWinner1", "restaurant", 0] = "hello";
		$BotInfoChat["WSWinner1", SAY, "ugly"] = "They're called Churls. I've studied them. The eye is their only organ. Ever heard of a 'cyclops'? Yeah, like that, but smaller.";
		$BotInfoChat["WSWinner1", "ugly", 0] = "churls";
		$BotInfoChat["WSWinner1", "ugly", 1] = "eye";
		$BotInfoChat["WSWinner1", "ugly", 2] = "hello";
			$BotInfoChat["WSWinner1", SAY, "eye"] = "They don't feel anything. Don't eat. Don't sleep. They just stare, and fight. Churls must be from Eviscera.";
			$BotInfoChat["WSWinner1", "eye", 0] = "churls";
			$BotInfoChat["WSWinner1", "eye", 1] = "eviscera";
			$BotInfoChat["WSWinner1", "eye", 2] = "hello";
				$BotInfoChat["WSWinner1", SAY, "eviscera"] = "Yeah, that's where people go if they fall off the edge of the world. It's flat, didn't you know?";
				$BotInfoChat["WSWinner1", "eviscera", 0] = "flat";
				$BotInfoChat["WSWinner1", "eviscera", 1] = "hello";
					$BotInfoChat["WSWinner1", SAY, "flat"] = "Shut up! I'm smart, remember? It's flat. Kill yourself trying to prove me wrong, see if I care.";
					$BotInfoChat["WSWinner1", "flat", 0] = "hello";
			$BotInfoChat["WSWinner1", SAY, "churls"] = "Whenever the water dries up in a divine cavern, these things show up. It can't be a coincidence.";
			$BotInfoChat["WSWinner1", "churls", 0] = "cavern";
			$BotInfoChat["WSWinner1", "churls", 1] = "hello";
				$BotInfoChat["WSWinner1", SAY, "cavern"] = "They took over the well here in Wellsprings, and the Ancient Dig Site over by Oasis. That's all I know.";
				$BotInfoChat["WSWinner1", "cavern", 0] = "hello";


//================================================================================================================================================================
// Order of Qod | Hazard
//		Perks:		Access to Septic System; Restoration gets better with rank; at max rank you can #brew potions
//		Fetch:		Toxin VileSubtsance SkeletonBone ImpClaw BloodyPentagram
//		Hunt:		randomly placed LostSoulPenant that drop PurgatingSoul. Will spawn in any of 	Graveyard  Damned Crypt  Undercity  Deadwood  Cavern of Torment
//================================================================================================================================================================
$BotInfoChat["OrderOfQodGM", SAY, "hello"] = "Welcome. Are you here to perform cleansing for the divine Qod?";
$BotInfoChat["OrderOfQodGM", "hello", 0] = "cleansing";
$BotInfoChat["OrderOfQodGM", "hello", 1] = "no";
	$BotInfoChat["OrderOfQodGM", SAY, "no"] = "Blessings and Justice, Newcomer.";
	
	$BotInfoChat["OrderOfQodGM", "!", "cleansing"] = "MarkOfOrder 1";
	$BotInfoChat["OrderOfQodGM", "=", "cleansing"] = "membership";
	$BotInfoChat["OrderOfQodGM", "~+", "cleansing"] = "SlayerGear 1 WayLink 1 Iradnium 1 TheLawfulMasses 1";
	$BotInfoChat["OrderOfQodGM", "~-", "cleansing"] = "badguild";
	$BotInfoChat["OrderOfQodGM", SAY, "cleansing"] = "Member, it is good to see you. Are you here to incinerate awful substances? Or perhaps you have saved a soul?";
	$BotInfoChat["OrderOfQodGM", EVAL, "cleansing"] = "rpg::OrderOfQodRewardCheck";
	$BotInfoChat["OrderOfQodGM", "cleansing", 0] = "incinerate";
	$BotInfoChat["OrderOfQodGM", "cleansing", 1] = "soul";
	$BotInfoChat["OrderOfQodGM", "cleansing", 2] = "hello";
	
	$BotInfoChat["OrderOfQodGM", SAY, "alreadycompleted"] = "Ordergiver! It is good to see you. Do you have anything you need to incinerate? Or perhaps you have saved a soul?";
	$BotInfoChat["OrderOfQodGM", "alreadycompleted", 0] = "incinerate";
	$BotInfoChat["OrderOfQodGM", "alreadycompleted", 1] = "soul";
	$BotInfoChat["OrderOfQodGM", "alreadycompleted", 2] = "hello";
	
	$BotInfoChat["OrderOfQodGM", SAY, "completed1"] = "Ordertaker, your cleansing is commendable. This Book Of Concoctions contains instruction to #brew many potions. May you heal the sick.";
	$BotInfoChat["OrderOfQodGM", "completed1", 0] = "incinerate";
	$BotInfoChat["OrderOfQodGM", "completed1", 1] = "soul";
	$BotInfoChat["OrderOfQodGM", "completed1", 2] = "hello";
	
	$BotInfoChat["OrderOfQodGM", SAY, "completed2"] = "Ordergiver, your cleansing is legendary! This Sign of Qod will protect you with FAVOR on your travels. We have nothing more to give. Be well.";
	$BotInfoChat["OrderOfQodGM", "completed2", 0] = "incinerate";
	$BotInfoChat["OrderOfQodGM", "completed2", 1] = "soul";
	$BotInfoChat["OrderOfQodGM", "completed2", 2] = "hello";

	$BotInfoChat["OrderOfQodGM", EVAL, "soul"] 	= "rpg::HouseHuntQuest ,\"Order Of Qod\",\"Penant\", 1, 30, \"PurgatingSoul\", 1000, \"saving\"";
	$BotInfoChat["OrderOfQodGM", SAY, "soul"] 	= "Ordertaker. %NAME wishes to find Qod, and so we will help. He is near a %HINT. Find him, and return his soul.";
	$BotInfoChat["OrderOfQodGM", "soul", 0] = "cleansing";
	$BotInfoChat["OrderOfQodGM", SAY, "gavePurgatingSoul"] = "Yes, we will save this soul. Do you seek others to save? Or, perhaps you bring vile items to incinerate?";
	$BotInfoChat["OrderOfQodGM", "gavePurgatingSoul", 0] = "incinerate";
	$BotInfoChat["OrderOfQodGM", "gavePurgatingSoul", 1] = "soul";
	$BotInfoChat["OrderOfQodGM", "gavePurgatingSoul", 2] = "hello";
	
	$BotInfoChat["OrderOfQodGM", EVAL, "incinerate"] = "rpg::HouseFetchQuestAwards ,\"Toxin VileSubtsance SkeletonBone ImpClaw BloodyPentagram\"," @
																					"\"1 10 25 50 75\", \"incinerating\", \"burn\"";
	$BotInfoChat["OrderOfQodGM", SAY, "burn"] 	= "Yes, we will cleanse these terrible things in the holy fire. Please continue to show devotion.";
	$BotInfoChat["OrderOfQodGM", "burn", 0] 	= "cleansing";
	$BotInfoChat["OrderOfQodGM", "burn", 1] 	= "hello";	
	$BotInfoChat["OrderOfQodGM", SAY, "noburn"] = "Hmm, you don't have any Undead or Demonic materials.";
	$BotInfoChat["OrderOfQodGM", "noburn", 0] 	= "cleansing";
	$BotInfoChat["OrderOfQodGM", "noburn", 1] 	= "hello";
	
	$BotInfoChat["OrderOfQodGM", SAY, "membership"] = "Newcomer, Qod rains Justice upon you. Salvation lies at the end of our holy mission. Join us?";
	$BotInfoChat["OrderOfQodGM", "membership", 0] = "why";
	$BotInfoChat["OrderOfQodGM", "membership", 1] = "perks";
	$BotInfoChat["OrderOfQodGM", "membership", 2] = "join";
	$BotInfoChat["OrderOfQodGM", "membership", 3] = "hello";
	
		$BotInfoChat["OrderOfQodGM", SAY, "badguild"] = "We do not have business, Newcomer. You belong to a different organization. Are you lost?";
		$BotInfoChat["OrderOfQodGM", "badguild", 0] = "hello";
	
		$BotInfoChat["OrderOfQodGM", SAY, "why"] = "We must cleanse Keldrin of its unclean souls! Qod demands Justice and Sanitation before he will allow the End. Help us with deliverance!";
		$BotInfoChat["OrderOfQodGM", "why", 0] = "membership";
	
		$BotInfoChat["OrderOfQodGM", SAY, "perks"] = "Members of our Order incinerate vile substances, #brew potions and become Restoration masters in pursuit of cleansing Keldrin.";
		$BotInfoChat["OrderOfQodGM", "perks", 0] = "join";
		$BotInfoChat["OrderOfQodGM", "perks", 1] = "membership";
			
		$BotInfoChat["OrderOfQodGM", "~+", "join"] = "SlayerGear 1 WayLink 1 Iradnium 1 TheLawfulMasses 1";
		$BotInfoChat["OrderOfQodGM", "~-", "join"] = "badguild";
		$BotInfoChat["OrderOfQodGM", SAY, "join"] = "Welcome, Ordertaker. Cherish your new Mark of Order. I have also given you a key to the Septic Sewer in town, where you will begin.";
		$BotInfoChat["OrderOfQodGM", EVAL, "join"] = "rpg::JoinHouse ,\"Order Of Qod\"";
		$BotInfoChat["OrderOfQodGM", "join", 0] = "cleansing";
		$BotInfoChat["OrderOfQodGM", "join", 1] = "hello";

//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["HazardSecret1", SAY, "hello"] = "Oh, uhh, we were just... uhh...";
$BotInfoChat["HazardSecret2", SAY, "hello"] = "Hi! Uhh, many prayers and praise Qod! Yeap! Nothing to see here, thanks for checking on us!";

//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["HazardDrunk1", SAY, "hello"] = "A-oh looki' this walkin chud kuffer. Yuu... you *hick*... you lookin' ann ME?!";
$BotInfoChat["HazardDrunk2", SAY, "hello"] = "S'art a fight wit me would ya!";
$BotInfoChat["HazardDrunk3", SAY, "hello"] = "(clearly sauced, the drunk wobbles around and nearly falls over)";
$BotInfoChat["HazardDrunk4", SAY, "hello"] = "Y-y-y-you g-g-got an-n-any ssss-ss-stones?";
$BotInfoChat["HazardDrunk5", SAY, "hello"] = "Ge'a my face ye slunt. I's drinkin'. Chudmuddar Newcomers ain't welcome.";

//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["HazardPriest1", SAY, "hello"] = "Get me out of this forsaken place.";
$BotInfoChat["HazardPriest2", SAY, "hello"] = "The END is here! Qod REMOVES the water to pay for YOUR sins!";
$BotInfoChat["HazardPriest3", SAY, "hello"] = "Pray for us.";
$BotInfoChat["HazardPriest4", SAY, "hello"] = "Light guide you.";

$BotInfoChat["HazardPriest1", EVAL, "hello"] = "rpg::NPCFullHealTarget";
$BotInfoChat["HazardPriest2", EVAL, "hello"] = "rpg::NPCFullHealTarget";
$BotInfoChat["HazardPriest3", EVAL, "hello"] = "rpg::NPCFullHealTarget";
$BotInfoChat["HazardPriest4", EVAL, "hello"] = "rpg::NPCFullHealTarget";


//================================================================================================================================================================
// Luminous Dawn | Mercator
//	!!	Perks:		Access to #fasttravel; gets an #outpost at the end. Waylink will #recall you to Mercator
//		Fetch:		Effigy AntanariMask ElfEar ClippedWing LettingBlade EnchantedStone
//	!!	Hunt:		Places to put down wayshrines
//================================================================================================================================================================
$BotInfoChat["MercatorDancer", SAY, "hello"] = "Hey there, sailor. Want a dance? It's free, since the world's ending and all.";
$BotInfoChat["MercatorDancer", "hello", 0] = "yes";
$BotInfoChat["MercatorDancer", "hello", 1] = "no";	
	$BotInfoChat["MercatorDancer", SAY, "yes"] = "(the dancer gyrates around)";
	$BotInfoChat["MercatorDancer", EVAL, "yes"] = "rpg::BotDance";
	$BotInfoChat["MercatorDancer", "yes", 0] = "hello";
	$BotInfoChat["MercatorDancer", SAY, "no"] = "Suit yourself.";

//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["LuminousDawnGM", SAY, "hello"] = "You here about the 'specials', or do we have some business?";
$BotInfoChat["LuminousDawnGM", "hello", 0] = "business";
$BotInfoChat["LuminousDawnGM", "hello", 1] = "specials";
	$BotInfoChat["LuminousDawnGM", SAY, "no"] = "You said it, not me. Later then.";
	
	$BotInfoChat["LuminousDawnGM", SAY, "specials"] = "Free dances! It's the 'End of the World', after all. Just keep your hands to yourself. Got it?";
	$BotInfoChat["LuminousDawnGM", "specials", 0] = "hello";	
	
	$BotInfoChat["LuminousDawnGM", EVAL, "business"] = "rpg::LuminousDawnCheckMembership";
	function rpg::LuminousDawnCheckMembership(%clientId) { if(rpg::IsMemberOfHouse(%clientId,"Luminous Dawn")) return ""; return "join"; }
	$BotInfoChat["LuminousDawnGM", SAY, "business"] = "Alright. Let's talk. You need info on Elders? You got any more stuff to give us? Other way 'round, maybe you need another Way Link?";
	$BotInfoChat["LuminousDawnGM", "business", 0] = "stuff";
	$BotInfoChat["LuminousDawnGM", "business", 1] = "elders";
	$BotInfoChat["LuminousDawnGM", "business", 2] = "waylink";
	$BotInfoChat["LuminousDawnGM", "business", 3] = "hello";
	
		$BotInfoChat["LuminousDawnGM", EVAL, "stuff"] = "rpg::HouseFetchQuestAwards ,\"Effigy AntanariMask ElfEar ClippedWing LettingBlade EnchantedStone\"," @
																						"\"50 100 50 125 10 25\", \"providing\", \"thanks\"";
		
		$BotInfoChat["LuminousDawnGM", SAY, "thanks"] 	= "Whoa! That's quite a haul. Thanks for handing over all that stuff. Now... back to business?";
		$BotInfoChat["LuminousDawnGM", "thanks", 0] 	= "business";		
		$BotInfoChat["LuminousDawnGM", SAY, "nothanks"] = "We need the elders' stuff to keep the waylinks up. Enchanted Stones, Effigies, Clipped Wings, Letting Knives, Antanari Masks, and... Elf Eears.";
		$BotInfoChat["LuminousDawnGM", "nothanks", 0] 	= "elders";
		$BotInfoChat["LuminousDawnGM", "nothanks", 1] 	= "business";
				
		$BotInfoChat["LuminousDawnGM", SAY, "elders"] = "Givin' us hell, all of 'em. The Elves, Antanari, Jheriman, Kymera and Craven don't want Newcomers. Period.";
		$BotInfoChat["LuminousDawnGM", "elders", 0] = "elves";			
		$BotInfoChat["LuminousDawnGM", "elders", 1] = "antanari";		
		$BotInfoChat["LuminousDawnGM", "elders", 2] = "jheriman";			
		$BotInfoChat["LuminousDawnGM", "elders", 3] = "kymera";			
		$BotInfoChat["LuminousDawnGM", "elders", 4] = "craven";			
		$BotInfoChat["LuminousDawnGM", "elders", 5] = "business";
		
			$BotInfoChat["LuminousDawnGM", SAY, "jheriman"] = "Jherigo Pass used to act as a damn for the water, but... you know. When you find them, try to recover their Effigies for us.";
			$BotInfoChat["LuminousDawnGM", "jheriman", 0] = "elders";
			$BotInfoChat["LuminousDawnGM", "jheriman", 1] = "business";
			
			$BotInfoChat["LuminousDawnGM", SAY, "kymera"] = "The Kymera claim to be fallen angels. All we know is they like to stomp Newcomers, and their Wings are useful. Get some, eh?";
			$BotInfoChat["LuminousDawnGM", "kymera", 0] = "elders";
			$BotInfoChat["LuminousDawnGM", "kymera", 1] = "business";
			
			$BotInfoChat["LuminousDawnGM", SAY, "craven"] = "Craven are insane. Everything's about blood with them. If you survive them, bring me their Letting Knives as proof.";
			$BotInfoChat["LuminousDawnGM", "craven", 0] = "elders";
			$BotInfoChat["LuminousDawnGM", "craven", 1] = "business";
			
			$BotInfoChat["LuminousDawnGM", SAY, "elves"] = "The Elven Outpost is full of spiteful Elves. Smote a Newcomer just yesterday. If you 'find' any Elf Ears, bring them here, eh?";
			$BotInfoChat["LuminousDawnGM", "elves", 0] = "elders";
			$BotInfoChat["LuminousDawnGM", "elves", 1] = "business";
			
			$BotInfoChat["LuminousDawnGM", SAY, "antanari"] = "Antaris was an ancient city full of Antanari heroes. They can't let go, it seems. If you drop 'em, bring back their Masks, eh?";
			$BotInfoChat["LuminousDawnGM", "antanari", 0] = "elders";
			$BotInfoChat["LuminousDawnGM", "antanari", 1] = "business";
		
		$BotInfoChat["LuminousDawnGM", EVAL, "waylink"] = "rpg::LuminousDawnGiveAnotherLink";
		function rpg::LuminousDawnGiveAnotherLink(%clientId) {			
			if(Belt::HasThisStuff(%clientId,"WayLink") >= 0 || !rpg::IsMemberOfHouse(%clientId,"Luminous Dawn"))
				return "";
			Belt::GiveThisStuff(%clientId,"WayLink", 1, true);
			return "newlink";
		}
		$BotInfoChat["LuminousDawnGM", SAY, "newlink"] = "Here's another Way Link. Hopefully I don't need to explain how this works by now...?";
		$BotInfoChat["LuminousDawnGM", "newlink", 0] = "explain";
		$BotInfoChat["LuminousDawnGM", "newlink", 1] = "business";
		$BotInfoChat["LuminousDawnGM", SAY, "waylink"] = "That's right. Fast traveling around this huge world is essential. Do I need to explain how this works?";
		$BotInfoChat["LuminousDawnGM", "waylink", 0] = "explain";
		$BotInfoChat["LuminousDawnGM", "waylink", 1] = "business";
			$BotInfoChat["LuminousDawnGM", SAY, "explain"] = "You go to a zone with a Way Link and type #waylink to build a #fasttravel destination. That's it! We need all of the zones marked.";
			$BotInfoChat["LuminousDawnGM", "explain", 0] = "business";
			
		$BotInfoChat["LuminousDawnGM", SAY, "join"] = "Listen, Newcomer, I work with some good people, the Luminous Dawn. You look like a good person. Are you a good person?";
		$BotInfoChat["LuminousDawnGM", "join", 0] = "yes";
		$BotInfoChat["LuminousDawnGM", "join", 1] = "no";
		$BotInfoChat["LuminousDawnGM", "join", 2] = "hello";
		
			$BotInfoChat["LuminousDawnGM", SAY, "yes"] = "So us good people, we need to live someone. But the natives here? Bit pushy. We push back. And I think you could help.";
			$BotInfoChat["LuminousDawnGM", "yes", 0] = "sure";
			$BotInfoChat["LuminousDawnGM", "yes", 1] = "whoa";
			$BotInfoChat["LuminousDawnGM", "yes", 2] = "no";
			$BotInfoChat["LuminousDawnGM", "yes", 3] = "hello";
			
			$BotInfoChat["LuminousDawnGM", SAY, "whoa"] = "All we ask is you help set up a fast travel network. Well, mostly. You work hard enough, you get an outpost. You in?";
			$BotInfoChat["LuminousDawnGM", "whoa", 0] = "sure";
			$BotInfoChat["LuminousDawnGM", "whoa", 1] = "no";
			$BotInfoChat["LuminousDawnGM", "whoa", 2] = "hello";
		
				$BotInfoChat["LuminousDawnGM", "+", "sure"] = "LVLS 5";
				$BotInfoChat["LuminousDawnGM", "-", "sure"] = "toolow";			
				$BotInfoChat["LuminousDawnGM", SAY, "sure"] = "Ok, here's a Way Link. After you use the #waylink, we can all #fasttravel to it. Come see me after you use it. Or, we can talk business.";
				$BotInfoChat["LuminousDawnGM", EVAL, "sure"] = "rpg::JoinHouse ,\"Luminous Dawn\"";
				$BotInfoChat["LuminousDawnGM", "sure", 0] = "business";
				$BotInfoChat["LuminousDawnGM", "sure", 1] = "hello";
		
					$BotInfoChat["LuminousDawnGM", SAY, "toolow"] = "Ha ha, my mistake. Hold on a second. You're THAT new? Oh, honey, that's funny. Come back when you're at least level 5.";
		
		$BotInfoChat["LuminousDawnGM", SAY, "badguild"] = "Wait, what organization are you a part of? Get lost.";
		$BotInfoChat["LuminousDawnGM", "badguild", 0] = "hello";	
		
		
//================================================================================================================================================================
// The Mandate
//		Perks:		Access to shields. Upgrades for free with more rank
//		Fetch:		Bring smith gear to other smiths via Larcy
//		Hunt:		Fugitives, Murderers, and Zatanists
//================================================================================================================================================================
$BotInfoChat["Mandatory", "+", "hello"] = "STOLEN 1";
$BotInfoChat["Mandatory", "-", "hello"] = "isthief";
$BotInfoChat["Mandatory", SAY, "hello"] = "Citizen! Observe The Mandate. If you would like to serve, see Kelson or Drinia in Keldrin Center - or Tanja in Fort Ethren.";
$BotInfoChat["Mandatory", "hello", 0] = "thanks";
	$BotInfoChat["Mandatory", SAY, "thanks"] = "Be well, citizen.";
$BotInfoChat["Mandatory", SAY, "isthief"] = "Thief! AND a fool! What, you thought we wouldn't recognize you?";
$BotInfoChat["Mandatory", EVAL, "isthief"] = "rpg::NPCKillAndJailTarget";

//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["TheMandateGM", SAY, "hello"] = "Welcome. The Mandate is here to protect and serve. Do you require aid?";
$BotInfoChat["TheMandateGM", "hello", 0] = "mandate";
$BotInfoChat["TheMandateGM", "hello", 1] = "protect";
$BotInfoChat["TheMandateGM", "hello", 2] = "serve";
$BotInfoChat["TheMandateGM", "hello", 3] = "aid";
	$BotInfoChat["TheMandateGM", SAY, "nonsense"] = "Backwards, Newcomer. The Mandate does not serve *you*. But, as you wish.";
	$BotInfoChat["TheMandateGM", SAY, "no"] = "As you wish.";

	$BotInfoChat["TheMandateGM", SAY, "mandate"] = "The Mandate outlaws indecency, theft, and necromancy. Our principles are described in The Lawful Masses.";
	$BotInfoChat["TheMandateGM", "mandate", 0] = "hello";
	
	$BotInfoChat["TheMandateGM", SAY, "protect"] = "The Mandate necessarily requires we confront demons and deliver aid. Our signature armor is the shield.";
	$BotInfoChat["TheMandateGM", "protect", 0] = "hello";
	
	$BotInfoChat["TheMandateGM", SAY, "aid"] = "Yes, I would be happy to heal your wounds. Please be still a moment.";
	$BotInfoChat["TheMandateGM", EVAL, "aid"] = "rpg::NPCFullHealTarget";
	$BotInfoChat["TheMandateGM", "aid", 0] = "hello";

	$BotInfoChat["TheMandateGM", "!", "serve"] = "TheLawfulMasses 1";
	$BotInfoChat["TheMandateGM", "=", "serve"] = "join";
	$BotInfoChat["TheMandateGM", "~+", "serve"] = "SlayerGear 1 MarkOfOrder 1 Iradnium 1 WayLink 1";
	$BotInfoChat["TheMandateGM", "~-", "serve"] = "badguild";
	$BotInfoChat["TheMandateGM", SAY, "serve"] = "There's always Mandatory work. We have supplies to deliver, Outlaws to catch, and decency to uphold. Or, do you need a shield upgrade? ";
	$BotInfoChat["TheMandateGM", "serve", 0] = "supplies";
	$BotInfoChat["TheMandateGM", "serve", 1] = "outlaws";
	$BotInfoChat["TheMandateGM", "serve", 2] = "decency";
	$BotInfoChat["TheMandateGM", "serve", 3] = "upgrade";
	$BotInfoChat["TheMandateGM", "serve", 4] = "hello";
	
		$BotInfoChat["TheMandateGM", SAY, "badguild"] 	= "I'm sorry Newcomer, but The Mandate requires absolute observance. You cannot be part of other groups.";
		$BotInfoChat["TheMandateGM", "badguild", 0] 	= "hello";
	
		$BotInfoChat["TheMandateGM", SAY, "supplies"] 	= "Yes, we deliver supplies around Keldrin. Go see Larcy in Delkin Heights to pick up. Take the raft on the left leaving town.";
		$BotInfoChat["TheMandateGM", "supplies", 0] 	= "hello";
		
		$BotInfoChat["TheMandateGM", EVAL, "outlaws"] 	= "rpg::HouseHuntQuest ,\"Keldrin Mandate\",\"Fugitive Murderer Zatanist\", 1, 10, \"ThiefHands\", 1000, \"'handing' over\"";
		$BotInfoChat["TheMandateGM", SAY, "outlaws"] 	= "Mandatory, we have a writ for %NAME! He was last seen with a %HINT. Bring him to justice, and bring us his hands!";
		$BotInfoChat["TheMandateGM", "outlaws", 0] = "serve";
		$BotInfoChat["TheMandateGM", "outlaws", 1] = "hello";
		$BotInfoChat["TheMandateGM", SAY, "gaveThiefHands"] = "Yes, these prints match an existing writ. You serve Keldrin well. It is a safer place because of you, Mandatory.";
		$BotInfoChat["TheMandateGM", "gaveThiefHands", 0] = "serve";
		$BotInfoChat["TheMandateGM", "gaveThiefHands", 1] = "hello";
		
		$BotInfoChat["TheMandateGM", "+", "decency"] 		= "SavageHeart 1";
		$BotInfoChat["TheMandateGM", "-", "decency"] 		= "isnecro";
			$BotInfoChat["TheMandateGM", SAY, "isnecro"] 	= "Necromancer! You bring Savage Hearts here?! Be gone with you!";
			$BotInfoChat["TheMandateGM", EVAL, "isnecro"] 	= "rpg::NPCKillAndJailTarget";
		$BotInfoChat["TheMandateGM", SAY, "decency"] = "There is chronic indecency in Keldrin. The Savages, Amazon, and Deluded cannot be allowed their ways.";
		$BotInfoChat["TheMandateGM", "decency", 0] = "amazon";
		$BotInfoChat["TheMandateGM", "decency", 1] = "deluded";
		$BotInfoChat["TheMandateGM", "decency", 2] = "savages";
		
			$BotInfoChat["TheMandateGM", EVAL, "amazon"] = "rpg::HouseFetchQuestAwards ,\"StolenGarments\", \"50\", \"returning\", \"returned\"";
			$BotInfoChat["TheMandateGM", SAY, "noreturned"] = "The Amazon 'lifestyle' is entirely devoid of decency. If you find find any items they've stolen, bring them here.";
			$BotInfoChat["TheMandateGM", "noreturned", 0] = "decency";
			$BotInfoChat["TheMandateGM", SAY, "returned"] = "Hmm, yes, thank-you. We'll hold on to these and speak with the owners, if they ever come looking...";
			$BotInfoChat["TheMandateGM", "returned", 0] = "decency";
			
			$BotInfoChat["TheMandateGM", EVAL, "deluded"] = "rpg::HouseFetchQuestAwards ,\"ControlCrystal\", \"150\", \"obtaining\", \"devices\"";
			$BotInfoChat["TheMandateGM", SAY, "nodevices"] = "Saddled with Mind Control devices, Gat commands these Deluded to practice dark arts. Removal of the device results in death. There is no other way.";
			$BotInfoChat["TheMandateGM", "nodevices", 0] = "decency";
			$BotInfoChat["TheMandateGM", SAY, "devices"] = "Hmm, yes, thank-you. If we discover a way to disable the Mind Control devices and remove them safely, we will pass this information to you.";
			$BotInfoChat["TheMandateGM", "devices", 0] = "decency";
			
			$BotInfoChat["TheMandateGM", EVAL, "savages"] = "rpg::HouseFetchQuestAwards ,\"LoinCloth\", \"50\", \"obtaining\", \"clothes\"";
			$BotInfoChat["TheMandateGM", SAY, "noclothes"] = "The Savages are simply unreasonable. They live in the hills near Oasis. Just... follow the smell.";
			$BotInfoChat["TheMandateGM", "noclothes", 0] = "decency";
			$BotInfoChat["TheMandateGM", SAY, "clothes"] = "Hmm. Yes, I'll... take... those. Please, if you can find some other proof of death on the Savages...?";
			$BotInfoChat["TheMandateGM", "clothes", 0] = "decency";
			
		$BotInfoChat["TheMandateGM", "~!", "upgrade"] = "PaddedShield0 1 PlateShield0 1 KnightShield0 1 BronzeShield0 1 DragonShield0 1 KeldriniteShield0 1 PaddedShield 1 PlateShield 1 KnightShield 1 BronzeShield 1 DragonShield 1 KeldriniteShield 1";
		$BotInfoChat["TheMandateGM", "~=", "upgrade"] = "forgotshield";	
		$BotInfoChat["TheMandateGM", EVAL, "upgrade"] = "rpg::TheMandateShieldUpgrade";
		$BotInfoChat["TheMandateGM", SAY, "upgrade"] = "Let's see what we can do.";
			$BotInfoChat["TheMandateGM", SAY, "maxupgrade"] = "You already have the best shield in the known world, Mandatory.";
			$BotInfoChat["TheMandateGM", SAY, "upgradeshield"] = "Such a well worn shield! Your service is commendable. It's time for an upgrade. Please accept this new shield as a reward.";			
			$BotInfoChat["TheMandateGM", SAY, "failupgrade"] = "You do not have a high enough Rank yet to upgrade your shield.";
			$BotInfoChat["TheMandateGM", SAY, "forgotshield"] = "Where is your shield? Have you lost it? If so, discard your copy of The Lawful Masses and beg us to start from the bottom.";
			$BotInfoChat["TheMandateGM", "maxupgrade", 0] = "serve";
			$BotInfoChat["TheMandateGM", "failupgrade", 0] = "serve";
			$BotInfoChat["TheMandateGM", "forgotshield", 0] = "serve";
			$BotInfoChat["TheMandateGM", "upgradeshield", 0] = "serve";
		
		$BotInfoChat["TheMandateGM", "+", "join"] = "STOLEN 1";
		$BotInfoChat["TheMandateGM", "-", "join"] = "isthief";
		$BotInfoChat["TheMandateGM", SAY, "join"] = "Newcomer! We welcome your interest. Will you shield The Lawful Masses by upholding The Mandate?";
		$BotInfoChat["TheMandateGM", "join", 0] = "shield";
		$BotInfoChat["TheMandateGM", "join", 1] = "yes";
		$BotInfoChat["TheMandateGM", "join", 2] = "no";
		$BotInfoChat["TheMandateGM", "join", 3] = "hello";
			$BotInfoChat["TheMandateGM", SAY, "isthief"] = "Thief! AND a fool! What, you thought we wouldn't recognize you?";
			$BotInfoChat["TheMandateGM", EVAL, "isthief"] = "rpg::NPCKillAndJailTarget";
		
			$BotInfoChat["TheMandateGM", SAY, "shield"] = "Our Mandatories are specifically entitled and trained to use shields. You will receive a shield on day one.";
			$BotInfoChat["TheMandateGM", "shield", 0] = "join";
			$BotInfoChat["TheMandateGM", "shield", 1] = "hello";
		
			$BotInfoChat["TheMandateGM", SAY, "yes"] = "Are you certain? You will be expected to study and observe The Lawful Masses at all times. You must agree to this blindly.";
			$BotInfoChat["TheMandateGM", "yes", 0] = "agree";
			$BotInfoChat["TheMandateGM", "yes", 1] = "blindly";
			$BotInfoChat["TheMandateGM", "yes", 2] = "nonsense";
			$BotInfoChat["TheMandateGM", "yes", 3] = "hello";
			
			$BotInfoChat["TheMandateGM", SAY, "blindly"] = "The Mandate is not for public viewing. Our Mandatories agree to spread its word. Only those who take our oath may read The Lawful Masses.";
			$BotInfoChat["TheMandateGM", "blindly", 0] = "agree";
			$BotInfoChat["TheMandateGM", "blindly", 1] = "nonsense";
			$BotInfoChat["TheMandateGM", "blindly", 2] = "hello";
		
				$BotInfoChat["TheMandateGM", "+", "agree"] = "LVLS 5";
				$BotInfoChat["TheMandateGM", "-", "agree"] = "toolow";
				$BotInfoChat["TheMandateGM", SAY, "agree"] = "As per The Mandate, here is your shield and copy of The Lawful Masses.";
				$BotInfoChat["TheMandateGM", EVAL, "agree"] = "rpg::JoinHouse ,\"Keldrin Mandate\"";
				$BotInfoChat["TheMandateGM", "agree", 0] = "serve";
				$BotInfoChat["TheMandateGM", "agree", 1] = "hello";
		
					$BotInfoChat["TheMandateGM", SAY, "toolow"] = "Apologies Newcomer, but we aren't familiar with your character yet. You may serve at level 5. Not before.";


//================================================================================================================================================================
// College of Geoastrics
//		Perks:		Free travel thru portals | AstralFlask | Fast firing mining tools that double as tree choppers and use Mining to attack.
//		Fetch:		DragonScale, Obsidian, Sulfur, Cobalt, Mercury
//		Hunt:		Rare samples
//================================================================================================================================================================
$BotInfoChat["Geostrologist", SAY, "hello"] = "So much to study! The key to getting the water flowing again MUST be in the land. Are you here to see Pryzm? Maybe, visit the Ancients?";
$BotInfoChat["Geostrologist", "hello", 0] = "pryzm";
$BotInfoChat["Geostrologist", "hello", 1] = "study";
$BotInfoChat["Geostrologist", "hello", 2] = "ancients";
	$BotInfoChat["Geostrologist", SAY, "ancients"] = "There's a Sanctuary to the west. We just finished building a bridge across the canyon. Hard work, but it was worth it.";
	$BotInfoChat["Geostrologist", "ancients", 0] = "hello";
	
	$BotInfoChat["Geostrologist", SAY, "study"] = "Each type of ore resonates differently depending on how it is etched. I only just began learning!";
	$BotInfoChat["Geostrologist", "study", 0] = "hello";
	
	$BotInfoChat["Geostrologist", SAY, "pryzm"] = "She's head Refractor here at the college. She keeps the focus stone resonating and handles our knowledge transfers.";
	$BotInfoChat["Geostrologist", "pryzm", 0] = "hello";

$BotInfoChat["GeostrologistWell", SAY, "hello"] = "So much to study! There's still water running under this well. I went down the mine over there to try to collect a sample for the College of Geoastrics, but there's a HUGE Golem down there!";
$BotInfoChat["GeostrologistWell", "hello", 0] = "college";
$BotInfoChat["GeostrologistWell", "hello", 1] = "water";
$BotInfoChat["GeostrologistWell", "hello", 2] = "golem";
	$BotInfoChat["GeostrologistWell", SAY, "college"] = "Climb the mountain where that cave is and you'll find the College of Geoastrics. We're studying everything we can to stop the end of the world. Pryzm is the head Refractor. Go see her.";
	$BotInfoChat["GeostrologistWell", "college", 0] = "pryzm";
	$BotInfoChat["GeostrologistWell", "college", 0] = "hello";
	
	$BotInfoChat["GeostrologistWell", SAY, "water"] = "I won't argue with you. Water's not supposed to just disappear. The good news is that whatever's left over at this point usually has trace elements in it we can study.";
	$BotInfoChat["GeostrologistWell", "water", 0] = "hello";
	
	$BotInfoChat["GeostrologistWell", SAY, "pryzm"] = "She's head Refractor here at the college. She keeps the focus stone resonating and handles our knowledge transfers.";
	$BotInfoChat["GeostrologistWell", "pryzm", 0] = "hello";
	
	$BotInfoChat["GeostrologistWell", SAY, "golem"] = "These 'stone men' are re-animated by the Ancients to serve as protectors. Most of them are locked away in the Chamber of Animation, but this one here? Seems like it's been around awhile.";
	$BotInfoChat["GeostrologistWell", "golem", 0] = "hello";
	
//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["GeostrologyGM", SAY, "hello"] = "So many rocks, so little time. We need Resonance, yesterday. Do you have knowledge to transfer?";
$BotInfoChat["GeostrologyGM", "hello", 0] = "knowledge";
$BotInfoChat["GeostrologyGM", "hello", 1] = "transfer";
$BotInfoChat["GeostrologyGM", "hello", 2] = "resonance";
	
	$BotInfoChat["GeostrologyGM", "!", "transfer"] = "Iradnium 1";
	$BotInfoChat["GeostrologyGM", "=", "transfer"] = "considering";
	$BotInfoChat["GeostrologyGM", "~+", "transfer"] = "SlayerGear 1 WayLink 1 MarkOfOrder 1 TheLawfulMasses 1";
	$BotInfoChat["GeostrologyGM", "~-", "transfer"] = "badguild";
	
	$BotInfoChat["GeostrologyGM", EVAL, "transfer"] = "rpg::GeostrologyRewardCheck";
		$BotInfoChat["GeostrologyGM", SAY, "upgradetool"] = "You're learning fast. It's time for a reward! Have this new tool to aid you with your research.";
		$BotInfoChat["GeostrologyGM", "upgradetool", 0] = "transfer";
		$BotInfoChat["GeostrologyGM", "upgradetool", 1] = "hello";
	$BotInfoChat["GeostrologyGM", SAY, "transfer"] = "My favourite student! Welcome back. Are you here to transfer some special ore? Maybe some Mineral Water?";	
	$BotInfoChat["GeostrologyGM", "transfer", 0] = "scales";
	$BotInfoChat["GeostrologyGM", "transfer", 1] = "obsidian";
	$BotInfoChat["GeostrologyGM", "transfer", 2] = "sulfur";
	$BotInfoChat["GeostrologyGM", "transfer", 3] = "cobalt";
	$BotInfoChat["GeostrologyGM", "transfer", 4] = "mercury";
	$BotInfoChat["GeostrologyGM", "transfer", 5] = "water";
	$BotInfoChat["GeostrologyGM", "transfer", 6] = "hello";
	
		$BotInfoChat["GeostrologyGM", EVAL, "obsidian"] = "rpg::HouseFetchQuestAwards ,\"Obsidian\", \"150\", \"transferring\"";
		$BotInfoChat["GeostrologyGM", SAY, "obsidian"] = "These are great Obsidian samples! If we ever learn how to inscribe anything on this ore, you'll be the first to know.";
		$BotInfoChat["GeostrologyGM", "obsidian", 0] = "transfer";
			$BotInfoChat["GeostrologyGM", SAY, "noObsidian"] = "Obsidian comes from Golems. They're wildly tough inanimates held together with magic. There's a portal across from Fort Ethren. Be careful...";
			$BotInfoChat["GeostrologyGM", "noObsidian", 0] = "transfer";
			
		$BotInfoChat["GeostrologyGM", EVAL, "scales"] = "rpg::HouseFetchQuestAwards ,\"DragonScale\", \"150\", \"transferring\"";
		$BotInfoChat["GeostrologyGM", SAY, "scales"] = "I can't believe it, you found a Dragon Scales? On Travellers? That's... interesting. We'll study these extensively. Thankyou.";
		$BotInfoChat["GeostrologyGM", "scales", 0] = "transfer";
			$BotInfoChat["GeostrologyGM", SAY, "noDragonScale"] = "Hmm, those definitely aren't Dragon Scales. You should probably get those looked at... by a Cleric?";
			$BotInfoChat["GeostrologyGM", "noDragonScale", 0] = "transfer";
			
		$BotInfoChat["GeostrologyGM", EVAL, "sulfur"] = "rpg::HouseFetchQuestAwards ,\"Sulfur\", \"25\", \"transferring\"";
		$BotInfoChat["GeostrologyGM", SAY, "sulfur"] = "Oh, that smell! I wish we didn't actually need Sulfur to energize anything. Just... awful. But thank you!";
		$BotInfoChat["GeostrologyGM", "sulfur", 0] = "transfer";
			$BotInfoChat["GeostrologyGM", SAY, "noSulfur"] = "If your nostrils feel like they're full of salt, you found Sulfur. Churls eat the stuff. Yeah, really. So, check with them?...";
			$BotInfoChat["GeostrologyGM", "noSulfur", 0] = "transfer";
			
		$BotInfoChat["GeostrologyGM", EVAL, "cobalt"] = "rpg::HouseFetchQuestAwards ,\"Cobalt\", \"50\", \"transferring\"";
		$BotInfoChat["GeostrologyGM", SAY, "cobalt"] = "You're alive, and you have Cobalt? I'm not going to ask what you've been through. Thankyou.";
		$BotInfoChat["GeostrologyGM", "cobalt", 0] = "transfer";
			$BotInfoChat["GeostrologyGM", SAY, "noCobalt"] = "This metal is incredible! The trouble is, the only way we can get it is by shattering Cragspawn in Overville.";
			$BotInfoChat["GeostrologyGM", "noCobalt", 0] = "transfer";
			
		$BotInfoChat["GeostrologyGM", EVAL, "mercury"] = "rpg::HouseFetchQuestAwards ,\"Mercury\", \"50\", \"transferring\"";
		$BotInfoChat["GeostrologyGM", SAY, "mercury"] = "Hey, whoa! Don't get that stuff on me! I appreciate the Mercury, but please, handle it more carefully?";
		$BotInfoChat["GeostrologyGM", "mercury", 0] = "transfer";
			$BotInfoChat["GeostrologyGM", SAY, "noMercury"] = "The Ancients across the bridge use Mercury as a lubricant. It's always rich with other trace elements.";
			$BotInfoChat["GeostrologyGM", "noMercury", 0] = "transfer";
	
		$BotInfoChat["GeostrologyGM", EVAL, "water"] = "rpg::HouseFetchQuestAwards ,\"MineralWater\", \"1000\", \"transferring\"";
		$BotInfoChat["GeostrologyGM", SAY, "water"] = "It's amazing how small things can get! This Mineral Water is probably rich with samples we can study. Thankyou!";
		$BotInfoChat["GeostrologyGM", "water", 0] = "transfer";
			$BotInfoChat["GeostrologyGM", SAY, "noMineralWater"] = "Remember that Astral Flask we gave you? Get near some water and use it to collect samples. If you find Mineral Water, bring the Flask back to me.";
			$BotInfoChat["GeostrologyGM", "noMineralWater", 0] = "transfer";
	
	$BotInfoChat["GeostrologyGM", SAY, "considering"] = "Do you wish to enroll as a student of Geoastrics? The 'why' should be self evident, but there are benefits I can explain.";
	$BotInfoChat["GeostrologyGM", "considering", 0] = "why";
	$BotInfoChat["GeostrologyGM", "considering", 1] = "benefits";
	$BotInfoChat["GeostrologyGM", "considering", 2] = "enroll";
	$BotInfoChat["GeostrologyGM", "considering", 3] = "hello";
	
		$BotInfoChat["GeostrologyGM", SAY, "badguild"] = "Affiliation with the College is exclusive, Newcomer. If you want to practice Geoastrics, leave your fiction and adventure behind.";
		$BotInfoChat["GeostrologyGM", "badguild", 0] = "hello";
	
		$BotInfoChat["GeostrologyGM", SAY, "why"] = "Speak to me about knowledge if you wish to understand the 'why'. Or are you considering committment?";
		$BotInfoChat["GeostrologyGM", "why", 0] = "knowledge";
		$BotInfoChat["GeostrologyGM", "why", 1] = "considering";
	
		$BotInfoChat["GeostrologyGM", SAY, "benefits"] = "Our students use portals for free. They receive expert Mining tools as they advance. And they're helping to stop the end of the world.";
		$BotInfoChat["GeostrologyGM", "benefits", 0] = "enroll";
		$BotInfoChat["GeostrologyGM", "benefits", 1] = "considering";
			
		$BotInfoChat["GeostrologyGM", "~+", "enroll"] = "SlayerGear 1 WayLink 1 MarkOfOrder 1 TheLawfulMasses 1";
		$BotInfoChat["GeostrologyGM", "~-", "enroll"] = "badguild";
		$BotInfoChat["GeostrologyGM", SAY, "enroll"] = "Welcome, student. I have given you Iradnium, which makes all portals in this world safe and free for use. May it speed your learning.";
		$BotInfoChat["GeostrologyGM", EVAL, "enroll"] = "rpg::JoinHouse ,\"College of Geoastrics\"";
		$BotInfoChat["GeostrologyGM", "enroll", 0] = "transfer";
		$BotInfoChat["GeostrologyGM", "enroll", 1] = "hello";
	
	$BotInfoChat["GeostrologyGM", SAY, "knowledge"] = "Every Geostrologist knows that the world doesn't need to 'end'. Our world was once round. Things were quite different.";
	$BotInfoChat["GeostrologyGM", "knowledge", 0] = "know";
	$BotInfoChat["GeostrologyGM", "knowledge", 1] = "end";
	$BotInfoChat["GeostrologyGM", "knowledge", 2] = "round";
	$BotInfoChat["GeostrologyGM", "knowledge", 3] = "hello";
	
		$BotInfoChat["GeostrologyGM", SAY, "round"] = "Water is supposed to flow in circles. If you haven't noticed, the world is flat. Where'd our water go? Drained off the edge.";
		$BotInfoChat["GeostrologyGM", "round", 0] = "uhh";
		$BotInfoChat["GeostrologyGM", "round", 1] = "edge";
		$BotInfoChat["GeostrologyGM", "round", 2] = "flat";
		$BotInfoChat["GeostrologyGM", "round", 3] = "knowledge";
		
			$BotInfoChat["GeostrologyGM", SAY, "flat"] = "It's a fact. Flat as a pancake. Walk any direction and don't stop. Let me know how it works out for you.";
			$BotInfoChat["GeostrologyGM", "flat", 0] = "knowledge";
		
			$BotInfoChat["GeostrologyGM", SAY, "uhh"] = "I know, it sounds crazy. But it isn't. Prove things to yourself if you have to. Share what you learn.";
			$BotInfoChat["GeostrologyGM", "uhh", 0] = "knowledge";
		
			$BotInfoChat["GeostrologyGM", SAY, "edge"] = "Walk off the edge and you'll fly into space. There's no ambient pressure out there.";
			$BotInfoChat["GeostrologyGM", "edge", 0] = "space";
			$BotInfoChat["GeostrologyGM", "edge", 1] = "knowledge";	
	
		$BotInfoChat["GeostrologyGM", SAY, "end"] = "Talk about the 'end' is all nonsense. Keldrin was hit by space rock and was cut away from a larger, rounded land.";
		$BotInfoChat["GeostrologyGM", "end", 0] = "uhh";
		$BotInfoChat["GeostrologyGM", "end", 1] = "larger";
		$BotInfoChat["GeostrologyGM", "end", 2] = "space";
		$BotInfoChat["GeostrologyGM", "end", 3] = "knowledge";
		
			$BotInfoChat["GeostrologyGM", SAY, "larger"] = "Before darkness settled in, you could see other round planets. We study ore in the hopes of achieving resonance between space rocks.";
			$BotInfoChat["GeostrologyGM", "larger", 0] = "uhh";
			$BotInfoChat["GeostrologyGM", "larger", 1] = "space";
			$BotInfoChat["GeostrologyGM", "larger", 2] = "resonance";
			$BotInfoChat["GeostrologyGM", "larger", 3] = "knowledge";
			
			$BotInfoChat["GeostrologyGM", SAY, "resonance"] = "All rock vibrates with energy. We can already control this on a small scale. Our end goal is to resonate with larger space rocks.";
			$BotInfoChat["GeostrologyGM", "resonance", 0] = "uhh";
			$BotInfoChat["GeostrologyGM", "resonance", 1] = "space";
			$BotInfoChat["GeostrologyGM", "resonance", 2] = "larger";
			$BotInfoChat["GeostrologyGM", "resonance", 3] = "knowledge";
			
			$BotInfoChat["GeostrologyGM", SAY, "space"] = "Space, the stuff between rocks, has no weight. Since the universe has nothing to press you against, you float freely in space.";
			$BotInfoChat["GeostrologyGM", "space", 0] = "uhh";
			$BotInfoChat["GeostrologyGM", "space", 1] = "float";
			$BotInfoChat["GeostrologyGM", "space", 2] = "knowledge";
			
				$BotInfoChat["GeostrologyGM", SAY, "float"] = "Don't experiment with the edge of the world. You won't get far. This creep named Zatan prevents anything from leaving.";
				$BotInfoChat["GeostrologyGM", "float", 0] = "uhh";
				$BotInfoChat["GeostrologyGM", "float", 1] = "zatan";
				$BotInfoChat["GeostrologyGM", "float", 2] = "edge";
				$BotInfoChat["GeostrologyGM", "float", 3] = "knowledge";
				
					$BotInfoChat["GeostrologyGM", SAY, "zatan"] = "He's got some kind of magical device that sustains a field around Keldrin. Escapees get sucked into his lair. Nasty bugger.";
					$BotInfoChat["GeostrologyGM", "zatan", 0] = "uhh";
					$BotInfoChat["GeostrologyGM", "zatan", 1] = "knowledge";
			
		$BotInfoChat["GeostrologyGM", SAY, "know"] = "We are researching a way to make it rain first and foremost. Filling the rivers will buy time to study resonance at scale.";
		$BotInfoChat["GeostrologyGM", "know", 0] = "uhh";
		$BotInfoChat["GeostrologyGM", "know", 1] = "rain";
		$BotInfoChat["GeostrologyGM", "know", 2] = "resonance";
		$BotInfoChat["GeostrologyGM", "know", 3] = "knowledge";			
			
			$BotInfoChat["GeostrologyGM", SAY, "rain"] = "We can make clouds, but not water. Are you considering becoming a student? We need to collect more water samples from ancient sources.";
			$BotInfoChat["GeostrologyGM", "rain", 0] = "uhh";
			$BotInfoChat["GeostrologyGM", "rain", 1] = "considering";
			$BotInfoChat["GeostrologyGM", "rain", 2] = "knowledge";


//================================================================================================================================================================
// Wildenslayer
//		Perks:		Wildenslayer Gear
//		Fetch:		Greenskins, Minotaur horns, Churl eyes
//		Hunt:		random Orcs 
//================================================================================================================================================================
$BotInfoChat["SlayerGloater", SAY, "hello"] = "Only the strong survive! Druids are weak! We slay! This is OUR home!";
$BotInfoChat["SlayerGloater", "hello", 0] = "druids";
$BotInfoChat["SlayerGloater", "hello", 1] = "slay";
	$BotInfoChat["SlayerGloater", SAY, "druids"] = "They were useless! Weak and needy! The water here makes us STRONG! The beasts are plenty!";
	$BotInfoChat["SlayerGloater", "druids", 0] = "were";
	$BotInfoChat["SlayerGloater", "druids", 1] = "water";
	$BotInfoChat["SlayerGloater", "druids", 2] = "beasts";
	
		$BotInfoChat["SlayerGloater", SAY, "were"] = "Ran away to another pond, they did! Hah! *sips water* NNRUGH SLAY! Uhruhuhuhu.... *cough*";
		$BotInfoChat["SlayerGloater", "were", 0] = "water";
		$BotInfoChat["SlayerGloater", "were", 1] = "slay";
	
		$BotInfoChat["SlayerGloater", SAY, "beasts"] = "Greenskins! Minotaurs! Endless blood! We SLAY them ALL!";
		$BotInfoChat["SlayerGloater", "beasts", 0] = "water";
		$BotInfoChat["SlayerGloater", "beasts", 1] = "slay";
		
		$BotInfoChat["SlayerGloater", SAY, "water"] = "What doesn't kill you MAKES YOU STRONGER! AAHAHAHAHAH! Do you want a drink?";
		$BotInfoChat["SlayerGloater", "water", 0] = "drink";
		$BotInfoChat["SlayerGloater", "water", 1] = "uhh";
			
			$BotInfoChat["SlayerGloater", SAY, "drink"] = "What?! MY water?! Touch it and I will SLAY you!";
			$BotInfoChat["SlayerGloater", "drink", 0] = "slay";
			
			$BotInfoChat["SlayerGloater", SAY, "uhh"] = "*sips water* NNRUGH SLAY! Uhruhuhuhu.... *cough*";
			$BotInfoChat["SlayerGloater", "uhh", 0] = "hello";
		
	$BotInfoChat["SlayerGloater", SAY, "slay"] = "Slay beasts! Become invincible, like us! Tell Stinger you will SLAY! ";
	$BotInfoChat["SlayerGloater", "slay", 0] = "uhh";
	$BotInfoChat["SlayerGloater", "slay", 0] = "stinger";
	
		$BotInfoChat["SlayerGloater", SAY, "stinger"] = "Find him in our fortress! He will ask you to SLAY! You will say YES!";
		$BotInfoChat["SlayerGloater", "stinger", 0] = "uhh";

//________________________________________________________________________________________________________________________________________________________________________
$BotInfoChat["WildenslayerGM", "~!", "hello"] = "SlayerGear 1 SlayerGear0 1";
$BotInfoChat["WildenslayerGM", "~=", "hello"] = "speak";
$BotInfoChat["WildenslayerGM", "~+", "hello"] = "Iradnium 1 WayLink 1 MarkOfOrder 1 TheLawfulMasses 1";
$BotInfoChat["WildenslayerGM", "~-", "hello"] = "badguild";
$BotInfoChat["WildenslayerGM", SAY, "hello"] = "Slayer! Do you bring trophies? Or questions about your Slayer Gear?";
$BotInfoChat["WildenslayerGM", "hello", 0] = "trophies";
$BotInfoChat["WildenslayerGM", "hello", 1] = "gear";

	$BotInfoChat["WildenslayerGM", SAY, "gear"] = "YES, Slayer Gear! Run faster! Jump higher! Hurt less! Slay, and your Gear will improve with you!";
	$BotInfoChat["WildenslayerGM", "gear", 0] = "run";
	$BotInfoChat["WildenslayerGM", "gear", 1] = "jump";
	$BotInfoChat["WildenslayerGM", "gear", 2] = "hurt";
	$BotInfoChat["WildenslayerGM", "gear", 3] = "improve";
	$BotInfoChat["WildenslayerGM", "gear", 4] = "hello";
	
		$BotInfoChat["WildenslayerGM", SAY, "run"] = "Your Slayer Gear will keep you moving at full speed! You will never slow down unless you are too heavy to move!";
		$BotInfoChat["WildenslayerGM", "run", 0] = "jump";
		$BotInfoChat["WildenslayerGM", "run", 1] = "hurt";
		$BotInfoChat["WildenslayerGM", "run", 2] = "improve";
		$BotInfoChat["WildenslayerGM", "run", 3] = "hello";
		
		$BotInfoChat["WildenslayerGM", SAY, "jump"] = "Your Slayer Gear has explosive jets and springs! You always jump higher! Use energy to boost!";
		$BotInfoChat["WildenslayerGM", "jump", 0] = "run";
		$BotInfoChat["WildenslayerGM", "jump", 1] = "hurt";
		$BotInfoChat["WildenslayerGM", "jump", 2] = "improve";	
		$BotInfoChat["WildenslayerGM", "jump", 3] = "hello";
		
		$BotInfoChat["WildenslayerGM", SAY, "hurt"] = "Your Armor rating increase as you slay! Slay your way to Rank 100, then ask me more!";
		$BotInfoChat["WildenslayerGM", "hurt", 0] = "run";
		$BotInfoChat["WildenslayerGM", "hurt", 1] = "jump";
		$BotInfoChat["WildenslayerGM", "hurt", 2] = "improve";	
		$BotInfoChat["WildenslayerGM", "hurt", 3] = "hello";
		
		$BotInfoChat["WildenslayerGM", SAY, "improve"] = "Your weight capacity increases as you slay! Give us trophies and we will improve your Slayer Gear!";
		$BotInfoChat["WildenslayerGM", "improve", 0] = "run";
		$BotInfoChat["WildenslayerGM", "improve", 1] = "jump";
		$BotInfoChat["WildenslayerGM", "improve", 2] = "hurt";
		$BotInfoChat["WildenslayerGM", "improve", 3] = "hello";

	$BotInfoChat["WildenslayerGM", SAY, "badguild"] = "*Sniff*... smells of... others.";

	$BotInfoChat["WildenslayerGM", SAY, "speak"] = "Slay or be slain! Beasts roam these lands. Join us! Slay beasts! Survive, and we will train you.";
	$BotInfoChat["WildenslayerGM", "speak", 0] = "slay";
	$BotInfoChat["WildenslayerGM", "speak", 1] = "train";
	$BotInfoChat["WildenslayerGM", "speak", 2] = "join";		
		
		$BotInfoChat["WildenslayerGM", SAY, "train"] = "You will bear any weight. You will climb new heights. You will conquer land and water.";
		$BotInfoChat["WildenslayerGM", "train", 0] = "slay";
		$BotInfoChat["WildenslayerGM", "train", 1] = "join";

		$BotInfoChat["WildenslayerGM", SAY, "slay"] = "You will tear horns from Minotaurs. You will rip hides from Greenskins. You will train. Join! Slay!";
		$BotInfoChat["WildenslayerGM", "slay", 0] = "join";
		$BotInfoChat["WildenslayerGM", "slay", 1] = "train";
		
		$BotInfoChat["WildenslayerGM", "~+", "join"] = "Iradnium 1 WayLink 1 MarkOfOrder 1 TheLawfulMasses 1";
		$BotInfoChat["WildenslayerGM", "~-", "join"] = "badguild";
		$BotInfoChat["WildenslayerGM", SAY, "join"] = "Slayer! You will wear this gear, now!";
		$BotInfoChat["WildenslayerGM", EVAL, "join"] = "rpg::JoinHouse ,\"Wildenslayers\"";

	
	$BotInfoChat["WildenslayerGM", EVAL, "trophies"] = "rpg::WildenslayerPrepareForReward";
	$BotInfoChat["WildenslayerGM", SAY, "finaltask"] = "Your final reward awaits!!! Slay yourself. Once by land. Once by water. You will know when you have succeeded.";
	$BotInfoChat["WildenslayerGM", SAY, "jumproof"] = "WHAT ARE YOU WAITING FOR?! SLAY YOURSELF! Fall until you die!";
	$BotInfoChat["WildenslayerGM", SAY, "watergurp"] = "WHAT ARE YOU WAITING FOR?! SLAY yourself!! Find water! Drown!";
	function rpg::WildenslayerPrepareForReward(%clientId) {
		if(rpg::GetHouseLevel(%clientId) >= 100) {
			if(fetchData(%clientId, "SlayerSuicideWater") > 0) return "watergurp";
			if(fetchData(%clientId, "SlayerSuicideLand") > 0) return "jumproof";
			storeData(%clientId, "SlayerSuicideWater", 10);
			storeData(%clientId, "SlayerSuicideLand", 10);
			return "finaltask";
		} return "";
	}
	
	$BotInfoChat["WildenslayerGM", SAY, "trophies"] = "Where is your proof, Slayer?! Minotaur Horns! Black Statues! Greenskins! Churl Eyes! Axes!";
	$BotInfoChat["WildenslayerGM", "trophies", 0] = "greenskins";
	$BotInfoChat["WildenslayerGM", "trophies", 1] = "statues";
	$BotInfoChat["WildenslayerGM", "trophies", 2] = "horns";
	$BotInfoChat["WildenslayerGM", "trophies", 3] = "eyes";
	$BotInfoChat["WildenslayerGM", "trophies", 4] = "axes";
		
		$BotInfoChat["WildenslayerGM", EVAL, "axes"] 	= "rpg::HouseHuntQuest ,\"Wildenslayers\",\"Roamer Scout Grunt Warmaiden\", 2, 20, \"OrcishAxe\", 1000, \"presenting\"";
		$BotInfoChat["WildenslayerGM", SAY, "axes"] 	= "Slayer! The Orc %NAME carries an Orcish Axe! IT IS FRIENDS WITH %HINTs! HA HA HA! Find this Orc and SLAY it for the Axe!";
		$BotInfoChat["WildenslayerGM", "axes", 0] = "trophies";
		$BotInfoChat["WildenslayerGM", SAY, "gaveOrcishAxe"] = "AN ORCISH AXE! YES! SLAYER! BRING MORE!!";
		$BotInfoChat["WildenslayerGM", "gaveOrcishAxe", 0] 	= "trophies";

		$BotInfoChat["WildenslayerGM", EVAL, "eyes"] = "rpg::HouseFetchQuestAwards ,\"ChurlEye\", \"50\", \"turning in\"";
		$BotInfoChat["WildenslayerGM", SAY, "eyes"] = "Slayer! These Churl Eyes are proof. Do you have MORE trophies?";
		$BotInfoChat["WildenslayerGM", "eyes", 0] = "trophies";
			$BotInfoChat["WildenslayerGM", SAY, "noChurlEye"] = "You lie! Slay Churls! Bring their Eye as proof!";
			$BotInfoChat["WildenslayerGM", "noChurlEye", 0] = "trophies";

		$BotInfoChat["WildenslayerGM", EVAL, "greenskins"] = "rpg::HouseFetchQuestAwards ,\"GreenSkin\", \"35\", \"turning in\"";
		$BotInfoChat["WildenslayerGM", SAY, "greenskins"] = "Greenskin! Yes! Slayer, we accept this skin as proof. Do you have MORE trophies?";
		$BotInfoChat["WildenslayerGM", "greenskins", 0] = "trophies";
			$BotInfoChat["WildenslayerGM", SAY, "noGreenSkin"] = "You lie! Kill Goblins. Kill Orcs. Kill Ogres. Return their skin as proof!";
			$BotInfoChat["WildenslayerGM", "noGreenSkin", 0] = "trophies";

		$BotInfoChat["WildenslayerGM", EVAL, "horns"] = "rpg::HouseFetchQuestAwards ,\"MinotaurHorn\", \"75\", \"turning in\"";
		$BotInfoChat["WildenslayerGM", SAY, "horns"] = "Horns! Yes! Slayer, we accept this proof. Do you have MORE trophies?";
		$BotInfoChat["WildenslayerGM", "horns", 0] = "trophies";
			$BotInfoChat["WildenslayerGM", SAY, "noMinotaurHorn"] = "You lie! Minotaur males grow horns! Females cut off their own and carry them! SLAY both! Bring back Horns!";
			$BotInfoChat["WildenslayerGM", "noMinotaurHorn", 0] = "trophies";
			
		$BotInfoChat["WildenslayerGM", EVAL, "statues"] = "rpg::HouseFetchQuestAwards ,\"BlackStatue\", \"50\", \"turning in\"";
		$BotInfoChat["WildenslayerGM", SAY, "statues"] = "These Black Statues serve as proof! Do you have MORE trophies?";
		$BotInfoChat["WildenslayerGM", "statues", 0] = "trophies";
			$BotInfoChat["WildenslayerGM", SAY, "noBlackStatue"] = "You lie! Roaming Minotaurs and Orcs always carry Black Statues! You would have some as proof!";
			$BotInfoChat["WildenslayerGM", "noBlackStatue", 0] = "trophies";
	
		
//________________________________________________________________________________________________________________________________________________________________________
function rpg::WeatherDevicesAreFull() { if($EndOfTheWorld == 6) return "allfull"; else return ""; }
function MakeWeatherDeviceDialog() {
	$BotInfoChat["WeatherDevice0", SAY, "needore"] = "[ There's Magic Dust all over this device and sharp slivers of Ruby on the ground. ] ";
	$BotInfoChat["WeatherDevice1", SAY, "needore"] = "[ This strange device has markings on it that look like... a potion? ] ";
	$BotInfoChat["WeatherDevice2", SAY, "needore"] = "[ As you look for glyphs on the device, you notice etchings depicting Orcs being slain. ] ";
	$BotInfoChat["WeatherDevice3", SAY, "needore"] = "[ The ground around the device is covered in fine shards of... glass? This must be shattered crystal of some kind. ] ";
	$BotInfoChat["WeatherDevice4", SAY, "needore"] = "[ This device appears to have a slot on the top shaped exactly like an Enchanted Stone. ] ";
	$BotInfoChat["WeatherDevice5", SAY, "needore"] = "[ The symbols on this device are immediately obvious - Mana Rocks. ]";
			
	for(%x=0;%x<$Array::Size[$WeatherDevices];%x++) {
		%device = "WeatherDevice" @ %x;
		%enabler = "WeatherEnabler" @ %x;
		$BotInfoChat[%device, "+", 	"hello"] = "Equalizer 1";
		$BotInfoChat[%device, "-", 	"hello"] = "canequalize";
		$BotInfoChat[%device, EVAL, "hello"] = "rpg::WeatherDeviceGetState ," @ %x; // outcome: canequalize | energized | needore | (can't use the device)
		
		$BotInfoChat[%device, SAY, "canequalize"]	= "[ The Equalizer vibrates violently as you approach the weather device. ]";
		$BotInfoChat[%device, "canequalize", 0] 	= "equalize";		
		$BotInfoChat[%device, "canequalize", 1] 	= "leave";
		
		$BotInfoChat[%device, EVAL, "equalize"] 	= "rpg::WeatherDeviceDestroy ," @ %x;
		
		$BotInfoChat[%device, SAY, "energized"] 	= "[ The weather device is making a quiet buzzing noise. It is energized. ]";
		
		$BotInfoChat[%device, "needore", 0]			= "deposit";		
		for(%z=1;(%item=getword(Array::Get($WeatherDevices,%x), %z)) != -1; %z++) {
			$BotInfoChat[%device, SAY, "deposit"] 	= "[ You view the glyphs on the side... ]";
			$BotInfoChat[%device, "deposit", %z-1] 	= %item;
			$BotInfoChat[%device, EVAL, %item] 		= "rpg::WeatherDeviceCrush ," @ %x @ "," @ %item;
			$BotInfoChat[%device, %item, 0] 		= "deposit";
		}
		%lastItem = %z-1;
		for(%z=0;(%item=getword(Array::Get($WeatherDeviceJammers,%x), %z)) != -1; %z++) {
			$BotInfoChat[%device, "deposit", %lastItem + %z] 	= %item;
			$BotInfoChat[%device, EVAL, %item] 					= "rpg::WeatherDeviceCrush ," @ %x @ "," @ %item;
			$BotInfoChat[%device, %item, 0] 					= "deposit";
		}
		
		$BotInfoChat[%enabler, EVAL, "hello"] = "rpg::WeatherDeviceGetState ," @ %x; // outcome: canequalize | energized | needore | (can't use the device)
		$BotInfoChat[%enabler, SAY, "hello"] = "[ It looks like this windmill is somehow connected to the device at the end of the long platform... ]";
		$BotInfoChat[%enabler, "hello", 0] = "inspect";
		$BotInfoChat[%enabler, "hello", 1] = "leave";
		
		$BotInfoChat[%enabler, SAY, "canequalize"]	= "[ The Equalizer turns white-hot as you approach the Windmill. There's no way you can hold it. It must be reacting with the Radium. ]";
		$BotInfoChat[%enabler, SAY, "energized"] 	= "[ The Weather Device connected to this Windmill is fully energized. There's nothing more to do here. ]";
		$BotInfoChat[%enabler, SAY, "needore"] 		= "[ The Windmill appears to be glowing with Radium. The Weather Device on the platform across the way hums quietly. ]";
		
		
		$BotInfoChat[%enabler, "+", "inspect"] = "Radium 10";
		$BotInfoChat[%enabler, "-", "inspect"] = "cansupply";
		$BotInfoChat[%enabler, SAY, "inspect"] = "[ You have no idea how this might work. There are small green rocks lodged in the cracks of stone. Are they... glowing? ]";
		$BotInfoChat[%enabler, "inspect", 0] = "hello";		
		
		$BotInfoChat[%enabler, SAY, "cansupply"] = "[ The Radium in your Belt begins to vibrate wildly. It looks like there's a place to put Radium here. Do you want to supply the Windmill? ]";
		$BotInfoChat[%enabler, "cansupply", 0] = "supply";
		$BotInfoChat[%enabler, "cansupply", 1] = "hello";		
		
		$BotInfoChat[%enabler, EVAL, "supply"] = "rpg::WeatherDeviceEnable ," @ %x @ ", \"Radium 10\"";
		$BotInfoChat[%enabler, SAY, "supply"] = "[ As you reach for the Radium, it flies out of your Belt, shatters, and flies through cracks in the Windmill. The mill doesn't move, but the Weather Device on the bridge makes a sound... ]";
	}
}

MakeWeatherDeviceDialog();


