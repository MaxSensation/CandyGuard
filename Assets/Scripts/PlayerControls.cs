using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!GameController.instance.IsGameover())
        {
            MovePlayer();
        }        
    }

    private void MovePlayer()
    {        
        //Mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                Vector2 touchedPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.GetTouch(0).position.x, transform.position.y));
                touchedPos.y = transform.position.y;
                transform.position = Vector2.Lerp(transform.position, touchedPos, Time.deltaTime * speed);
            }
        }
        //DEBUG PC
        Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, transform.position.y));
        pos.y = transform.position.y;
        transform.position = Vector2.Lerp(transform.position, pos, Time.deltaTime * speed);
    }
}