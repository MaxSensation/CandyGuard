using System.Collections;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private int startLevelNumberDefault;
    private int targetScoreDefault;
    private float candySpawnTimeDefault;
    private float candySpeedDefault;
    private float bonusCandySpawnTimeDefault;

    public int targetScoreMulti = 150;
    public float candySpawnTimeMulti = 0.9f;
    public float candySpeedMulti = 1.1f;
    public float bonusCandySpawnTimeMulti = 1.1f;        
    private ArrayList levels = new ArrayList();
    private Color32[] colors = {
        new Color32(130, 255, 130, 255),
        new Color32(255, 100, 100, 255),
        new Color32(255, 175, 90, 255),
        new Color32(255, 250, 110, 255)
    };
    private Color32[] blindColors = {
        new Color32(0, 0, 0, 255),
        new Color32(100, 100, 100, 255),
        new Color32(220, 220, 220, 255),
        new Color32(255, 255, 255, 255)
    };
    private Color32[] topColors;
    private Color32[] bottomColors;
    private Level lastLevel;

    public Level Generate()
    {
        if (PlayerPrefs.GetString("GameMode", "") == "Endless" && lastLevel == null)
        {
            startLevelNumberDefault = 1;
            targetScoreDefault = 50;
            candySpawnTimeDefault = 3f;
            candySpeedDefault = 2f;
            bonusCandySpawnTimeDefault = 20f;
        }
        else if (PlayerPrefs.GetString("GameMode", "") == "Timed" &&  lastLevel == null)        
        {
            startLevelNumberDefault = 1;
            targetScoreDefault = 10000;
            candySpawnTimeDefault = 0.5f;
            candySpeedDefault = 2.8f;
            bonusCandySpawnTimeDefault = 15f;
        }

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
                blindColors[Random.Range(0, blindColors.Length)],
                blindColors[Random.Range(0, blindColors.Length)],
                blindColors[Random.Range(0, blindColors.Length)],
                blindColors[Random.Range(0, blindColors.Length)]
            };            
        } else {
            topColors = new Color32[4]{
                colors[Random.Range(0, colors.Length)],
                colors[Random.Range(0, colors.Length)],
                colors[Random.Range(0, colors.Length)],
                colors[Random.Range(0, colors.Length)]
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
                availableColors = blindColors.Where(val => !val.Equals(topColors[i])).ToArray();
            }
            else
            {
                availableColors = colors.Where(val => !val.Equals(topColors[i])).ToArray();                                
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