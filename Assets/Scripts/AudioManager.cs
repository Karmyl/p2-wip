using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource backgroundMusic;

    private void Awake()
    { 

    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if(FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeBackgroundMusic(AudioClip music)
    {
        if(backgroundMusic.clip.name == music.name)
        {
            return;
        } else
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = music;
            backgroundMusic.Play();

        }
    }

}
