using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangePlayerProfile : MonoBehaviour
{
    public Dropdown dropdown;
    private List<Player> players = null;
    private int selectedPlayerIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        players = DatabaseLoader.GetAllPlayers();
        List<string> names = new List<string>();
        foreach (Player p in players)
        {
            names.Add(p.PlayerName);
        }
        dropdown.AddOptions(names);

        // Select first item in list if names-array contains at least
        // one item
        if (names.Count > 0)
        {
            // Find current player and set that as selected
            Player currentPlayer = DatabaseLoader.GetCurrentPlayer();
            int currentPlayerId = currentPlayer.Id;
            bool found = false;
            for (int i = 0; i < players.Count && !found; i++)
            {
                if(players[i].Id == currentPlayerId)
                {
                    found = true;
                    dropdown.value = i;
                    selectedPlayerIndex = i;
                }
            }
        }
    }

    public void SelectedIndexChanged(int index)
    {
        selectedPlayerIndex = index;
    }

    public void ApplyPlayerChange()
    {
        if (selectedPlayerIndex != -1)
        {
            DatabaseLoader.SetCurrentPlayer(players[selectedPlayerIndex]);
            SceneManager.LoadScene("Profiles");
        }
    }
}
