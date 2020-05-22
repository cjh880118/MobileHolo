using JHchoi.Constants;
using JHchoi.UI.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class StoreCashDialog : IDialog
    {
        public Button bntClose;
        public Button bntBackGround;
        public GameObject parent;
        public GameObject buff_Item;

        protected override void OnLoad()
        {
            bntClose.onClick.AddListener(() => Message.Send<CloseStoreDialogMsg>(new CloseStoreDialogMsg(StoreMenu.CashShop)));
            bntBackGround.onClick.AddListener(() => Message.Send<CloseStoreDialogMsg>(new CloseStoreDialogMsg(StoreMenu.CashShop)));
        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<SetCashStoreMsg>(SetCashStore);
        }

        private void SetCashStore(SetCashStoreMsg msg)
        {
            Cash_Table.Sheet tempBuff = msg.cash_Table.sheets[0];

            if (parent.transform.childCount == tempBuff.list.Count)
            {
                return;
            }
            else
            {
                for (int i = 0; i < parent.transform.childCount; i++)
                {
                    Destroy(parent.transform.GetChild(i).gameObject);
                }
            }

            float height = buff_Item.GetComponent<RectTransform>().sizeDelta.y;
            float blank = 10.0f;
            float parentHeight = (height + blank) * (tempBuff.list.Count) + blank;
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x, parentHeight);

            for (int i = 0; i < tempBuff.list.Count; i++)
            {
                GameObject temp_Obj = GameObject.Instantiate(buff_Item) as GameObject;
                temp_Obj.transform.parent = parent.transform;
                temp_Obj.transform.localScale = new Vector3(1, 1, 1);
                temp_Obj.transform.localPosition = new Vector3(0, -(blank + height) * i, 0);
                temp_Obj.GetComponent<Cash_Item_Controller>().InitItemButton(tempBuff.list[i].Index, tempBuff.list[i].name, tempBuff.list[i].price.ToString());
            }
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<SetCashStoreMsg>(SetCashStore);
        }
    }
}