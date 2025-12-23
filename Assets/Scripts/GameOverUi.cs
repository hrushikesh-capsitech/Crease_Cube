using UnityEngine;
using UnityEngine.UI;

public class GameOverUi : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Button restartBtn;
    [SerializeField] private Button HomeBtn;
    void Start()
    {
        restartBtn.onClick.AddListener(restart);
        HomeBtn.onClick.AddListener(Home);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Home()
    {
        
        Time.timeScale = 1.0f;

        AppManager.instance.ExitGame();
        AppManager.instance.startAnimation();
        AppStateManager.Instance.SetHome();
    }

    public void restart()
    {
        GameManager.Instance.RestartGame();
    }
}
