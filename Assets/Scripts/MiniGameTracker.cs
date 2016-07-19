using UnityEngine;
using System.Collections;

public class MiniGameTracker : MonoBehaviour {

    //Singleton reference object
    public static MiniGameTracker instance;

    //The two players in the game
    public enum Players { localPlayer, enemy, none }

    //Player values showing the winners
    Players assessOneWinner, assessTwoWinner, escalationWinner, fightWinner;
    public Players overallWinner = Players.none;

    public int numberOfAssess, numberOfEscalations;

    string lastWinner;

    public bool fought;

    public int myRHPRange = 60;

    public string enemyName;

    void Awake()
    {
        instance = this;
        numberOfAssess = 0;
    }

    /// <summary>
    /// Sets the winner of the assessment round
    /// Only call when in results screen
    /// </summary>
    /// <param name="winner">The winner of the round</param>
    /// <param name="round">Checks which of the assess rounds it is. Retrieve from GameData</param>
    public void SetAssessWinner(Players winner)
    {
        Debug.Log("<-----------------------------------------------------------ASSESS WINNER SET-------------------------------------------------->" + numberOfAssess);
        if (numberOfAssess == 0)
        {
            Debug.Log("-----------------------------------------------------------Set assess round one-----------------------------------------------------------");
            numberOfAssess++;
            assessOneWinner = winner;
            if (assessOneWinner == Players.localPlayer)
            {
                myRHPRange -= 10;
                lastWinner = MultiplayerController.Instance.GetMyName();
            }
            else
            {
                myRHPRange -= 5;
                lastWinner = GameData.instance.enemyName;
            }
        }
        else if (numberOfAssess == 1)
        {
            Debug.Log("-----------------------------------------------------------Set assess round two-----------------------------------------------------------");
            numberOfAssess++;
            assessTwoWinner = winner;
            if (assessTwoWinner == Players.localPlayer)
            {
                myRHPRange -= 5;
                lastWinner = MultiplayerController.Instance.GetMyName();
            }
            else
            {
                myRHPRange -= 2;
                lastWinner = GameData.instance.enemyName;
            }
        }
        MultiplayerController.Instance.SendMyRHP();
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
            myRHPRange -= 15;
            lastWinner = MultiplayerController.Instance.GetMyName();
        }
        else
        {
            myRHPRange -= 10;
            lastWinner = GameData.instance.enemyName;
        }
        MultiplayerController.Instance.SendMyRHP();
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
            Lizard.current.myRHPRemaining -= 10;
        }
        fought = true;
        Debug.Log("The winner is: " + winner.ToString());
        MultiplayerController.Instance.SendMyRHP();
        //Game is over at this point
        //Display name on MultiplayerMenu and have both users exit the room
    }

    public int GetNumberOfAssessments()
    {
        return numberOfAssess;
    }

    public int GetNumberOfEscalations()
    {
        return numberOfEscalations;
    }

    public void SetWinner()
    {
        Debug.Log("Enemy RHP at SetWinner(): " + GameData.instance.enemyCurrentRHP);
        if (GameData.instance.enemyCurrentRHP > Lizard.current.myRHPRemaining)
        {
            overallWinner = Players.enemy;
            Lizard.current.GiveXP(12, true);
        }
        else
        {
            overallWinner = Players.localPlayer;
            Lizard.current.GiveXP(50, false);
        }
    }
}
