using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip gameSound;
    public AudioClip plankHitClip;

    public bool isVolumeOn = true;
    public bool isMusicOn = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            PlayMusic();
            isMusicOn = true;
        }
        else
        {
            isMusicOn= false;
        }

        if(PlayerPrefs.GetInt("Sound") == 1)
        {
            isVolumeOn = true;
        }
        else
        {
            isVolumeOn = false;
        }
    }

    public void PlayMusic()
    {
        if(isMusicOn)
        {
            musicSource.clip = gameSound;
            musicSource.loop = true;
            musicSource.Play();
            Debug.Log("im Ruuning the music block");

        }
        else
        {
            StopMusic();
        }
        
    }

    public void StopMusic()
    {
        musicSource.Pause();
    }

    public void PlaySound(AudioClip clip, float volume = 1.5f)
    {
        if (sfxSource == null || clip == null)
            return;

        if (isVolumeOn)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }
}
