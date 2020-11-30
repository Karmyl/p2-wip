using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int id;
    private string playerName;
    private int score;
    private int avatarId;

    public int Id
    {
        get { return id;  }
        set { id = value; }
    }
    
    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    public int Score
    {
        get { return score;  }
        set { score = value; }
    }

    public int AvatarId
    {
        get { return avatarId;  }
        set { avatarId = value; }
    }

    public Player()
    {
        this.id = 0;
        this.playerName = "";
        this.score = 0;
        this.avatarId = 0;
    }

    public Player(int id, string playerName, int score, int avatarId)
    {
        this.id = id;
        this.playerName = playerName;
        this.score = score;
        this.avatarId = avatarId;
    }
}
