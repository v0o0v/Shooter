using UnityEngine;
using System.Collections;

/// <summary>
/// BossController script.
/// Boss Enemy Controller.
/// Hit handling.
/// </summary>
public class BossController : MonoBehaviour
{
    public float hp = 100f;
    public string bossPoolName;
    public Sprite normalSprite;
    public Sprite damagedSprite;

    private float _defaultHP;
    private SpriteRenderer _sprite;
    private EnemySpawnManager _spawnManager;
    private UIScoreManager _scoreManager;

    private readonly string _enemyString = "Enemy";
    private readonly string _enemyMissileString = "EnemyMissile";
    private readonly string _boomBossString = "BoomBoss";
    private readonly string _gameManagerString = "Game Manager";

    void Awake()
    {
        // Initialization.
        _spawnManager = GameObject.Find(_gameManagerString).GetComponent<EnemySpawnManager>();
        _scoreManager = GameObject.Find(_gameManagerString).GetComponent<UIScoreManager>();
        _sprite = GetComponent<SpriteRenderer>();
        _defaultHP = hp;
    }

    void OnEnable()
    {
        hp = _defaultHP;
    }

    void SetNormalSprite()
    {
        _sprite.sprite = normalSprite;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // When collided with tag name "Enemy" or "EnemyMissile", does nothing.
        if (other.CompareTag(_enemyString) || other.CompareTag(_enemyMissileString))
            return;

        // Change sprite to damage sprite.
        if (damagedSprite != null)
        {
            _sprite.sprite = damagedSprite;
            Invoke("SetNormalSprite", 0.05f);
        }

        // reduce hp.
        hp -= 30f;

        // when boss dead, start next enemy wave and show destroy effect.
        if (hp <= 0f)
        {
            // Player Gets Score.
            _scoreManager.CountScore(300);

            // start next enemy wave.
            _spawnManager.StartNextState();

            // show destroy effect.
            ObjectPool.Instance.GetObject(_boomBossString, transform);

            // return boss object to pool object manager.
            ObjectPool.Instance.ReturnObject(bossPoolName, transform.parent);
        }
    }
}