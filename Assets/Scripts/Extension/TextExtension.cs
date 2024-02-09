using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class TextExtension
{
    public static string SkillDescription(this string text, string description, Skill skill)
    {
        description = description.Replace("?SkillAttackDamage", skill.SkillAttackDamage.ToString());
        description = description.Replace("?SkillAbilityPower", skill.SkillAbilityPower.ToString());
        description = description.Replace("?SkillHealingIncRatePerDamage", skill.SkillHealingIncRatePerDamage.ToString());
        description = description.Replace("?SkillHealing", skill.SkillHealing.ToString());
        description = description.Replace("?SkillDPSSecond", skill.SkillDPSSecond.ToString());
        description = description.Replace("?SkillDPSCount", skill.SkillDPSCount.ToString());
        return description;
    }
}
