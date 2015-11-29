using UnityEngine;
using System.Collections;

/// <summary>
/// MissileMovement script.
/// Moves missile upward.
/// </summary>
public class MissileMovement : MonoBehaviour
{
    public float missileSpeed = 10f;

    void Update()
    {
        transform.localPosition += transform.up * missileSpeed * Time.deltaTime;
    }
}