using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaitingForOpponent : MonoBehaviour {

    Text myText;

    void Awake()
    {
        myText = GetComponent<Text>();
        StartCoroutine(ChangeText());
    }

    IEnumerator ChangeText()
    {
        yield return new WaitForSeconds(0.3f);
        myText.text = "Waiting for opponent..";
        yield return new WaitForSeconds(0.3f);
        myText.text = "Waiting for opponent...";
        yield return new WaitForSeconds(0.3f);
        myText.text = "Waiting for opponent.";
        StartCoroutine(ChangeText());
    }
}
