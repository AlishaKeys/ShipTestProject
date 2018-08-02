using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidElement : MonoBehaviour
{ 
    public AsteroidApplication app
    {
        get
        {
            return GameObject.FindObjectOfType<AsteroidApplication>();
        }
    }

}
