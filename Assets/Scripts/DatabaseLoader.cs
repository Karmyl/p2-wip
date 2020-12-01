using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseLoader : MonoBehaviour
{
    private static Player currentPlayer = null;
    private string tableName = "Pelaajat";
    private string databaseName = "Userdata.db";

    // Static attributes for accessing database
    public static string staticTableName;
    public static string staticDatabaseName;

    // Get current loaded Player
    public static Player GetCurrentPlayer()
    {
        return DatabaseLoader.currentPlayer;
    }

    public static void SaveCurrentPlayer()
    {
        Player player = DatabaseLoader.GetCurrentPlayer();
        IDbConnection dbconn = new SqliteConnection(GetStaticConnectionString());
        dbconn.Open();
        IDbCommand cmd = dbconn.CreateCommand();
        cmd.CommandText = "UPDATE " + staticTableName + " SET kokonaispisteet = " + player.Score + " WHERE id = " + player.Id;
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        dbconn.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Allocate new static Player-instance
        DatabaseLoader.currentPlayer = new Player();
        DatabaseLoader.staticDatabaseName = databaseName;
        DatabaseLoader.staticTableName = tableName;

        // Create new database or join to existing one.
        if (CheckExistingDatabase())
        {
            int lastId = PlayerPrefs.GetInt("lastId");
            if(lastId != 0)
            {
                // Reload player with last played id
                DatabaseLoader.currentPlayer = GetPlayerWithId(lastId);
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

    void OnApplicationFocus(bool focus)
    {
        
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

    private string GetConnectionString()
    {
        string s = "URI=file:" + Application.dataPath + "/" + databaseName;
        return s;
    }

    public static string GetStaticConnectionString()
    {
        string s = "URI=file:" + Application.dataPath + "/" + staticDatabaseName;
        return s;
    }

    // Create new command with text.
    private IDbCommand CreateCommand(string commandText, IDbConnection connection)
    {
        IDbCommand newCommand = connection.CreateCommand();
        newCommand.CommandText = commandText;
        return newCommand;
    }
}