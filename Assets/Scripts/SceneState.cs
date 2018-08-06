using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneState
{
    public List<BaseData> itemList = new List<BaseData>();

    public SceneState() { }

    public void AddItem(BaseData item)
    {
        itemList.Add(item);
    }

    public void Update()
    {
        foreach (BaseData t in itemList)
            t.Update();
    }
}
