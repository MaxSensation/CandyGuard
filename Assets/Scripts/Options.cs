using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider MusicVolume;
    public Slider EffectVolume;

    public AudioSource Music;
    public AudioSource Effect;

    void Update()
    {
        Music.volume = MusicVolume.value;
        Effect.volume = EffectVolume.value;
    }
}
