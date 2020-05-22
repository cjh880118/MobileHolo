using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class Cash_Item_Controller : MonoBehaviour
    {
        public Button btnIcon;
        public Text txtName;
        public Text txtPrice;
        int index;

        public void InitItemButton(int index, string name, string price)
        {
            this.index = index;
            txtName.text = name;
            txtPrice.text = price + "원";
        }
    }
}