using System;
using System.Collections;
using System.Collections.Generic;
using JHchoi.Constants;
using JHchoi.UI.Event;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class AloneGameBuffBuyDialog : IDialog
    {
        public Button btnClose;
        public Button btnBackGround;
        public Button btnOK;
        public Image imgBuffIcon;
        public Text txtInfo;
        public int index;
        public int cost;

        protected override void OnLoad()
        {
            btnClose.onClick.AddListener(() => Message.Send<ItmeIsBuyDialogMsg>(new ItmeIsBuyDialogMsg(Upgrade.Buff, false, index, cost)));
            btnBackGround.onClick.AddListener(() => Message.Send<ItmeIsBuyDialogMsg>(new ItmeIsBuyDialogMsg(Upgrade.Buff, false, index, cost)));
            btnOK.onClick.AddListener(() => Message.Send<ItmeIsBuyDialogMsg>(new ItmeIsBuyDialogMsg(Upgrade.Buff, true, index, cost)));
        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<BuyDialogSetMsg>(BuyDialogSet);
        }

        private void BuyDialogSet(BuyDialogSetMsg msg)
        {
            imgBuffIcon.sprite = Resources.Load<Sprite>(msg.imgPath) as Sprite;
            imgBuffIcon.SetNativeSize();
            Debug.Log("BuyDialogSetMsg :: " + msg.index);
            this.index = msg.index;
            this.cost = msg.cost;
            txtInfo.text = String.Format("코인 {0}을 사용하여 해당 버프를 구매 하시겠습니까?", msg.cost.ToString());
            if (msg.isBuyPossible)
            {
                btnOK.enabled = true;
                btnOK.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 1f);
            }
            else
            {
                btnOK.enabled = false;
                btnOK.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 0.5f);
            }
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<BuyDialogSetMsg>(BuyDialogSet);
        }
    }
}