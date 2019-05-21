using System.Collections;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private int startLevelNumberDefault = 1;
    private int targetScoreDefault = 5;
    private float candySpawnTimeDefault = 3f;
    private float candySpeedDefault = 2f;
    private float bonusCandySpawnTimeDefault = 20f;

    public int targetScoreMulti = 15;
    public float candySpawnTimeMulti = 0.9f;
    public float candySpeedMulti = 1.1f;
    public float bonusCandySpawnTimeMulti = 1.1f;

    public void Awake()
    {        
        if (PlayerPrefs.GetString("GameMode", "") == "Endless")
        {
            startLevelNumberDefault = 1;
            targetScoreDefault = 5;
            candySpawnTimeDefault = 3f;
            candySpeedDefault = 2f;
            bonusCandySpawnTimeDefault = 20f;
        }
        else
        {
            startLevelNumberDefault = 1;
            targetScoreDefault = 10000;
            candySpawnTimeDefault = 0.5f;
            candySpeedDefault = 3f;
            bonusCandySpawnTimeDefault = 15f;
        }
    }

    private ArrayList levels = new ArrayList();
    private Color32[] colors = {
        new Color32(50, 50, 50, 255),
        new Color32(100, 100, 100, 255),
        new Color32(150, 150, 150, 255),
        new Color32(200, 200, 200, 255)
    };
    private Color32[] blindColors = {
        new Color32(130, 255, 130, 255),
        new Color32(255, 100, 100, 255),
        new Color32(255, 175, 90, 255),
        new Color32(255, 250, 110, 255)
    };
    private Color32[] topColors;
    private Color32[] bottomColors;
    private Level lastLevel;

    public Level Generate()
    {
        Level level = new Level(GenerateLevelNumber(), GenerateTopColors(), GenerateBottomColors(), GenerateTargetScore(), GenerateCandySpawnTime(), GenerateCandySpeed(), GenerateBonusCandySpawnTime());
        levels.Add(level);
        lastLevel = level;
        return level;
    }

    private Color32[] GenerateTopColors()
    {
        if (GameController.instance.ColorBlindModeActive())
        {
            topColors = new Color32[4]{
                colors[Random.Range(0, colors.Length)],
                colors[Random.Range(0, colors.Length)],
                colors[Random.Range(0, colors.Length)],
                colors[Random.Range(0, colors.Length)]
            };
         } else {
            topColors = new Color32[4]{
                blindColors[Random.Range(0, blindColors.Length)],
                blindColors[Random.Range(0, blindColors.Length)],
                blindColors[Random.Range(0, blindColors.Length)],
                blindColors[Random.Range(0, blindColors.Length)]
            };
        }
        return topColors;
    }

    private Color32[] GenerateBottomColors() {
        Color32[] bottomColors = {
            new Color32(0, 0, 0, 0),
            new Color32(0, 0, 0, 0),
            new Color32(0, 0, 0, 0),
            new Color32(0, 0, 0, 0)
        };

        Color32[] availableColors;

        for (int i = 0; i < bottomColors.Length; i++)
        {
            if (GameController.instance.ColorBlindModeActive())
            {
                availableColors = colors.Where(val => !val.Equals(topColors[i])).ToArray();
            }
            else
            {
                availableColors = blindColors.Where(val => !val.Equals(topColors[i])).ToArray();                
            }            
            bottomColors[i] = availableColors[Random.Range(0, availableColors.Length - 1)];            
        }
        return bottomColors;
    }

    private int GenerateLevelNumber()
    {
        if (levels.Capacity == 0)
        {
            return startLevelNumberDefault;
        }

        return lastLevel.GetLevelNumber() + 1;
    }

    private int GenerateTargetScore()
    {
        if (levels.Capacity == 0)
        {
            return targetScoreDefault;
        }               
        return lastLevel.GetTargetScore() + targetScoreMulti;
    }

    private float GenerateCandySpawnTime()
    {
        if (levels.Capacity == 0)
        {
            return candySpawnTimeDefault;
        }
        return lastLevel.GetCandySpawnTime() * candySpawnTimeMulti;
    }

    private float GenerateCandySpeed()
    {
        if (levels.Capacity == 0)
        {
            return candySpeedDefault;
        }
        return lastLevel.GetCandySpeed() * candySpeedMulti;
    }

    private float GenerateBonusCandySpawnTime()
    {
        if (levels.Capacity == 0)
        {
            return bonusCandySpawnTimeDefault;
        }
        return lastLevel.GetCandySpawnTime() * bonusCandySpawnTimeMulti;
    }
}