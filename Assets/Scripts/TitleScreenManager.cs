using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public Animation candyAnimation;


    private void Start()
    {
        candyAnimation.wrapMode = WrapMode.Once;
        candyAnimation.Play();
    }


    void Update()
    {
        
    }
}
