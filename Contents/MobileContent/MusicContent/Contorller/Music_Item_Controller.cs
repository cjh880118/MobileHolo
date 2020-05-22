using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CellBig.UI.Event;


namespace CellBig.UI
{
    public class Music_Item_Controller : MonoBehaviour
    {
        public Button btnMusicSelect;
        public Text txtMusicName;

        public void InitMusicItem(int index, string musicName)
        {
            txtMusicName.text = musicName;
            btnMusicSelect.onClick.AddListener(() => Message.Send<MusicItemSelectMsg>(new MusicItemSelectMsg(index)));
        }
    }
}