using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{    
    public Animator[] candies;
    public GameObject startScreen;


    private void Start()
    {        
        startScreen = GameObject.Find("Start Screen");
    }


    void Update()
    {
        if (startScreen.activeInHierarchy) 
        {
            for (int i = 0; i < candies.Length; i++)
            {
                candies[i].GetComponent<Animator>().Play("CandyAnimation");
            }
        }
    }
}
