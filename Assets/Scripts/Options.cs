using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider musicVolume;
    public Slider effectVolume;
    public static Options instance = null;
    private AudioSource menuMusic;
    private bool colorBlindMode = false;

    private void Start()
    {
        menuMusic = GetComponentInParent<AudioSource>();

        if (PlayerPrefs.GetInt("ColorBlind", 0) == 0)
            colorBlindMode = false;
        else
        {
            colorBlindMode = true;
            GameObject.Find("ColorblindmodeToggle").GetComponent<Toggle>().isOn = true;
        }            
    }

    public void Update()
    {
        instance.menuMusic.volume = musicVolume.value;
    }

    public float GetMusicVolume()
    {
        return musicVolume.value;
    }

    public float GetEffectVolume()
    {
        return effectVolume.value;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public bool IsColorBlindMode()
    {
        return colorBlindMode;
    }

    public void SetColorBlindMode(bool toggle)
    {
        colorBlindMode = toggle;
        if (toggle == true)
        {
            PlayerPrefs.SetInt("ColorBlind", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ColorBlind", 0);
        }
    }
}