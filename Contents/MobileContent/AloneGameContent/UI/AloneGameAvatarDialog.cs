using JHchoi.Constants;
using JHchoi.UI.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class AloneGameAvatarDialog : IDialog
    {
        public RawImage ImgCharacter;
        public AvatarStatus_Controller avatarStatus_Controller;

        protected override void OnEnter()
        {
            AddMessage();
        }

        private void AddMessage()
        {
            Message.AddListener<AvatarStatusMsg>(AvatarStatus);
        }

        private void AvatarStatus(AvatarStatusMsg msg)
        {
            string path;
            if(msg.character == Character.Boy)
            {
                path = "UIImage/Character/BoyRtt";
            }
            else
            {
                path = "UIImage/Character/GirlRtt";
            }

            ImgCharacter.texture = Resources.Load<Texture>(path);
            avatarStatus_Controller.InitStatis(msg.playerStatus);
        }

        protected override void OnExit()
        {
            RemoveMessage();
        }

        private void RemoveMessage()
        {
            Message.RemoveListener<AvatarStatusMsg>(AvatarStatus);
        }
    }
}