using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseLoader : MonoBehaviour
{
    private static Player currentPlayer = null;
    private static string tableName = "Pelaajat";
    private static string databaseName = "Userdata.db";

    // Create new player to database and set it as current player
    public static void CreateNewPlayer(string name, int avatarIndex)
    {
        Player newPlayer = new Player();

        // Get new player id from database
        int newPlayerId = -1;
        IDbConnection dbconn = new SqliteConnection(GetConnectionString());
        dbconn.Open();
        IDbCommand cmdNewId = dbconn.CreateCommand();
        cmdNewId.CommandText = "SELECT MAX(id) FROM " + tableName;
        try
        {
            IDataReader reader = cmdNewId.ExecuteReader();
            while (reader.Read())
            {
                newPlayerId = reader.GetInt32(0);
                newPlayerId++;
            }
            reader.Dispose();
        }
        catch
        {
            // No players exist in table, start with id = 1
            newPlayerId = 1;
        }
        cmdNewId.Dispose();

        IDbCommand cmdNewPlayer = dbconn.CreateCommand();
        cmdNewPlayer.CommandText = "INSERT INTO " + tableName + "(id, nimi, avatarId, kokonaispisteet) VALUES(" + newPlayerId + ", '" + name + "', " + avatarIndex + ", 0)";
        cmdNewPlayer.ExecuteNonQuery();
        cmdNewPlayer.Dispose();
        dbconn.Close();

        newPlayer.Id = newPlayerId;
        newPlayer.PlayerName = name;
        newPlayer.AvatarId = avatarIndex;
        newPlayer.Score = 0;

        DatabaseLoader.SetCurrentPlayer(newPlayer);
    }

    // Get current loaded Player
    public static Player GetCurrentPlayer()
    {
        return DatabaseLoader.currentPlayer;
    }

    // Set current active player
    public static void SetCurrentPlayer(Player player)
    {
        DatabaseLoader.currentPlayer = player;
        PlayerPrefs.SetInt("lastId", player.Id);
    }

    public static void SaveCurrentPlayer()
    {
        Player player = DatabaseLoader.GetCurrentPlayer();
        IDbConnection dbconn = new SqliteConnection(GetConnectionString());
        dbconn.Open();
        IDbCommand cmd = dbconn.CreateCommand();
        cmd.CommandText = "UPDATE " + tableName + " SET kokonaispisteet = " + player.Score + " WHERE id = " + player.Id;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        dbconn.Close();
    }

    public static List<Player> GetAllPlayers()
    {
        List<Player> players= new List<Player>();
        IDbConnection dbconn = new SqliteConnection(GetConnectionString());
        dbconn.Open();

        IDbCommand cmd = dbconn.CreateCommand();
        cmd.CommandText = "SELECT id, nimi, avatarId, kokonaispisteet FROM " + tableName;
        IDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            int avatarId = reader.GetInt32(2);
            int score = reader.GetInt32(3);
            players.Add(new Player(id, name, score, avatarId));
        }

        reader.Close();
        cmd.Dispose();
        dbconn.Close();
        return players;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Allocate new static Player-instance
        currentPlayer = new Player();

        // Create new database or join to existing one.
        if (CheckExistingDatabase())
        {
            int lastId = PlayerPrefs.GetInt("lastId");
            if(lastId != 0)
            {
                // Reload player with last played id
                Player lastActivePlayer = GetPlayerWithId(lastId);
                DatabaseLoader.SetCurrentPlayer(lastActivePlayer);
                Debug.Log("Pelaaja, id: " + lastActivePlayer.Id + ", nimi: " + lastActivePlayer.PlayerName + ", avatarId: " + lastActivePlayer.AvatarId + ", score: " + lastActivePlayer.Score);
            }
        }
        else
        {
            // Create tables for database and default player.
            CreateTable(tableName);
            int newPlayerId = AddNewPlayer("Pelaaja", 0, 0);
            PlayerPrefs.SetInt("lastId", newPlayerId);
            DatabaseLoader.currentPlayer = GetPlayerWithId(newPlayerId);
        }
    }

    // Check if database already exists.
    // Return true if database is already created, otherwise return false;
    private bool CheckExistingDatabase()
    {
        bool result = true;
        IDbConnection dbconn = new SqliteConnection(GetConnectionString());
        dbconn.Open();

        // Check if database exists by making query to database.
        IDbCommand cmd = CreateCommand("SELECT * FROM Pelaajat", dbconn);
        try
        {
            IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int i = reader.GetInt32(0);
            }

            // Jos tultiin tänne asti niin yhdistettiin olemassaolevaan tietokantaan.
            result = true;
        }
        catch
        {
            // Tässä luodaan uusi tietokanta.
            result = false;
        }

        dbconn.Close();
        return result;
    }

    private void CreateTable(string tableName)
    {
        IDbConnection dbconn = new SqliteConnection(GetConnectionString());
        dbconn.Open();
        IDbCommand cmd = CreateCommand("CREATE TABLE IF NOT EXISTS " + tableName + " (id INTEGER PRIMARY KEY, nimi TEXT, avatarId INTEGER, kokonaispisteet INTEGER )", dbconn);
        IDataReader reader = cmd.ExecuteReader();
        dbconn.Close();
    }

    private int GetNewPlayerId()
    {
        int newPlayerId = -1;
        IDbConnection dbconn = new SqliteConnection(GetConnectionString());
        dbconn.Open();
        IDbCommand cmd = CreateCommand("SELECT MAX(id) FROM " + tableName, dbconn);
        try
        {
            IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                newPlayerId = reader.GetInt32(0);
                newPlayerId++;
            }
            reader.Dispose();
        }
        catch
        {
            // No players exist in table, start with id = 1
            newPlayerId = 1;
        }

        cmd.Dispose();
        dbconn.Close();
        return newPlayerId;
    }

    // Add new player to datase.
    // Return id for new player.
    private int AddNewPlayer(string name, int avatarId, int score)
    {
        int newPlayerId = GetNewPlayerId();
        IDbConnection dbconn = new SqliteConnection(GetConnectionString());
        dbconn.Open();
        IDbCommand cmd = CreateCommand("INSERT INTO " + tableName + "(id, nimi, avatarId, kokonaispisteet) VALUES(" + newPlayerId + ", '" + name + "', " + avatarId + ", " + score + ")", dbconn);
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        dbconn.Close();
        return newPlayerId;
    }

    private Player GetPlayerWithId(int id)
    {
        Player player = new Player();
        IDbConnection dbconn = new SqliteConnection(GetConnectionString());
        dbconn.Open();
        IDbCommand cmd = CreateCommand("SELECT nimi, avatarId, kokonaispisteet FROM " + tableName + " WHERE id = " + id, dbconn);
        IDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string name = reader.GetString(0);
            int avatarId = reader.GetInt32(1);
            int score = reader.GetInt32(2);
            player.Id = id;
            player.PlayerName = name;
            player.AvatarId = avatarId;
            player.Score = score;
        }

        reader.Close();
        cmd.Dispose();
        dbconn.Close();
        return player;
    }

    private bool RemovePlayer(int id)
    {
        bool result = true;
        IDbConnection dbconn = new SqliteConnection(GetConnectionString());
        dbconn.Open();

        // NOTE: Should check if id exists?
        IDbCommand cmd = CreateCommand("DELETE FROM " + tableName + " WHERE id = " + id, dbconn);
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        dbconn.Close();
        return result;
    }

    // Print whole database.
    void PrintPlayers()
    {
        IDbConnection dbconn = new SqliteConnection(GetConnectionString());
        dbconn.Open();
        IDbCommand cmd = CreateCommand("SELECT id, nimi, avatarId, kokonaispisteet FROM " + tableName, dbconn);
        IDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            int avatarId = reader.GetInt32(2);
            int score = reader.GetInt32(3);
            Debug.Log("Id: " + id + ", name: " + name + ", avatarId: " + avatarId + ", kokonaispisteet: " + score);
        }

        reader.Close();
        cmd.Dispose();
        dbconn.Close();
    }

    public static string GetConnectionString()
    {
        string s = "URI=file:" + Application.dataPath + "/" + databaseName;
        return s;
    }

    public static IDbCommand CreateCommand(string commandText, IDbConnection connection)
    {
        IDbCommand newCommand = connection.CreateCommand();
        newCommand.CommandText = commandText;
        return newCommand;
    }
}