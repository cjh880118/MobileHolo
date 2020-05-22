using CellBig.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CellBig.UI
{
    public class AloneGameMainDialog : IDialog
    {
        public Button btnOnSchedule;
        public Image imgNowSchedule;
        public Image imgNextSchedule;
        public Image imgScheduleGage;
        bool isTimerStart;

        List<Coroutine> ListCoroutine = new List<Coroutine>();
        //Coroutine ScheduleTimer;
        protected override void OnLoad()
        {
            btnOnSchedule.onClick.AddListener(() => Message.Send<AloneGmaeScheduleMsg>(new AloneGmaeScheduleMsg(true)));
        }

        protected override void OnEnter()
        {
            //스케줄 셋팅 메세지 호출
            AddMessage();
            Message.Send<AloneGameTopBarSettingRequestMsg>(new AloneGameTopBarSettingRequestMsg());
        }

        private void AddMessage()
        {
            Message.AddListener<AloneGameMainScheduleSettingMsg>(AloneGameMainScheduleSetting);
        }

        private void AloneGameMainScheduleSetting(AloneGameMainScheduleSettingMsg msg)
        {
            isTimerStart = true;
            imgNowSchedule.sprite = Resources.Load<Sprite>("UIImage/Schedule/" + msg.nowSchedule.ToString()) as Sprite;
            imgNextSchedule.sprite = Resources.Load<Sprite>("UIImage/Schedule/" + msg.nextSchedule.ToString()) as Sprite;
            ListCoroutine.Add(StartCoroutine(SetScheduleProgress(msg.scheduleTime, msg.completeTime)));
        }

        IEnumerator SetScheduleProgress(System.DateTime scheduleTime, float CompleteTime)
        {
            while (isTimerStart)
            {
                yield return null;
                System.DateTime nowTime = System.Convert.ToDateTime(System.DateTime.Now);
                System.DateTime firstScheduleTime = System.Convert.ToDateTime(scheduleTime);
                System.TimeSpan spanTime = nowTime - firstScheduleTime;
                float tempNum = System.Convert.ToSingle((spanTime.Minutes * 60) + spanTime.Seconds);
                imgScheduleGage.fillAmount = tempNum / CompleteTime;
                if (tempNum / CompleteTime > 1)
                {
                    isTimerStart = false;
                    StopAllCoroutines();
                    Message.Send<AloneGameScheduleCompleteMsg>(new AloneGameScheduleCompleteMsg());
                }
            }
        }

        protected override void OnExit()
        {
            RemoveMessage();
            isTimerStart = false;

            foreach (var o in ListCoroutine)
            {
                if (o != null)
                    StopCoroutine(o);
            }

            ListCoroutine.Clear();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<AloneGameMainScheduleSettingMsg>(AloneGameMainScheduleSetting);
        }
    }
}