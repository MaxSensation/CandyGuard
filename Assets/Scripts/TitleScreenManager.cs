using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public Animator candyAnimator;
    public GameObject startScreen;


    private void Start()
    {
        candyAnimator = GetComponent<Animator>();
        startScreen = GameObject.Find("Start Screen");
    }


    void Update()
    {
        if (startScreen.activeInHierarchy) 
        {
            candyAnimator.Play("Candy Animation", 0, 0);
        }
    }
}
