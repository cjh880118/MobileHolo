using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellBig.UI.Event;
using CellBig.Constants;
using CellBig.Models;
using System;
using System.Linq;
using CellBig.Android;

namespace CellBig.Contents
{
    public class StoreContent : IContent
    {
        static string TAG = "StoreContent :: ";

        MobileOptionModel optionModel;
        Cash_Table cash_Table;
        Character_Table character_Table;
        Skin_Table skin_Table;
        PlayerInventoryModel playerInventoryModel;
        PlayerStatusModel playerStatusModel;
        Coroutine btnEvent;

        protected override void OnLoadStart()
        {
            optionModel = Model.First<MobileOptionModel>();
            cash_Table = TableManager.Instance.GetTableClass<Cash_Table>();
            character_Table = TableManager.Instance.GetTableClass<Character_Table>();
            skin_Table = TableManager.Instance.GetTableClass<Skin_Table>();
            playerInventoryModel = Model.First<PlayerInventoryModel>();
            playerStatusModel = Model.First<PlayerStatusModel>();
            SetLoadComplete();
        }

        protected override void OnEnter()
        {
            Debug.Log(TAG + "OnEnter");
            Message.Send<SetStoreCharacterMsg>(new SetStoreCharacterMsg(true));
            IContent.RequestContentExit<MenuContent>();
            UI.IDialog.RequestDialogEnter<UI.StoreMainDialog>();
            btnEvent = StartCoroutine(ButtonInput());
            AddMessage();
        }

        IEnumerator ButtonInput()
        {
            while (true)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log(TAG + " : Escape");
                    IContent.RequestContentEnter<MenuContent>();
                }
            }
        }

        private void AddMessage()
        {
            Message.AddListener<StoreSkinItemSelectMsg>(StoreSkinItemSelect);
            Message.AddListener<StoreAvatarItemSelectMsg>(StoreAvatarItemSelect);
            Message.AddListener<OpenStoreDialogMsg>(OpenStoreDialog);
            Message.AddListener<CloseStoreDialogMsg>(CloseStoreDialog);
            Message.AddListener<AvatarBuyDialogCloseMsg>(AvatarBuyDialogClose);
            Message.AddListener<StorBuyAvatarMsg>(StorBuyAvatar);
            Message.AddListener<SkinBuyDialogCloseMsg>(SkinBuyDialogClose);
            Message.AddListener<StorBuySkinMsg>(StorBuySkin);
            Message.AddListener<SkinBuyButtonMsg>(SkinBuyButton);
        }

        //스킨 구매
        private void StoreSkinItemSelect(StoreSkinItemSelectMsg msg)
        {
            if (playerInventoryModel.NowSkin == msg.itemIndex)
            {
                //이미 적용중인 스킨
                Message.Send<SetCharacterDressMsg>(new SetCharacterDressMsg(playerInventoryModel.NowCharacter, msg.itemIndex));
                Message.Send<StoreSkinNowMsg>(new StoreSkinNowMsg());
                return;
            }
            else
            {
                SoundManager.Instance.PlaySound((int)SoundType.EFFECT_Clothes_Change, optionModel.MobileOption.effectVolume);
                if (playerInventoryModel.IsHaveSkin(msg.itemIndex))
                {
                    Debug.Log("적용할래?");
                    Message.Send<StoreSkinBtnTextMsg>(new StoreSkinBtnTextMsg(false, false, msg.itemIndex));
                }
                else
                {
                    Debug.Log("살래?");
                    bool isBuyPossible;
                    if (playerStatusModel.Coin >= msg.cost)
                        isBuyPossible = true;
                    else
                        isBuyPossible = false;

                    Message.Send<StoreSkinBtnTextMsg>(new StoreSkinBtnTextMsg(true, isBuyPossible, msg.itemIndex));
                }

                Message.Send<SetCharacterDressMsg>(new SetCharacterDressMsg(playerInventoryModel.NowCharacter, msg.itemIndex));
            }
        }

        //아바타 구매
        private void StoreAvatarItemSelect(StoreAvatarItemSelectMsg msg)
        {
            if ((int)playerInventoryModel.NowCharacter == msg.itemIndex)
            {
                //이미 적용중인 케릭터
                return;
            }
            else
            {
                SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Open, optionModel.MobileOption.effectVolume);
                if (playerInventoryModel.IsHaveCharacter(msg.itemIndex))
                {
                    Debug.Log("적용할래?");
                    UI.IDialog.RequestDialogEnter<UI.StoreIdolBuyDialog>();
                    Message.Send<AvatarBuyInfoMsg>(new AvatarBuyInfoMsg(false, false, msg.itemIndex));
                    //보유 하고 있다 적용 할래?
                }
                else
                {
                    Debug.Log("살래?");
                    UI.IDialog.RequestDialogEnter<UI.StoreIdolBuyDialog>();

                    bool isBuyPossible;
                    if (playerStatusModel.Potential >= msg.cost)
                        isBuyPossible = true;
                    else
                        isBuyPossible = false;

                    Message.Send<AvatarBuyInfoMsg>(new AvatarBuyInfoMsg(true, isBuyPossible, msg.itemIndex));
                    //없음 살래?
                }
            }
        }

        private void OpenStoreDialog(OpenStoreDialogMsg msg)
        {
            SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Open, optionModel.MobileOption.effectVolume);
            if (msg.storeMenu == StoreMenu.IdolShop)
            {
                UI.IDialog.RequestDialogEnter<UI.StoreIdolDialog>();
                List<Character_Table.Param> ListCharacter = new List<Character_Table.Param>();
                List<bool> ListIsHave = new List<bool>();

                for (int i = 0; i < character_Table.sheets[0].list.Count; i++)
                {
                    ListCharacter.Add(character_Table.sheets[0].list[i]);
                    ListIsHave.Add(playerInventoryModel.IsHaveCharacter(i));
                }

                Message.Send<SetCharacterStoreMsg>(new SetCharacterStoreMsg(playerInventoryModel.NowCharacter, ListCharacter, ListIsHave));
            }
            else if (msg.storeMenu == StoreMenu.CashShop)
            {
                UI.IDialog.RequestDialogEnter<UI.StoreCashDialog>();
                Message.Send<SetCashStoreMsg>(new SetCashStoreMsg(cash_Table));
            }

            else if (msg.storeMenu == StoreMenu.SkinShop)
            {
                SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Open, optionModel.MobileOption.effectVolume);
                UI.IDialog.RequestDialogEnter<UI.StoreSkinDialog>();
                List<Skin_Table.Param> ListSkin = new List<Skin_Table.Param>();
                List<bool> ListIsHave = new List<bool>();

                for (int i = 0; i < skin_Table.sheets[0].list.Count; i++)
                {
                    if (skin_Table.sheets[0].list[i].Character == (int)playerInventoryModel.NowCharacter)
                    {
                        ListSkin.Add(skin_Table.sheets[0].list[i]);
                        ListIsHave.Add(playerInventoryModel.IsHaveSkin(i));
                    }
                }

                Message.Send<SetSkinStoreMsg>(new SetSkinStoreMsg(playerInventoryModel.NowCharacter, playerInventoryModel.NowSkin, ListSkin, ListIsHave, playerStatusModel.Coin));

                List<Character_Table.Param> ListCharacter = new List<Character_Table.Param>();
                List<bool> ListIsCharacterHave = new List<bool>();

                for (int i = 0; i < character_Table.sheets[0].list.Count; i++)
                {
                    ListCharacter.Add(character_Table.sheets[0].list[i]);
                    ListIsCharacterHave.Add(playerInventoryModel.IsHaveCharacter(i));
                }

                Message.Send<SetSkinStoreCharacterMsg>(new SetSkinStoreCharacterMsg(playerInventoryModel.NowCharacter, ListCharacter, ListIsCharacterHave));
            }
        }

        private void CloseStoreDialog(CloseStoreDialogMsg msg)
        {
            SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Close, optionModel.MobileOption.effectVolume);

            if (msg.storeMenu == StoreMenu.IdolShop)
                UI.IDialog.RequestDialogExit<UI.StoreIdolDialog>();
            else if (msg.storeMenu == StoreMenu.CashShop)
                UI.IDialog.RequestDialogExit<UI.StoreCashDialog>();
            else if (msg.storeMenu == StoreMenu.SkinShop)
            {
                UI.IDialog.RequestDialogExit<UI.StoreSkinDialog>();
                Message.Send<SetCharacterDressMsg>(new SetCharacterDressMsg(playerInventoryModel.NowCharacter, playerInventoryModel.NowSkin));
            }
        }

        private void AvatarBuyDialogClose(AvatarBuyDialogCloseMsg msg)
        {
            SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Close, optionModel.MobileOption.effectVolume);
            UI.IDialog.RequestDialogExit<UI.StoreIdolBuyDialog>();
            Message.Send<SetCharacterDressMsg>(new SetCharacterDressMsg(playerInventoryModel.NowCharacter, playerInventoryModel.NowSkin));
        }

        private void StorBuyAvatar(StorBuyAvatarMsg msg)
        {
            if (msg.isBuy)
            {
                playerStatusModel.Potential -= character_Table.sheets[0].list[msg.itemIndex].cost;
                playerInventoryModel.BuyCharacter(msg.itemIndex);
                playerInventoryModel.NowCharacter = (Character)msg.itemIndex;
                if ((Character)msg.itemIndex == Character.Boy)
                {
                    playerInventoryModel.NowSkin = 10;
                    playerInventoryModel.BuySkin(10);
                }
                else
                {
                    playerInventoryModel.NowSkin = 0;
                    playerInventoryModel.BuySkin(0);
                }
            }
            else
            {
                playerInventoryModel.NowCharacter = (Character)msg.itemIndex;
                if ((Character)msg.itemIndex == Character.Boy)
                {
                    playerInventoryModel.NowSkin = 10;
                }
                else
                {
                    playerInventoryModel.NowSkin = 0;
                }
            }

            AndroidTrasferMgr.Instance.ShowToast("아바타가 변경되었습니다.");
            OpenStoreDialog(new OpenStoreDialogMsg(StoreMenu.SkinShop));
            CloseStoreDialog(new CloseStoreDialogMsg(StoreMenu.IdolShop));
            AvatarBuyDialogClose(new AvatarBuyDialogCloseMsg());
        }

        private void SkinBuyDialogClose(SkinBuyDialogCloseMsg msg)
        {
            UI.IDialog.RequestDialogExit<UI.StoreSkinBuyDialog>();
        }

        //스킨 구매
        private void SkinBuyButton(SkinBuyButtonMsg msg)
        {
            SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Open, optionModel.MobileOption.effectVolume);
            UI.IDialog.RequestDialogEnter<UI.StoreSkinBuyDialog>();
            Message.Send<SkinBuyInfoMsg>(new SkinBuyInfoMsg(msg.isBuy, msg.isBuyPossible, msg.itemIndex));
        }

        //todo.. 스킨 구매 처리 (추후 서버 연동 필요 부분)
        private void StorBuySkin(StorBuySkinMsg msg)
        {
            if (msg.isBuy)
            {
                //구매 부분
                playerStatusModel.Coin -= skin_Table.sheets[0].list[msg.itemIndex].cost;
                playerInventoryModel.BuySkin(msg.itemIndex);
                playerInventoryModel.NowSkin = msg.itemIndex;
            }
            else
            {
                playerInventoryModel.NowSkin = msg.itemIndex;
            }
            AndroidTrasferMgr.Instance.ShowToast("스킨이 변경되었습니다.");
            Message.Send<SetCharacterDressMsg>(new SetCharacterDressMsg(playerInventoryModel.NowCharacter, playerInventoryModel.NowSkin));
            SkinBuyDialogClose(new SkinBuyDialogCloseMsg());
            OpenStoreDialog(new OpenStoreDialogMsg(StoreMenu.SkinShop));
        }

        protected override void OnExit()
        {
            Message.Send<SetCharacterDressMsg>(new SetCharacterDressMsg(playerInventoryModel.NowCharacter, playerInventoryModel.NowSkin));
            Message.Send<SetStoreCharacterMsg>(new SetStoreCharacterMsg(false));
            if (btnEvent != null)
                StopCoroutine(btnEvent);
            DialogAllClose();
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<StoreSkinItemSelectMsg>(StoreSkinItemSelect);
            Message.RemoveListener<StoreAvatarItemSelectMsg>(StoreAvatarItemSelect);
            Message.RemoveListener<OpenStoreDialogMsg>(OpenStoreDialog);
            Message.RemoveListener<CloseStoreDialogMsg>(CloseStoreDialog);
            Message.RemoveListener<AvatarBuyDialogCloseMsg>(AvatarBuyDialogClose);
            Message.RemoveListener<StorBuyAvatarMsg>(StorBuyAvatar);
            Message.RemoveListener<SkinBuyDialogCloseMsg>(SkinBuyDialogClose);
            Message.RemoveListener<StorBuySkinMsg>(StorBuySkin);
            Message.RemoveListener<SkinBuyButtonMsg>(SkinBuyButton);
        }
    }
}