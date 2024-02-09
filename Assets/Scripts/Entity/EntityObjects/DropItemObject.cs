using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DropItemObject : ActorObject
{
    [SerializeField] private MeshRenderer meshRenderer;

    private DropItem dropItem;

    private Sequence sequence;

    public override void Init(Entity entity)
    {
        base.Init(entity);

        dropItem = Entity as DropItem;

        dropItem.GetOrderByVisableObjects(transform);

        Collider targetObj =  null;

        sequence = DOTween.Sequence().OnStart(() => targetObj = null)
            .Append(transform.DOMove(transform.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f)), 0.5f).SetEase(Ease.OutExpo))
            .AppendInterval(1f)
            .OnUpdate(() =>
            {
                var colliders = dropItem.GetOrderByVisableObjects(transform);
                if (colliders.Length > 0)
                {
                    targetObj = colliders[0];
                }

                if (targetObj == null) return;

                if ((transform.position - targetObj.transform.position).sqrMagnitude > 1f)
                {
                    transform.position = Vector3.Lerp(transform.position, targetObj.transform.position, Time.deltaTime * 20f);
                }
                else
                {
                    var dropItemId = (int)dropItem.Value;

                    if (1 <= dropItemId && dropItemId <= 50)
                    {
                        GameApplication.Instance.PlayerManager.SetGoodsInfo(new DBGoodsInfo() { id = dropItemId, value = 1 });
                    }
                    else
                    {
                        GameApplication.Instance.PlayerManager.SetItemInfo(new DBItemInfo() { id = dropItemId, value = 1 });
                    }

                    dropItem.OnRemoveData();

                    sequence.Kill();
                }
            });

        var iconInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<IconInfo>(nameof(IconInfo), (int)dropItem.Value);
        meshRenderer.material.SetTexture("_MainTex", Resources.Load<Texture>(iconInfo.Path));
    }
}
