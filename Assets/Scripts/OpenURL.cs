using UnityEngine;
using System.Collections;

public class OpenURL : MonoBehaviour {

	   public void OpenLink(string Link)
    {
        Application.OpenURL(Link);
    }
	
}
