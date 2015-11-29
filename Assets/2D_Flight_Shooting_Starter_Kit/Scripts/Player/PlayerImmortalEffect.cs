using UnityEngine;
using System.Collections;

/// <summary>
/// PlayerImmortalMode script.
/// Player immortal effect.
/// </summary>
public class PlayerImmortalEffect : MonoBehaviour
{
    public Color normalColor;
    public Color dimColor;
    public float blinkTime = 0.2f;

    private SpriteRenderer _sprite;
    private bool _isNormal = true;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void SetImmortal(float lastTime)
    {
        InvokeRepeating("Blink", blinkTime, blinkTime);
        Invoke("SetMortal", lastTime);
    }

    void Blink()
    {
        _sprite.color = _isNormal ? dimColor : normalColor;
        _isNormal = !_isNormal;
    }

    void SetMortal()
    {
        CancelInvoke("Blink");

        _sprite.color = normalColor;
        _isNormal = true;
    }
}