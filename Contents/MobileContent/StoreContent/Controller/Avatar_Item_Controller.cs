using JHchoi.UI.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JHchoi.UI
{
    public class Avatar_Item_Controller : MonoBehaviour
    {
        public Button btnAvatar;
        public RawImage imgCharacter;
        //int cost;

        public void InitButton(int index, string path, int cost, bool isHave)
        {
            btnAvatar.onClick.AddListener(() => Message.Send<StoreAvatarItemSelectMsg>(new StoreAvatarItemSelectMsg(index, cost)));
            imgCharacter.texture = Resources.Load<Texture>(path);
            imgCharacter.color = Color.black;
            if (isHave)
                imgCharacter.color = Color.white;
            else
                imgCharacter.color = Color.black;
            //  this.cost = cost;
        }
    }
}