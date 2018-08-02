using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipElement : MonoBehaviour
{
    public ShipApplication app
    {
        get
        {
            return GameObject.FindObjectOfType<ShipApplication>();
        }
    }
}
