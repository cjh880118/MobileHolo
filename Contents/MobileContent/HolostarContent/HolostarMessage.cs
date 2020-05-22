using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Constants;

namespace JHchoi.UI.Event
{
    public class SetHolostarCharacterMsg : Message
    {
        public Character character;
        public SetHolostarCharacterMsg(Character character)
        {
            this.character = character;
        }
    }

    public class HolostarImgClickMsg : Message
    {

    }

    public class HolostarStringMsg : Message
    {
        public string msg;
        public HolostarStringMsg(string msg)
        {
            this.msg = msg;
        }
    }
}