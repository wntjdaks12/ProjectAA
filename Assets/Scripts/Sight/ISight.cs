using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISight
{
    public string[] VisableLayerNames { get; set; }
    public float VisableRange { get; set; }
    public Collider[] GetVisableObjects(Transform transform);
    public Collider[] GetOrderByVisableObjects(Transform transform);
}
