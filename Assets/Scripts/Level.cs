using UnityEngine;

public class Level
{
    private readonly int levelNumber;
    private readonly Color32[] topColors;
    private readonly Color32[] bottomColors;
    private readonly int targetScore;
    readonly float candySpawnTime;
    readonly float candySpeed;
    readonly float bonusCandySpawnTime;

    public Level(int levelNumber, Color32[] topColors, Color32[] bottomColors, int targetScore, float candySpawnTime, float candySpeed, float bonusCandySpawnTime) {
        this.levelNumber = levelNumber;
        this.topColors = topColors;
        this.bottomColors = bottomColors;
        this.targetScore = targetScore;
        this.candySpawnTime = candySpawnTime;
        this.candySpeed = candySpeed;
        this.bonusCandySpawnTime = bonusCandySpawnTime;
    }


    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public Color32[] GetTopColors()
    {
        return topColors;
    }

    public Color32[] GetBottomColors()
    {
        return bottomColors;
    }

    public int GetTargetScore()
    {
        return targetScore;
    }

    public float GetCandySpawnTime()
    {
        return candySpawnTime;
    }

    public float GetCandySpeed()
    {
        return candySpeed;
    }

    public float GetBonusCandySpawnTime()
    {
        return bonusCandySpawnTime;
    }
}
