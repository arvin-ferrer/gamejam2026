using UnityEngine;

public class MazeGate : MonoBehaviour
{
    [Header("Settings")]
    public string requiredKey = "Khalil"; 

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
                string[] hint = { "This gate requires the " + requiredKey + " key found within the maze." };
                Object.FindFirstObjectByType<DialogueManager>().ShowMemory(hint);
            }
        }
    }

    void OpenGate()
    {
        Debug.Log("Khalil's path is open!");

        // --- ADDED AUDIO CALL ---
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.gateOpenSound);
        }
        // -------------------------
        
        Destroy(gameObject); 
    }
}