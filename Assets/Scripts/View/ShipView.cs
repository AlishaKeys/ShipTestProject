using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public interface IShipView
{
}

public class ShipView : ShipElement, IShipView
{
    void Start()
    {
        app.model.Life = app.model.lives;

        UpdateLivesText();

        Hit();

        Shoot();

        Observable.EveryUpdate()
          .Subscribe(x =>
          {
              Move();
          })
          .AddTo(this);
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float xPos = horizontal;
        float yPos = vertical;

        transform.Translate(new Vector3(xPos, yPos, 0) * app.model.speed * Time.deltaTime);
    }

    void Hit()
    {
        gameObject.OnTriggerEnter2DAsObservable()
            .Where(x => x.CompareTag("Asteroid"))
            .Select(x => x)
            .Distinct()
            .Subscribe(_ =>
            {
                app.controller.OnNotification(ShipNotification.Asteroid, this);
                UpdateLivesText();
            })
            .AddTo(this);
    }

    void UpdateLivesText()
    {
        app.controller.lives.text = app.model.Life.ToString();
    }

    void Shoot()
    {
        var clickSpace = Observable.EveryUpdate()
            .Where(x => Input.GetKey(KeyCode.Space));
            
            clickSpace.Buffer(clickSpace.Throttle(TimeSpan.FromMilliseconds(100)))
            .Where(x => x.Count >= 1) 
            .Subscribe(_ =>
            {
                GameObject bullet = Instantiate(app.controller.weapon, transform.position, transform.rotation, transform.parent);

                bullet.OnBecameInvisibleAsObservable()
                    .Subscribe(x => Destroy(bullet)).AddTo(bullet);

                bullet.OnTriggerEnter2DAsObservable()
                    .Where(x => x.CompareTag("Asteroid"))
                    .Select(x => x)
                    .Distinct()
                    .Subscribe(x => app.controller.OnNotification(ShipNotification.ShootAsteroid, this)).AddTo(bullet);

                bullet.UpdateAsObservable()
                    .Subscribe(x => bullet.transform.Translate(bullet.transform.up * app.model.speed * Time.deltaTime)).AddTo(bullet);
            });
    }
}
