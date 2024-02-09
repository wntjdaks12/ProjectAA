using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : Actor
{
    public event Action<Character> HitEvent;
    public event Action<Character> DeathEvent;

    public List<SkillTool> SkillTools { get; private set; }

    private int currentHp;
    public virtual int CurrentHp
    {
        get
        {
            return currentHp;
        }
        set
        {
            currentHp = Mathf.Clamp(value, 0, CurrentHp);
        }
    }

    private float currentMoveSpeed;
    public virtual float CurrentMoveSpeed
    {
        get
        {
            return currentMoveSpeed;
        }
        set
        {
            currentMoveSpeed = Mathf.Clamp(value, 0, MaxMoveSpeed);
        }
    }

    public virtual int MaxHp
    {
        get 
        {
            return (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.MaxHp);
        }
    }

    public virtual int AttackDamage
    {
        get
        {
            return (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.AttackDamage);
        }
    }

    public virtual int AbilityPower
    {
        get
        {
            return (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.AbilityPower);
        }
    }

    public virtual float AttackSpeed
    {
        get
        {
            return StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.AttackSpeed);
        }
    }

    public virtual int CriticalDamage
    {
        get
        {
            return (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.CriticalDamage);
        }
    }

    public virtual int CriticalProbability
    {
        get
        {
            return (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.CriticalProbability);
        }
    }

    public virtual int AttackRange
    {
        get
        {
            return (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.AttackRange);
        }
    }

    public virtual int ReduceArmor
    {
        get
        {
            return (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.ReduceArmor);
        }
    }

    public virtual int MagicResist
    {
        get
        {
            return (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.MagicResist);
        }
    }

    public virtual float MaxMoveSpeed
    {
        get
        {
            return StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.MaxMoveSpeed);
        }
    }

    public virtual float JumpPower
    {
        get
        {
            return StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.JumpPower);
        }
    }

    public override void Init(Transform transform)
    {
        base.Init(transform);

        SkillTools = GameApplication.Instance.PlayerManager.GetSkillTools(Id);

        currentHp = MaxHp;
        currentMoveSpeed = 0;
    }

    public void OnHit(int damage)
    {
        CurrentHp -= damage;

        HitEvent?.Invoke(this);

        if (currentHp <= 0)
        {
            DeathEvent?.Invoke(this);
        }
    }

    public void OnHeal(int heal)
    {
        CurrentHp = Mathf.Clamp(CurrentHp + heal, 0, MaxHp);
    }
}
