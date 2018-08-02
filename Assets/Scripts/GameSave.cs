using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSave
{
    public static GameSave current;
    public ShipModel objects;

    public GameSave()
    {
        objects = new ShipModel();
    }
}
