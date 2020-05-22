using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellBig.Constants;

namespace CellBig.UI.Event
{
    //public class RunSelectMenuMsg : Message
    //{
    //    public Menu menu;
    //    public RunSelectMenuMsg(Menu menu)
    //    {
    //        this.menu = menu;
    //    }
    //}

    public class RunMenuMsg : Message
    {
        public Menu menu;
        public RunMenuMsg(Menu menu)
        {
            this.menu = menu;
        }
    }

    public class RunGameMsg : Message
    {
        public GameType menu;
        public RunGameMsg(GameType menu)
        {
            this.menu = menu;
        }
    }

    public class GameSelectCloseMsg : Message
    {

    }
}