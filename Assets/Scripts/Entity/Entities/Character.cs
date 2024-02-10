using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Character : Actor
{
    public event Action<Character> HitEvent;
    public event Action<Character> DeathEvent;

    public List<SkillTool> SkillTools { get; private set; }
    public SubItem[] SubItems { get; private set; }

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

    public virtual int MaxHp
    {
        get
        {
            var entityMaxHp = (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.MaxHp);
            var subItemMaxHp = SubItems.Sum(x => x.StatAbility.MaxHp);
            return entityMaxHp + subItemMaxHp;
        }
    }

    public virtual int AttackDamage
    {
        get
        {
            var entityAttackDamage = (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.AttackDamage);
            var subItemAttackDamage = SubItems.Sum(x => x.StatAbility.AttackDamage);
            return entityAttackDamage + subItemAttackDamage;
        }
    }

    public virtual int AbilityPower
    {
        get
        {
            var entityAbilityPower = (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.AbilityPower);
            var subItemAbilityPower = SubItems.Sum(x => x.StatAbility.AbilityPower);
            return entityAbilityPower + subItemAbilityPower;
        }
    }

    public virtual float AttackSpeed
    {
        get
        {
            var entityAttackSpeed = StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.AttackSpeed);
            var subItemAttackSpeed = SubItems.Sum(x => x.StatAbility.AttackSpeed);
            return entityAttackSpeed + subItemAttackSpeed;
        }
    }

    public virtual int CriticalDamage
    {
        get
        {
            var entityCriticalDamage = (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.CriticalDamage);
            var subItemCriticalDamage = SubItems.Sum(x => x.StatAbility.CriticalDamage);
            return entityCriticalDamage + subItemCriticalDamage;
        }
    }

    public virtual int CriticalProbability
    {
        get
        {
            var entityCriticalProbability = (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.CriticalProbability);
            var subItemCriticalProbability = SubItems.Sum(x => x.StatAbility.CriticalProbability);
            return entityCriticalProbability + subItemCriticalProbability;
        }
    }

    public virtual int AttackRange
    {
        get
        {
            var entityCriticalProbability = (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.AttackRange);
            var subItemCriticalProbability = SubItems.Sum(x => x.StatAbility.AttackRange);
            return entityCriticalProbability + subItemCriticalProbability;
        }
    }

    public virtual int ReduceArmor
    {
        get
        {
            var entityReduceArmor = (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.ReduceArmor);
            var subItemReduceArmor = SubItems.Sum(x => x.StatAbility.ReduceArmor);
            return entityReduceArmor + subItemReduceArmor;
        }
    }

    public virtual int MagicResist
    {
        get
        {
            var entityMagicResist = (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.MagicResist);
            var subItemMagicResist = SubItems.Sum(x => x.StatAbility.MagicResist);
            return entityMagicResist + subItemMagicResist;
        }
    }

    public virtual float MaxMoveSpeed
    {
        get
        {
            var entityMaxMoveSpeed = StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.MaxMoveSpeed);
            var subItemMaxMoveSpeed = SubItems.Sum(x => x.StatAbility.MaxMoveSpeed);
            return entityMaxMoveSpeed + subItemMaxMoveSpeed;
        }
    }

    public virtual float JumpPower
    {
        get
        {
            var entityJumpPower = StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.JumpPower);
            var subItemJumpPower = SubItems.Sum(x => x.StatAbility.JumpPower);
            return entityJumpPower + subItemJumpPower;
        }
    }

    public override void Init(Transform transform)
    {
        base.Init(transform);

        SkillTools = GameApplication.Instance.PlayerManager.GetSkillTools(Id);
        if(this is Hero) SubItems = GameApplication.Instance.PlayerManager.GetSubItems(Id);

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
