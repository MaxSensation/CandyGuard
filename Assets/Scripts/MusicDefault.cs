using UnityEngine;
using UnityEngine.UI;

public class MusicDefault : MonoBehaviour
{
    void Start()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume", 1);
        GetComponent<Slider>().onValueChanged.AddListener(UpdateValue);
    }

    private void UpdateValue(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        MusicMixer.instance.SetVolume(value);
    }
}
