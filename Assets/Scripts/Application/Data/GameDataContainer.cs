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
    /// 데이터 테이블을 리턴합니다.
    /// </summary>
    /// <param name="dataTableName">데이터 테이블 이름</param>
    /// <returns></returns>
    public IDataTable ReturnDataTable(string dataTableName)
    {
        if (dataTables.ContainsKey(dataTableName))
            return dataTables[dataTableName];
        else
            return null;
    }

    /// <summary>
    /// 데이터의 존재 여부를 체크합니다.
    /// </summary>
    /// <param name="dataTable">데이터 테이블</param>
    /// <param name="id">데이터 아이디</param>
    /// <returns>존</returns>
    public bool ExitData(IDataTable dataTable, int id)
    {
        return dataTable.ExitData(id);
    }

    /// <summary>
    /// 데이터를 읽습니다.
    /// </summary>
    /// <typeparam name="T">데이터(T)</typeparam>
    /// <param name="dataTableName">데이터 테이블 이름</param>
    /// <param name="path">데이터 경로</param>
    public void LoadData<T>(string dataTableName, string path) where T : IData
    {
        var jsonTextAsset = Resources.Load<TextAsset>(path);

        if (jsonTextAsset == null) Debug.Log($"{path}경로가 잘못됐거나 존재하지 않는 파일입니다.");

        var jArray = GetJArray(jsonTextAsset.ToString());

        var instances = ParseJarrayTo<T>(jArray);

        foreach (var instance in instances)
        {
            AddData(dataTableName, instance.Id, instance);
        }
    }

    /// <summary>
    /// 데이터를 추가합니다.
    /// </summary>
    /// <param name="dataTableName">데이터 테이블 이름</param>
    public void AddTable(string dataTableName)
    {
        dataTables.Add(dataTableName, new DataTable(dataTableName, new Dictionary<int, IData>()));
    }

    /// <summary>
    /// 데이터를 추가합니다.
    /// </summary>
    /// <param name="dataTableName">데이터 테이블 이름</param>
    /// <param name="id">데이터 아이디</param>
    /// <param name="data">데이터</param>
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
    /// 데이터를 추가합니다.
    /// </summary>
    /// <param name="dataTableName">데이터 테이블 이름</param>
    /// <param name="data">데이터</param>
    public void AddData(string dataTableName, IData data)
    {
        if (!dataTables.ContainsKey(dataTableName))
            AddTable(dataTableName);

        var dataTable = ReturnDataTable(dataTableName);
        data.TableModel = dataTable;
        dataTables[dataTableName].AddData(data);

    }

    /// <summary>
    /// 데이터를 추가합니다.
    /// </summary>
    /// <param name="dataTableName">데이터 테이블 이름</param>
    /// <param name="id">데이터 아이디</param>
    /// <returns>데이터를 리턴합니다.</returns>
    public IData AddData(string dataTableName, int id)
    {
        var presetData = GameApplication.Instance.GameModel.PresetData;
        var runTimeData = GameApplication.Instance.GameModel.RunTimeData;

        var data = (IData)presetData.ReturnData<IData>(dataTableName, id).Clone();

        runTimeData.AddData(dataTableName, data);

        return data;
    }

    /// <summary>
    /// 데이터를 삭제합니다.
    /// </summary>
    /// <param name="tableName">데이터 테이블 일름</param>
    /// <param name="id">데이터 아이디</param>
    public void RemoveData(string tableName, int id)
    {
        dataTables[tableName].RemoveData(id);
    }

    /// <summary>
    /// 데이터를 리턴합니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataTable">데이터 테이블</param>
    /// <param name="id">데이터 아이디</param>
    /// <returns>데이터를 리턴합니다.</returns>
    private T ReturnData<T>(IDataTable dataTable, int id) where T : IData
    {
        if (!ExitData(dataTable, id))
        {
            Debug.Log($"{id}에 해당되는 데이터가 없습니다.");

            return default;
        }

        return (T)dataTable.GetData(id);
    }

    /// <summary>
    /// 데이터 테이블의 데이터를 리턴합니다. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dataTableName">데이터 테이블</param>
    /// <param name="id">데이터 아이디</param>
    /// <returns>데이터를 리턴합니다.</returns>
    public T ReturnData<T>(string dataTableName, int id) where T : IData
    {
        dataTables.TryGetValue(dataTableName, out IDataTable dataTable);

        if (dataTableName == null)
        {
            Debug.Log($"{dataTableName}에 해당되는 데이터 테이블이 없습니다.");

            return default;
        }
        return ReturnData<T>(dataTable, id);
    }

    public T[] ReturnDatas<T>(string tableName) where T : IData
    {
        dataTables.TryGetValue(tableName, out IDataTable tableModel);

        if (tableModel == null)
            Debug.LogError($"{tableName}에 해당하는 Table이없다");

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
