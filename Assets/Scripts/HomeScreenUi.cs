using UnityEngine;
using UnityEngine.UI;

public class HomeScreenUi : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Button playBtn;
    private Camera main;
    void Start()
    {
        playBtn.onClick.AddListener(playGame);
        //AppManager.instance.startAnimation();
        main = Camera.main;
        main.transform.position = new Vector3(4f, 4.1f, -6f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playGame()
    {

        AppStateManager.Instance.SetGameplay();
        AppManager.instance.stopAnimation();
        AppManager.instance.StartGame();
    }
}
