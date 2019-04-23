using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{    
    public int goalLevel = 1;
    public Text scoreText;
    public Text gameOverText;
    public GameObject[] candyBags;
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
        SetCandyBagColors();
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
        } else if (goalLevel == 2)
        {
            topColors = new Color32[4] {
                new Color32(255, 0, 0, 255),
                new Color32(255, 0, 0, 255),
                new Color32(0, 255, 0, 255),
                new Color32(0, 255, 0, 255)
            };
            bottomColors = new Color32[4] {
                new Color32(0, 255, 0, 255),
                new Color32(0, 255, 0, 255),
                new Color32(255, 0, 0, 255),
                new Color32(255, 0, 0, 255)
            };
        } else if (goalLevel == 3)
        {
            topColors = new Color32[4] {
                new Color32(255, 0, 0, 255),
                new Color32(0, 255, 0, 255),
                new Color32(0, 0, 255, 255),
                new Color32(100, 100, 0, 255)
            };
            bottomColors = new Color32[4] {
                new Color32(100, 100, 0, 255),
                new Color32(0, 0, 255, 255),
                new Color32(0, 255, 0, 255),
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
