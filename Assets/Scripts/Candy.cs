using UnityEngine;
using UnityEngine.UI;

public class Candy : MonoBehaviour
{
    public float speed = 5f;

    private int lane;
    private int type;
    private Rigidbody2D rb;
    private bool enteredArena = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AddForce();
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
            if (gameObject.transform.position.y >= 0)
            {
                GameController.instance.Goal(lane, true, GetComponent<Image>().color);
            }
            else
            {
                GameController.instance.Goal(lane, false, GetComponent<Image>().color);
            }
            Destroy(gameObject);
        }
    }

    public void SetLane(int lane) {
        this.lane = lane;
    }

    public void SetType(int type)
    {
        this.type = type;
    }

    void FlipDirection()
    {
        rb.velocity = rb.velocity * -1;
    }

    void AddForce()
    {
        if (transform.position.y <= 0)
        {            
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
        }
    }
}
