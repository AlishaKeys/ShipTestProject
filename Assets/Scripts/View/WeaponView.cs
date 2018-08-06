using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponView : BaseView<WeaponPresenter>
{
    public override void Move(float speed)
    {
        transform.Translate(transform.up * speed * Time.deltaTime);
    }
}
