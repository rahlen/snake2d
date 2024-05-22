using CodeMonkey;
using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Snake : MonoBehaviour
{
    private enum Direction
    {
        Left ,
        Right ,
        Up ,
        Down
    }
    private enum State
    {
        Alive,
        Dead
    }
    private State state;
    private Direction gridMoveDirection;
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private LevelGrid levelgrid;
    private int snakebodysize;
    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;

    [SerializeField] private float snakeSpeed = 0.5f; 


    // Start is called before the first frame update

    public void Setup(LevelGrid levelgrid)
    {
        this.levelgrid = levelgrid;
    }
    private void Awake()
    {
        gridPosition = new Vector2Int(0, 0);
        gridMoveTimerMax = snakeSpeed;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = Direction.Right;
        snakeMovePositionList = new List<SnakeMovePosition>();
        snakebodysize = 0;

        snakeBodyPartList = new List<SnakeBodyPart>();

        state = State.Alive;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (state) {
            case State.Alive:
        Handleinput();
        HandleGridMovement();
                break;
            case State.Dead:
                break;
    }

}
    private void Handleinput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            {
                if (gridMoveDirection != Direction.Down)
                {
                    gridMoveDirection = Direction.Up;
                    
                }
               

            }
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection != Direction.Up)
            {
                gridMoveDirection = Direction.Down;

            }
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection != Direction.Right)
            {
                gridMoveDirection = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection != Direction.Left)
            {
                gridMoveDirection = Direction.Right;
            }
        }

    }
    private void HandleGridMovement()
    {
        
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;

            Sounds.Playsound(Sounds.Sound.SnakeMove);

            SnakeMovePosition previousSnakeMovePosition = null;
            if ( snakeMovePositionList.Count > 0)
            {
                previousSnakeMovePosition = snakeMovePositionList[0];
            }
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection );
            snakeMovePositionList.Insert(0, snakeMovePosition);

            Vector2Int gridMoveDirectionVector;
            switch (gridMoveDirection)
            {
                default:
                case Direction.Right: gridMoveDirectionVector = new Vector2Int(+1, 0); break;
                case Direction.Left: gridMoveDirectionVector = new Vector2Int(-1, 0); break;
                case Direction.Up: gridMoveDirectionVector = new Vector2Int(0, +1); break;
                case Direction.Down: gridMoveDirectionVector = new Vector2Int(0, -1); break;
            }

            
            gridPosition += gridMoveDirectionVector;
            
            gridPosition = levelgrid.ValidateGridPosition(gridPosition);
            
            bool snakeAteFood = levelgrid.TrySnakeEatFood(gridPosition);
            if (snakeAteFood)
            {
                snakebodysize++;
                CreateSnakeBody();
                Sounds.Playsound(Sounds.Sound.SnakeEat);
            }

            if (snakeMovePositionList.Count >= snakebodysize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            UpdateSnakeBodyParts();

            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList)
            {
                Vector2Int snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
                if (gridPosition == snakeBodyPartGridPosition)
                {
                    // game over
                   // CMDebug.TextPopup("DEAD!", transform.position);
                    state = State.Dead;
                    GameHandler.SnakeDied();
                    Sounds.Playsound(Sounds.Sound.SnakeDie);
                }
            }
            
            

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) -90 );
            




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
            snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
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
    public List <Vector2Int> GetFullSnakeGridPositionList ()
    {

        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList)
        {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        return gridPositionList;
    }
    private class SnakeBodyPart
    {
        private SnakeMovePosition snakeMovePosition;
        private Transform transform;
        public SnakeBodyPart(int bodyIndex) {
            GameObject snakeBodyGameObject = new GameObject("Snakebody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            transform = snakeBodyGameObject.transform;

        }
        public void SetSnakeMovePosition (SnakeMovePosition snakeMovePosition)
        {
            this.snakeMovePosition = snakeMovePosition;
            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);
            float angle;
            switch (snakeMovePosition.GetDirection())
            {
                default:
                case Direction.Up:
                
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 0; break;

                        case Direction.Left:
                            angle = 0 + 45; break;

                        case Direction.Right:
                            angle = 0 - 45; break;
                    }
                    break;
                case Direction.Down:
                    
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 180; break;

                        case Direction.Left:
                            angle = 180 - 45; break;

                        case Direction.Right:
                            angle = 180 + 45; break;
                    }
                    break;

                case Direction.Left:

                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = -90; break;

                        case Direction.Down:
                            angle = -45; break;

                        case Direction.Up:
                            angle = 45; break;
                    }
                    break;

                case Direction.Right:
                    
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 90; break;

                        case Direction.Down:
                            angle = 45; break;

                        case Direction.Up:
                            angle = -45; break;
                    }
                break;

            }
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
       public Vector2Int GetGridPosition()
        {
            return snakeMovePosition.GetGridPosition();
        }
    }

    private class SnakeMovePosition
    {
        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private Direction direction;

        public SnakeMovePosition (SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition , Direction direction )
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }
        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }
        public Direction GetDirection()
        {
            return direction;
        }
        public Direction GetPreviousDirection()
        {
            if (previousSnakeMovePosition == null)
            {
                return Direction.Right;
            }
            else
            {
                return previousSnakeMovePosition.direction;
            }
        }
    }

}

