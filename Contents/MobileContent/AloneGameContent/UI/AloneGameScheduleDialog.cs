using JHchoi.UI.Event;
using JHchoi.Contents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class AloneGameScheduleDialog : IDialog
    {
        public GameObject schedule_Controller;
        public Button btnBackGround;
        public Button bntOnClose;
        public Text txtManageCount;
        public Text txtManageRecoveryTime;

        List<Coroutine> ListCoroutine = new List<Coroutine>();

        protected override void OnLoad()
        {
            btnBackGround.onClick.AddListener(() => Message.Send<AloneGmaeScheduleMsg>(new AloneGmaeScheduleMsg(false)));
            bntOnClose.onClick.AddListener(() => Message.Send<AloneGmaeScheduleMsg>(new AloneGmaeScheduleMsg(false)));
        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<AloneGameScheduleInfoMsg>(AloneGameScheduleInfo);
            Message.AddListener<AloneGameScheduleManageMestSetMsg>(AloneGameScheduleManageMestSet);
        }

        bool isRecovery;
        private void AloneGameScheduleManageMestSet(AloneGameScheduleManageMestSetMsg msg)
        {
            isRecovery = msg.isTimer;
            txtManageCount.text = string.Format("x {0}", msg.manageMentCout);
            if (msg.manageMentCout == 20)
                txtManageRecoveryTime.text = "0:00";

            ListCoroutine.Add(StartCoroutine(ManageMentRecovery(isRecovery, msg.startTime, msg.manageMentRecoveryTime)));
        }

        private void AloneGameScheduleInfo(AloneGameScheduleInfoMsg msg)
        {
            int count = msg.dicSchedule.Count;
            for (int i = 0; i < count; i++)
            {
                schedule_Controller.transform.GetChild(i).GetComponent<Schedule_Item_Controller>().InitItemButton(i, msg.dicSchedule[i].schedule);
            }
        }

        //남은 시간은 전달해서 남은시간 카운트?
        IEnumerator ManageMentRecovery(bool isRecovery, string startTime, float managementRecoverTime)
        {
            while (isRecovery)
            {
                yield return null;
                System.DateTime nowTime = System.Convert.ToDateTime(System.DateTime.Now);
                System.DateTime firstManagementTime = System.Convert.ToDateTime(DateTime.Parse(startTime));
                System.TimeSpan completeTime = firstManagementTime.AddSeconds(managementRecoverTime) - nowTime;
                string tempSecond;
                if (completeTime.Seconds < 10)
                {
                    tempSecond = "0" + completeTime.Seconds;
                }
                else
                {
                    tempSecond = completeTime.Seconds.ToString();
                }

                txtManageRecoveryTime.text = completeTime.Minutes.ToString() + ":" + tempSecond;

                if (completeTime.Minutes <= 0 && completeTime.Seconds <= 0)
                {
                    Debug.Log("매니지 먼트 회복");
                    isRecovery = false;
                    StopAllCoroutines();
                    Message.Send<AloneGameManageMentRecoveryMsg>(new AloneGameManageMentRecoveryMsg());
                }
            }
        }
        
        protected override void OnExit()
        {
            RemoveMessage();

            foreach (var o in ListCoroutine)
            {
                if (o != null)
                    StopCoroutine(o);
            }

            ListCoroutine.Clear();
        }


        private void RemoveMessage()
        {
            Message.RemoveListener<AloneGameScheduleInfoMsg>(AloneGameScheduleInfo);
            Message.RemoveListener<AloneGameScheduleManageMestSetMsg>(AloneGameScheduleManageMestSet);
        }
    }
}