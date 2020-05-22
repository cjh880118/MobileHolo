using UnityEngine;
using UnityEngine.UI;
using JHchoi.Constants;
using JHchoi.UI.Event;
using System;

public class ToggleButton : MonoBehaviour
{
    public Button btnON;
    public Button btnOFF;
    public OptionSet option;
    // Start is called before the first frame update
    void Start()
    {
        btnON.onClick.AddListener(() => Message.Send<SettingButtonSetMsg>(new SettingButtonSetMsg(option, false)));
        btnOFF.onClick.AddListener(() => Message.Send<SettingButtonSetMsg>(new SettingButtonSetMsg(option, true)));
    }

    public void SetToggle(bool isON)
    {
        if (isON)
        {
            btnOFF.gameObject.SetActive(false);
            btnON.gameObject.SetActive(true);
        }
        else
        {
            btnOFF.gameObject.SetActive(true);
            btnON.gameObject.SetActive(false);
        }
    }
}
