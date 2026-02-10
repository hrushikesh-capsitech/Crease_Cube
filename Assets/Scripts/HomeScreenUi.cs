using UnityEngine;
using UnityEngine.UI;

public class HomeScreenUi : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Button playBtn;
    private Camera main;
    [SerializeField] private Button SettingsBtn;
    void Start()
    {
        playBtn.onClick.AddListener(playGame);
        //AppManager.instance.startAnimation();
        main = Camera.main;
        main.transform.position = new Vector3(4f, 5.5f, -6f);
        SettingsBtn.onClick.AddListener(onSettingsClick);

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

    void onSettingsClick()
    {
        AppStateManager.Instance.ShowOverlay("Settings");
    }
}
