using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class StoreSkinBuyDialog : IDialog
    {
        public Button btnClose;
        public Button btnBackGround;
        public Button btnOK;
        public Text txtOK;
        public Text txtInfo;
        int index;
        bool isBuy;

        protected override void OnLoad()
        {
            btnClose.onClick.AddListener(() => Message.Send<SkinBuyDialogCloseMsg>(new SkinBuyDialogCloseMsg()));
            btnBackGround.onClick.AddListener(() => Message.Send<SkinBuyDialogCloseMsg>(new SkinBuyDialogCloseMsg()));
            btnOK.onClick.AddListener(() => Message.Send<StorBuySkinMsg>(new StorBuySkinMsg(isBuy, index)));
        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<SkinBuyInfoMsg>(SkinBuyInfo);
        }

        private void SkinBuyInfo(SkinBuyInfoMsg msg)
        {
            if (msg.isBuy)
            {
                txtOK.text = "구매";
                if (msg.isBuyPossible)
                {
                    //돈 있음
                    index = msg.itemIndex;
                    isBuy = true;
                    txtInfo.text = "해당 스킨을 구매하시겠습니까?";
                    btnOK.enabled = true;
                }
                else
                {
                    //돈 부족
                    txtInfo.text = "해당 스킨을 구매할 코인이 부족합니다.";
                    btnOK.enabled = false;
                }
            }
            else
            {
                index = msg.itemIndex;
                isBuy = false;
                txtOK.text = "적용";
                txtInfo.text = "해당 스킨으로 변경하시겠습니까?";
                btnOK.enabled = true;
            }
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<SkinBuyInfoMsg>(SkinBuyInfo);
        }
    }
}
