using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBarAI : EntityAI
{
    protected override void Update()
    {
        base.Update();

        var headBarObject = EntityObject as HeadBarObject;

        if (headBarObject.Target != null)
        {
            headBarObject.Move();
        }
    }
}
