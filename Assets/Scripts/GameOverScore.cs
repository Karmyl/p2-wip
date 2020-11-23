using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
    private static Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject go = GameObject.Find("CoinsText");
        if (go)
        {
            GameOverScore.text = go.GetComponent<Text>();
            GameOverScore.text.text = Score.score.ToString();
        }
    }
}
