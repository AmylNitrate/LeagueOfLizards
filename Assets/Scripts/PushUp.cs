using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PushUp : MonoBehaviour
{

    Animation anim;
    int Energy;
    float timeLeft = 20;
    public bool stop;
    public GameObject gameOver;

    private Text textRef1;
    private Text textRef2;

    void Start()
    {
        Energy = GameObject.Find("Data").GetComponent<Data>().energy;
        anim = GetComponent<Animation>();
        stop = false;
        textRef1 = GameObject.Find("EnergyText").GetComponent<Text>();
        textRef2 = GameObject.Find("Timer").GetComponent<Text>();
    }

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

            foreach (AnimationState state in anim)
            {
                state.speed = 0.3f;
            }


            if (Input.GetMouseButtonDown(0))
            {
                anim.Play("Take001");
                if (GameObject.Find("bar").GetComponent<Bar>().Good)
                {
                    Energy -= 1;
                }
                if (!GameObject.Find("bar").GetComponent<Bar>().Good)
                {
                    Energy -= 2;
                }
            }
            if (Input.GetMouseButtonUp(0))
                anim.Stop("Take001");
        }

        textRef1.text = "Energy = " + Energy;
        textRef2.text = "Timer = " + (int)timeLeft;
    }


}
