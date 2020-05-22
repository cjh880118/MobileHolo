using CellBig.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CellBig.UI
{
    public class RhythmGameMusicSelectDialog : IDialog
    {
        public Button btnBack;
        public GameObject musicItem;
        public GameObject parent;

        protected override void OnLoad()
        {
            btnBack.onClick.AddListener(() => Message.Send<RhythmGameMusicSeletCloseMsg>(new RhythmGameMusicSeletCloseMsg()));
        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<RhythmGameMusicListMsg>(RhythmGameMusicList);
        }

        private void RhythmGameMusicList(RhythmGameMusicListMsg msg)
        {
            int count = msg.ListRhythmGameMusic.Count;
          
            if (parent.transform.childCount == count)
            {
                return;
            }

            float height = musicItem.GetComponent<RectTransform>().sizeDelta.y;
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x, height * count);

            for (int i = 0; i < count; i++)
            {
                GameObject tempObj = GameObject.Instantiate(musicItem) as GameObject;
                tempObj.transform.parent = parent.transform;
                tempObj.transform.localScale = new Vector3(1, 1, 1);
                tempObj.transform.localPosition = new Vector3(0,- i * height, 0);
                tempObj.GetComponent<RhythmMusic_Item_Controller>().InitRhythmMusictme(msg.ListRhythmGameMusic[i].Title, msg.ListRhythmGameMusic[i].Index);
            }
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<RhythmGameMusicListMsg>(RhythmGameMusicList);
        }
    }
}