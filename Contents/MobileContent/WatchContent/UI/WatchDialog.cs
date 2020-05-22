using CellBig.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using CellBig.Constants;
using CellBig.Android;

namespace CellBig.UI
{
    public class WatchDialog : IDialog
    {
        public Weather_Controller weather_Controller;
        public Text txtHour;
        public Text txtMinute;
        public Text txtPm;
        public Text txtDay;
        public RawImage imgCharacter;

        protected override void OnEnter()
        {
            StartCoroutine(Time());
            AddMessage();
        }

        IEnumerator Time()
        {
            while (true)
            {
                txtHour.text = DateTime.Now.ToString("hh");
                txtMinute.text = DateTime.Now.ToString("mm");
                txtPm.text = DateTime.Now.ToString("tt");
                CultureInfo cultures = CultureInfo.CreateSpecificCulture("ko-KR");
                txtDay.text = DateTime.Now.ToString("MM월 dd일 ddd요일", cultures);
                yield return null;
            }
        }

        private void AddMessage()
        {
            Message.AddListener<SetWatchCharacterMsg>(SetWatchCharacter);
            Message.AddListener<WeatherMsg>(Weather);
            Message.AddListener<AddressMsg>(Address);
        }

        private void SetWatchCharacter(SetWatchCharacterMsg msg)
        {
            string path = "UIImage/Character/";
            if (msg.character == Character.Boy)
            {
                path = path + "BoyRtt";
            }
            else
            {
                path = path + "GirlRtt";
            }
            imgCharacter.GetComponent<RawImage>().texture = Resources.Load(path) as Texture;
        }

        private void Weather(WeatherMsg msg)
        {
            weather_Controller.InitWeatehr(msg.weathers, msg.degree.ToString());
        }

        private void Address(AddressMsg msg)
        {
            weather_Controller.InitLocation(msg.addr1, msg.addr2);
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<WeatherMsg>(Weather);
            Message.RemoveListener<AddressMsg>(Address);
            Message.RemoveListener<SetWatchCharacterMsg>(SetWatchCharacter);
        }
    }
}
