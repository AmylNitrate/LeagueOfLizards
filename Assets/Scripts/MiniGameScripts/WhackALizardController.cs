using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhackALizardController : MonoBehaviour
{
    public static WhackALizardController instance;
    [SerializeField]
    List<GameObject> spawnPoints = new List<GameObject>();

    [SerializeField]
    GameObject lizard, tail;

    public float baseSpawnTime;
    float spawnTime;

    public int points;

    void Awake()
    {
        instance = this;
        GetRandomSpawnTime();

    }

    void Update()
    {
        if (spawnTime <= 0)
        {
            Spawn();
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
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
}
