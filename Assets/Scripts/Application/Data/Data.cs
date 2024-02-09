using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : IData
{
    public event Action<IData> OnDataRemove;

    public int Id { get; set; }
    public int InstanceId { get; set; }

    public IDataTable TableModel { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public void OnRemoveData()
    {
        OnDataRemove?.Invoke(this);
    }
}
