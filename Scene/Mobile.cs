using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellBig.Scene
{
    public class Mobile : IScene
    {
        static string TAG = "Mobile :: ";
        protected override void OnLoadStart()
        {
            Debug.Log(TAG + "OnLoadStart");
        }

        protected override void OnLoadComplete()
        {
            Debug.Log(TAG + "OnLoadComplete");
        }

    }
}
