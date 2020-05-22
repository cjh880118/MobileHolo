using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JHchoi.UI.Event;
using JHchoi.Constants;
using System;

namespace JHchoi.UI
{
    public class AloneGameSkillUpgradeDialog : IDialog
    {
        public Button btnClose;
        public Button btnBackGround;
        public Button btnOK;
        public Image imgSkillIcon;
        public Text txtInfo;
        public int index;
        public int cost;

        protected override void OnLoad()
        {
            btnClose.onClick.AddListener(() => Message.Send<ItmeIsBuyDialogMsg>(new ItmeIsBuyDialogMsg(Upgrade.Skill, false, index, cost)));
            btnBackGround.onClick.AddListener(() => Message.Send<ItmeIsBuyDialogMsg>(new ItmeIsBuyDialogMsg(Upgrade.Skill, false, index, cost)));
            btnOK.onClick.AddListener(() => Message.Send<ItmeIsBuyDialogMsg>(new ItmeIsBuyDialogMsg(Upgrade.Skill, true, index, cost)));
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
            this.index = msg.index;
            this.cost = msg.cost;
            imgSkillIcon.sprite = Resources.Load<Sprite>(msg.imgPath) as Sprite;
            imgSkillIcon.SetNativeSize();
            txtInfo.text = String.Format("의 구매는 {0}의 잠재력이 필요합니다.", msg.cost.ToString());

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