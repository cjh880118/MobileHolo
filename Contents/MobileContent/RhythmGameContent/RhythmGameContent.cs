using CellBig.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellBig.Models;
using CellBig.Constants;

using CellBig.UI;
using Random = UnityEngine.Random;

namespace CellBig.Contents
{
    public class RhythmGameContent : IContent
    {
        static string TAG = "RhythmGameContent :: ";
        bool isBluetoothStart;
        BT_Sound bT_Sound;
        MobileOptionModel mobileOptionModel;
        NoteModel noteModel;
        PlayerInventoryModel playerInventoryModel;
        HolostarSettingModel holostarSettingModel;
        MobileOptionModel optionModel;
        GameObject stage;
        RhythmGame_Controller rhythmGameController;
        GameObject rhythmGameUI;

        List<BT_Sound.Param> ListRhythmGameMusic = new List<BT_Sound.Param>();
        List<Dictionary<string, object>> noteData;
        List<Coroutine> ListCorNote = new List<Coroutine>();
        List<IDialog> ListUi = new List<IDialog>();
        Coroutine corButtonInput;

        int nowCombo;
        int maxCombo;
        int perfect;
        int good;
        int normal;
        int bad;
        float score;

        protected override void OnLoadStart()
        {
            mobileOptionModel = Model.First<MobileOptionModel>();
            noteModel = Model.First<NoteModel>();
            playerInventoryModel = Model.First<PlayerInventoryModel>();
            holostarSettingModel = Model.First<HolostarSettingModel>();
            optionModel = Model.First<MobileOptionModel>();
            bT_Sound = TableManager.Instance.GetTableClass<BT_Sound>();
            foreach (var o in bT_Sound.sheets[0].list)
            {
                if (o.isGame)
                    ListRhythmGameMusic.Add(o);
            }
            StartCoroutine(LoadInitialData());
        }

        IEnumerator LoadInitialData()
        {
            string path = "Object/UI/RythmObject";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path,
               o =>
               {
                   rhythmGameUI = Instantiate(o) as GameObject;
                   rhythmGameController = rhythmGameUI.GetComponent<RhythmGame_Controller>();
                   rhythmGameUI.SetActive(false);
               }));

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
            stage = GameObject.Find("Stage(Clone)").transform.GetChild(0).gameObject;
            stage.SetActive(true);
            corButtonInput = StartCoroutine(ButtonInput());
            IContent.RequestContentExit<MenuContent>();
            AddMessage();
        }

        IEnumerator ButtonInput()
        {
            while (true)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    //0 메인 게임 1 음악 선택 2게임 결과
                    if (ListUi[0].isActive)
                    {
                        Time.timeScale = 0;
                        SoundManager.Instance.PauseAllSound();
                        UI.IDialog.RequestDialogEnter<RhytmGameEndInfoDialog>();
                        Debug.Log("게임 도중");

                    }
                    else if (ListUi[3].isActive)
                    {
                        RhytmeGameEndInfo(new RhytmeGameEndInfoMsg(false));
                    }
                    else
                    {
                        //RhythmGameMainDialog
                        Debug.Log(TAG + " : Escape");
                        IContent.RequestContentEnter<MenuContent>();
                    }
                }
            }
        }

        private void AddMessage()
        {
            Message.AddListener<RhythmGameBluetoothSelectMsg>(RhythmGameBluetoothSelect);
            Message.AddListener<RhythmGameNoteJudgeMsg>(RhythmGameNoteJudge);
            Message.AddListener<RhythmGameMusicSeletCloseMsg>(RhythmGameMusicSeletClose);
            Message.AddListener<RhythmGameMusicSelectMsg>(RhythmGameMusicSelect);
            Message.AddListener<RhythmGameResultCloseMsg>(RhythmGameResultClose);
            Message.AddListener<RhytmeGameEndInfoMsg>(RhytmeGameEndInfo);
        }

        private void RhythmGameBluetoothSelect(RhythmGameBluetoothSelectMsg msg)
        {
            isBluetoothStart = msg.isBluetoothSelect;
            if (msg.isBluetoothSelect)
            {
                UI.IDialog.RequestDialogEnter<UI.RhythmGameMainDialog>();
                Message.Send<RhythmGameSetMsg>(new RhythmGameSetMsg(playerInventoryModel.NowCharacter));
            }
            else
            {
                UI.IDialog.RequestDialogEnter<UI.RhythmGameMusicSelectDialog>();
                Message.Send<RhythmGameMusicListMsg>(new RhythmGameMusicListMsg(ListRhythmGameMusic));
            }
        }

        private void RhythmGameNoteJudge(RhythmGameNoteJudgeMsg msg)
        {
            if (msg.rhythmNote == RhythmNote.Bad || msg.rhythmNote == RhythmNote.Miss) 
            {
                nowCombo = 0;
                bad++;
            }
            else if (msg.rhythmNote == RhythmNote.Normal)
            {
                nowCombo++;
                normal++;
            }
            else if (msg.rhythmNote == RhythmNote.Good)
            {
                nowCombo++;
                good++;
            }
            else if (msg.rhythmNote == RhythmNote.Perfect)
            {
                nowCombo++;
                perfect++;
            }

            if (nowCombo > maxCombo)
                maxCombo = nowCombo;

            Message.Send<RhythmGameComboMsg>(new RhythmGameComboMsg(nowCombo));
        }

        private void RhythmGameMusicSeletClose(RhythmGameMusicSeletCloseMsg msg)
        {
            Debug.Log(TAG + " : Escape");
            SoundManager.Instance.PlaySound((int)SoundType.EFFECT_BTN_DOWN, optionModel.MobileOption.effectVolume);
            IContent.RequestContentEnter<MenuContent>();
        }

        private void RhythmGameMusicSelect(RhythmGameMusicSelectMsg msg)
        {
            rhythmGameUI.SetActive(true);
            rhythmGameController.InitRhythmGameNote();
            nowCombo = 0;
            maxCombo = 0;
            perfect = 0;
            good = 0;
            normal = 0;
            bad = 0;
            score = 0;
            SoundManager.Instance.PlaySound((int)SoundType.EFFECT_BTN_DOWN, mobileOptionModel.MobileOption.effectVolume);
            UI.IDialog.RequestDialogExit<UI.RhythmGameMusicSelectDialog>();
            UI.IDialog.RequestDialogEnter<UI.RhythmGameMainDialog>();
            Message.Send<RhythmGameSetMsg>(new RhythmGameSetMsg(playerInventoryModel.NowCharacter));
            SoundManager.Instance.PlaySound(msg.musicIndex, mobileOptionModel.MobileOption.gameVolume);

            Debug.Log("음악 번호 : " + msg.musicIndex);

            int aniNum = Random.Range((int)AnimationType.Motion1, (int)AnimationType.Motion2 + 1);
            Message.Send<SetCharacterAnimationMsg>(new SetCharacterAnimationMsg((AnimationType)aniNum, false));

            foreach (var o in ListRhythmGameMusic)
            {
                if (o.Index == msg.musicIndex)
                {
                    noteData = noteModel.GetNoteData(o.Title);
                    break;
                }
            }
            ListCorNote.Add(StartCoroutine(PlayNote()));
        }

        public IEnumerator PlayNote()
        {
            int currentNoteCount = 0;
            float noteTerm = noteModel.NoteTerm;

            while (currentNoteCount < noteData.Count)
            {
                float term = 0;
                float currentTime = float.Parse(noteData[currentNoteCount]["Time"].ToString());

                // 데이터 테이블의 첫번쨰 인자(시간) 동안 대기.
                if (currentNoteCount > 0)
                    term = (currentTime - noteTerm) - (float.Parse(noteData[currentNoteCount - 1]["Time"].ToString()) - noteTerm);
                else
                    term = (currentTime - noteTerm);

                //Debug.Log(noteData[currentNoteCount]["Time"].ToString() + "note0 :" + noteData[currentNoteCount]["0"].ToString());

                yield return new WaitForSeconds(term);

                if (noteData[currentNoteCount]["0"].ToString() == "1")
                {
                    //왼쪽노트
                    Message.Send<RhythmGameNoteCreateMsg>(new RhythmGameNoteCreateMsg(false, currentNoteCount));
                }

                if (noteData[currentNoteCount]["1"].ToString() == "1")
                {
                    //오른쪽 노트
                    Message.Send<RhythmGameNoteCreateMsg>(new RhythmGameNoteCreateMsg(true, currentNoteCount));
                }

                currentNoteCount++;
            }

            //게임 종료 시점 음악 종료 노트종료?
            StartCoroutine(RhythmGameEnd());
            //Debug.Log("게임 종료");
        }

        IEnumerator RhythmGameEnd()
        {
            yield return new WaitForSeconds(3.0f);
            int aniNum = Random.Range((int)AnimationType.Idel1, (int)AnimationType.Idel3 + 1);
            Message.Send<SetCharacterAnimationMsg>(new SetCharacterAnimationMsg((AnimationType)aniNum, false));
            UI.IDialog.RequestDialogExit<UI.RhythmGameMainDialog>();
            UI.IDialog.RequestDialogEnter<UI.RhythmGameResultDialog>();
            SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Open, mobileOptionModel.MobileOption.effectVolume);

            score = ((((normal + good + perfect) / noteData.Count) * 0.8f) +
                (((good + perfect) / noteData.Count) * 0.07f) +
                (((perfect) / noteData.Count) * 0.13f)) * 100;
            Message.Send<RhythmGameResultMsg>(new RhythmGameResultMsg(bad, normal, good, perfect, maxCombo, score));
            StartCoroutine(RhythmGameResultClose());
        }

        IEnumerator RhythmGameResultClose()
        {
            yield return new WaitForSeconds(3.0f);
            foreach (var o in ListCorNote)
            {
                if (o != null)
                    StopCoroutine(o);
            }

            ListCorNote.Clear();
            rhythmGameController.GameEnd();
            UI.IDialog.RequestDialogExit<UI.RhythmGameResultDialog>();
            UI.IDialog.RequestDialogEnter<UI.RhythmGameMusicSelectDialog>();
            SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Close, mobileOptionModel.MobileOption.effectVolume);
        }

        private void RhythmGameResultClose(RhythmGameResultCloseMsg msg)
        {
            UI.IDialog.RequestDialogExit<UI.RhythmGameResultDialog>();
            UI.IDialog.RequestDialogEnter<UI.RhythmGameMusicSelectDialog>();
        }

        private void RhytmeGameEndInfo(RhytmeGameEndInfoMsg msg)
        {
            Time.timeScale = 1;
            UI.IDialog.RequestDialogExit<RhytmGameEndInfoDialog>();

            if (msg.isEnd)
            {
                SoundManager.Instance.StopAllSound();
                SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Close, mobileOptionModel.MobileOption.effectVolume);
                Message.Send<SetCharacterAnimationMsg>(new SetCharacterAnimationMsg(AnimationType.Idel1, false));

                foreach (var o in ListCorNote)
                {
                    if (o != null)
                        StopCoroutine(o);
                }

                ListCorNote.Clear();
                rhythmGameController.GameEnd();
                rhythmGameUI.SetActive(false);
                if (isBluetoothStart)
                {
                    RhythmGameMusicSeletClose(new RhythmGameMusicSeletCloseMsg());
                }
                else
                {
                    UI.IDialog.RequestDialogEnter<UI.RhythmGameMusicSelectDialog>();
                    UI.IDialog.RequestDialogExit<UI.RhythmGameMainDialog>();
                }
            }
            else
            {
                SoundManager.Instance.UnPauseAllSound();
            }
        }

        protected override void OnExit()
        {
            if (corButtonInput != null)
            {
                StopCoroutine(corButtonInput);
                corButtonInput = null;
            }

            if (stage != null)
                stage.SetActive(false);

            DialogAllClose();
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<RhythmGameBluetoothSelectMsg>(RhythmGameBluetoothSelect);
            Message.RemoveListener<RhythmGameNoteJudgeMsg>(RhythmGameNoteJudge);
            Message.RemoveListener<RhythmGameMusicSeletCloseMsg>(RhythmGameMusicSeletClose);
            Message.RemoveListener<RhythmGameMusicSelectMsg>(RhythmGameMusicSelect);
            Message.RemoveListener<RhythmGameResultCloseMsg>(RhythmGameResultClose);
            Message.RemoveListener<RhytmeGameEndInfoMsg>(RhytmeGameEndInfo);
        }
    }
}
