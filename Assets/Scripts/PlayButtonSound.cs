using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonSound : MonoBehaviour
{    
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);        
    }   

    private void PlaySound()
    {
        EffectMixer.instance.PlayMenuButtonSound();
    }
}

