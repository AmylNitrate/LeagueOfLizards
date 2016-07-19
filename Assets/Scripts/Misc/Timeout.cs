using UnityEngine;
using System.Collections;

public class Timeout : MonoBehaviour {

    float timer;
    [SerializeField]
    UnityEngine.UI.Text text;
    [SerializeField]
    UnityEngine.UI.Button button;

    void Awake()
    {
        timer = 30;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            text.text = timer.ToString("0.00");
        }
        else
        {
            text.text = "0.00";
            button.interactable = true;
            Lizard.current.myRHPRemaining = Lizard.current.myMaxRHP;
            SaveLoad.Save();
        }
    }
}
