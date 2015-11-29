using UnityEngine;
using System.Collections;

/// <summary>
/// MissileController script.
/// Player Missile Controller.
/// Hit handling.
/// </summary>
public class MissileController : MonoBehaviour
{
    public string checkTagName;
    public float disableHeight = 50f;
    public string missilePoolName;

    // tag string.
    private readonly string _sparkString = "Spark";
    private readonly string _playerBombString = "PlayerBomb";
    private readonly string _missileString = "Missile";

    private bool _isTriggered = false;

    void OnEnable()
    {
        _isTriggered = false;
    }

    void Update()
    {
        // when missile is out of the screen, return missile object to pool manager.
        if (transform.position.y >= disableHeight || transform.position.y <= -disableHeight)
        {
            ObjectPool.Instance.ReturnObject(missilePoolName, transform);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // when this missile is already triggerd, does nothing. avoid hit test twice.
        if (_isTriggered)
            return;

        // when collided with tag name.
        // show spark effect and return missile object to pool manager.
        if (other.CompareTag(checkTagName))
        {
            ObjectPool.Instance.GetObject(_sparkString, transform);
            ObjectPool.Instance.ReturnObject(missilePoolName, transform);
            _isTriggered = true;
        }

        else if (other.CompareTag(_playerBombString) && tag != _missileString)
        {
            ObjectPool.Instance.ReturnObject(missilePoolName, transform);
            _isTriggered = true;
        }
    }
}