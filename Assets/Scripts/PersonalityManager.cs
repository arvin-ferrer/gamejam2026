using UnityEngine;

public class PersonalityManager : MonoBehaviour
{
    public SpriteRenderer playerSprite; 

    [Header("Personality Colors")]
    public Color defaultColor = Color.white;
    public Color reddColor = Color.red;
    // add KhalilColor, ElloColor, etc. later

    void Update()
    {
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