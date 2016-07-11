using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class GameData : MonoBehaviour {

    //Singleton
    public static GameData instance;

    //The types of rounds that players can choose
    public enum RoundTypes { Assess, Escalate, Fight, RunAway }

    //A dictionary that assigns a numerical value to the round types
    public Dictionary<RoundTypes, int> roundValues = new Dictionary<RoundTypes, int>();

    //The enemy participant from the GPGS multiplayer services
    public Participant enemyParticipant;

    //Tracking the RHP of both players
    public int myCurrentRHP, enemyCurrentRHP;

    //Checking the number of rounds completed so that players can not do too many assess or escalation rounds
    int assessRoundsComplete, escalationRoundsComplete;

    //Tracks the current round that the game is on so that the players can't choose to do a round with a smaller value
    int currentRoundValue;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }

    void Start()
    {
        GetData();
    }

    /// <summary>
    /// Retrieves data from MultiplayerController and the save file, performed on scene load
    /// </summary>
    void GetData()
    {
        //Gets round info for the dictionary
        PopDictionary();
        //Getting the participant ID of the player's opponent
        foreach (Participant part in MultiplayerController.Instance.GetAllPlayers())
        {
            if (part.ParticipantId != MultiplayerController.Instance.GetParticipantID())
            {
                enemyParticipant = part;
            }
        }
        //Read from save file
        //myCurrentRHP = 
        //enemyCurrentRHP = 
        //Set current round
        currentRoundValue = roundValues[RoundTypes.Assess];
    }

    /// <summary>
    /// Performs clean up end game tasks such as saving and destroying the current game tracking object
    /// Should be the last thing completed
    /// </summary>
    void EndGame()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Check if the players are allowed to do another type of a limited round or the game has already passed
    /// </summary>
    /// <param name="round">Which round the game is currently on</param>
    /// <returns></returns>
    public bool CheckRoundAvailability(RoundTypes round)
    {
        if (round == RoundTypes.Assess && currentRoundValue == roundValues[RoundTypes.Assess])
        {
            if (assessRoundsComplete < 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (round == RoundTypes.Escalate && currentRoundValue <= roundValues[RoundTypes.Escalate])
        {
            if (escalationRoundsComplete == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    //Populates the dictionary with the RoundTypes and int values
    void PopDictionary()
    {
        roundValues.Add(RoundTypes.Assess, 0);
        roundValues.Add(RoundTypes.Assess, 1);
        roundValues.Add(RoundTypes.Escalate, 2);
        roundValues.Add(RoundTypes.Fight, 3);
        roundValues.Add(RoundTypes.RunAway, 4);
    }

    /// <summary>
    /// Performed at the end of each round to update any necessary information
    /// </summary>
    /// <param name="round">Which type of round just ended</param>
    public void RoundOver(RoundTypes round)
    {
        //Update both players RHP
        switch (round)
        {
            case RoundTypes.Assess:
                assessRoundsComplete++;
                break;
            case RoundTypes.Escalate:
                escalationRoundsComplete++;
                break;
            case RoundTypes.Fight:
                EndGame();
                break;
            case RoundTypes.RunAway:
                EndGame();
                break;
        }
    }
}
