using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : AsteroidElement, IAsteroidController
{
    public void OnNotification(string p_event_path, System.Object p_target, params object[] p_data)
    {
        if (p_event_path == AsteroidNotification.Bullet)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
public interface IAsteroidController
{
}

