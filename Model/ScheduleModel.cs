using JHchoi.Contents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JHchoi.Models
{
    public class ScheduleModel : Model
    {
        GameModel _owner;

        public void Setup(GameModel owner)
        {
            _owner = owner;
        }

        Dictionary<int, AloneGameSchedule> dicSchedule;
        public Dictionary<int, AloneGameSchedule> DicSchedule { get => dicSchedule; set => dicSchedule = value; }
    }
}