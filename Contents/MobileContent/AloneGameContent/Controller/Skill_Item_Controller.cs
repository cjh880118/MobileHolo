using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CellBig.UI.Event;
using CellBig.Constants;
using System;

namespace CellBig.UI
{
    public class Skill_Item_Controller : MonoBehaviour
    {
        public Button btnIcon;
        public Text txtName;
        public Text txtPrice;
        public Text txtContent;
        public Text txtLV;
        public Image imgIcon;
        //int index;

        public void InitItemButton(int index, string name, string price, string content, string imgPath, int lv)
        {
            txtName.text = name;
            int tempPrice = Int32.Parse(price) * (lv + 1);
            txtPrice.text = string.Format("잠재력 : {0}", tempPrice);
            txtLV.text = string.Format("LV : {0}", lv);
            txtContent.text = content;
            imgIcon.sprite = Resources.Load<Sprite>(imgPath) as Sprite;
            imgIcon.SetNativeSize();
            btnIcon.onClick.AddListener(() => Message.Send<AlonGameItemBuyMsg>(new AlonGameItemBuyMsg(Upgrade.Skill, index, tempPrice)));
        }

        public void InitSkillLvAndPrice(int lv , int price)
        {
            txtLV.text = string.Format("LV : {0}", lv);
            txtPrice.text = string.Format("잠재력 : {0}", price);
        }
    }
}