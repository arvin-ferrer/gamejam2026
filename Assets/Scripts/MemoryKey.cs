using UnityEngine;

public class MemoryKey : MonoBehaviour
{
    public string keyName = "Redd"; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        
            PlayerInventory.Instance.AddKey(keyName);

            string[] message = { "Red Key Obtained!" };
            DialogueManager dm = FindFirstObjectByType<DialogueManager>();
            
            if (dm != null)
            {
                dm.ShowMemory(message);
            }

            Destroy(gameObject);
        }
    }
}