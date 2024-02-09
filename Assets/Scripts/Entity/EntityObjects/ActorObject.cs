using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorObject : EntityObject
{
    [field: SerializeField] public Transform HitNode { get; set; }
}
