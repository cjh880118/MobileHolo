using CellBig.Constants;
using CellBig.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmGame_Controller : MonoBehaviour
{
    MemoryPool memoryPoolRight;
    MemoryPool memoryPoolLeft;
    MemoryPool memoryPoolEffect;
    GameObject[] RightNote;
    GameObject[] LeftNote;
    GameObject[] Effect;

    public GameObject effectItem;
    public GameObject noteItem;
    public GameObject rightParent;
    public GameObject rightTarget;
    public GameObject leftParent;
    public GameObject leftTarget;
    public GameObject rightButton;
    public GameObject rightOver;
    public GameObject leftButton;
    public GameObject leftOver;

    public void InitRhythmGameNote()
    {
        RightNote = new GameObject[10];
        memoryPoolRight = new MemoryPool();
        memoryPoolRight.Create(noteItem, 10, rightParent);

        LeftNote = new GameObject[10];
        memoryPoolLeft = new MemoryPool();
        memoryPoolLeft.Create(noteItem, 10, leftParent);

        Effect = new GameObject[10];
        memoryPoolEffect = new MemoryPool();
        memoryPoolEffect.Create(effectItem, 10, this.gameObject);

        //rightButton.SetActive(true);
        //leftButton.SetActive(true);

        AddMessage();
    }

    private void AddMessage()
    {
        Message.AddListener<RhythmGameBtnTouchMsg>(RhythmGameBtnTouch);
        Message.AddListener<RhythmGameNoteDeleteMsg>(RhythmGameNoteDelete);
        Message.AddListener<RhythmGameNoteCreateMsg>(RhythmGameNoteCreate);
    }

    void RhythmGameBtnTouch(RhythmGameBtnTouchMsg msg)
    {
        if (msg.isRight)
        {
            rightOver.SetActive(true);
            StartCoroutine(ButtonOverClose(rightOver));
            
            RhythmNote tempRhythmNote = RhythmNote.None;
            int minValue = -1;
            int noteIndex = 0;
            for (int i = 0; i < RightNote.Length; i++)
            {
                if (RightNote[i] != null)
                {
                    int tempNum = RightNote[i].GetComponent<RhythmGameNote_Controller>().noteCount;
                    if (minValue == -1)
                    {
                        noteIndex = i;
                        minValue = tempNum;
                    }
                    else if (minValue > tempNum)
                    {
                        noteIndex = i;
                        minValue = tempNum;
                    }
                }
            }
            if (RightNote[noteIndex] == null)
                return;

            tempRhythmNote = RightNote[noteIndex].GetComponent<RhythmGameNote_Controller>().rhythmNote;
            if (tempRhythmNote != RhythmNote.None && tempRhythmNote != RhythmNote.Miss)
            {
                RhythmGameNoteDelete(new RhythmGameNoteDeleteMsg(tempRhythmNote, noteIndex, RightNote[noteIndex], true, false));
            }
        }
        else
        {
            leftOver.SetActive(true);
            StartCoroutine(ButtonOverClose(leftOver));
            RhythmNote tempRhythmNote = RhythmNote.None;
            int minValue = -1;
            int noteIndex = 0;
            for (int i = 0; i < LeftNote.Length; i++)
            {
                if (LeftNote[i] != null)
                {
                    int tempNum = LeftNote[i].GetComponent<RhythmGameNote_Controller>().noteCount;
                    if (minValue == -1)
                    {
                        noteIndex = i;
                        minValue = tempNum;
                    }
                    else if (minValue > tempNum )
                    {
                        noteIndex = i;
                        minValue = tempNum;
                    }
                }
            }
            if (LeftNote[noteIndex] == null)
                return;

            tempRhythmNote = LeftNote[noteIndex].GetComponent<RhythmGameNote_Controller>().rhythmNote;
            if (tempRhythmNote != RhythmNote.None && tempRhythmNote != RhythmNote.Miss)
            {
                RhythmGameNoteDelete(new RhythmGameNoteDeleteMsg(tempRhythmNote, noteIndex, LeftNote[noteIndex], false, false));
            }
        }
    }

    IEnumerator ButtonOverClose(GameObject obj)
    {
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(false);
    }

    private void RhythmGameNoteDelete(RhythmGameNoteDeleteMsg msg)
    {
        Message.Send<RhythmGameNoteJudgeMsg>(new RhythmGameNoteJudgeMsg(msg.rhythmNote));

        if (msg.isNoTouch)
        {
            if (msg.isRight)
            {
                memoryPoolRight.RemoveItem(msg.note);
                RightNote[msg.index] = null;
            }
            else
            {
                memoryPoolLeft.RemoveItem(msg.note);
                LeftNote[msg.index] = null;
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                if (Effect[i] == null)
                {
                    Effect[i] = memoryPoolEffect.NewItem();
                    Effect[i].transform.localPosition = new Vector3(0, 0.3f, 1);
                    Effect[i].transform.localScale = new Vector3(noteItem.transform.localScale.x, noteItem.transform.localScale.y, noteItem.transform.localScale.z);
                    Effect[i].GetComponent<RhythmTxt_Controller>().SetEffect(msg.rhythmNote);
                    StartCoroutine(TxtEffectReset(Effect[i], i));
                    break;
                }
            }

            msg.note.GetComponent<RhythmGameNote_Controller>().TouchEffect();
            StartCoroutine(NoteTouchAni(msg.isRight, msg.note, msg.index));
        }
    }

    IEnumerator TxtEffectReset(GameObject txtEffect, int index)
    {
        yield return new WaitForSeconds(0.5f);
        txtEffect.GetComponent<RhythmTxt_Controller>().ResetEffect();
        memoryPoolEffect.RemoveItem(txtEffect);
        Effect[index] = null;
    }


    IEnumerator NoteTouchAni(bool isRight, GameObject note, int index)
    {
        note.GetComponent<RhythmGameNote_Controller>().isTouch = true;
        yield return new WaitForSeconds(0.2f);
        if (isRight)
        {
            memoryPoolRight.RemoveItem(note);
            RightNote[index] = null;
        }
        else
        {
            memoryPoolLeft.RemoveItem(note);
            LeftNote[index] = null;
        }
    }

    private void RhythmGameNoteCreate(RhythmGameNoteCreateMsg msg)
    {
        if (msg.isRigth)
        {
            for (int i = 0; i < 10; i++)
            {
                if (RightNote[i] == null)
                {
                    RightNote[i] = memoryPoolRight.NewItem();
                    RightNote[i].transform.localPosition = Vector3.zero;
                    RightNote[i].transform.localScale = new Vector3(noteItem.transform.localScale.x, noteItem.transform.localScale.y, noteItem.transform.localScale.z);
                    RightNote[i].GetComponent<RhythmGameNote_Controller>().MoveStart(rightTarget.transform.position, true, i, msg.noteCount);
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                if (LeftNote[i] == null)
                {
                    LeftNote[i] = memoryPoolLeft.NewItem();
                    LeftNote[i].transform.localPosition = Vector3.zero;
                    LeftNote[i].transform.localScale = new Vector3(noteItem.transform.localScale.x, noteItem.transform.localScale.y, noteItem.transform.localScale.z);
                    LeftNote[i].GetComponent<RhythmGameNote_Controller>().MoveStart(leftTarget.transform.position, false, i, msg.noteCount);
                    return;
                }
            }
        }
    }

    public void GameEnd()
    {
        MemoryPoolDelete();
        RemoveMessage();
    }

    void MemoryPoolDelete()
    {
        memoryPoolRight.Dispose();
        memoryPoolLeft.Dispose();
        memoryPoolEffect.Dispose();
        foreach (var o in LeftNote)
        {
            Destroy(o);
        }

        foreach (var o in RightNote)
        {
            Destroy(o);
        }

        foreach (var o in Effect)
        {
            Destroy(o);
        }
    }

    private void RemoveMessage()
    {
        Message.RemoveListener<RhythmGameBtnTouchMsg>(RhythmGameBtnTouch);
        Message.RemoveListener<RhythmGameNoteDeleteMsg>(RhythmGameNoteDelete);
        Message.RemoveListener<RhythmGameNoteCreateMsg>(RhythmGameNoteCreate);
    }
}
