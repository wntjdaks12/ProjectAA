using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IDataTable
{
    public string TableName { get; set; }
    public bool ExitData(int id);
    public void AddData(int id, IData data);
    public void AddData(IData data);
    public void RemoveData(int id);
    public IData GetData(int id);
    public IData[] GetDatas();
}

public class DataTable : IDataTable
{
    private int currentInstanceId = 0;

    public string TableName { get; set; }

    Dictionary<int, IData> DataContainer { get; set; }

    public DataTable(string tableName, Dictionary<int, IData> dataContainer)
    {
        TableName = tableName;
        DataContainer = dataContainer;
    }

    /// <summary>
    /// �������� ���� ���θ� üũ�մϴ�.
    /// </summary>
    /// <param name="id">������ ���̵�</param>
    /// <returns></returns>
    public bool ExitData(int id)
    {
        DataContainer.TryGetValue(id, out IData data);

        if (data == null)
            return false;
        return true;
    }

    /// <summary>
    /// �����͸� �߰��մϴ�.
    /// </summary>
    /// <param name="id">������ ���̵�</param>
    /// <param name="data">������</param>
    public void AddData(int id, IData data)
    {
        data.InstanceId = ++currentInstanceId;
        DataContainer.Add(id, data);
    }

    /// <summary>
    /// �����͸� �߰��մϴ�.
    /// </summary>
    /// <param name="id">������ ���̵�</param>
    /// <param name="data">������</param>
    public void AddData(IData data)
    {
        data.InstanceId = ++currentInstanceId;
        DataContainer.Add(currentInstanceId, data);
    }

    /// <summary>
    /// �����͸� �����մϴ�.
    /// </summary>
    /// <param name="id">������ ���̵�</param>
    public void RemoveData(int id)
    {
        DataContainer.Remove(id);
    }

    /// <summary>
    /// �����͸� �����մϴ�.
    /// </summary>
    /// <param name="id">������ ���̵�</param>
    /// <returns>�����͸� �����մϴ�.</returns>
    public IData GetData(int id)
    {
        return DataContainer[id];
    }

    /// <summary>
    /// �����͵��� �����մϴ�.
    /// </summary>
    /// <returns>�����͸� �����մϴ�.</returns>
    public IData[] GetDatas()
    {
        return DataContainer.Values.Select(x => x).ToArray();
    }
}
