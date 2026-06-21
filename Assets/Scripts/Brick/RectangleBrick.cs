using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RectangleBrick : MonoBehaviour
{
    public static RectangleBrick Instance { get; private set; }
    private PowerUpType powerUpType = PowerUpType.None;
    public static event EventHandler OnDestroyRectangleBrick;
    private int hp;
    private Sprite normalSprite;
    private Sprite damagedSprite;
    private int brickScore;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        Instance = this;
        PowerUpManager.Instance.OnIsFireBall += RectangleBrick_OnIsFireBall;
        PowerUpManager.Instance.OffIsFireBall += RectangleBrick_OffIsFireBall;
    }
    private void OnDestroy()
    {
        if (PowerUpManager.Instance != null)
        {
            PowerUpManager.Instance.OnIsFireBall -= RectangleBrick_OnIsFireBall;
            PowerUpManager.Instance.OffIsFireBall -= RectangleBrick_OffIsFireBall;
        }
    }
    private void RectangleBrick_OffIsFireBall(object sender, EventArgs e)
    {
        boxCollider2D.isTrigger = false;
    }
    private void RectangleBrick_OnIsFireBall(object sender, EventArgs e)
    {
        boxCollider2D.isTrigger = true;
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (PowerUpManager.Instance.GetIsFireBall() && collision2D.gameObject.TryGetComponent<Ball>(out Ball ballFire))
        {
            return;
        }
        if (PowerUpManager.Instance.GetIsBombBall() && collision2D.gameObject.TryGetComponent<Ball>(out Ball ballBomb))
        {
            return;
        }
        if (collision2D.gameObject.TryGetComponent<Ball>(out Ball ball))
        {
            hp--;
            if (hp >= 2)
            {
                spriteRenderer.sprite = normalSprite;
            }
            else if (hp == 1)
            {
                spriteRenderer.sprite = damagedSprite;
            }
            else if (hp == 0)
            {
                DestroyBrick();
                return;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.GetComponent<Bullet>())
        {
            hp--;
            if (hp >= 2)
            {
                spriteRenderer.sprite = normalSprite;
            }
            else if (hp == 1)
            {
                spriteRenderer.sprite = damagedSprite;
            }
            else if (hp == 0)
            {
                DestroyBrick();
                return;
            }
        }
        if (PowerUpManager.Instance.GetIsFireBall() && collision2D.gameObject.TryGetComponent<Ball>(out Ball ballFire))
        {
            DestroyBrick();
            return;
        }
    }
    public void DestroyBrick()
    {
        GameManager.Instance.AddScore(brickScore);
        OnDestroyRectangleBrick?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
    public void SetSprites(Sprite normal, Sprite damaged, int hp)
    {
        spriteRenderer.sprite = hp >= 2 ? normal : damaged;
        normalSprite = normal;
        damagedSprite = damaged;
        this.hp = hp;
        brickScore = hp * 10;
    }
    public PowerUpType GetPowerUpType()
    {
        return powerUpType;
    }
    public void SetPowerUpType(PowerUpType powerUpType)
    {
        this.powerUpType = powerUpType;
    }
    public void OffIsTrigger()
    {
        boxCollider2D.isTrigger = false;
    }
}
