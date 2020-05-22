using System;
using UnityEngine;
using System.Collections.Generic;
using CellBig.Constants;
using System.IO;

namespace CellBig.Models
{
    public class SettingModel : Model
    {
        GameModel _owner;

        public LocalizingType LocalizingType = LocalizingType.KR;

        public void Setup(GameModel owner)
        {
            _owner = owner;
        }

        float completeSpanTime = 30;               //second      
        float managementRecoverTime = 10;        //second
        int scheduleCount = 20;
        int manageMentTotalCount = 20;
        bool isBluetoothConnet = false;
        bool isBluetootON;

        public float CompleteSpanTime { get => completeSpanTime; set => completeSpanTime = value; }
        public float ManagementRecoverTime { get => managementRecoverTime; set => managementRecoverTime = value; }
        public int ScheduleCount { get => scheduleCount; set => scheduleCount = value; }
        public int ManageMentTotalCount { get => manageMentTotalCount; set => manageMentTotalCount = value; }
        public bool IsBluetoothConnet { get => isBluetoothConnet; set => isBluetoothConnet = value; }
        public bool IsBluetootON { get => isBluetootON; set => isBluetootON = value; }
     
        public string GetLocalizingPath()
        {
            string path = "";
            switch (LocalizingType)
            {
                case LocalizingType.KR: path = "KR"; break;
                case LocalizingType.JP: path = "JP"; break;
                case LocalizingType.EN: path = "EN"; break;
                case LocalizingType.CH: path = "CH"; break;
            }
            return LocalizingType.ToString();
        }
    }
}
