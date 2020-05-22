using System.Collections;
using System.Collections.Generic;
using CellBig.UI.Event;
using UnityEngine;
using UnityEngine.UI;

namespace CellBig.UI
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
