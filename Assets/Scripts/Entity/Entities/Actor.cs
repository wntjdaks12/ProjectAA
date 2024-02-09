using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Actor : Entity, ISight
{
    public string[] VisableLayerNames { get; set; }
    public float VisableRange { get; set; }

    public Collider[] GetVisableObjects(Transform transform)
    {
        return Physics.OverlapSphere(transform.position, VisableRange, LayerMask.GetMask(VisableLayerNames));
    }

    public Collider[] GetOrderByVisableObjects(Transform transform)
    {
        var visableObjs = Physics.OverlapSphere(transform.position, VisableRange, LayerMask.GetMask(VisableLayerNames));
        return visableObjs.OrderBy(x => (transform.position - x.transform.position).sqrMagnitude).ToArray();
    }
}
