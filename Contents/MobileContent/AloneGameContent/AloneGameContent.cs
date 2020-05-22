using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.UI.Event;
using System;
using JHchoi.Constants;
using JHchoi.Models;
using JHchoi.Android;
using JHchoi.UI;

namespace JHchoi.Contents
{
    [Serializable]
    public class AloneGameContent : IContent
    {
        static string TAG = "AloneGameContent :: ";

        ScheduleModel scheduleModel;
        PlayerInventoryModel playerInventoryModel;
        PlayerStatusModel playerStatusModel;
        SettingModel settingModel;
        MobileOptionModel optionModel;

        List<IDialog> ListUi = new List<IDialog>();

        Skill_Table skill_Table;
        Buff_Table buff_Table;

        Schedule tempSelectSchedule;
        Schedule tempChangeSchedule;
        int tempSelectIndex;
        GameObject BackGround;
        ScheduleStage_Controller stage_Controller;

        List<Coroutine> ListCoroutine = new List<Coroutine>();
        //Coroutine btnEvent;

        #region Contents Load
        protected override void OnLoadStart()
        {
            scheduleModel = Model.First<ScheduleModel>();
            playerInventoryModel = Model.First<PlayerInventoryModel>();
            playerStatusModel = Model.First<PlayerStatusModel>();
            settingModel = Model.First<SettingModel>();
            optionModel = Model.First<MobileOptionModel>();

            skill_Table = TableManager.Instance.GetTableClass<Skill_Table>();
            buff_Table = TableManager.Instance.GetTableClass<Buff_Table>();

            StartCoroutine(LoadInitialData());
        }

        IEnumerator LoadInitialData()
        {
            string path = "Object/Stage/BackGround";
            yield return StartCoroutine(ResourceLoader.Instance.Load<GameObject>(path,
               o =>
               {
                   var inGameObject = Instantiate(o) as GameObject;
                   BackGround = inGameObject;
                   stage_Controller = BackGround.GetComponent<ScheduleStage_Controller>();
               }));

            SetLoadComplete();
        }
        #endregion

        protected override void OnLoadComplete()
        {
            foreach (var o in this.gameObject.GetComponent<IContentUILoader>()._uiList)
            {
                ListUi.Add(UIManager.Instance.Get(o).GetComponent<IDialog>());
            }
        }

        protected override void OnEnter()
        {
            Debug.Log(TAG + "OnEnter");
            ListCoroutine.Add(StartCoroutine(ButtonInput()));
            AddMessage();
            IContent.RequestContentExit<MenuContent>();
            BackGround.SetActive(true);
            UI.IDialog.RequestDialogEnter<UI.AloneGameMainDialog>();

            CharacterAnimaionPlay();
            stage_Controller.SetStage(scheduleModel.DicSchedule[0].schedule, playerInventoryModel.NowCharacter, optionModel.MobileOption.gameVolume);
        }

        IEnumerator ButtonInput()
        {
            while (true)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (ListUi[10].isActive)
                        ListUi[10].ExitDialog();
                    else if (ListUi[2].isActive)
                        ListUi[2].ExitDialog();
                    else if (ListUi[3].isActive)
                        ListUi[3].ExitDialog();
                    else if (ListUi[5].isActive)
                        ListUi[5].ExitDialog();
                    else if (ListUi[8].isActive)
                        ListUi[8].ExitDialog();
                    else if (ListUi[0].isActive || ListUi[6].isActive || ListUi[9].isActive)
                    {
                        AlongGmaeRunMenu(new AloneGameRunMenuMsg(AloneGameMenu.MainGame));
                    }
                    else
                    {
                        Debug.Log(TAG + " : Escape");
                        IContent.RequestContentEnter<MenuContent>();
                    }
                }
            }
        }

        void CharacterAnimaionPlay()
        {
            Schedule nowSchedule = scheduleModel.DicSchedule[0].schedule;
            AnimationType animationType;
            if (nowSchedule == Schedule.Vocal)
                animationType = AnimationType.Vocal;
            else if (nowSchedule == Schedule.Dance)
                animationType = AnimationType.Dance;
            else if (nowSchedule == Schedule.Entertainment)
                animationType = AnimationType.Entertainment;
            else if (nowSchedule == Schedule.Intelligence)
                animationType = AnimationType.Intelligence;
            else if (nowSchedule == Schedule.Meal)
                animationType = AnimationType.Meal;
            else
                animationType = AnimationType.Rest;

            Message.Send<SetCharacterAnimationMsg>(new SetCharacterAnimationMsg(animationType, false));
        }

        //완료된 스케줄 카운터
        int CompleteShceduleCnt()
        {
            System.DateTime nowTime = System.Convert.ToDateTime(System.DateTime.Now);
            System.DateTime firstScheduleTime = System.Convert.ToDateTime(scheduleModel.DicSchedule[0].dateTime);
            System.TimeSpan spanTime = nowTime - firstScheduleTime;
            double spanDay = spanTime.Days;
            double spanHour = spanTime.Hours;
            double spanMinute = spanTime.Minutes;
            double spanSecond = spanTime.Seconds;

            double spanTimeCheck = (spanDay * 24 * 60 * 60) + (spanHour * 60 * 60) + (spanTime.Minutes * 60) + spanSecond;
            if ((int)System.Math.Truncate(spanTimeCheck / settingModel.CompleteSpanTime) > 0)
                return (int)System.Math.Truncate(spanTimeCheck / settingModel.CompleteSpanTime);
            else
                return 0;
        }

        void ScheduleComplete(int cnt)
        {
            int upStatus;
            System.DateTime nowTime = System.Convert.ToDateTime(System.DateTime.Now);

            for (int i = 0; i < cnt; i++)
            {
                upStatus = UnityEngine.Random.Range(1, 10);
                if (scheduleModel.DicSchedule[0].schedule == Schedule.Vocal)
                {
                    playerStatusModel.VocalGage += upStatus;
                    if (playerStatusModel.VocalGage > 100)
                    {
                        playerStatusModel.Vocal += playerStatusModel.VocalGage / 100;
                        playerStatusModel.VocalGage %= 100;
                    }
                }
                else if (scheduleModel.DicSchedule[0].schedule == Schedule.Dance)
                {
                    playerStatusModel.DanceGage += upStatus;
                    if (playerStatusModel.DanceGage > 100)
                    {
                        playerStatusModel.Dance += playerStatusModel.DanceGage / 100;
                        playerStatusModel.DanceGage %= 100;
                    }
                }
                else if (scheduleModel.DicSchedule[0].schedule == Schedule.Entertainment)
                {
                    playerStatusModel.EnterTainmentGage += upStatus;
                    if (playerStatusModel.EnterTainmentGage > 100)
                    {
                        playerStatusModel.Entertainment += playerStatusModel.EnterTainmentGage / 100;
                        playerStatusModel.EnterTainmentGage %= 100;
                    }
                }
                else if (scheduleModel.DicSchedule[0].schedule == Schedule.Intelligence)
                {
                    playerStatusModel.IntelligenceGage += upStatus;
                    if (playerStatusModel.IntelligenceGage > 100)
                    {
                        playerStatusModel.Intelligence += playerStatusModel.IntelligenceGage / 100;
                        playerStatusModel.IntelligenceGage %= 100;
                    }
                }
                playerStatusModel.Potential += upStatus;

                scheduleModel.DicSchedule.Remove(0);

                for (int j = 0; j < scheduleModel.DicSchedule.Count; j++)
                {
                    string temp_DateTime = scheduleModel.DicSchedule[j + 1].dateTime;
                    int tempIndex = scheduleModel.DicSchedule[j + 1].index;
                    Schedule tempSchedule = scheduleModel.DicSchedule[j + 1].schedule;
                    scheduleModel.DicSchedule.Remove(j + 1);

                    AloneGameSchedule tempAlongGame = new AloneGameSchedule();
                    tempAlongGame.dateTime = temp_DateTime;
                    tempAlongGame.index = tempIndex;
                    tempAlongGame.schedule = tempSchedule;
                    scheduleModel.DicSchedule.Add(j, tempAlongGame);
                }

                // 끝에 신규 스케줄 생성
                System.DateTime startTime = Convert.ToDateTime(scheduleModel.DicSchedule[scheduleModel.DicSchedule.Count - 1].dateTime).AddSeconds(settingModel.CompleteSpanTime);
                int index = scheduleModel.DicSchedule[scheduleModel.DicSchedule.Count - 1].index + 1;

                Schedule tempNewSchedule;
                if (index % 12 == 1 || index % 12 == 5)
                {
                    tempNewSchedule = Schedule.Meal;
                }
                else if (index % 12 == 9 || index % 12 == 10)
                {
                    tempNewSchedule = Schedule.Rest;
                }
                else
                {
                    tempNewSchedule = (Schedule)UnityEngine.Random.Range(0, 4);
                }

                AloneGameSchedule tempNewAlonGameSchedule = new AloneGameSchedule();
                tempNewAlonGameSchedule.dateTime = startTime.ToString();
                tempNewAlonGameSchedule.index = index;
                tempNewAlonGameSchedule.schedule = tempNewSchedule;
                scheduleModel.DicSchedule.Add(scheduleModel.DicSchedule.Count, tempNewAlonGameSchedule);
            }

            stage_Controller.SetStage(scheduleModel.DicSchedule[0].schedule, playerInventoryModel.NowCharacter, optionModel.MobileOption.gameVolume);
            CharacterAnimaionPlay();

            Message.Send<AloneGameMainScheduleSettingMsg>(new AloneGameMainScheduleSettingMsg(scheduleModel.DicSchedule[0].schedule,
              scheduleModel.DicSchedule[1].schedule,
              Convert.ToDateTime(scheduleModel.DicSchedule[0].dateTime),
              settingModel.CompleteSpanTime));

            Message.Send<AloneGameScheduleInfoMsg>(new AloneGameScheduleInfoMsg(scheduleModel.DicSchedule));
        }

        //회복된 매니지먼트 카운트 체크  
        void ManageMentRecoveryCheck()
        {
            if (playerStatusModel.ManageMentCount == settingModel.ManageMentTotalCount)
            {
                Message.Send<AloneGameScheduleManageMestSetMsg>(new AloneGameScheduleManageMestSetMsg(false, playerStatusModel.ManageMentCount,
                    settingModel.ManagementRecoverTime, playerStatusModel.ManageMentUseTime
                    ));
                return;
            }

            System.DateTime nowTime = System.Convert.ToDateTime(System.DateTime.Now);
            System.DateTime firstManagementTime = System.Convert.ToDateTime(DateTime.Parse(playerStatusModel.ManageMentUseTime));
            System.TimeSpan spanTime = nowTime - firstManagementTime;

            double spanDay = spanTime.Days;
            double spanHour = spanTime.Hours;
            double spanMinute = spanTime.Minutes;
            double spanSecond = spanTime.Seconds;

            System.TimeSpan completeTime = firstManagementTime.AddSeconds(settingModel.ManagementRecoverTime) - nowTime;

            double spanTimeCheck = (spanDay * 24 * 60 * 60) + (spanHour * 60 * 60) + (spanTime.Minutes * 60) + spanSecond;

            int recoverCnt = (int)System.Math.Truncate(spanTimeCheck / settingModel.ManagementRecoverTime);
            int remainderTime = (int)System.Math.Truncate(spanTimeCheck % settingModel.ManagementRecoverTime);
            bool tempTimerStart;
            if (recoverCnt >= 1)
            {
                playerStatusModel.ManageMentCount += recoverCnt;

                if (playerStatusModel.ManageMentCount >= settingModel.ManageMentTotalCount)
                {
                    playerStatusModel.ManageMentCount = settingModel.ManageMentTotalCount;
                    tempTimerStart = false;
                }
                else
                {
                    tempTimerStart = true;
                }
            }
            else
            {
                tempTimerStart = true;
            }

            Message.Send<AloneGameScheduleManageMestSetMsg>(new AloneGameScheduleManageMestSetMsg(tempTimerStart, playerStatusModel.ManageMentCount,
                 settingModel.ManagementRecoverTime, playerStatusModel.ManageMentUseTime
                 ));

        }

        private void AddMessage()
        {
            Message.AddListener<AloneGameTopBarSettingRequestMsg>(AloneGameTopBarSettingRequest);
            Message.AddListener<AloneGameRunMenuMsg>(AlongGmaeRunMenu);
            Message.AddListener<AloneGameSkillorBuffSelectMsg>(AloneGameSkillorBuffSelect);
            Message.AddListener<AloneGmaeScheduleMsg>(AloneGmaeSchedule);
            Message.AddListener<AloneGameManageMentRecoveryMsg>(AloneGameManageMentRecovery);
            Message.AddListener<ScheduleItemSelectMsg>(ScheduleItemSelect);
            Message.AddListener<AloneGameScheduleChangeItemClickMsg>(AloneGameScheduleChangeItemClick);
            Message.AddListener<AloneGameScheduleChangeAgreeMsg>(AloneGameScheduleChangeAgree);
            Message.AddListener<AloneGameScheduleCompleteMsg>(AloneGameScheduleComplete);
            Message.AddListener<AloneGameScheduleChangeCloseMsg>(AloneGameScheduleChangeClose);
            Message.AddListener<AlonGameItemBuyMsg>(AloneGameItemBuy);
            Message.AddListener<ItmeIsBuyDialogMsg>(ItemIsBuyDialog);
        }

        private void AloneGameTopBarSettingRequest(AloneGameTopBarSettingRequestMsg msg)
        {
            if (CompleteShceduleCnt() > 0)
            {
                ScheduleComplete(CompleteShceduleCnt());
            }
            else
            {
                Message.Send<AloneGameMainScheduleSettingMsg>(new AloneGameMainScheduleSettingMsg(scheduleModel.DicSchedule[0].schedule,
                scheduleModel.DicSchedule[1].schedule,
                Convert.ToDateTime(scheduleModel.DicSchedule[0].dateTime),
                settingModel.CompleteSpanTime));
            }
        }

        private void AlongGmaeRunMenu(AloneGameRunMenuMsg msg)
        {
            SoundManager.Instance.PlaySound((int)SoundType.EFFECT_BTN_DOWN, optionModel.MobileOption.effectVolume);
            DialogAllClose();
            if (msg.aloneGameMenu == AloneGameMenu.MainGame)
            {
                UI.IDialog.RequestDialogEnter<UI.AloneGameMainDialog>();
                CharacterAnimaionPlay();
                BackGround.SetActive(true);
            }
            else if (msg.aloneGameMenu == AloneGameMenu.Avatar)
            {
                UI.IDialog.RequestDialogEnter<UI.AloneGameAvatarDialog>();
                Message.Send<AvatarStatusMsg>(new AvatarStatusMsg(playerInventoryModel.NowCharacter, playerStatusModel.CharacterStatus));
                Message.Send<SetCharacterAnimationMsg>(new SetCharacterAnimationMsg(AnimationType.Idel1, false));
                BackGround.SetActive(false);
            }
            else if (msg.aloneGameMenu == AloneGameMenu.Upgrade)
                UI.IDialog.RequestDialogEnter<UI.AloneGameSkillandBuffDialog>();
            else if (msg.aloneGameMenu == AloneGameMenu.Option)
                UI.IDialog.RequestDialogEnter<UI.AloneGameOption>();
        }

        private void AloneGameSkillorBuffSelect(AloneGameSkillorBuffSelectMsg msg)
        {
            SoundManager.Instance.PlaySound((int)SoundType.EFFECT_BTN_DOWN, optionModel.MobileOption.effectVolume);
            UI.IDialog.RequestDialogExit<UI.AloneGameSkillDialog>();
            UI.IDialog.RequestDialogExit<UI.AloneGameBuffDialog>();
            if (msg.upgrade == Upgrade.Skill)
            {
                UI.IDialog.RequestDialogEnter<UI.AloneGameSkillDialog>();
                List<int> tempSkillLv = new List<int>();
                tempSkillLv.Add(playerStatusModel.VocalSkill);
                tempSkillLv.Add(playerStatusModel.DanceSkill);
                tempSkillLv.Add(playerStatusModel.EntertainmentSkill);
                tempSkillLv.Add(playerStatusModel.IntelligenceSkill);
                tempSkillLv.Add(playerStatusModel.RelaxSkill);
                tempSkillLv.Add(playerStatusModel.SelfManagementSkill);
                Message.Send<SetSkillScrollMsg>(new SetSkillScrollMsg(skill_Table, tempSkillLv, playerStatusModel.Potential));
            }
            else if (msg.upgrade == Upgrade.Buff)
            {
                UI.IDialog.RequestDialogEnter<UI.AloneGameBuffDialog>();
                Message.Send<SetBuffScrolleMsg>(new SetBuffScrolleMsg(buff_Table));
            }
        }

        private void AloneGmaeSchedule(AloneGmaeScheduleMsg msg)
        {
            if (msg.isOpen)
            {
                SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Open, optionModel.MobileOption.effectVolume);
                //스케줄 정보 전달
                UI.IDialog.RequestDialogEnter<UI.AloneGameScheduleDialog>();

                ManageMentRecoveryCheck();
                Message.Send<AloneGameScheduleInfoMsg>(new AloneGameScheduleInfoMsg(scheduleModel.DicSchedule));
            }
            else
            {
                SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Close, optionModel.MobileOption.effectVolume);
                UI.IDialog.RequestDialogExit<UI.AloneGameScheduleDialog>();
            }
        }

        private void AloneGameManageMentRecovery(AloneGameManageMentRecoveryMsg msg)
        {
            playerStatusModel.ManageMentCount += 1;
            playerStatusModel.ManageMentUseTime = DateTime.Now.ToString();


            ManageMentRecoveryCheck();
        }

        private void ScheduleItemSelect(ScheduleItemSelectMsg msg)
        {
            if (playerStatusModel.ManageMentCount < 1)
            {
                AndroidTrasferMgr.Instance.ShowToast("매니지먼트가 부족합니다.");
                return;
            }

            SoundManager.Instance.PlaySound((int)SoundType.EFFECT_BTN_DOWN, optionModel.MobileOption.effectVolume);
            UI.IDialog.RequestDialogEnter<UI.AloneGameScheduleChangeDialog>();
            tempSelectIndex = msg.itemIndex;
            tempSelectSchedule = scheduleModel.DicSchedule[tempSelectIndex].schedule;
            Message.Send<AloneGameScheduleChangeDialogSetMsg>(new AloneGameScheduleChangeDialogSetMsg(tempSelectSchedule));
        }

        private void AloneGameScheduleChangeItemClick(AloneGameScheduleChangeItemClickMsg msg)
        {
            SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Open, optionModel.MobileOption.effectVolume);
            UI.IDialog.RequestDialogEnter<UI.AloneGameScheduleChangeOKDialog>();
            tempChangeSchedule = msg.schedule;
            Message.Send<AloneGameScheduleChangeAgreeDialogMsg>(new AloneGameScheduleChangeAgreeDialogMsg(tempSelectSchedule, msg.schedule));
        }

        private void AloneGameScheduleChangeAgree(AloneGameScheduleChangeAgreeMsg msg)
        {
            if (msg.isOK)
            {
                //스케줄 변경 동의
                SoundManager.Instance.PlaySound((int)SoundType.EFFECT_BTN_DOWN, optionModel.MobileOption.effectVolume);
                ScheduleChange(tempSelectIndex, tempChangeSchedule);
            }
            else
            {
                SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Close, optionModel.MobileOption.effectVolume);
            }
            UI.IDialog.RequestDialogExit<UI.AloneGameScheduleChangeOKDialog>();
            UI.IDialog.RequestDialogExit<UI.AloneGameScheduleChangeDialog>();
        }

        void ScheduleChange(int key, Schedule changeSchedule)
        {
            string temp_DateTime = scheduleModel.DicSchedule[key].dateTime;
            int tempIndex = scheduleModel.DicSchedule[key].index;
            scheduleModel.DicSchedule.Remove(key);

            AloneGameSchedule tempAlongGame = new AloneGameSchedule();
            tempAlongGame.dateTime = temp_DateTime;
            tempAlongGame.index = tempIndex;
            tempAlongGame.schedule = changeSchedule;
            scheduleModel.DicSchedule.Add(key, tempAlongGame);

            if (playerStatusModel.ManageMentCount == settingModel.ManageMentTotalCount)
                playerStatusModel.ManageMentUseTime = DateTime.Now.ToString();

            playerStatusModel.ManageMentCount -= 1;

            Message.Send<AloneGameScheduleInfoMsg>(new AloneGameScheduleInfoMsg(scheduleModel.DicSchedule));
            Message.Send<AloneGameTopBarSettingRequestMsg>(new AloneGameTopBarSettingRequestMsg());
            stage_Controller.SetStage(scheduleModel.DicSchedule[0].schedule, playerInventoryModel.NowCharacter, optionModel.MobileOption.gameVolume);
            CharacterAnimaionPlay();
            ManageMentRecoveryCheck();
        }

        private void AloneGameScheduleComplete(AloneGameScheduleCompleteMsg msg)
        {
            ScheduleComplete(CompleteShceduleCnt());
        }

        private void AloneGameScheduleChangeClose(AloneGameScheduleChangeCloseMsg msg)
        {
            SoundManager.Instance.PlaySound((int)SoundType.EFFECT_Clothes_Change, optionModel.MobileOption.effectVolume);
            UI.IDialog.RequestDialogExit<UI.AloneGameScheduleChangeDialog>();
        }

        private void AloneGameItemBuy(AlonGameItemBuyMsg msg)
        {
            if (msg.upgrade == Upgrade.Skill)
            {
                UI.IDialog.RequestDialogEnter<UI.AloneGameSkillUpgradeDialog>();

                int tempPrice;
                if (msg.index == 0)
                    tempPrice = skill_Table.sheets[0].list[msg.index].price * (playerStatusModel.VocalSkill + 1);
                else if (msg.index == 1)
                    tempPrice = skill_Table.sheets[0].list[msg.index].price * (playerStatusModel.DanceSkill + 1);
                else if (msg.index == 2)
                    tempPrice = skill_Table.sheets[0].list[msg.index].price * (playerStatusModel.EntertainmentSkill + 1);
                else if (msg.index == 3)
                    tempPrice = skill_Table.sheets[0].list[msg.index].price * (playerStatusModel.IntelligenceSkill + 1);
                else if (msg.index == 4)
                    tempPrice = skill_Table.sheets[0].list[msg.index].price * (playerStatusModel.RelaxSkill + 1);
                else
                    tempPrice = skill_Table.sheets[0].list[msg.index].price * (playerStatusModel.SelfManagementSkill + 1);

                bool tempIsBuyPossible;
                if (playerStatusModel.Potential >= tempPrice)
                    tempIsBuyPossible = true;
                else
                    tempIsBuyPossible = false;

                Message.Send<BuyDialogSetMsg>(new BuyDialogSetMsg(skill_Table.sheets[0].list[msg.index].path, tempPrice, msg.index, tempIsBuyPossible));
            }
            else if (msg.upgrade == Upgrade.Buff)
            {
                UI.IDialog.RequestDialogEnter<UI.AloneGameBuffBuyDialog>();

                bool tempIsBuyPossible;
                if (playerStatusModel.Coin >= buff_Table.sheets[0].list[msg.index].price)
                    tempIsBuyPossible = true;
                else
                    tempIsBuyPossible = false;

                Message.Send<BuyDialogSetMsg>(new BuyDialogSetMsg(buff_Table.sheets[0].list[msg.index].path, buff_Table.sheets[0].list[msg.index].price, msg.index, tempIsBuyPossible));
            }
        }

        private void ItemIsBuyDialog(ItmeIsBuyDialogMsg msg)
        {
            if (msg.upgrade == Upgrade.Skill)
            {
                
                UI.IDialog.RequestDialogExit<UI.AloneGameSkillUpgradeDialog>();
                if (msg.isBuy)
                {
                    SoundManager.Instance.PlaySound((int)SoundType.EFFECT_BTN_DOWN, optionModel.MobileOption.effectVolume);
                    if (msg.index == 0)
                        playerStatusModel.VocalSkill += 1;
                    else if (msg.index == 1)
                        playerStatusModel.DanceSkill += 1;
                    else if (msg.index == 2)
                        playerStatusModel.EntertainmentSkill += 1;
                    else if (msg.index == 3)
                        playerStatusModel.IntelligenceSkill += 1;
                    else if (msg.index == 4)
                        playerStatusModel.RelaxSkill += 1;
                    else if (msg.index == 5)
                        playerStatusModel.SelfManagementSkill += 1;

                    playerStatusModel.Potential -= msg.price;
                    AloneGameSkillorBuffSelect(new AloneGameSkillorBuffSelectMsg(Upgrade.Skill));
                }
                else
                {
                    SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Close, optionModel.MobileOption.effectVolume);
                }
            }
            else if (msg.upgrade == Upgrade.Buff)
            {
                
                UI.IDialog.RequestDialogExit<UI.AloneGameBuffBuyDialog>();
                if (msg.isBuy)
                {
                    SoundManager.Instance.PlaySound((int)SoundType.EFFECT_BTN_DOWN, optionModel.MobileOption.effectVolume);
                }
                else
                {
                    SoundManager.Instance.PlaySound((int)SoundType.EFFCT_MENU_Close, optionModel.MobileOption.effectVolume);
                }
            }
        }

        protected override void OnExit()
        {
            foreach (var o in ListCoroutine)
            {
                if (o != null)
                    StopCoroutine(o);
            }

            SoundManager.Instance.StopAllSound();
            ListCoroutine.Clear();
            BackGround.SetActive(false);
            DialogAllClose();
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<AloneGameTopBarSettingRequestMsg>(AloneGameTopBarSettingRequest);
            Message.RemoveListener<AloneGameRunMenuMsg>(AlongGmaeRunMenu);
            Message.RemoveListener<AloneGameSkillorBuffSelectMsg>(AloneGameSkillorBuffSelect);
            Message.RemoveListener<AloneGmaeScheduleMsg>(AloneGmaeSchedule);
            Message.RemoveListener<AloneGameManageMentRecoveryMsg>(AloneGameManageMentRecovery);
            Message.RemoveListener<ScheduleItemSelectMsg>(ScheduleItemSelect);
            Message.RemoveListener<AloneGameScheduleChangeItemClickMsg>(AloneGameScheduleChangeItemClick);
            Message.RemoveListener<AloneGameScheduleChangeAgreeMsg>(AloneGameScheduleChangeAgree);
            Message.RemoveListener<AloneGameScheduleCompleteMsg>(AloneGameScheduleComplete);
            Message.RemoveListener<AloneGameScheduleChangeCloseMsg>(AloneGameScheduleChangeClose);
            Message.RemoveListener<AlonGameItemBuyMsg>(AloneGameItemBuy);
            Message.RemoveListener<ItmeIsBuyDialogMsg>(ItemIsBuyDialog);
        }
    }
}