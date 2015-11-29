using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// UIOpenScene script.
/// Load New scene.
/// </summary>
public class UIOpenScene : MonoBehaviour
{
    public string levelName;                    // Level name to load
    public GameObject prefabScreenChange;       // prefab playing alpha animation.

#if !UNITY_EDITOR && UNITY_WEBPLAYER
    private readonly string _openUrl = "http://www.google.com";
#endif

    void Update()
    {
        // when android back button touched quit this app.
        if (Input.GetKeyUp(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
            Application.OpenURL(_openUrl);
#else
            Application.Quit();
#endif
        }
    }

    public void OnClickListener()
    {
        // Create prefab and set level name to load.
        GameObject fader = (GameObject)Instantiate(prefabScreenChange);
        fader.GetComponent<FadeOutAnimation>().SetDestLevel(levelName);
    }
}