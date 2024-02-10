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
}
