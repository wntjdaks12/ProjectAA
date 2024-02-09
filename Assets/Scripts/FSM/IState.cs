using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void OperateEnter();
    public void OperateUpdate();
    public void OperateExit();

    public event Action enterEvent;
    public event Action exitEvent;
    public event Action updateEvent;
}
