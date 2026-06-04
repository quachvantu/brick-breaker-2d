using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Rigidbody2D rb;
    float targetX = 0f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(
                new Vector3(
                Input.GetTouch(0).position.x,
                Input.GetTouch(0).position.y,
                10f
               )
            );
            targetX = touchPosition.x;
        }
        else
        {
            float inputX = Input.GetAxis("Horizontal");
            targetX = transform.position.x + inputX * Time.deltaTime * speed;
        }
        targetX = Mathf.Clamp(targetX, -8f, 8f);
        transform.position = new Vector2(targetX, transform.position.y);
    }
}
