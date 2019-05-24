using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugToggle : MonoBehaviour
{    
    void Start()
    {
        if (PlayerPrefs.GetString("DebugMode", "false") == "true")
        {
            GetComponent<Toggle>().isOn = true;
        }        
        GetComponent<Toggle>().onValueChanged.AddListener(ActivateDebugMode);
    }

    private void ActivateDebugMode(bool value)
    {
        if (value)
        {
            PlayerPrefs.SetString("DebugMode", "true");
        }
        else
        {
            PlayerPrefs.SetString("DebugMode", "false");
        }
    }
}
