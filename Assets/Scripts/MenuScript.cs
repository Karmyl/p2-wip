using System.Collections;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Application.LoadLevel(sceneName);

        FindObjectOfType<AudioManager>().PlaySound("buttonSoundFX");
        
        if (sceneName == "Gamelevel")
        {
            FindObjectOfType<AudioManager>().PlaySound("chickens");
        }
        else if (sceneName == "MainMenu")
        {
            FindObjectOfType<AudioManager>().StopSound("chickens");
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
