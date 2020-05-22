using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JHchoi.Constants;

namespace JHchoi.UI
{
    public class Weather_Controller : MonoBehaviour
    {
        public Text txtTemperature;
        public GameObject sun;
        public GameObject cloud;
        public GameObject rain;
        public GameObject snow;
        public Text txtWeather;
        public Text txtLocation;

        public void InitWeatehr(Weathers weathers, string degree)
        {
            sun.SetActive(false);
            cloud.SetActive(false);
            rain.SetActive(false);
            snow.SetActive(false);
            txtTemperature.text = (int)float.Parse(degree) + "˚";
            if (weathers == Weathers.Clean)
            {
                sun.SetActive(true);
                txtWeather.text = "맑음";
            }
            else if (weathers == Weathers.Cloud)
            {
                cloud.SetActive(true);
                txtWeather.text = "흐림";
            }
            else if (weathers == Weathers.Rain)
            {
                rain.SetActive(true);
                txtWeather.text = "비";
            }
            else if (weathers == Weathers.Snow)
            {
                snow.SetActive(true);
                txtWeather.text = "눈";
            }
        }

        public void InitLocation(string addr1, string addr2)
        {
            txtLocation.text = addr1 + " " + addr2;
        }
    }
}