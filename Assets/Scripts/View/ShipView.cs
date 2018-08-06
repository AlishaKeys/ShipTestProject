using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class ShipView : BaseView<ShipPresenter>
{
    public ReactiveCommand Ressurect;

    public Text hpText;

    public void RenderHp(int hp)
    {
        hpText.text = hp.ToString();
    }


    public override void Move(float speed)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime);
    }

    public void Shoot()
    {
        Generator.InstantiateItem(Presenter.weapon.name, transform.position, transform.rotation);
        var weapon = Instantiate(Presenter.weapon, transform.position, transform.rotation, transform.parent);
    }
}
