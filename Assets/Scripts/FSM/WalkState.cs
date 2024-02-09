using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState
{
    public event Action enterEvent;
    public event Action exitEvent;
    public event Action updateEvent;

    public void OperateEnter()
    {
        enterEvent?.Invoke();
    }

    public void OperateExit()
    {
        exitEvent?.Invoke();
    }

    public void OperateUpdate()
    {
        updateEvent?.Invoke();
    }
}
