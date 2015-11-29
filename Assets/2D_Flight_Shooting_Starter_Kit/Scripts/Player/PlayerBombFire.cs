using UnityEngine;
using System.Collections;

/// <summary>
/// PlayerBombFire script.
/// when user clicked bomb ui button, this script is loaded.
/// </summary>
public class PlayerBombFire : MonoBehaviour
{
    public Transform bombPosition;
    public float bombRepeatTime = 0.5f;
    public float bombLastTime = 2f;

    public float bombAreaWidthMin = -17f;
    public float bombAreaWidthMax = 17f;
    public float bombAreaHeightMin = -20f;
    public float bombAreaHeightMax = 30f;
    
    private PlayerManager _playerManager;
    private readonly string _playerBombString = "PlayerBomb";

    private bool _isAbleToFireBomb = false;

    void Start()
    {
        _playerManager = GetComponent<PlayerManager>();
    }

    // Bomb UI Button Click Listener.
    public void UseBomb()
    {
        if (_playerManager.bomb == 0 || !_isAbleToFireBomb)
            return;

        _playerManager.AddBomb(-1);

        InvokeRepeating("FireBomb", 0f, bombRepeatTime);
        Invoke("StopBomb", bombLastTime);
    }

    public void SetEnableFire()
    {
        _isAbleToFireBomb = true;
    }

    public void SetDisableFire()
    {
        _isAbleToFireBomb = false;
    }
    
    void FireBomb()
    {
        for (int ix = 0; ix < 4; ++ix)
        {
            // Set Bomb Spawn Position.
            bombPosition.position = new Vector3(
                Random.Range(bombAreaWidthMin, bombAreaWidthMax), 
                Random.Range(bombAreaHeightMin, bombAreaHeightMax), 
                0f
            );

            // Spawn Bomb.
            ObjectPool.Instance.GetObject(_playerBombString, bombPosition);
        }
    }

    void StopBomb()
    {
        CancelInvoke("FireBomb");
    }
}