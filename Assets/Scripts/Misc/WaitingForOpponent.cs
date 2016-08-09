using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaitingForOpponent : MonoBehaviour {

    Text myText;

    [SerializeField]
    string myString = "Waiting for opponent";

    void Awake()
    {
        myText = GetComponent<Text>();
        StartCoroutine(ChangeText());
    }

    IEnumerator ChangeText()
    {
        yield return new WaitForSeconds(0.3f);
        myText.text = myString + "..";
        yield return new WaitForSeconds(0.3f);
        myText.text = myString + "...";
        yield return new WaitForSeconds(0.3f);
        myText.text = myString + ".";
        StartCoroutine(ChangeText());
    }
}
