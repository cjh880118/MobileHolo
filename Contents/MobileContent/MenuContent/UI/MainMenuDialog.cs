using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JHchoi.Constants;
using JHchoi.UI.Event;
using JHchoi.Android;

namespace JHchoi.UI
{
    public class MainMenuDialog : IDialog
    {
        public Button btnWatch;
        public Button btnMusic;
        public Button btnGame;
        public Button btnHolostar;
        public Button btnStore;
        public Button btnOption;
        public Button btnBluetoothList;
        public Button btnCalander;

        protected override void OnLoad()
        {
            btnWatch.onClick.AddListener(()=> Message.Send<RunMenuMsg>(new RunMenuMsg(Menu.Watch)));
            btnMusic.onClick.AddListener(()=> Message.Send<RunMenuMsg>(new RunMenuMsg(Menu.Music)));
            btnGame.onClick.AddListener(() => Message.Send<RunMenuMsg>(new RunMenuMsg(Menu.Game)));
            btnHolostar.onClick.AddListener(() => Message.Send<RunMenuMsg>(new RunMenuMsg(Menu.HoloStar)));
            btnStore.onClick.AddListener(() => Message.Send<RunMenuMsg>(new RunMenuMsg(Menu.Store)));
            btnOption.onClick.AddListener(() => Message.Send<RunMenuMsg>(new RunMenuMsg(Menu.Option)));
            btnBluetoothList.onClick.AddListener(() => AndroidTrasferMgr.Instance.SearchDevice());
            btnCalander.onClick.AddListener(() => AndroidTrasferMgr.Instance.GetCalendar());
        }

        protected override void OnEnter()
        {

        }

        protected override void OnExit()
        {

        }

        protected override void OnUnload()
        {

        }
    }
}