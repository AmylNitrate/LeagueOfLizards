using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //private Text textRef1;

    public static UIManager instance;

    void Awake()
    {
        instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void GoToLevel(string Level)
    {
        SceneManager.LoadScene(Level);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void OpenInvitationScreen()
    {
        MultiplayerController.Instance.OpenInvitationScreen();
    }

    // Use this for initialization
    void Start ()
    {
        //textRef1 = GameObject.Find("EnergyText").GetComponent<Text>();

    }
	
	// Update is called once per frame
      
	void Update ()
    {
        //textRef1.text = "" + Data.control.energy;

    }

	public void SignIn()
	{
		MultiplayerController.Instance.SignInAndStartMPGame();
	}

    public void SignOut()
    {
        MultiplayerController.Instance.SignOut();
    }
		

    //void Save()
    //{
    //    Data.control.Save();
    //}
    //void Load()
    //{
    //    Data.control.Load();
    //}
}
