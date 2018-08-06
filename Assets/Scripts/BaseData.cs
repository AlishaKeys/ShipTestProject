using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseData
{
    [System.NonSerialized] private GameObject _inst; // ссылка на сам объект
    public GameObject Inst { set { _inst = value; } }
    public string Name { get; set; }
    public BaseModel model { get; set; }

    public BaseData() { }

    public BaseData(string name, Vector2 position)
    {
        this.Name = name;
        this.model.position = position;
    }

    public BaseData(GameObject current, string name, Vector2 position)
    {
        this.Inst = current;
        this.Name = name;
        this.model.position = position;
    }

    public virtual void Update()
    {
        if (_inst == null) // если объект был удален, он так же не будет восстановлен после загрузки сцены
        {
            this.Name = null;
            return;
        }

        this.model.position = (Vector2)_inst.transform.position;
    }
}
