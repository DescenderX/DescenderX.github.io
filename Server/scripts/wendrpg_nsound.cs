//______________________________________________________________________________________________________________________________________________________
// DescX Notes:
//	
//		WTF is this 255 max sounds my god my god NOOOOOooooOOooOOo!!!
//		I instantly decimated maximum number of sounds. Blinked, boom,
//		no more room. Oh well!
//		

//----------------------------------------------------------------------------
// IMPORTANT: 3d voice profile must go first (if voices are allowed)
SoundProfileData Profile3dVoice 			{ baseVolume = 0; minDistance = 10.0; maxDistance = 70.0; flags = SFX_IS_HARDWARE_3D; };
SoundProfileData Profile2d 					{ baseVolume = 0.0; };
SoundProfileData Profile2dLoop 				{ baseVolume = 0.0; flags = SFX_IS_LOOPING; }; 
SoundProfileData Profile3dNear 				{ baseVolume = 0; minDistance = 5.0; maxDistance = 100.0; flags = SFX_IS_HARDWARE_3D; }; 
SoundProfileData Profile3dMedium 			{ baseVolume = 0; minDistance = 8.0; maxDistance = 200.0; flags = SFX_IS_HARDWARE_3D; }; 
SoundProfileData Profile3dFar 				{ baseVolume = 0; minDistance = 8.0; maxDistance = 500.0; flags = SFX_IS_HARDWARE_3D; }; 
SoundProfileData Profile3dLudicrouslyFar 	{ baseVolume = 0; minDistance = 2.0; maxDistance = 700.0; flags = SFX_IS_HARDWARE_3D; }; 
SoundProfileData Profile3dNearLoop 			{ baseVolume = 0; minDistance = 2.0; maxDistance = 40.0; flags = { SFX_IS_HARDWARE_3D, SFX_IS_LOOPING }; }; 
SoundProfileData Profile3dMediumLoop 		{ baseVolume = 0; minDistance = 2.0; maxDistance = 100.0; flags = { SFX_IS_HARDWARE_3D, SFX_IS_LOOPING }; }; 
SoundProfileData Profile3dFoot 				{ baseVolume = 0; minDistance = 2.0; maxDistance = 30.0; flags = SFX_IS_HARDWARE_3D; }; 
SoundProfileData Profile3dFarLoop 			{ baseVolume = 0; minDistance = 2.0; maxDistance = 500.0; flags = { SFX_IS_HARDWARE_3D, SFX_IS_LOOPING }; }; 
SoundProfileData Profile3dVeryFarLoop 		{ baseVolume = 0; minDistance = 2.0; maxDistance = 1000.0; flags = { SFX_IS_HARDWARE_3D, SFX_IS_LOOPING }; }; 
SoundProfileData Profile3dVeryVeryFarLoop 	{ baseVolume = 0; minDistance = 2.0; maxDistance = 2500.0; flags = { SFX_IS_HARDWARE_3D, SFX_IS_LOOPING }; };

//______________________________________________________________________________________________________________________________________________________
SoundData NoSound { wavFileName = ""; profile = Profile3dNear; };
SoundData SoundLandOnGround { wavFileName = "Land_On_Ground.wav"; profile = Profile3dNear; };
SoundData SoundPlayerDeath { wavFileName = "player_death.wav"; profile = Profile3dMedium; };
SoundData SoundDoorClose { wavFileName = "door2.wav"; profile = Profile3dMedium; };
SoundData SoundWordsmithGeneric { wavFileName = "holdable.wav"; profile = Profile3dNear; };
SoundData SoundElevatorBlocked { wavFileName = "turret_whir.wav"; profile = Profile3dNear; };
SoundData SoundElevatorStart { wavFileName = "elevator1.wav"; profile = Profile3dNear; };
SoundData SoundObjectHarp { wavFileName = "object_harp.wav"; profile = Profile3dNear; };
SoundData SoundLFootRSoft { wavFileName = "footsoft1.wav"; profile = Profile3dFoot; };
SoundData SoundLFootRHard { wavFileName = "foothard1.wav"; profile = Profile3dFoot; };
SoundData SoundLFootLSoft { wavFileName = "footsoft2.wav"; profile = Profile3dFoot; };
SoundData SoundLFootLHard { wavFileName = "foothard2.wav"; profile = Profile3dFoot; };
SoundData SoundMFootRSoft { wavFileName = "mfootrsoft.wav"; profile = Profile3dFoot; };
SoundData SoundMFootRHard { wavFileName = "mfootrhard.wav"; profile = Profile3dFoot; };
SoundData SoundMFootLSoft { wavFileName = "mfootlsoft.wav"; profile = Profile3dFoot; };
SoundData SoundMFootLHard { wavFileName = "mfootlhard.wav"; profile = Profile3dFoot; };
SoundData SoundHFootRSoft { wavFileName = "hfootrsoft.wav"; profile = Profile3dFoot; };
SoundData SoundHFootRHard { wavFileName = "hfootrhard.wav"; profile = Profile3dFoot; };
SoundData SoundHFootLSoft { wavFileName = "hfootlsoft.wav"; profile = Profile3dFoot; };
SoundData SoundHFootLHard { wavFileName = "hfootlhard.wav"; profile = Profile3dFoot; };
SoundData SoundPlasmaTurretFire { wavFileName = "turretFire4.wav"; profile = Profile3dMedium; };
SoundData SoundSpinUp { wavFileName = "Machgun3.wav"; profile = Profile3dNear; };
SoundData SoundHolySmite { wavFileName = "holysmite.wav"; profile = Profile3dNear; };
SoundData SoundLaserHit { wavFileName = "laserhit.wav"; profile = Profile3dMedium; };
SoundData SoundHolyLaser { wavFileName = "tgt_laser.wav"; profile = Profile3dNear; };
SoundData SoundMortarReload { wavFileName = "mortar_reload.wav"; profile = Profile3dNear; };
SoundData SoundMortarFire { wavFileName = "mortar_fire.wav"; profile = Profile3dNear; };
SoundData SoundFireSeeking { wavFileName = "seek_fire.wav"; profile = Profile3dNear; };
SoundData SoundMineActivate { wavFileName = "mine_act.wav"; profile = Profile3dNear; };
SoundData SoundFloatMineTarget { wavFileName = "float_target.wav"; profile = Profile3dNear; };
SoundData SoundFireFlierRocket { wavFileName = "flierrocket.wav"; profile = Profile3dMedium; };
SoundData SoundELFFire { wavFileName = "elf_fire.wav"; profile = Profile3dNear; };
SoundData SoundShockTarget { wavFileName = "lightning_idle.wav"; profile = Profile3dNear; };
SoundData SoundPickupItem { wavFileName = "Pku_weap.wav"; profile = Profile3dNear; };
SoundData SoundPickupHealth { wavFileName = "Pku_hlth.wav"; profile = Profile3dNear; };
SoundData SoundPickupBackpack { wavFileName = "Dryfire1.wav"; profile = Profile3dNear; };
SoundData SoundPotionSwish { wavFileName = "potion.wav"; profile = Profile3dNear; };
SoundData SoundPickupAmmo { wavFileName = "Pku_ammo.wav"; profile = Profile3dNear; };
SoundData SoundUseInventoryStation { wavFileName = "inv_use.wav"; profile = Profile3dNear; };
SoundData SoundActivateMotionSensor { wavFileName = "motion_activate.wav"; profile = Profile3dNear; };
SoundData SoundCapturedTower { wavFileName = "CapturedTower.wav"; profile = Profile3dNear; };
SoundData SoundFlagReturned { wavFileName = "flagreturn.wav"; profile = Profile3dMedium; };
SoundData SoundFlagPickup { wavFileName = "flag1.wav"; profile = Profile3dMedium; };
SoundData SoundSmokerAttack { wavFileName = "flyer_mount.wav"; profile = Profile3dMedium; };
SoundData SoundHitBF { wavFileName = "HitBF.wav"; profile = Profile3dFar; };
SoundData ricochet1 { wavFileName = "ricoche1.wav"; profile = Profile3dNear; };
SoundData rocketExplosion { wavFileName = "rockexp.wav"; profile = Profile3dLudicrouslyFar; };
SoundData shockExplosion { wavFileName = "shockexp.wav"; profile = Profile3dMedium; };
SoundData mineExplosion { wavFileName = "mine_exp.wav"; profile = Profile3dFar; };
SoundData floatMineExplosion { wavFileName = "float_explode.wav"; profile = Profile3dFar; };
SoundData debrisSmallExplosion { wavFileName = "debris_small.wav"; profile = Profile3dNear; };
SoundData debrisMediumExplosion { wavFileName = "debris_medium.wav"; profile = Profile3dMedium; };
SoundData debrisLargeExplosion { wavFileName = "debris_large.wav"; profile = Profile3dFar; };
SoundData SoundFlyerDismount { wavFileName = "flyer_dismount.wav"; profile = Profile3dNear; };
SoundData SoundFlierCrash { wavFileName = "crash.wav"; profile = Profile3dMedium; };
SoundData SoundSpawn2 { wavFileName = "spawn2.wav"; profile = Profile3dMedium; };
SoundData SoundSplash1 { wavFileName = "water3.wav"; profile = Profile3dMedium; };
SoundData SoundBladeSwingSM { wavFileName = "swing_blade_sm.wav"; profile = Profile3dNear; };
SoundData SoundBladeSwingLG { wavFileName = "swing_blade_lg.wav"; profile = Profile3dNear; };
SoundData SoundBluntSwingSM { wavFileName = "swing_blunt_sm.wav"; profile = Profile3dNear; };
SoundData SoundBluntSwingLG { wavFileName = "swing_blunt_lg.wav"; profile = Profile3dNear; };
SoundData SoundAxeSwingSM { wavFileName = "swing_axe_sm.wav"; profile = Profile3dNear; };
SoundData SoundAxeSwingLG { wavFileName = "swing_axe_lg.wav"; profile = Profile3dNear; };
SoundData SoundPoleSwingSM { wavFileName = "swing_pole_sm.wav"; profile = Profile3dNear; };
SoundData SoundPoleSwingLG { wavFileName = "swing_pole_lg.wav"; profile = Profile3dNear; };
SoundData SoundSwordHit1 { wavFileName = "hit1.wav"; profile = Profile3dNear; };
SoundData SoundArrowHit { wavFileName = "arrowhit.wav"; profile = Profile3dNear; };
SoundData SoundHitFlesh { wavFileName = "Hit_Flesh.wav"; profile = Profile3dNear; };
SoundData SoundHitLeather { wavFileName = "Hit_Leather.wav"; profile = Profile3dNear; };
SoundData SoundHitChain { wavFileName = "Hit_Chain.wav"; profile = Profile3dNear; };
SoundData SoundHitPlate { wavFileName = "Hit_Plate.wav"; profile = Profile3dNear; };
SoundData SoundHitShield { wavFileName = "Hit_Shield.wav"; profile = Profile3dNear; };
SoundData SoundMoney1 { wavFileName = "money.wav"; profile = Profile3dNear; };
SoundData SoundSmith { wavFileName = "smith.wav"; profile = Profile3dMedium; };
SoundData SoundDrown1 { wavFileName = "drown.wav"; profile = Profile3dNear; };
SoundData SoundHitore { wavFileName = "pickaxe_use_001.wav"; profile = Profile3dNear; };
SoundData SoundHitore2 { wavFileName = "AXEHIT1.wav"; profile = Profile3dNear; };
SoundData ExplodeLM { wavFileName = "ExplodeLM.wav"; profile = Profile3dFar; };
SoundData ActivateBF { wavFileName = "ActivateBF.wav"; profile = Profile3dNear; };
SoundData Portal11 { wavFileName = "Portal11.wav"; profile = Profile3dNear; };
SoundData ActivateCH { wavFileName = "ActivateCH.wav"; profile = Profile3dNear; };
SoundData ActivateAR { wavFileName = "ActivateAR.wav"; profile = Profile3dNear; };
SoundData DeActivateWA { wavFileName = "DeActivateWA.wav"; profile = Profile3dNear; };
SoundData ActivateFK { wavFileName = "ActivateFK.wav"; profile = Profile3dNear; };
SoundData ActivateDE { wavFileName = "ActivateDE.wav"; profile = Profile3dNear; };
SoundData ActivateAM { wavFileName = "ActivateAM.wav"; profile = Profile3dNear; };
SoundData DeflectAS { wavFileName = "DeflectAS.wav"; profile = Profile3dNear; };
SoundData ActivateAB { wavFileName = "ActivateAB.wav"; profile = Profile3dNear; };
SoundData LaunchFB { wavFileName = "LaunchFB.wav"; profile = Profile3dFar; };
SoundData HitPawnDT { wavFileName = "HitPawnDT.wav"; profile = Profile3dFar; };
SoundData ImpactTR { wavFileName = "ImpactTR.wav"; profile = Profile3dNear; };
SoundData Reflected { wavFileName = "Reflected.wav"; profile = Profile3dFar; };
SoundData UnravelAM { wavFileName = "UnravelAM.wav"; profile = Profile3dNear; };
SoundData LaunchLS { wavFileName = "LaunchLS.wav"; profile = Profile3dNear; };
SoundData LaunchET { wavFileName = "LaunchET.wav"; profile = Profile3dMedium; };
SoundData LoopLS { wavFileName = "LoopLS.wav"; profile = Profile3dNear; };
SoundData LoopLG { wavFileName = "LoopLG.wav"; profile = Profile3dNear; };
SoundData LoopLT { wavFileName = "LoopLT.wav"; profile = Profile3dNear; };
SoundData RespawnA { wavFileName = "RespawnA.wav"; profile = Profile3dNear; };
SoundData RespawnB { wavFileName = "RespawnB.wav"; profile = Profile3dNear; };
SoundData RespawnC { wavFileName = "RespawnC.wav"; profile = Profile3dNear; };
SoundData Portal6 { wavFileName = "Portal6.wav"; profile = Profile3dNear; };
SoundData PlaceSeal { wavFileName = "PlaceSeal.wav"; profile = Profile3dNear; };
SoundData ActivateTR { wavFileName = "ActivateTR.wav"; profile = Profile3dNear; };
SoundData ActivateTD { wavFileName = "ActivateTD.wav"; profile = Profile3dNear; };
SoundData BonusStateExpire { wavFileName = "DeActivateIC.wav"; profile = Profile3dNear; };
SoundData AbsorbABS { wavFileName = "AbsorbABS.wav"; profile = Profile3dNear; };
SoundData SoundThunder1 { wavFileName = "thunder1.wav"; profile = Profile3dMedium; };
SoundData LoopSP { wavFileName = "LoopSP.wav"; profile = Profile3dNear; };
SoundData ActivateAS { wavFileName = "ActivateAS.wav"; profile = Profile3dNear; };
SoundData CrossbowShoot1 { wavFileName = "Crossbow_Shoot1.wav"; profile = Profile3dNear; };
SoundData BowShoot1 { wavFileName = "bowattack1.wav"; profile = Profile3dNear; };
SoundData RockLauncherFire { wavFileName = "rocklauncher.wav"; profile = Profile3dNear; };
SoundData JusticeStaffFire { wavFileName = "justicefire.wav"; profile = Profile3dNear; };
SoundData CrossbowSwitch1 { wavFileName = "Crossbow_Switch1.wav"; profile = Profile3dNear; };
SoundData SoundEquipWand { wavFileName = "usewand.wav"; profile = Profile3dNear; };
SoundData SoundGliders { wavFileName = "Gliders.wav"; profile = Profile3dMediumLoop; };
SoundData SoundWindWalkers { wavFileName = "heavy_thrust.wav"; profile = Profile3dMediumLoop; };
SoundData SoundBoat { wavFileName = "AmbBoat2m.wav"; profile = Profile3dMediumLoop; };
SoundData SoundLevelUp { wavFileName = "LevelUp.wav"; profile = Profile3dNear; };
SoundData SoundCanSmith { wavFileName = "canSmith.wav"; profile = Profile3dNear; };
SoundData SoundGlassBreak { wavFileName = "glassbreak.wav"; profile = Profile3dNear; };
SoundData SoundMedic4Spell { wavFileName = "medic4spell.wav"; profile = Profile3dNear; };
SoundData SoundFishWalk { wavFileName = "fishwalk.wav"; profile = Profile3dNear; };
SoundData SoundSpellcast { wavFileName = "spellcast.wav"; profile = Profile3dNear; };
SoundData SoundShieldHit { wavFileName = "shieldhit.wav"; profile = Profile3dNear; };
SoundData SoundToxinFire { wavFileName = "qk_sfx_ui_frag.wav"; profile = Profile3dNear; };
SoundData SoundToxinBoom { wavFileName = "gibimp3.wav"; profile = Profile3dNear; };
SoundData SoundFlintCasterFire { wavFileName = "flintcaster.wav"; profile = Profile3dNear; };
SoundData SoundSwordClang { wavFileName = "Button2.wav"; profile = Profile3dNear; };
SoundData SoundBleedStab { wavFileName = "bleed.wav"; profile = Profile3dNear; };
SoundData SoundInscribeOK { wavFileName = "inscribe_ok.wav"; profile = Profile3dNear; };
SoundData SoundInscribeFail { wavFileName = "inscribe_fail.wav"; profile = Profile3dNear; };
// OGRE 
SoundData SoundOgreDeath1 { wavFileName = "OgreDeath1.wav"; profile = Profile3dNear; };
SoundData SoundOgreAcquired1 { wavFileName = "OgreAcquired1.wav"; profile = Profile3dNear; };
SoundData SoundOgreTaunt1 { wavFileName = "OgreTaunt1.wav"; profile = Profile3dNear; };
SoundData SoundOgreRandom1 { wavFileName = "OgreRandom1.wav"; profile = Profile3dNear; };
SoundData SoundOgreHit1 { wavFileName = "OgreHit1.wav"; profile = Profile3dNear; };
// ZOMBIE 
SoundData SoundUndeadDeath1 { wavFileName = "UndeadDeath1.wav"; profile = Profile3dNear; };
SoundData SoundUndeadAcquired1 { wavFileName = "UndeadAcquired1.wav"; profile = Profile3dNear; };
SoundData SoundUndeadRandom1 { wavFileName = "UndeadRandom1.wav"; profile = Profile3dNear; };
SoundData SoundUndeadTaunt1 { wavFileName = "UndeadTaunt1.wav"; profile = Profile3dNear; };
SoundData SoundUndeadHit1 { wavFileName = "UndeadHit1.wav"; profile = Profile3dNear; };
SoundData SoundUndeadHit2 { wavFileName = "UndeadHit2.wav"; profile = Profile3dNear; };
//UBER 
SoundData SoundUberDeath1 { wavFileName = "UberDeath1.wav"; profile = Profile3dNear; };
SoundData SoundUberAcquired1 { wavFileName = "UberAcquired1.wav"; profile = Profile3dNear; };
SoundData SoundUberTaunt { wavFileName = "UberAcquired2.wav"; profile = Profile3dNear; };
SoundData SoundUberHit1 { wavFileName = "UberHit1.wav"; profile = Profile3dNear; };
SoundData SoundUberRandom1 { wavFileName = "UberRandom1.wav"; profile = Profile3dNear; };
//MINOTAUR 
SoundData SoundMinotaurDeath1 { wavFileName = "MinotaurDeath1.wav"; profile = Profile3dNear; };
SoundData SoundMinotaurAcquired1 { wavFileName = "MinotaurAcquired1.wav"; profile = Profile3dNear; };
SoundData SoundMinotaurTaunt { wavFileName = "MinotaurAcquired2.wav"; profile = Profile3dNear; };
SoundData SoundMinotaurHit1 { wavFileName = "MinotaurHit1.wav"; profile = Profile3dNear; };
//MINOTAUR FEMALE
SoundData Soundminotauridle { wavFileName = "minotaur_idle.wav"; profile = Profile3dNear; };
SoundData Soundminotaurfemacquired { wavFileName = "mintaurfem_acquired.wav"; profile = Profile3dNear; };
SoundData Soundminotaurfemdeath { wavFileName = "mintaurfem_death.wav"; profile = Profile3dNear; };
SoundData Soundminotaurfemhit1 { wavFileName = "mintaurfem_hit1.wav"; profile = Profile3dNear; };
SoundData Soundminotaurfemidle { wavFileName = "mintaurfem_idle.wav"; profile = Profile3dNear; };
SoundData Soundminotaurfemtaunt { wavFileName = "mintaurfem_taunt.wav"; profile = Profile3dNear; };
//GOBLIN 
SoundData SoundGoblinDeath1 { wavFileName = "GoblinDeath1.wav"; profile = Profile3dNear; };
SoundData SoundGoblinAcquired1 { wavFileName = "GoblinAcquired1.wav"; profile = Profile3dNear; };
SoundData SoundGoblinTaunt1 { wavFileName = "GoblinTaunt1.wav"; profile = Profile3dNear; };
SoundData SoundGoblinRandom1 { wavFileName = "GoblinRandom1.wav"; profile = Profile3dNear; };
SoundData SoundGoblinHit1 { wavFileName = "GoblinHit1.wav"; profile = Profile3dNear; };
SoundData SoundGoblinHit2 { wavFileName = "GoblinHit2.wav"; profile = Profile3dNear; };
//GNOLL 
SoundData SoundGnollDeath1 { wavFileName = "GnollDeath2.wav"; profile = Profile3dNear; };
SoundData SoundGnollAcquired1 { wavFileName = "GnollAcquired1.wav"; profile = Profile3dNear; };
SoundData SoundGnollTaunt1 { wavFileName = "GnollTaunt1.wav"; profile = Profile3dNear; };
SoundData SoundGnollRandom1 { wavFileName = "GnollRandom1.wav"; profile = Profile3dNear; };
SoundData SoundGnollHit1 { wavFileName = "GnollHit1.wav"; profile = Profile3dNear; };
SoundData SoundGnollHit2 { wavFileName = "GnollHit2.wav"; profile = Profile3dNear; };
//GNOLL FEMALE
SoundData Soundgnollfemacquired { wavFileName = "gnollfem_acquired.wav"; profile = Profile3dNear; };
SoundData Soundgnollfemdeath { wavFileName = "gnollfem_death.WAV"; profile = Profile3dNear; };
SoundData Soundgnollfemhit1 { wavFileName = "gnollfem_hit1.WAV"; profile = Profile3dNear; };
SoundData Soundgnollfemhit2 { wavFileName = "gnollfem_hit2.WAV"; profile = Profile3dNear; };
SoundData Soundgnollfemidle { wavFileName = "gnollfem_idle.WAV"; profile = Profile3dNear; };
SoundData Soundgnollfemtaunt { wavFileName = "gnollfem_taunt.wav"; profile = Profile3dNear; };
//AMAZON
SoundData Soundamazonacquired { wavFileName = "amazon_acquired.wav"; profile = Profile3dNear; };
SoundData Soundamazondeath { wavFileName = "amazon_death.wav"; profile = Profile3dNear; };
SoundData Soundamazonhit1 { wavFileName = "amazon_hit1.wav"; profile = Profile3dNear; };
SoundData Soundamazonhit2 { wavFileName = "amazon_hit2.wav"; profile = Profile3dNear; };
SoundData Soundamazonidle { wavFileName = "amazon_idle.wav"; profile = Profile3dNear; };
SoundData Soundamazontaunt { wavFileName = "amazon_taunt.wav"; profile = Profile3dNear; };
//DELUDED
SoundData Sounddeludedacquired { wavFileName = "deluded_acquired.wav"; profile = Profile3dNear; };
SoundData Sounddeludeddeath { wavFileName = "deluded_death.wav"; profile = Profile3dNear; };
SoundData Sounddeludedhit1 { wavFileName = "deluded_hit1.wav"; profile = Profile3dNear; };
SoundData Sounddeludedhit2 { wavFileName = "deluded_hit2.wav"; profile = Profile3dNear; };
SoundData Sounddeludedidle { wavFileName = "deluded_idle.wav"; profile = Profile3dNear; };
SoundData Sounddeludedtaunt1 { wavFileName = "deluded_taunt1.wav"; profile = Profile3dNear; };
//DEMON
SoundData Sounddemonacquired { wavFileName = "demon_acquired.wav"; profile = Profile3dNear; };
SoundData Sounddemondeath { wavFileName = "demon_death.wav"; profile = Profile3dNear; };
SoundData Sounddemonhit1 { wavFileName = "demon_hit1.wav"; profile = Profile3dNear; };
SoundData Sounddemonhit2 { wavFileName = "demon_hit2.wav"; profile = Profile3dNear; };
SoundData Sounddemonidle { wavFileName = "demon_idle.wav"; profile = Profile3dNear; };
SoundData Sounddemontaunt { wavFileName = "demon_taunt.wav"; profile = Profile3dNear; };
//DEMON FEMALE
SoundData Sounddemonfemacquired { wavFileName = "demonfem_acquired.wav"; profile = Profile3dNear; };
SoundData Sounddemonfemdeath { wavFileName = "demonfem_death.wav"; profile = Profile3dNear; };
SoundData Sounddemonfemhit1 { wavFileName = "demonfem_hit1.wav"; profile = Profile3dNear; };
SoundData Sounddemonfemhit2 { wavFileName = "demonfem_hit2.wav"; profile = Profile3dNear; };
SoundData Sounddemonfemidle { wavFileName = "demonfem_idle.wav"; profile = Profile3dNear; };
SoundData Sounddemonfemtaunt { wavFileName = "demonfem_taunt.wav"; profile = Profile3dNear; };
//ELF MALE
SoundData Soundelfacquired { wavFileName = "elf_acquired.wav"; profile = Profile3dNear; };
SoundData Soundelfdeath1 { wavFileName = "elf_death1.wav"; profile = Profile3dNear; };
SoundData Soundelfhit1 { wavFileName = "elf_hit1.wav"; profile = Profile3dNear; };
SoundData Soundelfhit2 { wavFileName = "elf_hit2.wav"; profile = Profile3dNear; };
SoundData Soundelfidle { wavFileName = "elf_idle.wav"; profile = Profile3dNear; };
SoundData Soundelftaunt { wavFileName = "elf_taunt.wav"; profile = Profile3dNear; };
//ELF FEMALE
SoundData Soundelffemacquired { wavFileName = "elffem_acquired.wav"; profile = Profile3dNear; };
SoundData Soundelffemdeath { wavFileName = "elffem_death.wav"; profile = Profile3dNear; };
SoundData Soundelffemhit1 { wavFileName = "elffem_hit1.wav"; profile = Profile3dNear; };
SoundData Soundelffemhit2 { wavFileName = "elffem_hit2.wav"; profile = Profile3dNear; };
SoundData Soundelffemidle { wavFileName = "elffem_idle.wav"; profile = Profile3dNear; };
SoundData Soundelffemtaunt { wavFileName = "elffem_taunt.wav"; profile = Profile3dNear; };
//IMP
SoundData Soundimpacquired { wavFileName = "imp_acquired.wav"; profile = Profile3dNear; };
SoundData Soundimpdeath { wavFileName = "imp_death.wav"; profile = Profile3dNear; };
SoundData Soundimphit1 { wavFileName = "imp_hit1.wav"; profile = Profile3dNear; };
SoundData Soundimphit2 { wavFileName = "imp_hit2.wav"; profile = Profile3dNear; };
SoundData Soundimpidle { wavFileName = "imp_idle.wav"; profile = Profile3dNear; };
SoundData Soundimptaunt { wavFileName = "imp_taunt.wav"; profile = Profile3dNear; };
//LOST SOUL
SoundData Soundlostsoulacquired { wavFileName = "lostsoul_acquired.wav"; profile = Profile3dNear; };
SoundData Soundlostsouldeath { wavFileName = "lostsoul_death.wav"; profile = Profile3dNear; };
SoundData Soundlostsoulhit1 { wavFileName = "lostsoul_hit1.wav"; profile = Profile3dNear; };
SoundData Soundlostsoulhit2 { wavFileName = "lostsoul_hit2.wav"; profile = Profile3dNear; };
SoundData Soundlostsoulidle { wavFileName = "lostsoul_idle.wav"; profile = Profile3dNear; };
SoundData Soundlostsoultaunt { wavFileName = "lostsoul_taunt.WAV"; profile = Profile3dNear; };
// ORC
SoundData Soundorcacquired { wavFileName = "orc_acquired.wav"; profile = Profile3dNear; };
SoundData Soundorcdeath { wavFileName = "orc_death.wav"; profile = Profile3dNear; };
SoundData Soundorchit1 { wavFileName = "orc_hit1.wav"; profile = Profile3dNear; };
SoundData Soundorchit2 { wavFileName = "orc_hit2.wav"; profile = Profile3dNear; };
SoundData Soundorcidle { wavFileName = "orc_idle.wav"; profile = Profile3dNear; };
SoundData Soundorctaunt { wavFileName = "orc_taunt.wav"; profile = Profile3dNear; };
//ORC FEMALE
SoundData Soundorcfemacquired { wavFileName = "orcfem_acquired.wav"; profile = Profile3dNear; };
SoundData Soundorcfemdeath { wavFileName = "orcfem_death.wav"; profile = Profile3dNear; };
SoundData Soundorcfemhit1 { wavFileName = "orcfem_hit1.wav"; profile = Profile3dNear; };
SoundData Soundorcfemhit2 { wavFileName = "orcfem_hit2.wav"; profile = Profile3dNear; };
SoundData Soundorcfemidle { wavFileName = "orcfem_idle.wav"; profile = Profile3dNear; };
SoundData Soundorcfemtaunt { wavFileName = "orcfem_taunt.wav"; profile = Profile3dNear; };
//OUTLAW
SoundData Soundoutlawacquired { wavFileName = "outlaw_acquired.wav"; profile = Profile3dNear; };
SoundData Soundoutlawdeath { wavFileName = "outlaw_death.wav"; profile = Profile3dNear; };
SoundData Soundoutlawhit1 { wavFileName = "outlaw_hit1.wav"; profile = Profile3dNear; };
SoundData Soundoutlawhit2 { wavFileName = "outlaw_hit2.wav"; profile = Profile3dNear; };
SoundData Soundoutlawidle { wavFileName = "outlaw_idle.wav"; profile = Profile3dNear; };
SoundData Soundoutlawtaunt { wavFileName = "outlaw_taunt.wav"; profile = Profile3dNear; };
//OUTLAW FEMALE
SoundData Soundoutlawfemaleacquired { wavFileName = "outlawfemale_acquired.wav"; profile = Profile3dNear; };
SoundData Soundoutlawfemaledeath { wavFileName = "outlawfemale_death.wav"; profile = Profile3dNear; };
SoundData Soundoutlawfemalehit1 { wavFileName = "outlawfemale_hit1.wav"; profile = Profile3dNear; };
SoundData Soundoutlawfemalehit2 { wavFileName = "outlawfemale_hit2.wav"; profile = Profile3dNear; };
SoundData Soundoutlawfemaleidle { wavFileName = "outlawfemale_idle.wav"; profile = Profile3dNear; };
SoundData Soundoutlawfemaletaunt { wavFileName = "outlawfemale_taunt.wav"; profile = Profile3dNear; };
//SKELETON
SoundData Soundskeletonacquired { wavFileName = "skeleton_acquired.wav"; profile = Profile3dNear; };
SoundData Soundskeletondeath { wavFileName = "skeleton_death.wav"; profile = Profile3dNear; };
SoundData Soundskeletonhit1 { wavFileName = "skeleton_hit1.wav"; profile = Profile3dNear; };
SoundData Soundskeletonhit2 { wavFileName = "skeleton_hit2.wav"; profile = Profile3dNear; };
SoundData Soundskeletonidle { wavFileName = "skeleton_idle.wav"; profile = Profile3dNear; };
SoundData Soundskeletontaunt { wavFileName = "skeleton_taunt.wav"; profile = Profile3dNear; };

//______________________________________________________________________________________________________________________________________________________
function InitSoundPoints() {
	dbecho($dbechoMode, "InitSoundPoints()");
	%group = nameToID("MissionGroup\\SoundPoints");
	if(%group != -1) {
		for(%i = 0; %i <= Group::objectCount(%group)-1; %i++) {
			%this = Group::getObject(%group, %i);
			%info = Object::getName(%this);
			if(%info != "") GameBase::playSound(%this, %info, 0);
		}
	}
}
function RandomRaceSound(%race, %type) {
	for(%i = 1; $RaceSound[%race, %type, %i] != ""; %i++){}
	%i--;
	%r = floor(getRandom() * %i) + 1;
	%s = $RaceSound[%race, %type, %r];
	if(%s != "")	return %s;
	else			return "NoSound";
}