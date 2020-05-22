using CellBig.Contents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellBig.Models
{
    public class HolostarSettingModel : Model
    {
        GameModel _owner;

        public void Setup(GameModel owner)
        {
            _owner = owner;
        }

        HoloStarSetting setting;

        public MusicSetting MusicSetting { get => setting.musicSetting; set => setting.musicSetting = value; }
        public HoloOptionSetting HoloOptionSetting { get => setting.holoOptionSetting; set => setting.holoOptionSetting = value; }
        public HoloStarSetting HoloStarSetting { get => setting; set => setting = value; }
    }
}