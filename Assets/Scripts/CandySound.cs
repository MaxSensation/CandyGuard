using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySound : MonoBehaviour
{
    public AudioClip[] bouncSounds;
    private AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.gameObject.tag == "Player")
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = Options.instance.GetEffectVolume();
            audioSource.clip = bouncSounds[Random.Range(0, bouncSounds.Length)];
            audioSource.Play();
        }
    }
}
