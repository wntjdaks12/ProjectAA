using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Entity
{
    public List<List<int>> VFXIds { get; set; }
    public StrategyTypes StrategyType { get; set; }
    public enum StrategyTypes
    {
        Attack = 0, // ���� ���� ��ų
        Heal = 1, // ġ�� ���� ��ų
        AttackHeal = 2 // ���� ġ�� ���� ��ų
    }

    public Types Type { get; set; }
    public enum Types
    {
        Passive = 0, // �нú�
        Active = 1, // ��Ƽ��
        AttackPassive = 2 // ���� �нú� 
    }

    public List<int> TargetCounts { get; set; } // value = -1 : ��ü ���???
    public List<float> DamageDelays { get; set; }
    public List<int> RepeatCounts { get; set; } // value = -1 : ���� ���� ����� ī��Ʈ 
}
