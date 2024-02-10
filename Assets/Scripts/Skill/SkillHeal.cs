using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillHeal : ISkillStrategy
{
    public void OnUseAsync(ActorObject[] targets, IStat[] stats)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] is CharacterObject)
            {
                var characterObj = targets[i] as CharacterObject;
                characterObj.OnHeal(stats.Sum(x => x.StatAbility.SkillHealing));

                var pos = Camera.main.WorldToScreenPoint(characterObj.HitNode.position);
                pos.x += Random.Range(-10f, 10f);
                pos.y += Random.Range(-10f, 10f);

                GameApplication.Instance.EntityController.Spawn<DamageFont, DamageFontObject>(80002, pos, Quaternion.identity, GameObject.Find("DynamicOverlayCanvas").transform, stats.Sum(x => x.StatAbility.SkillHealing));
            }
        }
    }
}
