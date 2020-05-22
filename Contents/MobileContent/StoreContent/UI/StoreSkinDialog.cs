using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JHchoi.UI.Event;
using JHchoi.Constants;
using System;

namespace JHchoi.UI
{
    public class StoreSkinDialog : IDialog
    {
        public GameObject itemButton;
        public GameObject parent;

        public GameObject itemCharacterButton;
        public GameObject characterParent;

        public Text txtCoin;
        public Text txtButton;
        public Button btnBuy;
        public RawImage imgNowCharacter;
        public Button bntClose;
        public Button bntBackGround;
        bool isBuy;
        bool isBuyPossible;
        int index;

        protected override void OnLoad()
        {
            btnBuy.onClick.AddListener(() => Message.Send<SkinBuyButtonMsg>(new SkinBuyButtonMsg(isBuy, isBuyPossible, index)));
            bntClose.onClick.AddListener(() => Message.Send<CloseStoreDialogMsg>(new CloseStoreDialogMsg(StoreMenu.SkinShop)));
            bntBackGround.onClick.AddListener(() => Message.Send<CloseStoreDialogMsg>(new CloseStoreDialogMsg(StoreMenu.SkinShop)));
        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<StoreSkinNowMsg>(StoreSkinNow);
            Message.AddListener<SetSkinStoreMsg>(SetSkinStore);
            Message.AddListener<SetSkinStoreCharacterMsg>(SetSkinStoreCharacter);
            Message.AddListener<StoreSkinBtnTextMsg>(StoreSkinBtnText);
        }

        private void SetSkinStoreCharacter(SetSkinStoreCharacterMsg msg)
        {
            int count = msg.ListCharacter.Count;
            int index = (int)msg.character;
            float height = itemCharacterButton.GetComponent<RectTransform>().sizeDelta.y;
            characterParent.GetComponent<RectTransform>().sizeDelta = new Vector2( characterParent.GetComponent<RectTransform>().sizeDelta.x, height * count);
            characterParent.GetComponent<RectTransform>().anchoredPosition = new Vector3(-420, 0, 0);

            for (int i = 0; i < characterParent.transform.childCount; i++)
            {
                Destroy(characterParent.transform.GetChild(i).gameObject);
            }

            for (int i = 0; i < count; i++)
            {
                GameObject temp = GameObject.Instantiate(itemCharacterButton) as GameObject;
                temp.transform.parent = characterParent.transform;
                temp.GetComponent<Avatar_Item_Controller>().InitButton(msg.ListCharacter[i].index, msg.ListCharacter[i].path, msg.ListCharacter[i].cost, msg.ListIsHave[i]);
                temp.transform.localScale = new Vector3(1, 1, 1);
                temp.transform.localPosition = new Vector3(0, (-height * i), 0);
            }
        }

        private void StoreSkinNow(StoreSkinNowMsg msg)
        {
            btnBuy.gameObject.SetActive(false);
        }

        private void SetSkinStore(SetSkinStoreMsg msg)
        {
            string path;
            if (msg.character == Character.Boy)
            {
                path = "UIImage/Character/BoyRtt";
            }
            else
            {
                path = "UIImage/Character/GirlRtt";
            }
            imgNowCharacter.texture = Resources.Load<Texture>(path);

            txtCoin.text = string.Format("코인 : {0}", msg.coin);
            btnBuy.gameObject.SetActive(false);

            int childCount = parent.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(parent.transform.GetChild(i).gameObject);
            }

            int skinCount = msg.ListIsHave.Count;
            float itemY = itemButton.GetComponent<RectTransform>().sizeDelta.y;
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x, itemY * skinCount);
            parent.GetComponent<RectTransform>().anchoredPosition = new Vector3(420, 0, 0);

            for (int i = 0; i < skinCount; i++)
            {
                GameObject temp = GameObject.Instantiate(itemButton) as GameObject;
                temp.transform.parent = parent.transform;
                temp.transform.localScale = Vector3.one;
                temp.transform.localPosition = new Vector3(0, -itemY * i, 0);
                temp.GetComponent<Skin_Item_Controller>().InitButton(msg.ListSkin[i].index, msg.ListSkin[i].path, msg.ListSkin[i].cost, msg.ListIsHave[i]);
            }
        }



        private void StoreSkinBtnText(StoreSkinBtnTextMsg msg)
        {
            isBuy = msg.isBuy;
            isBuyPossible = msg.isBuyPossible;
            index = msg.itemIndex;
            btnBuy.gameObject.SetActive(true);
            if (msg.isBuy)
                txtButton.text = "구매";
            else
                txtButton.text = "적용";
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<StoreSkinNowMsg>(StoreSkinNow);
            Message.RemoveListener<SetSkinStoreMsg>(SetSkinStore);
            Message.RemoveListener<SetSkinStoreCharacterMsg>(SetSkinStoreCharacter);
            Message.RemoveListener<StoreSkinBtnTextMsg>(StoreSkinBtnText);
        }
    }
}
