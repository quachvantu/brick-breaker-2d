using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BrickGrid : MonoBehaviour
{
    [SerializeField] private Sprite[] normalSprites;
    [SerializeField] private Sprite[] damagedSprites;
    [SerializeField] private Sprite[] squareNormalSprites;
    [SerializeField] private Sprite[] powerUpSprites;

    [SerializeField] private GameObject rectangleBrickPrefab;
    [SerializeField] private GameObject squareBrickPrefab;
    [SerializeField] private GameObject powerUpPrefab;
    private List<GameObject> spawnedBricks = new List<GameObject>();
    private PowerUpType[] allTypes;
    private List<PowerUpType> listPowerUpTypes;
    private GameObject selectedBrickPrefab;
    private int rows = 5;
    private int rectangleBrickColumns = 10;
    private int squareBrickColumns = 25;
    private float spacingX = 0.1f;
    private float spacingY = 0.1f;
    private float brickWidth;
    private float brickHeight;
    private void Awake()
    {
        allTypes = (PowerUpType[])System.Enum.GetValues(typeof(PowerUpType));
        listPowerUpTypes = new List<PowerUpType>(allTypes);
        listPowerUpTypes.Remove(PowerUpType.None);
    }
    private void Start()
    {
        CreateBrickGrid();
        AssignPowerUps();
        RectangleBrick.OnDestroyRectangleBrick += RectangleBrick_OnDestroyRectangleBrick;
        SquareBrick.OnDestroySquareBrick += SquareBrick_OnDestroySquareBrick;
    }
    private void OnDestroy()
    {
        RectangleBrick.OnDestroyRectangleBrick -= RectangleBrick_OnDestroyRectangleBrick;
        SquareBrick.OnDestroySquareBrick -= SquareBrick_OnDestroySquareBrick;
    }
    private void SquareBrick_OnDestroySquareBrick(object sender, System.EventArgs e)
    {
        SquareBrick brick = (SquareBrick)sender;
        if (brick.GetPowerUpType() == PowerUpType.None)
        {
            return;
        }
        GameObject powerUp = Instantiate(powerUpPrefab, brick.transform.position, Quaternion.identity);
        powerUp.GetComponentInChildren<SpriteRenderer>().sprite = powerUpSprites[(int)brick.GetPowerUpType()];
        powerUp.GetComponent<PowerUp>().SetPowerUpType(brick.GetPowerUpType());
    }
    private void RectangleBrick_OnDestroyRectangleBrick(object sender, System.EventArgs e)
    {
        RectangleBrick brick = (RectangleBrick)sender;
        if (brick.GetPowerUpType() == PowerUpType.None)
        {
            return;
        }
        GameObject powerUp = Instantiate(powerUpPrefab, brick.transform.position, Quaternion.identity);
        powerUp.GetComponentInChildren<SpriteRenderer>().sprite = powerUpSprites[(int)brick.GetPowerUpType()];
        powerUp.GetComponent<PowerUp>().SetPowerUpType(brick.GetPowerUpType());
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
                spawnedBricks.Add(brickObject);
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
    private void AssignPowerUps()
    {
        List<GameObject> bricksCopy = new List<GameObject>(spawnedBricks);
        foreach (PowerUpType type in listPowerUpTypes)
        {
            int randomBricks = Random.Range(0, bricksCopy.Count);
            GameObject brick = bricksCopy[randomBricks];
            brick.GetComponent<RectangleBrick>()?.SetPowerUpType(type);
            brick.GetComponent<SquareBrick>()?.SetPowerUpType(type);
            bricksCopy.RemoveAt(randomBricks);
        }
    }
}
