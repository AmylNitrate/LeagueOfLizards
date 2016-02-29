using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PushUp : MonoBehaviour
{

    Animation anim;
    public int Energy;
    float timeLeft = 30;

    void Start()
    {
        Energy = 50;
        anim = GetComponent<Animation>();

    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            SceneManager.LoadScene("Menu");
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
            if(!GameObject.Find("bar").GetComponent<Bar>().Good)
            {
                Energy -= 2;
            }
        }
        if (Input.GetMouseButtonUp(0))
            anim.Stop("Take001");

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
