using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{    
    public Text scoreText;
    public Text levelText;
    public GameObject gameoverUI;
    public GameObject highScoreUI;    
    public GameObject[] candyBags;
    public static GameController instance = null;
    public AudioSource gameMusic;
    public GameObject targetBar;

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

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        levelGenerator = GetComponent<LevelGenerator>();
        GenerateLevel();
        Cursor.visible = false;
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
        if (currentScore >= targetScore)
        {
            difficultyChangeActive = true;
        }
    }

    void Update()
    {
        if (difficultyChangeActive && candiesActive == 0)
        {            
            IncreaseDifficulty();
        }
    }

    private void IncreaseDifficulty()
    {
        GenerateLevel();
        SetCandyBagColors();        
        AddSparklesToAllBags();
        difficultyChangeActive = false;
        scoreBar.LevelUpdateBar(currentScore, currentLevel.GetTargetScore());
    }

    public void GameOver()
    {
        gameover = true;
        gameoverUI.SetActive(true);
        if (currentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            highScoreUI.transform.Find("ScoreText").GetComponent<Text>().text = currentScore.ToString();
            highScoreUI.SetActive(true);            
        }
    }

    public void AddSparklesToAllBags()
    {
        for (int i = 0; i < candyBags.Length; i++)
        {
            candyBags[i].GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}
