using UnityEngine;
using System.Collections;

public class WhackHole : MonoBehaviour {

    [SerializeField]
    float timer;
    float timerAtStart;

    void Start()
    {
        timerAtStart = timer;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = Random.Range(timerAtStart * 0.6f, timerAtStart * 1.4f);
            Spawn();
        }
    }

    void Spawn()
    {
        //Make lizard pop out the lizard hole
    }
}
