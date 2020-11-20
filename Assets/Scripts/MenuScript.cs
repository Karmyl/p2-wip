using System.Collections;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Application.LoadLevel(sceneName);

        FindObjectOfType<AudioManager>().PlaySound("buttonSoundFX");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
