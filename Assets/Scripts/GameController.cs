using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{    
    public Text scoreText;
    public Text timeleftText;
    public Text levelText;
    public GameObject gameoverUI;
    public GameObject highScoreUI;    
    public GameObject[] candyBags;
    public static GameController instance = null;
    public AudioSource gameMusic;
    public GameObject targetBar;
    public Effectmusic effectmusic;

    private bool colorBlindModeActive = false;
    private TargetScoreBar scoreBar;
    private bool difficultyChangeActive = false;
    private int candiesActive = 0;
    private float candySpawnTime;
    private float bonusCandySpawnTime;
    private float candySpeed;
    private bool gameover = false;
    private int currentScore = 0;
    private int targetScore = 0;
    private Color32[] topColors;
    private Color32[] bottomColors;
    private Level currentLevel;
    private LevelGenerator levelGenerator;
    private string currentGameMode;
    private float timeLeft = 60;    

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        currentGameMode = PlayerPrefs.GetString("GameMode", "");        
        if (PlayerPrefs.GetInt("ColorBlind", 0) == 0)
        {
            ColorBlindModeActive(false);
        }
        else
        {
            ColorBlindModeActive(true);
        }
        if (currentGameMode == "Timed")
        {
            ConvertToTimed();
        }
        else
        {
            ConvertToEndless();
        }
        levelGenerator = GetComponent<LevelGenerator>();
        GenerateLevel();
        
    }

    private void ConvertToTimed()
    {
        GameObject[] endlessUIs = GameObject.FindGameObjectsWithTag("Endless");
        GameObject[] timedUIs = GameObject.FindGameObjectsWithTag("Timed");
        foreach (GameObject endlessUi in endlessUIs)
        {
            endlessUi.SetActive(false); ;
        }
        foreach (GameObject timedUi in timedUIs)
        {
            timedUi.SetActive(true);
        }
        gameoverUI.SetActive(false);
    }

    private void ConvertToEndless()
    {
        GameObject[] endlessUIs = GameObject.FindGameObjectsWithTag("Endless");
        GameObject[] timedUIs = GameObject.FindGameObjectsWithTag("Timed");
        foreach (GameObject endlessUi in endlessUIs)
        {
            endlessUi.SetActive(true); ;
        }
        foreach (GameObject timedUi in timedUIs)
        {
            timedUi.SetActive(false);
        }
        gameoverUI.SetActive(false);
    }

    public bool ColorBlindModeActive()
    {
        return colorBlindModeActive;
    }

    public void ColorBlindModeActive(bool mode)
    {
        colorBlindModeActive = mode;
    }

    private void GenerateLevel()
    {               
        UpdateLevel(levelGenerator.Generate());
    }

    private void UpdateLevel(Level level)
    {
        currentLevel = level;
        levelText.text = level.GetLevelNumber().ToString();
        targetScore = level.GetTargetScore();
        topColors = level.GetTopColors();
        bottomColors = level.GetBottomColors();
        candySpeed = level.GetCandySpeed();
        candySpawnTime = level.GetCandySpawnTime();
        bonusCandySpawnTime = level.GetBonusCandySpawnTime();
    }

    public bool IsGameover()
    {
        return gameover;
    }


    public void Restart()
    {
        SceneManager.LoadScene("main");
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartScreenMenu");
    }

    public bool DifficultyChangeActive()
    {
        return difficultyChangeActive;
    }

    void Start()
    {                
        scoreBar = targetBar.GetComponentInChildren<TargetScoreBar>();
        scoreBar.LevelUpdateBar(currentScore, currentLevel.GetTargetScore());
        Application.targetFrameRate = 60;
        SetCandyBagColors();        
    }

    public float GetCandySpawnTime()
    {
        return candySpawnTime;
    }

    public float GetBonusCandySpawnTime()
    {
        return bonusCandySpawnTime;
    }    

    public float GetCandySpeed()
    {
        return candySpeed;
    }

    public void AnimateBag(bool top, int lane)
    {
        int[] topI = { 1, 3, 5, 7 };
        int[] bottomI = { 0, 2, 4, 6 };        

        if (top)
        {
            candyBags[topI[lane - 1]].GetComponent<Animator>().Play("TopBagAnimation");
        }
        else
        {
            candyBags[bottomI[lane - 1]].GetComponent<Animator>().Play("BottomBagAnimation");
        }
    }

    private void SetCandyBagColors()
    {
        candyBags[0].GetComponentInChildren<SpriteRenderer>().color = bottomColors[0];
        candyBags[2].GetComponentInChildren<SpriteRenderer>().color = bottomColors[1];
        candyBags[4].GetComponentInChildren<SpriteRenderer>().color = bottomColors[2];
        candyBags[6].GetComponentInChildren<SpriteRenderer>().color = bottomColors[3];
        candyBags[1].GetComponentInChildren<SpriteRenderer>().color = topColors[0];
        candyBags[3].GetComponentInChildren<SpriteRenderer>().color = topColors[1];
        candyBags[5].GetComponentInChildren<SpriteRenderer>().color = topColors[2];
        candyBags[7].GetComponentInChildren<SpriteRenderer>().color = topColors[3];
    }

    public Color32 GetTopColor(int lane)
    {
        return topColors[lane - 1];
    }

    public void AddActiveCandy()
    {
        candiesActive += 1;
    }

    public void RemoveActiveCandy()
    {
        candiesActive -= 1;
    }

    public Color32 GetBottomColor(int lane)
    {
        return bottomColors[lane - 1];
    }

    public void AddScore(int points)
    {        
        currentScore += points;
        scoreBar.UpdateBar(currentScore);
        scoreText.text = currentScore.ToString();
        if (currentScore >= targetScore && currentGameMode == "Endless")
        {
            difficultyChangeActive = true;
        }
    }

    public string GetGameMode()
    {
        return currentGameMode;
    }

    void Update()
    {
        if (difficultyChangeActive && candiesActive == 0 && currentGameMode == "Endless")
        {            
            IncreaseDifficulty();
        }
        if (currentGameMode == "Timed" && gameover == false)
        {
            timeLeft -= Time.deltaTime;
            timeleftText.text = ((int)Math.Round(timeLeft)).ToString();
            if (timeLeft < 0)
            {
                GameOver();
            }
        }
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }

    private void IncreaseDifficulty()
    {
        GenerateLevel();
        SetCandyBagColors();        
        AddLevelTransitionEffects();
        difficultyChangeActive = false;
        scoreBar.LevelUpdateBar(currentScore, currentLevel.GetTargetScore());
        effectmusic.PlayLevelTransitionSound();
    }

    public void GameOver()
    {
        gameover = true;
        gameoverUI.SetActive(true);
        if (currentGameMode == "Endless")
        {
            if (currentScore > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", currentScore);
                highScoreUI.transform.Find("ScoreText").GetComponent<Text>().text = currentScore.ToString();
                highScoreUI.SetActive(true);
            }
        }
        else
        {
            if (currentScore > PlayerPrefs.GetInt("TimedHighScore", 0))
            {
                PlayerPrefs.SetInt("TimedHighScore", currentScore);
                highScoreUI.transform.Find("ScoreText").GetComponent<Text>().text = currentScore.ToString();
                highScoreUI.SetActive(true);
            }
        }    
    }

    public void AddLevelTransitionEffects()
    {
        ParticleSystem[] confettis = GameObject.Find("Player").gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem confetti in confettis)
        {
            confetti.Play();
        }

        for (int i = 0; i < candyBags.Length; i++)
        {
            candyBags[i].GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}
