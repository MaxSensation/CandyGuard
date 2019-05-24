using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonSound : MonoBehaviour
{    
    private void Start()
    {
        if (GetComponent<Button>() == true)
        {
            GetComponent<Button>().onClick.AddListener(PlaySound);
        }
        else
        {
            GetComponent<Toggle>().onValueChanged.AddListener(PlaySound);
        }
    }

    private void PlaySound()
    {
        EffectMixer.instance.PlayMenuButtonSound();
    }

    private void PlaySound(bool toggle)
    {
        EffectMixer.instance.PlayColorBlindToggleSound();
    }
}