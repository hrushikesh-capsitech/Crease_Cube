using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score;
    void Start()
    {
        Instance = this;
    }

    
    void Update()
    {
        
    }

    public void AddScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }
}
