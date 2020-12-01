using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddProfile : MonoBehaviour
{
    public InputField input;
    private int selectedAvatarIndex = 0;

    public void SetSelectedAvatar(int avatarIndex)
    {
        selectedAvatarIndex = avatarIndex;
        Debug.Log(selectedAvatarIndex);
    }

    public void AddNewProfile()
    {
        Debug.Log(input.text);
        if (input.text.Length > 0 && selectedAvatarIndex != 0)
        {
            DatabaseLoader.CreateNewPlayer(input.text, selectedAvatarIndex);
        }
        Application.LoadLevel("Profiles");


    }
}
