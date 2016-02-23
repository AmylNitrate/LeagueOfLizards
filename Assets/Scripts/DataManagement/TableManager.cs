using UnityEngine;
using System.Collections;
using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using UnityEngine.UI;
using System.Threading;

public class TableManager : InitializePlayer {

    public Text resultText;
    private IAmazonDynamoDB _client;
    private DynamoDBContext _context;

    private DynamoDBContext Context
    {
        get
        {
            if (_context == null)
                _context = new DynamoDBContext(_client);

            return _context;
        }
    }
    // Use this for initialization
    void Awake () {
        _client = Client;
	}
    #region PlayerData Funtions
    public void CreatePlayerLevelData()
    {
        PlayerEntity myPlayer = new PlayerEntity
        {
            PlayerID = playerID,
            LevelID = "Hello"
        };

        //Save the music
        Context.SaveAsync(myPlayer, (result) =>
        {
            if (result.Exception == null)
            {
                Debug.Log("Data Saved");

            }
        });
    }

    public void UpdatePlayerLevelData()
    {
        PlayerEntity retrievedPlayer = null;
        Context.LoadAsync<PlayerEntity>(playerID, (result) =>
        {
            retrievedPlayer = result.Result as PlayerEntity;
            retrievedPlayer.LevelID = "Mine";
            //Additional update information to be added here. Should follow retrievedPlayer.YourVariable = storedLocalVariable
            Context.SaveAsync<PlayerEntity>(retrievedPlayer, (res) =>
            {
                if (res.Exception == null)
                    Debug.Log("Player Updated");
            });
        });
    }

    public void DeletePlayerLevelData(PlayerEntity playerEntity)
    {
        //Delete  Player
        Context.DeleteAsync<PlayerEntity>(playerID, (result) =>
        {
            if (result.Exception == null)
            {
                Context.LoadAsync<PlayerEntity>(playerID, (res) =>
                {
                    PlayerEntity deletedPlayer = res.Result;
                    if (deletedPlayer == null)
                        Debug.Log("Book is deleted");
                });
            }
        });
    }
    #endregion

    #region QuizData Functions
    public void CreateQuizData()
    {
        QuizEntity myPlayer = new QuizEntity
        {
            PlayerID = playerID,
            Q1Answer = "d"
        };

        //Save the data
        Context.SaveAsync(myPlayer, (result) =>
        {
            if (result.Exception == null)
            {
                Debug.Log("Data Saved");
            }
        });
    }

    public void UpdateQuizData()
    {
        QuizEntity retrievedPlayer = null;
        Context.LoadAsync<QuizEntity>(playerID, (result) =>
        {
            retrievedPlayer = result.Result as QuizEntity;
            retrievedPlayer.Q1Answer = "Whoa";
            //Additional update information to be added here. Should follow retrievedPlayer.YourVariable = storedLocalVariable
            Context.SaveAsync<QuizEntity>(retrievedPlayer, (res) =>
            {
                if (res.Exception == null)
                    resultText.text = retrievedPlayer.Q1Answer;
                    Debug.Log("Player Updated");
            });
        });
    }
    public void DeleteQuizData()
    {
        //Delete  Player
        Context.DeleteAsync<QuizEntity>(playerID, (result) =>
        {
            if (result.Exception == null)
            {
                Context.LoadAsync<QuizEntity>(playerID, (res) =>
                {
                    QuizEntity deletedPlayer = res.Result;
                    if (deletedPlayer == null)
                        Debug.Log("Book is deleted");
                });
            }
        });
    }
    


    #endregion
}
