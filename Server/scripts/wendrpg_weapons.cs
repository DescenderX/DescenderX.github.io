//____________________________________________________________________________________________________
// DescX Notes:
//
//		All weapon-specific data has been moved to their respective wp<x>.cs files.
//		Only the most general weapon code lives here.

$fireTimeDelay = 0.1;

$RangeTable[$AxeAccessoryType] = 3;
$RangeTable[$SwordAccessoryType] = 3;
$RangeTable[$PolearmAccessoryType] = 4;
$RangeTable[$BludgeonAccessoryType] = 3;

$DelayFactorTable[$RingAccessoryType] = "0.0";
$DelayFactorTable[$BodyAccessoryType] = "0.0";
$DelayFactorTable[$BootsAccessoryType] = "0.0";
$DelayFactorTable[$BackAccessoryType] = "0.0";
$DelayFactorTable[$ShieldAccessoryType] = "0.0";
$DelayFactorTable[$TalismanAccessoryType] = "0.0";
$DelayFactorTable[$AxeAccessoryType] = "1.0";
$DelayFactorTable[$SwordAccessoryType] = "1.0";
$DelayFactorTable[$PolearmAccessoryType] = "1.0";
$DelayFactorTable[$BludgeonAccessoryType] = "1.0";
$DelayFactorTable[$RangedAccessoryType] = "1.0";
$DelayFactorTable[$ProjectileAccessoryType] = "1.0";

$CostFactorTable[$RingAccessoryType] = "1.0";
$CostFactorTable[$BodyAccessoryType] = "1.0";
$CostFactorTable[$BootsAccessoryType] = "1.0";
$CostFactorTable[$BackAccessoryType] = "1.0";
$CostFactorTable[$ShieldAccessoryType] = "1.0";
$CostFactorTable[$TalismanAccessoryType] = "1.0";
$CostFactorTable[$SwordAccessoryType] = "1.0";
$CostFactorTable[$AxeAccessoryType] = "1.0";
$CostFactorTable[$PolearmAccessoryType] = "1.0";
$CostFactorTable[$BludgeonAccessoryType] = "1.0";
$CostFactorTable[$RangedAccessoryType] = "1.0";
$CostFactorTable[$ProjectileAccessoryType] = "0.01";


function GetRange(%weapon) {
	dbecho($dbechoMode, "GetRange(" @ %weapon @ ")");
	%minRange = 2.0;
	if($WeaponRange[%weapon] != "")		return %minRange + $WeaponRange[%weapon];
	else								return %minRange + $RangeTable[$AccessoryVar[%weapon, $AccessoryType]];
}

function GetDelay(%weapon) {
	dbecho($dbechoMode, "GetDelay(" @ %weapon @ ")");
	if($WeaponDelay[%weapon] != "") 
		return $WeaponDelay[%weapon];	
	%a = 3.0;
	%min = 0.2;		
	%b = Cap($AccessoryVar[%weapon, $Weight] / %a, %min, "inf");		
	%c = %b * $DelayFactorTable[$AccessoryVar[%weapon, $AccessoryType]];
	return %c;
}

$WeaponListCount = 0;
function rpg::DefineWeaponType(%name, %skillType, %accessoryType, %specialvars, %weight, %info, %delay) {	
	if(%accessoryType != $ProjectileAccessoryType) { // Add to weapon cycle table
		%index = 0;
		if($WeaponIndex[%name] == "") {
			$WeaponListCount++;
			%index = $WeaponListCount;
		} else {
			%index = $WeaponIndex[%name];
		}
		
		$WeaponNamesByIndex[%index] 			= %name;
		$WeaponIndex[%name] 					= %index;
		
		%prev = 1;
		if($WeaponListCount > 1)
			%prev = $WeaponListCount - 1;
		
		
		$NextWeapon[%name] 						= $WeaponNamesByIndex[1];
		$PrevWeapon[%name] 						= $WeaponNamesByIndex[%prev];
			
		$NextWeapon[$WeaponNamesByIndex[%prev]] = %name;
		$PrevWeapon[$WeaponNamesByIndex[1]] 	= %name;
	}

	$SkillType[%name] 						= %skillType;
	$AccessoryVar[%name, $AccessoryType] 	= %accessoryType;
	$AccessoryVar[%name, $SpecialVar] 		= %specialvars;	
	if(%weight != "")
		$AccessoryVar[%name, $Weight] 		= %weight;
	if(%info != "")
		$AccessoryVar[%name, $MiscInfo] 	= %info;	
	
	if(%accessoryType != $ProjectileAccessoryType) {
		if(!%delay || %delay == "" || %delay < 0)
			$WeaponDelay[%name] 			= GetDelay(%name);
		else
			$WeaponDelay[%name] 			= %delay;
	}
	
	if($HardcodedItemCost[%name] == "")
		$ItemCost[%name] 					= GenerateItemCost(%name);
	else
		$ItemCost[%name] 					= $HardcodedItemCost[%name];
}


//____________________________________________________________________________________________________
// Melee
//____________________________________________________________________________________________________
function MeleeAttackBase(%player, %length, %weapon, %force) {
	dbecho($dbechoMode, "MeleeAttack(" @ %player @ ", " @ %length @ ")");

	%clientId = Player::getClient(%player);
	if(fetchData(%clientId, "invisible") == 2) {
		return;
	}
	
	if(%clientId == "")
		%clientId = 0;
	if(%clientId.sleepMode > 0)		return;

	if($WeaponDelay[%weapon] != ""){		if($justmeleed[%clientId] && %force == 0) {			return;
		}	}	else $WeaponDelay[%weapon] = GetDelay(%weapon);
	
	if(%force == 0) {		$justmeleed[%clientId] = True;
	}
	
	if(%force == 0) {
		%repeats = AddBonusStatePoints(%clientId, "ASU");
		%stagger = $WeaponDelay[%weapon] / (%repeats + 1);
		for(%x = %repeats; %x > 0; %x--) {
			schedule("MeleeAttackBase(" @ %player @ ", " @ %length @ ", " @ %weapon @ ", 1);",  %x * %stagger );
		}		schedule("$justmeleed["@%clientId@"]=\"\";",   ($WeaponDelay[%weapon] - 0.11)   );
	}

	$los::object = "";
	if(GameBase::getLOSinfo(%player, %length))
	{
		%target = $los::object;
		%obj = getObjectType(%target);
		if(%obj == "Player")
		{
			GameBase::virtual($los::object, "onDamage", "", "", "0 0 0", "0 0 0", "0 0 0", "torso", "front_right", %clientId, %weapon);
		}
		
		if(%force == 1) {
			%bomb = newObject("", "Mine", "SpellFXSpark");
			addToSet("MissionCleanup", %bomb);
			GameBase::Throw(%bomb, %player, 0, false);
			%efxpos = GameBase::getPosition(%target);
			GameBase::setPosition(%bomb, %efxpos);
			playSound(%weapon.imageType.sfxFire, %efxpos);
		}
	}

	PostAttack(%clientId, %weapon);
}
function MeleeAttack(%player, %length, %weapon) {
	%clientId = Player::getClient(%player);
	if(fetchData(%clientId, "invisible") == 2) {
		Client::sendMessage(%clientId, $MsgRed, "You cannot attack while Shadow Walking. #shadowwalk again to unhide.");
		return;
	}
	MeleeAttackBase(%player,%length,%weapon,0);
}

//____________________________________________________________________________________________________
// Pick axe mining and melee
//____________________________________________________________________________________________________
function PickAxeSwingBase(%player, %length, %weapon, %forced) {
	dbecho($dbechoMode, "PickAxeSwing(" @ %player @ ", " @ %length @ ")");

	%clientId = Player::getClient(%player);
	
	if(fetchData(%clientId, "invisible") == 2) {
		Client::sendMessage(%clientId, $MsgRed, "You cannot attack or mine while Shadow Walking. #shadowwalk again to unhide.");
		return;
	}
	
	if(%clientId == "")
		%clientId = 0;

	//==== ANTI-SPAM CHECK, CAUSE FOR SPAM UNKNOWN ==========
	//%time = getIntegerTime(true) >> 5;
	//if(%time - %clientId.lastFireTime <= $fireTimeDelay)
	//	return;
	//%clientId.lastFireTime = %time;
	//=======================================================
	if($WeaponDelay[%weapon] != ""){		if($justmeleed[%clientId] && !%forced)			return;	}	else		$WeaponDelay[%weapon] = GetDelay(%weapon);	$justmeleed[%clientId] = True;	schedule("$justmeleed["@%clientId@"]=\"\";",$WeaponDelay[%weapon]-0.11);

	$los::object = "";
	if(GameBase::getLOSinfo(%player, %length))
	{
		%target = $los::object;
		%obj = getObjectType(%target);
		%type = GameBase::getDataName(%target);
		%oname = object::getname(%target);
		%isCrystal 		= (%type == "Crystal" || %type == "Crystals2" || %type == "Crystals3" || %type == "Crystals4" || %type == "Crystals5");
		%isGenericRock 	= (!%isCrystal && (Getword(%oname, 1) == "rockmine"));
		
		if(!%forced && (%isGenericRock || %isCrystal))
		{
			%sound = SoundHitore2;
			if(Vector::getDistance(%clientId.lastMinePos, GameBase::getPosition(%clientId)) > 1.0) {
				%repeats = AddBonusStatePoints(%clientId, "ASU") + 1;
				for(%x = %repeats; %x > 0; %x--) {
					%score = DoRandomMining(%clientId, %target, %isGenericRock);
					%sound = SoundHitore;
					if(%score != "") {
						givethisstuff(%clientId, %score@" 1");
						RefreshAll(%clientId);
						%desc = %score.description;
						if(%desc == False){
							%desc = $beltitem[%score, "Name"];
							%beltTag = " [" @ $BeltItemDescription[$beltitem[%score, "Type"]] @ ", have "@belt::hasthisstuff(%clientId, %score)@"]";
						}
						if(%repeats > 1 && %x < %repeats)
							Client::sendMessage(%clientId, 0, "Your echo unearthed " @ %desc @ "." @ %beltTag);
						else Client::sendMessage(%clientId, 0, "You found " @ %desc @ "." @ %beltTag);
						
						if( floor(getRandom() * 10) == 5)
							%clientId.lastMinePos = GameBase::getPosition(%clientId);
					}
					UseSkill(%clientId, $SkillMining, True, True);
				}
			}
			
			%vec = Vector::add(GameBase::getPosition(%clientId),
									Vector::scale(Vector::normalize(Vector::sub(GameBase::getPosition(%target), GameBase::getPosition(%clientId))), 0.5));
			playSound(%sound, %vec);
		}
		else {
			if(%forced) {
				%bomb = newObject("", "Mine", "SpellFXSpark");
				addToSet("MissionCleanup", %bomb);
				GameBase::Throw(%bomb, %player, 0, false);
				%efxpos = GameBase::getPosition(%target);
				GameBase::setPosition(%bomb, %efxpos);
				playSound(%weapon.imageType.sfxFire, %efxpos);
			}
			else if(%obj == "Player") {
				GameBase::virtual(%target, "onDamage", "", "", "0 0 0", "0 0 0", "0 0 0", "torso", "front_right", %clientId, %weapon);
			}
		}
	}
	
	if(!%forced) {
		%repeats = AddBonusStatePoints(%clientId, "ASU");
		
		if(%repeats > 0) {
			%stagger = $WeaponDelay[%weapon] / (%repeats + 1);
			for(%x = %repeats; %x > 0; %x--) {
				schedule("PickAxeSwingBase(" @ %player @ ", " @ %length @ ", " @ %weapon @ ", True);",  %x * %stagger, %clientId );
			}
			schedule("$justmeleed["@%clientId@"]=\"\";",   ($WeaponDelay[%weapon] - 0.11)   );
		}
	}

	PostAttack(%clientId, %weapon);
}
function PickAxeSwing(%player, %length, %weapon) {
	PickAxeSwingBase(%player, %length, %weapon, False);
}


//____________________________________________________________________________________________________
// Hatchet attack and wood chopping
//____________________________________________________________________________________________________
function HatchetSwingBase(%player, %length, %weapon, %forced) {
	dbecho($dbechoMode, "HatchetSwing(" @ %player @ ", " @ %length @ ")");

	%clientId = Player::getClient(%player);
	
	if(fetchData(%clientId, "invisible") == 2) {
		Client::sendMessage(%clientId, $MsgRed, "You cannot attack or mine while Shadow Walking. #shadowwalk again to unhide.");
		return;
	}
	
	if(%clientId == "")
		%clientId = 0;

	//==== ANTI-SPAM CHECK, CAUSE FOR SPAM UNKNOWN ==========
	//%time = getIntegerTime(true) >> 5;
	//if(%time - %clientId.lastFireTime <= $fireTimeDelay)
	//	return;
	//%clientId.lastFireTime = %time;
	//=======================================================
	if($WeaponDelay[%weapon] != ""){
		if($justmeleed[%clientId] && !%forced)
			return;
	}
	else
		$WeaponDelay[%weapon] = GetDelay(%weapon);
	$justmeleed[%clientId] = True;
	schedule("$justmeleed["@%clientId@"]=\"\";",$WeaponDelay[%weapon]-0.11);

	$los::object = "";
	if(GameBase::getLOSinfo(%player, %length))
	{
		
		%target = $los::object;
		%obj = getObjectType(%target);
		%type = GameBase::getDataName(%target);
		%oname = object::getname(%target);
		%isTree = (Getword(%oname, 1) == "tree");
		%noisePos = $los::position;
		if(%isTree || (!%forced && (%type == "Walktree9" || %type == "TreeShape" || %type == "TreeShapeTwo" || 
				%type == "PhantomStrangerTree1" || %type == "PhantomStrangerTree2" || %type == "PhantomStrangerTree3")))
		{
		
			if(Vector::getDistance(%clientId.lastMinePos, GameBase::getPosition(%clientId)) > 1.0)
			{
				playSound(SoundArrowHit, %noisePos);	//vectrex, modified by JI

				%repeats = AddBonusStatePoints(%clientId, "ASU") + 1;
				
				for(%x = %repeats; %x > 0; %x--) {
					%lastscore = "";
					for(%i = 1; $ItemList[Survival, %i] != ""; %i++) {
						%w1 = GetWord($ItemList[Survival, %i], 1);
						%n = Cap( (%w1 * getRandom()) + (%w1 / 2), 0, %w1);
						%r = 1 + (GetSkillWithBonus(%clientId, $SkillSurvival) * (1/10)) * getRandom();
						if(%n > %r)
							break;
						%lastscore = GetWord($ItemList[Survival, %i], 0);
					}
					%score = %lastscore;
					if(%score != "")
					{
						givethisstuff(%clientId, %score@" 1");
						RefreshAll(%clientId);
						%desc = %score.description;
						if(%desc == False){
							%desc = $beltitem[%score, "Name"];
							%beltTag = " [" @ $BeltItemDescription[$beltitem[%score, "Type"]] @ ", have "@belt::hasthisstuff(%clientId, %score)@"]";
						}
						if(%repeats > 1 && %x < %repeats) {
							Client::sendMessage(%clientId, 0, "Your echo cuts a " @ %desc @ "." @ %beltTag);
						} else {
							Client::sendMessage(%clientId, 0, "You cut a " @ %desc @ "." @ %beltTag);
						}
						if( floor(getRandom() * 10) == 5)
							%clientId.lastMinePos = GameBase::getPosition(%clientId);
					} else Client::sendMessage(%clientId, $MsgRed, "You hack at the tree, but your Survival skill isn't high enough to cut anything useful.");
					UseSkill(%clientId, $SkillSurvival, True, True);
				}
			}
			else
				playSound(SoundHitore2, %noisePos);
		}
		
		else {
			if(%forced) {
				%bomb = newObject("", "Mine", "SpellFXSpark");
				addToSet("MissionCleanup", %bomb);
				GameBase::Throw(%bomb, %player, 0, false);
				%efxpos = GameBase::getPosition(%target);
				GameBase::setPosition(%bomb, %efxpos);
				playSound(%weapon.imageType.sfxFire, %efxpos);
			}
			else if(%obj == "Player") {
				GameBase::virtual(%target, "onDamage", "", "", "0 0 0", "0 0 0", "0 0 0", "torso", "front_right", %clientId, %weapon);
			}
		}
	}
	
	if(!%forced) {
		%repeats = AddBonusStatePoints(%clientId, "ASU");
		
		if(%repeats > 0) {
			%stagger = $WeaponDelay[%weapon] / (%repeats + 1);
			for(%x = %repeats; %x > 0; %x--) {
				schedule("HatchetSwingBase(" @ %player @ ", " @ %length @ ", " @ %weapon @ ", True);",  %x * %stagger, %clientId );
			}
			schedule("$justmeleed["@%clientId@"]=\"\";",   ($WeaponDelay[%weapon] - 0.11)   );
		}
	}

	PostAttack(%clientId, %weapon);
}
function HatchetSwing(%player, %length, %weapon) {
	HatchetSwingBase(%player, %length, %weapon, False);
}
//____________________________________________________________________________________________________
function PostAttack(%clientId, %weapon) {
	dbecho($dbechoMode, "PostAttack(" @ %clientId @ ", " @ %weapon @ ")");

	if($postAttackGraphBar)
	{
		%t = GetDelay(%weapon);
		%ticks = 30;
		%chunks = 10;

		%chunklen = floor(%ticks / %chunks);
		%d = %t / %chunks;

		for(%i = 0; %i <= %chunks; %i++)
			schedule("bottomprint(" @ %clientId @ ", \" \" @ String::create(\"*\", " @ %ticks @ " - (" @ %chunklen @ " * " @ %i @ ")) @ \"\", " @ %d @ " + 0.25);", %d * %i);
	}

}

//____________________________________________________________________________________________________
function DoRandomMining(%clientId, %crystal, %isGenericRock) {
	dbecho($dbechoMode, "DoRandomMining(" @ %clientId @ ", " @ %crystal @ ")");
	%found = "";
	if(%isGenericRock)	%oreOffset = 3;
	else 				%oreOffset = 0;
	for(%ore = 1; $ItemList[Mining, %ore] != ""; %ore++) {		
		%oreName 	= GetWord($ItemList[Mining, %ore], 0);
		%cost		= $HardcodedItemCost[GetWord($ItemList[Mining, Cap(%ore + %oreOffset,1,19)], 0)];
		%bonus 		= %crystal.bonus[%oreName];
		if(%isGenericRock)
			
		
		%oreRoll 	= Cap(%ore * (%cost - %bonus) * getRandom(), %ore, "inf");
		%miningRoll = Cap(GetSkillWithBonus(%clientId, $SkillMining) * getRandom(), 5, 2000);
		if(%oreRoll < %miningRoll || %found == "") %found = %oreName;
	}
	return %found;
}
