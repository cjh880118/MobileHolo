using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JHchoi.UI.Event;
using JHchoi.Constants;

namespace JHchoi.UI
{
    public class Schedule_Item_Controller : MonoBehaviour
    {
        int itemIndex;
        public Image imgIcon;
        public Button btnItem;
        // Start is called before the first frame update
        void Start()
        {
            btnItem.onClick.AddListener(() => Message.Send<ScheduleItemSelectMsg>(new ScheduleItemSelectMsg(itemIndex)));
        }

        public void InitItemButton(int itemIndex, Schedule schedule)
        {
            this.itemIndex = itemIndex;
            imgIcon.sprite = Resources.Load<Sprite>("UIImage/Schedule/" + schedule.ToString()) as Sprite;
            imgIcon.SetNativeSize();
        }
    }
}
