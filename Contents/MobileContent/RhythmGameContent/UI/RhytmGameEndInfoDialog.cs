using CellBig.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CellBig.UI
{
    public class RhytmGameEndInfoDialog : IDialog
    {
        public Button btnClose;
        public Button btnBackGround;
        public Button btnOK;

        protected override void OnLoad()
        {
            btnClose.onClick.AddListener(() => Message.Send<RhytmeGameEndInfoMsg>(new RhytmeGameEndInfoMsg(false)));
            btnBackGround.onClick.AddListener(() => Message.Send<RhytmeGameEndInfoMsg>(new RhytmeGameEndInfoMsg(false)));
            btnOK.onClick.AddListener(() => Message.Send<RhytmeGameEndInfoMsg>(new RhytmeGameEndInfoMsg(true)));
        }
    }
}