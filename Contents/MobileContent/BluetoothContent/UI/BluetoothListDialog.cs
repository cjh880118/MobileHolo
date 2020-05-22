using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class BluetoothListDialog : IDialog
    {
        public Button btnClose;
        public Button btnBackground;
        public GameObject bluetoothItem;
        public GameObject parent;

        protected override void OnLoad()
        {
            btnClose.onClick.AddListener(() => Message.Send<BluetoothListCloseMsg>(new BluetoothListCloseMsg()));
            btnBackground.onClick.AddListener(() => Message.Send<BluetoothListCloseMsg>(new BluetoothListCloseMsg()));
        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<BluetoothListMsg>(BluetoothList);
        }

        private void BluetoothList(BluetoothListMsg msg)
        {
            for(int i = 0 ; i < parent.transform.childCount ; i ++)
            {
                Destroy(parent.transform.GetChild(i).gameObject);
            }

            int tempCount = msg.listBluetoothDevice.Count;
            float height = bluetoothItem.GetComponent<RectTransform>().sizeDelta.y;

           
            float blank = 10.0f;
            float parentHeight = (height + blank) * (tempCount) + blank;
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x, parentHeight);

            
            for ( int  i = 0; i < tempCount; i++)
            {
                GameObject tempItem = GameObject.Instantiate(bluetoothItem) as GameObject;
                tempItem.transform.parent = parent.transform;
                tempItem.transform.localScale = new Vector3(1, 1, 1);
                tempItem.transform.localPosition = new Vector3(0, -(blank + height) * i, 0);
                tempItem.GetComponent<Bluetooth_Item_Controller>().InitItemButton(msg.listBluetoothDevice[i]);

            }
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<BluetoothListMsg>(BluetoothList);
        }
    }
}