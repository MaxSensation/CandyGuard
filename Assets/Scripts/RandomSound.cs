using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class RandomSound : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioMixerGroup output;
    private AudioSource bounceSound;

    // Start is called before the first frame update
    void Start()
    {
        bounceSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Candy")
        {

            bounceSound.Play();
        }
    }

    void PlaySound() {

        int randomClips = Random.Range(0, clips.Length);
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clips[randomClips];
        source.outputAudioMixerGroup = output;
        Destroy(source, clips[randomClips].length);
        


    }



}

