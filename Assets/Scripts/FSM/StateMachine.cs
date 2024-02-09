using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    // 유한 머신 기계 상체 상태 종류
    public enum UpperBodyStateTypes
    {
        Idle,
        Walk,
        Run,
        Attack,
        Death
    }

    // 유한 머신 기계 하체 상태 종류
    public enum LowerBodyStateTypes
    {
        Idle,
        Walk,
        Run,
        Attack,
        Death
    }

    public IState CurrentUpperBodyState { get; private set; }
    public IState CurrentLowerBodyState { get; private set; }

    public StateMachine(IState upperBodyState, IState lowerBodyState)
    {
        CurrentUpperBodyState = upperBodyState;
        CurrentLowerBodyState = lowerBodyState;
    }

    /// <summary>
    /// 유한 머신 기계의 상태를 설정합니다.
    /// </summary>
    /// <param name="upperBodyState">상체 상태</param>
    /// <param name="lowerBodyState">하체 상태</param>
    public void setMachine(IState upperBodyState, IState lowerBodyState)
    {
        if (upperBodyState != CurrentUpperBodyState)
        {
            CurrentUpperBodyState.OperateExit();

            CurrentUpperBodyState = upperBodyState;

            CurrentUpperBodyState.OperateEnter();
        }

        if (lowerBodyState != CurrentLowerBodyState)
        {
            CurrentLowerBodyState.OperateExit();

            CurrentLowerBodyState = lowerBodyState;

            CurrentLowerBodyState.OperateEnter();
        }
    }

    public UpperBodyStateTypes GetCurrentUpperBodyState()
    {
        if (CurrentUpperBodyState is IdleState)
        {
            return UpperBodyStateTypes.Idle;
        }
        else if (CurrentUpperBodyState is WalkState)
        {
            return UpperBodyStateTypes.Walk;
        }
        else if (CurrentUpperBodyState is RunState)
        {
            return UpperBodyStateTypes.Run;
        }
        else if (CurrentUpperBodyState is AttackState)
        {
            return UpperBodyStateTypes.Attack;
        }
        else if (CurrentUpperBodyState is DeathState)
        {
            return UpperBodyStateTypes.Death;
        }
        else
            return UpperBodyStateTypes.Idle;
    }

    public LowerBodyStateTypes GetCurrentLowerBodyState()
    {
        if (CurrentLowerBodyState is IdleState)
        {
            return LowerBodyStateTypes.Idle;
        }
        else if (CurrentLowerBodyState is WalkState)
        {
            return LowerBodyStateTypes.Walk;
        }
        else if (CurrentLowerBodyState is RunState)
        {
            return LowerBodyStateTypes.Run;
        }
        else if (CurrentLowerBodyState is AttackState)
        {
            return LowerBodyStateTypes.Attack;
        }
        else if (CurrentLowerBodyState is DeathState)
        {
            return LowerBodyStateTypes.Death;
        }
        else
            return LowerBodyStateTypes.Idle;
    }

    /// <summary>
    /// 현재 상태를 작동시킵니다.
    /// </summary>
    public void OperateUpdate()
    {
        CurrentUpperBodyState.OperateUpdate();
        CurrentLowerBodyState.OperateUpdate();
    }
}
