using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : ISkillStrategy
{
    public void OnUseAsync(ActorObject[] targets, SkillTool skillTool)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] is CharacterObject)
            {
                var characterObj = targets[i] as CharacterObject;

                characterObj.OnHit(skillTool.Skill.SkillAttackPower);

                var pos = Camera.main.WorldToScreenPoint(characterObj.HitNode.position);

                if (skillTool.Skill.SkillAttackDamage != 0) GameApplication.Instance.EntityController.Spawn<DamageFont, DamageFontObject>(80001, pos, Quaternion.identity, GameObject.Find("DynamicOverlayCanvas").transform, skillTool.Skill.SkillAttackDamage);
                if (skillTool.Skill.SkillAbilityPower != 0) GameApplication.Instance.EntityController.Spawn<DamageFont, DamageFontObject>(80003, pos, Quaternion.identity, GameObject.Find("DynamicOverlayCanvas").transform, skillTool.Skill.SkillAbilityPower);
            }
        }
    }
}
