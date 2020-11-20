
using UnityEngine.Audio;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public AudioSource backgroundMusic;

    private void Awake()
    { 
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.name = s.name;
            s.source.volume = s.volume;

        }
    }

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
            if (backgroundMusic.clip.name == "Game_Over_BGMusic")
            {
                backgroundMusic.loop = false;
            }
            backgroundMusic.Play();

        }
    }

}
