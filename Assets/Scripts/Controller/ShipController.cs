using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : ShipElement
{
    public GameObject weapon;
    public Text lives;

    public void OnNotification(string p_event_path, System.Object p_target, params object[] p_data)
    {
        if (p_event_path == ShipNotification.ShootAsteroid)
        {
            app.model.CountAsteroids++;
            if (app.model.CountAsteroids >= app.model.winCountAsteroids)
            {
                OnGameWin();
            }
        }
        else if (p_event_path == ShipNotification.Asteroid)
        {
            ModifyLives();
            if (app.model.Life == 0)
            {
                OnGameLose();
            }
        }
    }

    public void ModifyLives()
    {
        app.model.Life--;
    }

    public void OnGameLose()
    {
        LevelManager.Instance.Lose();
    }

    public void OnGameWin()
    {
        LevelManager.Instance.Win();
    }
}
