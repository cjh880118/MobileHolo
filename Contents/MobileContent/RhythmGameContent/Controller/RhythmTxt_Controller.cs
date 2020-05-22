using CellBig.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTxt_Controller : MonoBehaviour
{
    public GameObject[] Judge;

    public void SetEffect(RhythmNote rhythmNote)
    {
        if (rhythmNote == RhythmNote.Perfect)
        {
            Judge[1].SetActive(true);
        }
        else if (rhythmNote == RhythmNote.Good)
        {
            Judge[0].SetActive(true);
        }
        else if (rhythmNote == RhythmNote.Bad)
        {
            Judge[2].SetActive(true);
        }
    }

    public void ResetEffect()
    {
        foreach (var o in Judge)
        {
            o.SetActive(false);
        }
    }
}
