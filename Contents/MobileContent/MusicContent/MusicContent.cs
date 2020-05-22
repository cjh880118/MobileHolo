using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Models;
using JHchoi.Android;
using JHchoi.Constants;

namespace JHchoi.Contents
{
    public class MusicContent : IContent
    {
        static string TAG = "MusicContent :: ";
        SettingModel settingModel;
        HolostarSettingModel holostarSettingModel;
        MobileOptionModel optionModel;
        Playlist playlist = null;
        GameObject stage;
        Coroutine btnEvent;

        protected override void OnLoadStart()
        {
            settingModel = Model.First<SettingModel>();
            holostarSettingModel = Model.First<HolostarSettingModel>();
            optionModel = Model.First<MobileOptionModel>();
            StartCoroutine(LoadInitialData());
        }

        IEnumerator LoadInitialData()
        {
            string path = "Object/Stage/Stage";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path,
               o =>
               {
                   var inGameObject = Instantiate(o) as GameObject;
                   stage = inGameObject.transform.GetChild(0).gameObject;
                   stage.SetActive(false);
               }));
            SetLoadComplete();
        }

        protected override void OnEnter()
        {
            Debug.Log(TAG + "OnEnter");
            btnEvent = StartCoroutine(ButtonInput());
            AddMessage();
            IContent.RequestContentExit<MenuContent>();
            UI.IDialog.RequestDialogEnter<UI.MusicDialog>();
            stage.SetActive(true);
            Message.Send<MusicPlaySettingMsg>(new MusicPlaySettingMsg(holostarSettingModel.MusicSetting));

            if (!holostarSettingModel.MusicSetting.isMusicPlay || !settingModel.IsBluetoothConnet)
                Message.Send<MusicSettingResetMsg>(new MusicSettingResetMsg());


           

            //if (holostarSettingModel.MusicSetting.isMusicPlay)
            //{
            //    int musicIndex = 0;
            //    for (int i = 0; i < playlist.data.Count; i++)
            //    {
            //        if (playlist.data[i].index == holostarSettingModel.MusicSetting.musicIndex)
            //        {
            //            musicIndex = i;
            //        }
            //    }

            //    Message.Send<MusicSettingMsg>(new MusicSettingMsg(playlist.data[musicIndex].title, playlist.data[musicIndex].artist, playlist.data[musicIndex].duration));
            //    Android.AndroidTrasferMgr.Instance.ShowToast("현재 음악은 재생중");
            //}
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
            Message.AddListener<MusicTimeSearchMsg>(MusicTimeSearch);
            Message.AddListener<BluetoothMusicCommandMsg>(BluetoothMusicCommand);
            Message.AddListener<MusicCommandMsg>(MusicCommand);
            Message.AddListener<MusicListCloseMsg>(MusicListClose);
            Message.AddListener<MusicItemSelectMsg>(MusicItemSelect);
            Message.AddListener<MusicListReceiveMsg>(MusicListReceive);
            Message.AddListener<MusicVolumeRequestMsg>(MusicVolumeRequest);
            Message.AddListener<MusicVolumeSetMsg>(MusicVolumeSet);
        }

        private void MusicTimeSearch(MusicTimeSearchMsg msg)
        {
            double playTime;
            double nowTime;
            int index = 0;

            if (playlist != null)
            {
                for (int i = 0; i < playlist.data.Count; i++)
                {
                    if (msg.musicIndex == playlist.data[i].index)
                    {
                        index = i;
                        break;
                    }
                }

                playTime = playlist.data[index].duration;


                nowTime = msg.nowTime;
                Message.Send<NowSelectMusicMsg>(new NowSelectMusicMsg(playlist.data[index].title, playlist.data[index].artist, playlist.data[index].duration));
            }
            else
            {
                playTime = 0;
                nowTime = 0;
            }


            Message.Send<MusicTimeSetMsg>(new MusicTimeSetMsg(playTime, nowTime));
        }

        //명령 수신
        private void BluetoothMusicCommand(BluetoothMusicCommandMsg msg)
        {
            MUSICINFO tempMusic = msg.musicInfo;

            if (tempMusic == MUSICINFO.MUSIC_PLAY)
            {
                int musicNum = int.Parse(msg.dataMsg);

                int musicIndex = 0;
                for (int i = 0; i < playlist.data.Count; i++)
                {
                    if (playlist.data[i].index == musicNum)
                    {
                        musicIndex = i;
                    }
                }

                Message.Send<MusicSettingMsg>(new MusicSettingMsg(playlist.data[musicIndex].title, playlist.data[musicIndex].artist, playlist.data[musicIndex].duration));
            }
            else if (tempMusic == MUSICINFO.MUSIC_SELECT)
            {
                int musicNum = int.Parse(msg.dataMsg);
                int musicIndex = 0;

                for (int i = 0; i < playlist.data.Count; i++)
                {
                    if (playlist.data[i].index == musicNum)
                    {
                        musicIndex = i;
                    }
                }

                Message.Send<MusicSettingMsg>(new MusicSettingMsg(playlist.data[musicIndex].title, playlist.data[musicIndex].artist, playlist.data[musicIndex].duration));
            }
            else if (tempMusic == MUSICINFO.MUSIC_LIST)
            {
                //이전

                //Playlist playlist = null;
                //playlist = JsonUtility.FromJson<Playlist>(msg.dataMsg);
                //Message.Send<MusicListReceiveMsg>(new MusicListReceiveMsg(playlist));



                ///테스트 
                if (msg.dataMsg == "start")
                {
                    try
                    {
                        playlist = new Playlist();
                        playlist.data = new List<Music>();
                    }
                    catch (Exception e)
                    {
                        Android.AndroidTrasferMgr.Instance.ShowToast(e.ToString());
                    }

                }
                else if (msg.dataMsg == "end")
                {
                    Message.Send<MusicListReceiveMsg>(new MusicListReceiveMsg(playlist));
                }
                else
                {
                    try
                    {
                        Music music = JsonUtility.FromJson<Music>(msg.dataMsg);
                        playlist.data.Add(music);
                    }
                    catch (Exception e)
                    {
                        Android.AndroidTrasferMgr.Instance.ShowToast("ADD :" + e.ToString());
                    }
                    //playlist.data   
                    //Music JsonUtility
                }

            }
            else if (tempMusic == MUSICINFO.MUSIC_TIME)
            {
                Message.Send<MusicPlayTimeMsg>(new MusicPlayTimeMsg(float.Parse(msg.dataMsg)));
            }

            else if (tempMusic == MUSICINFO.MUSIC_PAUSE ||
                tempMusic == MUSICINFO.MUSIC_MODE_REPEIT ||
                tempMusic == MUSICINFO.MUSIC_MODE_SHUFFLE)
            {
                Message.Send<MusicMainDialogCommandSetMsg>(new MusicMainDialogCommandSetMsg(tempMusic));
            }
        }

        //명령 전달
        private void MusicCommand(MusicCommandMsg msg)
        {
            if (settingModel.IsBluetoothConnet)
            {
                if (playlist == null && msg.musicInfo == MUSICINFO.MUSIC_PLAY)
                {
                    Android.AndroidTrasferMgr.Instance.ShowToast("음악 목록에서 곡을 선택해주세요.");
                    return;
                }

                SoundManager.Instance.PlaySound((int)SoundType.EFFECT_BTN_DOWN, optionModel.MobileOption.effectVolume);
                // AndroidTrasferMgr.Instance.BluetoothSendMsg(msg.ToString(), SENDMSGTYPE.MUSIC, MUSICINFO.MUSIC_SELECT);
                AndroidTrasferMgr.Instance.BluetoothSendMsg(null, SENDMSGTYPE.MUSIC, msg.musicInfo);
            }
        }

        //리스트 닫기
        private void MusicListClose(MusicListCloseMsg msg)
        {
            SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Close, optionModel.MobileOption.effectVolume);
            UI.IDialog.RequestDialogExit<UI.MusicListDialog>();
        }

        //음악 선택
        private void MusicItemSelect(MusicItemSelectMsg msg)
        {
            MusicListClose(new MusicListCloseMsg());
            AndroidTrasferMgr.Instance.BluetoothSendMsg(msg.index.ToString(), SENDMSGTYPE.MUSIC, MUSICINFO.MUSIC_SELECT);
        }

        private void MusicListReceive(MusicListReceiveMsg msg)
        {
            if (!settingModel.IsBluetoothConnet)
            {
                AndroidTrasferMgr.Instance.ShowToast("블루투스 연결이 안되어 있습니다.");
                return;
            }

            if (msg.playlist.data.Count < 1)
            {
                AndroidTrasferMgr.Instance.ShowToast("음악 목록이 없습니다.");
                return;
            }

            playlist = msg.playlist;
            UI.IDialog.RequestDialogEnter<UI.MusicListDialog>();
            Message.Send<MusicListSendMsg>(new MusicListSendMsg(msg.playlist));
        }

        private void MusicVolumeRequest(MusicVolumeRequestMsg msg)
        {
            Message.Send<MusicVolumeSetMsg>(new MusicVolumeSetMsg(holostarSettingModel.MusicSetting.musicVolume));
        }

        private void MusicVolumeSet(MusicVolumeSetMsg msg)
        {
            holostarSettingModel.MusicSetting.musicVolume = msg.volume;
            AndroidTrasferMgr.Instance.BluetoothSendMsg(msg.volume.ToString(), SENDMSGTYPE.MUSIC, MUSICINFO.MUSIC_VOLUME);
        }

        protected override void OnExit()
        {
            if (btnEvent != null)
                StopCoroutine(btnEvent);

            stage.SetActive(false);
            DialogAllClose();
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<MusicTimeSearchMsg>(MusicTimeSearch);
            Message.RemoveListener<BluetoothMusicCommandMsg>(BluetoothMusicCommand);
            Message.RemoveListener<MusicCommandMsg>(MusicCommand);
            Message.RemoveListener<MusicListCloseMsg>(MusicListClose);
            Message.RemoveListener<MusicItemSelectMsg>(MusicItemSelect);
            Message.RemoveListener<MusicListReceiveMsg>(MusicListReceive);
            Message.RemoveListener<MusicVolumeRequestMsg>(MusicVolumeRequest);
            Message.RemoveListener<MusicVolumeSetMsg>(MusicVolumeSet);
        }

        private void OnDestroy()
        {
            Destroy(stage);
        }
    }
}