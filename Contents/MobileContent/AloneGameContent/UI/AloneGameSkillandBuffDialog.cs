using CellBig.UI.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CellBig.Constants;
using System;

namespace CellBig.UI
{
    public class AloneGameSkillandBuffDialog : IDialog
    {
        public Button btnSkill;
        public Button btnBuff;

        protected override void OnLoad()
        {
            btnSkill.onClick.AddListener(() => Message.Send<AloneGameSkillorBuffSelectMsg>(new AloneGameSkillorBuffSelectMsg(Upgrade.Skill)));
            btnBuff.onClick.AddListener(() => Message.Send<AloneGameSkillorBuffSelectMsg>(new AloneGameSkillorBuffSelectMsg(Upgrade.Buff)));
        }

        protected override void OnEnter()
        {
            AddMessage();
            Message.Send<AloneGameSkillorBuffSelectMsg>(new AloneGameSkillorBuffSelectMsg(Upgrade.Skill));
        }

        private void AddMessage()
        {
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
        }
    }
}