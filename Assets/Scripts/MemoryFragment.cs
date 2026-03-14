using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    public string memoryName; // Set to "Redd" in Inspector

    public void CollectFragment()
    {
        UnlockSequence();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectFragment();
        }
    }

    void UnlockSequence()
        {
            MemoryState.Instance.Unlock(memoryName);
            TriggerStory();

            // Updated for Unity 6
            MemoryManager mm = FindFirstObjectByType<MemoryManager>();
            if (mm != null) mm.RestoreRedMemory();

            Destroy(gameObject);
        }

 void TriggerStory()
{
    Debug.Log("TriggerStory called. memoryName is: " + memoryName); 

    if (memoryName == "Redd")
    {
        Debug.Log("Match found! Searching for DialogueManager..."); 
        
        string[] story = {
            "I remember the forest... it was cold and quiet.",
            "Redd. He chose to walk beside me. I wasn't alone anymore."
        };
        
        DialogueManager dm = FindFirstObjectByType<DialogueManager>();
        if (dm != null) 
        {
            dm.ShowMemory(story);
        }
        else 
        {
            Debug.LogError("DIALOGUE MANAGER NOT FOUND IN SCENE!");
        }
    }
}
}