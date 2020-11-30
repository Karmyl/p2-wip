
using UnityEngine.Audio;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string FxVolumePref = "FxVolumePref";

    private int firstPlayValue;
    public Sound[] sounds;

    public AudioSource backgroundMusic;
    private float musicVolumeValue;
    private float fxVolumeValue;

    
    private void Awake()
    {
        ContinueSettings();

        Debug.Log(PlayerPrefs.GetInt("FirstPlay"));

    }

    //Keep sound volume settings alive
    private void ContinueSettings()
    {
        musicVolumeValue = PlayerPrefs.GetFloat(BackgroundPref);
        fxVolumeValue = PlayerPrefs.GetFloat(FxVolumePref);

        backgroundMusic.volume = musicVolumeValue;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.name = s.name;
            s.source.volume = s.volume + (fxVolumeValue - 0.5f);
        }
    }

    //play sound effect by name
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    //Stop playing sound effect 
    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    void Start()
    {

        DontDestroyOnLoad(gameObject);

        if(FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }

        //check if first play ---------------------korjaus
        firstPlayValue = PlayerPrefs.GetInt(FirstPlay);
        
        if (firstPlayValue == 0)//set values
        {
            backgroundMusic.volume = .5f;
            fxVolumeValue = .5f;
            musicVolumeValue = backgroundMusic.volume;

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.name = s.name;
                s.source.loop = s.loop;
                s.source.volume = s.volume;
            }

            PlayerPrefs.SetFloat(BackgroundPref, musicVolumeValue);
            PlayerPrefs.SetFloat(FxVolumePref, fxVolumeValue);
            PlayerPrefs.SetInt(FirstPlay, -1);       
        }
        
    }

    //Stop sound
    public void StopSound(AudioClip clip)
    {
       
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
