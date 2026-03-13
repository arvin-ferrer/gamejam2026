using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    public string requiredColor; // Match this to the key's colorID

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if player has the right key in their inventory
            if (PlayerInventory.Instance.HasKey(requiredColor))
            {
                UnlockSequence();
            }
            else
            {
                // Here we can trigger a small "Locked" text popup later
                Debug.Log($"This memory is locked. You need the {requiredColor} key.");
            }
        }
    }

    void UnlockSequence()
    {
        // 1. Tell the Brain the memory is officially restored
        MemoryState.Instance.Unlock(requiredColor);

        // 2. Trigger the Dialogue (Using the Redd Story we wrote earlier)
        string[] reddStory = {
            "I remember the forest... it was cold and quiet.",
            "Redd. He chose to walk beside me. I wasn't alone anymore."
        };
        FindObjectOfType<DialogueManager>().ShowMemory(reddStory);

        // 3. Restore the World Color
        // This calls your existing MemoryManager to fade the saturation back in
        FindObjectOfType<MemoryManager>().RestoreRedMemory();

        // 4. Remove the lock from the world
        Destroy(gameObject);
    }
}