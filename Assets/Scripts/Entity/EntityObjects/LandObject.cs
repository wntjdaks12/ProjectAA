using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandObject : ActorObject
{
    public override void Init(Entity entity)
    {
        base.Init(entity);

        onClick.AddListener(() =>
        {
            ObjectDescContents.Instance.Init(entity.Id);
        });
    }
}
