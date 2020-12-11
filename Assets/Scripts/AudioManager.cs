
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string FxVolumePref = "FxVolumePref";

    private int firstPlayValue;
    public Music[] musics;
    public Sound[] sounds;

    public AudioSource backgroundMusic;
    private float musicVolumeValue;
    private float fxVolumeValue;

    Scene currentScene;


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

        //Set backgroundmusic values
        foreach (Music m in musics)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.name = m.name;
            m.source.loop = m.loop;
            m.source.volume = m.volume * musicVolumeValue;
        }

        //Set soundeffect values
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.name = s.name;
            s.source.volume = s.volume * fxVolumeValue;
        }
    }

    //play sound effect by name
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    //Set new backgroundmusic by name
    public void SetMusic(string name)
    {
        Music m = Array.Find(musics, music => music.name == name);
        backgroundMusic = m.source;
        backgroundMusic.name = m.name;
        backgroundMusic.clip = m.clip;

    }

    //Stop playing sound effect 
    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    //Play backgroundmusic by name
    public void StopMusic(string name)
    {
        Music m = Array.Find(musics, music => music.name == name);
        m.source.Stop();
    }

    void Start()
    {
        Debug.Log(">>>>>>>>POINT");
        currentScene = SceneManager.GetActiveScene();

        //do not destroy game object if not dublicated
        DontDestroyOnLoad(gameObject);

        if(FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }

        //check if first play
        firstPlayValue = PlayerPrefs.GetInt(FirstPlay);
        
        if (firstPlayValue == 0)//set values
        {

            musicVolumeValue = 1.0f;
            fxVolumeValue = 1.0f;

            foreach (Music m in musics)
            {
                m.source = gameObject.AddComponent<AudioSource>();
                m.source.clip = m.clip;
                m.source.name = m.name;
                m.source.loop = m.loop;
                m.source.volume = m.volume;
            }

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

        string sceneName = currentScene.name;
        ChangeBackgroundMusic(sceneName);

    }


    public void ChangeBackgroundMusic(string music)
    {
        if(backgroundMusic.name == music)//Continue without changing music
        {
            return;
        } else //stop current music and start new bgmusic 
        {                    
            backgroundMusic.Stop();
            SetMusic(music);

            if (backgroundMusic.name == "GameOver")
            {
                backgroundMusic.loop = false;
            }

            backgroundMusic.Play();

        }
    }

}
