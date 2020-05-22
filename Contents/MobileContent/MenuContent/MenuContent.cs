using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.UI.Event;
using JHchoi.Constants;
using JHchoi.Models;
using JHchoi.Android;
using JHchoi.UI;

namespace JHchoi.Contents
{
    public class MenuContent : IContent
    {
        static string TAG = "MenuContent ::";
        SettingModel settingModel;
        MobileOptionModel optionModel;
        List<IDialog> ListUi = new List<IDialog>();

        protected override void OnLoadStart()
        {
            settingModel = Model.First<SettingModel>();
            optionModel = Model.First<MobileOptionModel>();
            SetLoadComplete();
        }

        protected override void OnLoadComplete()
        {
            foreach (var o in this.gameObject.GetComponent<IContentUILoader>()._uiList)
            {
                ListUi.Add(UIManager.Instance.Get(o).GetComponent<IDialog>());
            }
        }

        protected override void OnEnter()
        {
            AddMessage();
            //Android.AndroidTrasferMgr.Instance.GetCalendar();
            InitMenuContent();
        }

        void InitMenuContent()
        {
            UI.IDialog.RequestDialogEnter<UI.MainMenuDialog>();
            IContent.RequestContentExit<WatchContent>();
            IContent.RequestContentExit<AloneGameContent>();
            IContent.RequestContentExit<RhythmGameContent>();
            IContent.RequestContentExit<HolostarContent>();
            IContent.RequestContentExit<SettingContnet>();
            IContent.RequestContentExit<StoreContent>();
            IContent.RequestContentExit<MusicContent>();

            
            Message.Send<RunMenuMsg>(new RunMenuMsg(Menu.Main));
        }

        private void AddMessage()
        {
            Message.AddListener<RunMenuMsg>(RunMenu);
            Message.AddListener<RunGameMsg>(RunGame);
            Message.AddListener<GameSelectCloseMsg>(GameSelectClose);
        }

        //테블릿에서 받은 메뉴 이동
        private void RunMenu(RunMenuMsg msg)
        {
            Debug.Log(TAG + "RunMenu");

            SoundManager.Instance.PlaySound((int)SoundType.EFFECT_BTN_DOWN, optionModel.MobileOption.effectVolume);

            if (msg.menu == Menu.Watch)
                IContent.RequestContentEnter<WatchContent>();
            else if (msg.menu == Menu.Music)
                IContent.RequestContentEnter<MusicContent>();
            else if (msg.menu == Menu.Game)
                UI.IDialog.RequestDialogEnter<UI.GameSelectDialog>();
            else if (msg.menu == Menu.Store)
                IContent.RequestContentEnter<StoreContent>();
            else if (msg.menu == Menu.HoloStar)
                IContent.RequestContentEnter<HolostarContent>();
            else if (msg.menu == Menu.Option)
                IContent.RequestContentEnter<SettingContnet>();
       

            if (settingModel.IsBluetoothConnet && msg.menu != Menu.Option)
                AndroidTrasferMgr.Instance.BluetoothSendMsg("", SENDMSGTYPE.MENU, MUSICINFO.None, msg.menu);
        }

        private void RunGame(RunGameMsg msg)
        {

            if (msg.menu == GameType.AlongGame)
            {
                IContent.RequestContentEnter<AloneGameContent>();
            }
            else if (msg.menu == GameType.RhythmGame)
            {
                IContent.RequestContentEnter<RhythmGameContent>();
                Message.Send<RhythmGameBluetoothSelectMsg>(new RhythmGameBluetoothSelectMsg(false));

            }

            if (settingModel.IsBluetoothConnet)
                AndroidTrasferMgr.Instance.BluetoothSendMsg(msg.menu.ToString(), SENDMSGTYPE.GAME);
        }

        private void GameSelectClose(GameSelectCloseMsg msg)
        {
            SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Close, optionModel.MobileOption.effectVolume);
            UI.IDialog.RequestDialogExit<UI.GameSelectDialog>();
            UI.IDialog.RequestDialogEnter<UI.MainMenuDialog>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                for (int i = ListUi.Count - 1; i > 0; i--)
                {
                    if (ListUi[i].dialogView.activeSelf)
                    {
                        ListUi[i].ExitDialog();
                        break;
                    }
                }
            }
        }

        protected override void OnExit()
        {
            DialogAllClose();
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<RunMenuMsg>(RunMenu);
            Message.RemoveListener<RunGameMsg>(RunGame);
            Message.RemoveListener<GameSelectCloseMsg>(GameSelectClose);
        }
    }
}