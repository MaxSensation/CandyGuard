using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effectmusic : MonoBehaviour
{
    public AudioSource effect;
    public AudioClip levelTransitionSound;
    public AudioClip[] bounceSounds;
    private bool clip1NotPlayed = true; 

    void Start()
    {
        effect = GetComponent<AudioSource>();
    }

    public void PlayBounceSound()
    {
        effect.clip = bounceSounds[Random.Range(0, bounceSounds.Length)];
        effect.Play();
    }

    public void PlayLevelTransitionSound()
    {
        effect.clip = levelTransitionSound;
        effect.Play();
    }

}
