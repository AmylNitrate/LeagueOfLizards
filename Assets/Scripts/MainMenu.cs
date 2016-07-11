using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public static MainMenu instance;

    [SerializeField]
    Text invitationCounter, inviterName;

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
                //StartCoroutine(CheckForInvites());
                signedIn = true;
            }
        }
    }

    public void PopUpInvite(string popUpName)
    {
        incomingInvitationPanel.SetActive(true);
        inviterName.text = popUpName;
    }

    public void AcceptInvite()
    {
        MultiplayerController.Instance.AcceptIncomingInvitation();
    }

    public void DeclineInvite()
    {
        MultiplayerController.Instance.DeclineInvitation();
    }

    public void UpdateInviteCounter(int number)
    {
        invitationCounter.text = number.ToString();
    }

    IEnumerator CheckForInvites()
    {
        yield return new WaitForSeconds(1f);
        invitationCounter.text = MultiplayerController.Instance.GetInviteNumber().ToString();
        StartCoroutine(CheckForInvites());
    }
}
