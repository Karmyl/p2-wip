﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        dropdown.AddOptions(names);

        // Do not allow deleting only player in database.
        if (names.Count > 1)
        {
            // Set current player as current in list
            Player currentPlayer = DatabaseLoader.GetCurrentPlayer();
            bool found = false;
            for(int i = 0; i < players.Count && !found; i++)
            {
                if(players[i].Id == currentPlayer.Id)
                {
                    found = true;
                    dropdown.value = i;
                    selectedPlayerIndex = i;
                }
            }
        }
    }

    public void UpdateSelectedPlayerIndex(int index)
    {
        Debug.Log("index: " + index);
        selectedPlayerIndex = index;
    }

    public void DeleteSelectedPlayerProfile()
    {
        // Do not allow deleting last player profile
        if (selectedPlayerIndex != -1)
        {
            if (players.Count > 1)
            {
                DatabaseLoader.DeletePlayer(players[selectedPlayerIndex]);

                // Set next one in players as current player
                DatabaseLoader.SetCurrentPlayer(players[0]);
                SceneManager.LoadScene("Profiles");
            }
        }
    }
}
