using UnityEngine;
using UnityEngine.UI;

public class SettingsUi : MonoBehaviour
{
    public static SettingsUi Instance;

    [SerializeField] private Button VolBtn;
    [SerializeField] private Button MusBtn;

    [SerializeField] private Sprite SliderOnImage;
    [SerializeField] private Sprite SliderOffImage;

    [SerializeField] private GameObject VolSlider;
    [SerializeField] private GameObject MusSlider;

    [SerializeField] private Button PandPBtn;
    [SerializeField] private Button HomeBtn;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        VolBtn.onClick.AddListener(ToggleVolume);
        MusBtn.onClick.AddListener(ToggleMusic);
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

    void OnHomeClick()
    {
        AppStateManager.Instance.HideOverlay("Settings");
    }
}
