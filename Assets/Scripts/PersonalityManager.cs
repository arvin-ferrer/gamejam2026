using UnityEngine;

public class PersonalityManager : MonoBehaviour
{
    public SpriteRenderer playerSprite; 

    [Header("Personality Colors")]
    public Color defaultColor = Color.white;
    public Color reddColor = Color.red;
    public Color khalilColor = Color.green; // Added Khalil
    public Color elloColor = Color.yellow; // Example for future Ello

    void Update()
    {
        // Numeric keys for switching
        if (Input.GetKeyDown(KeyCode.Alpha1)) AttemptTransformation("Redd");
        if (Input.GetKeyDown(KeyCode.Alpha2)) AttemptTransformation("Khalil");
        if (Input.GetKeyDown(KeyCode.Alpha3)) AttemptTransformation("Ello");
        
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            TransformTo(MemoryState.Personality.None);
        }
    }

    void AttemptTransformation(string name)
    {
        // We check the specific bool in MemoryState based on the string name
        bool isUnlocked = false;

        switch (name.ToLower())
        {
            case "redd": isUnlocked = MemoryState.Instance.reddUnlocked; break;
            case "khalil": isUnlocked = MemoryState.Instance.khalilUnlocked; break;
            case "ello": isUnlocked = MemoryState.Instance.elloUnlocked; break;
        }

        if (isUnlocked)
        {
            // Convert string to the Enum to call TransformTo
            MemoryState.Personality targetEnum = (MemoryState.Personality)System.Enum.Parse(typeof(MemoryState.Personality), name, true);
            TransformTo(targetEnum);
        }
        else
        {
            Debug.Log($"{name} is not unlocked yet!");
        }
    }

    public void TransformTo(MemoryState.Personality newForm)
    {
        MemoryState.Instance.currentPersonality = newForm;

        // Apply the visual changes based on the state
        switch (newForm)
        {
            case MemoryState.Personality.Redd:
                playerSprite.color = reddColor;
                break;
            case MemoryState.Personality.Khalil:
                playerSprite.color = khalilColor;
                break;
            case MemoryState.Personality.Ello:
                playerSprite.color = elloColor;
                break;
            case MemoryState.Personality.None:
                playerSprite.color = defaultColor;
                break;
        }
        
        Debug.Log($"Transformed into: {newForm}");
    }
}