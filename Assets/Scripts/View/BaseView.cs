using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BaseView<T> : MonoBehaviour, IBaseView<T>
{
    public T Presenter;
    public string enemyString;

    public virtual void Move(float speed)
    {

    }
}
