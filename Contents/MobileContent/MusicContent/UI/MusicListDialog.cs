using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class MusicListDialog : IDialog
    {
        public Button btnClose;
        public GameObject musicItem;
        public GameObject parent;

        protected override void OnLoad()
        {
            btnClose.onClick.AddListener(() => Message.Send<MusicListCloseMsg>(new MusicListCloseMsg()));
        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<MusicListSendMsg>(MusicListSend);
        }

        private void MusicListSend(MusicListSendMsg msg)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                Destroy(parent.transform.GetChild(i).gameObject);
            }

            int tempCount = msg.playlist.data.Count;
            float height = musicItem.GetComponent<RectTransform>().sizeDelta.y;

            float blank = 10.0f;
            float parentHeight = (height + blank) * (tempCount) + blank;
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x, parentHeight);

            for (int i = 0; i < tempCount; i++)
            {
                GameObject tempItem = GameObject.Instantiate(musicItem) as GameObject;
                tempItem.transform.parent = parent.transform;
                tempItem.transform.localScale = new Vector3(1, 1, 1);
                tempItem.transform.localPosition = new Vector3(0, -(blank + height) * i, 0);
                tempItem.GetComponent<Music_Item_Controller>().InitMusicItem(msg.playlist.data[i].index, msg.playlist.data[i].title);
            }
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<MusicListSendMsg>(MusicListSend);
        }
    }
}