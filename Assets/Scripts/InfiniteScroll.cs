using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour {

    public ScrollRect scroll;
    private int count = 0;
    private float size = 0f;
    private Transform panelTr = null;
    // Use this for initialization
    void Start () {

        
            count = panelTr.childCount - 1; // How many kids do we have?
            //size = GetButtonSize();   // Here you pass the width of a button
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
