using System.Collections;
using System.Collections.Generic;
using JHchoi.UI.Event;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class Bluetooth_Item_Controller : MonoBehaviour
    {
        public Button btnIcon;
        public Text txtName;

        public void InitItemButton(string name)
        {
            btnIcon.onClick.AddListener(() => Message.Send<BluetoothItemSelectMsg>(new BluetoothItemSelectMsg(name)));
            txtName.text = name;
        }
    }
}
