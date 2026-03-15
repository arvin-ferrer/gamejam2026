using UnityEngine;

public class MemoryState : MonoBehaviour
{
    public static MemoryState Instance;

    // 1. The 6 Personality Types
    public enum Personality { None, Redd, Khalil, Ello, Jade, Bleu, Lila }
    public Personality currentPersonality = Personality.None;

    // 2. Tracking Unlocks (Precision Bools)
    [Header("Unlock Status")]
    public bool reddUnlocked = false;
    public bool khalilUnlocked = false;
    public bool elloUnlocked = false;
    public bool jadeUnlocked = false;
    public bool bleuUnlocked = false;
    public bool lilaUnlocked = false;

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // A precise method to unlock a memory by name
    public void Unlock(string memoryName)
    {
        switch (memoryName.ToLower())
        {
            case "redd": reddUnlocked = true; break;
            case "khalil": khalilUnlocked = true; break;
            case "ello": elloUnlocked = true; break;
            case "jade": jadeUnlocked = true; break;
            case "bleu": bleuUnlocked = true; break;
            case "lila": lilaUnlocked = true; break;
        }
        Debug.Log($"Memory Unlocked: {memoryName}");
    }

    public bool AllMemoriesUnlocked()
    {
        return reddUnlocked && khalilUnlocked && elloUnlocked && jadeUnlocked && bleuUnlocked && lilaUnlocked;
    }
}