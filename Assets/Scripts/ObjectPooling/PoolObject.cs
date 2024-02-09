using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject
{
    private Queue<PoolableObject> poolableObjects = new Queue<PoolableObject>();

    /// <summary>
    /// Ǯ�� ������Ʈ�� �����ɴϴ�.
    /// </summary>
    /// <typeparam name="T">Ǯ�� ������Ʈ(T)</typeparam>
    /// <param name="path">���</param>
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
    /// Ǯ�� ������Ʈ�� �����մϴ�.
    /// </summary>
    /// <param name="poolableObject">Ǯ�� ������Ʈ</param>
    public void ReturnPoolableObject(PoolableObject poolableObject)
    {
        poolableObjects.Enqueue(poolableObject);
    }

    /// <summary>
    /// Ǯ�� ������Ʈ�� �����մϴ�.
    /// </summary>
    /// <typeparam name="T">Ǯ�� ������Ʈ(T)</typeparam>
    /// <param name="path">���</param>
    private void CreatePoolableObject<T>(string path) where T : PoolableObject
    {
        var poolableObject = Resources.Load<T>(path);

        if (poolableObject == null) Debug.LogError($"{path}�� �߸��Ǿ����ϴ�.");

        var instance = Object.Instantiate(poolableObject);
        instance.PoolObject = this;

        poolableObjects.Enqueue(instance);
    }
}
