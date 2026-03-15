using UnityEngine;

public class JadeGate : MonoBehaviour
{
    [Header("Settings")]
    public string requiredKey = "Jade";

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
                string[] hint = { "This gate requires the " + requiredKey + " key to open." };
                Object.FindFirstObjectByType<DialogueManager>().ShowMemory(hint);
            }
        }
    }

    void OpenGate()
    {
        Debug.Log("Jade's path is open!");

        // --- PLAY SOUND BEFORE DESTROYING ---
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.gateOpenSound);
        }

        Destroy(gameObject);
    }
}