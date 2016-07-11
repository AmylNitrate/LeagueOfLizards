﻿using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using System;

public class MultiplayerController : RealTimeMultiplayerListener {

    private static MultiplayerController _instance = null;

    int invNumber = 0;
    Invitation[] invitationArray;
    private uint minOpponents = 1, maxOpponents = 1, gameVariation = 0;

    Invitation IncomingInvitation;

    private byte protocolVersion = 1;
    private List<byte> updateMessage;
    bool isMultiplayerReady = false;

    private MultiplayerController()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().WithInvitationDelegate(OnInvitationReceived).Build();
        PlayGamesPlatform.InitializeInstance(config);
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

    public void TrySignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Debug.Log("<----------------------------BREAK------------------------------------->");
                    Debug.Log("SUCCESSFULLY Signed in " + PlayGamesPlatform.Instance.localUser.userName);
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
    
    public int GetInviteNumber()
    {
        invNumber = 0;
        PlayGamesPlatform.Instance.RealTime.GetAllInvitations((invites) =>
        {
            invitationArray = invites;
            foreach (Invitation invite in invites)
            {
                invNumber++;
                Debug.Log("Invite received from" + invite.Inviter + invitationArray.Length);
            }
        });
        return invitationArray.Length;
    }

    public void OnInvitationReceived(Invitation invitation, bool shouldAutoAccept)
    {
        if (shouldAutoAccept)
        {
            PlayGamesPlatform.Instance.RealTime.AcceptInvitation(invitation.InvitationId, this);
        }
        else
        {
            IncomingInvitation = invitation;
            if (MainMenu.instance != null)
            {
                MainMenu.instance.PopUpInvite(invitation.Inviter.DisplayName);
                MainMenu.instance.UpdateInviteCounter(GetInviteNumber());
            }
        }
    }

    public void AcceptIncomingInvitation()
    {
        PlayGamesPlatform.Instance.RealTime.AcceptInvitation(IncomingInvitation.InvitationId, this);
    }

    public void DeclineInvitation()
    {
        MainMenu.instance.incomingInvitationPanel.SetActive(false);
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
        //PlayGamesPlatform.Instance.RealTime.CreateQuickGame(minOpponents, maxOpponents, gameVariation, this);
        PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(minOpponents, maxOpponents, gameVariation, this);
    }

    public void OpenInvitationScreen()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Debug.Log("<----------------------------BREAK------------------------------------->");
                    Debug.Log("SUCCESSFULLY Signed in " + PlayGamesPlatform.Instance.localUser.userName);
                    PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(this);
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
            PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(this);
        }
    }

    private void ShowMPMessage(string message)
    {
        Debug.Log("<------------------------------------" + message + "----------------------------------------->");
    }

    public void OnRoomSetupProgress(float percent)
    {
        UIManager.instance.GoToLevel("Multiplayer");
        ShowMPMessage("We are " + percent + "% complete with room setup");
    }

    public void OnRoomConnected(bool success)
    {
        if (success)
        {
            ShowMPMessage("We are connected to the room, start the game");
            UIManager.instance.GoToLevel("MiniGameMenu");
        }
        else
        {
            ShowMPMessage("Encountered an error when connecting to the room");
            UIManager.instance.GoToLevel("Menu");
        }
    }

    public void OnLeftRoom()
    {
        ShowMPMessage("Left the room, perform clean up tasks");
        UIManager.instance.GoToLevel("Menu");
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

    //Multiplayer messages
    /// <summary>
    /// Sends the players RHP to the other device
    /// Marked by A
    /// </summary>
    public void SendMyRHP()
    {
        updateMessage.Clear();
        updateMessage.Add(protocolVersion);
        updateMessage.Add((byte)'A');
        //Get RHP
        //messageToSend(//RHP from save file)
        byte[] messageToSend = updateMessage.ToArray();
        PlayGamesPlatform.Instance.RealTime.SendMessageToAll(false, messageToSend);
    }
}