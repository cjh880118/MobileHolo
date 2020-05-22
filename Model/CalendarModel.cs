using CellBig.Contents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellBig.Models
{
    public class CalendarModel : Model
    {
        GameModel _owner;

        public void Setup(GameModel owner)
        {
            _owner = owner;
            alarmEvents = new List<AlarmEvent>();
        }

        List<AlarmEvent> alarmEvents;
        public List<AlarmEvent> AlarmEvents { get => alarmEvents; set => alarmEvents = value; }
    }
}