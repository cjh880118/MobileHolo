using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellBig.Constants;

namespace CellBig.UI.Event
{
    public class OpenStoreDialogMsg : Message
    {
        public StoreMenu storeMenu;
        public OpenStoreDialogMsg(StoreMenu storeMenu)
        {
            this.storeMenu = storeMenu;
        }
    }

    public class CloseStoreDialogMsg : Message
    {
        public StoreMenu storeMenu;
        public CloseStoreDialogMsg(StoreMenu storeMenu)
        {
            this.storeMenu = storeMenu;
        }
    }

    public class SetCashStoreMsg : Message
    {
        public Cash_Table cash_Table;
        public SetCashStoreMsg(Cash_Table cash_Table)
        {
            this.cash_Table = cash_Table;
        }
    }

    public class SetCharacterStoreMsg : Message
    {
        public List<Character_Table.Param> ListCharacter;
        public List<bool> ListIsHave;
        public Character character;

        public SetCharacterStoreMsg(Character character, List<Character_Table.Param> ListCharacter, List<bool> ListIsBuy)
        {
            this.character = character;
            this.ListCharacter = ListCharacter;
            this.ListIsHave = ListIsBuy;
        }
    }

    public class SetSkinStoreMsg : Message
    {
        public Character character;
        //public Skin_Table skin_Table;
        public int skinIndex;
        public int coin;
        public List<Skin_Table.Param> ListSkin;
        public List<bool> ListIsHave;
        public SetSkinStoreMsg(Character character, int index, List<Skin_Table.Param> ListSkin, List<bool> ListIsHave, int coin)
        {
            this.character = character;
            this.skinIndex = index;
            this.ListSkin = ListSkin;
            this.ListIsHave = ListIsHave;
            this.coin = coin;
        }
    }

    public class SetSkinStoreCharacterMsg : Message
    {
        public List<Character_Table.Param> ListCharacter;
        public List<bool> ListIsHave;
        public Character character;

        public SetSkinStoreCharacterMsg(Character character, List<Character_Table.Param> ListCharacter, List<bool> ListIsBuy)
        {
            this.character = character;
            this.ListCharacter = ListCharacter;
            this.ListIsHave = ListIsBuy;
        }
    }

    public class StoreSkinItemSelectMsg : Message
    {
        public int itemIndex;
        public int cost;
        public StoreSkinItemSelectMsg(int itemIndex, int cost)
        {
            this.itemIndex = itemIndex;
            this.cost = cost;
        }
    }

    public class StoreSkinNowMsg : Message
    {

    }

    public class StoreSkinBtnTextMsg : Message
    {
        public bool isBuy;
        public bool isBuyPossible;
        public int itemIndex;
        public StoreSkinBtnTextMsg(bool isBuy, bool isBuyPossible, int itemIndex)
        {
            this.isBuy = isBuy;
            this.isBuyPossible = isBuyPossible;
            this.itemIndex = itemIndex;
        }
    }

    public class SkinBuyInfoMsg : Message
    {
        public bool isBuy;
        public bool isBuyPossible;
        public int itemIndex;
        public SkinBuyInfoMsg(bool isBuy, bool isBuyPossible, int itemIndex)
        {
            this.isBuy = isBuy;
            this.isBuyPossible = isBuyPossible;
            this.itemIndex = itemIndex;
        }
    }

    public class StorBuySkinMsg : Message
    {
        public bool isBuy;
        public int itemIndex;
        public StorBuySkinMsg(bool isBuy, int index)
        {
            this.isBuy = isBuy;
            itemIndex = index;
        }
    }

    public class SkinBuyButtonMsg : Message
    {
        public bool isBuy;
        public bool isBuyPossible;
        public int itemIndex;
        public SkinBuyButtonMsg(bool isBuy, bool isBuyPossible, int itemIndex)
        {
            this.isBuy = isBuy;
            this.isBuyPossible = isBuyPossible;
            this.itemIndex = itemIndex;
        }
    }

    public class SkinBuyDialogCloseMsg : Message
    {

    }

    public class StoreAvatarItemSelectMsg : Message
    {
        public int itemIndex;
        public int cost;
        public StoreAvatarItemSelectMsg(int itemIndex, int cost)
        {
            this.itemIndex = itemIndex;
        }
    }

    public class AvatarBuyInfoMsg : Message
    {
        public bool isBuy;
        public bool isBuyPossible;
        public int itemIndex;
        public AvatarBuyInfoMsg(bool isBuy, bool isBuyPossible, int itemIndex)
        {
            this.isBuy = isBuy;
            this.isBuyPossible = isBuyPossible;
            this.itemIndex = itemIndex;
        }
    }

    public class AvatarBuyDialogCloseMsg : Message
    {

    }

    public class StorBuyAvatarMsg : Message
    {
        public bool isBuy;
        public int itemIndex;
        public StorBuyAvatarMsg(bool isBuy, int index)
        {
            this.isBuy = isBuy;
            itemIndex = index;
        }
    }
}