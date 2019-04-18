using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{    
    public int goalLevel = 1;
    public Text scoreText;
    public Text gameOverText;   
    public static GameController instance = null;

    private bool gameover = false;
    private int currentScore = 0;
    private Color32[] topColors;
    private Color32[] bottomColors;

    void Awake()
    {     
        if (instance == null)
            instance = this;            
        else if (instance != this)
            Destroy(gameObject);
    }

    public bool IsGameOver()
    {
        return gameover;
    }


    void Start()
    {        
        SetGoals();
    }

    public Color32 GetTopColor(int lane)
    {        
        return topColors[lane - 1];
    }

    public Color32 GetBottomColor(int lane)
    {
        return bottomColors[lane - 1];
    }

    private void SetGoals()
    {
        if (goalLevel == 1)
        {
            topColors = new Color32[4] {
                new Color32(0, 255, 0, 255),
                new Color32(0, 255, 0, 255),
                new Color32(0, 255, 0, 255),
                new Color32(0, 255, 0, 255)
            };
            bottomColors = new Color32[4] {
                new Color32(255, 0, 0, 255),
                new Color32(255, 0, 0, 255),
                new Color32(255, 0, 0, 255),
                new Color32(255, 0, 0, 255)
            };
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;
        scoreText.text = currentScore.ToString();
    }

    public void Goal(int lane, bool top, Color32 color)
    {
        if (top)
        {
            if (GetTopColor(lane).Equals(color))
            {
                AddScore(1);
            }
            else
            {
                GameOver();
            }
        }
        else
        {
            if (GetBottomColor(lane).Equals(color))
            {
                AddScore(1);
            }
            else
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        gameover = true;
        gameOverText.color = new Color32(0, 0, 0, 255);
    }
}
