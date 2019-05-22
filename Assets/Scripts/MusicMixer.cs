using UnityEngine;

public class MusicMixer : MonoBehaviour
{
    public static MusicMixer instance = null;
    private AudioSource music;
    public AudioClip menuMusic;    
    public AudioClip endlessModeMusic;
    public AudioClip timedModeMusic;

    private void Awake()
    {
        if (instance == null)
            instance = this;        
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(GameObject.Find("SoundMixer"));
    }

    public void Start()
    {
        music = GetComponent<AudioSource>();
        music.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
    }

    public void PlayEndlessModeMusic()
    {
        music.clip = endlessModeMusic;
        music.Play();
    }

    public void PlayTimedModeMusic()
    {
        music.clip = timedModeMusic;
        music.Play();
    }

    public void PlayMenuMusic()
    {
        music.clip = menuMusic;
        music.Play();
    }

    public void SetVolume(float volume)
    {
        music.volume = volume;
    }
}
