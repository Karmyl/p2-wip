﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static Text text;
    public static int score;
    public static int numChickens;

    void Start()
    {
        Debug.Log("Nyt");
        //Set score to zero.
        Score.score = 0;
    }

    void Update()
    {

    }

    // Find ScoreText component and add value to it.
    public static void AddScore(int value)
    {

        Score.score += value;
        GameObject go = GameObject.Find("ScoreText");
        if (go)
        {
          Score.text = go.GetComponent<Text>();
          Score.text.text = Score.score.ToString();
        }
    }

}