using UnityEngine;

public class ElloGate : MonoBehaviour
{
    [Header("Settings")]
    public string requiredKey = "Ello"; 

    private void OnCollisionEnter2D(Collider2D other) // Use Trigger if the gate is a trigger, or Collision if it's solid
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PlayerInventory.Instance.HasKey(requiredKey))
            {
                OpenGate();
            }
            else
            {
                string[] hint = { "This gate requires the " + requiredKey + " key found within the ruins." };
                Object.FindFirstObjectByType<DialogueManager>().ShowMemory(hint);
            }
        }
    }

    void OpenGate()
    {
        Debug.Log("Ello's path is open!");

        // --- THE MAGIC LINE ---
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.gateOpenSound);
        }
        
        Destroy(gameObject); 
    }
}