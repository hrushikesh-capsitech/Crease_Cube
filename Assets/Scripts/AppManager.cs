using UnityEngine;

public class AppManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static AppManager instance;
    [SerializeField] private GameObject GameLogicPrefab;
    [SerializeField] private GameObject AnimationLogicPrefab;

    private GameObject GameLogic;
    private GameObject AnimationLogic;
    void Start()
    {
        instance = this;
        AppStateManager.Instance.SetHome();
        this.startAnimation();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startAnimation()
    {
        if (AnimationLogic == null)
        {
            AnimationLogic = Instantiate(AnimationLogicPrefab);
        }
        AnimationLogic.SetActive(true);
    }

    public void stopAnimation()
    {
        if (AnimationLogic == null) return;
        AnimationLogic.SetActive(false);
      //  Destroy(AnimationLogic);
    }

    public void StartGame()
    {

        if (GameLogic == null)
        {
            GameLogic = Instantiate(GameLogicPrefab);
            GameLogic.SetActive(true);
        }
    }

    public void ExitGame()
    {
        if (GameLogic == null) return;
        GameLogic.SetActive(false);
        Destroy(GameLogic);
    }

}
