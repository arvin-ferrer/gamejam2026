using UnityEngine;

public class LilaGate : MonoBehaviour
{
    [Header("Settings")]
    public string requiredKey = "Lila"; // Matches the 'keyName' on the Lila Key

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
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX("Gate Opening");
        // Destroy the gate so the player can proceed
        Destroy(gameObject); 
    }
}
