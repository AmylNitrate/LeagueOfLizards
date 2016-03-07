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

    int points;

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
            GetRandomSpawnTime();
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
        Vector3 pos = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
        bool spawnTail = (Random.Range(0, 2) == 1) ? true : false;
        if (spawnTail)
        {
            Instantiate(tail, pos, Quaternion.identity);
        }
        else
        {
            Instantiate(lizard, pos, Quaternion.identity);
        }
    }
}
