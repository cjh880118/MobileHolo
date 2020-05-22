using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Constants;
using System;
using JHchoi.Contents;

namespace JHchoi.UI.Event
{
    public class MusicListReceiveMsg : Message
    {
        public Playlist playlist;
        public MusicListReceiveMsg(Playlist playlist)
        {
            this.playlist = playlist;
        }
    }

    public class MusicSettingMsg : Message
    {
        public string musicTitle;
        public string musicArtist;
        public double musicTime;
        public MusicSettingMsg(string title, string artist, double musicTime)
        {
            musicTitle = title;
            musicArtist = artist;
            this.musicTime = musicTime;
        }
    }

    public class NowSelectMusicMsg : Message
    {
        public string musicTitle;
        public string musicArtist;
        public double musicTime;
        public NowSelectMusicMsg(string title, string artist, double musicTime)
        {
            musicTitle = title;
            musicArtist = artist;
            this.musicTime = musicTime;
        }
    }

    public class MusicCommandMsg : Message
    {
        public MUSICINFO musicInfo;
        public MusicCommandMsg(MUSICINFO musicInfo)
        {
            this.musicInfo = musicInfo;
        }
    }

    public class MusicSettingResetMsg : Message
    {

    }

    public class MusicVolumeRequestMsg : Message
    {

    }

    public class MusicVolumeSetMsg : Message
    {
        public float volume;
        public MusicVolumeSetMsg(float volume)
        {
            this.volume = volume;
        }
    }

    public class MusicTimeSearchMsg : Message
    {
        public double nowTime;
        public int musicIndex;
        public MusicTimeSearchMsg(double nowTime, int index)
        {
            this.nowTime = nowTime;
            this.musicIndex = index;
        }
    }

    public class MusicTimeSetMsg : Message
    {
        public double totalTime;
        public double nowTime;
        public MusicTimeSetMsg(double totalTime, double nowTime)
        {
            this.totalTime = totalTime;
            this.nowTime = nowTime;
        }
    }

    public class MusicPlayTimeMsg : Message
    {
        public float nowTime;
        public MusicPlayTimeMsg(float nowTime)
        {
            this.nowTime = nowTime;
        }
    }

    public class MusicListSendMsg : Message
    {
        public Playlist playlist;
        public MusicListSendMsg(Playlist playlist)
        {
            this.playlist = playlist;
        }
    }

    public class MusicListCloseMsg : Message
    {

    }

    public class MusicItemSelectMsg : Message
    {
        public int index;
        public MusicItemSelectMsg(int index)
        {
            this.index = index;
        }
    }

    public class MusicMainDialogCommandSetMsg : Message
    {
        public MUSICINFO musicInfo;
        public MusicMainDialogCommandSetMsg(MUSICINFO musicInfo)
        {
            this.musicInfo = musicInfo;
        }
    }

    public class MusicPlaySettingMsg : Message
    {
        public MusicSetting musicSetting;
        public MusicPlaySettingMsg(MusicSetting musicSetting)
        {
            this.musicSetting = musicSetting;
        }
    }
}

