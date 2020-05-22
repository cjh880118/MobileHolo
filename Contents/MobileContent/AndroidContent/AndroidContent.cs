using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellBig.UI.Event;
using CellBig.Models;
using System;

namespace CellBig.Contents
{
    public class AndroidContent : IContent
    {
        CalanderEvent calanderEvent;
        public List<AlarmEvent> alarmList;
        int alarmCursor;
        int _num;
        CalendarModel calendarModel;

        protected override void OnLoadStart()
        {
            calendarModel = Model.First<CalendarModel>();
            SetLoadComplete();
        }

        protected override void OnEnter()
        {
            //AndroidTrasferMgr.Instance.ShowToast("로딩 완료");
        }

        protected override void OnExit()
        {
        }

        public void ReceiveLocation(string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                Debug.Log("unknown location");
                return;
            }

            string split = ",";
            string[] loc = msg.Split(split.ToCharArray());
            Debug.Log("location lat : " + loc[0] + ", lon : " + loc[1]);
            Message.Send<LocationMsg>(new LocationMsg(loc[0], loc[1]));
        }

        public void ReceiveCalendar(string json)
        {
            try
            {
                _num = 0;
                alarmCursor = 0;
                alarmList = new List<AlarmEvent>();
                calanderEvent = new CalanderEvent();
                CalendarList calendarList = JsonUtility.FromJson<CalendarList>(json);
                Debug.Log(json);
                for (int i = 0; i < calendarList.data.Count; i++)
                {
                    Debug.LogError(calendarList.data.Count);
                    long time = new System.DateTime(int.Parse(calendarList.data[i].year),
                        int.Parse(calendarList.data[i].month),
                        int.Parse(calendarList.data[i].day),
                        int.Parse(calendarList.data[i].hour),
                        int.Parse(calendarList.data[i].minute),
                        int.Parse(calendarList.data[i].second), DateTime.Now.Kind).Ticks;


                    AlarmEvent alarmEvent = new AlarmEvent();
                    alarmEvent.title = calendarList.data[i].title;
                    alarmEvent.tick = time;


                    alarmList.Add(alarmEvent);
                }

                IComparer<AlarmEvent> sort = new Sorting();
                alarmList.Sort(sort);

                calanderEvent.alarmEvents = alarmList.ToArray();
                string temp = JsonUtility.ToJson(calanderEvent);
                //Android.AndroidTrasferMgr.Instance.ShowToast("처음 : " + temp);

                calendarModel.AlarmEvents.Clear();
                for (int i = 0; i < alarmList.Count; i++)
                {
                    calendarModel.AlarmEvents.Add(alarmList[i]);
                }


                if (alarmList.Count > 0)
                {
                    //기존
                    //Android.AndroidTrasferMgr.Instance.BluetoothSendMsg(temp, Constants.SENDMSGTYPE.CALENDAR);

                    //테스트
                    Android.AndroidTrasferMgr.Instance.BluetoothSendMsg("start", Constants.SENDMSGTYPE.CALENDAR);

                    for (int i = 0; i < alarmList.Count; i++)
                    {
                        string evt = JsonUtility.ToJson(alarmList[i]);
                        Debug.Log("이벤트 : " + evt);
                        Android.AndroidTrasferMgr.Instance.BluetoothSendMsg(evt, Constants.SENDMSGTYPE.CALENDAR);
                    }

                    Android.AndroidTrasferMgr.Instance.BluetoothSendMsg("end", Constants.SENDMSGTYPE.CALENDAR);

                }

                Message.Send<CreateJosnFileMsg>(new CreateJosnFileMsg("calander", temp));
            }

            catch
            {
                Debug.LogError("Json Catch");
            }
        }



        public void MMSReceived(string msg)
        {
            Android.AndroidTrasferMgr.Instance.BluetoothSendMsg(msg, Constants.SENDMSGTYPE.MSG);
        }

        public void SMSReceived(string msg)
        {
            Android.AndroidTrasferMgr.Instance.BluetoothSendMsg(msg, Constants.SENDMSGTYPE.MSG);
        }
    }
}