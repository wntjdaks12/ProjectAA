using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTool
{
    public ISkillStrategy[] Strategys { get; private set; }

    public Skill Skill { get; private set; }

    public bool IsCooldownTime { get; set; }

    public SkillTool(Skill skill)
    {
        Skill = skill;

        switch (Skill.StrategyType)
        {
            case Skill.StrategyTypes.Attack: setStrategy(new ISkillStrategy[1] { new SkillAttack() }); break;
            case Skill.StrategyTypes.Heal: setStrategy(new ISkillStrategy[1] { new SkillHeal() }); break;
            case Skill.StrategyTypes.AttackHeal: setStrategy(new ISkillStrategy[2] { new SkillAttack(), new SkillHeal() }); break;
        }
    }

    public void setStrategy(ISkillStrategy[] strategys)
    {
        Strategys = strategys;
    }
}
