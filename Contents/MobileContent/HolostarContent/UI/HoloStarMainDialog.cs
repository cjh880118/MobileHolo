using JHchoi.Constants;
using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Midiazen;

namespace JHchoi.UI
{
    public class HoloStarMainDialog : IDialog
    {
        public RawImage imgMainCharacter;
        public Button btnImg;
        public Button btnStt;
        public Text txtMsg;

        protected override void OnLoad()
        {
            btnImg.onClick.AddListener(() => Message.Send<HolostarImgClickMsg>(new HolostarImgClickMsg()));
            btnStt.onClick.AddListener(() => SttRecordStart());
        }

        void SttRecordStart()
        {
            txtMsg.text = "음성을 입력해주세요.";
            Message.Send<STTRecord>(new STTRecord());
        }

        protected override void OnEnter()
        {
            txtMsg.gameObject.SetActive(false);
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<HolostarStringMsg>(HolostarString);
            Message.AddListener<SetHolostarCharacterMsg>(SetHolostarCharacter);
        }

        private void HolostarString(HolostarStringMsg msg)
        {
            txtMsg.gameObject.SetActive(true);
            txtMsg.text = msg.msg;
        }

        private void SetHolostarCharacter(SetHolostarCharacterMsg msg)
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

            imgMainCharacter.texture = Resources.Load<Texture>(path);
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<HolostarStringMsg>(HolostarString);
            Message.RemoveListener<SetHolostarCharacterMsg>(SetHolostarCharacter);
        }
    }
}