using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamageFontObject : EntityObject
{
    [SerializeField] private TextMeshProUGUI damageText;

    private Sequence sequence;

    public void Awake()
    {
        sequence = DOTween.Sequence().SetAutoKill(false).OnStart(() =>
        {
        }).Append(transform.DOScale(0, 0.5f));
    }   

    public override void Init(Entity entity)
    {
        base.Init(entity);

        var damageFont = entity as DamageFont;
        damageText.text = damageFont.Value.ToString();

        sequence.Restart();
    }
}
