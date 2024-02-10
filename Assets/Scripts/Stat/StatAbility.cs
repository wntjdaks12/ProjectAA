using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStat
{
    public StatAbility StatAbility { get; set; }
}

public class StatAbility
{
    public Stat Stat { get; private set; }

    public StatAbility(Stat _stat)
    {
        Stat = _stat;
    }

    public int MaxHp
    {
        get
        {
            return (int)Stat.GetTotalStatValue(Stat.StatTypes.MaxHp);
        }
    }

    public int AttackDamage
    {
        get
        {
            return (int)Stat.GetTotalStatValue(Stat.StatTypes.AttackDamage);
        }
    }

    public int AbilityPower
    {
        get
        {
            return (int)Stat.GetTotalStatValue(Stat.StatTypes.AbilityPower);
        }
    }

    public float AttackSpeed
    {
        get
        {
            return Stat.GetTotalStatValue(Stat.StatTypes.AttackSpeed);
        }
    }

    public int CriticalDamage
    {
        get
        {
            return (int)Stat.GetTotalStatValue(Stat.StatTypes.CriticalDamage);
        }
    }

    public int CriticalProbability
    {
        get
        {
            return (int)Stat.GetTotalStatValue(Stat.StatTypes.CriticalProbability);
        }
    }

    public int AttackRange
    {
        get
        {
            return (int)Stat.GetTotalStatValue(Stat.StatTypes.AttackRange);
        }
    }

    public int ReduceArmor
    {
        get
        {
            return (int)Stat.GetTotalStatValue(Stat.StatTypes.ReduceArmor);
        }
    }

    public int MagicResist
    {
        get
        {
            return (int)Stat.GetTotalStatValue(Stat.StatTypes.MagicResist);
        }
    }

    public float MaxMoveSpeed
    {
        get
        {
            return Stat.GetTotalStatValue(Stat.StatTypes.MaxMoveSpeed);
        }
    }

    public float JumpPower
    {
        get
        {
            return Stat.GetTotalStatValue(Stat.StatTypes.JumpPower);
        }
    }

    public int SkillAttackPower
    {
        get
        {
            return SkillAttackDamage + SkillAbilityPower;
        }
    }

    public int SkillAttackDamage
    {
        get
        {
            return (int)Stat.GetTotalStatValue(Stat.StatTypes.SkillAttackDamage);
        }
    }

    public int SkillAbilityPower
    {
        get
        {
            return (int)Stat.GetTotalStatValue(Stat.StatTypes.SkillAbilityPower);
        }
    }

    public float CooldownTime
    {
        get
        {
            return Stat.GetTotalStatValue(Stat.StatTypes.SkillCooldownTime);
        }
    }

    public virtual float SkillAttackRange
    {
        get
        {
            return Stat.GetTotalStatValue(Stat.StatTypes.SkillAttackRange);
        }
    }

    public int SkillHealing
    {
        get
        {
            return int.Parse((Stat.GetTotalStatValue(Stat.StatTypes.SkillHealing) + (SkillAttackPower * SkillHealingIncRatePerDamage * 0.01f)).ToString());
        }
    }

    public float SkillHealingIncRatePerDamage
    {
        get
        {
            return Stat.GetTotalStatValue(Stat.StatTypes.SkillHealingIncRatePerDamage);
        }
    }

    public int SkillDPSCount
    {
        get
        {
            return (int)Stat.GetTotalStatValue(Stat.StatTypes.SkillDPSCount);
        }
    }

    public float SkillDPSSecond
    {
        get
        {
            return Stat.GetTotalStatValue(Stat.StatTypes.SkillDPSSecond);
        }
    }

    public float SkillAttackDamageRate
    {
        get
        {
            return Stat.GetTotalStatValue(Stat.StatTypes.SkillAttackDamageRate);
        }
    }

    public float SkillAbilityPowerRate
    {
        get
        {
            return Stat.GetTotalStatValue(Stat.StatTypes.SkillAbilityPowerRate);
        }
    }

    public int TotalSkillAttackDamage
    {
        get
        {
            return SkillAttackDamage + (int)((SkillAttackDamage * SkillAttackDamageRate) / 100);
        }
    }

    public int TotalSkillAbilityPower
    {
        get
        {
            return SkillAbilityPower + (int)((SkillAbilityPower * SkillAbilityPowerRate) / 100);
        }
    }
}
