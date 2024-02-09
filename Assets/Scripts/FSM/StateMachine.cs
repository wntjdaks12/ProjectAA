using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    // ���� �ӽ� ��� ��ü ���� ����
    public enum UpperBodyStateTypes
    {
        Idle,
        Walk,
        Run,
        Attack,
        Death
    }

    // ���� �ӽ� ��� ��ü ���� ����
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
    /// ���� �ӽ� ����� ���¸� �����մϴ�.
    /// </summary>
    /// <param name="upperBodyState">��ü ����</param>
    /// <param name="lowerBodyState">��ü ����</param>
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
    /// ���� ���¸� �۵���ŵ�ϴ�.
    /// </summary>
    public void OperateUpdate()
    {
        CurrentUpperBodyState.OperateUpdate();
        CurrentLowerBodyState.OperateUpdate();
    }
}
