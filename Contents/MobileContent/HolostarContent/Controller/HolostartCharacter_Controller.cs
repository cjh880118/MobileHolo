using CellBig.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolostartCharacter_Controller : MonoBehaviour
{
    public List<GameObject> holostarCharacter;
    Animator nowCharacterAnimator;

    public void SetCharacter(Character nowCharacter)
    {
        foreach (var o in holostarCharacter)
            o.SetActive(false);


        nowCharacterAnimator = null;
        holostarCharacter[(int)nowCharacter].SetActive(true);
        nowCharacterAnimator = holostarCharacter[(int)nowCharacter].GetComponent<Animator>();
    }

    public void SetAniMation(int aniNum, bool isBluetoothCommand)
    {
        nowCharacterAnimator.SetInteger("AnimationNum", aniNum);
    }
}
