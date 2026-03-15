using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BleuPlatform : MonoBehaviour
{
    private Collider2D platformCollider;
    private SpriteRenderer spriteRenderer;
    
    [Header("Visual Settings")]
    public float nonBleuAlpha = 0.3f;
    public float bleuAlpha = 1.0f;

    private Collider2D playerCollider;

    void Awake()
    {
        platformCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (MemoryState.Instance == null) return;

        // Dynamically find the player if not found yet
        if (playerCollider == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerCollider = playerObj.GetComponent<Collider2D>();
            }
        }

        bool isBleu = MemoryState.Instance.currentPersonality == MemoryState.Personality.Bleu;

        // Use IgnoreCollision to let ONLY the player pass through when not Bleu.
        // Other physics objects still treat it as a solid platform.
        if (playerCollider != null && platformCollider != null)
        {
            // Ignore collision if the current personality is NOT Bleu.
            Physics2D.IgnoreCollision(playerCollider, platformCollider, !isBleu);
        }

        // Adjust visibility to give player feedback
        if (spriteRenderer != null)
        {
            Color c = spriteRenderer.color;
            c.a = isBleu ? bleuAlpha : nonBleuAlpha;
            spriteRenderer.color = c;
        }
    }
}
