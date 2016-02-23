using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public void GoToLevel(string Level)
    {
        SceneManager.LoadScene(Level);
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
      
	void Update ()
    {

	}
}
