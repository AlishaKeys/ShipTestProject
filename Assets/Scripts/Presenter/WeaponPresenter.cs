using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class WeaponPresenter : BasePresenter<WeaponView, WeaponModel>
{
    public CompositeDisposable disposables;

    private void Awake()
    {
        disposables = new CompositeDisposable();

        Model = new WeaponModel();
        Model.Speed = 1f;
        Model.Hp = new ReactiveProperty<int>(1);

        //движение
        Observable.EveryUpdate()
          .Subscribe(x =>
          {
              View.Move(Model.Speed);
              Model.position = transform.position;
          })
          .AddTo(disposables);

        //смерть
        Model.Hp.Where(x => x <= 0)
            .Subscribe(_ =>
            {
                disposables.Dispose();
                Destroy(gameObject);
            });

        DestroyAsteroidInvisible();

        Hit();
    }

    public override void Hit()
    {
        gameObject.OnTriggerEnter2DAsObservable()
            .Where(x => x.CompareTag(View.enemyString))
            .Select(x => x)
            .Distinct()
            .Subscribe(_ =>
            {
                Model.Hp.Value -= 99;
            })
            .AddTo(this);
    }

    void DestroyAsteroidInvisible()
    {
        Observable.Timer(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                gameObject.OnBecameInvisibleAsObservable()
                .Subscribe(__ =>
                {
                    Model.Hp.Value -= 99;
                });
            })
            .AddTo(disposables);
    }
}
