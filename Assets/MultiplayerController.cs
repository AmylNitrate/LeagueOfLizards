using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using System;

public class MultiplayerController : RealTimeMultiplayerListener {

    private static MultiplayerController _instance = null;

    private uint minOpponents = 1, maxOpponents = 1, gameVariation;

    private MultiplayerController()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    public static MultiplayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MultiplayerController();
            }
            return _instance;
        }
    }

    public void SignInAndStartMPGame()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Debug.Log("<----------------------------BREAK------------------------------------->");
                    Debug.Log("SUCCESSFULLY Signed in " + PlayGamesPlatform.Instance.localUser.userName);
                    StartMatchMaking();
                }
                else
                {
                    Debug.Log("<----------------------------BREAK------------------------------------->");
                    Debug.Log("Not signed in correctly");
                    Debug.Log(PlayGamesPlatform.Instance.localUser.state);
                }
            });
        }
        else
        {
            Debug.Log("<----------------------------BREAK------------------------------------->");
            Debug.Log("Already signed in");
            StartMatchMaking();
        }
    }

    public List<Participant> GetAllPlayers()
    {
        return PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants();
    }

    public string GetParticipantID()
    {
        return PlayGamesPlatform.Instance.RealTime.GetSelf().ParticipantId;
    }

    public void SignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }

    public bool IsAuthenticated()
    {
        return PlayGamesPlatform.Instance.localUser.authenticated;
    }

    private void StartMatchMaking()
    {
        PlayGamesPlatform.Instance.RealTime.CreateQuickGame(minOpponents, maxOpponents, gameVariation, this);
    }

    private void ShowMPMessage(string message)
    {
        Debug.Log("<------------------------------------" + message + "----------------------------------------->");
    }

    public void OnRoomSetupProgress(float percent)
    {
        ShowMPMessage("We are " + percent + "% complete with room setup");
    }

    public void OnRoomConnected(bool success)
    {
        if (success)
        {
            ShowMPMessage("We are connected to the room, start the game");
            UIManager.instance.GoToLevel("StatMenu");
        }
        else
        {
            ShowMPMessage("Encountered an error when connecting to the room");
        }
    }

    public void OnLeftRoom()
    {
        ShowMPMessage("Left the room, perform clean up tasks");
    }

    public void OnParticipantLeft(Participant participant)
    {
        throw new NotImplementedException();
    }

    public void OnPeersConnected(string[] participantIds)
    {
        foreach (string participantID in participantIds)
        {
            ShowMPMessage("Player " + participantID + " has joined.");
        }
    }

    public void OnPeersDisconnected(string[] participantIds)
    {
        foreach (string participantID in participantIds)
        {
            ShowMPMessage("Player " + participantID + " has left.");
        }
    }

    public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data)
    {
        ShowMPMessage("We have received some gameplay messages from participant ID: " + senderId);
    }
}
