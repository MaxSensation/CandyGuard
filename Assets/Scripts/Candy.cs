using UnityEngine;
using UnityEngine.UI;

public class Candy : MonoBehaviour
{
    private float speedRotate = 100f;

    private bool rotateRight = false;
    private int lane;
    private int type;
    private Rigidbody2D rb;
    private bool enteredArena = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AddForce();
        GetRotation();
    }

    void Update()
    {
        Rotate();
    }

    private void GetRotation()
    {
        if (Random.Range(0,2) == 1)
        {
            rotateRight = true;
        }
    }

    private void Rotate()
    {
        if (rotateRight)
        {            
            GetComponentInChildren<Transform>().Rotate(Vector3.forward * Time.deltaTime * speedRotate);
        }
        else
        {
            GetComponentInChildren<Transform>().Rotate(Vector3.forward * Time.deltaTime * speedRotate * -1);
        }     
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
                GameController.instance.Goal(lane, true, GetComponentInChildren<SpriteRenderer>().color);
            }
            else
            {
                GameController.instance.Goal(lane, false, GetComponentInChildren<SpriteRenderer>().color);
            }
            Destroy(gameObject);
            GameController.instance.RemoveActiveCandy();
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
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GameController.instance.GetCandySpeed() * 100 * Time.deltaTime);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -GameController.instance.GetCandySpeed() * 100 * Time.deltaTime);
        }
    }
}
