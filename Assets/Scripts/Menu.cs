using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void PlayGame()
    {
        PlayerPrefs.SetString("GameMode", "Endless");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }
    public void PlayTimedGame()
    {
        PlayerPrefs.SetString("GameMode", "Timed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
