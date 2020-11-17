using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickensAreHome : MonoBehaviour
{
    public int chickensNeeded;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Score.score >= chickensNeeded) 
        {
            Time.timeScale = 0;
        }
    }
}
