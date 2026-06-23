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
    [SerializeField] private Animator paddleAnimator;
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
        paddleSprite.size += new Vector2(0.6f, 0f);
        paddleCollider2D.size += new Vector2(0.6f, 0f);
    }
    public void PaddleWidthDecrease()
    {
        paddleSprite.size -= new Vector2(0.6f, 0f);
        paddleCollider2D.size -= new Vector2(0.6f, 0f);
    }
    public void ResetPaddleWidth()
    {
        paddleAnimator.SetBool("Shoot", false);
        paddleSprite.size = new Vector2(1.4f, 0.37f);
        paddleCollider2D.size = new Vector2(1.4f, 0.37f);
    }
    public void PaddleShoot()
    {
        paddleAnimator.SetBool("Shoot", true);
    }
    private void CreateBullet()
    {
        Vector2 leftBulletPosition = new Vector2(transform.position.x, transform.position.y) + new Vector2(-0.645f, 0.3f);
        Vector2 rightBulletPosition = new Vector2(transform.position.x, transform.position.y) + new Vector2(0.645f, 0.3f);
        Instantiate(bulletPrefab, leftBulletPosition, Quaternion.identity);
        Instantiate(bulletPrefab, rightBulletPosition, Quaternion.identity);
    }
}
