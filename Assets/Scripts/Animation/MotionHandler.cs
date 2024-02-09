using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionHandler : MonoBehaviour
{
    public bool isAttack { get; private set; }
    [field:SerializeField] public int AttackStateCount { get; private set; }

    public event Action OnOnetimeStart;
    public event Action OnOnetimeEnd;

    public void Init()
    {
        isAttack = false;
    }

    public void StartAttack()
    {
        isAttack = true;

        OnOnetimeStart?.Invoke();
        OnOnetimeStart = null;
    }

    public void EndAttack()
    {
        isAttack = false;

        OnOnetimeEnd?.Invoke();
        OnOnetimeEnd = null;
    }
}
