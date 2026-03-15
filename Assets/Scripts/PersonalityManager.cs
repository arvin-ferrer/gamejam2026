using UnityEngine;

public class PersonalityManager : MonoBehaviour
{
    public SpriteRenderer playerSprite; 

    [Header("Personality Colors")]
    public Color defaultColor = Color.white;
    public Color reddColor = Color.red;
    public Color khalilColor = Color.green;
    public Color elloColor = Color.yellow;
    public Color jadeColor = new Color(0f, 0.8f, 0.6f); // Teal/jade

    [Header("Ello Shrink Settings")]
    public float elloScale = 0.5f; // How small Ello becomes (50%)
    private Vector3 originalScale;
    private bool isShrunk = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Numeric keys for switching
        if (Input.GetKeyDown(KeyCode.Alpha1)) AttemptTransformation("Redd");
        if (Input.GetKeyDown(KeyCode.Alpha2)) AttemptTransformation("Khalil");
        if (Input.GetKeyDown(KeyCode.Alpha3)) AttemptTransformation("Ello");
        if (Input.GetKeyDown(KeyCode.Alpha4)) AttemptTransformation("Jade");
        
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
            case "jade": isUnlocked = MemoryState.Instance.jadeUnlocked; break;
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

        // Handle Ello's shrink: shrink when becoming Ello, restore otherwise
        if (newForm == MemoryState.Personality.Ello)
        {
            transform.localScale = originalScale * elloScale;
            isShrunk = true;
        }
        else if (isShrunk)
        {
            transform.localScale = originalScale;
            isShrunk = false;
        }

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
            case MemoryState.Personality.Jade:
                playerSprite.color = jadeColor;
                break;
            case MemoryState.Personality.None:
                playerSprite.color = defaultColor;
                break;
        }
        
        Debug.Log($"Transformed into: {newForm}");
    }
}