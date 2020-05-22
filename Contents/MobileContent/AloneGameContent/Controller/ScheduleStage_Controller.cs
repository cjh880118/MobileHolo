using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellBig.Constants;

namespace CellBig.Contents
{
    public class ScheduleStage_Controller : MonoBehaviour
    {
        public GameObject Vocal;
        public GameObject Dance;
        public GameObject Entertainment;
        public GameObject Intelligence;
        public GameObject Meal;
        public GameObject Rest;

        public void SetStage(Schedule schedule, Character character, float volume)
        {
            AllObjectActiveFalse();

            SoundManager.Instance.StopAllSound();
            if (schedule == Schedule.Vocal)
            {
                SoundManager.Instance.PlaySound((int)SoundType.BGM_Record, volume);
                Vocal.SetActive(true);
                if (Character.Boy == character)
                {

                }
                else
                {
                    Vocal.transform.localPosition = Vector3.zero;
                    Vocal.transform.localEulerAngles = Vector3.zero;
                }
            }
            else if (schedule == Schedule.Dance)
            {
                SoundManager.Instance.PlaySound((int)SoundType.BGM_Dance, volume);
                Dance.SetActive(true);
                if (Character.Boy == character)
                {

                }
                else
                {
                    Dance.transform.localPosition = Vector3.zero;
                    Dance.transform.localEulerAngles = Vector3.zero;
                }
            }
            else if (schedule == Schedule.Entertainment)
            {
                SoundManager.Instance.PlaySound((int)SoundType.BGM_Enter, volume);
                Entertainment.SetActive(true);
                if (Character.Boy == character)
                {

                }
                else
                {
                    Entertainment.transform.localPosition = Vector3.zero;
                    Entertainment.transform.localEulerAngles = Vector3.zero;
                }
            }
            else if (schedule == Schedule.Intelligence)
            {
                SoundManager.Instance.PlaySound((int)SoundType.BGM_Lib, volume);
                Intelligence.SetActive(true);
                if (Character.Boy == character)
                {

                }
                else
                {
                    Intelligence.transform.localPosition = Vector3.zero;
                    Intelligence.transform.localEulerAngles = Vector3.zero;
                }
            }
            else if (schedule == Schedule.Meal)
            {
                SoundManager.Instance.PlaySound((int)SoundType.BGM_Meal, volume);
                Meal.SetActive(true);
                if (Character.Boy == character)
                {

                }
                else
                {
                    Meal.transform.localPosition = Vector3.zero;
                    Meal.transform.localEulerAngles = Vector3.zero;
                }
            }
            else if (schedule == Schedule.Rest)
            {
                SoundManager.Instance.PlaySound((int)SoundType.BGM_Sleep, volume);
                Rest.SetActive(true);
                if (Character.Boy == character)
                {

                }
                else
                {
                    Rest.transform.localPosition = Vector3.zero;
                    Rest.transform.localEulerAngles = Vector3.zero;
                }
            }
        }

        void AllObjectActiveFalse()
        {
            Meal.SetActive(false);
            Dance.SetActive(false);
            Intelligence.SetActive(false);
            Entertainment.SetActive(false);
            Rest.SetActive(false);
            Vocal.SetActive(false);
        }
    }
}