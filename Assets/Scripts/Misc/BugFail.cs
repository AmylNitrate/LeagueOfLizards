using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BugFail : MonoBehaviour {

    [SerializeField]
    Text description;

    void Awake()
    {
        if (Lizard.current.regenBugs > 0)
        {
            description.text = "Your RHP is already full";
        }
        else
        {
            description.text = "You have no bugs left. You will need to choose the gatherer spec if you have points remaining for more";
        }
    }
}
