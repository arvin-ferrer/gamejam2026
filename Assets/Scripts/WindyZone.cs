using UnityEngine;

public class WindyZone : MonoBehaviour
{
    [Header("Settings")]
    public float windForce = 5f; // How fast it pushes you horizontally
    public Vector2 windDirection = new Vector2(1, 0); // Pushing right by default

    [Header("Hint Dialogue")]
    [TextArea]
    public string[] hintDialogue = { "The wind is too strong to walk against...", "Maybe if I could dash through it..." };
    private bool hasShownHint = false;

    private AudioSource windAudioSource;

    private void Start()
    {
        windAudioSource = gameObject.AddComponent<AudioSource>();
        windAudioSource.loop = true;
        windAudioSource.playOnAwake = false;

        // Try to get the wind clip from SoundManager if it exists
        if (SoundManager.Instance != null)
        {
            foreach (Sound s in SoundManager.Instance.sfxLibrary)
            {
                if (s.name == "Wind")
                {
                    windAudioSource.clip = s.clip;
                    windAudioSource.volume = s.volume;
                    windAudioSource.pitch = s.pitch;
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (windAudioSource.clip != null && !windAudioSource.isPlaying)
            {
                windAudioSource.Play();
            }

            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.EnterWindyZone(windDirection * windForce);

                if (!hasShownHint)
                {
                    hasShownHint = true;
                    DialogueManager dm = Object.FindFirstObjectByType<DialogueManager>();
                    if (dm != null)
                    {
                        dm.ShowMemory(hintDialogue);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (windAudioSource.isPlaying)
            {
                windAudioSource.Stop();
            }

            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.ExitWindyZone();
            }
        }
    }
}
