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
        MaxHp = 0, // �ִ� ü��
        AttackDamage = 1, // ���� ���ݷ�
        AbilityPower = 2, // ���� ���ݷ�
        AttackSpeed = 3, // ���� �ӵ�
        CriticalDamage = 4, // ġ��Ÿ ����
        CriticalProbability = 5, // ġ��Ÿ ���� Ȯ��
        AttackRange = 6, // ���� ����
        ReduceArmor = 7, // ���� ����
        MagicResist = 8, // ���� ���׷�
        MaxMoveSpeed = 9, // �ִ� �̵� �ӵ�
        JumpPower = 10, // ������
        BasicAttackRange = 12, // �⺻ ���� ��Ÿ�
        SkillAttackDamage = 13, // ��ų ���� ���ݷ�  
        SkillAbilityPower = 14, // ��ų ���� ���ݷ�
        SkillCooldownTime = 15, // ��ų ��Ÿ��
        SkillAttackRange = 16, // ��ų ���� ����  
        SkillDPSCount = 17, // ��ų �ʴ� ����� Ƚ��
        SkillHealing = 18, // ��ų ȸ����
        SkillHealingIncRatePerDamage = 19, // ��ų �������� ȸ�� ������
        SkillHealRange = 20, // ��ų ȸ�� ����
        SkillDPSSecond = 21 // ��ų �ʴ� ����� ��
    }
    // 11�� 
    public float GetTotalStatValue(StatTypes statType)
    {
        var resStatValues = Stats.Where(x => x.StatType == statType)
                            .Select(x => x.Value).ToList();

        return resStatValues.Sum();
    }
}