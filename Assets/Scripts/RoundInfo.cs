using UnityEngine;
using System.Collections;

public class RoundInfo {

    public static RoundInfo current;

    public string uniqueRoundID, userOneName, userTwoName, miniGameWinner, userOneMiniGameChoice, userTwoMiniGameChoice, miniGameOutcome;

    public int userOneRHP, userTwoRHP, roundNumber, userOneMiniGameScore, userTwoMiniGameScore;

    bool isSetup;

	public RoundInfo()
    {
        //Strings
        miniGameWinner = "";
        userOneMiniGameChoice = "";
        userTwoMiniGameChoice = "";
        miniGameOutcome = "";

        //Ints
        roundNumber = GameData.instance.roundsPlayed;

        //Add to list
        //GameInfo.current.roundData.Add(current);
    }

    //CALL
    public void PopulateFromGameInfo()
    {
        Debug.Log("Populating");
        if (!isSetup)
        {
            if (GameInfo.current.isKeepingTrack)
            {
                userOneRHP = GameData.instance.myCurrentRHP;
                userTwoRHP = GameData.instance.enemyCurrentRHP;
            }
            else
            {
                userTwoRHP = GameData.instance.myCurrentRHP;
                userOneRHP = GameData.instance.enemyCurrentRHP;
            }
            uniqueRoundID = GameInfo.current.UniqueRoundID;
            userOneName = GameInfo.current.userOne;
            userTwoName = GameInfo.current.userTwo;
            Debug.Log(uniqueRoundID);
            isSetup = true;
        }
        Debug.Log("Populated");
    }

    /// <summary>
    /// Assign minigame points
    /// </summary>
    /// <param name="playerOnePoints">The first players points</param>
    /// <param name="playerTwoPoints">The second players points</param>
    public void AssignPoints(int playerOnePoints, int playerTwoPoints)
    {
        userOneMiniGameScore = playerOnePoints;
        userTwoMiniGameScore = playerTwoPoints;
    }
}
