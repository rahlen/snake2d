using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{
    private TMP_Text scoreText;

    private void Awake()
    {
     
    scoreText = GameObject.Find("scoreText").GetComponent<TMP_Text>();
     //Debug.Log(scoreText);
    }

    private void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = GameHandler.GetScore().ToString();
            
        }
    }
}
