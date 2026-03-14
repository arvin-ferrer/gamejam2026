using UnityEngine;

public class MemoryKey : MonoBehaviour
{
    public string keyName = "Redd"; 
    // Add this so you can customize the message in the Inspector
    public string displayName = "Red"; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory.Instance.AddKey(keyName);

            // Now it uses the display name (e.g., "Khalil Key Obtained!")
            string[] message = { displayName + " Key Obtained!" };
            DialogueManager dm = Object.FindFirstObjectByType<DialogueManager>();
            
            if (dm != null)
            {
                dm.ShowMemory(message);
            }

            Destroy(gameObject);
        }
    }
}