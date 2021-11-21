//This file is part of Tribes RPG.
//Tribes RPG server side scripts
//Written by Jason "phantom" Daley,  Matthiew "JeremyIrons" Bouchard, and more (yet undetermined)

//	Copyright (C) 2014  Jason Daley

//	This program is free software: you can redistribute it and/or modify
//	it under the terms of the GNU General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.

//	This program is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//	GNU General Public License for more details.

//	You should have received a copy of the GNU General Public License
//	along with this program.  If not, see <http://www.gnu.org/licenses/>.

//You may contact the author at beatme101@gmail.com or www.tribesrpg.org/contact.php

//This GPL does not apply to Starsiege: Tribes or any non-RPG related files included.
//Starsiege: Tribes, including the engine, retains a proprietary license forbidding resale.

//________________________________________________________________________________________________________________________________________________________________
// DescX Notes:
//		StaticShapes, Crystals, and the Ferry all live here now.
//		If it's a StaticShape and it doesn't do anything particularly fancy, it goes here.
//
//________________________________________________________________________________________________________________________________________________________________
// Various structs that aren't "shapes", but used early
//________________________________________________________________________________________________________________________________________________________________
DebrisData defaultDebrisSmall {
   type      = 0;
   imageType = 0;   
   mass       = 100.0;
   elasticity = 0.25;
   friction   = 0.5;
   center     = { 0, 0, 0 };
   animationSequence = -1;
   minTimeout = 3.0;
   maxTimeout = 6.0;
   explodeOnBounce = 0.3;
   damage          = 1000.0;
   damageThreshold = 100.0;
   spawnedDebrisMask     = 1;
   spawnedDebrisStrength = 90;
   spawnedDebrisRadius   = 0.2;
   spawnedExplosionID = debrisExpSmall;
   p = 1;
   explodeOnRest   = True;
   collisionDetail = 0;
};
ItemData BloodSpotSolid {
	description = "Blood";	className = "Blood";	shapeFile = "blood1";	heading = "xMisc";	shadowDetailMask = 4;	price = 0;
};
ExplosionData flashExpSmall {
	shapeName = "flash_small.dts";
	soundId   = debrisSmallExplosion;
	faceCamera = true; randomSpin = true; hasLight   = true;
	timeZero = 0.250; timeOne  = 0.650;
	lightRange = 2.5;
	colors[0]  = { 0.0, 0.0, 0.0  }; colors[1]  = { 1.0, 0.5, 0.16 }; colors[2]  = { 1.0, 0.5, 0.16 }; radFactors = { 0.0, 1.0, 1.0 };
};
DebrisData flashDebrisSmall {
	type = 0; imageType = 0; mass = 100.0; elasticity = 0.25; friction = 0.5; center = { 0, 0, 0 };
	animationSequence = -1; minTimeout = 3.0; maxTimeout = 6.0; explodeOnBounce = 0.3; damage = 1000.0; damageThreshold = 100.0; 
	spawnedDebrisMask = 1; spawnedDebrisStrength = 90; spawnedDebrisRadius = 0.2; spawnedExplosionID = flashExpSmall; p = 1;
	explodeOnRest = True; collisionDetail = 0;
};
ExplosionData flashExpMedium {
	shapeName = "flash_medium.dts"; soundId   = debrisMediumExplosion;
	faceCamera = true; randomSpin = true; hasLight   = true;
	timeZero = 0.250; timeOne  = 0.650;
	lightRange = 3.75;
	colors[0]  = { 0.0, 0.0, 0.0  }; colors[1]  = { 1.0, 0.5, 0.16 }; colors[2]  = { 1.0, 0.5, 0.16 }; radFactors = { 0.0, 1.0, 1.0 };
};


//________________________________________________________________________________________________________________________________________________________________
// Base shapes
//________________________________________________________________________________________________________________________________________________________________
StaticShapeData DefaultBeacon
{
	className = "Beacon";
	damageSkinData = "objectDamageSkins";
	shapeFile = "sensor_small";
	maxDamage = 0.1;
	maxEnergy = 200;
   castLOS = true;
   supression = false;
	mapFilter = 2;
	visibleToSensor = true;
   explosionId = flashExpSmall;
	debrisId = flashDebrisSmall;
	disableCollision = true;
};
StaticShapeData SmallAntenna
{
	shapeFile = "anten_small";
	debrisId = defaultDebrisSmall;
	maxDamage = 1.0;
	damageSkinData = "objectDamageSkins";
	shadowDetailMask = 16;
	explosionId = flashExpMedium;
   description = "Small Antenna";
};
StaticShapeData MediumAntenna
{
	shapeFile = "anten_med";
	debrisId = flashDebrisSmall;
	maxDamage = 1.5;
	damageSkinData = "objectDamageSkins";
	shadowDetailMask = 16;
	explosionId = flashExpMedium;
   description = "Medium Antenna";
};
StaticShapeData ForceBeacon
{
	shapeFile = "force";
	debrisId = defaultDebrisSmall;
	maxDamage = 0.5;
	damageSkinData = "objectDamageSkins";
	shadowDetailMask = 16;
	explosionId = debrisExpMedium;
   description = "Force Beacon";
};
StaticShapeData CargoCrate
{
	shapeFile = "magcargo";
	debrisId = flashDebrisSmall;
	maxDamage = 1.0;
	damageSkinData = "objectDamageSkins";
	shadowDetailMask = 16;
	explosionId = flashExpMedium;
	description = "CargoCrate";
};
StaticShapeData CargoBarrel
{
	shapeFile = "liqcyl";
	debrisId = defaultDebrisSmall;
	maxDamage = 1.0;
	damageSkinData = "objectDamageSkins";
	shadowDetailMask = 16;
	explosionId = debrisExpMedium;
   description = "Cargo Barrel";
};
StaticShapeData VerticalPanel
{
	shapeFile = "teleport_vertical";
	debrisId = defaultDebrisSmall;
	explosionId = debrisExpMedium;
	maxDamage = 0.5;
	damageSkinData = "objectDamageSkins";
   description = "Panel";
};
StaticShapeData VerticalPanelB
{
	shapeFile = "panel_vertical";
	debrisId = defaultDebrisSmall;
	explosionId = debrisExpMedium;
	maxDamage = 0.5;
	damageSkinData = "objectDamageSkins";
   description = "Panel";
};
StaticShapeData DisplayPanelOne
{
	shapeFile = "display_one";
	debrisId = flashDebrisSmall;
	explosionId = flashExpMedium;
	maxDamage = 0.5;
	damageSkinData = "objectDamageSkins";
   description = "Panel";
};
StaticShapeData DisplayPanelTwo
{
	shapeFile = "display_two";
	debrisId = defaultDebrisSmall;
	explosionId = debrisExpMedium;
	maxDamage = 0.5;
	damageSkinData = "objectDamageSkins";
   description = "Panel";
};
StaticShapeData DisplayPanelThree
{
	shapeFile = "display_three";
	debrisId = flashDebrisSmall;
	explosionId = flashExpMedium;
	maxDamage = 0.5;
	damageSkinData = "objectDamageSkins";
   description = "Panel";
};
StaticShapeData RForceField
{
	shapeFile = "forcefield";
	debrisId = defaultDebrisSmall;
	maxDamage = 10000.0;
	isTranslucent = true;
	description = "Force Field";
};
StaticShapeData PoweredElectricalBeam
{
	shapeFile = "zap";
	maxDamage = 10000.0;
	isTranslucent = true;
    description = "Electrical Beam";
   disableCollision = true;
};
StaticShapeData FlagStand
{
   description = "Flag Stand";
	shapeFile = "flagstand";
	visibleToSensor = false;
	disableCollision = true;
};
StaticShapeData SteamOnGrass
{
	shapeFile = "steamvent_grass";
	maxDamage = 999.0;
	isTranslucent = "True";
   description = "Steam Vent";
};
StaticShapeData SteamOnMud
{
	shapeFile = "steamvent_mud";
	maxDamage = 999.0;
	isTranslucent = "True";
   description = "Steam Vent";
};
StaticShapeData DepPlatSmallHorz
{
	shapeFile = "elevator_4x4";
	debrisId = defaultDebrisSmall;
	maxDamage = 9999;
	visibleToSensor = false;
	isTranslucent = true;
   	description = "SmallPlatform";
};
StaticShapeData DepPlatMediumHorz
{
	shapeFile = "elevator6x6thin";
	debrisId = defaultDebrisSmall;
	maxDamage = 9999;
	visibleToSensor = false;
	isTranslucent = true;
   	description = "MediumPlatform";
};
StaticShapeData DepPlatLargeHorz
{
	shapeFile = "elevator_9x9";
	debrisId = defaultDebrisSmall;
	maxDamage = 9999;
	visibleToSensor = false;
	isTranslucent = true;
   	description = "LargePlatform";
};
StaticShapeData DepPlatLargeVert
{
	shapeFile = "elevator_9x9";
	debrisId = defaultDebrisSmall;
	maxDamage = 9999;
	visibleToSensor = false;
	isTranslucent = true;
   	description = "LargePlatform";
};
StaticShapeData StaticDoorForceField
{
	shapeFile = "ForceField";
	debrisId = defaultDebrisSmall;
	maxDamage = 10000.0;
	visibleToSensor = false;
	isTranslucent = true;
   	description = "Door Force Field";
};
StaticShapeData DepPlatSmallVert
{
	shapeFile = "elevator_4x4";
	debrisId = defaultDebrisSmall;
	maxDamage = 9999;
	visibleToSensor = false;
	isTranslucent = true;
   	description = "SmallPlatform";
};
StaticShapeData DepPlatMediumVert
{
	shapeFile = "elevator6x6thin";
	debrisId = defaultDebrisSmall;
	maxDamage = 9999;
	visibleToSensor = false;
	isTranslucent = true;
   	description = "MediumPlatform";
};


//________________________________________________________________________________________________________________________________________________________________
// RPG shapes
//________________________________________________________________________________________________________________________________________________________________
StaticShapeData ElectricalBeam
{
	shapeFile = "zap";
	maxDamage = 10000.0;
	isTranslucent = true;
    description = "Electrical Beam";
   disableCollision = true;
};
StaticShapeData ElectricalBeamBig
{
	shapeFile = "zap_5";
	maxDamage = 10000.0;
	isTranslucent = true;
    description = "Electrical Beam";
   disableCollision = true;
};
StaticShapeData Cactus1
{
	shapeFile = "cactus1";
	debrisId = defaultDebrisSmall;
	maxDamage = 0.4;
   description = "Cactus";
};
StaticShapeData Cactus2
{
	shapeFile = "cactus2";
	debrisId = defaultDebrisSmall;
	maxDamage = 0.4;
   description = "Cactus";
};
StaticShapeData Cactus3
{
	shapeFile = "cactus3";
	debrisId = defaultDebrisSmall;
	maxDamage = 0.4;
   description = "Cactus";
};
StaticShapeData TreeShape
{
	shapeFile = "tree1";
	maxDamage = 99999.0;
	isTranslucent = "True";
   description = "Tree";
};
StaticShapeData TreeShapeTwo
{
	shapeFile = "tree2";
	maxDamage = 99999.0;
	isTranslucent = "True";
   description = "Tree";
};
StaticShapeData PhantomStrangerTree1
{
	shapeFile = "rpgtree1";
	maxDamage = 99999.0;
	isTranslucent = "False";
	description = "PhantomStranger's Tree Small";
};
StaticShapeData PhantomStrangerTree2
{
	shapeFile = "bigrpgtree1";
	maxDamage = 99999.0;
	isTranslucent = "False";
	description = "PhantomStranger's Tree Medium";
};
StaticShapeData PhantomStrangerTree3
{
	shapeFile = "REALLYbigrpgtree";
	maxDamage = 99999.0;
	isTranslucent = "False";
	description = "PhantomStranger's Tree Large";
};
StaticShapeData PlantTwo
{
	shapeFile = "plant2";
	debrisId = defaultDebrisSmall;
	maxDamage = 99999.0;
   description = "Plant";
};
StaticShapeData SoundPoint {
	shapeFile = "bullet";
	maxDamage = 999.0;
	isTranslucent = "True";
};
StaticShapeData WavyWater1 {
	shapeFile = "forcefield";
	maxDamage = 999.0;
	isTranslucent = "True";
	disableCollision = "True";
};
StaticShapeData WavyWater2 {
	shapeFile = "forcefield2";
	maxDamage = 999.0;
	isTranslucent = true;
	disableCollision = true;
	visibleToSensor = false;
	mapFilter = 1;
	shadowDetailMask = 0;
};
StaticShapeData WavyWater4 {
	shapeFile = "forcefield4";
	maxDamage = 999.0;
	isTranslucent = "True";
	disableCollision = "True";
};
StaticShapeData WavyWater16 {
	shapeFile = "forcefield16";
	maxDamage = 999.0;
	isTranslucent = "False";
	disableCollision = "False";
};
StaticShapeData SlayerFemaleTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Wildenslayer";	className = "TownBot";	
	shapeFile = "slayerfem";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData SlayerMaleTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Wildenslayer";	className = "TownBot";	
	shapeFile = "stinger";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData MandateMaleTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Mandator";	className = "TownBot";	
	shapeFile = "keldmandmale";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData MandateFemaleTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Mandator";	className = "TownBot";
	shapeFile = "keldmandfem";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData QodMasterTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Ordergiver";	className = "TownBot";
	shapeFile = "orderofqod";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData QodFemaleTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Ordergiver";	className = "TownBot";
	shapeFile = "orderofqodf";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData HazardDrunkTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Ordergiver";	className = "TownBot";
	shapeFile = "travellermale";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData TravellerFemTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Ordergiver";	className = "TownBot";
	shapeFile = "travellerfem";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData RubyMinerFemaleTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Ruby Miner";	className = "TownBot";
	shapeFile = "pryzm";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData RubyMinerMaleTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Ruby Miner";	className = "TownBot";
	shapeFile = "rubyminermale";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData DancerManagerTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Manager";	className = "TownBot";
	shapeFile = "dawngm";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData DancerTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Dancer";	className = "TownBot";
	shapeFile = "dancer";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData KeeperTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Keeper of Secrets";	className = "TownBot";
	shapeFile = "keeper";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData AncientTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Ancient";	className = "TownBot";
	shapeFile = "ancient";	visibleToSensor = true;		mapFilter = 1;		
};StaticShapeData MaleHumanTownBot {	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;	description = "Male Town Bot";	className = "TownBot";	shapeFile = "rpgmalehuman";	visibleToSensor = true;		mapFilter = 1;		};StaticShapeData FemaleHumanTownBot {	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;	description = "Female Town Bot";	className = "TownBot";	shapeFile = "lfemalehuman";	visibleToSensor = true;		mapFilter = 1;		};
StaticShapeData MaleHumanRobedTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Male Town Bot";	className = "TownBot";
	shapeFile = "magemale";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData FemaleHumanRobedTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Male Town Bot";	className = "TownBot";
	shapeFile = "femalemage";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData OutlawMaleTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Male Outlaw Bot";	className = "TownBot";
	shapeFile = "outlaw";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData OutlawFemaleTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Female Outlaw Bot";	className = "TownBot";
	shapeFile = "outlawfemale";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData GnollTownBot {
	debrisId = defaultDebrisSmall;	maxDamage = 9999.0;
	description = "Male Outlaw Bot";	className = "TownBot";
	shapeFile = "marmorgnoll";	visibleToSensor = true;		mapFilter = 1;		
};
StaticShapeData GeneratorTownBot {
	description = "Weather Device";shapeFile = "generator";debrisId = flashDebrisSmall;maxDamage = 9999;shadowDetailMask = 16;
	className = "TownBot"; visibleToSensor = true;		mapFilter = 1;
};
StaticShapeData SolarPanelTownBot {
	description = "Weather Device";shapeFile = "solar_med";debrisId = flashDebrisSmall;maxDamage = 9999;shadowDetailMask = 16;
	className = "TownBot"; visibleToSensor = true;		mapFilter = 1;
};
StaticShapeData PortGeneratorTownBot {
	description = "Weather Device";shapeFile = "generator_p";debrisId = flashDebrisSmall;maxDamage = 9999;shadowDetailMask = 16;
	className = "TownBot"; visibleToSensor = true;		mapFilter = 1;
};
StaticShapeData LargeAntennaTownBot {
	description = "Weather Device";shapeFile = "anten_lrg";debrisId = defaultDebrisSmall;maxDamage = 9999;shadowDetailMask = 16;
	className = "TownBot"; visibleToSensor = true;		mapFilter = 1;
};
StaticShapeData ArrayAntennaTownBot {
	description = "Weather Device";shapeFile = "anten_lava";debrisId = flashDebrisSmall;maxDamage = 9999;shadowDetailMask = 16;
	className = "TownBot"; visibleToSensor = true;		mapFilter = 1;
};
StaticShapeData RodAntennaTownBot {
	description = "Weather Device";shapeFile = "anten_rod";debrisId = defaultDebrisSmall;maxDamage = 9999;shadowDetailMask = 16;
	className = "TownBot"; visibleToSensor = true;		mapFilter = 1;
};
StaticShapeData nArrow{	shapeFile = "arrow5_b";	debrisId = defaultDebrisSmall;	maxDamage = 10000.0;	disableCollision = true;	shadowDetailMask = 1;	visibleToSensor = true;		mapFilter = 1;		};
StaticShapeData DTS_rockingchair
{
    debrisId = defaultDebrisSmall;
    maxDamage = 9999.0;
    description = "A shape";
    shapeFile = "chair_anni1";
    visibleToSensor = false;
    mapFilter = 1;
};
StaticShapeData DTS_bark
{
    debrisId = defaultDebrisSmall;
    maxDamage = 9999.0;
    description = "Dead grey tree no collision";
    shapeFile = "bark";
    visibleToSensor = false;
	disableCollision = false;
    mapFilter = 1;
};
StaticShapeData DTS_walkBlade
{
    debrisId = defaultDebrisSmall;
    maxDamage = 9999.0;
    description = "A shape";
    shapeFile = "walkBlade";
    visibleToSensor = false;
    mapFilter = 1;
};
StaticShapeData DTS_Tree_Evergreen
{
    debrisId = defaultDebrisSmall;
    maxDamage = 9999.0;
    description = "A shape";
    shapeFile = "treeWalk";
    visibleToSensor = false;
    mapFilter = 1;
};
StaticShapeData Walktree9 {
    shapeFile = "walktree_XL_leaf0"; debrisId = defaultDebrisSmall; maxDamage = 9999.0; description = "A shape";
};
StaticShapeData DTS_SpiderWeb
{
    debrisId = defaultDebrisSmall;
    maxDamage = 9999.0;
    description = "A shape";
    shapeFile = "spiderweb1l";
    visibleToSensor = false;
    mapFilter = 1;
};
StaticShapeData DTS_LargeTree1
{
    debrisId = defaultDebrisSmall;
    maxDamage = 9999.0;
    description = "A shape";
    shapeFile = "walktree_XL_leaf1";
    visibleToSensor = false;
    mapFilter = 1;
};
StaticShapeData DTS_LargeTree2
{
    debrisId = defaultDebrisSmall;
    maxDamage = 9999.0;
    description = "A shape";
    shapeFile = "walktree_XL_leaf2";
    visibleToSensor = false;
    mapFilter = 1;
};
StaticShapeData DTS_LargeTree3
{
    debrisId = defaultDebrisSmall;
    maxDamage = 9999.0;
    description = "A shape";
    shapeFile = "walktree_XL_leaf3";
    visibleToSensor = false;
    mapFilter = 1;
};
StaticShapeData DTS_MidTree1
{
    debrisId = defaultDebrisSmall;
    maxDamage = 9999.0;
    description = "A shape";
    shapeFile = "walktree_L_leaf1";
    visibleToSensor = false;
    mapFilter = 1;
};
StaticShapeData DTS_MidTree2
{
    debrisId = defaultDebrisSmall;
    maxDamage = 9999.0;
    description = "A shape";
    shapeFile = "walktree_L_leaf2";
    visibleToSensor = false;
    mapFilter = 1;
};
StaticShapeData DTS_MidTree3
{
    debrisId = defaultDebrisSmall;
    maxDamage = 9999.0;
    description = "A shape";
    shapeFile = "walktree_L_leaf3";
    visibleToSensor = false;
    mapFilter = 1;
};
StaticShapeData ForceFieldWall
{
	shapeFile = "forcefield";
	debrisId = defaultDebrisSmall;
	maxDamage = 500.0;
	isTranslucent = true;
	description = "Force Field";
};
StaticShapeData ForceFieldDome
{
	shapeFile = "domefiled";
	debrisId = defaultDebrisSmall;
	maxDamage = 10000.0;
	isTranslucent = true;
	description = "Force Field";
};
StaticShapeData fountain
{
	shapeFile = "fountain";
	debrisId = defaultDebrisSmall;
	maxDamage = 10000.0;
};
StaticShapeData fountainwater
{
	shapeFile = "fountain_water";
	debrisId = defaultDebrisSmall;
	maxDamage = 10000.0;
	disableCollision = true;
	isTranslucent = "True";
};
StaticShapeData BloodSpot
{
	shapeFile = "blood1";
	maxDamage = 999.0;
	isTranslucent = "True";
	disableCollision = true;
};

StaticShapeData BaseTribesFlag
{
	description = "Flag Stand";
	shapeFile = "flag";
	visibleToSensor = false;
};
StaticShapeData TowerSwitch
{
	description = "Buoy";
	className = "towerSwitch";
	shapeFile = "tower";
	showInventory = "false";
	visibleToSensor = true;
	mapFilter = 4;
	mapIcon = "M_generator";
};
StaticShapeData PlasmaFlame
{
	shapeFile = "plasmabolt";
	maxDamage = 10000.0;
	isTranslucent = true;
	description = "Flame";
	disableCollision = true;
};
StaticShapeData GiantFlame
{
	shapeFile = "fire_xl";
	maxDamage = 10000.0;
	isTranslucent = true;
	description = "Flame";
	disableCollision = true;
};
StaticShapeData MassiveFlame
{
	shapeFile = "fire_omg";
	maxDamage = 10000.0;
	isTranslucent = true;
	description = "Flame";
	disableCollision = true;
};
StaticShapeData MediumFlame
{
	shapeFile = "fire_medium";
	maxDamage = 10000.0;
	isTranslucent = true;
	description = "Flame";
	disableCollision = true;
};
ItemData Beacon 
{
	description = "Beacon";
	shapeFile = "sensor_small";
	heading = "jMiscellany";
	shadowDetailMask = 4;
	price = 5;
	className = "HandAmmo";
};

//________________________________________________________________________________________________________________________________________________________________
// Crystals
//________________________________________________________________________________________________________________________________________________________________
StaticShapeData Crystal		{ shapeFile = "crystals";  debrisId = flashDebrisSmall;  maxDamage = 1.0;  damageSkinData = "objectDamageSkins";  shadowDetailMask = 16;  explosionId = flashExpMedium;  description = "Crystal"; };
StaticShapeData Crystals2 	{ shapeFile = "Crystals2";  debrisId = flashDebrisSmall;  maxDamage = 1.0;  damageSkinData = "objectDamageSkins";  shadowDetailMask = 16;  explosionId = flashExpMedium;  description = "Crystal"; };
StaticShapeData Crystals3 	{ shapeFile = "Crystals3";  debrisId = flashDebrisSmall;  maxDamage = 1.0;  damageSkinData = "objectDamageSkins";  shadowDetailMask = 16;  explosionId = flashExpMedium;  description = "Crystal"; };
StaticShapeData Crystals4 	{ shapeFile = "Crystals4";  debrisId = flashDebrisSmall;  maxDamage = 1.0;  damageSkinData = "objectDamageSkins";  shadowDetailMask = 16;  explosionId = flashExpMedium;  description = "Crystal"; };
StaticShapeData Crystals5 	{ shapeFile = "Crystals5";  debrisId = flashDebrisSmall;  maxDamage = 1.0;  damageSkinData = "objectDamageSkins";  shadowDetailMask = 16;  explosionId = flashExpMedium;  description = "Crystal"; };
StaticShapeData EmptyCrystal { shapeFile = "crystals";  debrisId = flashDebrisSmall;  maxDamage = 1.0;  damageSkinData = "objectDamageSkins";  shadowDetailMask = 16;  explosionId = flashExpMedium;       description = "Empty Crystal";};
function Crystal::onDamage(){}
function Crystals2::onDamage(){}
function Crystals3::onDamage(){}
function Crystals4::onDamage(){}
function Crystals5::onDamage(){}

function InitStaticShapes() {
	dbecho($dbechoMode, "InitCrystals()");

	%group = nameToID("MissionGroup\\Crystals");
	if(%group != -1) {
		for(%i = 0; %i <= Group::objectCount(%group)-1; %i++) {
			%this 	= Group::getObject(%group, %i);
			%info 	= Object::getName(%this);
			%pos 	= GameBase::getPosition(%this);
			%rot 	= GameBase::getRotation(%this);
			%shape 	= %this.crystaltype;
			//echo(%shape);
			if(%shape == "") echo(%info);
			deleteObject(%this);
			%this = newObject(%info, StaticShape, %shape, true);
			GameBase::setRotation(%this, %rot);
			GameBase::setPosition(%this, %pos);
			addToSet("MissionGroup\\Crystals", %this);
			%this.crystaltype = %shape;
			if(%info != "") {
				for(%z = 0; (%p1 = GetWord(%info, %z)) != -1; %z+=2) {
					%p2 = GetWord(%info, %z+1);
					%this.bonus[%p1] = %p2;
				}
			}
		}
	}
	
	%group = nameToID("MissionGroup\\TeleportBeams");
	if(%group != -1) {
		for(%i = 0; %i <= Group::objectCount(%group)-1; %i++) {
			%this 	= Group::getObject(%group, %i);
			%name 	= Object::getName(%this);
			%pos 	= GameBase::getPosition(%this);
			%rot 	= GameBase::getRotation(%this);
			%shape 	= "electricalbeam";
			if(string::findSubStr(%shape, "Big") == 0) %shape = "ElectricalBeamBig";
			deleteObject(%this);
			%this = newObject(%name, StaticShape, %shape, true);
			GameBase::setRotation(%this, %rot);
			GameBase::setPosition(%this, %pos);
			addToSet("MissionGroup\\TeleportBeams", %this);
		}
	}
}



//________________________________________________________________________________________________________________________________________________________________
// FERRY
//________________________________________________________________________________________________________________________________________________________________
$FerryfolderNameForSystem = "System";
$FerryfolderNameForPath = "Path";
$FerryStationWait = 0.05;

if($ferryObject == "")
	$ferryObject = "raft_b";

ExplosionData PlatformFerryExplosion {
   shapeName = "tumult_large.dts";
   soundId   = debrisLargeExplosion;
   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 5.0;
   timeZero = 0.250;
   timeOne  = 0.650;
   colors[0]  = { 0.0, 0.0, 0.0  };
   colors[1]  = { 1.0, 0.5, 0.16 };
   colors[2]  = { 1.0, 0.5, 0.16 };
   radFactors = { 0.0, 1.0, 1.0 };
};
DebrisData PlatformFerryDebris {
	type = 0; imageType = 0; mass = 100.0; elasticity = 0.25; friction = 0.5; center = { 0, 0, 0 }; animationSequence = -1; 
	minTimeout = 3.0; maxTimeout = 6.0; explodeOnBounce = 0.3; damage = 1000.0; damageThreshold = 100.0; spawnedDebrisMask = 1; 
	spawnedDebrisStrength = 90; spawnedDebrisRadius = 0.2; spawnedExplosionID = PlatformFerryExplosion; p = 1; explodeOnRest = True; collisionDetail = 0;
};
MoveableData PlatformFerry {
	shapeFile = $ferryObject;
	className = "Ferry";
	destroyable = false;
	maxDamage = 100;
	triggerRadius = 4;
	isPerspective = true;
	displace = true;
	explosionId = PlatformFerryExplosion;
	debrisId = PlatformFerryDebris;
	sfxStart = NoSound;
	sfxStop = NoSound;
	sfxRun = SoundBoat;
	sfxBlocked = NoSound;
	speed = 25;
};

function Ferry::onNewPath(%this) {
	NextWaypoint(%this);
}
function Ferry::onWaypoint(%this) {
	NextWaypoint(%this);
}
function ResetFerry(%this) {
	dbecho($dbechoMode, "ResetFerry(" @ %this @ ")");
	Moveable::setWaypoint(%this, 0);
}
function NextWaypoint(%this) {
	dbecho($dbechoMode, "NextWaypoint(" @ %this @ ")"); 
	//This function makes %this go to the next waypoint on its stack.
	//Once the current waypoint is the last waypoint, switch the ferry to waypoint 0 and wait $FerryStationWait amount
	//of seconds before moving on to next waypoint.

	%which = round(Moveable::getPosition(%this));

	//echo("NextWaypoint: " @ %this);
	//echo("waypointCount = " @ Moveable::getWaypointCount(%this));
	//echo("%which: " @ %which);
	//echo("-------------------------------");

	if(%which < (Moveable::getWaypointCount(%this)-1))
	{
		schedule("Moveable::moveToWaypoint(" @ %this @ ", " @ %which+1 @ ");", 0.05+$Ferry::PauseTime[%this, %which]);
	}
	else if(%which == (Moveable::getWaypointCount(%this)-1))
	{
		ResetFerry(%this);
		schedule("NextWaypoint(" @ %this @ ");", $FerryStationWait, %this);
	}
}

function Ferry::onCollision(%this, %object) {
	return;
}

function IsOnFerry(%clientId) {
	dbecho($dbechoMode, "IsOnFerry(" @ %clientId @ ")");

	%wferry = "";

	for(%i = 1; %i <= 20; %i++)
	{
		%z = floor(GetWord($personalFerryArray[%clientId, %i], 0));
		if(%z == -1) %z = 0;

		%cnt += %z;
		if(GetWord($personalFerryArray[%clientId, %i], 1) != -1 && %wferry == "")
			%wferry = GetWord($personalFerryArray[%clientId, %i], 1);
	}

	if(%cnt > 3)
		return %wferry;
	else
		return -1;
}

function InitFerry() {
	dbecho($dbechoMode, "InitFerry()");

	%group = nameToId("MissionGroup\\Ferry");

	if(%group != -1)
	{
		%count = Group::objectCount(%group);
		for(%i = 0; %i <= %count-1; %i++)
		{
			%object = Group::getObject(%group, %i);
			%system = Object::getName(%object);
			%wferry = String::getSubStr(%system, String::len($FerryfolderNameForSystem), String::len(%system)-String::len($FerryfolderNameForSystem));

			//find %ferry id
			%g = nameToId("MissionGroup\\Ferry\\" @ %system);
			%c = Group::objectCount(%g);
			for(%k = 0; %k <= %c-1; %k++)
			{
				%o = Group::getObject(%g, %k);
				if(getObjectType(%o) == "Moveable")
					%ferry = %o;
			}

			$Ferry::FolderName[%ferry] = "MissionGroup\\Ferry\\" @ %system;

			//go thru all the markers / droppoints and perhaps do something?
			%groupForPath = nameToId("MissionGroup\\Ferry\\" @ %system @ "\\" @ $FerryfolderNameForPath);
			%countForPath = Group::objectCount(%groupForPath);
			for(%j = 0; %j <= %countForPath-1; %j++)
			{
				%o1 = Group::getObject(%groupForPath, %j);
				$Ferry::MarkerPos[%ferry, %j] = GameBase::getPosition(%o1);
				$Ferry::PauseTime[%ferry, %j] = floor((Object::getName(%o1)) * 100) / 100;
			}
		}
	}
}






//________________________________________________________________________________________________________________________________________________________________
// Trash never cleaned up from base
// Will remove eventually
//________________________________________________________________________________________________________________________________________________________________


function StaticShape::onDestroyed(%this)
{
	$owner[%this] = "";
	GameBase::stopSequence(%this,0);
	StaticShape::objectiveDestroyed(%this);
}
function StaticShape::onDamage(%this,%type,%value,%pos,%vec,%mom,%object)
{
	%damageLevel = GameBase::getDamageLevel(%this);
	%dValue = %damageLevel + %value;
   %this.lastDamageObject = %object;
   %this.lastDamageTeam = GameBase::getTeam(%object);
	if(GameBase::getTeam(%this) == GameBase::getTeam(%object)) {
		%name = GameBase::getDataName(%this);
		if(%name.className == Generator || %name.className == Station) { 
			%TDS = $Server::TeamDamageScale;
			%dValue = %damageLevel + %value * %TDS;
			%disable = GameBase::getDisabledDamage(%this);
			if(!$Server::TourneyMode && %dValue > %disable - 0.05) {
            if(%damageLevel > %disable - 0.05)
               return;
            else
               %dValue = %disable - 0.05;
			}
		}
	}
	else
	{
		GameBase::setDamageLevel(%this,%dValue);
	}
}
function StaticShape::shieldDamage(%this,%type,%value,%pos,%vec,%mom,%object)
{
	%damageLevel = GameBase::getDamageLevel(%this);
   %this.lastDamageObject = %object;
   %this.lastDamageTeam = GameBase::getTeam(%object);
	if (%this.shieldStrength) {
		%energy = GameBase::getEnergy(%this);
		%strength = %this.shieldStrength;
		if (%type == $ShrapnelDamageType)
			%strength *= 0.5;
		else
			if (%type == $MortarDamageType)
				%strength *= 0.25;
			else
				if (%type == $BlasterDamageType)
					%strength *= 2.0;
		%absorb = %energy * %strength;
		if (%value < %absorb) {
			GameBase::setEnergy(%this,%energy - (%value / %strength));
			%centerPos = getBoxCenter(%this);
			%sphereVec = findPointOnSphere(getBoxCenter(%object),%centerPos,%vec,%this);
			%centerPosX = getWord(%centerPos,0);
			%centerPosY = getWord(%centerPos,1);
			%centerPosZ = getWord(%centerPos,2);

			%pointX = getWord(%pos,0);
			%pointY = getWord(%pos,1);
			%pointZ = getWord(%pos,2);

			%newVecX = %centerPosX - %pointX;
			%newVecY = %centerPosY - %pointY;
			%newVecZ = %centerPosZ - %pointZ;
			%norm = Vector::normalize(%newVecX @ " " @ %newVecY @ " " @ %newVecZ);
			%zOffset = 0;
			if(GameBase::getDataName(%this) == PulseSensor)
				%zOffset = (%pointZ-%centerPosZ) * 0.5;
			GameBase::activateShield(%this,%sphereVec,%zOffset);
		}
		else {
			GameBase::setEnergy(%this,0);
			StaticShape::onDamage(%this,%type,%value - %absorb,%pos,%vec,%mom,%object);
		}
	}
	else {
		StaticShape::onDamage(%this,%type,%value,%pos,%vec,%mom,%object);
	}
}
function StaticShape::onPower(%this,%power,%generator){	if (%power) GameBase::playSequence(%this,0,"power");	else GameBase::stopSequence(%this,0);}
function StaticShape::onEnabled(%this){	if (GameBase::isPowered(%this)) GameBase::playSequence(%this,0,"power");}
function StaticShape::onDisabled(%this){	GameBase::stopSequence(%this,0);}
function PoweredElectricalBeam::onPower(%this, %power, %generator){ if(%power) GameBase::startFadeIn(%this); else GameBase::startFadeOut(%this); }
function FlagStand::onDamage(){}
function Generator::onEnabled(%this)	{	GameBase::setActive(%this,true);}
function Generator::onDisabled(%this)	{	GameBase::stopSequence(%this,0); 	GameBase::generatePower(%this, false);}
function Generator::onDestroyed(%this)	{	Generator::onDisabled(%this);	StaticShape::objectiveDestroyed(%this); }
function Generator::onActivate(%this)	{	GameBase::playSequence(%this,0,"power");	GameBase::generatePower(%this, true);}
function Generator::onDeactivate(%this)	{	GameBase::stopSequence(%this,0); 	GameBase::generatePower(%this, false);}
function DepPlatMediumVert::onDestroyed(%this){	StaticShape::onDestroyed(%this);}
function DepPlatLargeVert::onDestroyed(%this){	StaticShape::onDestroyed(%this);}
function DepPlatSmallVert::onDestroyed(%this){	StaticShape::onDestroyed(%this);}
function DepPlatSmallHorz::onDestroyed(%this){	StaticShape::onDestroyed(%this);}
function DepPlatLargeHorz::onDestroyed(%this){	StaticShape::onDestroyed(%this);}
function DepPlatMediumHorz::onDestroyed(%this){	StaticShape::onDestroyed(%this);}
function StaticDoorForceField::onCollision(%this, %object)
{
	%owner = $owner[%this];
	%clientId = Player::getClient(%object);
	%name = Client::getName(%clientId);
		
        if(IsInCommaList($grouplist[%owner], %name) || %name == %owner)
	{
	
		if($recreatingfField[%this] == "")
		{
			Client::sendMessage(%clientId,0,"Access granted.");

			%pos = GameBase::getPosition(%this);
			%rot = GameBase::getRotation(%this);
			//refreshing $owner in case the new force field has a different ID
			%backupowner = $owner[%this];
			$owner[%this] = "";
			//
			deleteObject(%this);
	
			$recreatingfField[%this] = 1;
			schedule("RecreateForceField(\"" @ %this @ "\", \"" @ %pos @ "\", \"" @ %rot @ "\", \"" @ %backupowner @ "\");", 3);
		}
	}
	else
	{
		Client::sendMessage(%clientId,0,"Access denied.~wError_Message.wav");
	}
}
function RecreateForceField(%this, %pos, %rot, %backupowner)
{
	%fField = newObject("","StaticShape",StaticDoorForceField,true);
	$owner[%fField] = %backupowner;
	addToSet("MissionCleanup", %fField);
	GameBase::setPosition(%fField, %pos);
	GameBase::setRotation(%fField, %rot);
	$recreatingfField[%this] = "";
}
function StaticDoorForceField::onDestroyed(%this)
{
	$owner[%this] = "";
	StaticShape::onDestroyed(%this);
}
