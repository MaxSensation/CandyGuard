using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScoreBar : MonoBehaviour
{
    private Transform bar;

    private float barZeroPos = 0.45f;
    private float barZeroScale = 0.001f;
    private float barFullPos = 3.0f;
    private float barFullScale = 1.0f;
    private int currentScore = 0;
    private int targetScore = 10;
    private float procentage;
    private float speed = 0.8f;

    void Start()
    {
        bar = GameObject.Find("BarLaying").transform;
        ResetBar();
    }

    public void LevelUpdateBar(int currentScore, int targetScore)
    {
        ResetBar();
        this.currentScore = currentScore;
        this.targetScore = targetScore;
    }

    public void UpdateBar(int currentScore)
    {        
        procentage = (float) currentScore / targetScore;
        if (procentage >= 1.0f)
        {
            procentage = 1.0f;
        }

        UpdateBarByScaleAndPos();
    }

    private void ResetBar()
    {
        bar.localPosition = (new Vector3(bar.localPosition.x, barZeroPos, bar.localPosition.z));
        bar.localScale = (new Vector3(barZeroScale, bar.localScale.y, bar.localScale.z));
    }

    private void UpdateBarByScaleAndPos()
    {        
        bar.localPosition = new Vector3(bar.localPosition.x, barFullPos * procentage, bar.localPosition.z);
        bar.localScale = new Vector3(barFullScale * procentage, bar.localScale.y, bar.localScale.z);
    }
}