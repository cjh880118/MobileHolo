using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JHchoi.Constants;
using JHchoi.UI.Event;


namespace JHchoi.UI
{
    public class GameSelectDialog : IDialog
    {
        public Button btnClose;
        public Button btnBackGround;
        public Button btnAloneGame;
        public Button btnRhythmGame;

        protected override void OnLoad()
        {
            btnClose.onClick.AddListener(() => Message.Send<GameSelectCloseMsg>(new GameSelectCloseMsg()));
            btnBackGround.onClick.AddListener(() => Message.Send<GameSelectCloseMsg>(new GameSelectCloseMsg()));
            btnAloneGame.onClick.AddListener(() => Message.Send<RunGameMsg>(new RunGameMsg(GameType.AlongGame)));
            btnRhythmGame.onClick.AddListener(() => Message.Send<RunGameMsg>(new RunGameMsg(GameType.RhythmGame)));
        }
    }
}