using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillStrategy
{
    public void OnUseAsync(ActorObject[] targets, IStat[] stats);
}
