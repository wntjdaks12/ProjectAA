using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class TextExtension
{
    public static string StatDescription(this string text, string description, IStat stat)
    {
        description = description.Replace("?SkillAttackDamage", stat.StatAbility.SkillAttackDamage.ToString());
        description = description.Replace("?SkillAbilityPower", stat.StatAbility.SkillAbilityPower.ToString());
        description = description.Replace("?SkillHealingIncRatePerDamage", stat.StatAbility.SkillHealingIncRatePerDamage.ToString());
        description = description.Replace("?SkillHealing", stat.StatAbility.SkillHealing.ToString());
        description = description.Replace("?SkillDPSSecond", stat.StatAbility.SkillDPSSecond.ToString());
        description = description.Replace("?SkillDPSCount", stat.StatAbility.SkillDPSCount.ToString());
        description = description.Replace("?SkillAttackDamageRate", stat.StatAbility.SkillAttackDamageRate.ToString());
        description = description.Replace("?SkillAbilityPowerRate", stat.StatAbility.SkillAbilityPowerRate.ToString());
        return description;
    }
}
