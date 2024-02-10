using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubItem : Data, IStat
{
    public StatAbility StatAbility { get; set ; }

    public void Init()
    {
        var stat = GameApplication.Instance.GameModel.PresetData.ReturnData<Stat>(nameof(Stat), Id);
        StatAbility = new StatAbility(stat);
    }
}
