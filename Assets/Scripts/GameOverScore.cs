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
        bool isDone = true;
        while (isDone)
        {
            if (displayScore < Score.score)
            {
                displayScore++;
                FindObjectOfType<AudioManager>().PlaySound("totalCoins");
                scoreUI.text = displayScore.ToString();
            } else
            {
                FindObjectOfType<AudioManager>().PlaySound("chickenGetsHome");
                isDone = false;
            }
            yield return new WaitForSeconds(0.1f);
        }

    }
}