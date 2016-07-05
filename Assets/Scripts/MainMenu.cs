using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public static MainMenu instance;

    [SerializeField]
    Text invitationCounter;

    bool signedIn;

    public GameObject incomingInvitationPanel;

    void Awake()
    {
        MultiplayerController.Instance.TrySignIn();
        instance = this;
    }

    void Update()
    {
        if (!signedIn)
        {
            if (MultiplayerController.Instance.IsAuthenticated())
            {
                invitationCounter.text = MultiplayerController.Instance.GetInviteNumber().ToString();
                StartCoroutine(CheckForInvites());
                signedIn = true;
            }
        }
    }

    public void AcceptInvite()
    {
        MultiplayerController.Instance.AcceptIncomingInvitation();
    }

    public void DeclineInvite()
    {
        MultiplayerController.Instance.DeclineInvitation();
    }

    IEnumerator CheckForInvites()
    {
        yield return new WaitForSeconds(1f);
        invitationCounter.text = MultiplayerController.Instance.GetInviteNumber().ToString();
        StartCoroutine(CheckForInvites());
    }
}
