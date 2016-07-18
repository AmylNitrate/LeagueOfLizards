using UnityEngine;
using System.Collections;

public class MiniGameTracker : MonoBehaviour {

    //Singleton reference object
    public static MiniGameTracker instance;

    //The two players in the game
    public enum Players { localPlayer, enemy }

    //Player values showing the winners
    Players assessOneWinner, assessTwoWinner, escalationWinner, fightWinner;

    public int numberOfAssess, numberOfEscalations;

    string lastWinner;

    public int myRHPRange = 50;

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Sets the winner of the assessment round
    /// Only call when in results screen
    /// </summary>
    /// <param name="winner">The winner of the round</param>
    /// <param name="round">Checks which of the assess rounds it is. Retrieve from GameData</param>
    public void SetAssessWinner(Players winner)
    {
        if (numberOfAssess == 0)
        {
            numberOfAssess++;
            assessOneWinner = winner;
            if (assessOneWinner == Players.localPlayer)
            {
                myRHPRange = 15;
                lastWinner = MultiplayerController.Instance.GetMyName();
            }
            else
            {
                myRHPRange = 20;
                lastWinner = GameData.instance.enemyName;
            }
        }
        if (numberOfAssess == 1)
        {
            numberOfAssess++;
            assessTwoWinner = winner;
            if (assessTwoWinner == Players.localPlayer)
            {
                myRHPRange = 10;
                lastWinner = MultiplayerController.Instance.GetMyName();
            }
            else
            {
                myRHPRange = 15;
                lastWinner = GameData.instance.enemyName;
            }
        }
    }

    /// <summary>
    /// Sets the escalation winner
    /// Only call when in results screen
    /// </summary>
    /// <param name="winner">The player who won the round</param>
    public void SetEscalationWinner(Players winner)
    {
        numberOfEscalations++;
        escalationWinner = winner;
        if (escalationWinner == Players.localPlayer)
        {
            if (numberOfAssess > 1)
            {
                myRHPRange = 5;
            }
            else
            {
                myRHPRange = 10;
            }
            lastWinner = MultiplayerController.Instance.GetMyName();
        }
        else
        {
            if (numberOfAssess > 1)
            {
                myRHPRange = 10;
            }
            else
            {
                myRHPRange = 15;
            }
            lastWinner = GameData.instance.enemyName;
        }
    }

    /// <summary>
    /// Sets the winner of the fight
    /// </summary>
    /// <param name="winner">The player who won the round</param>
    public void SetFightWinner(Players winner)
    {
        fightWinner = winner;
        if (fightWinner == Players.localPlayer)
        {
            lastWinner = MultiplayerController.Instance.GetMyName();
        }
        else
        {
            lastWinner = GameData.instance.enemyName;
        }
    }

    public int GetNumberOfAssessments()
    {
        return numberOfAssess;
    }

    public int GetNumberOfEscalations()
    {
        return numberOfEscalations;
    }
}
