using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundDescriptionPanel : MonoBehaviour {

    public static RoundDescriptionPanel instance;

    [SerializeField]
    Text roundTitle, roundDescription;

    [SerializeField]
    GameObject assessButton, escalateButton, fightButton, fleeButton;

    [TextArea]
    public string assessDescription, escalateDescription, fightDescription, fleeDescription;

    void Awake()
    {
        instance = this;
    }

    public void SetPanelInfo(GameData.RoundTypes round)
    {
        switch (round)
        {
            case GameData.RoundTypes.Assess:
                roundTitle.text = "Assess";
                roundDescription.text = assessDescription;
                assessButton.SetActive(true);
                break;
            case GameData.RoundTypes.Escalate:
                roundTitle.text = "Escalate";
                roundDescription.text = escalateDescription;
                escalateButton.SetActive(true);
                break;
            case GameData.RoundTypes.Fight:
                roundTitle.text = "Fight";
                roundDescription.text = fightDescription;
                fightButton.SetActive(true);
                break;
            case GameData.RoundTypes.RunAway:
                roundTitle.text = "Flee";
                roundDescription.text = fleeDescription;
                fleeButton.SetActive(true);
                break;
        }
    }
}
