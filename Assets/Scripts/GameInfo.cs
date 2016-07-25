using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class GameInfo {

    public static GameInfo current;

    public bool isKeepingTrack;

    public string UniqueRoundID, userOne, userTwo, timeStarted, date;

    public List<RoundInfo> roundData = new List<RoundInfo>();

    public GameInfo(string userOneID, string userTwoID)
    {
        Debug.Log("GameInfo Constructor called");
        userOne = userOneID;
        userTwo = userTwoID;
        timeStarted = Time.time.ToString();
        date = System.DateTime.Now.ToString();
        UniqueRoundID = userOne + "_" + userTwo + "_" + timeStarted;
        //Create first round info entry
        //NextRound();
        RoundInfo.current = new RoundInfo();
    }

    public GameInfo()
    {
        Debug.Log("GameInfo Constructor called");
        userOne = "";
        userTwo = "";
        timeStarted = Time.time.ToString();
        date = System.DateTime.Now.ToString();
        UniqueRoundID = userOne + "_" + userTwo + "_" + timeStarted;
        //Create first round info entry
        //NextRound();
        //RoundInfo.current = new RoundInfo();
    }

    /// <summary>
    /// Will only send the information if isKeepingTrack is true to avoid duplicate entries
    /// </summary>
    public void SendToGameSparks()
    {
        if (isKeepingTrack)
        {
            //Send the information to GameSparks
            CreateJSON();
        }
        else
        {
            //Don't send the information otherwise we will have duplicates
        }
    }

    /// <summary>
    /// Creates a new RoundInfo object and adds it to the list
    /// </summary>
    public void NextRound()
    {
        RoundInfo.current = new RoundInfo();
        roundData.Add(RoundInfo.current);
    }

    string CreateJSON()
    {
        string json = JsonConvert.SerializeObject(current);
        Debug.Log(json);
        return json;
    }
}