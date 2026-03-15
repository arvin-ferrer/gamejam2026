using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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
                else if (memoryName == "Lila")
                    pm.TransformTo(MemoryState.Personality.Lila);
            }

            // Keep your Unity 6 MemoryManager logic here if needed
            MemoryManager mm = FindFirstObjectByType<MemoryManager>();
            if (mm != null && MemoryState.Instance.AllMemoriesUnlocked()) mm.RestoreRedMemory();

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

        List<string> storyList = new List<string>();

        if (memoryName == "Redd")
        {
            storyList.AddRange(new string[] {
                "I remember the forest... it was cold and quiet.",
                "Redd. He chose to walk beside me.",
                "Memory Unlocked: Redd.",
                "Ability Obtained: Push — You can now move heavy objects.",
                "Press [1] to transform into Redd."
            });
        }
        else if (memoryName == "Khalil")
        {
            storyList.AddRange(new string[] {
                "The maze... Khalil knew every turn.",
                "He spoke of patience when I only felt panic.",
                "Memory Unlocked: Khalil.",
                "Ability Obtained: Dash — You can now dash through obstacles.",
                "Press [2] to transform into Khalil. Hold [Shift] to dash."
            });
        }
        else if (memoryName == "Ello")
        {
            storyList.AddRange(new string[] {
                "Ello...",
                "A bright spark of joy.",
                "Memory Unlocked: Ello.",
                "Ability Obtained: Shrink — You can now fit through narrow gaps.",
                "Press [3] to transform into Ello."
            });
        }
        else if (memoryName == "Jade")
        {
            storyList.AddRange(new string[] {
                "The weight of the stones... the silence of the room.",
                "Jade always said the answer was never force.",
                "Sometimes you just need to be small enough to see it.",
                "Memory Unlocked: Jade.",
                "Ability Obtained: Jade Sight — Reveal hidden objects nearby.",
                "Press [4] to transform into Jade. Press [F] to activate Jade Sight."
            });
        }
        else if (memoryName == "Bleu")
        {
            storyList.AddRange(new string[] {
                "The darkness... I couldn't see anything.",
                "But Bleu was there. A calm light in the void.",
                "He showed me that even shadows have a shape.",
                "Memory Unlocked: Bleu.",
                "Press [5] to transform into Bleu."
            });
        }
        else if (memoryName == "Lila")
        {
            storyList.AddRange(new string[] {
                "Lila...",
                "The gentle breeze and the smell of lavender.",
                "Memory Unlocked: Lila.",
                "Press [6] to transform into Lila."
            });
        }

        if (MemoryState.Instance.AllMemoriesUnlocked())
        {
            storyList.AddRange(new string[] {
                "The protagonist understands their past.",
                "The world fully regains its color.",
                "The forest is no longer empty or silent.",
                "Memories can bring pain, but they are also what give life its color."
            });

            dm.ShowMemory(storyList.ToArray(), () => {
                MemoryManager mm = FindFirstObjectByType<MemoryManager>();
                if (mm != null)
                {
                    mm.ShowRestartPrompt();
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            });
        }
        else
        {
            dm.ShowMemory(storyList.ToArray());
        }
    }
}
