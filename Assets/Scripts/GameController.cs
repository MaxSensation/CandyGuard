using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int difficulty = 1;
    public Text scoreText;
    public Text levelText;
    public GameObject gameoverUI;
    public GameObject highScoreUI;    
    public GameObject[] candyBags;
    public static GameController instance = null;
    public AudioSource gameMusic;

    private bool difficultyChangeActive = false;
    private int candiesActive = 0;
    private int candyTypesGroup;
    private float candySpawnTime;
    private float bonusCandySpawnTime;
    private float candySpeed;
    private bool gameover = false;
    private int currentScore = 0;
    private int targetScore = 0;
    private Color32[] topColors;
    private Color32[] bottomColors;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        SetDifficulty(difficulty);
        Cursor.visible = false;
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
        Application.targetFrameRate = 60;
        SetCandyBagColors();
        gameMusic.volume = Options.instance.GetMusicVolume();
    }

    public int GetCandyTypesGroup()
    {
        return candyTypesGroup;
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
        scoreText.text = currentScore.ToString();
        if (currentScore >= targetScore)
        {
            SetDifficultyChangeActive();
        }
    }

    void Update()
    {
        if (difficultyChangeActive && candiesActive == 0)
        {            
            IncreaseDifficulty();
            levelText.text = difficulty.ToString();
        }
    }

    private void SetDifficultyChangeActive()
    {
        difficultyChangeActive = true;
    }

    private void IncreaseDifficulty()
    {        
        difficulty += 1;
        SetDifficulty(difficulty);
        SetCandyBagColors();
        difficultyChangeActive = false;
        AddSparklesToAllBags();
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

    private void SetGoals(int goalLevel)
    {
        Color32 green = new Color32(130, 255, 130, 255);
        Color32 red = new Color32(255, 100, 100, 255);
        Color32 orange = new Color32(255, 175, 90, 255);
        Color32 yellow = new Color32(255, 250, 110, 255);

        if (goalLevel == 1)
        {
            topColors = new Color32[4] {
                red,
                red,
                red,
                red
            };
            bottomColors = new Color32[4] {
                green,
                green,
                green,
                green
            };
        }
        else if (goalLevel == 2)
        {
            topColors = new Color32[4] {
                green,
                green,
                green,
                green
            };
            bottomColors = new Color32[4] {
                red,
                red,
                red,
                red
            };
        }
        else if (goalLevel == 3)
        {
            topColors = new Color32[4] {
                green,
                red,
                green,
                red
            };
            bottomColors = new Color32[4] {
                red,
                green,
                red,
                green
            };
        }
        else if (goalLevel == 4)
        {
            topColors = new Color32[4] {
                red,
                green,
                red,
                green
            };
            bottomColors = new Color32[4] {
                green,
                red,
                green,
                red
            };
        }
        else if (goalLevel == 5)
        {
            topColors = new Color32[4] {
                green,
                red,
                green,
                yellow
            };
            bottomColors = new Color32[4] {
                yellow,
                green,
                red,
                green
            };
        }
        else if (goalLevel == 6)
        {
            topColors = new Color32[4] {
                red,
                green,
                yellow,
                green
            };
            bottomColors = new Color32[4] {
                green,
                red,
                green,
                yellow
            };
        }
        else if (goalLevel == 7)
        {
            topColors = new Color32[4] {
                green,
                yellow,
                red,
                yellow
            };
            bottomColors = new Color32[4] {
                yellow,
                red,
                yellow,
                green
            };
        }
        else if (goalLevel == 8)
        {
            topColors = new Color32[4] {
                yellow,
                red,
                yellow,
                green
            };
            bottomColors = new Color32[4] {
                red,
                yellow,
                green,
                yellow
            };
        }
        else if (goalLevel == 9)
        {
            topColors = new Color32[4] {
                orange,
                green,
                red,
                yellow
            };
            bottomColors = new Color32[4] {
                yellow,
                red,
                green,
                orange
            };
        }
        else if (goalLevel == 10)
        {
            topColors = new Color32[4] {
                yellow,
                red,
                green,
                orange
            };
            bottomColors = new Color32[4] {
                orange,
                green,
                red,
                yellow
            };
        }
        else if (goalLevel == 11)
        {
            topColors = new Color32[4] {
                red,
                orange,
                yellow,
                green
            };
            bottomColors = new Color32[4] {
                green,
                yellow,
                orange,
                red
            };
        }
        else if (goalLevel == 12)
        {
            topColors = new Color32[4] {
                green,
                yellow,
                orange,
                red
            };
            bottomColors = new Color32[4] {
                red,
                orange,
                yellow,
                green
            };
        }
        else if (goalLevel == 13)
        {
            topColors = new Color32[4] {
                yellow,
                green,
                yellow,
                green
            };
            bottomColors = new Color32[4] {
                orange,
                red,
                orange,
                red
            };
        }
        else if (goalLevel == 14)
        {
            topColors = new Color32[4] {
                orange,
                red,
                orange,
                red
            };
            bottomColors = new Color32[4] {
                yellow,
                green,
                yellow,
                green
            };
        }
        else if (goalLevel == 15)
        {
            topColors = new Color32[4] {
                yellow,
                green,
                green,
                yellow
            };
            bottomColors = new Color32[4] {
                orange,
                red,
                red,
                orange
            };
        }
    }

    private void SetDifficulty(int difficulty)
    {
        if (difficulty == 1)
        {
            candyTypesGroup = 1;
            targetScore = 5;
            SetGoals(1);            
            candySpawnTime = 3f;
            bonusCandySpawnTime = 20.1f;
            candySpeed = 2f;
        }
        else if (difficulty == 2)
        {
            targetScore = 20;
            SetGoals(1);            
            candySpawnTime = 3f;
            candySpeed = 2.2f;
        }
        else if (difficulty == 3)
        {
            targetScore = 35;
            SetGoals(2);            
            candySpawnTime = 3f;
            candySpeed = 2.3f;
        }
        else if (difficulty == 4)
        {
            targetScore = 50;
            SetGoals(2);           
            candySpawnTime = 2.8f;
            candySpeed = 2.4f;
        }
        else if (difficulty == 5)
        {
            targetScore = 60;
            SetGoals(2);            
            candySpawnTime = 2.8f;
            candySpeed = 2.5f;
        }
        else if (difficulty == 6)
        {
            targetScore = 80;
            SetGoals(3);         
            candySpawnTime = 2.5f;
            candySpeed = 2.6f;
        }
        else if (difficulty == 7)
        {
            targetScore = 95;
            SetGoals(3);            
            candySpawnTime = 2.5f;
            candySpeed = 2.7f;
        }
        else if (difficulty == 8)
        {            
            targetScore = 110;
            SetGoals(4);            
            candySpawnTime = 2.5f;
            candySpeed = 2.8f;
        }
        else if (difficulty == 9)
        {            
            targetScore = 125;
            SetGoals(5);           
            candySpawnTime = 2.3f;
            candySpeed = 2.9f;
        }
        else if (difficulty == 10)
        {            
            targetScore = 140;
            SetGoals(6);            
            candySpawnTime = 2.3f;
            candySpeed = 3f;
        }
        else if (difficulty == 11)
        {         
            targetScore = 155;
            SetGoals(7);            
            candySpawnTime = 2f;
            candySpeed = 3.1f;
        }
        else if (difficulty == 12)
        {
            targetScore = 170;
            SetGoals(8);         
            candySpawnTime = 1.5f;
            candySpeed = 3.2f;
        }
        else if (difficulty == 13)
        {            
            targetScore = 185;
            SetGoals(8);            
            candySpawnTime = 1.5f;
            candySpeed = 3.2f;
        }
        else if (difficulty == 14)
        {
            targetScore = 200;
            SetGoals(5);
            candySpawnTime = 1.3f;
            bonusCandySpawnTime = 22f;
            candySpeed = 3.5f;
        }
        else if (difficulty == 15)
        {
            targetScore = 215;
            SetGoals(2);
            candySpawnTime = 1.3f;
            bonusCandySpawnTime = 22f;
            candySpeed = 3.5f;
        }
        else if (difficulty == 16)
        {
            targetScore = 230;
            SetGoals(4);
            candySpawnTime = 1.2f;
            bonusCandySpawnTime = 20.1f;
            candySpeed = 3.5f;
        }
        else if (difficulty == 17)
        {
            targetScore = 245;
            SetGoals(9);
            candySpawnTime = 1.5f;
            bonusCandySpawnTime = 20.1f;
            candySpeed = 2.9f;
        }
        else if (difficulty == 18)
        {
            targetScore = 260;
            SetGoals(10);
            candySpawnTime = 1.4f;
            bonusCandySpawnTime = 20.1f;
            candySpeed = 3f;
        }
        else if (difficulty == 19)
        {
            targetScore = 275;
            SetGoals(5);
            candySpawnTime = 1.4f;
            bonusCandySpawnTime = 20.1f;
            candySpeed = 3f;
        }
        else if (difficulty == 20)
        {
            targetScore = 290;
            SetGoals(11);
            candySpawnTime = 1.2f;
            bonusCandySpawnTime = 19.9f;
            candySpeed = 2.9f;
        }
        else if (difficulty == 21)
        {
            targetScore = 305;
            SetGoals(12);
            candySpawnTime = 2f;
            bonusCandySpawnTime = 20.1f;
            candySpeed = 2.8f;
        }
        else if (difficulty == 22)
        {
            targetScore = 320;
            SetGoals(2);
            candySpawnTime = 1f;
            bonusCandySpawnTime = 22f;
            candySpeed = 2.9f;
        }
        else if (difficulty == 23)
        {
            targetScore = 335;
            SetGoals(13);
            candySpawnTime = 2.5f;
            bonusCandySpawnTime = 23f;
            candySpeed = 2.7f;
        }
        else if (difficulty == 24)
        {
            targetScore = 350;
            SetGoals(14);
            candySpawnTime = 3f;
            bonusCandySpawnTime = 25f;
            candySpeed = 1.9f;
        }
        else if (difficulty == 25)
        {
            targetScore = 100000;
            SetGoals(15);
            candySpawnTime = 1.9f;
            bonusCandySpawnTime = 27f;
            candySpeed = 1.8f;
        }
    }
}
