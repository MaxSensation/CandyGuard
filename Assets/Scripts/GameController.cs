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
        else if (goalLevel == 7)
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
    }

    private void SetDifficulty(int difficulty)
    {
        if (difficulty == 1)
        {
            targetScore = 5;
            SetGoals(1);
            candyTypesGroup = 1;
            candySpawnTime = 3f;
            bonusCandySpawnTime = 20f;
            candySpeed = 2f;
        }
        else if (difficulty == 2)
        {
            targetScore = 10;
            SetGoals(1);
            candyTypesGroup = 1;
            candySpawnTime = 3f;
            candySpeed = 2.2f;
        }
        else if (difficulty == 3)
        {
            targetScore = 15;
            SetGoals(2);
            candyTypesGroup = 1;
            candySpawnTime = 3f;
            candySpeed = 2.3f;
        }
        else if (difficulty == 4)
        {
            targetScore = 20;
            SetGoals(2);
            candyTypesGroup = 1;
            candySpawnTime = 2.8f;
            candySpeed = 2.4f;
        }
        else if (difficulty == 5)
        {
            targetScore = 25;
            SetGoals(2);
            candyTypesGroup = 1;
            candySpawnTime = 2.8f;
            candySpeed = 2.5f;
        }
        else if (difficulty == 6)
        {
            targetScore = 30;
            SetGoals(3);
            candyTypesGroup = 1;
            candySpawnTime = 2.5f;
            candySpeed = 2.6f;
        }
        else if (difficulty == 7)
        {
            targetScore = 35;
            SetGoals(3);
            candyTypesGroup = 1;
            candySpawnTime = 2.5f;
            candySpeed = 2.7f;
        }
        else if (difficulty == 8)
        {
            targetScore = 40;
            SetGoals(4);
            candyTypesGroup = 1;
            candySpawnTime = 2.5f;
            candySpeed = 2.8f;
        }
        else if (difficulty == 9)
        {
            targetScore = 45;
            SetGoals(5);
            candyTypesGroup = 1;
            candySpawnTime = 2.3f;
            candySpeed = 2.9f;
        }
        else if (difficulty == 10)
        {
            targetScore = 50;
            SetGoals(6);
            candyTypesGroup = 1;
            candySpawnTime = 2.3f;
            candySpeed = 3f;
        }
        else if (difficulty == 11)
        {
            targetScore = 55;
            SetGoals(7);
            candyTypesGroup = 1;
            candySpawnTime = 2f;
            candySpeed = 3.1f;
        }
        else if (difficulty == 12)
        {
            targetScore = 100000;
            SetGoals(8);
            candyTypesGroup = 1;
            candySpawnTime = 1.5f;
            candySpeed = 3.2f;
        }
    }
}
