using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dewlap : MonoBehaviour
{

    public GameObject dewlap;
    public Collider dewlapCollider;
    float timeLeft = 15;

    public GameObject gameOver;
    bool stop;

    private Text textRef2;
    // Use this for initialization
    void Start ()
    {
        textRef2 = GameObject.Find("Timer").GetComponent<Text>();
        stop = false;
	}

    // Update is called once per frame
    void Update()
    {

        if (!stop)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                stop = true;
                gameOver.SetActive(true);
            }

            dewlap.transform.Translate((float)-0.01, (float)0.01, 0);

            if (Input.GetMouseButtonDown(0))
            {
                Data.control.energy--;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                    if (hit.collider == dewlapCollider)
                        dewlap.transform.Translate((float)0.05, (float)-0.05, 0);
            }

        }

        textRef2.text = "Timer = " + (int)timeLeft;

    }


}

