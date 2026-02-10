using UnityEngine;
using UnityEngine.UI;

public class PauseUi : MonoBehaviour
{
    [SerializeField] private Button HomeBtn;
    [SerializeField] private Button ResumeBtn;
    [SerializeField] private Button VolBtn;
    [SerializeField] private Button MusBtn;

    [SerializeField] private Sprite SliderOnImage;
    [SerializeField] private Sprite SliderOffImage;

    [SerializeField] private GameObject VolSlider;
    [SerializeField] private GameObject MusSlider;
    private bool isVolumeOn = true;
    private bool isMusicOn = true;

    void Start()
    {
        VolBtn.onClick.AddListener(ToggleVolume);
        MusBtn.onClick.AddListener(ToggleMusic);
        ResumeBtn.onClick.AddListener(OnResumeClick);
        HomeBtn.onClick.AddListener(OnHomeClick);

        UpdateVolumeUI();
        UpdateMusicUI();
    }

    void ToggleVolume()
    {
        isVolumeOn = !isVolumeOn;
        UpdateVolumeUI();

    }

    void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        UpdateMusicUI();
    }

    void UpdateVolumeUI()
    {
        VolSlider.GetComponent<Image>().sprite = isVolumeOn ? SliderOnImage : SliderOffImage;
    }

    void UpdateMusicUI()
    {
        MusSlider.GetComponent<Image>().sprite = isMusicOn ? SliderOnImage : SliderOffImage;
    }

    void OnResumeClick()
    {
        Time.timeScale = 1f;
        AppStateManager.Instance.HideOverlay("Pause");
    }
    void OnHomeClick()
    {
        Time.timeScale = 1.0f;
        AppStateManager.Instance.HideOverlay("Pause");
        AppManager.instance.ExitGame();
        AppStateManager.Instance.SetHome();
    }
}
