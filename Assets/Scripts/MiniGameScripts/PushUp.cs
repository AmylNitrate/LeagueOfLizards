using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PushUp : MonoBehaviour
{
    public static PushUp instance;

    Animation anim;
    float timeLeft = 20;
    public bool stop;
    public GameObject gameOver;

    [SerializeField]
    Text timerText, pointsValue;

    [SerializeField]
    Button goToMenu;

    [SerializeField]
    Bar bar;

    public int myPoints, enemyPoints;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //Data.control.points = 0;
        anim = GetComponent<Animation>();
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

            foreach (AnimationState state in anim)
            {
                state.speed = 0.3f;
            }


            if (Input.GetMouseButtonDown(0) && !anim.isPlaying)
            {
                anim.Play("Take001");
                if (bar.Good)
                {
                    myPoints += 5;
                }
                else
                {
                    myPoints += 1;
                }
            }
                //anim.Stop("Take001");
        }

        timerText.text = "Timer = " + timeLeft.ToString("0.00");
        pointsValue.text = myPoints.ToString();
    }

    void GameOver()
    {
        stop = true;
        gameOver.SetActive(true);
        MultiplayerController.Instance.SendMyEscalationPoints(myPoints);
    }

    public void ComparePoints()
    {
        if (myPoints == enemyPoints)
        {
            //Tie
            MiniGameTracker.instance.SetEscalationWinner(MiniGameTracker.Players.localPlayer);
        }
        if (myPoints > enemyPoints)
        {
            MiniGameTracker.instance.SetEscalationWinner(MiniGameTracker.Players.localPlayer);
        }
        else
        {
            MiniGameTracker.instance.SetEscalationWinner(MiniGameTracker.Players.enemy);
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
