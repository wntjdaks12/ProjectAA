using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStat
{
    public StatAbility StatAbility { get; set; }
}

public class StatAbility
{
    public Stat Stat { get; private set; }

    public StatAbility(Stat _stat)
    {
        Stat = _stat;
    }
}
