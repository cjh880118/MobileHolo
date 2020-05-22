using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellBig.Constants;
using System;
using CellBig.Contents;

namespace CellBig.UI.Event
{
    public class BluetoothListMsg : Message
    {
        public List<string> listBluetoothDevice;

        public BluetoothListMsg(List<string> listBluetoothDevice)
        {
            this.listBluetoothDevice = listBluetoothDevice;
        }
    }

    public class BluetoothMusicCommandMsg : Message
    {
        public MUSICINFO musicInfo;
        public string dataMsg;
        public BluetoothMusicCommandMsg(MUSICINFO musicInfo, string dataMsg)
        {
            this.musicInfo = musicInfo;
            this.dataMsg = dataMsg;
        }
    }

    public class BluetoothListCloseMsg : Message
    {
    }

    public class BluetoothItemSelectMsg : Message
    {
        public string name;
        public BluetoothItemSelectMsg(string name)
        {
            this.name = name;
        }
    }

    public class BluetoothReceiveRunMenuMsg : Message
    {
        public Menu menu;
        public BluetoothReceiveRunMenuMsg(string menuName)
        {
            this.menu = (Menu)Enum.Parse(typeof(Menu), menuName);
        }
    }

    public class TabletReceiveMsg : Message
    {
        public string msg;
        public TabletReceiveMsg(string msg)
        {
            this.msg = msg;
        }
    }

}
