using System;
using System.Collections;
using System.Collections.Generic;
using CellBig.Constants;
using CellBig.UI.Event;
using UnityEngine;
using UnityEngine.UI;

namespace CellBig.UI
{
    public class Buff_Item_Controller : MonoBehaviour
    {
        public Button btnIcon;
        public Text txtName;
        public Text txtPrice;
        public Text txtTime;
        public Text txtContent;
        public Image imgIcon;
        int index;

        public void InitItemButton(int index, string name, string price, string time, string content, string imgPath)
        {
            this.index = index;
            txtName.text = name;
            txtPrice.text = string.Format("{0} 코인", price);
            txtTime.text = time;
            txtContent.text = content;
            imgIcon.sprite = Resources.Load<Sprite>(imgPath) as Sprite;
            imgIcon.SetNativeSize();
            btnIcon.onClick.AddListener(() => Message.Send<AlonGameItemBuyMsg>(new AlonGameItemBuyMsg(Upgrade.Buff, index, Int32.Parse(price))));
        }
    }
}