using JHchoi.Contents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Models
{
    public class MobileOptionModel : Model
    {
        GameModel _owner;

        public void Setup(GameModel owner)
        {
            _owner = owner;
        }

        MobileOption mobileOption;
        public MobileOption MobileOption { get => mobileOption; set => mobileOption = value; }
    }
}