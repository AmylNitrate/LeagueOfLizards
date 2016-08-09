using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSparksManager : MonoBehaviour {

    public static GameSparksManager instance = null;

    [SerializeField]
    GameObject otherAuthenticateOptions, pleaseWaitText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            TryDeviceAuthenticate();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void TryDeviceAuthenticate()
    {
        Debug.Log("Attempting device authentication");
        new GameSparks.Api.Requests.AuthenticationRequest().SetPassword("temp").SetUserName("temp").Send((response) =>
        {
            if (!response.HasErrors)
            {
                Debug.Log("Authenticated successfully");
                SceneManager.LoadScene(1);
            }
            else
            {
                Debug.Log("Error when authenticating");
                otherAuthenticateOptions.SetActive(true);
                pleaseWaitText.SetActive(false);
            }
        });
    }

    /// <summary>
    /// Registers the player with the game
    /// </summary>
    /// <param name="displayName">The display name the player has chosen</param>
    /// <param name="password">The password the player has chosen</param>
    /// <param name="userName">The user name the player has chosen</param>
    public void Register(string displayName, string password, string userName)
    {
        new GameSparks.Api.Requests.RegistrationRequest().SetDisplayName(displayName).SetPassword(password).SetUserName(userName).Send((response) =>
        {
            if (!response.HasErrors)
            {
                Debug.Log("Player registered successfully as: " + displayName);
                Authenticate(userName, password);
            }
            else
            {
                Debug.Log("Error registering the player");
            }
        });
    }

    /// <summary>
    /// Authenticates an already registered player
    /// </summary>
    /// <param name="userName">The username of the player</param>
    /// <param name="password">The password of the user</param>
    public void Authenticate(string userName, string password)
    {
        new GameSparks.Api.Requests.AuthenticationRequest().SetUserName(userName).SetPassword(password).Send((response) =>
        {
            if (!response.HasErrors)
            {
                Debug.Log("Player authenticated successfully");
                SceneManager.LoadScene(1);
            }
            else
            {
                Debug.Log("Error when authenticating player");
            }
        });
    }
}
