using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellBig.UI.Event;
using CellBig.Android;
using System;
using CellBig.Constants;
using CellBig.Models;

namespace CellBig.Contents
{
    public class WatchContent : IContent
    {
        static string TAG = "WatchContent :: ";
        WeatherInfo weatherInfo;
        LocationInfo locationInfo;
        PlayerInventoryModel playerInventoryModel;
        Coroutine btnEvent;

        protected override void OnLoadStart()
        {
            playerInventoryModel = Model.First<PlayerInventoryModel>();
            SetLoadComplete();
        }

        protected override void OnEnter()
        {
            Debug.Log(TAG + "OnEnter");
            btnEvent = StartCoroutine(ButtonInput());
            AddMessage();
            IContent.RequestContentExit<MenuContent>();
            UI.IDialog.RequestDialogEnter<UI.WatchDialog>();
            Message.Send<SetWatchCharacterMsg>(new SetWatchCharacterMsg(playerInventoryModel.NowCharacter));
            AndroidTrasferMgr.Instance.GetLocation();
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
            Message.AddListener<LocationMsg>(Location);
        }

        private void Location(LocationMsg msg)
        {
            StartCoroutine(IGetWeather(msg.lat, msg.lon));
        }

        IEnumerator IGetWeather(string lat, string lon)
        {
            WWW www = new WWW("http://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&appid=ce4995f5d2e026bf6ac967963210ceb1&units=metric");
            yield return www;
            if (www.error != null)
            {
            }
            else if (www.isDone)
            {
                //AndroidTrasferMgr.Instance.ShowToast(www.text);
                Debug.Log(www.text);
                weatherInfo = JsonUtility.FromJson<WeatherInfo>(www.text);
                Weathers tempWeathers = Weathers.Clean;
                foreach (WeatherInfo.Weather weather in weatherInfo.weather)
                {
                    if (weather.id == 800)
                    {   //clear                    
                        //myWeather.SetWeatherInfo(Weathers.맑음);
                        tempWeathers = Weathers.Clean;
                        break;
                    }
                    else if (weather.id > 800 && weather.id > 805)
                    { //clouds
                      //myWeather.SetWeatherInfo(Weathers.흐림);
                        tempWeathers = Weathers.Cloud;
                        break;
                    }
                    else if (weather.id > 599 && weather.id < 623)
                    { //snowy
                      //myWeather.SetWeatherInfo(Weathers.눈);
                        tempWeathers = Weathers.Snow;
                        break;
                    }
                    else if (weather.id > 499 && weather.id < 532)
                    { //rainy
                      //myWeather.SetWeatherInfo(Weathers.비);
                        tempWeathers = Weathers.Rain;
                        break;
                    }
                    else if (weather.id > 299 && weather.id < 322)
                    { //drizzle
                      //myWeather.SetWeatherInfo(Weathers.비);
                        tempWeathers = Weathers.Rain;
                        break;
                    }
                    else if (weather.id > 199 && weather.id < 233)
                    { //thunder
                      //myWeather.SetWeatherInfo(Weathers.비);
                        tempWeathers = Weathers.Rain;
                        break;
                    }
                    else if (weather.id > 700 && weather.id < 782)
                    {//etcs
                     //myWeather.SetWeatherInfo(Weathers.흐림)
                        tempWeathers = Weathers.Cloud;
                        break;
                    }
                }
                Android.AndroidTrasferMgr.Instance.BluetoothSendMsg(lat + "&" + lon, SENDMSGTYPE.LOCATION);
                Message.Send<WeatherMsg>(new WeatherMsg(tempWeathers, weatherInfo.main.temp));
                StartCoroutine(IGetLocale(lat, lon));
            }
        }

        IEnumerator IGetLocale(string lat, string lon)
        {
            WWW www = new WWW("https://maps.googleapis.com/maps/api/geocode/json?address=" + lat + "," + lon + "&key=AIzaSyC55Xgl4YTGrnILi4jgqiXroEB4venvhzI&language=ko");
            yield return www;
            if (www.error != null)
            {

            }
            else if (www.isDone)
            {
                Debug.Log(www.text);
                locationInfo = JsonUtility.FromJson<LocationInfo>(www.text);
                try
                {
                    string split = " ";
                    string[] addr = locationInfo.results[0].formatted_address.Split(split.ToCharArray());
                    Message.Send<AddressMsg>(new AddressMsg(addr[1], addr[2]));
                    //myWeather.SetWeatherLocale(addr[1], addr[2]);
                }
                catch (System.Exception e)
                {
                    //Debug.Log(e.ToString);
                }
            }
        }

        protected override void OnExit()
        {
            Debug.Log(TAG + " OnExit");
            if (btnEvent != null)
                StopCoroutine(btnEvent);

            DialogAllClose();
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<LocationMsg>(Location);
        }
    }
}
