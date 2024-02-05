﻿/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
using System.Runtime.CompilerServices;

public class GameHandler : MonoBehaviour {

    [SerializeField] private Snake snake;
    private LevelGrid levelgrid;
    private void Start() {
    
    Debug.Log("GameHandler.Start");

        //GameObject snakeHeadGameObject = new GameObject();
        //SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        //snakeSpriteRenderer.sprite = GameAssets.i.snakeHeadSprite;

        levelgrid = new LevelGrid(20, 20);
        snake.Setup(levelgrid);
        levelgrid.Setup(snake);
    }

}
