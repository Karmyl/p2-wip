using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
    //private static Text text;
    private int displayScore;
    public Text scoreUI;
    // Start is called before the first frame update
    void Start()
    {
        displayScore = 0;
        StartCoroutine(ScoreUpdater());
    }

    private IEnumerator ScoreUpdater()
    {
        while (true)
        {
            if (displayScore < Score.score)
            {
                displayScore++;
                scoreUI.text = displayScore.ToString();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}