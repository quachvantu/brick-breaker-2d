using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBrick : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private int hp;
    private Sprite normalSprite;
    private int brickScore;

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
                Destroy(gameObject);
            }
        }

    }
}
