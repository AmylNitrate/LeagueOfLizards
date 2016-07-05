using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    Text invitationCounter;

    bool signedIn;

    void Awake()
    {
        MultiplayerController.Instance.TrySignIn();
    }

    void Update()
    {
        if (!signedIn)
        {
            if (MultiplayerController.Instance.IsAuthenticated())
            {
                invitationCounter.text = MultiplayerController.Instance.GetInviteNumber().ToString();
                signedIn = true;
            }
        }
    }
}
