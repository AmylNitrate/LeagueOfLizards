using UnityEngine;
using System.Collections;
using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using UnityEngine.UI;
using System.Threading;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

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
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true; //for development!
    }
    
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

    

    
    #endregion
}
