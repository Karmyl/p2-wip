using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{

    AudioManager music;
    AudioSource backgroundMusic;
    [SerializeField] Slider musicVolumeSlider;
   

    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private int firstPlayValue;
    private float musicVolumeValue;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void Awake()
    {
        //Get playerprefs to see if players first play
        music = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        backgroundMusic = music.backgroundMusic;

        if (firstPlayValue == 0)//set values
        {
            musicVolumeValue = backgroundMusic.volume;
            musicVolumeSlider.value = musicVolumeValue;

            PlayerPrefs.SetFloat(BackgroundPref, musicVolumeValue);
            PlayerPrefs.SetInt(FirstPlay, -1);

            SaveSoundSettings();
        }
        else //get values from playerprefs and set slider
        {
            musicVolumeValue = PlayerPrefs.GetFloat(BackgroundPref);
            musicVolumeSlider.value = musicVolumeValue;

            
        }

        Debug.Log(music.name);
    }
    //save sound values
    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(BackgroundPref, musicVolumeSlider.value);
    }

    //if player loses focus in the game -> save settings
    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            SaveSoundSettings();
        }
    }

    public void ChangeBackgroundVolume()
    {
        backgroundMusic.volume = musicVolumeSlider.value;
    }

    public void ChangeFXVolume()
    {
        //backgroundMusic.volume = musicVolumeSlider.value;
        foreach (Sound s in music.sounds)
        {
            float tmp = musicVolumeSlider.value - s.source.volume;
            s.source.volume = s.source.volume + tmp;
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
