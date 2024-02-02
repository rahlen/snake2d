using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Snake : MonoBehaviour
{
    private Vector2Int gridMoveDirection;
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    // Start is called before the first frame update
    private void Awake()
    {
        gridPosition = new Vector2Int(0, 0);
        gridMoveTimerMax = 1f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);
    }

    // Update is called once per frame
    private void Update()
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
        if(Input.GetKeyDown(KeyCode.A))
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
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridPosition += gridMoveDirection;
            gridMoveTimer -= gridMoveTimerMax;
        }
        transform.position = new Vector3(gridPosition.x, gridPosition.y);
    }
}
