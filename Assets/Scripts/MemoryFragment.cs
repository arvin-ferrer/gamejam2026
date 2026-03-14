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

            // 1. Find the PersonalityManager on the Player
            PersonalityManager pm = FindFirstObjectByType<PersonalityManager>();
            if (pm != null)
            {
                // 2. Check the name and tell the manager to switch the color
                if (memoryName == "Redd") 
                    pm.TransformTo(MemoryState.Personality.Redd);
                else if (memoryName == "Khalil") 
                    pm.TransformTo(MemoryState.Personality.Khalil);
            }

            // Keep your Unity 6 MemoryManager logic here if needed
            MemoryManager mm = FindFirstObjectByType<MemoryManager>();
            if (mm != null && memoryName == "Redd") mm.RestoreRedMemory();

            Destroy(gameObject);
        }
    void TriggerStory()
    {
        DialogueManager dm = FindFirstObjectByType<DialogueManager>();
        if (dm == null) 
        {
            Debug.LogError("DIALOGUE MANAGER NOT FOUND!");
            return;
        }

        if (memoryName == "Redd")
        {
            string[] story = {
                "I remember the forest... it was cold and quiet.",
                "Redd. He chose to walk beside me."
            };
            dm.ShowMemory(story);
        }
        else if (memoryName == "Khalil") // Add this block
        {
            string[] story = {
                "The maze... Khalil knew every turn.",
                "He spoke of patience when I only felt panic."
            };
            dm.ShowMemory(story);
        }

}
}