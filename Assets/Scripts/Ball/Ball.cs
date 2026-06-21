using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Ball : MonoBehaviour
{
    public static Ball Instance { get; private set; }
    [SerializeField] private Paddle paddle;
    public event EventHandler OnBallLost;
    private float speed;
    private bool isLaunched = false;
    private Rigidbody2D rb;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        rb = GetComponent<Rigidbody2D>();
        speed = GameManager.Instance.ballSpeed;
    }
    private void OnEnable()
    {
        GameManager.Instance.RegisterBall(this);
    }
    private void OnDisable()
    {
        GameManager.Instance.UnregisterBall(this);
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
                GameManager.Instance.StartTimer();
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
        if (PowerUpManager.Instance.GetIsBombBall() && (collision2D.gameObject.GetComponent<RectangleBrick>() || collision2D.gameObject.GetComponent<SquareBrick>()))
        {
            Vector2 contactPoint = collision2D.GetContact(0).point;
            Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                contactPoint,
                1.5f
            );
            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent<RectangleBrick>(out RectangleBrick rectBrick))
                {
                    rectBrick.DestroyBrick();
                }
                if (hit.TryGetComponent<SquareBrick>(out SquareBrick squaBrick))
                {
                    squaBrick.DestroyBrick();
                }
            }
        }
        if (collision2D.gameObject.GetComponent<Paddle>())
        {
            float paddleWidth = collision2D.collider.bounds.size.x;
            float hitPoint = collision2D.contacts[0].point.x
            - collision2D.collider.bounds.center.x;
            float normalizeHit = hitPoint / (paddleWidth / 2);
            Vector2 direction = new Vector2(normalizeHit, 1).normalized;
            rb.velocity = direction * speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.GetComponent<WallBottom>())
        {
            OnBallLost?.Invoke(this, EventArgs.Empty);
        }
    }
    public void SetIsLaunched(bool launched)
    {
        isLaunched = launched;
    }
    public void IncreaseSpeed(float speed)
    {
        this.speed += speed;
    }
    public void DecreaseSpeed(float speed)
    {
        this.speed -= speed;
    }
    public void ResetSpeed()
    {
        this.speed = GameManager.Instance.ballSpeed;
    }
    public void CreateBall()
    {
        Vector3 vector3 = transform.position;
        GameObject newBallObj = Instantiate(gameObject, vector3, Quaternion.identity);
        Ball newBall = newBallObj.GetComponent<Ball>();
        newBall.isLaunched = true;
        newBall.rb.velocity = new Vector2(UnityEngine.Random.Range(-1f, 1f), 1).normalized * speed;
    }
}
