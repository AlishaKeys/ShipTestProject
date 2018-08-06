using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipPresenter : BasePresenter<ShipView, ShipModel>
{
    public Button ressurectButton;
    public GameObject weapon;

    void Start()
    {
        Model = new ShipModel();
        Model.Speed = .5f;
        Model.Hp = new ReactiveProperty<int>(3);

        //восстановление
        View.Ressurect = Model.Hp.Select(x => x <= 0).ToReactiveCommand();

        View.Ressurect.Subscribe(_ => Model.Hp.Value = 3);

        Model.Hp
            .ObserveEveryValueChanged(x => x.Value)
            .Subscribe(xs =>
            {
                View.RenderHp(xs);

            }).AddTo(this);

        View.Ressurect.BindTo(ressurectButton);

        //движение
        Observable.EveryUpdate()
          .Subscribe(x =>
          {
              View.Move(Model.Speed);
              Model.position = transform.position;
          })
          .AddTo(this);        

        //смерть
        Model.Hp.Where(x => x <= 0).Subscribe(_ => GameManager.Instance.LoseGame());

        //стрельба
        Observable.EveryUpdate()
          .Where(x => Input.GetKey(KeyCode.Space))
          .Throttle(TimeSpan.FromMilliseconds(100))
          .Subscribe(x =>
          {
              View.Shoot();
          })
          .AddTo(this);

        //урон
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
                Model.Hp.Value--;
            })
            .AddTo(this);
    }
}
