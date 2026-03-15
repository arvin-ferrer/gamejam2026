using UnityEngine;

public class LilaGate : MonoBehaviour
{
    [Header("Settings")]
    public string requiredKey = "Lila"; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerInventory.Instance.HasKey(requiredKey))
            {
                OpenGate();
            }
            else
            {
                string[] hint = { "This gate requires the " + requiredKey + " key to unlock." };
                Object.FindFirstObjectByType<DialogueManager>().ShowMemory(hint);
            }
        }
    }

    void OpenGate()
    {
        Debug.Log("Lila's path is open!");

        // --- PLAY SOUND ---
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.gateOpenSound);
        }

        Destroy(gameObject); 
    }
}