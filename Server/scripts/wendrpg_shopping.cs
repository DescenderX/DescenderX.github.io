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



function SetupShop(%clientId, %botid)
{
	dbecho($dbechoMode, "SetupShop(" @ %clientId @ ", " @ %id @ ")");

	ClearCurrentShopVars(%clientId);
	%clientId.currentShop = %botid;

	%clientId.bulkNum = "";

	Client::clearItemShopping(%clientId);
	Client::clearItemBuying(%clientId);

	Client::setGuiMode(%clientId, 4);
	if(%clientId.repack >= 20)
		remoteEval(%clientId,InvHUD::turnOn, 3);

	%txt = "<f1><jc>COINS: " @ fetchData(%clientId, "COINS");
	Client::setInventoryText(%clientId, %txt);

	%info = $BotInfo[%botid.name, SHOP];	
	if($BotInfo[%botid.name, BeltEvaluated] == "")
	{
		$BotInfo[%botid.name, BeltEvaluated] = True;
		%max = $numBeltItems;
		for(%i = 0; GetWord(%info, %i) != -1; %i++)
		{
			%added = False;
			%a = GetWord(%info, %i);
			for(%z = 0; %z < %max; %z++)
			{
				%item = $beltItemData[%z];
					if(%a == %item) {
						%added = True;
						$BotInfo[%botid.name, BELTSHOP] = $BotInfo[%botid.name, BELTSHOP] @ %a @ " ";
					}
			}
			if(!%added)
				%newString = %newString @ %a @ " ";
		}
		$BotInfo[%botid.name, SHOP] = %newString;
		%info = %newString;
	}
	%max = getNumItems();			for(%id = 0; %id < %max; %id++)	{		%item = getItemData(%id);
				for(%i = 0; GetWord(%info, %i) != -1; %i++)		{
						%a = GetWord(%info, %i);			if(%a == %item)			{				Client::setItemShopping(%clientId, %item);				Client::setItemBuying(%clientId, %item);			}		}	}	if($BotInfo[%botid.name, BELTSHOP] != ""){		Client::setItemShopping(%clientId, BeltItemTool);		Client::setItemBuying(%clientId, BeltItemTool);		if($BotInfo[%botid.name, SHOP] == ""){			%clientId.beltShop = %botid;			MenuBuyBeltItem(%clientId, 1);		}	}
}

function SetupBank(%clientId, %id)
{
	dbecho($dbechoMode, "SetupBank(" @ %clientId @ ", " @ %id @ ")");

	ClearCurrentShopVars(%clientId);
	%clientId.currentBank = %id;
	
	%clientId.bulkNum = "";

	Client::clearItemShopping(%clientId);
	Client::clearItemBuying(%clientId);

	if(Client::getGuiMode(%clientId) != 4){
		Client::setGuiMode(%clientId, 4);
		if(%clientId.repack >= 20)
			remoteEval(%clientId,InvHUD::turnOn, 3);
	}

	%txt = "<f1><jc>COINS: " @ fetchData(%clientId, "COINS");
	Client::setInventoryText(%clientId, %txt);
	Client::setItemShopping(%clientId, BeltItemTool);	Client::setItemBuying(%clientId, BeltItemTool);

	%info = fetchData(%clientId, "BankStorage");

	for(%i = 0; GetWord(%info, %i) != -1; %i+=2)
	{
		%item = GetWord(%info, %i);
		if(isBeltItem(%item)){			%cnt = rpg::GetItemListCount(fetchData(%clientId, "BankStorage"), %item);			givethisstuff(%clientId, %item@" "@%cnt, True);		}		else		{
			Client::setItemShopping(%clientId, %item);
			Client::setItemBuying(%clientId, %item);
		}
	}
}

function SetupInvSteal(%clientId, %id)
{
	dbecho($dbechoMode, "SetupInvSteal(" @ %clientId @ ", " @ %id @ ")");
	
	if(IsDead(%clientId) || IsDead(%id))
		return;

	ClearCurrentShopVars(%clientId);
	%clientId.currentInvSteal = %id;

	%clientId.bulkNum = "";

	Client::clearItemShopping(%clientId);
	Client::clearItemBuying(%clientId);

	if(Client::getGuiMode(%clientId) != 4)
		Client::setGuiMode(%clientId, 4);
	
//	if(%clientId.repack >= 20)
//		remoteEval(%clientId,InvHUD::turnOn, 3);
	
	%txt = "<f1><jc>" @ Client::getName(%id) @ "'s inventory";
	Client::setInventoryText(%clientId, %txt);

	%max = getNumItems();
	for(%i = 0; %i < %max; %i++)
	{
		%item = getItemData(%i);
		%itemcount = Player::getItemCount(%id, %item);

		if(%itemcount > 0)
		{
			Client::setItemShopping(%clientId, %item);
			Client::setItemBuying(%clientId, %item);
		}
	}
}

function SetupCreatePack(%clientId)
{
	dbecho($dbechoMode, "SetupCreatePack(" @ %clientId @ ")");

	Client::clearItemShopping(%clientId);
	Client::clearItemBuying(%clientId);

	if(Client::getGuiMode(%clientId) != 4)
		Client::setGuiMode(%clientId, 4);

	%info = fetchData(%clientId, "TempPack");
	for(%i = 0; GetWord(%info, %i) != -1; %i+=2)
	{
		%item = GetWord(%info, %i);

		Client::setItemShopping(%clientId, %item);
		Client::setItemBuying(%clientId, %item);
	}
}

function ClearCurrentShopVars(%clientId)
{
	dbecho($dbechoMode, "ClearCurrentShopVars(" @ %clientId @ ")");

	%clientId.currentShop = "";
	%clientId.currentBank = "";
	%clientId.currentSmith = "";
	%clientId.currentInvSteal = "";

	storeData(%clientId, "TempPack", "");
	storeData(%clientId, "TempSmith", "");
}

