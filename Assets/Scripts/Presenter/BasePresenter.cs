using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePresenter<M, T> : MonoBehaviour, IBasePresenter<M, T>
{
    public M View;
    public T Model;
    
    public virtual void Hit()
    {

    }
}
