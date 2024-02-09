using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectContainer
{
    private static PoolObjectContainer instance;
    public static PoolObjectContainer Instance { get => instance == null ? instance = new PoolObjectContainer() : instance; }

    private Dictionary<string, PoolObject> poolObjects = new Dictionary<string, PoolObject>();

    /// <summary>
    /// 풀링 오브젝트를 생성합니다.
    /// </summary>
    /// <typeparam name="T">풀링 오브젝트(T)</typeparam>
    /// <param name="path">경로</param>
    /// <returns></returns>
    public static T CreatePoolObject<T>(string path) where T : PoolableObject
    {
        if (!Instance.poolObjects.ContainsKey(path))
            Instance.poolObjects.Add(path, new PoolObject());

        Instance.poolObjects.TryGetValue(path, out PoolObject poolObject);

        return poolObject.GetPoolableObject<T>(path);
    }
}
