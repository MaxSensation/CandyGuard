using UnityEngine;

public class EffectMixer : MonoBehaviour
{
    public static EffectMixer instance = null;
    private AudioSource effect;
    public AudioClip menuButtonSound;
    public AudioClip levelTransitionSound;
    public AudioClip newHighScoreSound;
    public AudioClip candyBagSound;
    public AudioClip[] bounceSounds;    

    private void Awake()
    {
        if (instance == null)
            instance = this;         
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(GameObject.Find("SoundMixer"));
    }

    private void Start()
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

    public void PlayMenuButtonSound()
    {
        effect.clip = levelTransitionSound;
        effect.Play();
    }

    public void PlayNewHighScoreSound()
    {
        effect.clip = newHighScoreSound;
        effect.Play();
    }

    public void PlayCandyBagSound()
    {
        effect.clip = candyBagSound;
        effect.Play();
    }

    public void SetVolume(float volume)
    {
        effect.volume = volume;
    }
}
