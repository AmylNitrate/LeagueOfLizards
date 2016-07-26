using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class GameData : MonoBehaviour {

    //Singleton
    public static GameData instance;

    //The types of rounds that players can choose
    public enum RoundTypes { none, Assess, Escalate, Fight, RunAway }

    //A dictionary that assigns a numerical value to the round types
    public Dictionary<RoundTypes, int> roundValues = new Dictionary<RoundTypes, int>();

    //The enemy participant from the GPGS multiplayer services
    public Participant enemyParticipant;

    //Tracking the RHP of both players
    public int myCurrentRHP, enemyCurrentRHP;

    //Enemy name
    public string enemyName, playerOneName, playerTwoName;

    //Number of rounds played
    public int roundsPlayed = 0;

    //Checking the number of rounds completed so that players can not do too many assess or escalation rounds
    int assessRoundsComplete, escalationRoundsComplete;

    //Tracks the current round that the game is on so that the players can't choose to do a round with a smaller value
    int currentRoundValue;

    //The two choices for the local player and the enemy that is selected on the results UI screen
    RoundTypes enemyChoice, myChoice;

    //List of participants
    List<Participant> parts = new List<Participant>();

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
        int i = 0;
        //Read from save file
        myCurrentRHP = Lizard.current.myRHPRemaining;
        Debug.Log("My current RHP = " + myCurrentRHP);
        //enemyCurrentRHP = 
        //Set current round
        currentRoundValue = roundValues[RoundTypes.Assess];
    }

    public void SetNames()
    {
        parts = MultiplayerController.Instance.GetAllPlayers();
        foreach (Participant part in parts)
        {
            if (part.ParticipantId != MultiplayerController.Instance.GetParticipantID())
            {
                enemyParticipant = part;
            }
        }
        playerOneName = parts[0].DisplayName;
        playerTwoName = parts[1].DisplayName;
        GameInfo.current.userOne = playerOneName;
        GameInfo.current.userTwo = playerTwoName;
        RoundInfo.current.userOneName = playerOneName;
        RoundInfo.current.userTwoName = playerTwoName;
        GameInfo.current.SetGameID();
        //If the first in the array is not the enemy then it must be the player   
        if (parts[0].ParticipantId != enemyParticipant.ParticipantId)
        {
            //Therefore the local user will be the one keeping track of the information
            GameInfo.current.isKeepingTrack = true;
        }
    }

    void OnLevelWasLoaded(int level)
    {
        Debug.Log("Level loaded");
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Destroy(this.gameObject);
            Debug.Log("Destroying game data object because of main menu");
        }
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
    /// <returns>True if round is legal, false if not</returns>
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
        //roundValues.Add(RoundTypes.Assess, 1);
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

    /// <summary>
    /// Updates the enemy lizards RHP
    /// </summary>
    /// <param name="enemyRHP">The enemy RHP value</param>
    public void UpdateEnemyLizardRHP(int enemyRHP)
    {
        enemyCurrentRHP = enemyRHP;
    }

    /// <summary>
    /// Called when the enemy's choice is received
    /// </summary>
    /// <param name="type"></param>
    public void SetEnemyChoice(RoundTypes type)
    {
        RoundInfo.current.PopulateFromGameInfo();
        enemyChoice = type;
        //Setting the choices of players dependant on who is keeping track
        if (GameInfo.current.isKeepingTrack)
        {
            SetNames();
            RoundInfo.current.userTwoMiniGameChoice = type.ToString();
        }
        else
        {
            RoundInfo.current.userOneMiniGameChoice = type.ToString();
        }
        CheckChoices();
    }

    /// <summary>
    /// Set the choice of the local player
    /// </summary>
    /// <param name="type">Which round type they are selecting</param>
    public void SetMyChoice(RoundTypes type)
    {
        myChoice = type;
        switch (type)
        {
            case RoundTypes.Assess:
                MultiplayerController.Instance.SendAssessRequest();
                break;
            case RoundTypes.Escalate:
                MultiplayerController.Instance.SendEscalationRequest();
                break;
            case RoundTypes.Fight:
                MultiplayerController.Instance.SendFightRequest();
                break;
            case RoundTypes.RunAway:
                MultiplayerController.Instance.SendFleeRequest();
                break;
        }
        //Setting the choices of players dependant on who is keeping track

        Debug.Log("Check track?????????????????????????????????????????????????????");
        if (GameInfo.current.isKeepingTrack)
        {
            SetNames();
            Debug.Log("Keeping track");
            RoundInfo.current.userOneMiniGameChoice = type.ToString();
        }
        else
        {
            RoundInfo.current.userTwoMiniGameChoice = type.ToString();
            Debug.Log("Not Keeping track");
        }
        CheckChoices();
    }

    /// <summary>
    /// Whenever the player makes their choice or the enemy's choice is received
    /// Check to see if both choices are made and then take action
    /// </summary>
    void CheckChoices()
    {
        if (enemyChoice != RoundTypes.none && myChoice != RoundTypes.none)
        {
            //If either player is trying to run away, inform the other player and set them as the winner
            if (myChoice == RoundTypes.RunAway || enemyChoice == RoundTypes.RunAway)
            {
                roundsPlayed++;
                ResultsUI.instance.runAwayPanel.SetActive(true);
                if (myChoice == RoundTypes.RunAway)
                {
                    ResultsUI.instance.runAwayPanelValue.text = "You have run away";
                }
                else
                {
                    ResultsUI.instance.runAwayPanelValue.text = "Your enemy has run away\n\nYou have won";
                    MiniGameTracker.instance.GiveXP(50);
                }
                GameInfo.current.SendToGameSparks();
            }
            //If both choices match then load the correct scene
            else if (enemyChoice == myChoice)
            {
                roundsPlayed++;
                RoundInfo.current.miniGameOutcome = myChoice.ToString();
                LoadMiniGame();
            }
            //If the enemy's choice has a higher value than the local players choice then the local player will need to select again
            else if (roundValues[enemyChoice] > roundValues[myChoice])
            {
                //Make user choose again
                Debug.Log("I Need to choose again");
                myChoice = RoundTypes.none;
                ResultsUI.instance.enemyChosePanel.SetActive(true);
                ResultsUI.instance.enemyChoseValue.text = enemyChoice.ToString();
            }
            //Likewise if the local players choice has a higher value than the enemy's choice, the enemy will need to choose again
            //They will have the same calculation on their device and will know so no information needs to be sent
            else if (roundValues[myChoice] > roundValues[enemyChoice])
            {
                //Make enemy choose again
                Debug.Log("Enemy needs to choose again");
                enemyChoice = RoundTypes.none;
            }
        }
    }

    /// <summary>
    /// Loads the appropriate minigame
    /// </summary>
    void LoadMiniGame()
    {
        Debug.Log("Loading the game: " + myChoice.ToString());
        if (myChoice == RoundTypes.Assess)
        {
            UIManager.instance.GoToLevel("RoundOne");
            Lizard.current.TakeAwayRHP(5);
        }
        if (myChoice == RoundTypes.Escalate)
        {
            UIManager.instance.GoToLevel("RoundTwo");
            Lizard.current.TakeAwayRHP(10);
        }
        if (myChoice == RoundTypes.Fight)
        {
            UIManager.instance.GoToLevel("RoundThree");
            Lizard.current.TakeAwayRHP(20);
        }
        enemyChoice = RoundTypes.none;
        myChoice = RoundTypes.none;
    }
}