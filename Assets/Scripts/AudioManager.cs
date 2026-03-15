using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("--- Audio Sources ---")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("--- Audio Clips ---")]
    public AudioClip backgroundMusic;
    public AudioClip dashSound;
    public AudioClip teleportSound;
    public AudioClip pushSound;
    [Header("--- Collectible Sounds ---")]
    public AudioClip keySound;
    public AudioClip fragmentSound;
    [Header("--- Environment Sounds ---")]
    public AudioClip gateOpenSound;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}