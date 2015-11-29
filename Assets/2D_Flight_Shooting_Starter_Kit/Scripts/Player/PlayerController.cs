using UnityEngine;
using System.Collections;

/// <summary>
/// PlayerController script.
/// Player Move with user click | touch / Hit Handling.
/// </summary>
/// 
[System.Serializable]
public class Boundary
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
}

public class PlayerController : MonoBehaviour
{
    public GameObject destroyEffect;
    public AudioClip itemSound;
    public Boundary boundary;

    private PlayerManager _playerManager;
    private Camera _mainCamera;
    private Vector3 _screenPos;
    private Vector3 _offset;

    // tag strings.
    private readonly string _enemyString = "Enemy";
    private readonly string _enemyMissileString = "EnemyMissile";
    private readonly string _boom1String = "Boom1";
    private readonly string _boom2String = "Boom2";
    private readonly string _hpItemString = "HpItem";
    private readonly string _itemHPString = "ItemHP";
    private readonly string _bombItemString = "BombItem";
    private readonly string _powerItemString = "PowerItem";
    private readonly string _itemBombString = "ItemBomb";
    private readonly string _itemPowerString = "ItemPower";

#if UNITY_5
    private new AudioSource audio;
#endif

    void Start()
    {
#if UNITY_5
        audio = GetComponent<AudioSource>();
#endif

        // Get references.
        _playerManager = GetComponent<PlayerManager>();
        _mainCamera = Camera.main;
    }

    void Update()
    {
        // when user started click or touch, then calculate the offset between player's position and touch position.
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            touchPos.z = 0f;

            _offset = transform.position - touchPos;
        }

        // when user keep dragging, move player character.
        if (Input.GetMouseButton(0))
        {
            Vector3 targetPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            targetPos.x = Mathf.Clamp(targetPos.x + _offset.x, boundary.xMin, boundary.xMax);
            targetPos.y = Mathf.Clamp(targetPos.y + _offset.y, boundary.yMin, boundary.yMax);
            targetPos.z = 0f;

            transform.position = targetPos;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // when collided with tag name "Enemy" or "EnemyMissile", lose hp.
        if (other.CompareTag(_enemyString) || other.CompareTag(_enemyMissileString))
        {
            if (_playerManager.isImmortal)
                return;

            _playerManager.AddHP(-1);

            if (_playerManager.hp > 0)
                ObjectPool.Instance.GetObject(_boom2String, transform);
            else
                ObjectPool.Instance.GetObject(_boom1String, transform);
        }

        // when collided with tag name "HpItem", get HP.
        else if (other.CompareTag(_hpItemString))
        {
            ObjectPool.Instance.ReturnObject(_itemHPString, other.gameObject.transform.parent);
            _playerManager.AddHP(1);
            audio.PlayOneShot(itemSound);
        }

        // when collided with tag name "BombItem", get bomb.
        else if (other.CompareTag(_bombItemString))
        {
            ObjectPool.Instance.ReturnObject(_itemBombString, other.gameObject.transform.parent);
            _playerManager.AddBomb(1);
            audio.PlayOneShot(itemSound);
        }

        // when collided with tag name "PowerItem", get power -> more missile.
        else if (other.CompareTag(_powerItemString))
        {
            ObjectPool.Instance.ReturnObject(_itemPowerString, other.gameObject.transform.parent);
            _playerManager.AddLevel(1);
            audio.PlayOneShot(itemSound);
        }
    }
}