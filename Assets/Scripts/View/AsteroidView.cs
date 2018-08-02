using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class AsteroidView : AsteroidElement, IAsteroidView
{
	void Start ()
    {
        Hit();

        Observable.EveryUpdate()
          .Subscribe(x =>
          {
              Move();
          })
          .AddTo(this);
    }

    void Hit()
    {
        gameObject.OnTriggerEnter2DAsObservable()
            .Where(x => x.CompareTag("Bullet"))
            .Select(x => x)
            .Distinct()
            .Subscribe(_ =>
            {
                app.controller.OnNotification(AsteroidNotification.Bullet, this);
            })
            .AddTo(this);
    }

    void Move()
    {
        transform.Translate(-transform.up * app.model.speed * Time.deltaTime);
    }

    void DestroyAsteroidInvisible()
    {
        Observable.Timer(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                gameObject.OnBecameInvisibleAsObservable()
                .Subscribe(__ =>
                {
                    Destroy(gameObject);
                });
            })
            .AddTo(this);
    }
}

public interface IAsteroidView
{
}

