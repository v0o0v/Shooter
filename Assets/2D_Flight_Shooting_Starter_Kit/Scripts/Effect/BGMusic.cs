using UnityEngine;
using System.Collections;

/// <summary>
/// BGMusic script.
/// Singleton Component playing BGM.
/// </summary>
public class BGMusic : MonoBehaviour
{
    private static BGMusic _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            Screen.SetResolution(480, 840, false);
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}