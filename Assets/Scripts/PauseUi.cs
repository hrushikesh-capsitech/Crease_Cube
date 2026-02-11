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
        SoundManager.Instance.isVolumeOn = !SoundManager.Instance.isVolumeOn;
        UpdateVolumeUI();

    }

    void ToggleMusic()
    {
        SoundManager.Instance.isMusicOn = !SoundManager.Instance.isMusicOn;
        UpdateMusicUI();
    }

    void UpdateVolumeUI()
    {
        VolSlider.GetComponent<Image>().sprite = SoundManager.Instance.isVolumeOn ? SliderOnImage : SliderOffImage;
    }

    void UpdateMusicUI()
    {
        MusSlider.GetComponent<Image>().sprite = SoundManager.Instance.isMusicOn ? SliderOnImage : SliderOffImage;
        SoundManager.Instance.PlayMusic();

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
