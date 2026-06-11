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
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void Start()
    {
        Instance = this;
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
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
                GameManager.Instance.AddScore(brickScore);
                OnDestroyRectangleBrick?.Invoke(this, EventArgs.Empty);
                Destroy(gameObject);
            }
        }
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
}
