using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TMP_Text PerfectScorePopup;
    [SerializeField] private TMP_Text ComboPopup;

    public int ComboScore = 0;
    public int score;
    void Start()
    {
        Instance = this;
        ComboScore = 0;
    }

    
    void Update()
    {
        
    }

    public void AddScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }


    public void AddComboScore()
    {
        ComboScore++;
    }

    public void ResetScore()
    {
        ComboScore = 0;
    }
    public void showPopup()
    {
        StartCoroutine(startRoutine());
    }

    public void showComboPopup()
    {
        Debug.Log("Combo is popped up" + ComboScore);
      if(ComboScore > 1)  StartCoroutine(startComboRoutine());
       
    }

    IEnumerator startRoutine()
    {
        PerfectScorePopup.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        PerfectScorePopup.gameObject.SetActive(false);
    }

    IEnumerator startComboRoutine()
    {
        ComboPopup.gameObject.SetActive(true);
        ComboPopup.text = "COMBO " + ComboScore + "x";
        yield return new WaitForSeconds(1f);
        ComboPopup.gameObject.SetActive(false);
    }
}
