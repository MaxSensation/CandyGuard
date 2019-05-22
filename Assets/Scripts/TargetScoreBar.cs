using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScoreBar : MonoBehaviour
{
    private Transform barTransform;

    private float barZeroScale = 0f;    
    private float barFullScale = 0.96f;    
    private int currentScore = 0;
    private int targetScore = 10;
    private float procentage;

    void Start()
    {
        barTransform = GameObject.Find("ProgressionBarFront").transform;        
        ResetBar();
    }

    public void LevelUpdateBar(int currentScore, int targetScore)
    {        
        this.currentScore = currentScore;
        this.targetScore = targetScore;
        UpdateBar(currentScore);
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
        barTransform.localScale = new Vector3(barZeroScale, 1, 1);        
    }

    private void UpdateBarByScaleAndPos()
    {
        barTransform.localScale = new Vector3(barFullScale * procentage, 1, 1);        
    }
}