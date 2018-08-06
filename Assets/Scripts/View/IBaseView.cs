using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface IBaseView<T>
{
    void Move(float speed);
}
