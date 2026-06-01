using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGird : MonoBehaviour
{
    [SerializeField] private Sprite[] normalSprites;
    [SerializeField] private Sprite[] damagedSprites;
    [SerializeField] private GameObject brickPrefabs;
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 10;
    [SerializeField] private float brickWidth = 1f;
    [SerializeField] private float brickHeight = 0.5f;

    private void Start()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float x = transform.position.x + col * brickWidth;
                float y = transform.position.y + row * -brickHeight;
                int randomColor = Random.Range(0, normalSprites.Length);
                int randomHP = Random.Range(1, 3);
                GameObject brickObject = Instantiate(brickPrefabs, new Vector3(x, y, 0), Quaternion.identity);
                Brick brick = brickObject.GetComponent<Brick>();
                brick.SetSprites(normalSprites[randomColor], damagedSprites[randomColor], randomHP);
            }
        }
    }
}
