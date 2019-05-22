using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
    public Slider musicSlider;
    public Slider effectSlider;

    private void Start()
    {
        SetDefualtEffectVolume();
        SetDefualtMusicVolume();        
    }

    private void SetDefualtMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);        
    }

    private void SetDefualtEffectVolume()
    {
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 1);        
    }

    public void UpdateVolume()
    {            
        PlayerPrefs.SetFloat("EffectVolume", effectSlider.value);
        EffectMixer.instance.SetVolume(effectSlider.value);

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        MusicMixer.instance.SetVolume(musicSlider.value);
    }
}
