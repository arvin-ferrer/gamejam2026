using UnityEngine;

public class MemoryKey : MonoBehaviour
{
    // Type "Redd", "Khalil", etc. in the Inspector
    public string colorID; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Record that we have this key
            PlayerInventory.Instance.AddKey(colorID);
            
            // Visual feedback
            Debug.Log($"Picked up {colorID} key!");
            Destroy(gameObject); 
        }
    }
}