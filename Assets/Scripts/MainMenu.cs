using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public static MainMenu instance;

    [SerializeField]
    Text invitationCounter, inviterName, numberOfBugs;

    bool signedIn;

    public GameObject incomingInvitationPanel, bugSuccessPanel, bugFailPanel, levelUpPopUp, useBugPopUp, timeOutPopUp;

    void Awake()
    {
        MultiplayerController.Instance.TrySignIn();
        instance = this;
        SaveLoad.Load();
        numberOfBugs.text = Lizard.current.regenBugs.ToString();
        if (Lizard.current.level > Lizard.current.lastLevel)
        {
            levelUpPopUp.SetActive(true);
            Lizard.current.lastLevel = Lizard.current.level;
            Debug.Log("Levelled up to " + Lizard.current.level + " and last " + Lizard.current.lastLevel);
            SaveLoad.Save();
        }
        if (Lizard.current.myRHPRemaining <= 0 && Lizard.current.regenBugs > 0)
        {
            useBugPopUp.SetActive(true);
        }
        else if (Lizard.current.myRHPRemaining <= 0 && Lizard.current.regenBugs <= 0)
        {
            timeOutPopUp.SetActive(true);
        }
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

    public void UseBug()
    {
        if (Lizard.current.UseRegenBug())
        {
            bugSuccessPanel.SetActive(true);
        }
        else
        {
            bugFailPanel.SetActive(true);
        }
        numberOfBugs.text = Lizard.current.regenBugs.ToString();
    }
}
