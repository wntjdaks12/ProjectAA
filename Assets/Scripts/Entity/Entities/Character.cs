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
            currentMoveSpeed = Mathf.Clamp(value, 0, StatAbility.MaxMoveSpeed);
        }
    }

    public override void Init(Transform transform)
    {
        base.Init(transform);

        SkillTools = GameApplication.Instance.PlayerManager.GetSkillTools(Id);

        currentHp = StatAbility.MaxHp;
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
        CurrentHp = Mathf.Clamp(CurrentHp + heal, 0, StatAbility.MaxHp);
    }
}
