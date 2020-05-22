using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Android;

public class CameraTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AndroidTrasferMgr.Instance.ShowToast("있다");
    }
}
