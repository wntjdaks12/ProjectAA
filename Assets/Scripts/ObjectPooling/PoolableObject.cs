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
    /// Ǯ�� ������Ʈ�� �����մϴ�.
    /// </summary>
    public void ReturnPoolableObject()
    {
        gameObject.SetActive(false);
        PoolObject.ReturnPoolableObject(this);
    }
}
