using CellBig.Contents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellBig.Models
{
    public class PlayerStatusModel : Model
    {
        GameModel _owner;

        public void Setup(GameModel owner)
        {
            _owner = owner;
        }

        PlayerStatus characterStatus;

        public PlayerStatus CharacterStatus { get => characterStatus; set => characterStatus = value; }
        public int Vocal { get => characterStatus.vocal; set => characterStatus.vocal = value; }
        public int VocalGage { get => characterStatus.vocalGage; set => characterStatus.vocalGage = value; }
        public int VocalSkill { get => characterStatus.vocalSkill; set => characterStatus.vocalSkill = value; }
        public int Dance { get => characterStatus.dance; set => characterStatus.dance = value; }
        public int DanceGage { get => characterStatus.danceGage; set => characterStatus.danceGage = value; }
        public int DanceSkill { get => characterStatus.danceSkill; set => characterStatus.danceSkill = value; }
        public int Entertainment { get => characterStatus.entertainment; set => characterStatus.entertainment = value; }
        public int EnterTainmentGage { get => characterStatus.entertainmentGage; set => characterStatus.entertainmentGage = value; }
        public int EntertainmentSkill { get => characterStatus.entertainmentSkill; set => characterStatus.entertainmentSkill = value; }
        public int Intelligence { get => characterStatus.intelligence; set => characterStatus.intelligence = value; }
        public int IntelligenceGage { get => characterStatus.intelligenceGage; set => characterStatus.intelligenceGage = value; }
        public int IntelligenceSkill { get => characterStatus.intelligenceSkill; set => characterStatus.intelligenceSkill = value; }
        public int Coin { get => characterStatus.coin; set => characterStatus.coin = value; }
        public int Potential { get => characterStatus.potential; set => characterStatus.potential = value; }
        public int RelaxSkill { get => characterStatus.relaxSkill; set => characterStatus.relaxSkill = value; }
        public int SelfManagementSkill { get => characterStatus.selfManagementSkill; set => characterStatus.selfManagementSkill = value; }
        public int ManageMentCount { get => characterStatus.manageMentCout; set => characterStatus.manageMentCout = value; }
        public string ManageMentUseTime { get => characterStatus.manageMentUseTime; set => characterStatus.manageMentUseTime = value; }
    }
}