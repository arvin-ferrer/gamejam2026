using UnityEngine;

public class ElloGate : MonoBehaviour
{
    [Header("Settings")]
    public string requiredKey = "Ello"; // Matches the 'keyName' on your Ello Key

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reusing the PlayerInventory logic
            if (PlayerInventory.Instance.HasKey(requiredKey))
            {
                OpenGate();
            }
            else
            {
                // Trigger the dialogue hint instead of a keypad
                string[] hint = { "This gate requires the " + requiredKey + " key found within the ruins." };
                Object.FindFirstObjectByType<DialogueManager>().ShowMemory(hint);
            }
        }
    }

    void OpenGate()
    {
        Debug.Log("Ello's path is open!");
        // You can play a 'gate opening' sound here
        
        // Destroy the gate so the player can proceed
        Destroy(gameObject); 
    }
}
