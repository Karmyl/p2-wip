using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusicOnLoad : MonoBehaviour
{
    private AudioManager audiomanager;
    public string trackName;
    // Start is called before the first frame update
    void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();

        if (trackName != null)
        {
            audiomanager.ChangeBackgroundMusic(trackName);

            if (trackName == "GameOver")
            {
                audiomanager.backgroundMusic.loop = false;
            } else 
            {
                audiomanager.backgroundMusic.loop = true;

            }

        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
