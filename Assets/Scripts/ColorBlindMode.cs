using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBlindMode : MonoBehaviour
{
    private Toggle toggle;
    void Start()
    {
        toggle = GetComponent<Toggle>();
        if (PlayerPrefs.GetInt("ColorBlind", 0) == 1)
            toggle.isOn = true;
    }

    public void SetColorBlindMode()
    {        
        if (toggle.isOn == true)
        {
            PlayerPrefs.SetInt("ColorBlind", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ColorBlind", 0);
        }
    }
}
