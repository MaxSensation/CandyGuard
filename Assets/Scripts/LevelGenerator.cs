using System.Collections;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private readonly int startLevelNumberDefault = 1;
    private readonly int targetScoreDefault = 5;
    private readonly float candySpawnTimeDefault = 3f;
    private readonly float candySpeedDefault = 2f;
    private readonly float bonusCandySpawnTimeDefault = 20f;

    private ArrayList levels = new ArrayList();
    private Color32[] colors = {
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

    public void PrintTest()
    {        
        foreach(Level level in levels)
        {            
            Debug.Log(level.GetLevelNumber());
            Debug.Log(level.GetTopColors());
            Debug.Log(level.GetBottomColors());
            Debug.Log(level.GetTargetScore());
            Debug.Log(level.GetCandySpawnTime());
            Debug.Log(level.GetCandySpeed());
            Debug.Log(level.GetBonusCandySpawnTime());
        }
    }

    private Color32[] GenerateTopColors()
    {
        topColors = new Color32[4]{
            colors[Random.Range(0, colors.Length)],
            colors[Random.Range(0, colors.Length)],
            colors[Random.Range(0, colors.Length)],
            colors[Random.Range(0, colors.Length)]
        };

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
            availableColors = colors.Where(val => !val.Equals(topColors[i])).ToArray();
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
        
        return lastLevel.GetTargetScore() * 3;
    }

    private float GenerateCandySpawnTime()
    {
        if (levels.Capacity == 0)
        {
            return candySpawnTimeDefault;
        }
        return lastLevel.GetCandySpawnTime() * 0.9f;
    }

    private float GenerateCandySpeed()
    {
        if (levels.Capacity == 0)
        {
            return candySpeedDefault;
        }
        return lastLevel.GetCandySpawnTime() * 1.1f;
    }

    private float GenerateBonusCandySpawnTime()
    {
        if (levels.Capacity == 0)
        {
            return bonusCandySpawnTimeDefault;
        }
        return lastLevel.GetCandySpawnTime() * 1.1f;
    }
}