using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SquareBrick : MonoBehaviour
{
    public static SquareBrick Instance { get; private set; }
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider2D;
    public static event EventHandler OnDestroySquareBrick;
    private PowerUpType powerUpType = PowerUpType.None;
    private int hp;
    private Sprite normalSprite;
    private int brickScore;
    private void Start()
    {
        Instance = this;
        PowerUpManager.Instance.OnIsFireBall += PowerUpManager_OnIsFireBall;
        PowerUpManager.Instance.OffIsFireBall += PowerUpManager_OffIsFireBall;
    }
    private void OnDestroy()
    {
        if (PowerUpManager.Instance != null)
        {
            PowerUpManager.Instance.OnIsFireBall -= PowerUpManager_OnIsFireBall;
            PowerUpManager.Instance.OffIsFireBall -= PowerUpManager_OffIsFireBall;
        }
    }

    private void PowerUpManager_OffIsFireBall(object sender, EventArgs e)
    {
        boxCollider2D.isTrigger = false;
    }

    private void PowerUpManager_OnIsFireBall(object sender, EventArgs e)
    {
        boxCollider2D.isTrigger = true;
    }

    public void SetSprites(Sprite normal, int hp)
    {
        spriteRenderer.sprite = normal;
        normalSprite = normal;
        this.hp = hp;
        brickScore = hp * 10;
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
            if (hp == 0)
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
            if (hp == 0)
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
        OnDestroySquareBrick?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
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
