using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SquareBrick : MonoBehaviour
{
    public static SquareBrick Instance { get; private set; }
    [SerializeField] private SpriteRenderer spriteRenderer;
    public static event EventHandler OnDestroySquareBrick;
    private PowerUpType powerUpType = PowerUpType.None;
    private int hp;
    private Sprite normalSprite;
    private int brickScore;
    private void Start()
    {
        Instance = this;
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
        if (collision2D.gameObject.TryGetComponent<Ball>(out Ball ball))
        {
            hp--;
            if (hp == 0)
            {
                GameManager.Instance.AddScore(brickScore);
                OnDestroySquareBrick?.Invoke(this, EventArgs.Empty);
                Destroy(gameObject);
            }
        }

    }
    public PowerUpType GetPowerUpType()
    {
        return powerUpType;
    }
    public void SetPowerUpType(PowerUpType powerUpType)
    {
        this.powerUpType = powerUpType;
    }
}
