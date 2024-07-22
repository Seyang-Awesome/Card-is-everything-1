using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PoolManager : Singleton<PoolManager>
{
    //对象池根节点
    private GameObject poolRootGameObject;
    private GameObject PoolRootGameObject
    {
        get 
        { 
            if(poolRootGameObject == null)
            {
                poolRootGameObject = new GameObject();
                poolRootGameObject.name = "PoolRoot";
            }
            return poolRootGameObject;
        }
    }

    /// <summary>
    /// GameObject池
    /// </summary>
    public Dictionary<string, GameObjectPool> gameObjectPool = new Dictionary<string, GameObjectPool>();

    /// <summary>
    /// Object池
    /// </summary>
    public Dictionary<string, ObjectPool> objectPool = new Dictionary<string, ObjectPool>();

    /// <summary>
    /// 通过给定的预制体得到一个物体
    /// 1.如果对象池有，就拿一个出来
    /// 2.若果对象池没有，就Instantiate一个
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject GetGameObject(GameObject prefab, Transform parent = null)// where T : UnityEngine.Object
    {
        GameObject gameObject = null;

        if (CheckCache(prefab))
            gameObject = gameObjectPool[prefab.name].GetGameObject(parent);
        else
        {
            gameObject = Instantiate(prefab,parent);
            gameObject.name = prefab.name;
        }
        
        return gameObject;
    }

    public GameObject GetGameObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject gameObject = GetGameObject(prefab, parent);
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        return gameObject;
    }

    public T GetGameObject<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : MonoBehaviour
    {
        return GetGameObject(prefab.gameObject, position, rotation, parent).GetComponent<T>();
    }

    public T GetGameObject<T>(GameObject prefab, Transform parent = null) where T : UnityEngine.Object
    {
        GameObject gameObject = GetGameObject(prefab, parent);
        if (gameObject != null)
            return gameObject.GetComponent<T>();
        return null;
    }

    public T GetGameObject<T>(T prefab, Transform parent = null) where T : Component
    {
        return GetGameObject(prefab.gameObject, parent).GetComponent<T>();
    }
    /// <summary>
    /// 放入对象池
    /// </summary>
    /// <param name="prefab"></param>
    public void PushGameObject(GameObject prefab) 
    {
        if(gameObjectPool.ContainsKey(prefab.name))
        {
            gameObjectPool[prefab.name].PushGameObject(prefab);
        }
        else
        {
            gameObjectPool.Add(prefab.name, new GameObjectPool(prefab,PoolRootGameObject));
        }
    }
    public T GetObject<T>() where T:class, new()
    {
        T forReturn=null;
        string fullName = typeof(T).FullName;
        if (objectPool.ContainsKey(fullName) && objectPool[fullName].objectQueue.Count > 0)
        {
            forReturn = (T)objectPool[fullName].GetObject();
        }
        else
        {
            forReturn = new T();
        }
        return forReturn;
    }
    public void PushObject(object obj)
    {
        string fullName = obj.GetType().FullName;
        if (objectPool.ContainsKey(fullName))
        {
            objectPool[fullName].PushObject(obj); 
        }
        else
        {
            objectPool.Add(fullName, new ObjectPool(obj));
        }
    }
    public bool CheckCache(string prefabName)
    {
        return gameObjectPool.ContainsKey(prefabName) && gameObjectPool[prefabName].gameObjectQueue.Count > 0;
    }
    public bool CheckCache(GameObject prefab)
    {
        return CheckCache(prefab.name);
    }
    public void ClearGameObjectPool()
    {
        gameObjectPool.Clear();
    }
    public void ClearObjectPool()
    {
        objectPool.Clear();
    }
}

