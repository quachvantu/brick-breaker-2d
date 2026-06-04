using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickGrid : MonoBehaviour
{
    [SerializeField] private Sprite[] normalSprites;
    [SerializeField] private Sprite[] damagedSprites;
    [SerializeField] private Sprite[] squareNormalSprites;
    [SerializeField] private GameObject rectangleBrickPrefab;
    [SerializeField] private GameObject squareBrickPrefab;
    private GameObject selectedBrickPrefab;
    private int rows = 5;
    private int rectangleBrickColumns = 10;
    private int squareBrickColumns = 25;
    private float spacingX = 0.1f;
    private float spacingY = 0.1f;
    private float brickWidth;
    private float brickHeight;
    private void Start()
    {
        CreateBrickGrid();
    }
    private void CreateBrickGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            int columns = 0;
            int randomBrickTypeIndex = Random.Range(0, 2);
            if (randomBrickTypeIndex == 0)
            {
                selectedBrickPrefab = rectangleBrickPrefab;
                columns = rectangleBrickColumns;
            }
            else
            {
                selectedBrickPrefab = squareBrickPrefab;
                columns = squareBrickColumns;
            }
            SpriteRenderer spriteRenderer = selectedBrickPrefab.GetComponentInChildren<SpriteRenderer>();
            brickWidth = spriteRenderer.bounds.size.x;
            brickHeight = spriteRenderer.bounds.size.y;
            bool isRectangleBrick = randomBrickTypeIndex == 0;
            for (int col = 0; col < columns; col++)
            {
                float x = transform.position.x + col * (spacingX + brickWidth);
                float y = transform.position.y + row * -(spacingY + brickHeight);
                int randomColor = Random.Range(0, normalSprites.Length);
                int randomHP = Random.Range(1, 3);
                GameObject brickObject = Instantiate(selectedBrickPrefab, new Vector3(x, y, 0), Quaternion.identity);
                if (isRectangleBrick)
                {
                    RectangleBrick rectangleBrick = brickObject.GetComponent<RectangleBrick>();
                    rectangleBrick.SetSprites(normalSprites[randomColor], damagedSprites[randomColor], randomHP);
                }
                else
                {
                    SquareBrick squareBrick = brickObject.GetComponent<SquareBrick>();
                    squareBrick.SetSprites(squareNormalSprites[randomColor], randomHP);
                }

            }
        }
    }
}
