using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WhackALizardController : MonoBehaviour
{
    public static WhackALizardController instance;
    [SerializeField]
    List<GameObject> spawnPoints = new List<GameObject>();


	public GameObject lizard;
	public GameObject tail;

    public float baseSpawnTime;
    float spawnTime;

    public GameObject gameOver;
    bool stop;
    float timeLeft = 20;
    [SerializeField]
    private Text timerText, pointsValueText;

    [SerializeField]
    Button goToMenu;

    public int myPoints, enemyPoints;

    void Awake()
    {
        instance = this;
        GetRandomSpawnTime();

    }

    void Start()
    {
        //Data.control.points = 0;
        stop = false;

    }

    void Update()
    {
        if (!stop)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                GameOver();
            }
            if (spawnTime <= 0)
            {
                Spawn();
            }
            else
            {
                spawnTime -= Time.deltaTime;
            }
        }
        timerText.text = "Timer = " + timeLeft.ToString("0.00");
        //textRef3.text = "Points = " + Data.control.points;

    }

    public void UpdatePoints()
    {
        pointsValueText.text = myPoints.ToString();
    }

    void GameOver()
    {
        stop = true;
        gameOver.SetActive(true);
        MultiplayerController.Instance.SendMyFightPoints(myPoints);
    }

    void GetRandomSpawnTime()
    {
        spawnTime = Random.Range(baseSpawnTime * 0.8f, baseSpawnTime * 1.2f);
    }

    void Spawn()
    {
        GameObject temp = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Vector3 pos = temp.transform.position;
        GameObject tempLiz;
        if (temp.GetComponent<WhackHole>().isOccupied)
        {
            Spawn();
        }
        else
        {
            bool spawnTail = (Random.Range(0, 4) == 1) ? true : false;
            if (spawnTail)
            {
                tempLiz = Instantiate(tail, pos, Quaternion.identity) as GameObject;
            }
            else
            {
                tempLiz = Instantiate(lizard, pos, Quaternion.identity) as GameObject;
            }
            tempLiz.GetComponent<WhackLizard>().Init(temp);
            temp.GetComponent<WhackHole>().isOccupied = true;
            GetRandomSpawnTime();
        }
    }

    public void ComparePoints()
    {
        if (myPoints == enemyPoints)
        {
            //Tie
            MiniGameTracker.instance.SetFightWinner(MiniGameTracker.Players.localPlayer);
        }
        if (myPoints > enemyPoints)
        {
            MiniGameTracker.instance.SetFightWinner(MiniGameTracker.Players.localPlayer);
        }
        else
        {
            MiniGameTracker.instance.SetFightWinner(MiniGameTracker.Players.enemy);
        }
        if (GameInfo.current.isKeepingTrack)
        {
            RoundInfo.current.userOneMiniGameScore = myPoints;
            RoundInfo.current.userTwoMiniGameScore = enemyPoints;
        }
        else
        {
            RoundInfo.current.userTwoMiniGameScore = myPoints;
            RoundInfo.current.userOneMiniGameScore = enemyPoints;
        }
        goToMenu.interactable = true;
    }
}
