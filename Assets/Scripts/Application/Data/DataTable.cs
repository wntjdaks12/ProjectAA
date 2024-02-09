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
    /// 데이터의 존재 여부를 체크합니다.
    /// </summary>
    /// <param name="id">데이터 아이디</param>
    /// <returns></returns>
    public bool ExitData(int id)
    {
        DataContainer.TryGetValue(id, out IData data);

        if (data == null)
            return false;
        return true;
    }

    /// <summary>
    /// 데이터를 추가합니다.
    /// </summary>
    /// <param name="id">데이터 아이디</param>
    /// <param name="data">데이터</param>
    public void AddData(int id, IData data)
    {
        data.InstanceId = ++currentInstanceId;
        DataContainer.Add(id, data);
    }

    /// <summary>
    /// 데이터를 추가합니다.
    /// </summary>
    /// <param name="id">데이터 아이디</param>
    /// <param name="data">데이터</param>
    public void AddData(IData data)
    {
        data.InstanceId = ++currentInstanceId;
        DataContainer.Add(currentInstanceId, data);
    }

    /// <summary>
    /// 데이터를 삭제합니다.
    /// </summary>
    /// <param name="id">데이터 아이디</param>
    public void RemoveData(int id)
    {
        DataContainer.Remove(id);
    }

    /// <summary>
    /// 데이터를 리턴합니다.
    /// </summary>
    /// <param name="id">데이터 아이디</param>
    /// <returns>데이터를 리턴합니다.</returns>
    public IData GetData(int id)
    {
        return DataContainer[id];
    }

    /// <summary>
    /// 데이터들을 리턴합니다.
    /// </summary>
    /// <returns>데이터를 리턴합니다.</returns>
    public IData[] GetDatas()
    {
        return DataContainer.Values.Select(x => x).ToArray();
    }
}
