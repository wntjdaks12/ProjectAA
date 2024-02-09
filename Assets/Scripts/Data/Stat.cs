using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Stat : Data
{
    public StatTypes StatType { get; set; }
    public float Value { get; set; }

    public List<Stat> Stats { get; set; }

    public Stat(StatTypes statType, float value)
    {
        StatType = statType;
        Value = value;
    }

    public enum StatTypes
    {
        MaxHp = 0, // 최대 체력
        AttackDamage = 1, // 물리 공격력
        AbilityPower = 2, // 마법 공격력
        AttackSpeed = 3, // 공격 속도
        CriticalDamage = 4, // 치명타 피해
        CriticalProbability = 5, // 치명타 피해 확률
        AttackRange = 6, // 공격 범위
        ReduceArmor = 7, // 물리 방어력
        MagicResist = 8, // 마법 저항력
        MaxMoveSpeed = 9, // 최대 이동 속도
        JumpPower = 10, // 점프력
        BasicAttackRange = 12, // 기본 공격 사거리
        SkillAttackDamage = 13, // 스킬 물리 공격력  
        SkillAbilityPower = 14, // 스킬 마법 공격력
        SkillCooldownTime = 15, // 스킬 쿨타임
        SkillAttackRange = 16, // 스킬 공격 범위  
        SkillDPSCount = 17, // 스킬 초당 대미지 횟수
        SkillHealing = 18, // 스킬 회복량
        SkillHealingIncRatePerDamage = 19, // 스킬 데미지당 회복 증가량
        SkillHealRange = 20, // 스킬 회복 범위
        SkillDPSSecond = 21 // 스킬 초당 대미지 초
    }
    // 11번 
    public float GetTotalStatValue(StatTypes statType)
    {
        var resStatValues = Stats.Where(x => x.StatType == statType)
                            .Select(x => x.Value).ToList();

        return resStatValues.Sum();
    }
}