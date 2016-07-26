﻿using UnityEngine;
using System.Collections;

public class GameSparksManager : MonoBehaviour {

    public static GameSparksManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
