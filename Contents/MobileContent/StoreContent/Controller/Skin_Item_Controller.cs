using JHchoi.UI.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class Skin_Item_Controller : MonoBehaviour
    {
        public Button btnItem;
        public Image imgIcon;
        int cost;

        public void InitButton(int index, string path, int cost, bool isHave)
        {
            btnItem.onClick.AddListener(() => Message.Send<StoreSkinItemSelectMsg>(new StoreSkinItemSelectMsg(index, cost)));
            imgIcon.sprite = Resources.Load<Sprite>(path);
            if (isHave)
                imgIcon.color = Color.white;
            else
                imgIcon.color = new Color(255, 255, 255, 0.3f);

            this.cost = cost;
        }
    }
}