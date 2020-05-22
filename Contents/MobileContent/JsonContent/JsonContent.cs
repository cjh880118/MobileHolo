using JHchoi.Constants;
using JHchoi.Models;
using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace JHchoi.Contents
{
    public class JsonContent : IContent
    {
        //스케줄
        GameSchedule gameSchedule;
        List<AloneGameSchedule> listAlongGameSchedule;
        Dictionary<int, AloneGameSchedule> dicSchedule;
        //플레이어 정보
        PlayerStatus playerStatus;
        //플레이어 인벤토리
        PlayerInventory playerInventory;
        HoloStarSetting holostarSetting;
        MobileOption mobileOption;

        MobileOptionModel mobileOptionModel;
        ScheduleModel scheduleModel;
        PlayerStatusModel playerStatusModel;
        PlayerInventoryModel playerInventoryModel;
        SettingModel settingModel;
        HolostarSettingModel holostarSettingModel;

        protected override void OnLoadStart()
        {
            gameSchedule = new GameSchedule();
            listAlongGameSchedule = new List<AloneGameSchedule>();
            dicSchedule = new Dictionary<int, AloneGameSchedule>();
            playerStatus = new PlayerStatus();
            playerInventory = new PlayerInventory();
            holostarSetting = new HoloStarSetting();
            mobileOption = new MobileOption();

            scheduleModel = Model.First<ScheduleModel>();
            playerStatusModel = Model.First<PlayerStatusModel>();
            playerInventoryModel = Model.First<PlayerInventoryModel>();
            settingModel = Model.First<SettingModel>();
            holostarSettingModel = Model.First<HolostarSettingModel>();
            mobileOptionModel = Model.First<MobileOptionModel>();

            CheckFile("Schedule");
            CheckFile("PlayerStatus");
            CheckFile("PlayerInventory");
            CheckFile("HoloStarSetting");
            CheckFile("MobileOption");
            SetLoadComplete();
        }

        void CheckFile(string fileName)
        {

#if (UNITY_EDITOR)
            string path = Application.dataPath + "/Resources/Json/" + fileName + ".json";

#elif (UNITY_ANDROID)

            string sDirPath;
            sDirPath = Application.persistentDataPath + "/Json";
            DirectoryInfo di = new DirectoryInfo(sDirPath);
            if (di.Exists == false)
            {
                di.Create();
            }

            string path = Application.persistentDataPath + "/Json/" + fileName + ".json";
#endif
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                if (fileName == "PlayerStatus")
                {
                    playerStatus = LoadJsonFile<PlayerStatus>("PlayerStatus");
                    playerStatusModel.CharacterStatus = playerStatus;
                }
                else if (fileName == "Schedule")
                {
                    gameSchedule = LoadJsonFile<GameSchedule>("Schedule");
                    for (int i = 0; i < gameSchedule.alonGameShedule.Length; i++)
                    {
                        listAlongGameSchedule.Add(gameSchedule.alonGameShedule[i]);
                        dicSchedule.Add(i, gameSchedule.alonGameShedule[i]);
                    }
                    scheduleModel.DicSchedule = dicSchedule;
                }
                else if (fileName == "PlayerInventory")
                {
                    playerInventory = LoadJsonFile<PlayerInventory>("PlayerInventory");
                    playerInventoryModel.PlayerInventory = playerInventory;
                    playerInventoryModel.ArrayBuyCharacter = playerInventory.buyCharacter;
                    playerInventoryModel.ArrayBuySkinNum = playerInventory.buySkinNum;
                }
                else if (fileName == "HoloStarSetting")
                {
                    holostarSetting = LoadJsonFile<HoloStarSetting>("HoloStarSetting");
                    holostarSettingModel.HoloStarSetting = holostarSetting;
                }
                else if(fileName == "MobileOption")
                {
                    mobileOption = LoadJsonFile<MobileOption>("MobileOption");
                    mobileOptionModel.MobileOption = mobileOption;
                }
            }
            else
            {
                if (fileName == "PlayerStatus")
                {
                    CreatePlayerStatus();
                    playerStatusModel.CharacterStatus = playerStatus;
                }
                else if (fileName == "Schedule")
                {
                    CreateScheduleJson();

                    for (int i = 0; i < listAlongGameSchedule.Count; i++)
                    {
                        dicSchedule.Add(i, listAlongGameSchedule[i]);
                    }

                    scheduleModel.DicSchedule = dicSchedule;
                }
                else if (fileName == "PlayerInventory")
                {
                    CreatePlayerInventoryJson();
                    playerInventoryModel.PlayerInventory = playerInventory;
                    playerInventoryModel.ArrayBuyCharacter = playerInventory.buyCharacter;
                    playerInventoryModel.ArrayBuySkinNum = playerInventory.buySkinNum;
                }
                else if (fileName == "HoloStarSetting")
                {
                    CreateSettingJson();
                    holostarSettingModel.HoloStarSetting = holostarSetting;
                }
                else if (fileName == "MobileOption")
                {
                    CreateMobileOptionJson();
                    mobileOptionModel.MobileOption = mobileOption;
                }
            }
        }

        T LoadJsonFile<T>(string fileName)
        {
            string path;
#if (UNITY_EDITOR)
            path = Application.dataPath + "/Resources/Json/";

#elif (UNITY_ANDROID)
             path = Application.persistentDataPath + "/Json/";
#endif
            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", path, fileName), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);
            return JsonUtility.FromJson<T>(jsonData);
        }

        //최초 케릭터 정보 생성
        void CreatePlayerStatus()
        {
            playerStatus.vocal = 0;
            playerStatus.vocalGage = 0;
            playerStatus.vocalSkill = 0;
            playerStatus.dance = 0;
            playerStatus.danceGage = 0;
            playerStatus.danceSkill = 0;
            playerStatus.entertainment = 0;
            playerStatus.entertainmentGage = 0;
            playerStatus.intelligence = 0;
            playerStatus.intelligenceGage = 0;
            playerStatus.intelligenceSkill = 0;
            playerStatus.coin = 9999999;
            playerStatus.potential = 0;
            playerStatus.relaxSkill = 0;
            playerStatus.relaxSkill = 0;
            playerStatus.selfManagementSkill = 0;
            playerStatus.manageMentCout = 20;
            string temp = JsonUtility.ToJson(playerStatus);
            CreateJsonFile("PlayerStatus", temp);
        }

        //최초 스케줄 정보 생성
        void CreateScheduleJson()
        {
            for (int i = 0; i < settingModel.ScheduleCount; i++)
            {
                System.DateTime startTime = System.DateTime.Now.AddSeconds(i * settingModel.CompleteSpanTime);
                Schedule tempSchedule;

                if (i % 12 == 1 || i % 12 == 5)
                {
                    tempSchedule = Schedule.Meal;
                }
                else if (i % 12 == 9 || i % 12 == 10)
                {
                    tempSchedule = Schedule.Rest;
                }
                else
                {
                    tempSchedule = (Schedule)UnityEngine.Random.Range(0, 4);
                }
                listAlongGameSchedule.Add(CreateSchedule(i, tempSchedule, startTime.ToString()));
            }

            gameSchedule.alonGameShedule = listAlongGameSchedule.ToArray();
            string temp = JsonUtility.ToJson(gameSchedule);
            CreateJsonFile("Schedule", temp);
        }

        //최초 플레이어 인벤토리 생성
        private void CreatePlayerInventoryJson()
        {
            playerInventory.nowCharacter = Character.Girl;
            playerInventory.nowSkin = 0;

            List<int> listBuyCharacter = new List<int>();
            listBuyCharacter.Add(0);
            playerInventory.buyCharacter = listBuyCharacter.ToArray();

            List<int> listBuySkin = new List<int>();
            listBuySkin.Add(0);
            //listBuySkin.Add(1);
            playerInventory.buySkinNum = listBuySkin.ToArray();

            string temp = JsonUtility.ToJson(playerInventory);
            CreateJsonFile("PlayerInventory", temp);
        }

        AloneGameSchedule CreateSchedule(int index, Schedule schedule, string dateTime)
        {
            AloneGameSchedule alonGameShedule = new AloneGameSchedule();
            alonGameShedule.index = index;
            alonGameShedule.schedule = schedule;
            alonGameShedule.dateTime = dateTime;
            return alonGameShedule;
        }

        void CreateMobileOptionJson()
        {
            mobileOption.gameVolume = 1.0f;
            mobileOption.effectVolume = 1.0f;
            string temp = JsonUtility.ToJson(mobileOption);
            CreateJsonFile("MobileOption", temp);
        }

        void CreateSettingJson()
        {
            holostarSetting.holoOptionSetting = new HoloOptionSetting();
            holostarSetting.musicSetting = new MusicSetting();

            holostarSetting.holoOptionSetting.isMMSReceive = false;
            holostarSetting.holoOptionSetting.isTTSReceive = false;
            holostarSetting.holoOptionSetting.isSchedule = false;

            holostarSetting.musicSetting.isMusicPlay = false;
            holostarSetting.musicSetting.musicVolume = 1.0f;
            holostarSetting.musicSetting.musicIndex = 0;
            holostarSetting.musicSetting.isRepeat = true;
            holostarSetting.musicSetting.musicNowTime = 0;

            string temp = JsonUtility.ToJson(holostarSetting);
            CreateJsonFile("HoloStarSetting", temp);
        }

        void CreateJsonFile(string fileName, string jsonData)
        {
            string path;
#if (UNITY_EDITOR)
            path = Application.dataPath + "/Resources/Json/";

#elif (UNITY_ANDROID)
             path = Application.persistentDataPath + "/Json/";
#endif

            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", path, fileName), FileMode.Create);
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }

        void DestroyHoloStar()
        {
            Debug.Log("OnApplicationPause");
            listAlongGameSchedule.Clear();
            for (int i = 0; i < scheduleModel.DicSchedule.Count; i++)
            {
                listAlongGameSchedule.Add(scheduleModel.DicSchedule[i]);
            }
            gameSchedule.alonGameShedule = listAlongGameSchedule.ToArray();

            SaveJson("Schedule", gameSchedule);
            SaveJson("PlayerInventory", playerInventoryModel.PlayerInventory);
            SaveJson("PlayerStatus", playerStatusModel.CharacterStatus);
            SaveJson("HoloStarSetting", holostarSettingModel.HoloStarSetting);
            SaveJson("MobileOption", mobileOptionModel.MobileOption);
        }

        private void OnApplicationPause(bool pause)
        {
            Debug.Log("OnApplicationPause");
            listAlongGameSchedule.Clear();
            for (int i = 0; i < scheduleModel.DicSchedule.Count; i++)
            {
                listAlongGameSchedule.Add(scheduleModel.DicSchedule[i]);
            }
            gameSchedule.alonGameShedule = listAlongGameSchedule.ToArray();

            SaveJson("Schedule", gameSchedule);
            SaveJson("PlayerInventory", playerInventoryModel.PlayerInventory);
            SaveJson("PlayerStatus", playerStatusModel.CharacterStatus);
            SaveJson("HoloStarSetting", holostarSettingModel.HoloStarSetting);
            SaveJson("MobileOption", mobileOptionModel.MobileOption);
        }

        //private void OnApplicationQuit()
        //{
        //    Debug.Log("OnApplicationQuit");
        //    listAlongGameSchedule.Clear();
        //    for (int i = 0; i < scheduleModel.DicSchedule.Count; i++)
        //    {
        //        listAlongGameSchedule.Add(scheduleModel.DicSchedule[i]);
        //    }
        //    gameSchedule.alonGameShedule = listAlongGameSchedule.ToArray();

        //    SaveJson("Schedule", gameSchedule);
        //    SaveJson("PlayerInventory", playerInventoryModel.PlayerInventory);
        //    SaveJson("PlayerStatus", playerStatusModel.CharacterStatus);
        //    SaveJson("HoloStarSetting", holostarSettingModel.HoloStarSetting);
        //}

        //종료 직전 모델 데이터 json 으로 저장
        private void OnDestroy()
        {
            Debug.Log("OnDestroy");
            listAlongGameSchedule.Clear();
            for (int i = 0; i < scheduleModel.DicSchedule.Count; i++)
            {
                listAlongGameSchedule.Add(scheduleModel.DicSchedule[i]);
            }
            gameSchedule.alonGameShedule = listAlongGameSchedule.ToArray();

            SaveJson("Schedule", gameSchedule);
            SaveJson("PlayerInventory", playerInventoryModel.PlayerInventory);
            SaveJson("PlayerStatus", playerStatusModel.CharacterStatus);
            SaveJson("HoloStarSetting", holostarSettingModel.HoloStarSetting);
            SaveJson("MobileOption", mobileOptionModel.MobileOption);
        }

        void SaveJson(string fileName, object o)
        {
            Debug.Log("SAVE : " + fileName);
            string temp = JsonUtility.ToJson(o);
            CreateJsonFile(fileName, temp);
        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<CreateJosnFileMsg>(CreateJosnFile);
        }

        private void CreateJosnFile(CreateJosnFileMsg msg)
        {
            CreateJsonFile(msg.fileName, msg.jsonFile);
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<CreateJosnFileMsg>(CreateJosnFile);
        }

        protected override void OnUnload()
        {
        }
    }
}