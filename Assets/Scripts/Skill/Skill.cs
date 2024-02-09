using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Entity
{
    public List<List<int>> VFXIds { get; set; }
    public StrategyTypes StrategyType { get; set; }
    public enum StrategyTypes
    {
        Attack = 0, // 공격 전략 스킬
        Heal = 1, // 치유 전략 스킬
        AttackHeal = 2 // 공격 치유 전략 스킬
    }

    public Types Type { get; set; }
    public enum Types
    {
        Passive = 0, // 패시브
        Active = 1, // 액티브
        AttackPassive = 2 // 공격 패시브 
    }

    public List<int> TargetCounts { get; set; } // value = -1 : 전체 대상???
    public List<float> DamageDelays { get; set; }
    public List<int> RepeatCounts { get; set; } // value = -1 : 피해 받은 대상의 카운트 

    public virtual int SkillAttackPower
    {
        get
        {
            return SkillAttackDamage + SkillAbilityPower;
        }
    }

    public virtual int SkillAttackDamage
    {
        get
        {
            return (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.SkillAttackDamage);
        }
    }

    public virtual int SkillAbilityPower
    {
        get
        {
            return (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.SkillAbilityPower);
        }
    }

    public virtual float CooldownTime
    {
        get
        {
            return StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.SkillCooldownTime);
        }
    }

    public virtual float SkillAttackRange
    {
        get
        {
            return StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.SkillAttackRange);
        }
    }

    public virtual int SkillHealing
    {
        get
        {
            return int.Parse((StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.SkillHealing) + (SkillAttackPower * SkillHealingIncRatePerDamage * 0.01f)).ToString());
        }
    }

    public virtual float SkillHealingIncRatePerDamage
    {
        get
        {
            return StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.SkillHealingIncRatePerDamage);
        }
    }

    public virtual int SkillDPSCount
    {
        get
        {
            return (int)StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.SkillDPSCount);
        }
    }

    public virtual float SkillDPSSecond
    {
        get
        {
            return StatAbility.Stat.GetTotalStatValue(Stat.StatTypes.SkillDPSSecond);
        }
    }
}
