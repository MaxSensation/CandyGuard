using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider musicVolume;
    public Slider effectVolume;
    public static Options instance = null;
    private AudioSource menuMusic;

    private void Start()
    {
        menuMusic = GetComponentInParent<AudioSource>();

        if (PlayerPrefs.GetInt("ColorBlind", 0) == 1)
            GameObject.Find("ColorblindmodeToggle").GetComponent<Toggle>().isOn = true;
    }

    public void UpdateVolume()
    {
        menuMusic.volume = musicVolume.value;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("EffectVolume", effectVolume.value);        
    }

    public float GetMusicVolume()
    {
        return musicVolume.value;
    }

    public float GetEffectVolume()
    {
        return effectVolume.value;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);        
    }

    public void SetColorBlindMode(bool toggle)
    {     
        if (toggle == true)
        {
            PlayerPrefs.SetInt("ColorBlind", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ColorBlind", 0);
        }
    }
}