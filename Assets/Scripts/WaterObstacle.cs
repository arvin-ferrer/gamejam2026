using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WaterObstacle : MonoBehaviour
{
    private Collider2D waterCollider;
    private Collider2D playerCollider;

    void Awake()
    {
        waterCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (MemoryState.Instance == null) return;

        // Find the player dynamically if not found yet
        if (playerCollider == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerCollider = playerObj.GetComponent<Collider2D>();
            }
        }

        if (playerCollider != null && waterCollider != null)
        {
            bool isBleu = MemoryState.Instance.currentPersonality == MemoryState.Personality.Bleu;

            // If the personality is Bleu, ignore collision with Water (making it passable).
            // Otherwise, enforce collision (acting as an obstacle).
            Physics2D.IgnoreCollision(playerCollider, waterCollider, isBleu);
        }
    }
}
