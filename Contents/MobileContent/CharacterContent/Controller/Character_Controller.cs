using CellBig.Constants;
using CellBig.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public Camera frontCamera;
    public GameObject face;
    public GameObject normal;
    public RuntimeAnimatorController NewAniController;

    //public RuntimeAnimatorController AloneGame;
    //public RuntimeAnimatorController MotionDance;
    //public RuntimeAnimatorController Face;
    //public RuntimeAnimatorController Idle;
    public Animator animator;
    public GameObject lightObject;

    Vector3 oriPosition;
    Vector3 facialPotition;

    public GameObject[] dress;
    public GameObject etc;
    Coroutine corAnimation;

    public void InitCharacter()
    {
        AddMessage();
        animator.runtimeAnimatorController = NewAniController;
        frontCamera.orthographicSize = 1;
        oriPosition = frontCamera.transform.localPosition;
        facialPotition = new Vector3(0, 1.5f, 0.5f);
    }

    private void AddMessage()
    {
        Message.AddListener<RunMenuMsg>(RunMenu);
    }

    private void RunMenu(RunMenuMsg msg)
    {
        if (msg.menu == Menu.Game || msg.menu == Menu.Music)
            lightObject.SetActive(false);
        else if(msg.menu == Menu.Store)
        {
            face.SetActive(false);
            normal.SetActive(true);
        }
        else
            lightObject.SetActive(true);
    }

    public void SetHoloCharacter(bool isHolostar)
    {
        if (isHolostar)
        {
            face.SetActive(true);
            normal.SetActive(false);
        }
        else
        {
            normal.SetActive(true);
            face.SetActive(false);
        }
    }

    public void SetDress(int dressNum)
    {
        etc.SetActive(false);
        foreach (var o in dress)
        {
            o.SetActive(false);
        }
        if (dressNum < 5)
            etc.SetActive(true);

        dress[dressNum].SetActive(true);
    }

    public void SetAniMation(int aniNum, bool isBluetoothCommand)
    {
        animator = normal.GetComponent<Animator>();
        if (corAnimation != null)
        {
            StopCoroutine(corAnimation);
            corAnimation = null;
        }

        if (aniNum >= 0 && aniNum < 3)
        {
            //animator.runtimeAnimatorController = Idle;
            if (!isBluetoothCommand)
                corAnimation = StartCoroutine(RandomIdleAni());

            animator.SetInteger("AnimationNum", aniNum);
        }
        else if (aniNum >= 3 && aniNum < 9)
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger(((AnimationType)aniNum).ToString());

            //animator.runtimeAnimatorController = AloneGame;
        }
        else if (aniNum >= 9 && aniNum < 15)
        {
            animator = face.GetComponent<Animator>();
            animator.SetInteger("AnimationNum", aniNum);
            //animator = face.GetComponent<Animator>();
            //animator.runtimeAnimatorController = Face;
        }
        else if (aniNum >= 15)
        {
            animator.SetBool("Idle", false);
            Debug.Log(((AnimationType)aniNum).ToString());
            animator.SetTrigger(((AnimationType)aniNum).ToString());
            //animator.runtimeAnimatorController = MotionDance;

            if (!isBluetoothCommand)
                corAnimation = StartCoroutine(RandomMotionAni(aniNum));
        }

        Debug.Log("aniNum : " + aniNum);
        //animator.SetInteger("AnimationNum", aniNum);
    }

    IEnumerator RandomIdleAni()
    {
        float rndSec = UnityEngine.Random.Range(5, 10);
        yield return new WaitForSeconds(rndSec);
        int aniNum = UnityEngine.Random.Range((int)AnimationType.Idel1, (int)AnimationType.Idel3 + 1);

        Message.Send<SetCharacterAnimationMsg>(new SetCharacterAnimationMsg((AnimationType)aniNum, false));
    }

    IEnumerator RandomMotionAni(int aniNum)
    {
        yield return new WaitForSeconds(0.5f);

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        int rndDance = UnityEngine.Random.Range((int)AnimationType.Motion1, (int)AnimationType.Motion2 + 1);

        while (aniNum == rndDance)
        {
            rndDance = UnityEngine.Random.Range((int)AnimationType.Motion1, (int)AnimationType.Motion2 + 1);
            Debug.Log("rndDance : " + rndDance);
        }

        Message.Send<SetCharacterAnimationMsg>(new SetCharacterAnimationMsg((AnimationType)rndDance, false));
        //SetAniMation(rndDance, false);
    }

    public void SetCameraPosition(bool isHoloStar)
    {
        if (isHoloStar)
        {
            frontCamera.orthographicSize = 0.2f;
            frontCamera.transform.localPosition = facialPotition;
        }
        else
        {
            frontCamera.orthographicSize = 1f;
            frontCamera.transform.localPosition = oriPosition;
        }
    }
}
