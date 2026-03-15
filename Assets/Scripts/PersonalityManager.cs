using UnityEngine;

public class PersonalityManager : MonoBehaviour
{
    public SpriteRenderer playerSprite; 
    private PlayerController playerController;

    [Header("Main Character Sprites")]
    public Sprite mainUp;
    public Sprite mainDown;
    public Sprite mainLeft;
    public Sprite mainRight;

    [Header("Redd Sprites")]
    public Sprite reddUp;
    public Sprite reddDown;
    public Sprite reddLeft;
    public Sprite reddRight;

    [Header("Redd Walk Sprites")]
    public Sprite[] reddWalkUp;
    public Sprite[] reddWalkDown;
    public Sprite[] reddWalkLeft;
    public Sprite[] reddWalkRight;

    [Header("Khalil Sprites")]
    public Sprite khalilUp;
    public Sprite khalilDown;
    public Sprite khalilLeft;
    public Sprite khalilRight;

    [Header("Khalil Walk Sprites")]
    public Sprite[] khalilWalkUp;
    public Sprite[] khalilWalkDown;
    public Sprite[] khalilWalkLeft;
    public Sprite[] khalilWalkRight;

    [Header("Ello Sprites")]
    public Sprite elloUp;
    public Sprite elloDown;
    public Sprite elloLeft;
    public Sprite elloRight;

    [Header("Ello Walk Sprites")]
    public Sprite[] elloWalkUp;
    public Sprite[] elloWalkDown;
    public Sprite[] elloWalkLeft;
    public Sprite[] elloWalkRight;

    [Header("Jade Sprites")]
    public Sprite jadeUp;
    public Sprite jadeDown;
    public Sprite jadeLeft;
    public Sprite jadeRight;

    [Header("Jade Walk Sprites")]
    public Sprite[] jadeWalkUp;
    public Sprite[] jadeWalkDown;
    public Sprite[] jadeWalkLeft;
    public Sprite[] jadeWalkRight;

    [Header("Bleu Sprites")]
    public Sprite bleuUp;
    public Sprite bleuDown;
    public Sprite bleuLeft;
    public Sprite bleuRight;

    [Header("Bleu Walk Sprites")]
    public Sprite[] bleuWalkUp;
    public Sprite[] bleuWalkDown;
    public Sprite[] bleuWalkLeft;
    public Sprite[] bleuWalkRight;

    [Header("Lila Sprites")]
    public Sprite lilaUp;
    public Sprite lilaDown;
    public Sprite lilaLeft;
    public Sprite lilaRight;

    [Header("Lila Walk Sprites")]
    public Sprite[] lilaWalkUp;
    public Sprite[] lilaWalkDown;
    public Sprite[] lilaWalkLeft;
    public Sprite[] lilaWalkRight;

    [Header("Ello Shrink Settings")]
    public float elloScale = 0.5f;
    private Vector3 originalScale;
    private bool isShrunk = false;

    void Start()
    {
        originalScale = transform.localScale;
        playerController = GetComponent<PlayerController>();

        // Set default sprites on start
        if (playerController != null)
        {
            playerController.spriteRenderer = playerSprite;
            playerController.SetSprites(mainUp, mainDown, mainLeft, mainRight);
        }
    }

    void Update()
    {
        // Numeric keys for switching
        if (Input.GetKeyDown(KeyCode.Alpha1)) AttemptTransformation("Redd");
        if (Input.GetKeyDown(KeyCode.Alpha2)) AttemptTransformation("Khalil");
        if (Input.GetKeyDown(KeyCode.Alpha3)) AttemptTransformation("Ello");
        if (Input.GetKeyDown(KeyCode.Alpha4)) AttemptTransformation("Jade");
        if (Input.GetKeyDown(KeyCode.Alpha5)) AttemptTransformation("Bleu");
        if (Input.GetKeyDown(KeyCode.Alpha6)) AttemptTransformation("Lila");
        
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            TransformTo(MemoryState.Personality.None);
        }
    }

    void AttemptTransformation(string name)
    {
        if (MemoryState.Instance == null) return;
        bool isUnlocked = false;

        switch (name.ToLower())
        {
            case "redd": isUnlocked = MemoryState.Instance.reddUnlocked; break;
            case "khalil": isUnlocked = MemoryState.Instance.khalilUnlocked; break;
            case "ello": isUnlocked = MemoryState.Instance.elloUnlocked; break;
            case "jade": isUnlocked = MemoryState.Instance.jadeUnlocked; break;
            case "bleu": isUnlocked = MemoryState.Instance.bleuUnlocked; break;
            case "lila": isUnlocked = MemoryState.Instance.lilaUnlocked; break;
        }

        if (isUnlocked)
        {
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
        if (MemoryState.Instance == null) return;
        MemoryState.Instance.currentPersonality = newForm;

        // Handle Ello's shrink
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

        // Swap sprites based on personality
        switch (newForm)
        {
            case MemoryState.Personality.Redd:
                playerController.SetSprites(reddUp, reddDown, reddLeft, reddRight);
                SetOrClearWalkSprites(reddWalkUp, reddWalkDown, reddWalkLeft, reddWalkRight);
                break;
            case MemoryState.Personality.Khalil:
                playerController.SetSprites(khalilUp, khalilDown, khalilLeft, khalilRight);
                SetOrClearWalkSprites(khalilWalkUp, khalilWalkDown, khalilWalkLeft, khalilWalkRight);
                break;
            case MemoryState.Personality.Ello:
                playerController.SetSprites(elloUp, elloDown, elloLeft, elloRight);
                SetOrClearWalkSprites(elloWalkUp, elloWalkDown, elloWalkLeft, elloWalkRight);
                break;
            case MemoryState.Personality.Jade:
                playerController.SetSprites(jadeUp, jadeDown, jadeLeft, jadeRight);
                SetOrClearWalkSprites(jadeWalkUp, jadeWalkDown, jadeWalkLeft, jadeWalkRight);
                break;
            case MemoryState.Personality.Bleu:
                playerController.SetSprites(bleuUp, bleuDown, bleuLeft, bleuRight);
                SetOrClearWalkSprites(bleuWalkUp, bleuWalkDown, bleuWalkLeft, bleuWalkRight);
                break;
            case MemoryState.Personality.Lila:
                playerController.SetSprites(lilaUp, lilaDown, lilaLeft, lilaRight);
                SetOrClearWalkSprites(lilaWalkUp, lilaWalkDown, lilaWalkLeft, lilaWalkRight);
                break;
            case MemoryState.Personality.None:
                playerController.SetSprites(mainUp, mainDown, mainLeft, mainRight);
                playerController.RestoreMainWalkSprites();
                break;
        }

        // Reset color to white so sprites show their true colors
        playerSprite.color = Color.white;
        
        Debug.Log($"Transformed into: {newForm}");
    }

    /// <summary>
    /// Sets walk sprites if any are assigned, otherwise clears them (idle-only fallback).
    /// </summary>
    private void SetOrClearWalkSprites(Sprite[] up, Sprite[] down, Sprite[] left, Sprite[] right)
    {
        bool hasWalkSprites = (up != null && up.Length > 0) ||
                              (down != null && down.Length > 0) ||
                              (left != null && left.Length > 0) ||
                              (right != null && right.Length > 0);

        if (hasWalkSprites)
        {
            playerController.SetWalkSprites(up, down, left, right);
        }
        else
        {
            playerController.ClearWalkSprites();
        }
    }
}