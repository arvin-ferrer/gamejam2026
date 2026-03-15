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
                else if (memoryName == "Ello")
                    pm.TransformTo(MemoryState.Personality.Ello);
                else if (memoryName == "Jade")
                    pm.TransformTo(MemoryState.Personality.Jade);
                else if (memoryName == "Bleu")
                    pm.TransformTo(MemoryState.Personality.Bleu);
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
                "Redd. He chose to walk beside me.",
                "Memory Unlocked: Redd.",
                "Ability Obtained: Push — You can now move heavy objects.",
                "Press [1] to transform into Redd."
            };
            dm.ShowMemory(story);
        }
        else if (memoryName == "Khalil")
        {
            string[] story = {
                "The maze... Khalil knew every turn.",
                "He spoke of patience when I only felt panic.",
                "Memory Unlocked: Khalil.",
                "Ability Obtained: Dash — You can now dash through obstacles.",
                "Press [2] to transform into Khalil. Hold [Shift] to dash."
            };
            dm.ShowMemory(story);
        }
        else if (memoryName == "Ello")
        {
            string[] story = {
                "Ello...",
                "A bright spark of joy.",
                "Memory Unlocked: Ello.",
                "Ability Obtained: Shrink — You can now fit through narrow gaps.",
                "Press [3] to transform into Ello."
            };
            dm.ShowMemory(story);
        }
        else if (memoryName == "Jade")
        {
            string[] story = {
                "The weight of the stones... the silence of the room.",
                "Jade always said the answer was never force.",
                "Sometimes you just need to be small enough to see it.",
                "Memory Unlocked: Jade.",
                "Ability Obtained: Jade Sight — Reveal hidden objects nearby.",
                "Press [4] to transform into Jade. Press [F] to activate Jade Sight."
            };
            dm.ShowMemory(story);
        }
        else if (memoryName == "Bleu")
        {
            string[] story = {
                "The darkness... I couldn't see anything.",
                "But Bleu was there. A calm light in the void.",
                "He showed me that even shadows have a shape.",
                "Memory Unlocked: Bleu.",
                "Press [5] to transform into Bleu."
            };
            dm.ShowMemory(story);
        }

}
}