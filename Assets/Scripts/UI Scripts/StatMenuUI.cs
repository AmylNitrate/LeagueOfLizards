using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatMenuUI : MonoBehaviour
{
    private Text textRef1;
    private Text textRef2;

    void Start()
    {
        textRef1 = GameObject.Find("EnergyText").GetComponent<Text>();
        textRef2 = GameObject.Find("UpgradePoints").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        textRef1.text = "" + Data.control.energy;
        textRef2.text = "" + Data.control.upgradePoints;
    }
}
