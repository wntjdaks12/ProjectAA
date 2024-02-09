using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : Data, IStat
{
    public Transform Transform { get; private set; }

    public StatAbility StatAbility { get; set; }

    public float Lifetime { get; set; }

    public virtual void Init(Transform transform = null)
    {
        Transform = transform;

        var stat = GameApplication.Instance.GameModel.PresetData.ReturnData<Stat>(nameof(Stat), Id);
        StatAbility = new StatAbility(stat);
    }

    public IEnumerator StartLifeTime()
    {
        yield return new WaitForSeconds(Lifetime);

        OnRemoveData();
    }
}
