using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        rb.AddForce(movement * speed);


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
    }
}