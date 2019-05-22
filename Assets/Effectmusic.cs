using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effectmusic : MonoBehaviour
{
    public static Effectmusic instance = null;
    private AudioSource effect;
    public AudioClip levelTransitionSound;
    public AudioClip[] bounceSounds;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        effect = GetComponent<AudioSource>();
        effect.volume = PlayerPrefs.GetFloat("EffectVolume", 1);
    }

    public void PlayBounceSound()
    {
        effect.clip = bounceSounds[Random.Range(1, bounceSounds.Length)];
        effect.Play();
    }

    public void PlayLevelTransitionSound()
    {
        effect.clip = levelTransitionSound;
        effect.Play();
    }

}
