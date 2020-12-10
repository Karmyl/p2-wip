using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int id;
    private string playerName;
    private int score;
    private int avatarId;
    private int skipIntroLevel1;
    private int skipIntroLevel2;
    private int skipIntroLevel3;

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

    public int AvatarId
    {
        get { return avatarId; }
        set { avatarId = value; }
    }

    public int Score
    {
        get { return score;  }
        set { score = value; }
    }

    public int SkipIntroLevel1
    {
        get { return skipIntroLevel1;  }
        set { skipIntroLevel1 = value; }
    }
    public int SkipIntroLevel2
    {
        get { return skipIntroLevel2; }
        set { skipIntroLevel2 = value; }
    }
    public int SkipIntroLevel3
    {
        get { return skipIntroLevel3; }
        set { skipIntroLevel3 = value; }
    }


    public Player()
    {
        this.id = 0;
        this.playerName = "";
        this.avatarId = 0;
        this.score = 0;
        this.skipIntroLevel1 = 0;
        this.skipIntroLevel2 = 0;
        this.skipIntroLevel3 = 0;
    }

    public Player(int id, string playerName, int score, int avatarId)
    {
        this.id = id;
        this.playerName = playerName;
        this.avatarId = avatarId;
        this.score = score;
        this.skipIntroLevel1 = 0;
        this.skipIntroLevel2 = 0;
        this.skipIntroLevel3 = 0;
    }

    public string GetAsString()
    {
        string s = "" + id + "#" + playerName + "#" + avatarId + "#" + score + "#" + skipIntroLevel1 + "#" + skipIntroLevel2 + "#" + skipIntroLevel3;
        return s;
    }
}
