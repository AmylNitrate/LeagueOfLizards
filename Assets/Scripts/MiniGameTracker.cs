using UnityEngine;
using System.Collections;

public class MiniGameTracker : MonoBehaviour {

    //Singleton reference object
    public static MiniGameTracker instance;

    //The two players in the game
    public enum Players { localPlayer, enemy }

    //Player values showing the winners
    Players assessOneWinner, assessTwoWinner, escalationWinner, fightWinner;

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
    public void SetAssessWinner(Players winner, int round)
    {
        if (round == 1)
        {
            assessOneWinner = winner;
            if (assessOneWinner == Players.localPlayer)
            {
                ResultsUI.instance.GetRHP(15);
            }
            else
            {
                ResultsUI.instance.GetRHP(20);
            }
        }
        if (round == 2)
        {
            assessTwoWinner = winner;
            if (assessTwoWinner == Players.localPlayer)
            {
                ResultsUI.instance.GetRHP(10);
            }
            else
            {
                ResultsUI.instance.GetRHP(15);
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
        escalationWinner = winner;
        if (escalationWinner == Players.localPlayer)
        {
            ResultsUI.instance.GetRHP(5);
        }
        else
        {
            ResultsUI.instance.GetRHP(10);
        }
    }

    /// <summary>
    /// Sets the winner of the fight
    /// </summary>
    /// <param name="winner">The player who won the round</param>
    public void SetFightWinner(Players winner)
    {
        fightWinner = winner;
    }
}
