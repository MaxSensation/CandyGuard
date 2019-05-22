using UnityEngine;
using UnityEngine.UI;

public class EffectDefault : MonoBehaviour
{
    void Start()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("EffectVolume", 1);
        GetComponent<Slider>().onValueChanged.AddListener(UpdateValue);
    }
    
    private void UpdateValue(float value)
    {
        PlayerPrefs.SetFloat("EffectVolume", value);
        EffectMixer.instance.SetVolume(value);
    }
}
