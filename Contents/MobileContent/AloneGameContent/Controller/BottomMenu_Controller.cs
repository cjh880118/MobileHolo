using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CellBig.UI.Event;
using CellBig.Constants;

namespace CellBig.UI
{
    public class BottomMenu_Controller : MonoBehaviour
    {
        public Button btnAlongGame;
        public Button btnAlongGameAvatar;
        public Button btnAlongGameSkill;
        public Button btnAlongGameOption;

        public void Start()
        {
            btnAlongGame.onClick.AddListener(() => Message.Send<AloneGameRunMenuMsg>(new AloneGameRunMenuMsg(AloneGameMenu.MainGame)));
            btnAlongGameAvatar.onClick.AddListener(() => Message.Send<AloneGameRunMenuMsg>(new AloneGameRunMenuMsg(AloneGameMenu.Avatar)));
            btnAlongGameSkill.onClick.AddListener(() => Message.Send<AloneGameRunMenuMsg>(new AloneGameRunMenuMsg(AloneGameMenu.Upgrade)));
            btnAlongGameOption.onClick.AddListener(() => Message.Send<AloneGameRunMenuMsg>(new AloneGameRunMenuMsg(AloneGameMenu.Option)));
        }
    }
}