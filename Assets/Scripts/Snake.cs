using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Snake : MonoBehaviour
{
    private Vector2Int gridMoveDirection;
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private LevelGrid levelgrid;
    private int snakebodysize;
    private List<Vector2Int> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;


    // Start is called before the first frame update

    public void Setup(LevelGrid levelgrid)
    {
        this.levelgrid = levelgrid;
    }
    private void Awake()
    {
        gridPosition = new Vector2Int(0, 0);
        gridMoveTimerMax = 0.5f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);
        snakeMovePositionList = new List<Vector2Int>();
        snakebodysize = 0;

        snakeBodyPartList = new List<SnakeBodyPart>(); 
    }

    // Update is called once per frame
    private void Update()
    {
        Handleinput();
        HandleGridMovement();
        
    }
    private void Handleinput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            {
                if (gridMoveDirection.y != -1)
                {
                    gridMoveDirection.y = +1;
                    gridMoveDirection.x = 0;
                }
                gridMoveDirection.y = +1;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (gridMoveDirection.y != +1)
            {
                gridMoveDirection.y = -1;
                gridMoveDirection.x = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (gridMoveDirection.x != +1)
            {
                gridMoveDirection.y = 0;
                gridMoveDirection.x = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (gridMoveDirection.x != -1)
                gridMoveDirection.x += 1;
            gridMoveDirection.y = 0;
        }
        
    }
    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;
            snakeMovePositionList.Insert(0, gridPosition);
            gridPosition += gridMoveDirection;
            
            bool snakeAteFood = levelgrid.TrySnakeEatFood(gridPosition);
            if (snakeAteFood)
            {
                snakebodysize++;
                CreateSnakeBody();
            }

            if (snakeMovePositionList.Count >= snakebodysize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            /*for (int i = 0; i < snakeMovePositionList.Count; i++)
            {
                Vector2Int snakeMovePosition = snakeMovePositionList[i];
                World_Sprite worldSprite = World_Sprite.Create(new Vector3(snakeMovePosition.x, snakeMovePosition.y), Vector3.one * .5f, Color.white);
                FunctionTimer.Create(worldSprite.DestroySelf, gridMoveTimerMax);

               }*/
            
                
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) -90 );


            UpdateSnakeBodyParts();
          
                
        }
      

    }
    private void CreateSnakeBodyPart()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));
    }
    private void UpdateSnakeBodyParts()
    {
        for (int i = 0; i < snakeBodyPartList.Count; i++)
        {

            Vector3 snakBodyPosition = new Vector3(snakeMovePositionList[i].x, snakeMovePositionList[i].y);
            snakeBodyPartList[i].SetGridPosition(snakeMovePositionList[i]);
        }

    }
    private void CreateSnakeBody()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));

    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
    public Vector2Int GetGridposition()
    {
        return gridPosition;
    } 
    public List <Vector2Int> GetFullSnakePositionList ()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        gridPositionList.AddRange(snakeMovePositionList);
        return gridPositionList;
    }
    private class SnakeBodyPart
    {
        private Vector2Int gridPosition;
        private Transform transform;
        public SnakeBodyPart(int bodyIndex) {
            GameObject snakeBodyGameObject = new GameObject("Snakebody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            transform = snakeBodyGameObject.transform;

        }
        public void SetGridPosition (Vector2Int gridPosition)
        {
            this.gridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }
    }

}

