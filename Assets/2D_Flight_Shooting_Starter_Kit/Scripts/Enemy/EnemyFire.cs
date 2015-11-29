using UnityEngine;
using System.Collections;

/// <summary>
/// EnemyFire script.
/// shoot enemy missile.
/// </summary>
public class EnemyFire : MonoBehaviour
{
    public AudioClip sound;
    public Transform[] firePos;
    public float repeat = 0.8f;

    public string missilePoolName;
    public string shootingFirePoolName;

    void OnEnable()
    {
        InvokeRepeating("Shoot", 0f, repeat);
    }

    void OnDisable()
    {
        CancelInvoke("Shoot");
    }

    void Shoot()
    {
        for (int ix = 0; ix < firePos.Length; ++ix)
        {
            ObjectPool.Instance.GetObject(missilePoolName, firePos[ix]);
            ObjectPool.Instance.GetObject(shootingFirePoolName, firePos[ix]);
        }

        AudioSource.PlayClipAtPoint(sound, Vector3.zero);
    }
}