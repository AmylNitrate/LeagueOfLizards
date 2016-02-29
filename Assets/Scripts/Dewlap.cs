using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Dewlap : MonoBehaviour
{

    public GameObject dewlap;
    public Collider dewlapCollider;
    public int Energy;
    float timeLeft = 30;

    // Use this for initialization
    void Start ()
    {
        Energy = 20;

	}

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            SceneManager.LoadScene("Menu");
        }

        dewlap.transform.Translate((float)-0.01, (float)0.01, 0);

        //foreach (Touch touch in Input.touches)
        //{
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        // Construct a ray from the current touch coordinates
        //        Ray ray;
        //        ray = Camera.main.ScreenPointToRay(touch.position);

        //        if (Physics.Raycast(ray))
        //        {
        //            dewlap.transform.Translate((float)0.01, (float)-0.01, 0);

        //        }
        //    }
        //}


        if (Input.GetMouseButtonDown(0))
        {
            Energy --;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.collider == dewlapCollider)
                        dewlap.transform.Translate((float)0.05, (float)-0.05, 0);



        }

        if(Energy <= 0)
        {
            SceneManager.LoadScene("Menu");
        }

    }
    void OnGUI()
    {
        GUI.Label(new Rect(50, 150, 200, 50), "Energy: " + Energy);
        {
        }
        GUI.Label(new Rect(50, 100, 200, 50), "Timer: " + (int)timeLeft);
        {
        }
    }


}

