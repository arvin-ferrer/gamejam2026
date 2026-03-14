using UnityEngine;

public class MemoryKey : MonoBehaviour
{
    public string keyName = "Redd"; // Ensure this matches the Lock's 'Required Key'

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Add key to inventory
            PlayerInventory.Instance.AddKey(keyName);

            // 2. Trigger the "Obtained" message using existing DialogueManager
            string[] message = { "Red Key Obtained!" };
            DialogueManager dm = FindFirstObjectByType<DialogueManager>();
            
            if (dm != null)
            {
                dm.ShowMemory(message);
            }

            // 3. Remove key from the world
            Destroy(gameObject);
        }
    }
}