using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource audioSource;

    [Header("Clips")]
    public AudioClip plankHitClip;
    public AudioClip cubeSlideClip;
    public AudioClip gameOverClip;
    public AudioClip nextCubeClip;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        audioSource.PlayOneShot(clip, volume);
    }
}
