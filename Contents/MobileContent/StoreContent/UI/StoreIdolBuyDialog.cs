using CellBig.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CellBig.UI
{
    public class StoreIdolBuyDialog : IDialog
    {
        public Button btnClose;
        public Button btnBackGround;
        public Button btnOK;
        //public Text txtBtnMessage;
        public Text txtOK;
        public Text txtInfo;
        int index;
        bool isBuy;

        protected override void OnLoad()
        {
            btnClose.onClick.AddListener(() => Message.Send<AvatarBuyDialogCloseMsg>(new AvatarBuyDialogCloseMsg()));
            btnBackGround.onClick.AddListener(() => Message.Send<AvatarBuyDialogCloseMsg>(new AvatarBuyDialogCloseMsg()));
            btnOK.onClick.AddListener(() => Message.Send<StorBuyAvatarMsg>(new StorBuyAvatarMsg(isBuy, index)));
        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<AvatarBuyInfoMsg>(AvatarBuyInfo);
        }

        private void AvatarBuyInfo(AvatarBuyInfoMsg msg)
        {
            if (msg.isBuy)
            {
                txtOK.text = "구매";
                if (msg.isBuyPossible)
                {
                    //돈 있음
                    index = msg.itemIndex;
                    isBuy = true;
                    txtInfo.text = "해당 아바타를 구매하시겠습니까?";
                    btnOK.enabled = true;
                }
                else
                {
                    //돈 부족
                    txtInfo.text = "해당 아바타를 구매할 잠재력이 부족합니다.";
                    btnOK.enabled = false;
                }
            }
            else
            {
                index = msg.itemIndex;
                isBuy = false;
                txtOK.text = "적용";
                txtInfo.text = "해당 아바타로 변경하시겠습니까?";
                btnOK.enabled = true;
            }
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<AvatarBuyInfoMsg>(AvatarBuyInfo);
        }
    }
}