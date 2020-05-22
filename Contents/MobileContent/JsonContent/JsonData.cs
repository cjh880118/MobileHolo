using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellBig.Constants;
using System;

namespace CellBig.Contents
{
    [Serializable]
    public class GameSchedule
    {
        public AloneGameSchedule[] alonGameShedule;
    }

    [Serializable]
    public class AloneGameSchedule
    {
        public int index;
        public Schedule schedule;
        public string dateTime;
    }

    [Serializable]
    public class PlayerStatus
    {
        public int vocal;
        public int vocalGage;
        public int vocalSkill;
        public int dance;
        public int danceGage;
        public int danceSkill;
        public int entertainment;
        public int entertainmentGage;
        public int entertainmentSkill;
        public int intelligence;
        public int intelligenceGage;
        public int intelligenceSkill;
        public int coin;
        public int potential;
        public int relaxSkill;
        public int selfManagementSkill;
        public int manageMentCout;
        public string manageMentUseTime;
    }

    [Serializable]
    public class PlayerInventory
    {
        public Character nowCharacter;
        public int nowSkin;
        public int[] buyCharacter;
        public int[] buySkinNum;
    }


    [Serializable]
    public class WeatherInfo
    {
        [SerializeField]
        public Weather[] weather;

        [Serializable]
        public class Weather
        {
            public int id;
            public string main;
            public string description;
            public string icon;
        }

        public Main main;

        [System.Serializable]
        public class Main
        {
            public float temp;
        }
    }

    [Serializable]
    public class LocationInfo
    {
        [SerializeField]
        public Location[] results;

        [Serializable]
        public class Location
        {
            public string formatted_address;
        }
    }

    [System.Serializable]
    public class Playlist
    {
        public List<Music> data;
    }

    [System.Serializable]
    public class Music
    {
        public int index;
        public string title;
        public string artist;
        public string path;
        public double duration;
    }

    [System.Serializable]
    public class CalendarList
    {
        public List<CalendarEvent> data;
    }

    [System.Serializable]
    public class CalendarEvent
    {
        public string title;
        public long dtstart;
        public string year;
        public string month;
        public string day;
        public string hour;
        public string minute;
        public string second;
    }

    [System.Serializable]
    public class HoloStarSetting
    {
        public MusicSetting musicSetting;
        public HoloOptionSetting holoOptionSetting;
    }

    [System.Serializable]
    public class MusicSetting
    {
        public bool isMusicPlay;
        public int musicIndex;
        public double musicNowTime;
        public bool isRepeat;
        public float musicVolume;
    }

    [System.Serializable]
    public class HoloOptionSetting
    {
        public bool isSchedule;
        public bool isTTSReceive;
        public bool isMMSReceive;
        public bool isMute;
        public float TabletVolume;
    }

    [System.Serializable]
    public class BluetoothData
    {
        public SENDMSGTYPE dataType;
        public MUSICINFO musicInfo;
        public Menu menu;
        public string msg;
    }

    [System.Serializable]
    public class MobileOption
    {
        public float gameVolume;
        public float effectVolume;
    }

    [System.Serializable]
    public class AlarmEvent
    {
        public string title;
        public long tick;
    }

    [System.Serializable]
    public class Sorting : IComparer<AlarmEvent>
    {
        public int Compare(AlarmEvent a, AlarmEvent b)
        {
            return a.tick.CompareTo(b.tick);
        }
    }

    [System.Serializable]
    public class CalanderEvent
    {
        public AlarmEvent[] alarmEvents;
    }

}