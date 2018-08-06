using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[System.Serializable]
public class BaseModel : IBaseModel
{ 
    public float Speed { get; set; }
    public ReactiveProperty<int> Hp { get; set; }

    [System.NonSerialized]
    public Vector2 position;
}
