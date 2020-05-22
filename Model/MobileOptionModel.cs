using CellBig.Contents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellBig.Models
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