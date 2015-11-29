using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpawnInfomation
{
    public string spawnEnemyPoolName;
    public float spawnTime = 4f;
    public int spawnPos = 0;

    public bool isBoss = false;
}

/// <summary>
/// EnemySpawnManager script.
/// Spawns enemies with enemy wave information array.
/// </summary>
public class EnemySpawnManager : MonoBehaviour
{
    public Transform[] enemySpawnPos;
    public SpawnInfomation[] enemySpawnInfos;

    private int _currentSceneario;

    void Start()
    {
        _currentSceneario = 0;
        Invoke("SpawnEnemy", enemySpawnInfos[0].spawnTime);
    }

    void SpawnEnemy()
    {
        Transform t = ObjectPool.Instance.GetObject(
            enemySpawnInfos[_currentSceneario].spawnEnemyPoolName, 
            enemySpawnPos[enemySpawnInfos[_currentSceneario].spawnPos]
        );

        t.gameObject.SetActive(true);

        if (!enemySpawnInfos[_currentSceneario].isBoss)
        {
            StartNextState();
        }
    }

    public void StartNextState()
    {
        _currentSceneario++;
        if (_currentSceneario == enemySpawnInfos.Length)
            _currentSceneario = 0;

        Invoke("SpawnEnemy", enemySpawnInfos[_currentSceneario].spawnTime);
    }
}