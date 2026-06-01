using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private int hp;
    private Sprite normalSprite;
    private Sprite damagedSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void SetSprites(Sprite normal, Sprite damaged, int hp)
    {
        spriteRenderer.sprite = hp >= 2 ? normal : damaged;
        normalSprite = normal;
        damagedSprite = damaged;
        this.hp = hp;
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
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
