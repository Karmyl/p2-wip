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
        bool isDone = false;
        while (!isDone)
        {
            if (displayScore < Score.score)
            {
                displayScore++;
                FindObjectOfType<AudioManager>().PlaySound("totalCoins");
                scoreUI.text = displayScore.ToString();
            } else
            {
                FindObjectOfType<AudioManager>().PlaySound("chickenGetsHome");
                isDone = true;
            }
            yield return new WaitForSeconds(0.1f);
        }

        // Save scores to database
        Player player = DatabaseLoader.GetCurrentPlayer();
        player.Score += Score.score;

        DatabaseLoader.SaveCurrentPlayer();
        Debug.Log("id: " + player.Id + ", name: " + player.PlayerName + "avatarId: " + player.AvatarId + ", score: " + player.Score);
    }
}