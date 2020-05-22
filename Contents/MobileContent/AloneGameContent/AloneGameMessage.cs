using CellBig.Constants;
using CellBig.Contents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellBig.UI.Event
{
    public class AloneGameRunMenuMsg : Message
    {
        public AloneGameMenu aloneGameMenu;
        public AloneGameRunMenuMsg(AloneGameMenu aloneGameMenu)
        {
            this.aloneGameMenu = aloneGameMenu;
        }
    }

    public class AloneGameTopBarSettingRequestMsg : Message
    {

    }

    public class AloneGameMainScheduleSettingMsg : Message
    {
        public Schedule nowSchedule;
        public Schedule nextSchedule;
        public DateTime scheduleTime;
        public float completeTime;
        public AloneGameMainScheduleSettingMsg(Schedule nowSchedule, Schedule nextSchedule, DateTime scheduleTime, float completeTime)
        {
            this.nowSchedule = nowSchedule;
            this.nextSchedule = nextSchedule;
            this.scheduleTime = scheduleTime;
            this.completeTime = completeTime;
        }
    }

    public class AloneGmaeScheduleMsg : Message
    {
        public bool isOpen;
        public AloneGmaeScheduleMsg(bool isOpen)
        {
            this.isOpen = isOpen;
        }
    }

    public class AloneGameScheduleCompleteMsg : Message
    {

    }

    public class AloneGameScheduleChangeCloseMsg : Message
    {

    }

    public class AloneGameScheduleChangeDialogSetMsg : Message
    {
        public Schedule schedule;
        public AloneGameScheduleChangeDialogSetMsg(Schedule schedule)
        {
            this.schedule = schedule;
        }
    }

    public class AloneGameScheduleChangeItemClickMsg : Message
    {
        public Schedule schedule;
        public AloneGameScheduleChangeItemClickMsg(Schedule schedule)
        {
            this.schedule = schedule;
        }
    }

    public class AloneGameScheduleChangeAgreeDialogMsg : Message
    {
        public Schedule nowSchedule;
        public Schedule changeSchedule;
        public AloneGameScheduleChangeAgreeDialogMsg(Schedule nowSchedule, Schedule changeSchedule)
        {
            this.nowSchedule = nowSchedule;
            this.changeSchedule = changeSchedule;
        }
    }

    public class AloneGameScheduleChangeAgreeMsg : Message
    {
        public bool isOK;
        public AloneGameScheduleChangeAgreeMsg(bool isOK)
        {
            this.isOK = isOK;
        }
    }

    public class AloneGameScheduleInfoMsg : Message
    {
        public Dictionary<int, AloneGameSchedule> dicSchedule;

        public AloneGameScheduleInfoMsg(Dictionary<int, AloneGameSchedule> dicSchedule)
        {
            this.dicSchedule = dicSchedule;
        }
    }

    public class AloneGameManageMentRecoveryMsg : Message
    {
      
    }

    public class AloneGameScheduleManageMestSetMsg : Message
    {
        public bool isTimer;
        public int manageMentCout;
        public float manageMentRecoveryTime;
        public string startTime;

        public AloneGameScheduleManageMestSetMsg(bool isTimer, int manageMentCout, float recoveryTime, string startTime)
        {
            this.isTimer = isTimer;
            this.manageMentCout = manageMentCout;
            this.manageMentRecoveryTime = recoveryTime;
            this.startTime = startTime;
        }
    }

    public class AvatarStatusMsg : Message
    {
        public Character character;
        public PlayerStatus playerStatus;

        public AvatarStatusMsg(Character character, PlayerStatus playerStatus)
        {
            this.character = character;
            this.playerStatus = playerStatus;
        }
    }

    public class AloneGameSkillorBuffSelectMsg : Message
    {
        public Upgrade upgrade;
        public AloneGameSkillorBuffSelectMsg(Upgrade upgrade)
        {
            this.upgrade = upgrade;
        }
    }

    public class SetBuffScrolleMsg : Message
    {
        public Buff_Table buff_Table;
        public SetBuffScrolleMsg(Buff_Table buff_Table)
        {
            this.buff_Table = buff_Table;
        }
    }

    public class SetSkillScrollMsg : Message
    {
        public Skill_Table skill_Table;
        public List<int> listSkillLv;
        public int potential;
        public SetSkillScrollMsg(Skill_Table skill_Table, List<int> listSkillLv, int potential)
        {
            this.skill_Table = skill_Table;
            this.listSkillLv = listSkillLv;
            this.potential = potential;
        }
    }

    public class AlonGameItemBuyMsg : Message
    {
        public Upgrade upgrade;
        public int index;
        public int price;
        public AlonGameItemBuyMsg(Upgrade upgrade, int index, int price)
        {
            this.upgrade = upgrade;
            this.index = index;
            this.price = price;
        }
    }

    public class ItmeIsBuyDialogMsg : Message
    {
        public bool isBuy;
        public Upgrade upgrade;
        public int index;
        public int price;
        public ItmeIsBuyDialogMsg(Upgrade upgrade, bool isBuy, int index, int price)
        {
            this.upgrade = upgrade;
            this.isBuy = isBuy;
            this.index = index;
            this.price = price;
        }
    }

    public class BuyDialogSetMsg : Message
    {
        public string imgPath;
        public int cost;
        public int index;
        public bool isBuyPossible;

        public BuyDialogSetMsg(string imgPath, int cost, int index, bool isPossible)
        {
            this.imgPath = imgPath;
            this.cost = cost;
            this.index = index;
            this.isBuyPossible = isPossible;
        }
    }

    public class ScheduleItemSelectMsg : Message
    {
        public int itemIndex;
        public ScheduleItemSelectMsg(int itemIndex)
        {
            this.itemIndex = itemIndex;
        }
    }
}
