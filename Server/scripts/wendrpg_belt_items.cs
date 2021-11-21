//____________________________________________________________________________________________________________________________________
// DescX Notes:
//		Just items
//____________________________________________________________________________________________________________________________________
// Ore
BeltItem::Add("Small Rock","SmallRock","AmmoItems",0.2,1);
BeltItem::Add("Quartz","Quartz","AmmoItems",0.2,GenerateItemCost(Quartz), $ItemDropIgnoreLuck);
BeltItem::Add("Granite","Granite","AmmoItems",0.2,GenerateItemCost(Granite), $ItemDropIgnoreLuck);
BeltItem::Add("Opal","Opal","AmmoItems",0.2,GenerateItemCost(Opal), $ItemDropIgnoreLuck);
BeltItem::Add("Jade","Jade","AmmoItems",0.25,GenerateItemCost(Jade), $ItemDropIgnoreLuck);
BeltItem::Add("Turquoise","Turquoise","AmmoItems",0.3,GenerateItemCost(Turquoise), $ItemDropIgnoreLuck);
BeltItem::Add("Ruby","Ruby","AmmoItems",0.3,GenerateItemCost(Ruby), $ItemDropIgnoreLuck);
BeltItem::Add("Topaz","Topaz","AmmoItems",0.3,GenerateItemCost(Topaz), $ItemDropIgnoreLuck);
BeltItem::Add("Sapphire","Sapphire","AmmoItems",0.3,GenerateItemCost(Sapphire), $ItemDropIgnoreLuck);
BeltItem::Add("Gold","Gold","AmmoItems",0.3,GenerateItemCost(Gold), $ItemDropIgnoreLuck);
BeltItem::Add("Emerald","Emerald","AmmoItems",0.2,GenerateItemCost(Emerald), $ItemDropIgnoreLuck);
BeltItem::Add("Diamond","Diamond","AmmoItems",0.1,GenerateItemCost(Diamond), $ItemDropIgnoreLuck);
BeltItem::Add("Keldrinite","Keldrinite","AmmoItems",0.3,GenerateItemCost(Keldrinite), $ItemDropIgnoreLuck);
BeltItem::Add("Platinum","Platinum","AmmoItems",0.2,GenerateItemCost(Platinum), $ItemDropIgnoreLuck);
BeltItem::Add("Radium","Radium","AmmoItems",0.2,GenerateItemCost(Radium), $ItemDropIgnoreLuck);
BeltItem::Add("Silver","Silver","AmmoItems",0.2,GenerateItemCost(Silver), $ItemDropIgnoreLuck);
BeltItem::Add("Aluminum","Aluminum","AmmoItems",0.2,GenerateItemCost(Aluminum), $ItemDropIgnoreLuck);
BeltItem::Add("Copper","Copper","AmmoItems",0.2,GenerateItemCost(Copper), $ItemDropIgnoreLuck);
BeltItem::Add("Iron","Iron","AmmoItems",0.2,GenerateItemCost(Iron), $ItemDropIgnoreLuck);
//____________________________________________________________________________________________________________________________________
// Crushing ore into MagicDust
Smith::addItem("CrushedGranite","Granite 1","MagicDust " @ floor($HardcodedItemCost[Granite] / 100));
Smith::addItem("CrushedCopper","Copper 1","MagicDust " @ floor($HardcodedItemCost[Copper] / 100));
Smith::addItem("CrushedQuartz","Quartz 1","MagicDust " @ floor($HardcodedItemCost[Quartz] / 100));
Smith::addItem("CrushedOpal","Opal 1","MagicDust " @ floor($HardcodedItemCost[Opal] / 100));
Smith::addItem("CrushedIron","Iron 1","MagicDust " @ floor($HardcodedItemCost[Iron] / 100));
Smith::addItem("CrushedJade","Jade 1","MagicDust " @ floor($HardcodedItemCost[Jade] / 100));
Smith::addItem("CrushedAluminum","Aluminum 1","MagicDust " @ floor($HardcodedItemCost[Aluminum] / 100));
Smith::addItem("CrushedTurquoise","Turquoise 1","MagicDust " @ floor($HardcodedItemCost[Turquoise] / 100));
Smith::addItem("CrushedRuby","Ruby 1","MagicDust " @ floor($HardcodedItemCost[Ruby] / 100));
Smith::addItem("CrushedTopaz","Topaz 1","MagicDust " @ floor($HardcodedItemCost[Topaz] / 100));
Smith::addItem("CrushedSapphire","Sapphire 1","MagicDust " @ floor($HardcodedItemCost[Sapphire] / 100));
Smith::addItem("CrushedSilver","Silver 1","MagicDust " @ floor($HardcodedItemCost[Silver] / 100));
Smith::addItem("CrushedEmerald","Emerald 1","MagicDust " @ floor($HardcodedItemCost[Emerald] / 100));
Smith::addItem("CrushedGold","Gold 1","MagicDust " @ floor($HardcodedItemCost[Gold] / 100));
Smith::addItem("CrushedDiamond","Diamond 1","MagicDust " @ floor($HardcodedItemCost[Diamond] / 100));
Smith::addItem("CrushedPlatinum","Platinum 1","MagicDust " @ floor($HardcodedItemCost[Platinum] / 100));
Smith::addItem("CrushedRadium","Radium 1","MagicDust " @ floor($HardcodedItemCost[Radium] / 100));
Smith::addItem("CrushedKeldrinite","Keldrinite 1","MagicDust " @ floor($HardcodedItemCost[Keldrinite] / 100));
//____________________________________________________________________________________________________________________________________
// Projectiles
BeltItem::Add("Basic Arrow","BasicArrow","AmmoItems",0.1,GenerateItemCost(BasicArrow)/6, $ItemDropIgnoreLuck);
BeltItem::Add("Metal Arrow","MetalArrow","AmmoItems",0.1,GenerateItemCost(MetalArrow)/3, $ItemDropIgnoreLuck);
BeltItem::Add("Metal Dart","MetalDart","AmmoItems",0.1,GenerateItemCost(MetalDart)/3, $ItemDropIgnoreLuck);
BeltItem::Add("Basic Bolt","BasicBolt","AmmoItems",0.1,GenerateItemCost(BasicBolt)/6, $ItemDropIgnoreLuck);
BeltItem::Add("Metal Bolt","MetalBolt","AmmoItems",0.1,GenerateItemCost(MetalBolt)/3, $ItemDropIgnoreLuck);
BeltItem::Add("Basic Dart","BasicDart","AmmoItems",0.1,GenerateItemCost(BasicDart)/6, $ItemDropIgnoreLuck);
BeltItem::Add("Poison Dart","PoisonDart","AmmoItems",0.1,GenerateItemCost(PoisonDart), $ItemDropIgnoreLuck);
BeltItem::Add("Poison Arrow","PoisonArrow","AmmoItems",0.1,GenerateItemCost(PoisonArrow), $ItemDropIgnoreLuck);
BeltItem::Add("Poison Bolt","PoisonBolt","AmmoItems",0.1,GenerateItemCost(PoisonBolt), $ItemDropIgnoreLuck);
BeltItem::Add("Gas Bomb","GasBomb","AmmoItems",0.1,GenerateItemCost(GasBomb), $ItemDropIgnoreLuck);
BeltItem::Add("Throwing Knife","ThrowingKnife","AmmoItems",0.1,GenerateItemCost(ThrowingKnife)/4, $ItemDropIgnoreLuck);
BeltItem::Add("Throwing Spear","ThrowingSpear","AmmoItems",0.1,GenerateItemCost(ThrowingSpear)/3, $ItemDropIgnoreLuck);

//____________________________________________________________________________________________________________________________________
// Wood
BeltItem::Add("Wood Chip","WoodChip","MiscItems",0.1,0, "A chip of wood.");
BeltItem::Add("Wood Chunk","WoodChunk","MiscItems",1.0,0, "A block of wood.");
BeltItem::Add("Wood","Wood","MiscItems",5.0,0, "A log of wood.");
//____________________________________________________________________________________________________________________________________
// Wood conversion
Smith::addItem("ChippedWood","WoodChunk 1","WoodChip 10");
Smith::addItem("ChoppedWood","Wood 1","WoodChunk 5");
Smith::addItem("WoodChunk","WoodChip 10 TreeSap 1","WoodChunk 1");
Smith::addItem("Wood","WoodChunk 5 TreeSap 2","Wood 1");

//____________________________________________________________________________________________________________________________________
// "Resources"
BeltItem::Add("Churl Eye","ChurlEye","MiscItems",5.0,5,"A hideous eyeball taken a dead Churl.");
BeltItem::Add("Savage Heart","SavageHeart","MiscItems",5.0,1,"A human heart. You should only ever have one...");
BeltItem::Add("Clipped Wing","ClippedWing","MiscItems",2.5,5000,"A Kymera wing.");
BeltItem::Add("Elven Ear","ElfEar","MiscItems",2.5,2500,"A Elf's severed ear.");
BeltItem::Add("Antanari Mask","AntanariMask","MiscItems",2.5,5000,"A silver Antanari hero mask.");
BeltItem::Add("Effigy","Effigy","MiscItems",2.5,5,"A Jheriman effigy of a Newcomer.");
BeltItem::Add("Letting Blade","LettingBlade","MiscItems",2.5,250,"A Craven bloodletting blade.");
BeltItem::Add("Control Crystal","ControlCrystal","MiscItems",2.5,5000,"Recovered from the mind of fallen Deluded.");
BeltItem::Add("Bloody Pentagram","BloodyPentagram","MiscItems",2.5,7500,"A blood-soaked pentagram trinket.");
BeltItem::Add("Imp Claw","ImpClaw","MiscItems",5,2000,"An Imp's tendon and claw.");
BeltItem::Add("Toxin","Toxin","MiscItems",0.5,10,"A noxious, poisonous toxin.");
BeltItem::Add("Minotaur Horn","MinotaurHorn","MiscItems",2.5,2000,"A Minotaur horn and stump.");
BeltItem::Add("Green Skin","GreenSkin","MiscItems",2.5,300,"A green, leathery hide.");
BeltItem::Add("Rat Fur","RatFur","MiscItems",2.5,750,"Fur from a Ratbeast.");
BeltItem::Add("Gnoll Hide","GnollHide","MiscItems",2.5,250,"A Gnoll's tough hide.");
BeltItem::Add("Stolen Garments","StolenGarments","MiscItems",2.5,5,"Taken off a dead Amazon. Who owned these...?");
BeltItem::Add("Dragon Scale","DragonScale","MiscItems",8,25000,"A dragon scale");
BeltItem::Add("Enchanted Stone","EnchantedStone","MiscItems",5,5000,"An enchanted stone");
BeltItem::Add("Skeleton Bone","SkeletonBone","MiscItems",5,500,"A skeleton bone");
BeltItem::Add("Parchment","Parchment","MiscItems",0.1,1,"A piece of parchment");
BeltItem::Add("Black Statue","BlackStatue","MiscItems",1,180,"A black statue");
BeltItem::Add("Magic Dust","MagicDust","MiscItems",0.01,1,"A satchel of magic dust");
BeltItem::Add("Loin Cloth","LoinCloth","MiscItems",0.25,1,"A Savage's loin cloth. Hmm.");
BeltItem::Add("Mercury","Mercury","MiscItems",0.1,1,"A satchel of Mercury drained from an Ancient.");
BeltItem::Add("Obsidian","Obsidian","MiscItems",0.5,1,"Black Golem ore.");
BeltItem::Add("Cobalt","Cobalt","MiscItems",1.5,1,"A piece of a Cragspawn.");
BeltItem::Add("Sulfur","Sulfur","MiscItems",0.05,1,"The Churl's favourite 'food'...");
//____________________________________________________________________________________________________________________________________
// Resource combos
Smith::addItem("Parchment","WoodChunk 1","Parchment 3");
Smith::addItem("ParchmentBundle","Wood 1","Parchment 15");

//____________________________________________________________________________________________________________________________________
// Keys and quest items WITHOUT value / possibly nodrop
BeltItem::Add("Septic System Key","SepticSystemKey","MiscItems",1.0,0, "A key for the Septic System of Hazard.", $ItemDropNever);
BeltItem::Add("Black Market Key","BlackMarketKey","MiscItems",0.2,0,"A key for entry into the Black Market.", $ItemDropNever);
BeltItem::Add("Mark of Order","MarkOfOrder","MiscItems",0.2,0,"Given to Members of the Order of Qod.");
BeltItem::Add("WayLink","WayLink","MiscItems",5.0,0,"Fast travel device of the Luminous Dawn.");
BeltItem::Add("The Lawful Masses","TheLawfulMasses","MiscItems",2.0,0,"A text describing principles of The Mandate.");
BeltItem::Add("Iradnium","Iradnium","MiscItems",4.0,0,"A special ore given to new Geostrologists.");
BeltItem::Add("Purgating Soul","PurgatingSoul","MiscItems",0,0,"A wretched soul, trapped in crystal.", $ItemDropIgnoreLuck);
BeltItem::Add("Thief Hands","ThiefHands","MiscItems",2,0,"The hands of a Thief.", $ItemDropIgnoreLuck);
BeltItem::Add("Mineral Water","MineralWater","MiscItems",0,0,"A sample of water rich with minerals.", ($ItemDropIgnoreLuck|$ItemDropNever));
BeltItem::Add("Supply Crate","MandateCrate","MiscItems",50,0,"A 50 pound crate for delivery.");
BeltItem::Add("Supply Backpack","MandateBackpack","MiscItems",25,0,"A 25 pound backpack for delivery.");
BeltItem::Add("Supply Satchel","MandateSatchel","MiscItems",9,0,"A 9 pound satchel for delivery.");
BeltItem::Add("Message For Slicer","MessageForSlicer","MiscItems",0.2,0,"A note to be given to the notorious Black Market King known as 'Slicer'.");
BeltItem::Add("Thief's Writ","ThiefWrit","MiscItems",0.2,0,"A writ for a Thief. Proof of a Mandatory kill.");

//____________________________________________________________________________________________________________________________________
// Healing potions
BeltItem::Add("Tree Sap","TreeSap","PotionItems",0.2,150,"Poisonous tree sap. Do not consume.", $ItemDropIgnoreLuck);
$restoreValue[TreeSap, HP] = -25;
BeltItem::Add("Healing Herbs","HealingHerbs","PotionItems",0.2,10,"Easy to carry herbs that treat light wounds. Restores 5 HP.", $ItemDropIgnoreLuck);
$restoreValue[HealingHerbs, HP] = 5;
BeltItem::Add("Dried Berries","DriedBerries","PotionItems",0.2,10,"Easy to carry berries with healing properties. Restores 5 HP.", $ItemDropIgnoreLuck);
$restoreValue[DriedBerries, HP] = 5;
BeltItem::Add("Bandages","Bandages","PotionItems",0.2,10,"Easy to carry bandages that cover light wounds. Restores 5 HP.", $ItemDropIgnoreLuck);
$restoreValue[Bandages, HP] = 5;
BeltItem::Add("Fresh Fruit","FreshFruit","PotionItems",0.5,30,"A fresh fruit. +15 HP.", $ItemDropIgnoreLuck);
$restoreValue[FreshFruit, HP] = 15;
$restoreValue[FreshFruit, THIRST] = 10;
BeltItem::Add("Large Fruit","LargeFruit","PotionItems",1.5,100,"A large fresh fruit. +40 HP.", $ItemDropIgnoreLuck);
$restoreValue[LargeFruit, HP] = 40;
$restoreValue[LargeFruit, THIRST] = 20;
BeltItem::Add("Meat Stick","MeatStick","PotionItems",1,5,"A delicious chunk of meat on bone. +8 HP.", $ItemDropIgnoreLuck);
$restoreValue[MeatStick, HP] = 8;
$restoreValue[MeatSlab, THIRST] = -8;
BeltItem::Add("Meat Slab","MeatSlab","PotionItems",1,5,"A delicious slab of meat. +15 HP.", $ItemDropIgnoreLuck);
$restoreValue[MeatSlab, HP] = 15;
$restoreValue[MeatSlab, THIRST] = -15;
BeltItem::Add("Weak Health Potion","WeakHealthPotion","PotionItems",3,15,"A potion of healing. +25 HP", $ItemDropIgnoreLuck);
$restoreValue[WeakHealthPotion, HP] = 25;
$restoreValue[WeakHealthPotion, THIRST] = 25;
BeltItem::Add("Health Potion","HealthPotion","PotionItems",4,20,"A potion of healing. +40 HP.", $ItemDropIgnoreLuck);
$restoreValue[HealthPotion, HP] = 40;
$restoreValue[HealthPotion, THIRST] = 40;
BeltItem::Add("Strong Health Potion","StrongHealthPotion","PotionItems",5,100,"A strong potion. +80 HP", $ItemDropIgnoreLuck);
$restoreValue[StrongHealthPotion, HP] = 80;
$restoreValue[StrongHealthPotion, THIRST] = 80;
BeltItem::Add("Vial of Stamina","VialOfStamina","PotionItems",5,500,"A vial of healing. +100 HP", $ItemDropIgnoreLuck);
$restoreValue[VialOfStamina, HP] = 100;
$restoreValue[VialOfStamina, THIRST] = 100;
BeltItem::Add("Vigorous Vial","VigorousVial","PotionItems",5,1500,"A reactive magical substance. +150 HP", $ItemDropIgnoreLuck);
$restoreValue[VigorousVial, HP] = 150;
$restoreValue[VigorousVial, THIRST] = 150;
BeltItem::Add("Soul Essence","SoulEssence","PotionItems",5,3500,"The stuff of life. +500 HP", $ItemDropIgnoreLuck);
$restoreValue[SoulEssence, HP] = 500;
$restoreValue[SoulEssence, MP] = 50;
//____________________________________________________________________________________________________________________________________
// Water
BeltItem::Add("Water Vial","WaterVial","PotionItems",2,100,"A slim vial of water. -100 THIRST");
$restoreValue[WaterVial, THIRST] = 100;
BeltItem::Add("Water Flask","WaterFlask","PotionItems",5,300,"A flask of water. -300 THIRST");
$restoreValue[WaterFlask, THIRST] = 300;
BeltItem::Add("Water Jug","WaterJug","PotionItems",30,600,"A large jug of water. -600 THIRST");
$restoreValue[WaterJug, THIRST] = 600;
BeltItem::Add("Water Cask","WaterCask","PotionItems",50,1200,"Guaranteed to hydrate. -1200 THIRST");
$restoreValue[WaterCask, THIRST] = 1200;
BeltItem::Add("Ice Flask","IceFlask","PotionItems",10,10,"A magical flask of water for Hemac.");
$restoreValue[IceFlask, THIRST] = 50;
BeltItem::Add("Radiating Flask","RadiatingFlask","PotionItems",10,10,"A flask full of potentially explosive material.");
$restoreValue[RadiatingFlask, HP] = -500;
//____________________________________________________________________________________________________________________________________
// Mana potions
BeltItem::Add("Words of Energy","EnergyScroll","PotionItems",0.2,20,"An inspiring scroll. +15 MP");
$restoreValue[EnergyScroll, MP] = 15;
BeltItem::Add("Fermented Drink","EnergyDrink","PotionItems",5,20,"A refreshing drink. +30 MP");
$restoreValue[EnergyDrink, MP] = 30;
$restoreValue[EnergyDrink, THIRST] = 50;
BeltItem::Add("Mana Rock","EnergyRock","PotionItems",1,250,"A chewable mineral. +50 MP");
$restoreValue[EnergyRock, MP] = 50;
$restoreValue[EnergyRock, THIRST] = -50;
BeltItem::Add("Vial of Energy","EnergyVial","PotionItems",2,100,"An energy vial. +75 MP");
$restoreValue[EnergyVial, MP] = 75;
$restoreValue[EnergyVial, THIRST] = 100;
BeltItem::Add("Potion of Energy","EnergyPotion","PotionItems",3,400,"Three vials of energy. +225 MP");
$restoreValue[EnergyPotion, MP] = 225;
$restoreValue[EnergyPotion, THIRST] = 300;
BeltItem::Add("Crystal of Energy","EnergyCrystal","PotionItems",5,750,"A magic crystal. +300 MP");
$restoreValue[EnergyCrystal, MP] = 300;
BeltItem::Add("Soulsphere","Soulsphere","PotionItems",10,2000,"The soul of a beast. +1000 MP");
$restoreValue[Soulsphere, MP] = 1000;
$restoreValue[Soulsphere, MP] = 1000;
//____________________________________________________________________________________________________________________________________
// Elixirs
BeltItem::Add("Minor Elixir","MinorElixir","PotionItems",2,250,"Restores HP, MP and Thirst. +50");
$restoreValue[MinorElixir, THIRST] = 50;
$restoreValue[MinorElixir, HP] = 50;
$restoreValue[MinorElixir, MP] = 50;
BeltItem::Add("Elixir","Elixir","PotionItems",2,750,"Restores HP, MP and Thirst. +250");
$restoreValue[MinorElixir, THIRST] = 250;
$restoreValue[MinorElixir, HP] = 250;
$restoreValue[MinorElixir, MP] = 250;
BeltItem::Add("Major Elixir","MajorElixir","PotionItems",2,1500,"Restores HP, MP and Thirst. +750");
$restoreValue[MinorElixir, THIRST] = 750;
$restoreValue[MinorElixir, HP] = 75;
$restoreValue[MinorElixir, MP] = 750;


//____________________________________________________________________________________________________________________________________
// Brewable combos
Smith::addItem("HealthPotion","WaterVial 1 HealingHerbs 1 TreeSap 1","HealthPotion 1", $SkillRestorationMagic);
Smith::addItem("StrongHealthPotion","WaterVial 1 LargeFruit 1 DriedBerries 1 FreshFruit 1 EnergyScroll 1","StrongHealthPotion 1", $SkillRestorationMagic);
Smith::addItem("VialOfStamina","WaterVial 1 MeatStick 1 FreshFruit 1","VialOfStamina 1", $SkillRestorationMagic);
Smith::addItem("VigorousVial","WaterVial 1 VialOfStamina 1 VigorousVial 1 EnergyCrystal 1","VigorousVial 1", $SkillRestorationMagic);
Smith::addItem("SoulEssence","SavageHeart 1 VialOfStamina 1 EnergyScroll 1","SoulEssence 1", $SkillRestorationMagic);

Smith::addItem("EnergyVial","WaterVial 1 EnergyRock 1","EnergyPotion 1", $SkillRestorationMagic);
Smith::addItem("EnergyPotion","WaterVial 1 HealingHerbs 1 TreeSap 1","EnergyPotion 1", $SkillRestorationMagic);
Smith::addItem("Soulsphere","EnchantedStone 1 EnergyCrystal 1 VigorousVial 1","Soulsphere 1", $SkillRestorationMagic);

Smith::addItem("MinorElixir","","EnergyVial 1 HealthPotion 1", $SkillRestorationMagic);
Smith::addItem("Elixir","VialOfStamina 1 EnergyPotion 1","Elixir 1", $SkillRestorationMagic);
Smith::addItem("MajorElixir","Soulsphere 1 SoulEssence 1","MajorElixir 1", $SkillRestorationMagic);

Smith::addItem("EnergyScroll","MagicDust 1 Parchment 1","EnergyScroll 1");
Smith::addItem("EnergyRock","MagicDust 1 SmallRock 1 TreeSap 1","EnergyRock 1");
Smith::addItem("EnergyCrystal","EnergyRock 1 Bandages 1 TreeSap 1","EnergyCrystal 1");

//____________________________________________________________________________________________________________________________________
// Smithing books
BeltItem::Add("Book Of Concoctions","BookOfConcoctions","MiscItems",2.5,0, "", ($ItemDropNever | $ItemDropStealProof));
BeltItem::Add("Book Of Robes","BookOfRobes","MiscItems",2.5,50000);
BeltItem::Add("Book Of Old","BookOfOld","MiscItems",2.5,50000);
BeltItem::Add("Book Of Daggers","BookOfDaggers","MiscItems",2.5,50000);
BeltItem::Add("Book Of Swords","BookOfSwords","MiscItems",2.5,50000);
BeltItem::Add("Book Of Earth","BookOfEarth","MiscItems",2.5,50000);
BeltItem::Add("Book Of Justice","BookOfJustice","MiscItems",2.5,50000);
BeltItem::Add("Book Of Armor","BookOfArmor","MiscItems",2.5,50000);
function rpg::GenerateSmithingBookHelpText() {	
	%bookForSkill[$SkillRestorationMagic] 		= BookOfConcoctions;
	%bookForSkill[$SkillStrength] 				= BookOfArmor;
	%bookForSkill[$SkillWands] 					= BookOfJustice;
	%bookForSkill[$SkillArchery] 				= BookOfEarth;
	%bookForSkill[$SkillSlashing] 				= BookOfSwords;
	%bookForSkill[$SkillPiercing] 				= BookOfDaggers;
	%bookForSkill[$SkillBludgeoning] 			= BookOfOld;
	%bookForSkill[$SkillEndurance] 				= BookOfRobes;
	
	$AccessoryVar[BookOfConcoctions, $MiscInfo] = "To brew potions, type #brew followed by:\n<f1>";
	$AccessoryVar[BookOfArmor, $MiscInfo] 		= "This book describes how to #smith armor:\n<f1>";
	$AccessoryVar[BookOfJustice, $MiscInfo] 	= "This book describes how to #smith wands and devices:\n<f1>";
	$AccessoryVar[BookOfEarth, $MiscInfo] 		= "This book describes how to #smith archery weapons and projectiles:\n<f1>";
	$AccessoryVar[BookOfSwords, $MiscInfo] 		= "This book describes how to #smith slashing weapons:\n<f1>";
	$AccessoryVar[BookOfDaggers, $MiscInfo] 	= "This book describes how to #smith piercing weapons:\n<f1>";
	$AccessoryVar[BookOfOld, $MiscInfo] 		= "This book describes how to #smith bludgeoning weapons:\n<f1>";
	$AccessoryVar[BookOfRobes, $MiscInfo] 		= "This book describes how to #smith robes:\n<f1>";
	
	for(%x=0; %x<$SmithItemCount; %x++) {
		%book = %bookForSkill[$SmithAssociatedSkillType[%x]];		
		$RequireItemToExamine[%book] = true;
		$AccessoryVar[%book, $MiscInfo] = $AccessoryVar[%book, $MiscInfo] @ $SmithNum[%x] @ " ";
	}
}

//____________________________________________________________________________________________________________________________________
// Glass Idiom generation
function rpg::GenerateGlassIdiomsFromSpells() {
	for(%x=1;%x<=$SpellDefIndex;%x++) {
		%itemname = $Spell::keyword[%x] @ "idiom";
		$SkillRestriction[%itemname] = $SkillWordsmith @ " " @ getword($SkillRestriction[$Spell::keyword[%x]],1);
		
		%cost = $Spell::manaCost[%x] + $Spell::damageValue[%x] + $Spell::refVal[%x] + $Spell::radius[%x] + $Spell::LOSrange[%x] +
				$Spell::recoveryTime[%x] + $Spell::ticks[%x] + $Spell::delay[%x] + $Spell::FXDetonateCount[%x] + String::len($Spell::bonuses[%x]) +
				$Spell::graceDistance[%x];
		%cost *= (getword($SkillRestriction[%itemname],1) / 75);
		if(%cost < 0) %cost = -%cost;
		
		BeltItem::Add("Idiomatic " @ $Spell::name[%x], %itemname, "GlassIdioms", 0.2 + ($Spell::FXDetonateCount[%x] * 0.2), %cost);
		$AccessoryVar[%itemname, $MiscInfo] = $Spell::description[%x];
		$GlassIdiomspell[%itemname] = %x;
		$SkillType[%itemname] = $SkillWordsmith;		
	}
}
