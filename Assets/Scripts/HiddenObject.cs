using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    [Header("Reveal Settings")]
    public Color hiddenColor = new Color(1f, 1f, 1f, 0f);           // Fully invisible
    public Color revealedColor = new Color(0f, 1f, 0.7f, 1f);       // Jade glow when revealed
    public float collectRange = 3f; // How close the player must be to collect with E

    private SpriteRenderer spriteRenderer;
    private Transform player;
    private bool isRevealed = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = hiddenColor; // Start invisible

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Check if Jade Sight is active
        if (JadeSight.isActive)
        {
            JadeSight sight = Object.FindFirstObjectByType<JadeSight>();

            if (sight != null && distance <= sight.revealRadius)
            {
                // Show hint when first revealed
                if (!isRevealed)
                {
                    DialogueManager dm = Object.FindFirstObjectByType<DialogueManager>();
                    if (dm != null)
                    {
                        string[] hint = { "Something is glowing nearby... Press [E] to collect it." };
                        dm.ShowMemory(hint);
                    }
                }

                spriteRenderer.color = revealedColor;
                isRevealed = true;
            }
            else
            {
                spriteRenderer.color = hiddenColor;
                isRevealed = false;
            }
        }
        else
        {
            spriteRenderer.color = hiddenColor;
            isRevealed = false;
        }

        // If revealed and close enough, allow collection with E
        if (isRevealed && distance <= collectRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectHiddenObject();
        }
    }

    void CollectHiddenObject()
    {
        // If this object has a MemoryKey, trigger its pickup logic
        MemoryKey key = GetComponent<MemoryKey>();
        if (key != null)
        {
            PlayerInventory.Instance.AddKey(key.keyName);

            string[] message = { key.displayName + " Key Obtained!" };
            DialogueManager dm = Object.FindFirstObjectByType<DialogueManager>();
            if (dm != null) dm.ShowMemory(message);
        }

        Destroy(gameObject);
    }
}
