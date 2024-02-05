using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class LevelGrid
{

    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;
    private int width;
    private int height;
    private Snake snake;

    public LevelGrid(int width , int height)
    {
        this.width = width;
        this.height = height;
        SpawnFood();
        FunctionPeriodic.Create(SpawnFood, 1f);
    }
    public void Setup(Snake snake)
    {
        this.snake = snake;
    }
    private void SpawnFood()
    {
        foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));

        GameObject foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }
    public void SnakeMoved(Vector2Int SnakeGridPosition)
    {
        if (SnakeGridPosition == foodGridPosition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            CMDebug.TextPopupMouse("Snake Ate Food");
        }
    }
}
