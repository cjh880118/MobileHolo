using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Models;
using JHchoi.UI.Event;
using JHchoi.Constants;
using JHchoi.Android;

namespace JHchoi.Contents
{
    public class SettingContnet : IContent
    {
        static string TAG = "AloneGameContent :: ";

        MobileOptionModel mobileOptionModel;
        HolostarSettingModel holostarSettingModel;
        SettingModel settingModel;
        Coroutine btnEvent;

        protected override void OnLoadStart()
        {
            mobileOptionModel = Model.First<MobileOptionModel>();
            holostarSettingModel = Model.First<HolostarSettingModel>();
            settingModel = Model.First<SettingModel>();
            SetLoadComplete();
        }

        protected override void OnEnter()
        {
            Debug.Log(TAG + "OnEnter");
            btnEvent = StartCoroutine(ButtonInput());
            AddMessage();
            IContent.RequestContentExit<MenuContent>();
            UI.IDialog.RequestDialogEnter<UI.SettingMainDialog>();
            Message.Send<SetOptionVolumeMsg>(new SetOptionVolumeMsg(mobileOptionModel.MobileOption));
            AndroidTrasferMgr.Instance.IsBluetoothOn();
        }

        IEnumerator ButtonInput()
        {
            while (true)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log(TAG + " : Escape");
                    IContent.RequestContentEnter<MenuContent>();
                }
            }
        }

        private void AddMessage()
        {
            Message.AddListener<SettingSendTabletMsg>(SettingSendTablet);
            Message.AddListener<SettingButtonSetMsg>(SettingButtonSet);
            Message.AddListener<SettingVolumeMsg>(SettingVolume);
        }

        private void SettingSendTablet(SettingSendTabletMsg msg)
        {
            if (settingModel.IsBluetoothConnet)
            {
                string temp = JsonUtility.ToJson(holostarSettingModel.HoloStarSetting);
                AndroidTrasferMgr.Instance.BluetoothSendMsg(temp, SENDMSGTYPE.SETTING);
            }
        }

        private void SettingButtonSet(SettingButtonSetMsg msg)
        {
            if (msg.option == OptionSet.Bluetooth)
            {
                if (settingModel.IsBluetoothConnet)
                {
                    settingModel.IsBluetoothConnet = false;
                }

                AndroidTrasferMgr.Instance.BluetoothTurnOn(msg.isON);
            }
            if (msg.option == OptionSet.Schedule)
            {
                holostarSettingModel.HoloOptionSetting.isSchedule = msg.isON;
            }
            if (msg.option == OptionSet.TTS)
            {
                holostarSettingModel.HoloOptionSetting.isTTSReceive = msg.isON;
            }
            if (msg.option == OptionSet.SMS)
            {
                holostarSettingModel.HoloOptionSetting.isMMSReceive = msg.isON;
            }

            if (msg.option != OptionSet.Bluetooth)
                Message.Send<SettingDialogSetMsg>(new SettingDialogSetMsg(
                   holostarSettingModel.HoloOptionSetting,
                   settingModel.IsBluetootON,
                   settingModel.IsBluetoothConnet));
        }

        private void SettingVolume(SettingVolumeMsg msg)
        {
            if (msg.isGame)
                mobileOptionModel.MobileOption.gameVolume = msg.value;
            else
                mobileOptionModel.MobileOption.effectVolume = msg.value;
        }

        protected override void OnExit()
        {
            if (btnEvent != null)
                StopCoroutine(btnEvent);

            DialogAllClose();
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<SettingSendTabletMsg>(SettingSendTablet);
            Message.RemoveListener<SettingButtonSetMsg>(SettingButtonSet);
            Message.RemoveListener<SettingVolumeMsg>(SettingVolume);
        }
    }
}