//________________________________________________________________________________________________________________________________
// DescX Notes:
//		An attempt to generically manage mapobjects by creating or deleting them in batches.
//		Idea from phantom's TribesRPG.org Main server.
//		Code depends on Array from World Ender package.
//
//		Array[ "ZoneObj Keldrin Town" ] = 		Array [ [0] = OBJ 
//														[1] = TYPE 
//														[2] = FILE
//														[3] = POS
//														[4] = ROT
//														[5] = NAME
//														[6] = LIGHTS ]	
$ManagedObjectListPrefix = "ZoneObj ";

//________________________________________________________________________________________________________________________________
// Make sure we have the managed cleanup groups created
function rpg::EnsureManagedCleanupExists(%zone) {
	if(!(nametoid("MissionCleanup\\Managed") > 0))				addToSet("MissionCleanup",newObject("Managed", SimGroup));
	if(!(nametoid("MissionCleanup\\Managed\\" @ %zone) > 0)) 	addToSet("MissionCleanup\\Managed",newObject(%zone, SimGroup));
}

//________________________________________________________________________________________________________________________________
//	[re]Create a list of objects.
//	Track the unique %Zone name in $ListOfZoneNames
function rpg::CreateManagedObjectList(%zone) {
	$Array::NullValue[$ListOfZoneNames] = %zone;
	Array::Shrink($ListOfZoneNames);
	$Array::NullValue[$ListOfZoneNames] = "";
	Array::Push($ListOfZoneNames, %zone);
	
	%whichArray = $ManagedObjectListPrefix @ %zone;
	Array::ForEach(%whichArray, "rpg::DeleteManagedObjectList");
	Array::New("",%whichArray);
	return %zone;
}
function rpg::DeleteManagedObjectList(%array, %index, %val) { Array::Delete(%val); }

//________________________________________________________________________________________________________________________________
// Define a mapobject to be managed
function rpg::DefineManagedObject(%zone, %type, %name, %fileName, %pos, %rot, %lightParams) {
	%newObject = Array::New();	// last created ID default -1 ... -> the rest
	Array::Fill(%newObject, "-1" @ "|" @ %type @ "|" @ %fileName @ "|" @ %pos @ "|" @ %rot @ "|" @ %name @ "|" @ %lightParams, "|");
	Array::Push($ManagedObjectListPrefix @ %zone, %newObject);
}

//________________________________________________________________________________________________________________________________
// Create or destroy mapobjects based on number of players detected
function rpg::UpdateManagedZoneObjects(%zone, %numPlayers) {
	if($ForceManagedObjectCreation[%zone] || (!$BlockManagedObjectCreation[%zone] && %numPlayers > 0)) {	
		if(!$ManagedObjectsConstructed[$ManagedObjectListPrefix @ %zone]) {
			if($DebugManagedObjectUpdate) echo("Creating: " @ %zone);
			rpg::EnsureManagedCleanupExists(%zone);
			Array::ForEach($ManagedObjectListPrefix @ %zone, "rpg::CreateManagedObjects", 	"\"" @ %zone @ "\"");
			%worldStateLevel 	= Cap($EndOfTheWorld,0,7);
			//setTerrainVisibility(0, $EndWorldState[%worldStateLevel,"Vis"], $EndWorldState[%worldStateLevel,"Haze"]);
			//setTerrainVisibility(8, $EndWorldState[%worldStateLevel,"Vis"], $EndWorldState[%worldStateLevel,"Haze"]);
			
			InitTownBots(true);
			InitStaticShapes();
		}
		$ManagedObjectsConstructed[$ManagedObjectListPrefix @ %zone] = true;
	}
	else {
		if($ManagedObjectsConstructed[$ManagedObjectListPrefix @ %zone]) {
			if(!$BlockManagedObjectCreation[%zone] || %numPlayers <= 0) {
				if($DebugManagedObjectUpdate) echo("Deleting: " @ %zone);
				Array::ForEach($ManagedObjectListPrefix @ %zone, "rpg::DestroyManagedObjects", "\"" @ %zone @ "\"");
			}
		}
		$ManagedObjectsConstructed[$ManagedObjectListPrefix @ %zone] = false;
	}
}

//________________________________________________________________________________________________________________________________
//
function rpg::UpdateVisibleManagedZones() {
	for(%z = 1; %z <= $numZones; %z++) {
		if($Zone::Type[%z] != "WATER"){
			%visLevel 	= $EndWorldState[Cap($EndOfTheWorld,0,7), "Vis"] + 300;		// Cap minimum size to the current visible distance
			%set 		= newObject("set", SimSet);
			%n 			= containerBoxFillSet(%set, $SimPlayerObjectType, $Zone::Marker[%z],	
									%visLevel + $Zone::Length[%z], 
									%visLevel + $Zone::Width[%z], 
									%visLevel + $Zone::Height[%z], 
									%visLevel + $Zone::SHeight[%z]);
			rpg::UpdateManagedZoneObjects($Zone::Desc[%z], %n);
			deleteObject(%set);
		}
	}
}

//________________________________________________________________________________________________________________________________
// Loop defined objects and create them
function rpg::CreateManagedObjects(%array, %index, %val, %zone) {
	%object = Array::Get(%val, 0);
	if(%object > 0) {
		return;
	}
	%type 			= Array::Get(%val, 1);
	%fileName 		= Array::Get(%val, 2);
	%pos 			= Array::Get(%val, 3);
	%rot 			= Array::Get(%val, 4);
	%name 			= Array::Get(%val, 5);
	%lightParams 	= Array::Get(%val, 6);	
	
	%object = Array::Get(%val, 0);
	if(%type=="InteriorShape")
		%newObject = newObject(%name @ " " @ %index, %type, %fileName);	
	else
		%newObject = newObject(%name @ " " @ %index, %type, %fileName, true);	
	addToSet("MissionCleanup\\Managed\\" @ %zone, %newObject);
	GameBase::setRotation(%newObject, %rot);
	GameBase::setPosition(%newObject, %pos);
	Array::Insert(%val, %newObject, 0);
}

//________________________________________________________________________________________________________________________________
// Destroy collection of mapobjects
function rpg::DestroyManagedObjects(%array, %index, %val) {
	%object = Array::Get(%val, 0);
	if(%object > 0) {
		GameBase::setPosition(%object, "0 0 0");
		deleteObject(%object);
		Array::Insert(%val, -1, 0);
	}
}



//________________________________________________________________________________________________________________________________
//________________________________________________________________________________________________________________________________
//________________________________________________________________________________________________________________________________
function rpg::DefineManagedZoneObjects() {
	$ListOfZoneNames = Array::New("","ListOfZoneNames");


	%zone = "Keldrin Town";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "house", 					"npchut.0.dis", 	"-2525.75 -80.9823 107.125", "0 0 0.157012", "1 1.000000 1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wharf", 					"wharf.0.dis", 		"-2173.5 -368.351 39.486", "0 -0 -0.779831");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wharf", 					"wharf.1.dis", 		"-2267.5 -487.22 39.4645", "0 -0 -1.57963");
		rpg::DefineManagedObject(%zone, "InteriorShape", "barracks",				"barrack.0.dis", 	"-2735 -239.375 218.375", "0 0 -1.06398");
		rpg::DefineManagedObject(%zone, "InteriorShape", "house", 					"merchant.0.dis", 	"-2707.5 196 389.637", "0 -0 -3.09837");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bridge", 					"bbridge.dis", 		"-2600 -65 135", "0 0 2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "teleporter", 				"remorterhall.dis", "-2601.5 -50 156", "0 0 3.57");
		rpg::DefineManagedObject(%zone, "InteriorShape", "anvil1 anvil", 			"anvil.0.dis", 		"-2402.11 -286.746 65.0943", "0 -0 -2.07947");
		rpg::DefineManagedObject(%zone, "InteriorShape", "sign", 					"bssign.0.dis", 	"-2399.5 -274.303 64.9956", "0 -0 -0.799941");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bed", 					"bed.0.dis", 		"-2432.08 -241.679 77.5942", "0 -0 -2.41985");
		rpg::DefineManagedObject(%zone, "StaticShape",   "fountain", 				"fountain", 		"-2407 -271.25 65", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape",   "fountainwater",			"fountainwater", 	"-2407 -271.25 65", "0 0 0");
	
	
	%zone = "College of Geoastrics";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "geo1", "windmill.dis", "-1968.25 923.625 671.810999999999", "0 -0 -1.55999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "geo2", "windmill.dis", "-1806.39 864.435 661.143", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "geo2q1", "looklook.dis", "-1861.63999999999 916.859999999999 667.105999999999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "geo2q2", "bed3.dis", "-1864.25 911.294 665.194", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "geo2q3", "morosabld4.dis", "-1829.78999999999 921.235999999999 660.853999999999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "geo2q5", "rmrwindmill.dis", "-1915.31 902.552 662.676", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "millland", "kobastand.dis", "-1935.5 907.912999999999 679.960999999999", "0.119999 -0.119999 1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tpblockhelp", "milburnewalls1final.dis", "-1930.42999999999 902.510999999999 680.326999999999", "0 0 0");


	%zone = "Damned Crypt";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock61 rockmine", 		"lrock6.0.dis", "-800.375 864.25 700.875", "-0.944907 1.01115 -1.53264");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock21 rockmine", 		"lrock2.0.dis", "-814.25 863.5 700.875", "-0.34907 1.17265 -0.723544");
		rpg::DefineManagedObject(%zone, "StaticShape", "PlantTwo1 tree", 			"PlantTwo", "-792.875 866.5 700.875", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "PlantTwo1 tree", 			"PlantTwo", "-795.5 855.125 700.875", "0 -0 -1.71875");
		rpg::DefineManagedObject(%zone, "StaticShape", "PlantTwo1 tree", 			"PlantTwo", "-811.875 867.125 700.875", "0 -0 1.43824");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock41 rockmine", 		"lrock4.0.dis", "-778.25 859.75 700.875", "-0.125731 -2.00203 3.08203");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock41 rockmine", 		"lrock4.1.dis", "-810.25 848.5 700.875", "-0.949299 -0.998708 0.463641");
		rpg::DefineManagedObject(%zone, "StaticShape", "PlantTwo1 tree", 			"PlantTwo", "-807.25 876.375 700.875", "0 0 0");


	%zone = "Fort Ethren";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeOne1 tree", "TreeShape", "-2359.75 -2458 63.847", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo1 tree", "TreeShapeTwo", "-2394.5 -2394 64.8", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wharf1", "wharf.2.dis", "-2194.75 -2445.5 39.5", "0 -0 -0.0398523");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock21 rockmine", "lrock2.1.dis", "-2395 -2377.5 65", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock61 rockmine", "lrock6.1.dis", "-2182 -2400 50.545", "0 -0.899834 -0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock51 rockmine", "lrock5.0.dis", "-2417.75 -2282.25 65", "0 -0 2.53871");
		rpg::DefineManagedObject(%zone, "InteriorShape", "portal2Jaten", "remorterhall.dis", "-2440.12 -2245 73", "0 -0 1.5");


	%zone = "Jaten Outpost";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo113", "PhantomStrangerTree2", "-303.375 1684.25 57.827", "0 -0 1.57753");
		rpg::DefineManagedObject(%zone, "InteriorShape", "beachwharf1", "beachwharf.0.dis", "-109.5 1635.25 46.252", "0 -0 0.819842");
		rpg::DefineManagedObject(%zone, "InteriorShape", "sign1", "sign.1.dis", "-123 1634.75 47.75", "0 -0 2.40174");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bankercab2", "cabinet2.dis", "-306.714999999999 1745.81999999999 65.7986999999999", "0 -0 2.44999");		
		rpg::DefineManagedObject(%zone, "InteriorShape", "jrock1 rockmine", "jrock.0.dis", "-392.015 1812.07 65", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wharf1", "wharf.3.dis", "-155 1661.25 45.1875", "0 -0 -0.719834");
		rpg::DefineManagedObject(%zone, "InteriorShape", "arena11", "arena1.0.dis", "-198.519 1660 66.4207", "0 -0 2.40203");		
		rpg::DefineManagedObject(%zone, "InteriorShape", "jornbed", "bed2.dis", "-326.375 1801.52999999999 65.2990999999999", "0 -0 -0.739999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "portal2Hazard", "remorterhall.dis", "-171.888999999999 1748.5 58.1714999999999", "0 0 4.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "anvil2 anvil", "anvil.0.dis", "-342.738 1735.09 65.301", "0 -0 -2.07947");
		rpg::DefineManagedObject(%zone, "InteriorShape", "entrancetrapdoor", "cdoorb.dis", "-412.95499 1773 65", "1.56941 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "doorcover1 rockmine", "lrock2.0.dis", "-409.746 1766.56 65", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "doorcover2 rockmine", "lrock1.0.dis", "-412.541 1766.64 65", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "doorcover3", "pwood_cube_m.dis", "-408.729 1768.98 65", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "doorcover4", "pwood_beam_s.dis", "-411.413 1773.06 65.0507", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "doorcover6", "pwood_cube_m.dis", "-408.923 1772.94 65", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "doorcover7", "pwood_cube_m.dis", "-408.716 1770.83 65", "0 0 0");

	%zone = "Orc Base";
	%zone = rpg::CreateManagedObjectList(%zone);		
		rpg::DefineManagedObject(%zone, "StaticShape", "treemark tree", "PhantomStrangerTree3", "705 -440 125", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ctower3", "ctower3.dis", "479.22 -263.74 326.014", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ctower2", "ctower2.dis", "496.986 -262.037 327.225", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "telebox2", "telebox2.dis", "485.8439999 -287.880999 336.04599999", "0 -0 0.3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tree tree", "rmrforestspike.dis", "521.962 -323.178 330.379", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tree tree", "rmrforestspike.dis", "520.637 -310.186 330.154", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tree tree", "rmrforestspike.dis", "486.868 -318.91 329.597", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tree tree", "rmrforestspike.dis", "494.969 -300.846 327.785", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tree tree", "rmrforestspike.dis", "488.558 -311.645 329.002", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tree tree", "rmrforestspike.dis", "507.866 -309.338 328.356", "0 0 0");

	%zone = "Black Market";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "dungeonarea", "thievesden.dis", "345.536 1025.87 235.437", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "dungeonarea", "pwood_base_m.dis", "350.440999999999 1034.25 267.012999999999", "0 0 0");

	%zone = "Traveller's Canyon";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "pathmercator", "ffort.dis", "-385 -1325 65", "0 0 0.2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "pathwellspring", "ffort.dis", "-255 -1110 70", "0 0 0.2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "pathhazard", "ffort.dis", "-55 -1555 40", "0 0 0.3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t1", "ctower2.dis", "-202.786 -1287.91 10.4133", "0 0 2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t2", "ctower3.dis", "-253.937 -1279.72 8.93648", "0 0 1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t3", "ctower4.dis", "-229.86 -1233.44 11.273", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t4", "ctower2.dis", "-123.083 -1373.73 18.2068", "0 0 3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t5", "ctower3.dis", "-170.63 -1347.49 16.8547", "0 0 0.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t6", "ctower4.dis", "-194.725 -1326.25 13.5252", "0 0 -2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t7", "ctower4.dis", "-311.01 -1311.53 9.06203", "0 0 1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tower7", "ctower4.dis", "-467.343 -1325 26.9419", "0 0 -3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tower6", "ctower3.dis", "-539.086 -1336.65 22.8004", "0 0 1.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tower5", "ctower3.dis", "-862.208 -1483.96 3.50056", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tower4", "ctower2.dis", "-735.2 -1414.73 11.2896", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tower3", "ctower4.dis", "-282.701 -1308.7 8.34", "0 0 -0.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tower2", "ctower2.dis", "-338.845 -1318.93 17.1108", "0 0 -2.3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "brickhouse1", "chouse1.dis", "-232.532999 -1268.75 21.288299", "1.56896 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "yolwatchtow1", "yolanda.dis", "-96.3282 -1520.75 25.1934", "0 0 0.35");


	%zone = "Oasis";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrbridge1", "cbridge2.dis", "-4675.75 1686.75 164.290999", "0 -0 0.799999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrbridge2", "cbridge2.dis", "-4501 1964.75 163.27599", "0 -0 1.89999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "homefndtn1", "pstone_cube_s.dis", "-4173.36 1889.35 169.782", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "homefndtn2", "pstone_cube_s.dis", "-4183.09 1889.65 169.624", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "treerock1 rockmine", "lrock1.dis", "-4129.89 1882.57 170.923", "-1.2 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "sleephouse1", "cbarracksfinal.dis", "-4814.5 1763.75 226.75", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "waterhouse1", "blackbridgehousefinal.dis", "-4542.24 1608.05 162.141", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "waterhouses1", "lowershildriksfinal.dis", "-4477.1199999 1800.619999 165", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bridgetomid1", "cbridge2.dis", "-4571.769999 1780.30999 164.673999", "0 -0 1.56999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bridgetomid2", "cbridge2.dis", "-4315.75 1780.25 164.65999", "0 -0 1.56999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bridgetomid3", "cbridge2.dis", "-4288.25 1552.5 163.08999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bridgetowell1", "cbridge2.dis", "-4516 1840.25 89.75", "0.599996 0 0.349999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bridgetowell2", "pwood_wstairs_s.dis", "-4548.66999 1940.68999999 152.6209999", "0 -0.199999 1.89999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "defensehouse1", "blackbridgehousefinal.dis", "-4215.25 1784 162.6609999", "0 -0 -1.58318");
		rpg::DefineManagedObject(%zone, "InteriorShape", "defensehouse2", "blackbridgehousefinal.dis", "-4279.25 1721 162.6319999", "0 -0 3.13999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall0", "bbridge.dis", "-4403.75 1353.5 152.58299", "0 -0 0.199999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall1", "bbridge.dis", "-4268 1381 152.5999", "0 -0 0.199899");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall2", "bbridge.dis", "-4379.5 1332 94.3364999", "0 1.59999 1.76999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall3", "bbridge.dis", "-4402.5 1327.39999 94.335299", "0 1.59998 1.76998");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall4", "bbridge.dis", "-4425.5 1322.75 94.33329999", "0 1.59997 1.76997");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwal5", "bbridge.dis", "-4447.5 1318.25 94.331299", "0 1.59996 1.76996");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall6", "bbridge.dis", "-4356.5 1336.5 94.3347999", "0 1.59998 1.76998");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall7", "bbridge.dis", "-4333.5 1341.089999 94.33279999", "0 1.59998 1.76998");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall8", "bbridge.dis", "-4310 1346.049999 94.330999", "0 1.59997 1.76997");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall9", "bbridge.dis", "-4287.5 1350.5 94.32969999", "0 1.59997 1.76997");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall10", "pstone_base_l.dis", "-4089.5 1752.5 17.7754", "0 0 2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall20", "bbridge.dis", "-4093.25 1681.5 150.635", "0 0 1.84");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall21", "bbridge.dis", "-4129.11999999999 1811.5 150.625", "0 -0 1.83999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall22", "bbridge.dis", "-4111.5 1839 87.7862999999999", "0 1.59996 -2.86318");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall23", "bbridge.dis", "-4105 1816 87.7845999999999", "0 1.59995 -2.86317");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall24", "bbridge.dis", "-4098.5 1793 87.7855999999999", "0 1.59995 -2.86317");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall25", "bbridge.dis", "-4092 1770 87.7844999999999", "0 1.59995 -2.86317");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall26", "bbridge.dis", "-4085.68999999999 1748 87.7827999999999", "0 1.59994 -2.86316");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall27", "bbridge.dis", "-4078.83999999999 1725 87.7822999999999", "0 1.59993 -2.86315");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall28", "bbridge.dis", "-4072.00999999999 1702 87.7819999999999", "0 1.59993 -2.86315");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall29", "bbridge.dis", "-4065.49999999999 1679 87.7808999999999", "0 1.59992 -2.86314");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall210", "bbridge.dis", "-4059 1655.94999999999 87.7797999999999", "0 1.59992 -2.86314");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wtrwall211", "bbridge.dis", "-4329 1339.99999999999 12.3956999999999", "-1.5691 0 0.199999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "fixwater1", "myblock02.dis", "-4896.5 1453.25 156.091", "0 0 -0.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "fixwater2", "bigblock.dis", "-4924.27999999999 1465.75 149.625", "0 -0 -0.299999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "fixwater3", "bigblock.dis", "-4902.28 1457.5 144.816", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "fixwater4", "bigblock.dis", "-4891.62 1461.53 154.284", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "fixwater5", "bigblock.dis", "-4904.53 1460.48 150.098", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "vertbridge1", "cbridge2.dis", "-4326 1430.25 165.55", "0 1.56999 1.56999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "doorblk1", "pwood_pillar_s.dis", "-4488.23 1789.39 158", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "hcr1", "woodcrateb.dis", "-4197.86 1885.59 172.522", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "hcr2", "woodcrateb.dis", "-4192.72 1887.73 171.4", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "hcr3", "woodcrate.dis", "-4195.39 1888.1 172.812", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "hopup1", "pstone_walk_s.dis", "-4202.25999999999 1774.03999999999 157.75", "0.299999 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "stair1", "pstone_tstairs_l.dis", "-4450.25 1424.75 155.531999999999", "0 -0 1.59999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "stairs2", "pstone_base_l.dis", "-4447.75 1452.25 167.632999999999", "0 0 0");
		

	%zone = "Kymer Deadwood";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "spawn1", "bank.dis", "-1153.53999999999 -2431.25 112.168999999999", "0 -0 1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "spawn2", "bank.dis", "-1256.25 -2667.5 207.506999999999", "0 -0 3.09999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "spawn3", "bank.dis", "-1064.5 -2674.75 176.710999999999", "0 -0 -1.99999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "spawn4", "bank.dis", "-1121.25 -2605 166.827999999999", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree1", "walktree9", "-1074.67 -2591.56 171", "0 -0 0.1");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree2", "walktree9", "-1346.55 -2643.48 223", "0 -0 1");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree3", "walktree9", "-1307.83 -2495.15 132", "0 -0 0.5");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree4", "walktree9", "-1217.06 -2543.87 96", "0 -0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree5", "walktree9", "-1213.21 -2605.51 140", "0 -0 -1.2");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree6", "walktree9", "-1057.16 -2456.86 197", "0 -0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree10", "walktree9", "-1182.43 -2849.42 209", "0 -0 -0.2");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree20", "walktree9", "-1154.19 -2713.44 90", "0 -0 0.6");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree30", "walktree9", "-990.603 -2588.07 219", "0 -0 -1.5");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree40", "walktree9", "-1256.33 -2700.28 206", "0 -0 0.5");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree50", "walktree9", "-1151.45 -2386.03 115", "0 -0 -0");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree60", "walktree9", "-1132.57 -2604.59 166", "0 -0 3");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree70", "walktree9", "-913.369 -2549.74 263", "0 -0 -0.7");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree80", "walktree9", "-1168.44 -2284.89 214", "0 -0 2");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree90", "walktree9", "-1294.49 -2622.21 226", "0 -0 -1");
		rpg::DefineManagedObject(%zone, "StaticShape", "dwoodtree100", "walktree9", "-1052.16 -2636.05 189", "0 -0 1");


	%zone = "Old Ethren";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "centertown1", "gnollhouse2.dis", "150 2200 15", "-0.1 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "centertown2", "gnoll.dis", "110 2100 25", "0 0 -0.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "sidetown1", "gardens.dis", "50 2310 10", "0 0 -1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "sunkenentrfrnt", "gardens.dis", "290 2275 15", "-0.15 0 1.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "unusedtower1", "ctower1.dis", "175.905 2130.43 13.9655", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "coverhouse1", "myblock02.dis", "131.004999999999 2282.5 33.8084999999999", "0 -0 0.599999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "covermid1", "myblock02.dis", "189.287999999999 2157.02999999999 21.0979999999999", "-0.0999998 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "covermid2", "myblock02.dis", "189.317999999999 1965.18999999999 21.0464999999999", "0 0 0");
		
		
	%zone = "Delkin Heights";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "dcty1", "DCTY.0.dis", "-1262 364.773 85.4742", "0 -0 -0.759972");
		rpg::DefineManagedObject(%zone, "InteriorShape", "dcty1p2", "dcty.dis", "-1262 364.75 128.452999999999", "0 -0 -0.759971");
		rpg::DefineManagedObject(%zone, "InteriorShape", "dcty1p3", "dcty.dis", "-1262 364.7499 171.440999999999", "0 -0 -0.759971");
		rpg::DefineManagedObject(%zone, "InteriorShape", "dcty1p4", "dcty.dis", "-1262 364.7501 214.447999999999", "0 -0 -0.75997");
		rpg::DefineManagedObject(%zone, "InteriorShape", "dcty1p5", "dcty.dis", "-1262 364.75 257.441999999999", "0 -0 -0.75997");
		rpg::DefineManagedObject(%zone, "InteriorShape", "dcty1p6", "dcty.dis", "-1262 364.7501 300.446999999999", "0 -0 -0.75997");
		rpg::DefineManagedObject(%zone, "InteriorShape", "dcty1p7", "dcty.dis", "-1262 364.75 343.443999999999", "0 -0 -0.75997");
		rpg::DefineManagedObject(%zone, "InteriorShape", "dcty1p8", "dcty.dis", "-1262 364.7501 386.439999999999", "0 -0 -0.75997");
		rpg::DefineManagedObject(%zone, "InteriorShape", "dcty1p9", "dcty.dis", "-1262 364.75 429.440999999999", "0 -0 -0.75997");
		rpg::DefineManagedObject(%zone, "InteriorShape", "dcty1p10", "dcty.dis", "-1262 364.7501 472.341999999999", "0 -0 -0.75997");
		rpg::DefineManagedObject(%zone, "InteriorShape", "dcty1p11", "dcty.dis", "-1262 364.75 515.273999999999", "0 -0 -0.759969");
		rpg::DefineManagedObject(%zone, "InteriorShape", "dcty1p12", "dcty.dis", "-1262 364.7501 558.269999999999", "0 -0 -0.759968");
		rpg::DefineManagedObject(%zone, "InteriorShape", "anvil3 anvil", "anvil.0.dis", "-1271.12 352.389 171.44", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "jemstf1", "woodcrate.dis", "-1246.88 363.577 128.453", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "jemstf2", "woodcrate.dis", "-1249.36 354.819 128.453", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "jemstf3", "woodcrateb.dis", "-1251.39 352.331 128.453", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "jemstf4", "woodcrateb.dis", "-1252.9 368.037 128.453", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "jemstf5", "desk.dis", "-1256.5 358.214 128.451", "0 0 -1.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bartop", "rmr7thheaven.dis", "-1236.40999999999 353.135999999999 581.424999999999", "0 -0 -2.32318");


	%zone = "Hazard";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "cityfndtn1", "pstone_base_m.dis", "-460 -2315 65.1", "0 0 1.42");
		rpg::DefineManagedObject(%zone, "InteriorShape", "openhousefr", "rmrmayorhouse.dis", "-335 -2355 73", "0 0 1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "closedbar1", "rmrshildrasinn.dis", "-350 -2265 80", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "houseline1", "rmrhousing.dis", "-301 -2340 72.3", "0 0 -3.21");
		rpg::DefineManagedObject(%zone, "InteriorShape", "portal2Mercator", "remorterhall.0.dis", "-608.375 -2480.25 64", "0 -0 2.35817");
		rpg::DefineManagedObject(%zone, "InteriorShape", "anvil6 anvil", "anvil.dis", "-396.063 -2380.62 65.1312", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "heavenbarpil", "pstone_rpillar_l.dis", "-433 -2129 83", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "heavenbar2", "rmr7thheaven.dis", "-100 -2180 190", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "heavenbar3", "rmr7thheaven.dis", "-450 -2102 123", "0 0 0.8");
	
	
	%zone = "Septic System";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "sewerblock", "pstone_wall_s.dis", "-382.914 -2268.25 65.5", "0 0 1.57");


	%zone = "Cavern of Torment";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "HazardMines", "newgpassfinal.dis", "340 -3048 260", "-0.55 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "HMblock1", "pstone_wall_l.dis", "348 -3070 266", "-0.9 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "HMblock2", "pstone_wall_l.dis", "347.5 -3064 270", "-1.2 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "HMblock3", "pstone_wall_l.dis", "344 -3066 265", "0 0.1 1.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "HMblock4 rockmine", "blocker.dis", "345 -3059 275", "0.5 -0.4 -0.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "HMblock5 rockmine", "blocker.dis", "350.576 -3063.1 276.205", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "HMblock6 rockmine", "blocker.dis", "345 -3072 257", "-0.7 0.5 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "HMblock7 rockmine", "blocker.dis", "353.498 -3072.42 253.471", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "HMblock8 rockmine", "bigblock.dis", "353 -3070 266", "0 0 0.1");


	%zone = "Mercator";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "kobadis", "newkoba.dis", "-1800 -1865 52.5", "0 0 -0.2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "porttown1", "porttown.dis", "-1852.25 -1862 36.6276999999999", "0 0 4.6");
		rpg::DefineManagedObject(%zone, "InteriorShape", "portal2Wellsprings", "remorterhall.dis", "-1740 -1842 50.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "undercliff1 rockmine", "ccliff.dis", "-1889.5 -1912 37", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "undercliff2 rockmine", "ccliff.dis", "-1889.5 -1972 42", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gmstand", "endtable.dis", "-1829.12 -1865.01 43", "0 0 -0.2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "deska", "desk.dis", "-1759.1 -1881.56 43.031", "0 0 -1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "deskb", "desk.dis", "-1801.39 -1908.25 43.0309", "0 0 -1.78");
		rpg::DefineManagedObject(%zone, "InteriorShape", "deskc", "desk.dis", "-1786.59 -1837.67 43.0311", "0 0 0.2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "deskd", "desk.dis", "-1857.27 -1899.65 43.1074", "0 0 -0.9");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wcrate1", "woodcrateb.dis", "-1853.43 -1904.35 43.1077", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bed1", "bed.dis", "-1818.25 -1919 43.1068", "0 0 -0.15");


	%zone = "Collapsed Castle";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "midtower", "looklook.dis", "353 -1022 277", "0 0 0.05");
		rpg::DefineManagedObject(%zone, "InteriorShape", "outerwall1", "milburnewalls2final.dis", "365 -1003 297.2", "0 0 0.05");
		rpg::DefineManagedObject(%zone, "InteriorShape", "outerwall1back", "milburnewalls1final.dis", "365 -1040 297.2", "0 0 0.05");
		rpg::DefineManagedObject(%zone, "InteriorShape", "outerwallside1", "milburnewalls1final.dis", "368 -1020 297.2", "0 0 1.7");
		rpg::DefineManagedObject(%zone, "InteriorShape", "outerwallside2", "milburnewalls1final.dis", "325 -1020 297.2", "0 0 1.6");
		rpg::DefineManagedObject(%zone, "InteriorShape", "houseblock1", "pstone_wall_s.dis", "381.976 -1003.25 269", "0 0 0.1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "houseblock2", "pstone_wall_s.dis", "384.5 -1005.25 269", "0 0 1.7");
		rpg::DefineManagedObject(%zone, "InteriorShape", "houseblock3", "pstone_wall_s.dis", "382 -1003.5 272.75", "1.55 0 0.1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blockhiexit1", "pstone_wall_s.dis", "350.9 -1046 269.2", "0 0 0.05");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blockhiexit2", "pstone_wall_s.dis", "331.505 -1028.4 269.2", "0 0 1.62");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blockloexit1", "pstone_wall_s.dis", "349.15 -1010.5 262.2", "0 0 0.05");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blockloexit2", "pstone_wall_s.dis", "331 -1028.45 262.2", "0 0 1.65");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ledgeblock1", "pstone_wall_l.dis", "380 -1003.45 265", "0 -0.9 0.1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "roundpillarstair", "pstone_rpillar_l.dis", "370 -1004 260.21", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "roundpillarfill1", "pstone_wall_s.dis", "374.454 -1004 262", "0 0 0.1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "roundpillarfill2", "pstone_wall_l.dis", "379.586 -1008.25 260", "0 0 0.11");
		rpg::DefineManagedObject(%zone, "InteriorShape", "roundpillarfill3", "pstone_wall_m.dis", "377 -1003.75 269", "0 0 0.1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "floatfloor1", "pstone_base_l.dis", "373 -1022 262", "0 0 0.1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "pillar1", "pstone_pillar_m.dis", "404 -1053 262", "0 0 -0.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "pillar2", "pstone_pillar_m.dis", "397.5 -1053.5 262", "0 0 0.9");
		rpg::DefineManagedObject(%zone, "InteriorShape", "pillar3", "pstone_pillar_m.dis", "401 -1055 262", "0 0 0.8");
		rpg::DefineManagedObject(%zone, "InteriorShape", "pillar5", "pstone_wall_l.dis", "384.5 -1012 262", "0 0 1.66");
		rpg::DefineManagedObject(%zone, "InteriorShape", "pillar6", "pstone_wall_l.dis", "385 -1018 262", "0 0 1.65");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blockbottom1", "pstone_floor_m.dis", "350 -1027.5 261.6", "0 0 0.05");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wallstop1", "pstone_wall_l.dis", "350.676 -1032 260", "0 0 0.05");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wallstop2", "pstone_wall_l.dis", "354.6 -1028 260", "0 0 1.62");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wallstop3", "pstone_wall_l.dis", "350 -1023 260", "0 0 0.05");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gpassdungeon", "gpasspass.dis", "417 -1047.5 257", "0 0 -3.04");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ceil1", "pstone_floor_l.dis", "380 -1031.5 274.5", "0 0 0.11");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ceil2", "pstone_floor_l.dis", "400 -1030 274.5", "0 0 0.1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ceil3", "pstone_floor_l.dis", "400 -1045 274.5", "0 0 0.1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ceil4", "pstone_floor_l.dis", "388 -1028 274.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blockmtn1", "pstone_base_m.dis", "397 -1019.5 275", "0 0 -0.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "entrancelid", "pstone_floor_l.dis", "399.158 -1048.33 261.951", "0 0 0.11");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blockmtn2", "pstone_base_l.dis", "421 -1035 277", "0 0 0.1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blockmtn3", "pstone_base_l.dis", "379 -1051.4 277", "0 0 0.09");
		rpg::DefineManagedObject(%zone, "InteriorShape", "rockblock1 rockmine", "blocker.dis", "361.468 -1013.36 283.784", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "rockblock2 rockmine", "blocker.dis", "348 -1015 284", "0 -0.3 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "rockblock3 rockmine", "blocker.dis", "336.735 -1016.34 280.62", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "rockblock4 rockmine", "bigblock.dis", "340 -1025 288", "1.6 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "rockblock5 rockmine", "bigblock.dis", "360 -1025 288", "1.6 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "rockblock6 rockmine", "bigblock.dis", "363 -1038 283", "-0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "rockblock7 rockmine", "bigblock.dis", "340 -1044 288", "1.5 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "rockblock8 rockmine", "bigblock.dis", "339.96 -1015.54 288.039", "0 -1 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "rockblock9 rockmine", "bigblock.dis", "328 -1009 268", "1.5 0 0.3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "arenablk1", "pstone_wall_s.dis", "351.562 -1052.85 262.375", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "arenablk2", "pstone_wall_s.dis", "350.065999999999 -1047.58999999999 262.375", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "arenablk3", "pstone_pillar_s.dis", "377.378 -1019.38 268.25", "0 0 0");


	%zone = "Wellsprings";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "portal2Keldrin", "remorterhall.dis", "-722.034 -297.412 221.909", "0 0 1.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "hallbase1", "pstone_base_l.dis", "-717.931 -293.433 214.294", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "fplace", "fireplaceb.dis", "-382.031 -247.305 66.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "pic1", "pic2.dis", "-393.474 -289.9 72.3396", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "pica3", "pic3.dis", "-401.796 -261.131 69.8537", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "pica5", "pic5.dis", "-391.992 -246.461 78.7542", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tablea4", "bigtable2.dis", "-377.785 -272.718 66.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bencha6", "bench1.dis", "-375.003 -246.193 66.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bencha7", "bench2.dis", "-370.69 -246.058 74.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "flagblka8", "twosideflag.dis", "-367.399999999999 -245.631999999999 72.8986999999999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "treea10 tree", "rmrforestspike.dis", "-344.816999999999 -278.75 77", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "treea11 tree", "rmrforestspike.dis", "-298.724999999999 -261.451999999999 75", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "woodfq1", "woodfire.dis", "-403.568 -249.709 66.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "basetq4 tree", "basetree.dis", "-414.488 -242.681 65", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "logcabq5", "chouse2.dis", "-310.590999999999 -284.576999999999 65", "0 -0 1.99999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tableb1", "bigtable1.dis", "-310.423 -275.955 65.5", "0 0 0.4");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bssib2", "bssign.dis", "-311.73 -268.052 65", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bb1", "pwood_cube_l.dis", "-326.788 -200.172 65.5999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bb1", "pwood_cube_s.dis", "-354.819 -182.05 65.5999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bb2", "pwood_cube_m.dis", "-355.92 -183.25 65.5999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bb3", "woodcrate.dis", "-359.23 -181.871 65.5999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bb4", "woodcrate.dis", "-353.153 -181.015 65.5999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bb4", "woodcrateb.dis", "-350.214 -183.209 65.5999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bb5", "woodcrateb.dis", "-360.64 -186.769 65.5999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wsprt1", "shildrikhouse.dis", "-331.579999999999 -237.905999999999 73", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wsprt2", "shildrikhouse.dis", "-335.886999999999 -264.243999999999 73", "0 -0 2.99999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wsprb1", "pwood_flag_red.dis", "-376.306 -289.028 65", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wspra2", "fireplace.dis", "-391.024999999999 -262.620999999999 74.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wspra1", "fireplace.dis", "-379.376 -262.239 74.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wspra3", "pstone_wall_l.dis", "-404.676 -273.651 74.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wsprr1 rockmine", "lrock1.dis", "-369.263 -216.529 66.1867", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wsprr2 rockmine", "lrock2.dis", "-363.45 -202.039 65.0999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wsprr3 rockmine", "lrock3.4.dis", "-351.356 -209.571 65.0999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wspr1 rockmine", "lrock5.4.dis", "-358.24 -232.439 65", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wsprr4 rockmine", "lrock6.1.dis", "-376.104 -211.899 65.0999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "anvil5 anvil", "anvil.dis", "-306.981 -268.398 65.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "anvil4 anvil", "anvil.dis", "-322.278 -285.65 65.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bed", "bed.dis", "-409.401 -258.19 66.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bed2", "bed.dis", "-401.742 -257.314 66.5", "0 0 0");


	%zone = "The Wastes";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "twdec1 rockmine", "jrock.dis", "-1784.57 1172.76 185.177", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "twdec2 rockmine", "ccliff.dis", "-2494.66 1523.03 172.869", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "twdec4", "wharf.dis", "-2182.05 1678.59 51.4093", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "twdec5 tree", "basetree.dis", "-2380.1 1766.8 65", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "twdec6 tree", "basetree.dis", "-2060.92 1495.73 104.485", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec10 tree", "basetree.dis", "-2669.44 1600.05 58.8885", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec12 rockmine", "jrock.dis", "-2416.33 1720.18 65", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec15", "cruins2final.dis", "-2342.6899999 1800.75 75", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec13", "cruins2final.dis", "-2442.63 1775.85 75", "0 0 2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec14", "cavernsenter.dis", "-2431.58999 1812.75 78.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec16", "rmrbloodgate.dis", "-2131.25 1337.75 220.585999", "-0.399999 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec17", "mountainring.dis", "-2730 1742.25 227.072999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec19 rockmine", "mine4.dis", "-2152.13 1596.04 6.48048", "0 0 1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec20 rockmine", "mine3a.dis", "-1964.36999 1622.779999 162.32299", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec21 rockmine", "mine3b.dis", "-2279.03 2030.12 218.977", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec22", "cbuilding1.dis", "-2576.5 1087.5 458.548999", "1.56981 0 1.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec23", "myblock02.dis", "-2126.55 1601 22.8961", "0 -0.01 1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec33 rockmine", "blocker.dis", "-2743.89 1480.88 208.271", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec34 rockmine", "blocker.dis", "-2764.43 1593.76 172.319", "0 0 -0.1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec35 rockmine", "bigblock.dis", "-2569.7 1582.05 144.92", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec36 rockmine", "bigblock.dis", "-2214.85 1414.21 142.847", "0 0 -0.3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec37 rockmine", "bigblock.dis", "-1968.94 1517.28 178.269", "0 0 0.3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec38 rockmine", "bigblock.dis", "-2152 1720.48 46.3536", "0 0 0.3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec39", "myblock04.dis", "-1986 1682 57.9713", "-1.09999 0.999999 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec0", "etruin2.dis", "-2272.6 1735.04 51.3505", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec1 rockmine", "rockarch2.dis", "-2295.75 1613.26999999999 91.3704999999999", "0 -0 -0.499999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec2", "cwalls1.dis", "-2273.25 1520.75 50.2980999999999", "0 -0 -0.599999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec3", "milburnewalls2final.dis", "-2144.5 1606 47.8337999999999", "0 -0 -0.599999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec4", "castle4.dis", "-2153.25 1885.13999999999 183.382999999999", "0 0.0499999 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec5", "etruin2.dis", "-2041.5 1745.52999999999 15.2072999999999", "0 -0 1.99999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ztwdec6", "milburnewalls2final.dis", "-2329.5 1733 84.6051999999999", "0.199999 0 0.799999");


	%zone = "Antaris Remains";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "oldwall1", "w64long.dis", "-1330 -1700 0", "0 0 0.4");
		rpg::DefineManagedObject(%zone, "InteriorShape", "oldwall2", "w64long.dis", "-1388 -1724.5 0", "0 0 0.4");
		rpg::DefineManagedObject(%zone, "InteriorShape", "oldwall3", "w64long.dis", "-1272 -1675.5 0", "0 0 0.3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "oldwall4", "w64long.dis", "-1215 -1658 0", "0 0.1 0.3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "oldwall5", "w64long.dis", "-1153 -1638.8 -6.5", "0 0 0.2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "oldwall6", "w64long.dis", "-1390 -1720 -0.01", "0 0 -1.6");
		rpg::DefineManagedObject(%zone, "InteriorShape", "oldwall7", "w64long.dis", "-1391.75 -1780 0", "0 0 -1.6");
		rpg::DefineManagedObject(%zone, "InteriorShape", "oldwall8", "w64long.dis", "-1090 -1630 -6.501", "0 0 -1.35");
		rpg::DefineManagedObject(%zone, "InteriorShape", "floor1", "myblock07.dis", "-1360 -1871 4.73657", "0 0 -1.3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "floor2", "myblock07.dis", "-1078.5 -1703.5 5.21661999999999", "0 -0 0.199999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bridge1", "cbridge2.dis", "-1254.75 -1711.75 14.3532999999999", "-0.0249999 0 1.89999");		
		rpg::DefineManagedObject(%zone, "InteriorShape", "oldtree1 tree", "rmrforestspike.dis", "-1380.25 -1748.06999999999 12.7472999999999", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "oldtree2 tree", "PhantomStrangerTree2", "-1380.25 -1748.06999999999 12.7472999999999", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "oldtree3 tree", "PhantomStrangerTree2", "-1222.17 -1675.63 6.21661", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "oldtree4 tree", "PhantomStrangerTree2", "-1273.4 -1735.26 5.73656", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "oldtree5 tree", "PhantomStrangerTree2", "-1166.25 -1678.2 9.21611", "0 0 0");


	%zone = "Ancient Sanctuary";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "ancblock1", "pwood_floor_l.dis", "-2850.8 848.72 700.498", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "stairanc1", "pwood_wstairs_s.dis", "-2853.16999999999 835.034999999999 700.991999999999", "0 -0 1.56999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "stairanc2", "pwood_walk_s.dis", "-2847.12 854.106 706", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "stairanc3", "pwood_walk_s.dis", "-2846.49 854.112 706.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "stairanc4", "pwood_walk_s.dis", "-2846.07 854.117 707", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blocker2", "pstone_wall_m.dis", "-2768.09 861.564 662.25", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blocker3", "pstone_wall_m.dis", "-2786.55 861.557 662.25", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blocker4", "pstone_wall_m.dis", "-2786.64 844.209 662.25", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blocker5", "pstone_wall_m.dis", "-2767.99 844.207 662.25", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blocker6", "pstone_cube_l.dis", "-2764.98 855.687 661.75", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blocker7", "pstone_cube_l.dis", "-2765.13 850.742 661.75", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blocker8", "pstone_cube_l.dis", "-2761.09 846.436 662.25", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blocker9", "pstone_cube_l.dis", "-2761.22 859.321 662.25", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blocker10", "pstone_cube_l.dis", "-2794.14 846.988 658.25", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blocker11", "pstone_cube_l.dis", "-2794.29 858.023 658.25", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blocker12", "pstone_cube_l.dis", "-2795.49 862.323 662.66", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blocker13", "pstone_cube_l.dis", "-2793.5 843.901 663.488", "0 0 0");


//	%zone = "Temple Of Delusion";
//	%zone = rpg::CreateManagedObjectList(%zone);
		


	%zone = "Amazon Forest";
	%zone = rpg::CreateManagedObjectList(%zone);		
		rpg::DefineManagedObject(%zone, "InteriorShape", "AmazonBarrier1", "milburnewalls1final.dis", "-3243 -555 163", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AmazonBarrier2 tree", "stree.dis", "-3243 -724 148", "0 0 .5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AmazonBarrier3 tree", "stree.dis", "-3234 -720 148", "0 0 .5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AmazonBarrier5 tree", "stree.dis", "-3261 -729 148", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AmazonBarrier8 tree", "stree.dis", "-3130.26 -656.76 175", "0 .25 -3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AmazonBarrier9 tree", "stree.dis", "-3115.26 -656.76 180", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AmazonBarrier43 tree", "stree.dis", "-3156.31 -532.497 170", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AmazonBarrier44 tree", "stree.dis", "-3146.31 -532.497 170", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeShape11", "TreeShape", "-3136.56 -565.1 166.533", "0 0 0.7");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeShape21", "TreeShape", "-3243.63 -671.843 136.11", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeShape31", "TreeShape", "-3181.67 -647.89 100.675", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeShapeTwo42", "TreeShapeTwo", "-3213.35 -679.793 104.384", "0 0 0.1");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeShapeTwo32", "TreeShapeTwo", "-3155.18 -559.147 164.729", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeShapeTwo12", "TreeShapeTwo", "-3149.77 -568.757 161.92", "0 0 0.1");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeShapeTwo22", "TreeShapeTwo", "-3171 -546.326 168.846", "0 0 0.1");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree9", "PhantomStrangerTree1", "-3204.85 -653.617 87.6831", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree8", "PhantomStrangerTree1", "-3169.23 -641.863 119.032", "0 0 0.65");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree7", "PhantomStrangerTree1", "-3109.85 -563.479 178.821", "0 0 0.43");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree6", "PhantomStrangerTree1", "-3177.34 -551.191 168.613", "0 0 0.3");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree51", "PhantomStrangerTree1", "-3184.18 -681.463 113.466", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree11", "PhantomStrangerTree1", "-3220.08 -654.475 98.2953", "0 0 0.7");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree41", "PhantomStrangerTree1", "-3227.6 -631.741 109.133", "0 0 0.6");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree991", "PhantomStrangerTree1", "-3220.6 -715.607 147.377", "0 0 0.2");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree31", "PhantomStrangerTree1", "-3215.33 -603.278 119.89", "0 0 0.7");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree151", "PhantomStrangerTree1", "-3176.7 -590.862 142.578", "0 0 0.33");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree141", "PhantomStrangerTree1", "-3235.86 -592.09 133.039", "0 0 0.9");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree111", "PhantomStrangerTree1", "-3201.68 -560.985 163.491", "0 0 0.8");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree131", "PhantomStrangerTree1", "-3244.71 -635.462 139.151", "0 0 0");


	%zone = "Palace Safeway";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock11 rockmine", "lrock1.5.dis", "-2052 -2580.25 116.375", "0 -1.19838 0.01999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock31 rockmine", "lrock3.5.dis", "-2042.25 -2589.25 117.625", "0.375747 -0.861069 -0.285146");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock31 rockmine", "lrock3.6.dis", "-2034.75 -2603 119.5", "-0.0233227 -0.399189 0.0752577");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock41 rockmine", "lrock4.7.dis", "-2063.5 -2637 116.875", "-1.03475e-05 -3.46607e-05 0.959741");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock11 rockmine", "lrock1.6.dis", "-2083 -2603 113.75", "-0.271719 2.1875 -0.378334");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock11 rockmine", "lrock1.7.dis", "-2037.75 -2618 120.75", "-0.0198881 1.65804 0.0217734");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock31 rockmine", "lrock3.7.dis", "-2045 -2629 118.5", "0.0692126 -0.375 -1.36977");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock31 rockmine", "lrock3.8.dis", "-2086.75 -2615.25 116.5", "0.0297681 0.51906 0.07207");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock11 rockmine", "lrock1.8.dis", "-2077.5 -2594.5 111.5", "0 -0 0.01999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "lrock41 rockmine", "lrock4.8.dis", "-2074.75 -2626 116.5", "-0.0938091 -0.896305 0.0947751");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl1", "candelabra.dis", "-1885 -2854.02 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl2", "candelabra.dis", "-1894.99 -2833.64 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl3", "candelabra.dis", "-1896.98 -2831.51 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl4", "candelabra.dis", "-1885.01 -2836.8 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl5", "candelabra.dis", "-1884.54 -2839.53 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl6", "candelabra.dis", "-1887.04 -2835.48 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl7", "candelabra.dis", "-1923.19 -2864.78 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl8", "candelabra.dis", "-1921 -2865.12 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl9", "candelabra.dis", "-1915.16 -2844.87 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl10", "candelabra.dis", "-1918.99 -2833.47 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl11", "candelabra.dis", "-1921.14 -2833.48 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl12", "candelabra.dis", "-1914.95 -2839.47 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl13", "candelabra.dis", "-1924.9 -2834.66 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl14", "candelabra.dis", "-1917.93 -2844.71 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl15", "candelabra.dis", "-1917.62 -2845.74 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl16", "candelabra.dis", "-1904.46 -2859.46 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl17", "candelabra.dis", "-1904.62 -2861.5 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl18", "candelabra.dis", "-1928.46 -2833.48 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl19", "candelabra.dis", "-1928.52 -2839.52 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl20", "candelabra.dis", "-1928.45 -2849.49 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl21", "candelabra.dis", "-1929 -2858.05 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl22", "candelabra.dis", "-1917.64 -2855.47 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl23", "candelabra.dis", "-1897.37 -2855.52 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl24", "candelabra.dis", "-1890.78 -2843.49 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl25", "candelabra.dis", "-1925.75 -2841.5 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl26", "candelabra.dis", "-1918.96 -2842.42 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl27", "candelabra.dis", "-1904.25 -2835.42 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl28", "candelabra.dis", "-1912.79 -2837.49 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl29", "candelabra.dis", "-1912.99 -2835.55 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl30", "candelabra.dis", "-1911 -2838.94 159.921", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "cndl31", "candelabra.dis", "-1909.59 -2835.46 159.921", "0 0 0");


//	%zone = "Stronghold Yolanda";
//	%zone = rpg::CreateManagedObjectList(%zone);
		


//	%zone = "Chamber of Animation";
//	%zone = rpg::CreateManagedObjectList(%zone);
		


	%zone = "Keldrin Mine";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo1", "TreeShapeTwo", "-1727.25 -933.5 241.25", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeOne1", "TreeShape", "-1833.75 -771.25 242.375", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "kminedoor1", "cdoora.dis", "-1743.5 -1117.75 245.125", "0 -0 1.56999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "kminedoor2", "cdoord.dis", "-1741 -1239.2799 283.125", "0 1.56999 -1.56999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "house11", "house1.0.dis", "-1785.75 -876.7 185.785", "0 -0 -1.49774");
		rpg::DefineManagedObject(%zone, "StaticShape", "CryptTreeMarker1", "DTS_bark", "-976 688 590", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "CryptTreeMarker2", "DTS_bark", "-885 872 697", "0 0 0");


	%zone = "Elven Outpost";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "w64long1", "W64long.0.dis", "-1513.5 2680 458.625", "0 -0 1.55838");
		rpg::DefineManagedObject(%zone, "InteriorShape", "w64long1", "W64long.1.dis", "-1641 2450.75 466.3", "-0.0111787 0.46075 0.108197");
		rpg::DefineManagedObject(%zone, "InteriorShape", "w64long1", "W64long.2.dis", "-1791.25 2699 480.625", "0 -0 1.02901");
		rpg::DefineManagedObject(%zone, "InteriorShape", "w64long1", "W64long.3.dis", "-1574 2491.5 421.32", "0 -0 1.11839");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree11", "PhantomStrangerTree1", "-1629.25 2702.25 398.625", "0 -0 -1.53906");
		rpg::DefineManagedObject(%zone, "InteriorShape", "w64long1", "W64long.4.dis", "-1698.25 2453.5 480.3", "-0.00755387 0.259845 -0.0283973");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree31", "PhantomStrangerTree3", "-1629 2732 404.946", "0 -0 -0.139963");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeOne1", "PhantomStrangerTree2", "-1660.5 2707.25 399.561", "0 -0 -0.239967");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeOne1", "PhantomStrangerTree2", "-1593.75 2745.75 411.963", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeOne1", "PhantomStrangerTree2", "-1680.5 2692.5 399.931", "0 -0 -0.479967");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo1", "PhantomStrangerTree2", "-1718 2625 439.91", "0 -0 -2.13846");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo1", "PhantomStrangerTree2", "-1605 2561.5 408.2", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo1", "PhantomStrangerTree2", "-1665 2576.75 417.875", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeOne1", "PhantomStrangerTree2", "-1679.75 2661.25 398.25", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeOne1", "PhantomStrangerTree2", "-1615.75 2627.5 402.25", "0 -0 0.902032");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeOne1", "PhantomStrangerTree2", "-1584 2667.75 398.152", "0 -0 -0.539965");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo1", "PhantomStrangerTree2", "-1721.75 2650.75 439.325", "0 -0 0.179782");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeOne1", "PhantomStrangerTree2", "-1703.25 2693.75 424.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo1", "PhantomStrangerTree2", "-1587.75 2600.75 429.625", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo1", "PhantomStrangerTree2", "-1669.25 2613.5 398.125", "0 -0 -1.57871");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo1", "PhantomStrangerTree2", "-1556 2687.25 425.593", "0 -0 -0.599779");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo1", "PhantomStrangerTree2", "-1590 2698 397.94", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo1", "PhantomStrangerTree2", "-1640.5 2610.5 398.146", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeOne1", "PhantomStrangerTree2", "-1618.25 2601.25 406.286", "0 -0 1.55949");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo1", "PhantomStrangerTree2", "-1681 2641.5 398.419", "0 -0 2.52168");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo1", "PhantomStrangerTree2", "-1576 2645.5 419.843", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "TreeTwo1", "PhantomStrangerTree2", "-1645.25 2545.25 427.05", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree31", "PhantomStrangerTree3", "-1702.5 2597 414.814", "0 -0 -0.719904");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree11", "PhantomStrangerTree1", "-1656 2670.25 398.168", "0 -0 -1.25968");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree11", "PhantomStrangerTree1", "-1629.75 2641.5 398.568", "0 -0 -2.11906");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree11", "PhantomStrangerTree1", "-1619.25 2669 398.768", "0 -0 -1.11906");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree11", "PhantomStrangerTree1", "-1602.25 2688 395.641", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree11", "PhantomStrangerTree1", "-1657 2603.5 399.53", "0 -0 -2.15967");
		rpg::DefineManagedObject(%zone, "StaticShape", "PhantomStrangerTree11", "PhantomStrangerTree1", "-1675.25 2630 398.152", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "mylehi", "town51.dis", "-1647.25 2583.75 715.892999999999", "0 -0 2.19999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "mylehisupport1", "pwood_beam_l.dis", "-1665.03999999999 2578.02999999999 693.377999999999", "0 -0 -0.899999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "mylehisupport2", "pwood_beam_l.dis", "-1658 2583 693.299999999999", "0 -0 -0.899999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "mylehisupport3", "pwood_walk_l.dis", "-1645.5 2591.25 693.312999999999", "0 -0 -0.949999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "mylehidoor", "drawbridge.dis", "-1709.25 2517.5 693.625", "0 -0 0.619999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "merchant1", "merchant.1.dis", "-1639.5 2686.5 401.946", "0 -0 -0.539968");
		rpg::DefineManagedObject(%zone, "InteriorShape", "npchut1", "npchut.2.dis", "-1688.25 2658.5 401.71", "0 -0 -0.539968");
		rpg::DefineManagedObject(%zone, "InteriorShape", "joutp1", "JOUTP.0.dis", "-1653.75 2637.5 399.875", "0 -0 1.04434");
		

	%zone = "Syncronicon";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "Syncronicon1", "rmrsuntemplefinal.dis", "19.5 2 1374.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "Syncronicon2", "myblock02.dis", "120.765999999999 -84.6303999999999 1358.75", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "Syncronicon3", "DTS_Rockingchair", "26.1413 -40.7003 1359.75", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "Syncronicon4", "bookshelfl.dis", "25.0146999999999 1.22547999999999 1360", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "Syncronicon5", "sign.dis", "49.5838 -11.9999 1349.08", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "Syncronicon6", "desk.dis", "54.3944 -5.43236 1343", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "Syncronicon7", "bed.dis", "-4.98529 -6.10849 1350", "0 0 0");


	%zone = "Eviscera";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "evis1", "rmrsirastrialp2.dis", "-1666 -2800 -1300", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "evis2", "throne.dis", "-1657.9 -2851.43 -1402.47", "0 0 3.14");
		rpg::DefineManagedObject(%zone, "StaticShape", "evis3", "PlasmaFlame", "-1652.73 -2823 -1400.88", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "evis58", "PlasmaFlame", "-1639.84 -2799 -1396.56", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "evis59", "PlasmaFlame", "-1639.54 -2799 -1399.26", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "evis60", "PlasmaFlame", "-1645.13 -2799 -1395.35", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev3", "GiantFlame", "-1647.6 -2810.21 -1205", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev8", "MassiveFlame", "-1645.49 -2815.49 -1205.51", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev8", "GiantFlame", "-1649.44 -2806.43 -1205", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev9", "GiantFlame", "-1655.55 -2804.52 -1205", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev20", "GiantFlame", "-1657.78 -2853.43 -1402.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev21", "GiantFlame", "-1654.13 -2824.3 -1403", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev23", "GiantFlame", "-1643.88 -2804.64 -1403", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev26", "GiantFlame", "-1652.87 -2817.95 -1403", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev27", "GiantFlame", "-1643.7 -2813.38 -1403", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev29", "MassiveFlame", "-1659.49 -2809.28 -1400.87", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev30", "GiantFlame", "-1657.18 -2806.17 -1402.2", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev32", "GiantFlame", "-1654 -2836.82 -1401.85", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev33", "GiantFlame", "-1662 -2844.96 -1401.75", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev41", "GiantFlame", "-1634 -2804.5 -1395.24", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev42", "GiantFlame", "-1634 -2809.14 -1394.97", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev44", "GiantFlame", "-1634 -2804.48 -1399.68", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev45", "GiantFlame", "-1634 -2809.24 -1399.45", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev47", "GiantFlame", "-1634.39 -2804.79 -1403", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev48", "MassiveFlame", "-1634.34 -2809.61 -1403", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev49", "MassiveFlame", "-1637.62 -2803.52 -1403", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev50", "MassiveFlame", "-1657.66 -2824.3 -1403", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "ev51", "MassiveFlame", "-1651.98 -2811.57 -1403", "0 0 0");


	%zone = "Altar Of Shame";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "t1", "rmrwwatchtower.dis", "-3693.76999999999 2641 424", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t2", "rmrwwatchtower.dis", "-3689.5 2686.64 422", "0 0 1");
		rpg::DefineManagedObject(%zone, "StaticShape", "t3", "GiantFlame", "-3692.52 2634.42 416.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "t4", "GiantFlame", "-3684.64 2684.85 414.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t5", "rmrwindmill.dis", "-3565.25 2634.29999999999 500.334999999999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t6", "rmrwindmill.dis", "-3888.75999999999 2544.77999999999 540.303999999999", "0 -0 -1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t8 tree", "rmrforestspike.dis", "-3907.81 2645.51 539.459", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t10 tree", "rmrforestspike.dis", "-3864.42 2755.22 497.581", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t12 tree", "rmrforestspike.dis", "-3818.74 2582.41 500.667", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t13 tree", "rmrforestspike.dis", "-3627.61 2556.11 420.475", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t14 tree", "rmrforestspike.dis", "-3714.11 2473.18 479.787", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t16 tree", "rmrforestspike.dis", "-3595.32 2721.03 436.774", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t17 tree", "rmrforestspike.dis", "-3667.3 2758.74 427.726", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t20 tree", "rmrforestspike.dis", "-3718.26 2670.81 400", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t21 tree", "rmrforestspike.dis", "-3774.13 2722.01 460.105", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t22 tree", "rmrforestspike.dis", "-3701.61 2554.58 427.577", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t23 tree", "rmrforestspike.dis", "-3569.19 2825.72 559.246", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t24 tree", "rmrforestspike.dis", "-3565.78 2763.81 496.224", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "sp1 tree", "rmrforestspike.dis", "-3618.13 2412.5 399.045", "0.3 0.3 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "sp2 tree", "rmrforestspike.dis", "-3582.79 2427.1 392.961", "0.3 0.3 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "sp3 tree", "rmrforestspike.dis", "-3619.53 2441.22 430.335", "0.3 0.3 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "sp4 tree", "rmrforestspike.dis", "-3653.61 2427.52 431.871", "0.3 0.3 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "sp5 tree", "rmrforestspike.dis", "-3581.07 2464.5 411.122", "0.3 0.3 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "sp6 tree", "rmrforestspike.dis", "-3626.32 2476.41 434.424", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bl1", "myblock07.dis", "-3558.75 2646.25 461.875", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bl2", "myblock07.dis", "-3600.25 2459.25 423.664999999999", "0.199999 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bb1", "myblock07.dis", "-3750.75 2647.25999999999 461.775999999999", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "f1", "MassiveFlame", "-3711 2665.5 401", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "st1", "pwood_pillar_m.dis", "-3693.64 2643.37 400", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "st2", "mineshaft.dis", "-3693.5 2649 398.378999999999", "1.569 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "k1", "pstone_roof_m.dis", "-3693.50999999999 2648.98999999999 400", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "mf1", "GiantFlame", "-3693.5 2648.98 403.99", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "mf2", "GiantFlame", "-3693.5 2648.98 410", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "mf3", "GiantFlame", "-3693 2648.98 414", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "k2", "pstone_walk_m.dis", "-3693.76999999999 2643.16999999999 416", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tb1", "bench1.dis", "-3693.69 2644.21 416.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tb1", "bench1.dis", "-3693.69 2644.21 416.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "bs1", "bloodspot", "-3694.06 2643.97 417.187", "1.2 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "bs2", "bloodspot", "-3693.76 2644.11 417.187", "1.6 0 1");
		rpg::DefineManagedObject(%zone, "StaticShape", "bs4", "bloodspot", "-3693.37 2643.9 417.187", "1.5 0 -0.5");
		rpg::DefineManagedObject(%zone, "StaticShape", "bs5", "bloodspot", "-3693.75 2643.75 416.5", "1.5 0 1");
		rpg::DefineManagedObject(%zone, "StaticShape", "bs9", "bloodspot", "-3693.99 2644.43 417.187", "1.2 0 2.1");
		rpg::DefineManagedObject(%zone, "StaticShape", "bs10", "bloodspot", "-3694.78 2644.06 417.331", "1.5 0 0.5");
		rpg::DefineManagedObject(%zone, "StaticShape", "bs11", "bloodspot", "-3693.7 2643.74 417.158", "1.5 0 1.5");
		rpg::DefineManagedObject(%zone, "StaticShape", "bs17", "bloodspot", "-3693.66 2644.05 417.187", "1.5 0 0.8");
		rpg::DefineManagedObject(%zone, "StaticShape", "bs18", "bloodspot", "-3693.56 2644.21 417.187", "1.5 0 0.6");
		rpg::DefineManagedObject(%zone, "StaticShape", "bs20", "bloodspot", "-3693.78 2643.55 416.5", "1.5 0 0.3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "p1", "bigblock.dis", "-3600 2573 440.287999999999", "0 -0 1.56999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "p2", "bigblock.dis", "-3597 2551 435.125", "0 -0 1.56998");
		rpg::DefineManagedObject(%zone, "InteriorShape", "p3", "bigblock.dis", "-3599 2527 430.125", "0 -0 1.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "p4", "bigblock.dis", "-3603.91 2513.19 423.292", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "p5", "bigblock.dis", "-3602.58 2482.93 425.849", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "p7", "bigblock.dis", "-3603.71 2476.26 428.134", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "p8", "rmrcliffhouse.dis", "-3617.75 2505.75 434.482999999999", "0 -0 1.09999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "n1 rockmine", "lrock1.3.dis", "-3613.31 2491.72 427.468", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "n2 rockmine", "lrock1.3.dis", "-3625.21 2497.26 428.645", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "jr rockmine", "jrock.dis", "-3614.5 2503 434.584999999999", "0 -0 1.09999");
//		rpg::DefineManagedObject(%zone, "InteriorShape", "cpult1", "catipult.dis", "-3859 2504.39 536.922", "0 0 4");
//		rpg::DefineManagedObject(%zone, "InteriorShape", "cpult2", "catipult.dis", "-3561.96 2339.33 350.298", "0 0 4");
//		rpg::DefineManagedObject(%zone, "InteriorShape", "cpult3", "catipult.dis", "-3560.04 2386.19 350.297", "0 0 3.8");
//		rpg::DefineManagedObject(%zone, "InteriorShape", "cpult4", "catipult.dis", "-3547.8 2362.02 350.298", "0 0 4.1");


	%zone = "Abandoned Dig Site";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "AbandonedDig1 rockmine", "strawberrymines.dis", "-3603.5 2421.75 339.508999999999", "0 -0 -2.62318");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AbandonedDig2", "ccliff.dis", "-3620.5 2355.5 357.5", "0 -0 -1.09999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AbandonedDig3 rockmine", "blocker.dis", "-3564.31 2418.37 325.509", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AbandonedDig4 rockmine", "blocker.dis", "-3574.32 2412.84 325.506", "0 0 0.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AbandonedDig5 rockmine", "blocker.dis", "-3592.12 2399.5 332.432", "0 0 0.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AbandonedDig6 rockmine", "bigblock.dis", "-3614.53 2393.1 311.317", "0 0 0.4");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AbandonedDig7 rockmine", "lrock5.3.dis", "-3589.63 2402.11 355.566", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AbandonedDig8 rockmine", "lrock6.3.dis", "-3589.29 2403.17 358.494", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AbandonedDig9 rockmine", "lrock1.3.dis", "-3594.03 2400.09 355.509", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AbandonedDig11 rockmine", "lrock2.3.dis", "-3589.78 2399.88 359.714", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "AbandonedDig12 rockmine", "lrock3.3.dis", "-3591.75 2400 358.193999999999", "0 0.499999 -2.99999");


	%zone = "Overville";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff1", "morosatown.dis", "-4915 -1202 717", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff2", "pstone_wall_m.dis", "-4900 -1184.5 700", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff3", "pstone_wall_m.dis", "-4900 -1191.5 700", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff4", "pstone_wall_m.dis", "-4896.5 -1188 700.01", "0 0 1.56");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff5", "pstone_wall_m.dis", "-4903.5 -1188 700.01", "0 0 1.56");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff6", "icem100f.dis", "-4864.98999999999 -1174 718", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff7", "icexs100f.dis", "-4839.02999999999 -1212 718", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff8", "icexs100f.dis", "-4851 -1212 718", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff9", "icexs100f.dis", "-4863 -1212 718", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff11", "icexs100f.dis", "-4875 -1212 718", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff12", "icexs100f.dis", "-4887 -1212 718", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff13", "icexs100f.dis", "-4899 -1212 718", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff14", "icexs100f.dis", "-4910.99999999999 -1212 718", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff15", "ices100f.dis", "-4913 -1190 718", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff16", "ices100f.dis", "-4913 -1166 718", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff17", "icexs100f.dis", "-4923 -1147.96999999999 717", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff18", "icexs100f.dis", "-4911 -1147.89999999999 717", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff19", "icexs100f.dis", "-4903 -1147.85999999999 717", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff21", "ices100f.dis", "-4875.59999999999 -1216.19999999999 701.995999999999", "1.56981 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "OvervilleStuff22", "icexs0f.dis", "-4907.12999999999 -1209.5 721.317999999999", "0 0.599999 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1", "mineshaft.dis", "-4900 -1188 685", "1.57 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d2", "mineshaft.dis", "-4900 -1188 665", "1.57 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d3", "mineshaft.dis", "-4900 -1188 645", "1.57 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d4", "mineshaft.dis", "-4900 -1188 625", "1.57 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d5", "mineshaft.dis", "-4900 -1188 605", "1.57 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d6", "mineshaft.dis", "-4900 -1188 585", "1.57 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d7", "mineshaft.dis", "-4900 -1188 565", "1.57 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d8", "mineshaft.dis", "-4900 -1188 545", "1.57 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d9", "mineshaft.dis", "-4900 -1188 525", "1.57 0 0");


	%zone = "Undercity";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "hellcave1", "rmrcavernspart2.dis", "-4840 -2273.5 3", "0 0 1.56");
		rpg::DefineManagedObject(%zone, "InteriorShape", "hellcave2", "rmrcavernspart5tester.dis", "-4745 -2327.1 23.1", "0 0 -1.55");
		rpg::DefineManagedObject(%zone, "InteriorShape", "hellcave3", "rmrcavernspart4.dis", "-4846.5 -1096 -79.8", "0 0 -1.56");
		rpg::DefineManagedObject(%zone, "InteriorShape", "hellcave4", "rmrcavernspart3.dis", "-4864.45 -1194 -125.7", "0 0 -1.56");
		rpg::DefineManagedObject(%zone, "InteriorShape", "MEHending", "rmrcavernspart6.dis", "-4917.2 -1098.5 -165.2", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "BottomSide", "rmrcavernspart1.dis", "-4906 -1172 -30", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "llava1", "llava.dis", "-4890 -1149 -20.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "llava2", "llava.dis", "-4920 -1149 -20.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "llava3", "llava.dis", "-4878 -1177 -20.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "llava4", "llava.dis", "-4925 -1185 -20.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "llava5", "llava.dis", "-4925 -1165 -20.49", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "llava6", "llava.dis", "-4878.5 -1207 -20.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "llava7", "slava.dis", "-4905 -1197 -20.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "roof1", "pstone_base_l.dis", "-4922 -1168 10.001", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "roof2", "pstone_base_l.dis", "-4892 -1158 10.002", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "roof3", "pstone_base_l.dis", "-4922 -1190 10.003", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "roof4", "pstone_base_l.dis", "-4881 -1182 10.004", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "roof5", "pstone_floor_l.dis", "-4900 -1177 -6.01", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "roof6", "pstone_floor_l.dis", "-4898 -1199 -6", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "roof7", "pstone_floor_m.dis", "-4907 -1189 -6.001", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "beamcap1", "pstone_beam_l.dis", "-4893.32 -1198.05 -20", "0 0 1.56");
		rpg::DefineManagedObject(%zone, "InteriorShape", "beamcap2", "pstone_beam_l.dis", "-4899.78 -1198.05 -20", "0 0 1.57");
		rpg::DefineManagedObject(%zone, "InteriorShape", "blockwall1", "pstone_base_m.dis", "-4896 -1208 -4", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "shafted1", "mineshaft.dis", "-4896.5 -1205 -26", "0.5 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1a", "mineshaft.dis", "-4896.5 -1233 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1b", "mineshaft.dis", "-4896.5 -1253 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1c", "mineshaft.dis", "-4896.5 -1273 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1d", "mineshaft.dis", "-4896.5 -1293 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1e", "mineshaft.dis", "-4896.5 -1313 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1f", "mineshaft.dis", "-4896.5 -1333 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1g", "mineshaft.dis", "-4896.5 -1353 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1h", "mineshaft.dis", "-4896.5 -1373 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1u", "mineshaft.dis", "-4896.5 -1393 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1j", "mineshaft.dis", "-4896.5 -1413 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1k", "mineshaft.dis", "-4896.5 -1433 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1l", "mineshaft.dis", "-4896.5 -1453 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1m", "mineshaft.dis", "-4896.5 -1473 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1o", "mineshaft.dis", "-4896.5 -1493 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1p", "mineshaft.dis", "-4896.5 -1513 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1q", "mineshaft.dis", "-4896.5 -1533 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1r", "mineshaft.dis", "-4896.5 -1553 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1s", "mineshaft.dis", "-4896.5 -1573 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1t", "mineshaft.dis", "-4896.5 -1593 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1u", "mineshaft.dis", "-4896.5 -1613 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1v", "mineshaft.dis", "-4896.5 -1633 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1w", "mineshaft.dis", "-4896.5 -1653 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1x", "mineshaft.dis", "-4896.5 -1673 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1y", "mineshaft.dis", "-4896.5 -1693 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d1z", "mineshaft.dis", "-4896.5 -1713 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d11", "mineshaft.dis", "-4896.5 -1733 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d12", "mineshaft.dis", "-4896.5 -1753 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d13", "mineshaft.dis", "-4896.5 -1773 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d14", "mineshaft.dis", "-4896.5 -1793 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d15", "mineshaft.dis", "-4896.5 -1813 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d16", "mineshaft.dis", "-4896.5 -1833 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d17", "mineshaft.dis", "-4896.5 -1853 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d18", "mineshaft.dis", "-4896.5 -1873 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d19", "mineshaft.dis", "-4896.5 -1893 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d10", "mineshaft.dis", "-4896.5 -1913 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "dd1", "mineshaft.dis", "-4896.5 -1933 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ee2", "mineshaft.dis", "-4896.5 -1953 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ff3", "mineshaft.dis", "-4896.5 -1973 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gg4", "mineshaft.dis", "-4896.5 -1993 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "hh5", "mineshaft.dis", "-4896.5 -2013 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ii6", "mineshaft.dis", "-4896.5 -2033 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "jj7", "mineshaft.dis", "-4896.5 -2053 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "kk8", "mineshaft.dis", "-4896.5 -2073 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "ll9", "mineshaft.dis", "-4896.5 -2093 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "oo0", "mineshaft.dis", "-4896.5 -2113 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "nn1", "mineshaft.dis", "-4896.5 -2133 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "bb2", "mineshaft.dis", "-4896.5 -2153 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "mm3", "mineshaft.dis", "-4896.5 -2173 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "aa4", "mineshaft.dis", "-4896.5 -2193 -33", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d2", "mineshaft.dis", "-4721 -2332 18.3", "0 0 1.56");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d3", "mineshaft.dis", "-4689 -2332.35 18.3", "0 0 1.56");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d4", "mineshaft.dis", "-4657 -2332.7 18.3", "0 0 1.56");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d5", "mineshaft.dis", "-4625 -2333.05 18.3", "0 0 1.56");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d6", "rmrcavernspart1btester.dis", "-4490 -2290 54.9", "0 0 1.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "morshaft1", "mineshaft.dis", "-4528 -2257.5 2.7", "0 0 2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "morshaft2", "mineshaft.dis", "-4596 -2326.8 18.31", "0 0 2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "morshaft3", "mineshaft.dis", "-4574 -2309 18.3", "0 0 2.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "morshaft4", "mineshaft.dis", "-4556 -2284.9 13", "0.35 0 2.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "morshaft6", "pstone_wstairs_m.dis", "-4543 -2267.5 -2.7", "0 0 4.06");
		rpg::DefineManagedObject(%zone, "InteriorShape", "morshaft5", "pstone_floor_l.dis", "-4545 -2270 -0.4", "0 0 0.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "morshaft10", "pstone_floor_l.dis", "-4548 -2268 5", "1.57 0 1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "morshaft11", "pstone_floor_l.dis", "-4545 -2274 5", "1.57 0 1.1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "morshaft12", "pstone_floor_l.dis", "-4548 -2268 8.5", "-0.2 0.5 0.4");


	%zone = "Graveyard";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "gyardbot1", "graveyardlit.dis", "-4460 -2300 71", "0 0 1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gyardtop1", "graveyardlit.dis", "-4460 -2300 83", "0 0 1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gyardbot2", "graveyardlit.dis", "-4550 -2320 71", "0 0 1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gyardtop2", "graveyardlit.dis", "-4550 -2320 83", "0 0 1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gyardbot3", "graveyardlit.dis", "-4479 -2252 70", "0 0 3.3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gyardtop3", "graveyardlit.dis", "-4479 -2252 82", "0 0 3.3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gystairup1", "pstone_tstairs_l.dis", "-4494 -2338 63.4", "0 0 2.5705");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gystairup2", "pstone_tstairs_l.dis", "-4404 -2317 63.4", "0 0 2.5705");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gystairup3", "pstone_tstairs_l.dis", "-4503 -2198.5 62.35", "0 0 -1.4");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gywalnokhlp", "pstone_roof_l.dis", "-4513.78 -2308.41 75.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gywalkhlp1", "pstone_roof_l.dis", "-4493 -2258 74.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gywalkhlp2", "pstone_base_s.dis", "-4466.5 -2270 70", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gywalkhlp3", "pstone_tstairs_s.dis", "-4474.5 -2271 65", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gywalkhlp4", "pstone_beam_l.dis", "-4481.75 -2268.64 71.0436", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gywalkhlp5", "pstone_beam_l.dis", "-4493.82 -2270.57 71.6722", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gywalkhlp6", "pstone_tstairs_s.dis", "-4506 -2283 75", "0 -0.1 3.07");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gywalkhlp7", "pstone_walk_m.dis", "-4518 -2280 79", "0 0 -0.5");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gywalkhlp8", "pstone_beam_m.dis", "-4520 -2280 82", "0 0.7 0.1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gywalkhlp9", "pstone_floor_s.dis", "-4459.5 -2267 79", "0 0 -0.3");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gywalkhlp10", "pstone_floor_m.dis", "-4476.4 -2318 68", "0 -0.9 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "gywalkhlp11", "pstone_floor_m.dis", "-4469 -2321 76.5", "0 -0.55 -0.1");
		rpg::DefineManagedObject(%zone, "InteriorShape", "tolavatomb", "pstone_tstairs_s.dis", "-4896.5 -1195.5 -24.3", "0 -0.1 1.5708");
		rpg::DefineManagedObject(%zone, "StaticShape", "dead1", "DTS_bark", "-4144.86 -2224.5 170", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "dead2", "DTS_bark", "-4057.83 -2512.65 189", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "dead2", "DTS_bark", "-4338.64 -2491.21 94", "0 0 0");


	%zone = "Wyzanhyde Priory";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "thepriory", "lichlair.dis", "-3739 1010 271", "0 0 3.2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "thewall", "bbridge.dis", "-3745 1042 257", "0.1 0 -0.05");
		rpg::DefineManagedObject(%zone, "InteriorShape", "watchtower1", "watcher.dis", "-3604.25 1734.58 328.685", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "watchtower2", "watcher.dis", "-3604.92 1785.62 328.82", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "maintower1", "towerf.dis", "-3750 1310 360.8", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "maintower1br1", "bbridge.dis", "-3886.54 1350.51 287.278", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "maintower2", "magetower.dis", "-3760 1500 350", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "maintower2flr", "pstone_base_m.dis", "-3753.5 1493.5 343.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "maintower2br1", "bbridge.dis", "-3752 1416.2 323.5", "0 -0.1 1.57");
		rpg::DefineManagedObject(%zone, "InteriorShape", "portal2Ethren", "remorterhall.dis", "-3919.25 1345.5 308.258", "0 -0 1.56999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wybnkd1", "desk.dis", "-3682.25 952.715 271", "0 0 4.76");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d2", "woodcrate.dis", "-3684.24 981.223 271", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d3", "woodcrate.dis", "-3803.63 971.227 271", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d4", "woodcrate.dis", "-3803.66 971.299 273", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d5", "woodcrate.dis", "-3803.65 971.186 275", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "d6", "woodcrate.dis", "-3803.73 971.15 277", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "m1", "desk.dis", "-3802.65 933.667 271", "0 0 3.14");
		rpg::DefineManagedObject(%zone, "InteriorShape", "m2", "desk.dis", "-3787.9 933.257 271", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "t1", "bigtable1.dis", "-3794.87 932.805 271", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "w1", "woodcrateb.dis", "-3807.1 929.958 271", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "w2", "woodcrateb.dis", "-3783.62 931.186 271", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "f1", "GiantFlame", "-3618.97 1727.78 343.684", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "f2", "GiantFlame", "-3619.89 1778.64 343.82", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "b1", "bookshelfl.dis", "-3730.88999999999 1003.43999999999 271", "0 -0 0.0499999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "b2", "chest.dis", "-3689.25 954.551999999999 278", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "b3", "cabinet2.dis", "-3678.25 947.758 271", "0 0 0.05");
		rpg::DefineManagedObject(%zone, "InteriorShape", "b4", "pstone_rpillar_s.dis", "-3757.58 946.578 271", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "b5", "pstone_rpillar_s.dis", "-3761.6 998.775 271", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "b6", "pstone_rpillar_s.dis", "-3703.7 976.942 271", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "b7", "pstone_rpillar_s.dis", "-3720.47 976.026 271", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "b8", "pstone_rpillar_s.dis", "-3727.28 949.22 271", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "b9", "sign.dis", "-3763.24 970.517 276.601", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "b10", "sign.dis", "-3802.33 928.163 275.75", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "b11", "sign.dis", "-3788.9 928.948 276.041", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "in1", "jfnt.dis", "-3745.85 989.491 271", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "in2", "MediumFlame", "-3745.79 989.536 276.5", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "in3", "SpellFXdomesmall", "-3745.8 989.5 277", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "n4 rockmine", "lrock1.2.dis", "-3736.51 991.865 271", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "in5", "DTS_rockingchair", "-3752.35 991.832 271", "0 0 0");


	%zone = "Jherigo Pass";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "grhouse1", "neobridge1.dis", "5.71286999999999 307.400999999999 26.6344999999999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "grhouse2", "neobridge1.dis", "50.4075 262.383 30.9351", "0 0 -2");
		rpg::DefineManagedObject(%zone, "InteriorShape", "grhouse3", "neobridge1.dis", "16.6469999999999 335.349999999999 35.6497999999999", "0 -0 1.39999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "grhouse4", "neobridge1.dis", "41.0475999999999 317.875 44.9116999999999", "0 -0 0.299999");
		rpg::DefineManagedObject(%zone, "InteriorShape", "support1", "pstone_cube_l.dis", "16.3583 329.173 6.89376", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "support2", "pstone_base_m.dis", "39.6839999999999 311.079999999999 18.1444999999999", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wallwalk1", "pwood_floor_l.dis", "246.536 -12.6989 222.325", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wwlk2", "pwood_floor_l.dis", "63.5883 386.918 127.48", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wwlk3", "pwood_floor_l.dis", "84.2006 360.478 150.204", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wwlk4", "pwood_floor_l.dis", "51.4545 447.091 155.922", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wwlk5", "pwood_floor_l.dis", "64.4721 404.726 163.622", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wwlk6", "pwood_floor_l.dis", "81.5917 384.984 200.771", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wwlk7", "pwood_floor_l.dis", "99.0229 357.725 214.637", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wwlk8", "pwood_floor_l.dis", "89.9785 334.3 127.127", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wwlk9", "pwood_floor_l.dis", "-77.4194 320.242 112.196", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wlkw10", "pwood_floor_l.dis", "-99.5356 372.801 136.608", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wlkw11", "pwood_floor_l.dis", "-70.6787 265.674 160.791", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wlkw12", "pwood_floor_l.dis", "-101.706 333.98 183.14", "0 0 0");
		rpg::DefineManagedObject(%zone, "InteriorShape", "wlkw13", "pwood_floor_l.dis", "-92.4321 286.452 220.883", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "BigTree tree", "PhantomStrangerTree3", "-44.5971 447.909 0", "0 0 3");
		rpg::DefineManagedObject(%zone, "StaticShape", "BigTree tree", "PhantomStrangerTree3", "122.291 205.672 0", "0 0 1");
		rpg::DefineManagedObject(%zone, "StaticShape", "BigTree tree", "PhantomStrangerTree3", "-81.2646 576.143 0", "0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "BigTree tree", "PhantomStrangerTree3", "-6.11376 351.715 0", "0 0 2");
		rpg::DefineManagedObject(%zone, "StaticShape", "MidTree tree", "PhantomStrangerTree2", "73.2758 166.35 16","0 0 3");
		rpg::DefineManagedObject(%zone, "StaticShape", "MidTree tree", "PhantomStrangerTree2", "-10.0327 254.351 30","0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "MidTree tree", "PhantomStrangerTree2", "-46.1694 382.457 15","0 0 2");
		rpg::DefineManagedObject(%zone, "StaticShape", "MidTree tree", "PhantomStrangerTree2", "13.4541 272.596 5","0 0 0");
		rpg::DefineManagedObject(%zone, "StaticShape", "MidTree tree", "PhantomStrangerTree2", "-50.8964 519.795 10","0 0 1");



	%zone = "Cravv Keep";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "cravkeep1", "cthh.dis", "-1542.75 1736.25 330.937", "0 -0 1");


	%zone = "Restaurant at the End of the World";
	%zone = rpg::CreateManagedObjectList(%zone);
		rpg::DefineManagedObject(%zone, "InteriorShape", "heavenbar1", "rmr7thheaven.dis", "510 -2350 350", "0 0 -2");
}

if($ListOfZoneNames == "")
	rpg::DefineManagedZoneObjects();

// instant InteriorShape "(.*?)".*fileName.*?"(.*?)".*position.*?"(.*?)".*rotation.*?"(.*?)".*
// rpg::DefineManagedObject\(%zone, "InteriorShape", "\1", "\2", "\3", "\4"\);



//	instant StaticShape "(.*?)".*dataB.*?"(.*?)".*position.*?"(.*?)".*rotation.*?"(.*?)".*
//	rpg::DefineManagedObject\(%zone, "StaticShape", "\1", "\2", "\3", "\4"\);


