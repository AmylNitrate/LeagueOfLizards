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

    [SerializeField]
    Text timerTextValue, pointsTextValue;

    float myPoints, enemyPoints;


    // Use this for initialization
    void Start ()
    {
        //Data.control.points = 0;
        stop = false;
        StartCoroutine(RemovePoints());
	}

    // Update is called once per frame
    void Update()
    {

        if (!stop)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                GameOver();
            }

            if (dewlap.transform.position.x < 1.8)
            {
                dewlap.transform.Translate((float)-0.01, (float)0.01, 0);
            }
            else if(dewlap.transform.position.x > 1.8)
            {
                Debug.Log("Stop");
            }

            if (Input.GetMouseButtonDown(0) && dewlap.transform.position.x > 1.4)
            {
                //Data.control.energy--;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                    if (hit.collider == dewlapCollider)
                    {
                        dewlap.transform.Translate((float)0.05, (float)-0.05, 0);
                        myPoints++;
                        GetPointsValue();
                    }
            }
            else if(dewlap.transform.position.x < 1.4)
            {
                Debug.Log("STOP");
            }
        }

        timerTextValue.text = "Timer = " + timeLeft.ToString("0.00");

    }

    void GameOver()
    {
        gameOver.SetActive(true);
        stop = true;

        //Send points to opponent
        
    }

    //Lowest points will win
    void GetPointsValue()
    {
        pointsTextValue.text = myPoints.ToString();
    }

    IEnumerator RemovePoints()
    {
        yield return new WaitForSeconds(0.6f);
        if (!stop)
        {
            if (myPoints > 1)
            {
                myPoints -= 2;
                GetPointsValue();
            }
            StartCoroutine(RemovePoints());
        }
    }

    void ComparePoints()
    {
        if (myPoints > enemyPoints)
        {
            MiniGameTracker.instance.SetAssessWinner(MiniGameTracker.Players.localPlayer);
        }
        else
        {
            MiniGameTracker.instance.SetAssessWinner(MiniGameTracker.Players.enemy);
        }
    }
}

