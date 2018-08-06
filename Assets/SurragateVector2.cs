using System.Collections;
using UnityEngine;

[System.Serializable]
public struct SurrogateVector2
{

    public float x, y;

    public SurrogateVector2(float rX, float rY)
    {
        x = rX;
        y = rY;
    }

    public static implicit operator Vector2(SurrogateVector2 rValue)
    {
        return new Vector3(rValue.x, rValue.y);
    }

    public static implicit operator SurrogateVector2(Vector2 rValue)
    {
        return new SurrogateVector2(rValue.x, rValue.y);
    }
}