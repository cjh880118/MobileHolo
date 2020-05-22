using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CellBig.Android;
using CellBig.Constants;
using CellBig.UI.Event;
using System;

namespace CellBig.UI
{
    public class MusicDialog : IDialog
    {
        public Button btnMusicList;
        public Button btnPlay;
        public Button btnPause;
        public Button btnNext;
        public Button btnPrev;
        public Button btnShuffle;
        public Button btnRepeat;
        public Button btnVolumeBar;
        public Text txtMusicTitle;
        public Text txtMusicTime;
        public Text txtMusicPlayTime;
        public Image imgMusicTime;
        public Slider SliderVolume;

        float totalTime;

        protected override void OnLoad()
        {
            SliderVolume.onValueChanged.AddListener(delegate { VolumeChange(); });
            btnMusicList.onClick.AddListener(() => Message.Send<MusicCommandMsg>(new MusicCommandMsg(MUSICINFO.MUSIC_LIST)));
            btnPlay.onClick.AddListener(() => Message.Send<MusicCommandMsg>(new MusicCommandMsg(MUSICINFO.MUSIC_PLAY)));
            btnPause.onClick.AddListener(() => Message.Send<MusicCommandMsg>(new MusicCommandMsg(MUSICINFO.MUSIC_PAUSE)));
            btnNext.onClick.AddListener(() => Message.Send<MusicCommandMsg>(new MusicCommandMsg(MUSICINFO.MUSIC_NEXT)));
            btnPrev.onClick.AddListener(() => Message.Send<MusicCommandMsg>(new MusicCommandMsg(MUSICINFO.MUSIC_PREV)));
            btnShuffle.onClick.AddListener(() => Message.Send<MusicCommandMsg>(new MusicCommandMsg(MUSICINFO.MUSIC_MODE_SHUFFLE)));
            btnRepeat.onClick.AddListener(() => Message.Send<MusicCommandMsg>(new MusicCommandMsg(MUSICINFO.MUSIC_MODE_REPEIT)));
            btnVolumeBar.onClick.AddListener(VolumeBarOpen);
        }

        private void VolumeChange()
        {
            Message.Send<MusicVolumeSetMsg>(new MusicVolumeSetMsg(SliderVolume.value));
        }

        private void VolumeBarOpen()
        {
            if (SliderVolume.gameObject.activeSelf)
                SliderVolume.gameObject.SetActive(false);
            else
            {
                Message.Send<MusicVolumeRequestMsg>(new MusicVolumeRequestMsg());
                SliderVolume.gameObject.SetActive(true);
            }
        }

        protected override void OnEnter()
        {
            SliderVolume.gameObject.SetActive(false);
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<NowSelectMusicMsg>(NowSelectMusic);
            Message.AddListener<MusicSettingResetMsg>(MusicSettingReset);
            Message.AddListener<MusicVolumeSetMsg>(MusicVolumeSet);
            Message.AddListener<MusicPlayTimeMsg>(MusicPlayTime);
            Message.AddListener<MusicTimeSetMsg>(MusicTimeSet);
            Message.AddListener<MusicPlaySettingMsg>(MusicPlaySetting);
            Message.AddListener<MusicMainDialogCommandSetMsg>(MusicMainDialogCommandSet);
            Message.AddListener<MusicSettingMsg>(MusicSetting);
        }

        private void NowSelectMusic(NowSelectMusicMsg msg)
        {
            txtMusicTitle.text = string.Format("{0} / {1}", msg.musicTitle, msg.musicArtist);
            txtMusicPlayTime.text = TimeToString(msg.musicTime);
        }

        private void MusicSettingReset(MusicSettingResetMsg msg)
        {
            btnPlay.gameObject.SetActive(true);
            btnPause.gameObject.SetActive(false);
            btnShuffle.gameObject.SetActive(true);
            btnRepeat.gameObject.SetActive(false);
            txtMusicTitle.text = "음악";
            txtMusicTime.text = "0:00";
            txtMusicPlayTime.text = "0:00"; ;
            imgMusicTime.fillAmount = 1f;
            SliderVolume.gameObject.SetActive(false); ;
        }

        private void MusicVolumeSet(MusicVolumeSetMsg msg)
        {
            SliderVolume.value = msg.volume;
        }

        private void MusicPlayTime(MusicPlayTimeMsg msg)
        {
            txtMusicPlayTime.text = TimeToString(msg.nowTime);
            imgMusicTime.fillAmount = msg.nowTime / totalTime;
        }

        private void MusicTimeSet(MusicTimeSetMsg msg)
        {
            totalTime = (float)msg.totalTime;
            float nowTime = (float)msg.nowTime;
            txtMusicTime.text = TimeToString(totalTime);
        }

        private void MusicPlaySetting(MusicPlaySettingMsg msg)
        {
            if (msg.musicSetting.isMusicPlay)
                MusicMainDialogCommandSet(new MusicMainDialogCommandSetMsg(MUSICINFO.MUSIC_PLAY));
            else
                MusicMainDialogCommandSet(new MusicMainDialogCommandSetMsg(MUSICINFO.MUSIC_PAUSE));

            if (msg.musicSetting.isRepeat)
                MusicMainDialogCommandSet(new MusicMainDialogCommandSetMsg(MUSICINFO.MUSIC_MODE_REPEIT));
            else
                MusicMainDialogCommandSet(new MusicMainDialogCommandSetMsg(MUSICINFO.MUSIC_MODE_SHUFFLE));

            Message.Send<MusicTimeSearchMsg>(new MusicTimeSearchMsg(msg.musicSetting.musicNowTime, msg.musicSetting.musicIndex));
        }

        private void MusicMainDialogCommandSet(MusicMainDialogCommandSetMsg msg)
        {
            if (msg.musicInfo == MUSICINFO.MUSIC_PLAY)
            {
                btnPlay.gameObject.SetActive(false);
                btnPause.gameObject.SetActive(true);
            }
            if (msg.musicInfo == MUSICINFO.MUSIC_PAUSE)
            {
                btnPlay.gameObject.SetActive(true);
                btnPause.gameObject.SetActive(false);
            }
            else if (msg.musicInfo == MUSICINFO.MUSIC_MODE_REPEIT)
            {
                btnRepeat.gameObject.SetActive(false);
                btnShuffle.gameObject.SetActive(true);
            }
            else if (msg.musicInfo == MUSICINFO.MUSIC_MODE_SHUFFLE)
            {
                btnRepeat.gameObject.SetActive(true);
                btnShuffle.gameObject.SetActive(false);
            }
        }

        private void MusicSetting(MusicSettingMsg msg)
        {
            txtMusicTitle.text = string.Format("{0} / {1}", msg.musicTitle, msg.musicArtist);
            txtMusicPlayTime.text = "0:00";
            MusicMainDialogCommandSet(new MusicMainDialogCommandSetMsg(MUSICINFO.MUSIC_PLAY));
        }

        string TimeToString(double time)
        {
            double tempTime = time;
            string sec;

            if (Math.Truncate(Convert.ToDouble(tempTime % 60)) < 10)
            {
                sec = "0" + Math.Truncate(Convert.ToDouble(tempTime % 60));
            }
            else
            {
                sec = Math.Truncate(Convert.ToDouble(tempTime % 60)).ToString();
            }

            return Math.Truncate((tempTime / 60)).ToString() + ":" + sec;
        }

        protected override void OnExit()
        {
            //if (timer != null)
            //    StopCoroutine(timer);
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<NowSelectMusicMsg>(NowSelectMusic);
            Message.RemoveListener<MusicSettingResetMsg>(MusicSettingReset);
            Message.RemoveListener<MusicVolumeSetMsg>(MusicVolumeSet);
            Message.RemoveListener<MusicPlayTimeMsg>(MusicPlayTime);
            Message.RemoveListener<MusicTimeSetMsg>(MusicTimeSet);
            Message.RemoveListener<MusicPlaySettingMsg>(MusicPlaySetting);
            Message.RemoveListener<MusicMainDialogCommandSetMsg>(MusicMainDialogCommandSet);
            Message.RemoveListener<MusicSettingMsg>(MusicSetting);
        }
    }
}
