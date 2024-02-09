using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Entity
{
    public enum HandednessTypes
    { 
        left = 0,
        Right = 1
    }
    public HandednessTypes HandednessType { get; set; }


}
