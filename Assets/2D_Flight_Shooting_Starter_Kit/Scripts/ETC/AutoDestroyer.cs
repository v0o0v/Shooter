using UnityEngine;
using System.Collections;

public class AutoDestroyer : MonoBehaviour
{
    public string poolName;
    public float lifeTime = 2f;

    void OnEnable()
    {
        Invoke("ReturnObject", lifeTime);
    }

    void OnDisable()
    {
        CancelInvoke("ReturnObject");
    }

    void ReturnObject()
    {
        ObjectPool.Instance.ReturnObject(poolName, transform);
    }
}