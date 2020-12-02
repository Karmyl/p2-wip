using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeletePlayerProfile : MonoBehaviour
{
    public Dropdown dropdown;
    private int selectedPlayerIndex = -1;
    private List<Player> players = null;

    // Start is called before the first frame update
    void Start()
    {
        players = DatabaseLoader.GetAllPlayers();
        List<string> names = new List<string>();
        foreach(Player p in players)
        {
            names.Add(p.PlayerName);
        }

        // Do not allow deleting only player in database.
        if(names.Count > 1)
        {
            selectedPlayerIndex = 0;
        }

        dropdown.AddOptions(names);
    }

    public void UpdateSelectedPlayerIndex(int index)
    {
        Debug.Log("index: " + index);
        selectedPlayerIndex = index;
    }

    public void DeleteSelectedPlayerProfile()
    {
        if(selectedPlayerIndex != -1)
        {
            DatabaseLoader.DeletePlayer(players[selectedPlayerIndex]);
            Application.LoadLevel("Profiles");
        }
    }
}
