using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseApplication<M, T, C> : MonoBehaviour
{
    public M model;
    public T view;
    public C controller;

    public virtual void Notify(string p_event_path, Object p_target, params object[] p_data)
    {
    }
}