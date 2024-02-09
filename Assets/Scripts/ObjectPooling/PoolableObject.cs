using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour, IData
{
    public PoolObject PoolObject { get; set; }
    public int Id { get; set; }
    public int InstanceId { get; set; }
    public IDataTable TableModel { get; set; }

    public event Action<IData> OnDataRemove;

    public object Clone()
    {
        return MemberwiseClone();
    }

    /// <summary>
    /// 풀링 오브젝트를 리턴합니다.
    /// </summary>
    public void ReturnPoolableObject()
    {
        gameObject.SetActive(false);
        PoolObject.ReturnPoolableObject(this);
    }
}
