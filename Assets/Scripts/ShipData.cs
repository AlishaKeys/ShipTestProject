using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : BaseData
{
    public float Health { get; set; }

    public ShipData() { }

    public ShipData(string name, Vector3 position, Quaternion rotation, float health) : base(name, position)
    {
        this.Health = health;
    }

    public ShipData(GameObject current, string name, Vector3 position) : base(current, name, position)
    {
        this.Health = model.Hp.Value;
    }

    public override void Update()
    {
        base.Update();

        this.Health = model.Hp.Value;
    }
}
