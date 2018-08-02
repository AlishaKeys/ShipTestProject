using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    List<GameSave> savedGames = new List<GameSave>();

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        savedGames.Add(GameSave.current);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/savedGames.gd");
        bf.Serialize(file, savedGames);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/savedGames.gd", FileMode.Open);
            savedGames = (List<GameSave>)bf.Deserialize(file);
            file.Close();
        }
    }
}
