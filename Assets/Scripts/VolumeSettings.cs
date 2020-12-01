using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{

    AudioManager music;
    AudioSource backgroundMusic;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider fxVolumeSlider;
   

    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string FxVolumePref = "FxVolumePref";
    private int firstPlayValue;
    private float musicVolumeValue;
    private float fxVolumeValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        //Get playerprefs to see if players first play
        music = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        backgroundMusic = music.backgroundMusic;

        firstPlayValue = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayValue == 0)//set values
        {
            musicVolumeValue = .5f;
            fxVolumeValue = .5f;
            musicVolumeSlider.value = musicVolumeValue;
            fxVolumeSlider.value = fxVolumeValue;

            PlayerPrefs.SetFloat(BackgroundPref, musicVolumeValue);
            PlayerPrefs.SetFloat(FxVolumePref, fxVolumeValue);
            PlayerPrefs.SetInt(FirstPlay, -1);

            SaveSoundSettings();
        }
        else //get values from playerprefs and set slider
        {
            musicVolumeValue = PlayerPrefs.GetFloat(BackgroundPref);
            musicVolumeSlider.value = musicVolumeValue;

            fxVolumeValue = PlayerPrefs.GetFloat(FxVolumePref);
            fxVolumeSlider.value = fxVolumeValue; 
            
        }
    }

    //check when slider is released
    public void SliderIsReleased()
    {
        music.PlaySound("buttonSoundFX");
    }


    //Listen when player drops slider handle 
    public void DragHasEnded()
    {
        music.PlaySound("buttonSoundFX");
    }

    //save sound values
    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(BackgroundPref, musicVolumeSlider.value);
        Debug.Log("asetukset tallennettu!");
        Debug.Log(fxVolumeSlider.value);

        PlayerPrefs.SetFloat(FxVolumePref, fxVolumeSlider.value);
    }

    //if player loses focus in the game -> save settings
    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            SaveSoundSettings();
        }
    }

    // Update is called once per frame
    void Update()
    {

        backgroundMusic.volume = musicVolumeSlider.value;

        foreach (Sound s in music.sounds)
        {
            float tmp = fxVolumeSlider.value - 0.5f;
            s.source.volume = s.volume + tmp;
        }

        SaveSoundSettings();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
