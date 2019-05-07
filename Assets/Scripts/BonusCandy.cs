using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCandy : Candy
{
    ParticleSystem ps;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AddForce();
        GetRotation();
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        var colorOverLifetime = ps.colorOverLifetime;
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(GetComponentInChildren<SpriteRenderer>().color * 1.20f, 0.0f), new GradientColorKey(Color.white, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });        
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(grad);
    }

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
                if (GameController.instance.GetTopColor(lane).Equals((Color32)GetComponentInChildren<SpriteRenderer>().color))
                {
                    GameController.instance.AddScore(5);
                }
            }
            else
            {
                if (GameController.instance.GetBottomColor(lane).Equals((Color32)GetComponentInChildren<SpriteRenderer>().color))
                {
                    GameController.instance.AddScore(5);
                }
            }
            Destroy(gameObject);
            GameController.instance.RemoveActiveCandy();
        }
    }
}
