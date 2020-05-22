using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Constants;

namespace JHchoi.UI.Event
{
    public class SetCharacterDressMsg : Message
    {
        public Character character;
        public int dressNum;

        public SetCharacterDressMsg(Character character, int dressNum)
        {
            this.character = character;
            this.dressNum = dressNum;
        }
    }

    public class SetCharacterAnimationMsg : Message
    {
        public bool isBluetoothCommand;
        public AnimationType animationType;
        public SetCharacterAnimationMsg(AnimationType animationType, bool isBluetoothCommand)
        {
            this.animationType = animationType;
            this.isBluetoothCommand = isBluetoothCommand;
        }
    }

    public class SetStoreCharacterMsg : Message
    {
        public bool isStoreEnter;
        public SetStoreCharacterMsg(bool isStoreEnter)
        {
            this.isStoreEnter = isStoreEnter;
        }
    }
}