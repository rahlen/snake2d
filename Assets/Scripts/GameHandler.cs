/* 
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

    private static GameHandler instance;

    private static int score = 0;
    
    [SerializeField] private Snake snake;
    private LevelGrid levelGrid;

    private void Awake()
    {
        instance = this;
        InitializeStatic();
    }
    private void Start() {
    
    Debug.Log("GameHandler.Start");

        GameObject snakeHeadGameObject = new GameObject();
        SpriteRenderer snakeSpriteRenderer = snakeHeadGameObject.AddComponent<SpriteRenderer>();
        snakeSpriteRenderer.sprite = GameAssets.i.snakeHeadSprite;

        levelGrid = new LevelGrid(20, 20);
        snake.Setup(levelGrid);
        levelGrid.Setup(snake);

        //CMDebug.ButtonUI(Vector2.zero, "Reload Scene", () =>
        //{
          //  Loader.Load(Loader.Scene.SampleScene);
        //});
    }
    public static int GetScore()
    {
        return score;
    }

    public static void AddScore()
    {
        score += 100;
        
    }

    private static void InitializeStatic()
    {
        score = 0;
    }

    public static void SnakeDied()
    {
        GameOverWindow.ShowStatic();
    }
}
