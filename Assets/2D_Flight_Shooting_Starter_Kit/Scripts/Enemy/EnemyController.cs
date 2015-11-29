using UnityEngine;
using System.Collections;

/// <summary>
/// EnemyController script.
/// Controls enemy's behavior.
/// hit handling / spawn item.
/// </summary>
public class EnemyController : MonoBehaviour
{
    public float hp = 100f;
    public string[] spawnItemPoolName;
    public float itemSpawnPercentage = 0.2f;

    private float _defaultHP;
    private EnemyMoveSpriteAnimation _enemySpriteAnim;
    private UIScoreManager _scoreManager;

    // tag strings.
    private readonly string _enemyString = "Enemy";
    private readonly string _enemyMissileString = "EnemyMissile";
    private readonly string _hpItemString = "HpItem";
    private readonly string _powerItemString = "PowerItem";
    private readonly string _bombItemString = "BombItem";
    private readonly string _boom2String = "Boom2";

    void Awake()
    {
        // Initialization.
        _defaultHP = hp;
        _enemySpriteAnim = GetComponentInChildren<EnemyMoveSpriteAnimation>();
        _scoreManager = GameObject.Find("Game Manager").GetComponent<UIScoreManager>();
    }

    void OnEnable()
    {
        hp = _defaultHP;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // when collided with tag name "Enemy" or "EnemyMissile" or "HpItem" or "PowerItem" or "BombItem", does nothing.
        if (other.CompareTag(_enemyString) || other.CompareTag(_enemyMissileString)
            || other.CompareTag(_hpItemString) || other.CompareTag(_powerItemString) || other.CompareTag(_bombItemString))
        {
            return;
        }

        // change sprite to damage sprite.
        _enemySpriteAnim.Damaged();

        // reduce hp.
        hp -= 60f;

        // when dead, player get score and given item according to it's percentage (default is 20%)
        if (hp <= 0f)
        {
            // player gets score.
            if (_scoreManager != null)
                _scoreManager.CountScore(100);

            // show Destroy Effect.
            ObjectPool.Instance.GetObject(_boom2String, transform);

            // Check whether item will be spawned or not.
            if (Random.Range(0f, 1f) < itemSpawnPercentage)
            {
                ObjectPool.Instance.GetObject(spawnItemPoolName[Random.Range(0, spawnItemPoolName.Length)], transform);
            }

            gameObject.SetActive(false);
        }
    }
}