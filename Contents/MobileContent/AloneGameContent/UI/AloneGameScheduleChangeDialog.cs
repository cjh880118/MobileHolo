using System.Collections;
using System.Collections.Generic;
using CellBig.UI.Event;
using UnityEngine;
using UnityEngine.UI;
using CellBig.Constants;
using System;

namespace CellBig.UI
{
    public class AloneGameScheduleChangeDialog : IDialog
    {
        public Button btnClose;
        public Button btnBackGround;
        public Button btnVocal;
        public Button btnDance;
        public Button btnIntelligence;
        public Button btnEnterTainment;

        protected override void OnLoad()
        {
            btnClose.onClick.AddListener(() => Message.Send<AloneGameScheduleChangeCloseMsg>(new AloneGameScheduleChangeCloseMsg()));
            btnBackGround.onClick.AddListener(() => Message.Send<AloneGameScheduleChangeCloseMsg>(new AloneGameScheduleChangeCloseMsg()));
            btnVocal.onClick.AddListener(() => Message.Send<AloneGameScheduleChangeItemClickMsg>(new AloneGameScheduleChangeItemClickMsg(Schedule.Vocal)));
            btnDance.onClick.AddListener(() => Message.Send<AloneGameScheduleChangeItemClickMsg>(new AloneGameScheduleChangeItemClickMsg(Schedule.Dance)));
            btnIntelligence.onClick.AddListener(() => Message.Send<AloneGameScheduleChangeItemClickMsg>(new AloneGameScheduleChangeItemClickMsg(Schedule.Intelligence)));
            btnEnterTainment.onClick.AddListener(() => Message.Send<AloneGameScheduleChangeItemClickMsg>(new AloneGameScheduleChangeItemClickMsg(Schedule.Entertainment)));
        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<AloneGameScheduleChangeDialogSetMsg>(AloneGameScheduleChangeDialogSet);
        }

        private void AloneGameScheduleChangeDialogSet(AloneGameScheduleChangeDialogSetMsg msg)
        {
            btnVocal.enabled = true;
            btnDance.enabled = true;
            btnIntelligence.enabled = true;
            btnEnterTainment.enabled = true;
            btnVocal.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            btnDance.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            btnIntelligence.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            btnEnterTainment.transform.GetChild(1).GetComponent<Image>().color = Color.white;

            if (msg.schedule == Schedule.Vocal)
            {
                btnVocal.enabled = false;
                btnVocal.transform.GetChild(1).GetComponent<Image>().color = Color.red;
            }
            else if (msg.schedule == Schedule.Dance)
            {
                btnDance.enabled = false;
                btnDance.transform.GetChild(1).GetComponent<Image>().color = Color.red;
            }
            else if (msg.schedule == Schedule.Intelligence)
            {
                btnIntelligence.enabled = false;
                btnIntelligence.transform.GetChild(1).GetComponent<Image>().color = Color.red;
            }
            else if (msg.schedule == Schedule.Entertainment)
            {
                btnEnterTainment.enabled = false;
                btnEnterTainment.transform.GetChild(1).GetComponent<Image>().color = Color.red;
            }
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<AloneGameScheduleChangeDialogSetMsg>(AloneGameScheduleChangeDialogSet);
        }
    }
}