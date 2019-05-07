using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCandy : Candy
{
    void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.gameObject.tag == "Player")
        {
            FlipDirection();
        }
        if (colider.gameObject.tag == "Goal")
        {
            Goal();
        }
    }

    void Goal()
    {
        if (enteredArena == false)
        {
            enteredArena = true;
        }
        else
        {
            if (gameObject.transform.position.y > 0)
            {                
                if (GameController.instance.GetTopColor(lane).Equals((Color32) GetComponentInChildren<SpriteRenderer>().color))
                {                    
                    GameController.instance.AddScore(1);
                    GameController.instance.AnimateBag(true, lane);
                }
                else
                {
                    GameController.instance.GameOver();
                }            
            }
            else
            {                
                if (GameController.instance.GetBottomColor(lane).Equals((Color32) GetComponentInChildren<SpriteRenderer>().color))
                {
                    GameController.instance.AddScore(1);
                    GameController.instance.AnimateBag(false, lane);
                }
                else
                {
                    GameController.instance.GameOver();
                }
            }
            Destroy(gameObject);
            GameController.instance.RemoveActiveCandy();
        }
    }
}
