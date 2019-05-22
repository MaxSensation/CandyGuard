using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        MusicMixer.instance.PlayMenuMusic();
    }
    public void PlayGame()
    {
        PlayerPrefs.SetString("GameMode", "Endless");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        MusicMixer.instance.PlayEndlessModeMusic();
    }
    public void PlayTimedGame()
    {
        PlayerPrefs.SetString("GameMode", "Timed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        MusicMixer.instance.PlayTimedModeMusic();
    }
}
