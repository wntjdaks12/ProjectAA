using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodTransfusionObject : VFXObject
{
    public void Init()
    { 
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Caster.HitNode.position, Time.deltaTime * 10);
    }
}
