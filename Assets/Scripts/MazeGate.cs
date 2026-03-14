using UnityEngine;

public class MazeGate : MonoBehaviour
{
    [Header("Settings")]
    public string requiredKey = "Khalil"; // Matches the 'keyName' on your Khalil Key

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reusing your PlayerInventory logic
            if (PlayerInventory.Instance.HasKey(requiredKey))
            {
                OpenGate();
            }
            else
            {
                // Trigger the dialogue hint instead of a keypad
                string[] hint = { "This gate requires the " + requiredKey + " key found within the maze." };
                Object.FindFirstObjectByType<DialogueManager>().ShowMemory(hint);
            }
        }
    }

    void OpenGate()
    {
        Debug.Log("Khalil's path is open!");
        // You can play a 'gate opening' sound here
        
        // Destroy the gate so the player can reach the Khalil Fragment
        Destroy(gameObject); 
    }
}