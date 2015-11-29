using UnityEngine;
using System.Collections;

/// <summary>
/// EffectSound script.
/// Play effect sound.
/// </summary>
public class EffectSound : MonoBehaviour
{
    public AudioClip sound;

#if UNITY_5
    private new AudioSource audio;
#endif

    void Start()
    {
#if UNITY_5
        audio = GetComponent<AudioSource>();
#endif

        audio.PlayOneShot(sound);
    }
}