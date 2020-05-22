using CellBig.Constants;
using CellBig.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CellBig.UI
{
    public class RhythmGameMainDialog : IDialog
    {
        public RawImage imgMainCharacter;
        public GameObject combo;
        public Text txtCombo;

        public Button btnRight;
        public Button btnLeft;

        protected override void OnLoad()
        {
            btnRight.onClick.AddListener(() => BtnOnClick(true));
            btnLeft.onClick.AddListener(() => BtnOnClick(false));
        }

        void BtnOnClick(bool isRight)
        {
            Message.Send<RhythmGameBtnTouchMsg>(new RhythmGameBtnTouchMsg(isRight));
        }

        protected override void OnEnter()
        {
            combo.SetActive(false);
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<RhythmGameSetMsg>(RhythmGameSet);
            Message.AddListener<RhythmGameComboMsg>(RhythmGameCombo);
        }

        private void RhythmGameSet(RhythmGameSetMsg msg)
        {
            string path = "UIImage/Character/";
            if (msg.mainCharacter == Character.Boy)
            {
                path = path + "BoyRtt";
            }
            else
            {
                path = path + "GirlRtt";
            }
            imgMainCharacter.texture = Resources.Load(path) as Texture;
        }

        private void RhythmGameCombo(RhythmGameComboMsg msg)
        {
            if (msg.combo > 1)
                combo.SetActive(true);
            else
                combo.SetActive(false);

            txtCombo.text = msg.combo.ToString();
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<RhythmGameSetMsg>(RhythmGameSet);
            Message.RemoveListener<RhythmGameComboMsg>(RhythmGameCombo);
        }
    }
}