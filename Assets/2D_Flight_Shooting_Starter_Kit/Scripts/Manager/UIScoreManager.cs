using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

/// <summary>
/// UIScoreManager script.
/// Manages score data.
/// </summary>
public class UIScoreManager : MonoBehaviour
{
    public static UIScoreManager _instance;
    public static UIScoreManager Instance { get { return _instance; } }

    public int score;
    public Text scoreText;
    public Text highScoreText;

    public int highScore;

    public string levelName;
    public GameObject prefabScreenChange;

#if UNITY_WEBPLAYER
    private string _scoreText = "Score";
#else
    private string _scoreTextPath;
#endif

    void Awake()
    {
#if !UNITY_WEBPLAYER
        _scoreTextPath = Application.persistentDataPath + "/Score.txt";
#endif
    }

    void OnEnable()
    {
        if (_instance == null)
            _instance = this;
    }

    void Start()
    {
        Load();
        highScoreText.text = highScore.ToString();
    }

    public void Save()
    {
#if UNITY_WEBPLAYER
        PlayerPrefs.SetString(_scoreText, highScore.ToString());
#else
        File.WriteAllText(_scoreTextPath, highScore.ToString());
#endif
    }

    public void Load()
    {
#if UNITY_WEBPLAYER
        if (PlayerPrefs.HasKey(_scoreText))
            highScore = int.Parse(PlayerPrefs.GetString(_scoreText));

        else
        {
            highScore = 0;
            PlayerPrefs.SetString(_scoreText, highScore.ToString());
        }
#else
        if (File.Exists(_scoreTextPath))
        {
            highScore = int.Parse(File.ReadAllText(_scoreTextPath));
        }   

        else
        {
            highScore = 0;
            File.WriteAllText(_scoreTextPath, highScore.ToString());
        }
#endif
    }

    public void CountScore(int point)
    {
        this.score += point;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        if (highScore < score)
        {
            highScore = score;
            Save();
        }

        Invoke("MoveScene", 3f);
    }

    void MoveScene()
    {
        GameObject fader = (GameObject)Instantiate(prefabScreenChange);
        fader.GetComponent<FadeOutAnimation>().SetDestLevel(levelName);
    }
}