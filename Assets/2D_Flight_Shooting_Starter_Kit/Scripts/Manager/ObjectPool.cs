using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PoolObject
{
    public string name;
    public int poolCount;
    public GameObject poolPrefab;

    public List<Transform> activeList = new List<Transform>();
    public List<Transform> inactiveList = new List<Transform>();
}

/// <summary>
/// ObjectPool script.
/// Manages pool objects.
/// </summary>
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool _instance;
    public static ObjectPool Instance { get { return _instance; } }

    public Transform activeListTransform;
    public Transform inactiveListTransform;

    public PoolObject[] poolObjects;

    public bool isTutorial = false;

    void Awake()
    {
        for (int ix = 0; ix < poolObjects.Length; ++ix)
        {
            for (int jx = 0; jx < poolObjects[ix].poolCount; ++jx)
            {
                AddPoolObject(poolObjects[ix], jx);
            }
        }
    }

    void OnEnable()
    {
        if (_instance == null)
            _instance = this;
    }

    void AddPoolObject(PoolObject po, int count)
    {
        if (po == null)
            Debug.Log("po null");
        else if (po.poolPrefab == null)
            Debug.Log("po.poolPrefab null, " + po.name);
        Transform t = ((GameObject)Instantiate(po.poolPrefab)).transform;
        t.SetParent(inactiveListTransform);
        t.name = po.name + count;
        po.inactiveList.Add(t);
        DeactivateObject(t);
    }

    void DeactivateObject(Transform t)
    {
        t.SetParent(inactiveListTransform);
    }

    void ActivateObject(Transform t)
    {
        t.SetParent(activeListTransform);
    }

    public Transform GetObject(string objName, Transform target)
    {
        for (int ix = 0; ix < poolObjects.Length; ++ix)
        {
            if (poolObjects[ix].name.Equals(objName))
            {
                if (poolObjects[ix].inactiveList.Count == 0)
                {
                    AddPoolObject(poolObjects[ix], poolObjects[ix].inactiveList.Count + 1);
                }

                Transform t = poolObjects[ix].inactiveList[0];
                poolObjects[ix].inactiveList.Remove(t);
                poolObjects[ix].activeList.Add(t);
                ActivateObject(t);
                t.position = target.position;
                t.rotation = target.rotation;

                return t;
            }
        }

        return null;
    }

    public void ReturnObject(string poolName, Transform t)
    {
        for (int ix = 0; ix < poolObjects.Length; ++ix)
        {
            if (poolObjects[ix].name.Equals(poolName))
            {
                if (!poolObjects[ix].activeList.Contains(t) && !isTutorial)
                {
                    Debug.LogError("No such game object in activeList : " + t.name);
                    return;
                }

                poolObjects[ix].activeList.Remove(t);
                poolObjects[ix].inactiveList.Add(t);
                DeactivateObject(t);
            }
        }
    }
}