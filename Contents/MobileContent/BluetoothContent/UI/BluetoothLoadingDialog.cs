using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CellBig.UI
{
    public class BluetoothLoadingDialog : IDialog
    {
        public RectTransform rectComponent;
        private float rotateSpeed = 500f;
        bool isLoadingStart;
        protected override void OnEnter()
        {
            isLoadingStart = true;
            StartCoroutine(Loading());
        }

        IEnumerator Loading()
        {
            while (isLoadingStart)
            {
                yield return null;
                rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
            }
        }

        protected override void OnExit()
        {
            isLoadingStart = false;
            StopCoroutine(Loading());
        }
    }
}