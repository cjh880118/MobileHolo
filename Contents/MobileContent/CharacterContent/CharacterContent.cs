using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellBig.Models;
using System;
using CellBig.UI.Event;
using CellBig.Android;
using CellBig.Constants;

namespace CellBig.Contents
{
    public class CharacterContent : IContent
    {
        static string TAG = "CharacterContent :: ";
        PlayerInventoryModel playerInventoryModel;
        SettingModel settingModel;
        Dictionary<Character, GameObject> dicCharacter;

        #region Contents Load
        protected override void OnLoadStart()
        {
            Debug.Log(TAG + "OnLoadStart");
            dicCharacter = new Dictionary<Character, GameObject>();
            playerInventoryModel = Model.First<PlayerInventoryModel>();
            settingModel = Model.First<SettingModel>();
            StartCoroutine(LoadInitialData());
        }

        IEnumerator LoadInitialData()
        {
            string path = "Object/Character/GirlRttObject";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path,
               o =>
               {
                   var girl = Instantiate(o) as GameObject;
                   dicCharacter.Add(Character.Girl, girl);
                   girl.SetActive(false);
               }));

            path = "Object/Character/BoyRttObject";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path,
               o =>
               {
                   var boy = Instantiate(o) as GameObject;
                   dicCharacter.Add(Character.Boy, boy);
                   boy.transform.position = new Vector3(4, 0, 0);
                   boy.SetActive(false);
               }));

            foreach (var o in dicCharacter)
            {
                o.Value.GetComponent<Character_Controller>().InitCharacter();
            }
            SetLoadComplete();
        }
        #endregion

        protected override void OnEnter()
        {
            AddMessage();
            SetCharacterDress(new SetCharacterDressMsg(playerInventoryModel.NowCharacter, playerInventoryModel.NowSkin));
        }

        private void AddMessage()
        {
            Message.AddListener<RunMenuMsg>(RunMenu);
            Message.AddListener<SetCharacterDressMsg>(SetCharacterDress);
            Message.AddListener<SetCharacterAnimationMsg>(SetCharacterAnimation);
            Message.AddListener<SetStoreCharacterMsg>(SetStoreCharacter);
        }

        private void SetCharacterAnimation(SetCharacterAnimationMsg msg)
        {
            dicCharacter[playerInventoryModel.NowCharacter].GetComponent<Character_Controller>().SetAniMation((int)msg.animationType, msg.isBluetoothCommand);

            if (settingModel.IsBluetoothConnet && !msg.isBluetoothCommand)
                AndroidTrasferMgr.Instance.BluetoothSendMsg(msg.animationType.ToString(), SENDMSGTYPE.ANIMATION);
        }

        private void SetStoreCharacter(SetStoreCharacterMsg msg)
        {
            if (msg.isStoreEnter)
            {
                for (int i = 0; i < dicCharacter.Count; i++)
                {
                    if ((Character)i != playerInventoryModel.NowCharacter)
                    {
                        dicCharacter[(Character)i].GetComponent<Character_Controller>().SetDress(0);
                    }

                    dicCharacter[(Character)i].GetComponent<Character_Controller>().SetAniMation((int)AnimationType.Idel1, false);
                }
            }
        }

        private void SetCharacterDress(SetCharacterDressMsg msg)
        {
            foreach (var o in dicCharacter)
            {
                o.Value.SetActive(false);
            }

            dicCharacter[msg.character].SetActive(true);

            if (msg.character == Character.Boy)
                msg.dressNum -= 10;

            dicCharacter[msg.character].GetComponent<Character_Controller>().SetDress(msg.dressNum);

            if (settingModel.IsBluetoothConnet)
                AndroidTrasferMgr.Instance.BluetoothSendMsg(JsonUtility.ToJson(playerInventoryModel.PlayerInventory), SENDMSGTYPE.CHARINFO);
        }

        private void RunMenu(RunMenuMsg msg)
        {
            foreach (var o in dicCharacter)
            {
                o.Value.SetActive(false);
                o.Value.GetComponent<Character_Controller>().SetCameraPosition(false);
            }

            dicCharacter[playerInventoryModel.NowCharacter].GetComponent<Character_Controller>().SetHoloCharacter(false);
            if (msg.menu == Menu.Watch)
            {
                dicCharacter[playerInventoryModel.NowCharacter].SetActive(true);
                SetCharacterAnimation(new SetCharacterAnimationMsg(AnimationType.Idel2, false));
            }
            else if (msg.menu == Menu.Music)
            {
                dicCharacter[playerInventoryModel.NowCharacter].SetActive(true);
                dicCharacter[playerInventoryModel.NowCharacter].transform.position = Vector3.zero;
            }
            else if (msg.menu == Menu.Game)
            {
                dicCharacter[playerInventoryModel.NowCharacter].SetActive(true);
                dicCharacter[playerInventoryModel.NowCharacter].transform.position = Vector3.zero;
            }
            else if (msg.menu == Menu.HoloStar)
            {
                dicCharacter[playerInventoryModel.NowCharacter].SetActive(true);
                dicCharacter[playerInventoryModel.NowCharacter].GetComponent<Character_Controller>().SetHoloCharacter(true);
                dicCharacter[playerInventoryModel.NowCharacter].GetComponent<Character_Controller>().SetCameraPosition(true);
                //SetCharacterAnimation(new SetCharacterAnimationMsg(AnimationType.Smile, false));
            }
            else if (msg.menu == Menu.Store)
            {
                foreach (var o in dicCharacter)
                {
                    o.Value.SetActive(true);
                    if (o.Key == Character.Boy)
                        o.Value.transform.position = new Vector3(5, 0, 0);

                    o.Value.GetComponent<Character_Controller>().SetAniMation((int)AnimationType.Idel1, false);
                }
            }
            else if (msg.menu == Menu.Option)
            {
                foreach (var o in dicCharacter)
                {
                    o.Value.SetActive(false);
                }
            }
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<RunMenuMsg>(RunMenu);
            Message.RemoveListener<SetCharacterDressMsg>(SetCharacterDress);
            Message.RemoveListener<SetCharacterAnimationMsg>(SetCharacterAnimation);
            Message.RemoveListener<SetStoreCharacterMsg>(SetStoreCharacter);
        }
    }
}
