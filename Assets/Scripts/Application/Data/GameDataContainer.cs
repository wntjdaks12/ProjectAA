using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

public class GameDataContainer
{
    private Dictionary<string, IDataTable> dataTables = new Dictionary<string, IDataTable>();

    /// <summary>
    /// ������ ���̺��� �����մϴ�.
    /// </summary>
    /// <param name="dataTableName">������ ���̺� �̸�</param>
    /// <returns></returns>
    public IDataTable ReturnDataTable(string dataTableName)
    {
        if (dataTables.ContainsKey(dataTableName))
            return dataTables[dataTableName];
        else
            return null;
    }

    /// <summary>
    /// �������� ���� ���θ� üũ�մϴ�.
    /// </summary>
    /// <param name="dataTable">������ ���̺�</param>
    /// <param name="id">������ ���̵�</param>
    /// <returns>��</returns>
    public bool ExitData(IDataTable dataTable, int id)
    {
        return dataTable.ExitData(id);
    }

    /// <summary>
    /// �����͸� �н��ϴ�.
    /// </summary>
    /// <typeparam name="T">������(T)</typeparam>
    /// <param name="dataTableName">������ ���̺� �̸�</param>
    /// <param name="path">������ ���</param>
    public void LoadData<T>(string dataTableName, string path) where T : IData
    {
        var jsonTextAsset = Resources.Load<TextAsset>(path);

        if (jsonTextAsset == null) Debug.Log($"{path}��ΰ� �߸��ưų� �������� �ʴ� �����Դϴ�.");

        var jArray = GetJArray(jsonTextAsset.ToString());

        var instances = ParseJarrayTo<T>(jArray);

        foreach (var instance in instances)
        {
            AddData(dataTableName, instance.Id, instance);
        }
    }

    /// <summary>
    /// �����͸� �߰��մϴ�.
    /// </summary>
    /// <param name="dataTableName">������ ���̺� �̸�</param>
    public void AddTable(string dataTableName)
    {
        dataTables.Add(dataTableName, new DataTable(dataTableName, new Dictionary<int, IData>()));
    }

    /// <summary>
    /// �����͸� �߰��մϴ�.
    /// </summary>
    /// <param name="dataTableName">������ ���̺� �̸�</param>
    /// <param name="id">������ ���̵�</param>
    /// <param name="data">������</param>
    public void AddData(string dataTableName, int id, IData data)
    {
        if (!dataTables.ContainsKey(dataTableName))
            AddTable(dataTableName);

        var dataTable = ReturnDataTable(dataTableName);
        data.Id = id;
        data.TableModel = dataTable;
        dataTables[dataTableName].AddData(id, data);
    }

    /// <summary>
    /// �����͸� �߰��մϴ�.
    /// </summary>
    /// <param name="dataTableName">������ ���̺� �̸�</param>
    /// <param name="data">������</param>
    public void AddData(string dataTableName, IData data)
    {
        if (!dataTables.ContainsKey(dataTableName))
            AddTable(dataTableName);

        var dataTable = ReturnDataTable(dataTableName);
        data.TableModel = dataTable;
        dataTables[dataTableName].AddData(data);

    }

    /// <summary>
    /// �����͸� �߰��մϴ�.
    /// </summary>
    /// <param name="dataTableName">������ ���̺� �̸�</param>
    /// <param name="id">������ ���̵�</param>
    /// <returns>�����͸� �����մϴ�.</returns>
    public IData AddData(string dataTableName, int id)
    {
        var presetData = GameApplication.Instance.GameModel.PresetData;
        var runTimeData = GameApplication.Instance.GameModel.RunTimeData;

        var data = (IData)presetData.ReturnData<IData>(dataTableName, id).Clone();

        runTimeData.AddData(dataTableName, data);

        return data;
    }

    /// <summary>
    /// �����͸� �����մϴ�.
    /// </summary>
    /// <param name="tableName">������ ���̺� �ϸ�</param>
    /// <param name="id">������ ���̵�</param>
    public void RemoveData(string tableName, int id)
    {
        dataTables[tableName].RemoveData(id);
    }

    /// <summary>
    /// �����͸� �����մϴ�.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataTable">������ ���̺�</param>
    /// <param name="id">������ ���̵�</param>
    /// <returns>�����͸� �����մϴ�.</returns>
    private T ReturnData<T>(IDataTable dataTable, int id) where T : IData
    {
        if (!ExitData(dataTable, id))
        {
            Debug.Log($"{id}�� �ش�Ǵ� �����Ͱ� �����ϴ�.");

            return default;
        }

        return (T)dataTable.GetData(id);
    }

    /// <summary>
    /// ������ ���̺��� �����͸� �����մϴ�. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataTableName">������ ���̺�</param>
    /// <param name="id">������ ���̵�</param>
    /// <returns>�����͸� �����մϴ�.</returns>
    public T ReturnData<T>(string dataTableName, int id) where T : IData
    {
        dataTables.TryGetValue(dataTableName, out IDataTable dataTable);

        if (dataTableName == null)
        {
            Debug.Log($"{dataTableName}�� �ش�Ǵ� ������ ���̺��� �����ϴ�.");

            return default;
        }
        return ReturnData<T>(dataTable, id);
    }

    public T[] ReturnDatas<T>(string tableName) where T : IData
    {
        dataTables.TryGetValue(tableName, out IDataTable tableModel);

        if (tableModel == null)
            Debug.LogError($"{tableName}�� �ش��ϴ� Table�̾���");

        return tableModel.GetDatas().Select(x => (T)x).ToArray();
    }

    private JArray GetJArray(string jsontText)
    {
        return JArray.Parse(jsontText);
    }

    private T[] ParseJarrayTo<T>(JArray jArray)
    {
        return jArray.Select(x => JsonConvert.DeserializeObject<T>(x.ToString())).ToArray();
    }
}
