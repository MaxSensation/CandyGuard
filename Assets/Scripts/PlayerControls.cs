using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!GameController.instance.IsGameover() )
        {
            if (!GameController.instance.IsPaused())
            {
                MovePlayer();
            }
            
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
                transform.position = touchedPos;
            }
        }
        //DEBUG PC
        Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, transform.position.y));
        pos.y = transform.position.y;
        transform.position = pos;
    }
}