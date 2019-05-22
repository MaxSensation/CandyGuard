using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
    public Slider musicSlider;
    public Slider effectSlider;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);        
        MusicMixer.instance.SetVolume(musicSlider.value);

        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 1);
        EffectMixer.instance.SetVolume(effectSlider.value);
    }

    public void UpdateVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        MusicMixer.instance.SetVolume(musicSlider.value);

        PlayerPrefs.SetFloat("EffectVolume", effectSlider.value);
        EffectMixer.instance.SetVolume(effectSlider.value);
    }
}
