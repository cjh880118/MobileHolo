using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellBig.Android;
using CellBig.UI.Event;
using System;
using CellBig.Constants;
using CellBig.Models;
using Midiazen;

namespace CellBig.Contents
{
    public class BluetoothContent : IContent
    {
        SettingModel settingModel;
        PlayerInventoryModel playerInventoryModel;
        HolostarSettingModel holostarSettingModel;
        MobileOptionModel mobileOptionModel;

        protected override void OnLoadStart()
        {
            StartCoroutine(AndroidTrasferMgr.Instance.SendCoroutine());
            mobileOptionModel = Model.First<MobileOptionModel>();
            settingModel = Model.First<SettingModel>();
            playerInventoryModel = Model.First<PlayerInventoryModel>();
            holostarSettingModel = Model.First<HolostarSettingModel>();
            SetLoadComplete();
        }

        protected override void OnEnter()
        {
            AddMessage();
            AndroidTrasferMgr.Instance.SearchDevice();
        }

        private void AddMessage()
        {
            Message.AddListener<BluetoothItemSelectMsg>(BluetoothItemSelect);
            Message.AddListener<BluetoothListCloseMsg>(BluetoothListClose);
            Message.AddListener<STTReceiveMsg>(STTReceive);
        }

        //todo..음성 인신 Intnet 전달
        private void STTReceive(STTReceiveMsg msg)
        {
            Android.AndroidTrasferMgr.Instance.BluetoothSendMsg(msg.intent, Constants.SENDMSGTYPE.INTENT);
        }

        private void BluetoothItemSelect(BluetoothItemSelectMsg msg)
        {
            AndroidTrasferMgr.Instance.SelectDevice(msg.name);
            UI.IDialog.RequestDialogEnter<UI.BluetoothLoadingDialog>();
        }

        private void BluetoothListClose(BluetoothListCloseMsg msg)
        {
            SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Close, mobileOptionModel.MobileOption.effectVolume);
            UI.IDialog.RequestDialogExit<UI.BluetoothListDialog>();
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<BluetoothItemSelectMsg>(BluetoothItemSelect);
            Message.RemoveListener<BluetoothListCloseMsg>(BluetoothListClose);
            Message.RemoveListener<STTReceiveMsg>(STTReceive);
        }

        //수신
        void BluetoothList(string msg)
        {
            string[] spstring = msg.Split('|');
            int deviceCnt = spstring.Length;
            List<string> bluetoothList = new List<string>();

            for (int i = 0; i < deviceCnt; i++)
            {
                bluetoothList.Add(spstring[i]);
            }

            //블루투스 다이얼로그 오픈
            UI.IDialog.RequestDialogEnter<UI.BluetoothListDialog>();
            Message.Send<BluetoothListMsg>(new BluetoothListMsg(bluetoothList));
        }

        void BluetoothConnectResult(string msg)
        {
            //연결 요청 결과
            UI.IDialog.RequestDialogExit<UI.BluetoothLoadingDialog>();
            if (msg == "ConnectedSuccess")
            {
                settingModel.IsBluetoothConnet = true;
                AndroidTrasferMgr.Instance.ShowToast("Bluetooth 연결에 성공하였습니다.");
                BluetoothListClose(new BluetoothListCloseMsg());
                //AndroidTrasferMgr.Instance.BluetoothSendMsg(JsonUtility.ToJson(playerInventoryModel.PlayerInventory), SENDMSGTYPE.CHARINFO);
                AndroidTrasferMgr.Instance.BluetoothSendMsg("True", SENDMSGTYPE.CONNECTION);
            }
            else
            {
                settingModel.IsBluetoothConnet = false;
                AndroidTrasferMgr.Instance.ShowToast("Bluetooth 연결에 연결할 HOLOSTAR를 찾지 못하였습니다.");
            }
        }

        //수신
        void BluetootEnableMsg(string msg)
        {
            if (msg == "Cancel")
            {
                AndroidTrasferMgr.Instance.ShowToast("블루투스를 키지 않으면 일부 기능은 사용할수 없습니다.");
            }
        }

        void ReceiveMsg(string msg)
        {
            BluetoothData data = JsonUtility.FromJson<BluetoothData>(msg);
            SENDMSGTYPE tempMsgType = data.dataType;

            if (tempMsgType == SENDMSGTYPE.MENU)
            {
                try
                {
                    IContent.RequestContentExit<WatchContent>();
                    IContent.RequestContentExit<AloneGameContent>();
                    IContent.RequestContentExit<RhythmGameContent>();
                    IContent.RequestContentExit<HolostarContent>();
                    IContent.RequestContentExit<SettingContnet>();
                    IContent.RequestContentExit<StoreContent>();
                    IContent.RequestContentExit<MusicContent>();

                    Menu tempMenu = data.menu;

                    if (tempMenu == Menu.Watch)
                    {
                        IContent.RequestContentEnter<WatchContent>();
                        Message.Send<RunMenuMsg>(new RunMenuMsg(tempMenu));
                    }
                    else if (tempMenu == Menu.Music)
                    {
                        IContent.RequestContentEnter<MusicContent>();
                        Message.Send<RunMenuMsg>(new RunMenuMsg(tempMenu));
                    }
                    else if (tempMenu == Menu.Game)
                    {
                        if (data.msg == "rhythm")
                        {
                            IContent.RequestContentEnter<RhythmGameContent>();
                            Message.Send<RhythmGameBluetoothSelectMsg>(new RhythmGameBluetoothSelectMsg(true));
                            Message.Send<RunMenuMsg>(new RunMenuMsg(tempMenu));
                        }
                        else
                        {
                            UI.IDialog.RequestDialogEnter<UI.GameSelectDialog>();
                            Message.Send<RunMenuMsg>(new RunMenuMsg(tempMenu));
                        }
                    }
                    else if (tempMenu == Menu.Store)
                    {
                        IContent.RequestContentEnter<StoreContent>();
                        Message.Send<RunMenuMsg>(new RunMenuMsg(tempMenu));
                    }
                    else if (tempMenu == Menu.HoloStar)
                    {
                        IContent.RequestContentEnter<HolostarContent>();
                        Message.Send<RunMenuMsg>(new RunMenuMsg(tempMenu));
                    }
                    else if (tempMenu == Menu.Option)
                        IContent.RequestContentEnter<SettingContnet>();
                    else if (tempMenu == Menu.Main)
                        IContent.RequestContentEnter<MenuContent>();
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
            }
            else if (tempMsgType == SENDMSGTYPE.MSG) { }
            else if (tempMsgType == SENDMSGTYPE.MUSIC)
            {
                if (data.musicInfo == MUSICINFO.MUSIC_PLAY)
                {
                    holostarSettingModel.MusicSetting.isMusicPlay = true;
                }
                else if (data.musicInfo == MUSICINFO.MUSIC_PAUSE)
                {
                    holostarSettingModel.MusicSetting.isMusicPlay = false;
                }

                Message.Send<BluetoothMusicCommandMsg>(new BluetoothMusicCommandMsg(data.musicInfo, data.msg));
            }
            else if (tempMsgType == SENDMSGTYPE.CHARINFO)
            {
                AndroidTrasferMgr.Instance.BluetoothSendMsg(JsonUtility.ToJson(playerInventoryModel.PlayerInventory), SENDMSGTYPE.CHARINFO);
            }
            else if (tempMsgType == SENDMSGTYPE.LOCATION) { }
            else if (tempMsgType == SENDMSGTYPE.CALENDAR) { }
            else if (tempMsgType == SENDMSGTYPE.SETTING)
            {
                HoloStarSetting holoStarSetting;
                holoStarSetting = JsonUtility.FromJson<HoloStarSetting>(data.msg);
                holostarSettingModel.HoloStarSetting = holoStarSetting;
                Message.Send<MusicPlaySettingMsg>(new MusicPlaySettingMsg(holostarSettingModel.HoloStarSetting.musicSetting));
            }
            else if (tempMsgType == SENDMSGTYPE.ANIMATION)
            {
                AnimationType aniType = (AnimationType)Enum.Parse(typeof(AnimationType), data.msg);
                Message.Send<SetCharacterAnimationMsg>(new SetCharacterAnimationMsg(aniType, true));
            }
            else if (tempMsgType == SENDMSGTYPE.CONNECTION)
            {
                if (!Convert.ToBoolean(data.msg))
                {
                    Message.Send<MusicSettingResetMsg>(new MusicSettingResetMsg());
                    settingModel.IsBluetoothConnet = false;
                    holostarSettingModel.MusicSetting.isMusicPlay = false;
                    Message.Send<SetCharacterAnimationMsg>(new SetCharacterAnimationMsg(AnimationType.Idel1, false));
                    AndroidTrasferMgr.Instance.ShowToast("홀로스타 본체와 연결이 끊어졌습니다.");
                }
            }
            else if (tempMsgType == SENDMSGTYPE.RECEIVE)
            {
                if (AndroidTrasferMgr.Instance.corTimer != null)
                {
                    StopCoroutine(AndroidTrasferMgr.Instance.corTimer);
                    AndroidTrasferMgr.Instance.corTimer = null;
                }

                AndroidTrasferMgr.Instance.IsSendPossible = true;

                if (AndroidTrasferMgr.Instance.ListMsg.Count > 0)
                {
                    Debug.Log("지워지는 데인터 :" + AndroidTrasferMgr.Instance.ListMsg[0]);
                    AndroidTrasferMgr.Instance.ListMsg.RemoveAt(0);
                }
            }
        }

        void IsBluetoothOn(string msg)
        {
            if (msg == "True")
                settingModel.IsBluetootON = true;
            else
                settingModel.IsBluetootON = false;

            Message.Send<SettingDialogSetMsg>(new SettingDialogSetMsg(
                holostarSettingModel.HoloOptionSetting,
                settingModel.IsBluetootON,
                settingModel.IsBluetoothConnet));
        }
    }
}