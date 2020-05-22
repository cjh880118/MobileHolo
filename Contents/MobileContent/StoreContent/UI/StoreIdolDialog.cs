using JHchoi.Constants;
using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class StoreIdolDialog : IDialog
    {
        public Button bntClose;
        public Button bntBackGround;

        public GameObject itemButton;
        public GameObject parent;

        protected override void OnLoad()
        {
            bntClose.onClick.AddListener(() => Message.Send<CloseStoreDialogMsg>(new CloseStoreDialogMsg(StoreMenu.IdolShop)));
            bntBackGround.onClick.AddListener(() => Message.Send<CloseStoreDialogMsg>(new CloseStoreDialogMsg(StoreMenu.IdolShop)));
        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<SetCharacterStoreMsg>(SetCharacterStore);
        }

        private void SetCharacterStore(SetCharacterStoreMsg msg)
        {
            int count = msg.ListCharacter.Count;
            int index = (int)msg.character;
            float width = itemButton.GetComponent<RectTransform>().sizeDelta.x;
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(width * count, parent.GetComponent<RectTransform>().sizeDelta.y);
            parent.GetComponent<RectTransform>().anchoredPosition = new Vector3((-width * index), 0, 0);

            for(int i = 0; i < parent.transform.childCount; i++)
            {
                Destroy(parent.transform.GetChild(i).gameObject);
            }

            for (int i = 0; i < count; i++)
            {
                GameObject temp = GameObject.Instantiate(itemButton) as GameObject;
                temp.transform.parent = parent.transform;
                temp.GetComponent<Avatar_Item_Controller>().InitButton(msg.ListCharacter[i].index, msg.ListCharacter[i].path, msg.ListCharacter[i].cost, msg.ListIsHave[i]);
                temp.transform.localScale = new Vector3(1, 1, 1);
                temp.transform.localPosition = new Vector3((width * i), 0, 0);
            }
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<SetCharacterStoreMsg>(SetCharacterStore);
        }
    }
}