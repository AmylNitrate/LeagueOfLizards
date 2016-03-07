using UnityEngine;
using System.Collections;

public class Attributes : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void Bluff()
    {
        //Bluffing moves your energy range up
        switch(Data.control.bluffPoints)
        {
            case 1:
                //plus 5%
                break;

            case 2:
                //plus 10%
                break;

            case 3:
                //plus 15%
                break;

            default:
                break;

        }
    }

    void Assess()
    {
        //Assess decreases your opponents energy range
        switch(Data.control.assessPoints)
        {
            case 1:
                //plus 5%
                break;

            case 2:
                //plus 10%
                break;

            case 3:
                //plus 15%
                break;

            default:
                break;

        }
    }
}
