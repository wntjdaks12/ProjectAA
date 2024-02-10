using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillAttack : ISkillStrategy
{
    public void OnUseAsync(ActorObject[] targets, IStat[] stats)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] is CharacterObject)
            {
                var characterObj = targets[i] as CharacterObject;

                characterObj.OnHit(stats.Sum(x => x.StatAbility.SkillAttackPower));

                var pos = Camera.main.WorldToScreenPoint(characterObj.HitNode.position);

                var skillAttackDamage = stats.Sum(x => x.StatAbility.SkillAttackDamage);
                var skillAbilityPower = stats.Sum(x => x.StatAbility.SkillAbilityPower);
                if (skillAttackDamage != 0) GameApplication.Instance.EntityController.Spawn<DamageFont, DamageFontObject>(80001, pos, Quaternion.identity, GameObject.Find("DynamicOverlayCanvas").transform, skillAttackDamage);
                if (skillAbilityPower != 0) GameApplication.Instance.EntityController.Spawn<DamageFont, DamageFontObject>(80003, pos, Quaternion.identity, GameObject.Find("DynamicOverlayCanvas").transform, skillAbilityPower);
            }
        }
    }
}
