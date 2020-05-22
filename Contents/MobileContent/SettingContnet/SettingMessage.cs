using JHchoi.Constants;
using JHchoi.Contents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.UI.Event
{
    public class SettingDialogSetMsg : Message
    {
        public HoloOptionSetting holoOptionSetting;
        public bool isBluetoothON;
        public bool isBluetoothConnet;
        public SettingDialogSetMsg(HoloOptionSetting holoOptionSetting, bool isBluetoothON, bool isBluetoothConnet)
        {
            this.holoOptionSetting = holoOptionSetting;
            this.isBluetoothON = isBluetoothON;
            this.isBluetoothConnet = isBluetoothConnet;
        }
    }

    public class SettingButtonSetMsg : Message
    {
        public OptionSet option;
        public bool isON;
        public SettingButtonSetMsg(OptionSet option, bool isON)
        {
            this.option = option;
            this.isON = isON;
        }
    }

    public class SetOptionVolumeMsg : Message
    {
        public MobileOption mobileOption;
        public SetOptionVolumeMsg (MobileOption mobileOption)
        {
            this.mobileOption = mobileOption;
        }

    }

    public class SettingVolumeMsg : Message
    {
        public float value;
        public bool isGame;
        public SettingVolumeMsg(float value, bool isGame)
        {
            this.value = value;
            this.isGame = isGame;
        }
    }

    public class SettingSendTabletMsg : Message { }
}
