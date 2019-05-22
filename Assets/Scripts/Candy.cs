using UnityEngine;

public class Candy : MonoBehaviour
{
    private float speedRotate = 100f;
    private bool rotateRight = false;
    protected int lane;
    private int type;
    protected Rigidbody2D rb;
    protected bool enteredArena = false;

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

    protected void GetRotation()
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

    private void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.gameObject.tag == "Player")
        {
            FlipDirection();
        }
    }

    public void SetLane(int lane) {
        this.lane = lane;
    }

    public void SetType(int type)
    {
        this.type = type;
    }

    protected void FlipDirection()
    {
        rb.velocity = rb.velocity * -1;
        EffectMixer.instance.PlayBounceSound();
    }

    protected void AddForce()
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
