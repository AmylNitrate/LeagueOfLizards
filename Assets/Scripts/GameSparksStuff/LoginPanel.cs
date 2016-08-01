using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginPanel : MonoBehaviour {

    [SerializeField]
    InputField passwordField, userNameField;

    public void SendInfo()
    {
        GameSparksManager.instance.Authenticate(userNameField.text, passwordField.text);
    }
}