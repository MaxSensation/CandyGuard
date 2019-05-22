using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musicmixer : MonoBehaviour
{
    public static Musicmixer instance = null;
    private AudioSource aud;
    public AudioClip clip1;
    public AudioClip clip2;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        if (PlayerPrefs.GetString("GameMode", "") == "Endless")
        {
            aud.clip = clip1; 
        }
        if (PlayerPrefs.GetString("GameMode", "") == "Timed") {

            aud.clip = clip2; 

        }

        aud.Play();
    }

   
}
