// FORMAT in .MIS files:		instant Marker ".5 1 2 20 50 17"
//	0 = max spawn at this location
//	1 = radius min
//	2 = radius max
//	3 = delay min
//	4 = delay max
//	5...  = index of the bot in EnemyArmor.cs
function InitSpawnPoints()
{
	dbecho($dbechoMode, "InitSpawnPoints()");

	%group = nameToID("MissionGroup\\SpawnPoints");

	if(%group != -1)
	{
		echo("Found " @ Group::objectCount(%group) @ " spawn points to load");
		for(%i = 0; %i <= Group::objectCount(%group)-1; %i++)
		{
		      %this = Group::getObject(%group, %i);
			%info = Object::getName(%this);

			$MarkerZone[%this] = ObjectInWhichZone(%this);

			if(%info != "")
			{
				$numAIperSpawnPoint[%this] = 0;
				%indexes = "";

				for(%z = 5; GetWord(%info, %z) != -1; %z++)
					%indexes = %indexes @ GetWord(%info, %z) @ " ";

				%numSpawnPoints++;
				
				%mindelay = GetWord(%info, 3);
				%maxdelay = GetWord(%info, 4);
				%diff = %maxdelay - %mindelay;
				%delay = floor(getRandom() * %diff) + %mindelay;
				schedule("SpawnLoop(" @ %this @ ");", %delay);		// make sure first spawn respects delays				
				//SpawnLoop(%this);
			}
		}
		echo(%numSpawnPoints@" spawn points initialized.");
	}
}

function SpawnLoop(%this)
{
	dbecho($dbechoMode, "SpawnLoop(" @ %this @ ")");

	%info = Object::getName(%this);

	%mindelay = GetWord(%info, 3);
	%maxdelay = GetWord(%info, 4);
	%diff = %maxdelay - %mindelay;
	%delay = floor(getRandom() * %diff) + %mindelay;

	%indexes = "";
	for(%i = 5; GetWord(%info, %i) != -1; %i++)
		%indexes = %indexes @ GetWord(%info, %i) @ " ";

	%flag = "";
	if($SelectiveZoneBotSpawning) {
		%players = Zone::getNumPlayers($MarkerZone[%this]);
		if(%players > 0 || $MarkerZone[%this] == "")
			%flag = True;
		else if (%players == 0) {
			for(%x=0;(%b=getword($AISpawnedInZone[$MarkerZone[%this]],%x)) != -1; %x++) {
				if(!IsDead(%b)){
					if($AIFromZone[%b] = $MarkerZone[%this]) {
						$AIFromZone[%b] = "";
						%AIname = fetchData(%b, "BotInfoAiName");
						storeData(%b, "BotCleanupNoEXP", 1);
						AI::setVar(%AIname, SpotDist, 0);
						AI::setVar(%aiName, attackMode, 0);
						AI::newDirectiveRemove(%AIname, 99);
						Player::Kill(%b);
					}
				}
			}
			$AISpawnedInZone[$MarkerZone[%this]] = "";
		}
	} else %flag = True;

	%r = floor(getRandom() * (%i-5));
	%index = GetWord(%indexes, %r);
	
	%spawnThis = $spawnIndex[%index];
	if((%spawnThis == "" || %spawnThis <= 0) && %index != "") {		// Allow for names or indicies
		%spawnThis = $BotEquipment[%index];
		if(%spawnThis != "") %spawnThis = %index;
		else %flag = False;
	}

	for(%h="";%h<=5;%h++){
		for(%x=0;%x<$TheHuntedMax[%h];%x++) {
			if ($TheHuntedSpawn[%h, %x] == %info) {
				%spawnThis = $TheHunted[%h, %x];
				break;
			}
		}
	}

	%maxs = Cap(round(GetWord(%info, 0) * $spawnMultiplier), 1, "inf");
	if(%flag && $numAIperSpawnPoint[%this] < %maxs) {
		%AIname = AI::helper(%spawnThis, %spawnThis, "SpawnPoint " @ %this);
		%aiId = AI::getId(%AIname);
		$AIFromZone[%aiId] = $MarkerZone[%this];
		$AISpawnedInZone[$MarkerZone[%this]] = %aiId @ " " @ $AISpawnedInZone[$MarkerZone[%this]];
		schedule("SpawnLoop(" @ %this @ ");", %delay);
	} else {
		schedule("SpawnLoop(" @ %this @ ");", %delay * 2);	// slow it down if we're maxed out or nobody is in the zone
	}
}
