using CellBig.UI.Event;
using CellBig.Android;
using CellBig.Constants;
using UnityEngine.UI;
using System;

namespace CellBig.UI
{
    public class SettingMainDialog : IDialog
    {
        public ToggleButton bluetooth;
        public ToggleButton Schedule;
        public ToggleButton TTS;
        public ToggleButton SMS;
        public Slider sliderGameVolume;
        public Slider sliderEffectVolume;
        public Button btnHide;

        protected override void OnLoad()
        {
            sliderGameVolume.onValueChanged.AddListener(delegate { VolumeChange(true); });
            sliderEffectVolume.onValueChanged.AddListener(delegate { VolumeChange(false); });
            btnHide.onClick.AddListener(() => AndroidTrasferMgr.Instance.ShowToast("홀로스타와 페어링이 필요합니다."));
        }

        private void VolumeChange(bool isGame)
        {
            float volume;
            if (isGame)
                volume = sliderGameVolume.value;
            else
                volume = sliderEffectVolume.value;

            Message.Send<SettingVolumeMsg>(new SettingVolumeMsg(volume, isGame)); 
        }

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<SetOptionVolumeMsg>(SetOptionVolume);
            Message.AddListener<SettingDialogSetMsg>(SettingDialogSet);
        }

        private void SetOptionVolume(SetOptionVolumeMsg msg)
        {
            sliderGameVolume.value = msg.mobileOption.gameVolume;
            sliderEffectVolume.value = msg.mobileOption.effectVolume;
        }

        private void SettingDialogSet(SettingDialogSetMsg msg)
        {
            bluetooth.SetToggle(msg.isBluetoothON);
            Schedule.SetToggle(msg.holoOptionSetting.isSchedule);
            TTS.SetToggle(msg.holoOptionSetting.isTTSReceive);
            SMS.SetToggle(msg.holoOptionSetting.isMMSReceive);

            //sliderGameVolume.value = msg.mobileOption.gameVolume;
            //sliderEffectVolume.value = msg.mobileOption.effectVolume;
            

            if (msg.isBluetoothConnet)
            {
                btnHide.gameObject.SetActive(false);
            }
            else
            {
                btnHide.gameObject.SetActive(true);
            }

            Message.Send<SettingSendTabletMsg>(new SettingSendTabletMsg());
        }

        protected override void OnExit()
        {
            RemoverMessage();
        }

        private void RemoverMessage()
        {
            Message.RemoveListener<SetOptionVolumeMsg>(SetOptionVolume);
            Message.RemoveListener<SettingDialogSetMsg>(SettingDialogSet);
        }
    }
}