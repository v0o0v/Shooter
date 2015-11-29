using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// UIShowScore script.
/// When GameOver, show result.
/// </summary>
public class UIShowScore : MonoBehaviour
{
    public Text highScoreText;
    public Text scoreText;

    void Start()
    {
        if (highScoreText != null && scoreText != null)
        {
            int score = UIScoreManager.Instance.score;
            int highScore = UIScoreManager.Instance.highScore < score ? score : UIScoreManager.Instance.highScore;

            highScoreText.text = "Hight Score : " + highScore.ToString();
            scoreText.text = "Score : " + score.ToString();
        }
    }
}