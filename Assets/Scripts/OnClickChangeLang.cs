using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickChangeLang : MonoBehaviour
{
    private LocalizationManager lm;
    
    void Start()
    {
        lm = GameObject.FindWithTag("LocalizationManager").GetComponent<LocalizationManager>();       
    }

    public void changeLang(string path)
    {
        lm.SetLanguage(path);
    }
}
