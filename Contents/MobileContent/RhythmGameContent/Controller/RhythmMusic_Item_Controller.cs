using JHchoi.UI.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmMusic_Item_Controller : MonoBehaviour
{
    public Button btnItem;
    public Text txtMusicTitle;

    public void InitRhythmMusictme(string name, int musicIndex)
    {
        btnItem.onClick.AddListener(() => Message.Send<RhythmGameMusicSelectMsg>(new RhythmGameMusicSelectMsg(musicIndex)));
        txtMusicTitle.text = name;
    }
}
