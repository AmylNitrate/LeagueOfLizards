using UnityEngine;
using System.Collections;

public class SaveToGameSparks{

    public static SaveToGameSparks _instance = null;

    public static SaveToGameSparks Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveToGameSparks();
            }
            return _instance;
        }
    }

    public void SaveData()
    {

    }
}
