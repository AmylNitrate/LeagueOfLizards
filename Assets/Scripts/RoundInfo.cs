using UnityEngine;
using System.Collections;

public class RoundInfo {

    public static RoundInfo current;

    public string uniqueRoundID, userOneName, userTwoName, miniGameWinner, userOneMiniGameChoice, userTwoMiniGameChoice, miniGameOutcome;

    public int userOneRHP, userTwoRHP, roundNumber, userOneMiniGameScore, userTwoMiniGameScore;

	public RoundInfo()
    {
        //Strings
        uniqueRoundID = GameInfo.current.UniqueRoundID;
        userOneName = GameInfo.current.userOne;
        userTwoName = GameInfo.current.userTwo;
        miniGameWinner = "";
        userOneMiniGameChoice = "";
        userTwoMiniGameChoice = "";
        miniGameOutcome = "";

        //Ints
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
        roundNumber = GameData.instance.roundsPlayed;
        userOneMiniGameScore = 0;
        userTwoMiniGameScore = 0;

        //Add to list
        //GameInfo.current.roundData.Add(current);
    }
}
