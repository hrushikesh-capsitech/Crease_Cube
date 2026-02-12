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
        PlayerPrefs.SetInt("Sound", SoundManager.Instance.isVolumeOn ? 1 : 0);
        UpdateVolumeUI();

    }

    void ToggleMusic()
    {
        SoundManager.Instance.isMusicOn = !SoundManager.Instance.isMusicOn;
        PlayerPrefs.SetInt("Music", SoundManager.Instance.isMusicOn ? 1 : 0);
        if (SoundManager.Instance.isMusicOn) SoundManager.Instance.PlayMusic();
        else
        {
            SoundManager.Instance.StopMusic();
        }
        UpdateMusicUI();
    }

    void UpdateVolumeUI()
    {
        VolSlider.GetComponent<Image>().sprite = PlayerPrefs.GetInt("Sound") == 1 ? SliderOnImage : SliderOffImage;
    }

    void UpdateMusicUI()
    {
        MusSlider.GetComponent<Image>().sprite = PlayerPrefs.GetInt("Music") == 1 ? SliderOnImage : SliderOffImage;
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
