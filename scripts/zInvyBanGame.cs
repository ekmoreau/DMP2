//Item Ban Script
//By DarkTiger

//step1 add invyban obj to your mission via the world creator should be under shapes -> misc
//note this will add a secondary obj called invySpawnObj witch as more settings will get too

//step 2 save close the game and open up the mission file in a text editor as doing changes in game is bit annoying 

//step 3 find invyBanObj in the mission file and there you will list of all of t2s items and armors 

//step 4 everything starts out banned so set the items you want unbanned with 0 be sure to unban one of the armors 

//step 5 now find that other object invySpawnObj this whats used for the spawn loudout
//0 being no weapon anything greater then zero will be equip with that amount ammo
//the value armor = "Light"; is the spawning armor, grenade = "Grenade"; type of grenade  
//startWep = "Blaster"; the default starting weapon note needs to be enabled for this to work 
//lastly ammoMod are ammo modifiers  ammoMod[DiscAmmo] = 10; is 10 extra discs ammoMod[DiscAmmo] = -10; is 10 less discs ammo 0 being default 

//step 6 save and test 

////////////////////////////////////////////////////////////////////////////////////////////////////
datablock StaticShapeData(invyBan){
   catagory = "misc";
   shapeFile = "flag.dts";
};

$itemArrayCount = 0;
$itemArray[$itemArrayCount] = "Light"; $itemArrayCount++;
$itemArray[$itemArrayCount] = "Medium"; $itemArrayCount++;
$itemArray[$itemArrayCount] = "Heavy"; $itemArrayCount++;

$itemArray[$itemArrayCount] = "InventoryDeployable"; $itemArrayCount++;
$itemArray[$itemArrayCount] = "TurretOutdoorDeployable"; $itemArrayCount++;
$itemArray[$itemArrayCount] = "TurretIndoorDeployable";  $itemArrayCount++;
$itemArray[$itemArrayCount] = "ElfBarrelPack";            $itemArrayCount++;
$itemArray[$itemArrayCount] = "MortarBarrelPack";         $itemArrayCount++;
$itemArray[$itemArrayCount] = "PlasmaBarrelPack";         $itemArrayCount++;
$itemArray[$itemArrayCount] = "AABarrelPack";             $itemArrayCount++;
$itemArray[$itemArrayCount] = "AmmoPack";                 $itemArrayCount++;
$itemArray[$itemArrayCount] = "CloakingPack";             $itemArrayCount++;
$itemArray[$itemArrayCount] = "MotionSensorDeployable";   $itemArrayCount++;
$itemArray[$itemArrayCount] = "PulseSensorDeployable";    $itemArrayCount++;
$itemArray[$itemArrayCount] = "EnergyPack";               $itemArrayCount++;
$itemArray[$itemArrayCount] = "RepairPack";               $itemArrayCount++;
$itemArray[$itemArrayCount] = "SatchelCharge";            $itemArrayCount++;
$itemArray[$itemArrayCount] = "SensorJammerPack";         $itemArrayCount++;
$itemArray[$itemArrayCount] = "ShieldPack";               $itemArrayCount++;
$itemArray[$itemArrayCount] = "MissileBarrelPack";        $itemArrayCount++;

$itemArray[$itemArrayCount] = "Blaster";                  $itemArrayCount++;
$itemArray[$itemArrayCount] = "Disc";                     $itemArrayCount++;
$itemArray[$itemArrayCount] = "ShockLance";               $itemArrayCount++;
$itemArray[$itemArrayCount] = "Chaingun";                 $itemArrayCount++;
$itemArray[$itemArrayCount] = "Plasma";                   $itemArrayCount++;
$itemArray[$itemArrayCount] = "ELFGun";                   $itemArrayCount++;
$itemArray[$itemArrayCount] = "GrenadeLauncher";          $itemArrayCount++;
$itemArray[$itemArrayCount] = "Mortar";                   $itemArrayCount++;
$itemArray[$itemArrayCount] = "SniperRifle";              $itemArrayCount++;
$itemArray[$itemArrayCount] = "MissileLauncher";          $itemArrayCount++;
$itemArray[$itemArrayCount] = "TargetingLaser";           $itemArrayCount++;


$itemArray[$itemArrayCount] = "Mine";                     $itemArrayCount++;
$itemArray[$itemArrayCount] = "ConcussionGrenade";        $itemArrayCount++;
$itemArray[$itemArrayCount] = "CameraGrenade";            $itemArrayCount++;
$itemArray[$itemArrayCount] = "FlareGrenade";             $itemArrayCount++;
$itemArray[$itemArrayCount] = "FlashGrenade";             $itemArrayCount++;
$itemArray[$itemArrayCount] = "Grenade";                  $itemArrayCount++;

$armorArray[0] = LightMaleHumanArmor;
$armorArray[1] = MediumMaleHumanArmor;
$armorArray[2] = HeavyMaleHumanArmor;
$armorArray[3] = LightFemaleHumanArmor;
$armorArray[4] =  MediumFemaleHumanArmor;
$armorArray[5] = HeavyFemaleHumanArmor;
$armorArray[6] = LightMaleBiodermArmor;
$armorArray[7] = MediumMaleBiodermArmor;
$armorArray[8] = HeavyMaleBiodermArmor;
function invyBan::onAdd(%this, %obj){  
   Parent::onAdd(%this, %obj);
   if(!isObject(invyBanObj)){
      %obj.setName("invyBanObj");
      new scriptObject(invySpawnObj){ // default load out 
         armor = "Light";
         blaster = 1;
         disc = 20;
         shockLance = 0;
         chaingun = 100;
         plasma = 0;
         elfGun = 0;
         grenadeLauncher = 0;
         mortar = 0;
         sniperRifle = 0;
         missileLauncher = 0;
         targetingLaser = 1;

         pack = 0;//"EnergyPack" for energy pack 
         grenade = "Grenade";
         grenadeAmmo = 3;
         mine = 0;//amount of mines


         repairKit = 1;
         beacon = 3;
   
         startWep = "Blaster";

         ammoMod[DiscAmmo] = 0;
         ammoMod[ChaingunAmmo] = 0;
         ammoMod[PlasmaAmmo] = 0;
         ammoMod[GrenadeLauncherAmmo] = 0;
         ammoMod[MissileLauncherAmmo] = 0;
         ammoMod[MortarAmmo] = 0;
         ammoMod[Mine] = 0;
         ammoMod[Grenade] = 0;

      };
      MissionGroup.add(invySpawnObj);
   }
   for (%i = 0; %i < $itemArrayCount; %i++){ // we ban it at the datablock level as everythign else gets messy with packages and may conflict with other stuff 
      %item = $itemArray[%i];
      if(%obj.ban[%item] $= ""){
         %obj.ban[%item] = true;
      }
      $InvBanListDefault[$CurrentMissionType, %item] = $InvBanList[$CurrentMissionType, %item];
      $InvBanList[$CurrentMissionType, %item] = %obj.ban[%item]; 
   }
   if(!isActivePackage(invyCtrlPack)){
      activatePackage(invyCtrlPack);
   }
}

function invyBan::onRemove(%this, %Obj){
   Parent::onRemove();
   // RESTORE DEFAULT INVENTORY BAN LIST
   error("disable invyCtrlPack");
   for (%i = 0; %i < $itemArrayCount; %i++){
      %item = $itemArray[%i];
      $InvBanList[$CurrentMissionType, %item] = $InvBanListDefault[$CurrentMissionType, %item];
   }    
   deactivatePackage(invyCtrlPack);
}

package invyCtrlPack{
   function InventoryScreen::updateHud( %this, %client, %tag )
   {
      %cmt = $CurrentMissionType;
      %noSniperRifle = true;
      %armor = getArmorDatablock( %client, $NameToInv[%client.favorites[0]] );
      
      if ( %client.lastArmor !$= %armor )
      {
         %client.lastArmor = %armor;
         for ( %x = 0; %x < %client.lastNumFavs; %x++ )
            messageClient( %client, 'RemoveLineHud', "", 'inventoryScreen', %x );
         %setLastNum = true;
         error("update hud");
      }
   

   %armorList = %client.favorites[0];

   for ( %y = 0; $InvArmor[%y] !$= ""; %y++ )
   {
      %AInv = $NameToInv[$InvArmor[%y]];

      // skip current armor
      if ( $InvArmor[%y] $= %client.favorites[0] )
         continue;

      // skip banned armors
      if ( $InvBanList[%cmt, %AInv] )
         continue;

      %armorList = %armorList TAB $InvArmor[%y];
   }

      //Create - WEAPON - List
      for ( %y = 0; $InvWeapon[%y] !$= ""; %y++ )
      {
         %notFound = true;
         for ( %i = 0; %i < getFieldCount( %client.weaponIndex ); %i++ )
         {
            %WInv = $NameToInv[$InvWeapon[%y]];
            if ( ( $InvWeapon[%y] $= %client.favorites[getField( %client.weaponIndex,%i )] ) || !%armor.max[%WInv] )  
            {
               %notFound = false;
               break;
            }
            else if ( "SniperRifle" $= $NameToInv[%client.favorites[getField( %client.weaponIndex,%i )]] )
            {
               %noSniperRifle = false;
               %packList = "noSelect\tEnergy Pack\tEnergy Pack must be used when \tLaser Rifle is selected!";     
               %client.favorites[getField(%client.packIndex,0)] = "Energy Pack";
            }   
         }

         if ( !($InvBanList[%cmt, %WInv]) )
         {
            if ( %notFound && %weaponList $= "" )
               %weaponList = $InvWeapon[%y];
            else if ( %notFound )
               %weaponList = %weaponList TAB $InvWeapon[%y];
         }
      }

      //Create - PACK - List
      if ( %noSniperRifle )
      {
         if ( getFieldCount( %client.packIndex ) )
            %packList = %client.favorites[getField( %client.packIndex, 0 )];
         else
         {
            %packList = "EMPTY";
            %client.numFavs++;
         }
         for ( %y = 0; $InvPack[%y] !$= ""; %y++ )
         {
            %PInv = $NameToInv[$InvPack[%y]];
            if ( ( $InvPack[%y] !$= %client.favorites[getField( %client.packIndex, 0 )]) && 
            %armor.max[%PInv] && !($InvBanList[%cmt, %PInv]))  
               %packList = %packList TAB $Invpack[%y];
         }
      }   
      //Create - GRENADE - List
      for ( %y = 0; $InvGrenade[%y] !$= ""; %y++ )
      {
         %notFound = true;
         for(%i = 0; %i < getFieldCount( %client.grenadeIndex ); %i++)
         {
            %GInv = $NameToInv[$InvGrenade[%y]];
            if ( ( $InvGrenade[%y] $= %client.favorites[getField( %client.grenadeIndex, %i )] ) || !%armor.max[%GInv] )  
            {
               %notFound = false;
               break;
            }
         }
         if ( !($InvBanList[%cmt, %GInv]) )
         { 
            if ( %notFound && %grenadeList $= "" )
               %grenadeList = $InvGrenade[%y];
            else if ( %notFound )
               %grenadeList = %grenadeList TAB $InvGrenade[%y];
         }
      }

   //Create - MINE - List
      for ( %y = 0; $InvMine[%y] !$= "" ; %y++ )
      {
         %notFound = true;
         // -----------------------------------------------------------------------------------------------------
         // z0dd - ZOD, 4/24/02. This was broken, Fixed. 
         for(%i = 0; %i < getFieldCount( %client.mineIndex ); %i++)
         {
            %MInv = $NameToInv[$InvMine[%y]];
            if ( ( $InvMine[%y] $= %client.favorites[getField( %client.mineIndex, %i )] ) || !%armor.max[%MInv] )  
            {
               %notFound = false;
               break;
            }
         }
         // -----------------------------------------------------------------------------------------------------
         if ( !($InvBanList[%cmt, %MInv]) )
         {
            if ( %notFound && %mineList $= "" )
               %mineList = $InvMine[%y];
            else if ( %notFound )
               %mineList = %mineList TAB $InvMine[%y];
         }
      }
      %client.numFavsCount++;
      messageClient( %client, 'SetLineHud', "", %tag, 0, "Armor:", %armorList, armor, %client.numFavsCount );
      %lineCount = 1;

      for ( %x = 0; %x < %armor.maxWeapons; %x++ )
      {
         %client.numFavsCount++;
         if ( %x < getFieldCount( %client.weaponIndex ) )
         {
            %list = %client.favorites[getField( %client.weaponIndex,%x )];
            if ( %list $= Invalid )
            {
               %client.favorites[%client.numFavs] = "INVALID";
               %client.weaponIndex = %client.weaponIndex TAB %client.numFavs;
            }   
         }
         else
         {
            %list = "EMPTY";
            %client.favorites[%client.numFavs] = "EMPTY";
            %client.weaponIndex = %client.weaponIndex TAB %client.numFavs;
            %client.numFavs++;
         }
         if ( %list $= empty )
            %list = %list TAB %weaponList;
         else
            %list = %list TAB %weaponList TAB "EMPTY";
         messageClient( %client, 'SetLineHud', "", %tag, %x + %lineCount, "Weapon Slot " @ %x + 1 @ ": ", %list , weapon, %client.numFavsCount );
      }
      %lineCount = %lineCount + %armor.maxWeapons;
      
      %client.numFavsCount++;
      if ( getField( %packList, 0 ) !$= empty && %noSniperRifle )
         %packList = %packList TAB "EMPTY";
      %packText = %packList;
      %packOverFlow = "";
      if ( strlen( %packList ) > 255 )
      {
         %packText = getSubStr( %packList, 0, 255 );
         %packOverFlow = getSubStr( %packList, 255, 512 );
      }
      messageClient( %client, 'SetLineHud', "", %tag, %lineCount, "Pack:", %packText, pack, %client.numFavsCount, %packOverFlow );
      %lineCount++;
      
      for( %x = 0; %x < %armor.maxGrenades; %x++ )
      {
         %client.numFavsCount++;
         if ( %x < getFieldCount( %client.grenadeIndex ) )
         {
            %list = %client.favorites[getField( %client.grenadeIndex, %x )];
            if (%list $= Invalid)
            {
               %client.favorites[%client.numFavs] = "INVALID";
               %client.grenadeIndex = %client.grenadeIndex TAB %client.numFavs;
            }
         }
         else
         {
            %list = "EMPTY";
            %client.favorites[%client.numFavs] = "EMPTY";
            %client.grenadeIndex = %client.grenadeIndex TAB %client.numFavs;
            %client.numFavs++;
         }
         
         if ( %list $= empty )
            %list = %list TAB %grenadeList;
         else
            %list = %list TAB %grenadeList TAB "EMPTY";

         messageClient( %client, 'SetLineHud', "", %tag, %x + %lineCount, "Grenade:", %list, grenade, %client.numFavsCount );
      }
      %lineCount = %lineCount + %armor.maxGrenades;
      
      for ( %x = 0; %x < %armor.maxMines; %x++ )
      {
         %client.numFavsCount++;
         if ( %x < getFieldCount( %client.mineIndex ) )
         {
            %list = %client.favorites[getField( %client.mineIndex, %x )];
            if ( %list $= Invalid )
            {
               %client.favorites[%client.numFavs] = "INVALID";
               %client.mineIndex = %client.mineIndex TAB %client.numFavs;
            }
         }
         else
         {
            %list = "EMPTY";
            %client.favorites[%client.numFavs] = "EMPTY";
            %client.mineIndex = %client.mineIndex TAB %client.numFavs;
            %client.numFavs++;
         }
         
         if ( %list !$= Invalid )
         {
            if ( %list $= empty )
               %list = %list TAB %mineList;
            else if ( %mineList !$= "" )
               %list = %list TAB %mineList TAB "EMPTY";
            else 
               %list = %list TAB "EMPTY";
         }
            
         messageClient( %client, 'SetLineHud', "", %tag, %x + %lineCount, "Mine:", %list, mine, %client.numFavsCount );
      }

      if ( %setLastNum )
         %client.lastNumFavs = %client.numFavsCount;
   }


   function DefaultGame::playerSpawned(%game, %player)
   {
      if( %player.client.respawnTimer )
         cancel(%player.client.respawnTimer);
      
      %player.client.observerStartTime = "";
      %game.equipCustom(%player);

      //set the spawn time (for use by the AI system)
      %player.client.spawnTime = getSimTime();

   // jff: this should probably be checking the team of the client
      //update anyone observing this client
      %count = ClientGroup.getCount();
      for (%i = 0; %i < %count; %i++)
      {
         %cl = ClientGroup.getObject(%i);
         if (%cl.camera.mode $= "observerFollow" && %cl.observeClient == %player.client)
         {
            %transform = %player.getTransform();
            %cl.camera.setOrbitMode(%player, %transform, 0.5, 4.5, 4.5);
            %cl.camera.targetObj = %player;
         }
      }
   }

   function ShapeBase::setInventory(%this,%data,%value,%force)
   {
      if (!isObject(%data))
         return;

      %name = %data.getName();
      if (%value < 0)
         %value = 0;
      else 
      {
         if (!%force) 
         {
            %bonus = 0;
            // Impose inventory limits
            if(%name $= "DiscAmmo" || %name $= "ChaingunAmmo" || 
               %name $= "PlasmaAmmo" || %name $= "GrenadeLauncherAmmo" || 
               %name $= "MissileLauncherAmmo" || %name $= "MortarAmmo" || 
               %name $= "Mine" || %name $= "Grenade"){
               %bonus = invySpawnObj.ammoMod[%name];
            }

            %max = %this.maxInventory(%data);
            if (%value > %max)
               %value = %max;

            %value += %bonus;
         }
      }
      if (%this.inv[%name] != %value) 
      {
         %this.inv[%name] = %value;
         %data.onInventory(%this,%value);

         if ( %data.className $= "Weapon" )
         {
            if ( %this.weaponSlotCount $= "" )
               %this.weaponSlotCount = 0;

            %cur = -1;
            for ( %slot = 0; %slot < %this.weaponSlotCount; %slot++ )
            {
               if ( %this.weaponSlot[%slot] $= %name )
               {
                  %cur = %slot;
                  break;
               }
            }

            if ( %cur == -1 )
            {
               // Put this weapon in the next weapon slot:
               if ( %this.weaponSlot[%this.weaponSlotCount - 1] $= "TargetingLaser" )
               {
                  %this.weaponSlot[%this.weaponSlotCount - 1] = %name;
                  %this.weaponSlot[%this.weaponSlotCount] = "TargetingLaser";
               }
               else
                  %this.weaponSlot[%this.weaponSlotCount] = %name;
               %this.weaponSlotCount++;
            }
            else
            {
               // Remove the weapon from the weapon slot:
               for ( %i = %cur; %i < %this.weaponSlotCount - 1; %i++ )
                  %this.weaponSlot[%i] = %this.weaponSlot[%i + 1];
               %this.weaponSlot[%i] = "";
               %this.weaponSlotCount--;
            }
         }

         %this.getDataBlock().onInventory(%data,%value);
      }
      return %value;
   }

   function buyFavorites(%client)
   {
      if(isObject(Game)) // z0dd - ZOD, 8/9/03. No armors in Spawn CTF.
      {
         if(Game.class $= SCtFGame)
         {
            buyDeployableFavorites(%client);
            return;
         }
      }
      %cmt = $CurrentMissionType;
      if($InvBanList[%cmt, $NameToInv[%client.favorites[0]]]){
         buyDeployableFavorites(%client);
         return;
      }
      // z0dd - ZOD, 5/27/03. Check to see if we reached the cap on armors, if so, buy ammo and go away mad.
      if(%client.favorites[0] !$= "Scout" && !$Host::TournamentMode && $LimitArmors)
      {
         if($TeamArmorCount[%client.team, $NameToInv[%client.favorites[0]]] >= $TeamArmorMax)
         {
            messageClient(%client, 'MsgTeamDepObjCount', '\c2Your team has reached the maximum (%2) allotment of %1 armors', %client.favorites[0], $TeamArmorMax);
            getAmmoStationLovin(%client);
            return;
         }
      }

      // z0dd - ZOD, 5/27/03. Increase the teams armor count and let the player know whats left etc.
      if(!$Host::TournamentMode && $LimitArmors)
      {
         $TeamArmorCount[%client.team, %client.armor]--;
         $TeamArmorCount[%client.team, $NameToInv[%client.favorites[0]]]++;
         if(%client.favorites[0] !$= "Scout")
            messageClient(%client, 'MsgTeamDepObjCount', '\c2Your team has %1 of %2 %3 armors in use', $TeamArmorCount[%client.team, $NameToInv[%client.favorites[0]]], $TeamArmorMax, %client.favorites[0]);
      }

      // don't forget -- for many functions, anything done here also needs to be done
      // below in buyDeployableFavorites !!!
      %client.player.clearInventory();
      %client.setWeaponsHudClearAll();


      %curArmor = %client.player.getDatablock();
      %curDmgPct = getDamagePercent(%curArmor.maxDamage, %client.player.getDamageLevel());

      // armor
      %client.armor = $NameToInv[%client.favorites[0]];
      %client.player.setArmor( %client.armor );
      %newArmor = %client.player.getDataBlock();

      %client.player.setDamageLevel(%curDmgPct * %newArmor.maxDamage);
      %weaponCount = 0;

      // weapons
      for(%i = 0; %i < getFieldCount( %client.weaponIndex ); %i++)
      {
         %inv = $NameToInv[%client.favorites[getField( %client.weaponIndex, %i )]];
         
         if( %inv !$= "" )
         {   
            %weaponCount++;
            %client.player.setInventory( %inv, 1 );
         }
         
         // ----------------------------------------------------
         // z0dd - ZOD, 4/24/02. Code optimization.
         if ( %inv.image.ammo !$= "" )
            %client.player.setInventory( %inv.image.ammo, 999 );
         // ----------------------------------------------------
      }
      %client.player.weaponCount = %weaponCount;

      // pack
      %pCh = $NameToInv[%client.favorites[%client.packIndex]];
      if ( %pCh $= "" )
         %client.clearBackpackIcon();
      else
         %client.player.setInventory( %pCh, 1 );

      // if this pack is a deployable that has a team limit, warn the purchaser
      // if it's a deployable turret, the limit depends on the number of players (deployables.cs)
      if(%pCh $= "TurretIndoorDeployable" || %pCh $= "TurretOutdoorDeployable")
         %maxDep = countTurretsAllowed(%pCh);
      else
         %maxDep = $TeamDeployableMax[%pCh];

      if(%maxDep !$= "")
      {
         %depSoFar = $TeamDeployedCount[%client.player.team, %pCh];
         %packName = %client.favorites[%client.packIndex];

         if(Game.numTeams > 1)
            %msTxt = "Your team has "@%depSoFar@" of "@%maxDep SPC %packName@"s deployed.";
         else
            %msTxt = "You have deployed "@%depSoFar@" of "@%maxDep SPC %packName@"s.";

         messageClient(%client, 'MsgTeamDepObjCount', %msTxt);
      }

      // grenades
      for ( %i = 0; %i < getFieldCount( %client.grenadeIndex ); %i++ )
      {
         if ( !($InvBanList[%cmt, $NameToInv[%client.favorites[getField( %client.grenadeIndex, %i )]]]) )
         %client.player.setInventory( $NameToInv[%client.favorites[getField( %client.grenadeIndex,%i )]], 30 );
      }

      %client.player.lastGrenade = $NameToInv[%client.favorites[getField( %client.grenadeIndex,%i )]];

      // if player is buying cameras, show how many are already deployed
      if(%client.favorites[%client.grenadeIndex] $= "Deployable Camera")
      {
         %maxDep = $TeamDeployableMax[DeployedCamera];
         %depSoFar = $TeamDeployedCount[%client.player.team, DeployedCamera];
         if(Game.numTeams > 1)
            %msTxt = "Your team has "@%depSoFar@" of "@%maxDep@" Deployable Cameras placed.";
         else
            %msTxt = "You have placed "@%depSoFar@" of "@%maxDep@" Deployable Cameras.";
         messageClient(%client, 'MsgTeamDepObjCount', %msTxt);
      }

      // mines
      // -----------------------------------------------------------------------------------------------------
      // z0dd - ZOD, 4/24/02. Old code did not check to see if mines are banned, fixed.
      for ( %i = 0; %i < getFieldCount( %client.mineIndex ); %i++ )
      {
         if ( !($InvBanList[%cmt, $NameToInv[%client.favorites[getField( %client.mineIndex, %i )]]]) )
         %client.player.setInventory( $NameToInv[%client.favorites[getField( %client.mineIndex,%i )]], 30 );
      }
      // -----------------------------------------------------------------------------------------------------
      // miscellaneous stuff -- Repair Kit, Beacons, Targeting Laser
      if ( !($InvBanList[%cmt, RepairKit]) )
         %client.player.setInventory( RepairKit, 1 );
      if ( !($InvBanList[%cmt, Beacon]) )
         %client.player.setInventory( Beacon, 20 ); // z0dd - ZOD, 4/24/02. 400 was a bit much, changed to 20
      if ( !($InvBanList[%cmt, TargetingLaser]) )
         %client.player.setInventory( TargetingLaser, 1 );

      // ammo pack pass -- hack! hack!
      if( %pCh $= "AmmoPack" )
         invAmmoPackPass(%client);
   }
};


function DefaultGame::equipCustom(%game, %player){
   
   if(isObject(invySpawnObj)){
      for(%i =0; %i<$InventoryHudCount; %i++)
         %player.client.setInventoryHudItem($InventoryHudData[%i, itemDataName], 0, 1);
      %player.client.clearBackpackIcon();

      if(invySpawnObj.armor !$= "Light"){
         %player.setArmor(invySpawnObj.armor);
      }

      %player.weaponCount = 0;
      if(invySpawnObj.blaster > 0){
         %player.setInventory(Blaster, invySpawnObj.blaster,1);
         %player.weaponCount++;
      }
      if(invySpawnObj.disc > 0){
         %player.setInventory(Disc, 1, 1);
         %player.setInventory(DiscAmmo, invySpawnObj.disc, 1);
         %player.weaponCount++;
      }
      if(invySpawnObj.shockLance > 0){
         %player.setInventory(ShockLance, 1, 1);
         %player.weaponCount++;
      }
      if(invySpawnObj.chaingun > 0){
         %player.setInventory(Chaingun, 1, 1);
         %player.setInventory(ChaingunAmmo, invySpawnObj.chaingun, 1);
         %player.weaponCount++;
      }
      if(invySpawnObj.plasma > 0){
         %player.setInventory(Plasma, 1);
         %player.setInventory(PlasmaAmmo, invySpawnObj.plasma, 1);
         %player.weaponCount++;
      }
        if(invySpawnObj.grenadeLauncher > 0){
         %player.setInventory(GrenadeLauncher, 1, 1);
         %player.setInventory(GrenadeLauncherAmmo, invySpawnObj.GrenadeLauncher, 1);
         %player.weaponCount++;
      }
       if(invySpawnObj.mortar > 0){
         %player.setInventory(Mortar, 1, 1);
         %player.setInventory(MortarAmmo, invySpawnObj.mortar, 1);
         %player.weaponCount++;
      }
      if(invySpawnObj.missileLauncher > 0){
         %player.setInventory(MissileLauncher, 1, 1);
         %player.setInventory(MissileLauncherAmmo, invySpawnObj.missileLauncher, 1);
         %player.weaponCount++;
      }
      if(invySpawnObj.elfGun > 0){
         %player.setInventory(ELFGun, 1, 1);
         %player.weaponCount++;
      }
      if(invySpawnObj.sniperRifle > 0){
         %player.setInventory(SniperRifle, 1, 1);
         %player.weaponCount++;
      }
 
      if(invySpawnObj.pack > 0){
         %player.setInventory(invySpawnObj.pack, 1, 1);
      }

      if(invySpawnObj.targetingLaser > 0){
         %player.setInventory(TargetingLaser, 1, 1);
      }
      if(invySpawnObj.repairKit > 0){
         %player.setInventory(RepairKit, invySpawnObj.repairKit, 1);
      }
      if(invySpawnObj.beacon > 0){
         %player.setInventory(Beacon, invySpawnObj.beacon, 1);
      }
      if(invySpawnObj.grenade !$= ""){
         %player.setInventory(invySpawnObj.grenade, invySpawnObj.grenadeAmmo, 1);
      }
      if(invySpawnObj.mine > 0){
         %player.setInventory(Mine, invySpawnObj.mine, 1);
      }
      if(invySpawnObj.startWep !$= ""){
         %player.use(invySpawnObj.startWep);
      }
   
   }
   else{
      %game.equip(%player);
   }
} 