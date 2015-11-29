using UnityEngine;
using System.Collections;

/// <summary>
/// EnemyMoveSpriteAnimation script.
/// </summary>
public class EnemyMoveSpriteAnimation : MonoBehaviour
{
    public Sprite[] normalSprites;
    public Sprite[] damageSprites;

    private SpriteRenderer _spriteRenderer;
    private Vector3 _postPosition;
    private int _currentSpriteIndex;
    private bool _isDamaged;
    private float _moveOffset = 0.01f;

    void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void OnEnable()
    {
        _currentSpriteIndex = 0;
        _postPosition = transform.position;
        _isDamaged = false;
    }

    void Update()
    {
        // Change sprite by enemy move.
        if ((_postPosition.x - transform.position.x) > _moveOffset)
            _currentSpriteIndex = Mathf.Clamp(_currentSpriteIndex - 1, 0, 4);

        else if (_postPosition.x - transform.position.x < -_moveOffset)
            _currentSpriteIndex = Mathf.Clamp(_currentSpriteIndex + 1, 0, 4);

        else
            _currentSpriteIndex = 2;

        // when enemy get damaged, change sprite to damaged sprite.
        if (_isDamaged)
            _spriteRenderer.sprite = damageSprites[_currentSpriteIndex];
        else
            _spriteRenderer.sprite = normalSprites[_currentSpriteIndex];

        _postPosition = transform.position;
    }

    // change sprite to damaged sprite.
    public void Damaged()
    {
        _isDamaged = true;
        Invoke("SetNormalSprite", 0.5f);
    }

    void SetNormalSprite()
    {
        _isDamaged = false;
    }
}