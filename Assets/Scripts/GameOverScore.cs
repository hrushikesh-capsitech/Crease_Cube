using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverScore : MonoBehaviour
{
    public static GameOverScore Instance;

    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI highestScoreText;

    private int highestScore = 0;
    private const string HighestScoreKey = "HighestScore";


    private void Awake()
    {
        Instance = this;
    }

    public void GameOverScores()
    {
        int score = ScoreManager.Instance.score;

        highestScore = PlayerPrefs.GetInt(HighestScoreKey, 0);

        if (finalScoreText != null)
            finalScoreText.text = score.ToString();
        else
            Debug.LogError("ScoreManager: finalScoreText not assigned!");

        if (score > highestScore)
        {
            PlayerPrefs.SetInt(HighestScoreKey, score);
            PlayerPrefs.Save();
        }

        if (highestScoreText != null)
            highestScoreText.text = PlayerPrefs.GetInt(HighestScoreKey, 0).ToString();
        else
            Debug.LogError("ScoreManager: highestScoreText not assigned!");
    }
}
