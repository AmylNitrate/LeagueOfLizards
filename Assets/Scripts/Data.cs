using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Data : MonoBehaviour {


    public int energy;
    public int upgradePoints;
    private Text textRef1;
    private Text textRef2;

	// Use this for initialization
	void Start ()
    {
        energy = 50;
        upgradePoints = 10;
        textRef1 = GameObject.Find("EnergyText").GetComponent<Text>();
        textRef2 = GameObject.Find("UpgradePoints").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        textRef1.text = "Energy = " + energy;
        textRef2.text = "Upgrade Points = " + upgradePoints;
	}

}
