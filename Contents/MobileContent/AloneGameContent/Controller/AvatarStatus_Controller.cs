using CellBig.Contents;
using CellBig.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CellBig.UI
{
    public class AvatarStatus_Controller : MonoBehaviour
    {
        public Text txtVocal;
        public Text txtDance;
        public Text txtEntertainment;
        public Text txtIntelligence;
        public Text txtPotential;
        public Text txtCoin;

        public Image imgVocal;
        public Image imgDance;
        public Image imgEntertainment;
        public Image imgIntelligence;

        public void InitStatis(PlayerStatus playerStatus)
        {
            txtVocal.text = playerStatus.vocal.ToString();
            txtDance.text = playerStatus.dance.ToString();
            txtEntertainment.text = playerStatus.entertainment.ToString();
            txtIntelligence.text = playerStatus.intelligence.ToString();
            txtPotential.text = playerStatus.potential.ToString();
            txtCoin.text = playerStatus.coin.ToString();

            imgVocal.fillAmount = playerStatus.vocalGage * 0.01f;
            imgDance.fillAmount = playerStatus.danceGage * 0.01f;
            imgEntertainment.fillAmount = playerStatus.entertainmentGage * 0.01f;
            imgIntelligence.fillAmount = playerStatus.intelligenceGage * 0.01f;
        }
    }
}