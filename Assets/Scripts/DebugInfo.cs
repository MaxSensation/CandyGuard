using UnityEngine;
using UnityEngine.UI;

public class DebugInfo : MonoBehaviour
{
    private Text levelInfoText;
    private Text fpsText;

    private void Start()
    {
        levelInfoText = GameObject.Find("LevelInfo").GetComponent<Text>();
        fpsText = GameObject.Find("fps").GetComponent<Text>();
    }

    private string GatherLevelInfo()
    {
        string info = "";
        Level currentLevel = GameController.instance.GetCurrentLevel();
        info += "GameMode: " + GameController.instance.GetGameMode() + '\n';
        info += "Level: " + currentLevel.GetLevelNumber() + '\n';
        info += "TargetScore: " + currentLevel.GetTargetScore() + '\n';
        info += "CandySpeed: " + currentLevel.GetCandySpeed() + '\n';
        info += "CandySpawnTime: " + currentLevel.GetCandySpawnTime() + '\n';
        info += "BonusCandySpawnTime: " + currentLevel.GetBonusCandySpawnTime() + '\n';
        info += "Total candies on scene: " + GameController.instance.GetActiveCandies();
        return info;
    }

    private string GatherFps() {        
        return ((int)(1.0f / Time.smoothDeltaTime)).ToString();
    }

    private void Update()
    {
        levelInfoText.text = GatherLevelInfo();
        fpsText.text = GatherFps();
    }
}
