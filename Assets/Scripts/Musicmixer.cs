using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musicmixer : MonoBehaviour
{
    private AudioSource aud;
    public AudioClip clip1;
    public AudioClip clip2;
     
   
    
    // Start is called before the first frame update
    void Awake()
    {
        aud = GetComponent<AudioSource>();
       
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
