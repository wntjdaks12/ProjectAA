using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class TextExtension
{
    public static string SkillDescription(this string text, string description, Skill skill)
    {
        description = description.Replace("?SkillAttackDamage", skill.StatAbility.SkillAttackDamage.ToString());
        description = description.Replace("?SkillAbilityPower", skill.StatAbility.SkillAbilityPower.ToString());
        description = description.Replace("?SkillHealingIncRatePerDamage", skill.StatAbility.SkillHealingIncRatePerDamage.ToString());
        description = description.Replace("?SkillHealing", skill.StatAbility.SkillHealing.ToString());
        description = description.Replace("?SkillDPSSecond", skill.StatAbility.SkillDPSSecond.ToString());
        description = description.Replace("?SkillDPSCount", skill.StatAbility.SkillDPSCount.ToString());
        return description;
    }
}
