using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public static Paddle Instance { get; private set; }
    private float speed = GameManager.Instance.paddleSpeed;
    private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D paddleCollider2D;
    [SerializeField] private SpriteRenderer paddleSprite;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Sprite[] sprites;
    private float targetX = 0f;
    private float timer = 0f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
        timer += Time.deltaTime;
        if (PowerUpManager.Instance.GetIsShootPaddle() && timer >= 0.5f)
        {
            CreateBullet();
            timer = 0f;
        }
    }

    public void PaddleWidthIncrease()
    {
        paddleSprite.sprite = sprites[1];
        paddleCollider2D.size = new Vector2(2f, paddleCollider2D.size.y);
    }
    public void PaddleWidthDecrease()
    {
        paddleSprite.sprite = sprites[2];
        paddleCollider2D.size = new Vector2(0.67f, paddleCollider2D.size.y);
    }
    public void ResetPaddleWidth()
    {
        paddleSprite.sprite = sprites[0];
        paddleCollider2D.size = new Vector2(1.4f, paddleCollider2D.size.y);
    }
    public void PaddleShoot()
    {
        paddleSprite.sprite = sprites[3];
        paddleCollider2D.size = new Vector2(1.4f, paddleCollider2D.size.y);

    }
    private void CreateBullet()
    {
        Vector2 leftBulletPosition = new Vector2(transform.position.x, transform.position.y) + new Vector2(-0.645f, 0.3f);
        Vector2 rightBulletPosition = new Vector2(transform.position.x, transform.position.y) + new Vector2(0.645f, 0.3f);
        Instantiate(bulletPrefab, leftBulletPosition, Quaternion.identity);
        Instantiate(bulletPrefab, rightBulletPosition, Quaternion.identity);
    }
}
