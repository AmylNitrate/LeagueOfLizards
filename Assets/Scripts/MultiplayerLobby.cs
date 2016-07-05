using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MultiplayerLobby : MonoBehaviour {

    [SerializeField]
    Text waitingText;
    
    void Awake()
    {
        StartCoroutine(WaitingForPlayer());
    }

    IEnumerator WaitingForPlayer()
    {
        yield return new WaitForSeconds(0.6f);
        waitingText.text = "Waiting for player..";
        yield return new WaitForSeconds(0.6f);
        waitingText.text = "Waiting for player...";
        yield return new WaitForSeconds(0.6f);
        waitingText.text = "Waiting for player.";
        StartCoroutine(WaitingForPlayer());
    }
}
