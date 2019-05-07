using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySound : MonoBehaviour
{
    public AudioClip[] bouncSounds;
    private AudioSource audioSouce;

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.gameObject.tag == "Player")
        {
            audioSouce = GetComponent<AudioSource>();
            audioSouce.clip = bouncSounds[Random.Range(0, bouncSounds.Length)];
            audioSouce.Play();
        }
    }
}
