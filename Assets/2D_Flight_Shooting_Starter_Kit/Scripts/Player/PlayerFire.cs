using UnityEngine;
using System.Collections;

/// <summary>
/// PlayerFire script.
/// Shoot missile.
/// </summary>
public class PlayerFire : MonoBehaviour
{
    public GameObject missile;
    public AudioClip sound;
    public Transform[] firePos;
    public float repeat = 0.1f;

    public bool isTutorial = false;

    private bool _isEnableToShoot = false;

    private Animator _animator;
    private PlayerManager _playerManager;

    // tag strings.
    private readonly string _missileString = "Missile";
    private readonly string _shootingFireString = "ShootingFire";
    
#if UNITY_5
    private new AudioSource audio;
#endif
    
    void Start()
    {   
#if UNITY_5
        audio = GetComponent<AudioSource>();
#endif
        _animator = GetComponent<Animator>();
        _playerManager = GetComponent<PlayerManager>();
        InvokeRepeating("Shoot", 0f, repeat);

        if (isTutorial)
            _isEnableToShoot = true;

        this.SetEnableShoot();
    }

    void Shoot()
    {
        if (!_isEnableToShoot)
            return;

        Fire(0);
        Fire(1);

        ObjectPool.Instance.GetObject(_shootingFireString, firePos[0]);
        ObjectPool.Instance.GetObject(_shootingFireString, firePos[1]);

        if (_playerManager.level >= 2)
        {
            Fire(2);
            Fire(3);
        }

        if (_playerManager.level >= 3)
        {
            Fire(4);
            Fire(5);
        }

        audio.PlayOneShot(sound);
    }

    void Fire(int index)
    {
        ObjectPool.Instance.GetObject(_missileString, firePos[index]);
    }

    // When player start animation finished, this function will be called from animation event.
    public void SetEnableShoot()
    {
        _isEnableToShoot = true;
        _animator.enabled = false;

        // Enable fire bomb.
        if (GetComponent<PlayerBombFire>() != null)
            GetComponent<PlayerBombFire>().SetEnableFire();
    }

    public void SetDisableShoot()
    {
        _isEnableToShoot = false;
        _playerManager.SetDefault();

        // Disable fire bomb. 
        if (GetComponent<PlayerBombFire>() != null)
            GetComponent<PlayerBombFire>().SetDisableFire();
    }
}