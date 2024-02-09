using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject
{
    private Queue<PoolableObject> poolableObjects = new Queue<PoolableObject>();

    /// <summary>
    /// 풀링 오브젝트를 가져옵니다.
    /// </summary>
    /// <typeparam name="T">풀링 오브젝트(T)</typeparam>
    /// <param name="path">경로</param>
    /// <returns></returns>
    public T GetPoolableObject<T>(string path) where T : PoolableObject
    {
        if (poolableObjects.Count == 0)
        {
            CreatePoolableObject<T>(path);
        }

        return poolableObjects.Dequeue() as T;
    }

    /// <summary>
    /// 풀링 오브젝트를 리턴합니다.
    /// </summary>
    /// <param name="poolableObject">풀링 오브젝트</param>
    public void ReturnPoolableObject(PoolableObject poolableObject)
    {
        poolableObjects.Enqueue(poolableObject);
    }

    /// <summary>
    /// 풀링 오브젝트를 생성합니다.
    /// </summary>
    /// <typeparam name="T">풀링 오브젝트(T)</typeparam>
    /// <param name="path">경로</param>
    private void CreatePoolableObject<T>(string path) where T : PoolableObject
    {
        var poolableObject = Resources.Load<T>(path);

        if (poolableObject == null) Debug.LogError($"{path}가 잘못되었습니다.");

        var instance = Object.Instantiate(poolableObject);
        instance.PoolObject = this;

        poolableObjects.Enqueue(instance);
    }
}
