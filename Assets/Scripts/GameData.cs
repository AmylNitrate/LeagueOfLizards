using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;

public class GameData : MonoBehaviour {

    public static GameData instance;

    public Participant enemyParticipant;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        foreach(Participant part in MultiplayerController.Instance.GetAllPlayers())
        {
            if (part.ParticipantId != MultiplayerController.Instance.GetParticipantID())
            {
                enemyParticipant = part;
            }
        }
    }
}
