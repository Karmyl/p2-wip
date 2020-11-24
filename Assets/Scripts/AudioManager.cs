﻿
using UnityEngine.Audio;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private int firstPlayValue;
    public Sound[] sounds;

    public AudioSource backgroundMusic;
    private float musicVolumeValue;

    
    private void Awake()
    {
        ContinueSettings();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.name = s.name;
            s.source.volume = s.volume;
        }

        Debug.Log(PlayerPrefs.GetInt("FirstPlay"));

    }

    //Keep sound volume settings alive
    private void ContinueSettings()
    {
        musicVolumeValue = PlayerPrefs.GetFloat(BackgroundPref);
        backgroundMusic.volume = musicVolumeValue;
    }

    //play sound effect by name
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if(FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }

        if (firstPlayValue == 0)//set values
        {
            musicVolumeValue = .5f;

            PlayerPrefs.SetFloat(BackgroundPref, musicVolumeValue);
            PlayerPrefs.SetInt(FirstPlay, -1);

        }
        else //get values from playerprefs and set slider
        {
            musicVolumeValue = PlayerPrefs.GetFloat(BackgroundPref);
        }
    }

    public void ChangeBackgroundMusic(AudioClip music)
    {
        if(backgroundMusic.clip.name == music.name)//Continue without changing music
        {
            return;
        } else //stop current music and start new bgmusic 
        {           
            backgroundMusic.Stop();
            backgroundMusic.clip = music;
            if (backgroundMusic.clip.name == "Game_Over_BGMusic")
            {
                backgroundMusic.loop = false;
            }
            backgroundMusic.Play();

        }
    }

}
