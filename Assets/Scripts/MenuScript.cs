using System.Collections;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    AudioManager audiomanager;
    private void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
    }
    public void ChangeScene(string sceneName)
    {
        Application.LoadLevel(sceneName);

        audiomanager.PlaySound("buttonSoundFX");
        
        if (sceneName == "Gamelevel")
        {
            audiomanager.PlaySound("chickens");
        }
        else if (sceneName == "MainMenu")
        {
            audiomanager.StopSound("chickens");
        } 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
