using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad {

    public static Lizard currentLizard;

    public static void Save()
    {
        currentLizard = Lizard.current;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGame.arl");
        bf.Serialize(file, currentLizard);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGame.arl"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.arl", FileMode.Open);
            currentLizard = (Lizard)bf.Deserialize(file);
            Lizard.current = currentLizard;
            file.Close();
        }
        else
        {
            Lizard.current = new Lizard();
            Save();
        }
    }
}
