﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultsUI : MonoBehaviour {

    public static ResultsUI instance;

    [SerializeField]
    Text playerRHP, opponentRHP, opponentActualRHP;

    public Text enemyChoseValue, runAwayPanelValue;
    
    public GameObject enemyChosePanel, runAwayPanel;

    void Awake()
    {
        instance = this;
        //Sets the RHP values for the first time
        playerRHP.text = GameData.instance.myCurrentRHP.ToString();
        opponentRHP.text = "???";
    }

    void Start()
    {
        opponentActualRHP.text = GameData.instance.enemyCurrentRHP.ToString();
    }

    /// <summary>
    /// Gets the current RHP of the player and the range that their opponent's RHP is in based on the results of the minigames
    /// </summary>
    /// <param name="range">Pass in which range the opponents RHP should be shown in depending on the result of the last game</param>
    public void GetRHP(int range)
    {
        playerRHP.text = Lizard.current.myRHPRemaining.ToString();
        //Get random number between 1 and the range
        int temp = Random.Range(1, range);
        //Calculates the bounds of the range depending on the opponents actual RHP
        int opponentRHPVal = GameData.instance.enemyCurrentRHP;
        int rangeMax = opponentRHPVal + temp;
        int rangeMin = opponentRHPVal - (range - temp);
        opponentRHP.text = rangeMin.ToString() + " - " + rangeMax.ToString();
        opponentActualRHP.text = GameData.instance.enemyCurrentRHP.ToString();
    }

    /// <summary>
    /// Called when pressing buttons on the results page
    /// </summary>
    /// <param name="round">The button that was hit</param>
    public void SendRoundRequest(string roundName)
    {
        switch (roundName)
        {
            case "Assess":
                if (GameData.instance.CheckRoundAvailability(GameData.RoundTypes.Assess))
                {
                    //Send request to opponent
                    GameData.instance.SetMyChoice(GameData.RoundTypes.Assess);
                    Debug.Log("Assess request sent");
                }
                break;
            case "Escalate":
                if (GameData.instance.CheckRoundAvailability(GameData.RoundTypes.Escalate))
                {
                    //Send request to opponent
                    GameData.instance.SetMyChoice(GameData.RoundTypes.Escalate);
                    Debug.Log("Escalate request sent");
                }
                break;
            case "Fight":
                Debug.Log("Fight request sent");
                GameData.instance.SetMyChoice(GameData.RoundTypes.Fight);
                //Send request to opponent
                break;
            case "Run":
                Debug.Log("Run away request sent");
                GameData.instance.SetMyChoice(GameData.RoundTypes.RunAway);
                //Send message to opponent and close game
                break;
        }
    }

    public void LeaveRoom()
    {
        MultiplayerController.Instance.LeaveRoom();
    }
}
