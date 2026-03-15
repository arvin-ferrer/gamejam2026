using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [Tooltip("AudioSource component for background music")]
    public AudioSource musicSource;
    [Tooltip("AudioSource component for playing sound effects")]
    public AudioSource sfxSource;

    [Header("Sound Library")]
    public Sound[] sfxLibrary;
    public Sound[] musicLibrary;

    private Dictionary<string, Sound> sfxDictionary;
    private Dictionary<string, Sound> musicDictionary;

    private void Awake()
    {
        // Singleton pattern to ensure only one SoundManager exists and it persists between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDictionaries();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDictionaries()
    {
        sfxDictionary = new Dictionary<string, Sound>();
        foreach (Sound s in sfxLibrary)
        {
            sfxDictionary[s.name] = s;
        }

        musicDictionary = new Dictionary<string, Sound>();
        foreach (Sound m in musicLibrary)
        {
            musicDictionary[m.name] = m;
        }
    }

    private void Start()
    {
        // Auto-play the first music track in the library if one exists
        if (musicLibrary != null && musicLibrary.Length > 0)
        {
            PlayMusic(musicLibrary[0].name);
        }
    }

    /// <summary>
    /// Play a sound effect by its string name.
    /// Example: SoundManager.Instance.PlaySFX("Jump");
    /// </summary>
    public void PlaySFX(string soundName)
    {
        if (sfxDictionary.TryGetValue(soundName, out Sound sound))
        {
            // PlayOneShot allows multiple SFX to overlap without cutting each other off
            sfxSource.pitch = sound.pitch;
            sfxSource.PlayOneShot(sound.clip, sound.volume);
        }
        else
        {
            Debug.LogWarning($"SFX '{soundName}' not found in SoundManager library.");
        }
    }

    /// <summary>
    /// Play background music by its string name.
    /// Example: SoundManager.Instance.PlayMusic("Level1BGM");
    /// </summary>
    public void PlayMusic(string trackName)
    {
        if (musicDictionary.TryGetValue(trackName, out Sound sound))
        {
            musicSource.clip = sound.clip;
            musicSource.volume = sound.volume;
            musicSource.pitch = sound.pitch;
            musicSource.loop = true; 
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music track '{trackName}' not found in SoundManager library.");
        }
    }

    /// <summary>
    /// Stops the currently playing background music.
    /// </summary>
    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    /// <summary>
    /// Play a sound effect by passing an AudioClip directly.
    /// </summary>
    public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        if (clip != null)
        {
            sfxSource.pitch = pitch;
            sfxSource.PlayOneShot(clip, volume);
        }
    }

    /// <summary>
    /// Play background music by passing an AudioClip directly.
    /// </summary>
    public void PlayMusic(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.volume = volume;
            musicSource.pitch = pitch;
            musicSource.loop = true; 
            musicSource.Play();
        }
    }
}
