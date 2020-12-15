using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScore : MonoBehaviour
{
    public TMP_Text nameText;
    public GameObject avatar;
    public int avatarID;
    private int displayScore;
    public TMP_Text scoreUI;
    AudioManager audiomanager;
    public GameObject[] avatarPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
        displayScore = 0;
        StartCoroutine(ScoreUpdater());
        nameText.text = DatabaseLoader.GetCurrentPlayer().PlayerName;
        avatarID = DatabaseLoader.GetCurrentPlayer().AvatarId;

        avatar = avatarPrefabs[avatarID];
        GameObject go = Instantiate(avatar, avatar.transform);
        //avatarPrefabs[0] = avatar;
    }

    private IEnumerator ScoreUpdater()
    {
        bool isDone = false;
        while (!isDone)
        {
            if (displayScore < Score.score)
            {
                displayScore++;
                audiomanager.PlaySound("totalCoins");
                scoreUI.text = displayScore.ToString();
            } else
            {
                audiomanager.PlaySound("chickenGetsHome");
                isDone = true;
            }
            yield return new WaitForSeconds(0.1f);
        }

        // Save scores to database
        Player player = DatabaseLoader.GetCurrentPlayer();
        player.Score += Score.score;

        DatabaseLoader.SaveCurrentPlayer();
        Debug.Log("id: " + player.Id + ", name: " + player.PlayerName + " avatarId: " + player.AvatarId + ", score: " + player.Score);
    }
}