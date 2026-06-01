using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Ball : MonoBehaviour
{
    [SerializeField] private PaddleMovement paddle;
    // public event EventHandler OnBallLost;
    private float speed = 7f;
    private bool isLaunched = false;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!isLaunched)
        {
            transform.position = paddle.transform.position + new Vector3(0, 0.5f, 0);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isLaunched = true;
                rb.velocity = new Vector2(UnityEngine.Random.Range(-1f, 1f), 1).normalized * speed;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isLaunched)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.GetComponent<PaddleMovement>())
        {
            float paddleWidth = collision2D.collider.bounds.size.x;
            float hitPoint = collision2D.contacts[0].point.x
            - collision2D.collider.bounds.center.x;
            float normalizeHit = hitPoint / (paddleWidth / 2);
            Vector2 direction = new Vector2(normalizeHit, 1).normalized;
            rb.velocity = direction * speed;
        }
    }
    // private void OnTriggerEnter2D(Collider2D collision2D)
    // {
    //     if (collision2D.gameObject.GetComponent<WallBottom>())
    //     {
    //         OnBallLost?.Invoke(this, EventArgs.Empty);
    //     }
    // }
}
