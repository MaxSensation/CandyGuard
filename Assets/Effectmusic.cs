using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effectmusic : MonoBehaviour
{
    public AudioSource effect;
    public AudioClip clip1;
    private bool clip1NotPlayed = true; 

    void Start()
    {
        effect = GetComponent<AudioSource>();
    }

    public void PlayLevelTransitionSound()
    {
        effect.clip = clip1;
        effect.Play();
    }

}
