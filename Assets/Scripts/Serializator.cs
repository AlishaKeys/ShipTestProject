using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Serializator
{
    public static void SaveBinary(SceneState state, string dataPath)
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream stream = new FileStream(dataPath, FileMode.Create);
        binary.Serialize(stream, state);
        stream.Close();
    }

    public static SceneState LoadBinary(string dataPath)
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream stream = new FileStream(dataPath, FileMode.Open);
        SceneState state = (SceneState)binary.Deserialize(stream);
        stream.Close();
        return state;
    }
}
