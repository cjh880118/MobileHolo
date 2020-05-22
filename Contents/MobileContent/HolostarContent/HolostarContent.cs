using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellBig.Models;
using CellBig.UI.Event;
using CellBig.Constants;
using CellBig.Android;
using Midiazen;
using System;
using CellBig.UI;

namespace CellBig.Contents
{
    public class HolostarContent : IContent
    {
        static string TAG = "HolostarContent :: ";
        HolostartCharacter_Controller character_Controller;
        PlayerInventoryModel playerInventoryModel;
        SettingModel settingModel;
        Coroutine btnEvent;

        protected override void OnLoadStart()
        {
            playerInventoryModel = Model.First<PlayerInventoryModel>();
            settingModel = Model.First<SettingModel>();
            SetLoadComplete();
            //StartCoroutine(LoadInitialData());
        }

        IEnumerator LoadInitialData()
        {
            string path = "Object/Character/Face";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path,
               o =>
               {
                   var gameObject = Instantiate(o) as GameObject;
                   character_Controller = gameObject.GetComponent<HolostartCharacter_Controller>();
                   gameObject.transform.position = new Vector3(0, 0, 0);
                   gameObject.SetActive(false);
               }));

            SetLoadComplete();
        }

        protected override void OnEnter()
        {
            Debug.Log(TAG + "OnEnter");
            btnEvent = StartCoroutine(ButtonInput());
            AddMessage();
            IContent.RequestContentExit<MenuContent>();
            UI.IDialog.RequestDialogEnter<UI.HoloStarMainDialog>();
            //character_Controller.SetCharacter(playerInventoryModel.NowCharacter);
            Message.Send<SetHolostarCharacterMsg>(new SetHolostarCharacterMsg(playerInventoryModel.NowCharacter));
        }

        IEnumerator ButtonInput()
        {
            while (true)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log(TAG + " : Escape");
                    IContent.RequestContentEnter<MenuContent>();
                }
            }
        }

        private void AddMessage()
        {
            Message.AddListener<HolostarImgClickMsg>(HolostarImgClick);
        }

        private void HolostarImgClick(HolostarImgClickMsg msg)
        {
            string temp_comment = "";
            int temp_num = UnityEngine.Random.Range(0, 16);
            AnimationType face = AnimationType.Smile;

            if (temp_num == 0)
            {
                face = AnimationType.Sad;
                temp_comment = "어서오세요!";
            }
            else if (temp_num == 1)
            {
                face = AnimationType.Smile;
                temp_comment = "세계는 즐거움으로 가득하네요.";
            }
            else if (temp_num == 2)
            {
                face = AnimationType.Surprise;
                temp_comment = "당신도 저의 팬이셨던가요?";
            }
            else if (temp_num == 3)
            {
                face = AnimationType.Smile;
                temp_comment = "그래도 곤란하네요. 제가 해드릴수 있는 거라곤.. 그렇지, 개인기라도 해볼까요?";
            }
            else if (temp_num == 4)
            {
                face = AnimationType.Angry;
                temp_comment = "자꾸 콕콕 찌르시면 곤란해요.";
            }
            else if (temp_num == 5)
            {
                face = AnimationType.Sad;
                temp_comment = "그럼요 혼자서도 잘 놀아요.";
            }
            else if (temp_num == 6)
            {
                face = AnimationType.Surprise;
                temp_comment = "당신도 방치플레이 좋아하시나요?";
            }
            else if (temp_num == 7)
            {
                face = AnimationType.Smile;
                temp_comment = "언젠가 멋진 소식을 당신에게 들려드릴께요.";
            }
            else if (temp_num == 8)
            {
                face = AnimationType.Angry;
                temp_comment = "그만!";
            }
            else if (temp_num == 9)
            {
                face = AnimationType.Surprise;
                temp_comment = "아! 부르셨어요?";
            }
            else if (temp_num == 10)
            {
                face = AnimationType.Sad;
                temp_comment = "스케줄 관리는 잘 해주고 계신거죠?";
            }
            else if (temp_num == 11)
            {
                face = AnimationType.Smile;
                temp_comment = "아뇨아뇨. 잠들지 않았어요.";
            }
            else if (temp_num == 12)
            {
                face = AnimationType.Smile;
                temp_comment = "오늘도 행운 가득한 하루가 될 거예요.";
            }
            else if (temp_num == 13)
            {
                face = AnimationType.Smile;
                temp_comment = "네. 저 여기 있어요.";
            }
            else if (temp_num == 14)
            {
                face = AnimationType.Smile;
                temp_comment = "아! 오래만이예요. 저는 잘 지내고 있어요.";
            }
            else
            {
                face = AnimationType.Smile;
                temp_comment = "오늘은 좋은 하루가 되셨나요?";
            }
            if (settingModel.IsBluetoothConnet)
                AndroidTrasferMgr.Instance.BluetoothSendMsg(temp_comment, SENDMSGTYPE.MSG);

            Message.Send<HolostarStringMsg>(new HolostarStringMsg(temp_comment));
            Message.Send<SetCharacterAnimationMsg>(new SetCharacterAnimationMsg(face, false));
        }

        protected override void OnExit()
        {
            if (btnEvent != null)
                StopCoroutine(btnEvent);
            DialogAllClose();
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<HolostarImgClickMsg>(HolostarImgClick);
        }

    }
}