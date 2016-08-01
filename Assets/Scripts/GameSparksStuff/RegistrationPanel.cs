using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RegistrationPanel : MonoBehaviour {

    [SerializeField]
    InputField displayNameField, passwordField, userNameField;

    public void SendInfo()
    {
        GameSparksManager.instance.Register(displayNameField.text, passwordField.text, userNameField.text);
    }
}
