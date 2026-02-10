using UnityEngine;
using UnityEngine.UI;

public class SettingsUi : MonoBehaviour
{
    [SerializeField] private Button VolBtn;
    [SerializeField] private Button MusBtn;

    [SerializeField] private Sprite SliderOnImage;
    [SerializeField] private Sprite SliderOffImage;

    [SerializeField] private GameObject VolSlider;
    [SerializeField] private GameObject MusSlider;

    private bool isVolumeOn = true;
    private bool isMusicOn = true;

    [SerializeField] private Button PandPBtn;
    [SerializeField] private Button HomeBtn;
    void Start()
    {
        VolBtn.onClick.AddListener(ToggleVolume);
        MusBtn.onClick.AddListener(ToggleMusic);
        HomeBtn.onClick.AddListener(OnHomeClick);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void OnHomeClick()
    {
        AppStateManager.Instance.HideOverlay("Settings");
    }
}
