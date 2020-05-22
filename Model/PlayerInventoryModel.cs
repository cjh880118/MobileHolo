using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Constants;
using JHchoi.Contents;
using System.Linq;

namespace JHchoi.Models
{
    public class PlayerInventoryModel : Model
    {
        GameModel _owner;

        public void Setup(GameModel owner)
        {
            _owner = owner;
        }

        PlayerInventory playerInventory;
        List<int> ListBuyCharacter = new List<int>();
        List<int> ListBuySkin = new List<int>();

        public PlayerInventory PlayerInventory { get => playerInventory; set => playerInventory = value; }
        public Character NowCharacter { get => playerInventory.nowCharacter; set => playerInventory.nowCharacter = value; }
        public int NowSkin { get => playerInventory.nowSkin; set => playerInventory.nowSkin = value; }
        public int[] ArrayBuyCharacter
        {
            get => playerInventory.buyCharacter;
            set
            {
                playerInventory.buyCharacter = value;
                ListBuyCharacter = value.ToList();
            }
        }
        public int[] ArrayBuySkinNum
        {
            get => playerInventory.buySkinNum;
            set
            {
                playerInventory.buySkinNum = value;
                ListBuySkin = value.ToList();
            }
        }

        public bool IsHaveCharacter(int index)
        {
            return ListBuyCharacter.Contains(index);
        }

        public bool IsHaveSkin(int index)
        {
            return ListBuySkin.Contains(index);
        }

        public void BuyCharacter(int index)
        {
            ListBuyCharacter = playerInventory.buyCharacter.ToList();
            ListBuyCharacter.Add(index);
            playerInventory.buyCharacter = ListBuyCharacter.ToArray();
        }

        public void BuySkin(int index)
        {
            ListBuySkin = playerInventory.buySkinNum.ToList();
            ListBuySkin.Add(index);
            playerInventory.buySkinNum = ListBuySkin.ToArray();
        }
    }
}