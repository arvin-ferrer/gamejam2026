using UnityEngine;

public class PersonalityManager : MonoBehaviour
{
    public SpriteRenderer playerSprite; // Drag the Square's SpriteRenderer here

    [Header("Personality Colors")]
    public Color defaultColor = Color.white;
    public Color reddColor = Color.red;
    // You can add KhalilColor, ElloColor, etc. later

    void Update()
    {
        // 1. Check for Input (Using 1, 2, 3... for quick switching)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AttemptTransformation("Redd");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            TransformTo(MemoryState.Personality.None);
        }
    }

    void AttemptTransformation(string name)
    {
        // 2. Check the "Brain" (MemoryState) to see if this is unlocked yet
        if (name == "Redd" && MemoryState.Instance.reddUnlocked)
        {
            TransformTo(MemoryState.Personality.Redd);
        }
        else
        {
            Debug.Log($"{name} is not unlocked yet!");
        }
    }

    public void TransformTo(MemoryState.Personality newForm)
    {
        MemoryState.Instance.currentPersonality = newForm;

        // 3. Update the Visuals
        switch (newForm)
        {
            case MemoryState.Personality.Redd:
                playerSprite.color = reddColor;
                break;
            case MemoryState.Personality.None:
                playerSprite.color = defaultColor;
                break;
        }
        
        Debug.Log($"Transformed into: {newForm}");
    }
}