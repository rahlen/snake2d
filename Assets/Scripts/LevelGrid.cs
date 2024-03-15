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
        
    }
    public void Setup(Snake snake)
    {
        this.snake = snake;
        SpawnFood();
    }
    private void SpawnFood()    
    {
        do
        {
            foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakePositionList().IndexOf (foodGridPosition) != -1 );
        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }
    public bool TrySnakeEatFood(Vector2Int SnakeGridPosition)
    {
        if (SnakeGridPosition == foodGridPosition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            return true;
            //CMDebug.TextPopupMouse("Snake Ate Food");
        }
        else
        {
            return false;
        }
    }
}
