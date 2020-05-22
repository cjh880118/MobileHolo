using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CellBig.Constants;


namespace CellBig.UI.Event
{
    public class WatchCharacterMsg : Message
    {
        public Character character;
        public WatchCharacterMsg(Character character)
        {
            this.character = character;
        }
    }

    public class LocationMsg : Message
    {
        public string lat;
        public string lon;
        public LocationMsg(string lat, string lon)
        {
            this.lat = lat;
            this.lon = lon;
        }
    }

    public class WeatherMsg : Message
    {
        public Weathers weathers;
        public float degree;
        public WeatherMsg(Weathers weathers, float degree)
        {
            this.weathers = weathers;
            this.degree = degree;
        }
    }

    public class AddressMsg: Message
    {
        public string addr1;
        public string addr2;
        public AddressMsg(string addr1, string addr2)
        {
            this.addr1 = addr1;
            this.addr2 = addr2;
        }
    }
    
    public class SetWatchCharacterMsg : Message
    {
        public Character character;
        public SetWatchCharacterMsg(Character character)
        {
            this.character = character;
        }
    }
}