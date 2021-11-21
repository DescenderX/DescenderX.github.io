//This file is part of Tribes RPG.
//Tribes RPG smithing scripts
//Written by Jason "phantom" Daley, and Matthiew "JeremyIrons" Bouchard

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

//________________________________________________________________________________________________________________________________________________
// DescX Notes:
//		All Smithing combos have been relocated to the files where their results are produced.
//		E.g. see wpslashing for Slashing smith combos.
//		I will eventually move Smith::addItem somewhere else and delete this file.

if(!$SmithItemCount)
	$SmithItemCount=0;

function Smith::addItem(%item,%combo,%result,%skillType)
{
	if(floor(%result) == %result){
		pecho("Smith error "@%item);
		return;
	}
	if($SmithVar[%item] == "") {	// Make list updates work. Allows re-exec after launch.
		$SmithItemCount++;
		%num = $SmithItemCount;
	}
	else %num = $SmithVar[%item];
	
	$SmithNum[%num] = %item;
	$SmithVar[%item] = %num;
	$SmithCombo[%num] = %combo;
	$SmithComboResult[%num] = %result;
	$SmithAssociatedSkillType[%num] = %skillType;
}

//The code beyond here isn't used anymore.
function SetupBlacksmith(%clientId, %id)
{
	dbecho($dbechoMode, "SetupBlacksmith(" @ %clientId @ ", " @ %id @ ")");

	%clientId.currentSmith = %id;

	%clientId.bulkNum = "";

	Client::clearItemShopping(%clientId);
	Client::clearItemBuying(%clientId);

	if(Client::getGuiMode(%clientId) != 4)
		Client::setGuiMode(%clientId, 4);

	%info = fetchData(%clientId, "TempSmith");
	for(%i = 0; GetWord(%info, %i) != -1; %i+=2)
	{
		%item = GetWord(%info, %i);

		Client::setItemShopping(%clientId, %item);
		Client::setItemBuying(%clientId, %item);
	}

	%txt = "<f1><jc>COINS: " @ fetchData(%clientId, "COINS");
	Client::setInventoryText(%clientId, %txt);
}

function CompleteSmith(%clientId, %cost, %sc, %tempsmith, %multiplier)
{
	dbecho($dbechoMode, "CompleteSmith(" @ %clientId @ ", " @ %cost @ ", " @ %sc @ ", " @ %tempsmith @ ", " @ %multiplier @ ")");

	%clientId.IsSmithing = "";

	if(fetchData(%clientId, "COINS") < %cost)
		return;

	storeData(%clientId, "COINS", %cost, "dec");
	playSound(SoundMoney1, GameBase::getPosition(%clientId));
	GiveThisStuff(%clientId, $SmithComboResult[%sc], True, %multiplier);

	for(%i = 0; (%w = GetWord(%tempsmith, %i)) != -1; %i+=2)
	{
		%w2 = GetWord(%tempsmith, %i+1) * %multiplier;
		storeData(%clientId, "BankStorage", rpg::ModifyItemList(fetchData(%clientId, "BankStorage"), %w, -%w2));
	}

	AI::sayLater(%clientId, %clientId.currentSmith, "Here you go.", True);
}

function BlackSmithClick(%clientId, %item, %delta)
{
	dbecho($dbechoMode, "BlackSmithClick(" @ %clientId @ ", " @ %item @ ", " @ %delta @ ")");

	if(%clientId.IsSmithing)
	{
		Client::sendMessage(%clientId, $MsgRed, "The blacksmith is busy...");
	}
	else
	{
		if(%item.className != Equipped)
		{
			storeData(%clientId, "TempSmith", rpg::ModifyItemList(fetchData(%clientId, "TempSmith"), %item, %delta));
			SetupBlacksmith(%clientId, %clientId.currentSmith);

			%tempsmith = LTrim(fetchData(%clientId, "TempSmith"));
			if((%sc = GetSmithCombo(%tempsmith)) != 0)
			{
				%cost = GetSmithComboCost(%clientId, %sc);

				Client::sendMessage(%clientId, $MsgWhite, "It will cost you " @ %cost @ " coins to smith these items.~wcanSmith.wav");
				Client::sendMessage(%clientId, $MsgBeige, "(type #smith to accept the cost and start smithing)");

				return 0;
			}
			else
			{
				%cnt = rpg::GetItemListCount(fetchData(%clientId, "TempSmith"), %item);
				Client::sendMessage(%clientId, $MsgRed, "Invalid combination. (" @ %cnt @ " " @ %item.description @ ")");
			}
		}
	}
}

function GetSmithCombo(%tempsmith) {
	for(%i = 1; $SmithCombo[%i] != ""; %i++)
		if(rpg::IsItemListEqual(%tempsmith, $SmithCombo[%i]))
			return %i;
	return 0;
}

function GetSmithComboCost(%clientId, %sc) {
	%c1 = GetStuffStringCost(%clientId, $SmithCombo[%sc]);
	%c2 = GetStuffStringCost(%clientId, $SmithComboResult[%sc]);
	return Cap( round(%c2 - (%c1 * 1.35)), 1, "inf");
}