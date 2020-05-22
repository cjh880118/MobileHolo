using System;
using System.Collections;
using System.Collections.Generic;
using JHchoi.UI.Event;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class AloneGameScheduleChangeOKDialog : IDialog
    {
        public Image imgNowSchedule;
        public Image imgChangeSchedule;
        public Button bntOK;
        public Button btnCancel;

        protected override void OnLoad()
        {
            bntOK.onClick.AddListener(() => Message.Send<AloneGameScheduleChangeAgreeMsg>(new AloneGameScheduleChangeAgreeMsg(true)));
            btnCancel.onClick.AddListener(() => Message.Send<AloneGameScheduleChangeAgreeMsg>(new AloneGameScheduleChangeAgreeMsg(false)));
        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<AloneGameScheduleChangeAgreeDialogMsg>(AloneGameScheduleChangeAgreeDialog);
        }

        private void AloneGameScheduleChangeAgreeDialog(AloneGameScheduleChangeAgreeDialogMsg msg)
        {
            imgNowSchedule.sprite = Resources.Load<Sprite>("UIImage/Schedule/" + msg.nowSchedule.ToString()) as Sprite;
            imgChangeSchedule.sprite = Resources.Load<Sprite>("UIImage/Schedule/" + msg.changeSchedule.ToString()) as Sprite;
            imgNowSchedule.SetNativeSize();
            imgChangeSchedule.SetNativeSize();
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<AloneGameScheduleChangeAgreeDialogMsg>(AloneGameScheduleChangeAgreeDialog);
        }
    }
}