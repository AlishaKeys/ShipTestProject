using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class AsteroidView : BaseView<AsteroidPresenter>
{
    public override void Move(float speed)
    {
        transform.Translate(-transform.up * speed * Time.deltaTime);
    }
}

