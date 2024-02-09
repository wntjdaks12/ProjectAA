using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadBarObject : EntityObject
{
    [SerializeField] private Image currentHpImg;

    private IHeadBarSubject target;
    public IHeadBarSubject Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;

            if (value is CharacterObject)
            {
                var characterObj = value as CharacterObject;
                var character = characterObj.Entity as Character;
                UpdateDynamicUi(character.CurrentHp, character.MaxHp);
  
                character.HitEvent += (x) => UpdateDynamicUi(x.CurrentHp, x.MaxHp);

                UpdateStaticUI();
            }
        }
    }

    public override void Init(Entity entity)
    {
        base.Init(entity);
    }

    public void UpdateStaticUI()
    {
        if (target is MonsterObject)
            currentHpImg.color = new Color(1, 0, 0, 1);
        else if (target is HeroObject)
            currentHpImg.color = new Color(0, 1, 111 / 255f, 1);
    }

    public void UpdateDynamicUi(int currentValue, int maxValue)
    {
        currentHpImg.fillAmount = currentValue / (float)maxValue;
    }

    public void Move()
    {
        var targetScrPos = Camera.main.WorldToScreenPoint(Target.HeadBarNode.position);
        transform.position = targetScrPos;
    }
}
