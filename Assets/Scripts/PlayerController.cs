using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Interaction")]
    public float interactRange = 1.5f;
    public LayerMask interactableLayer; // Make sure to set this to 'Interactable' in Unity

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Ensure no gravity
        rb.freezeRotation = true;
    }

    void Update()
    {
        // 1. Movement Input Polling
        float x = 0;
        float y = 0;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) y = 1;
            if (Keyboard.current.sKey.isPressed) y = -1;
            if (Keyboard.current.aKey.isPressed) x = -1;
            if (Keyboard.current.dKey.isPressed) x = 1;

            // 2. Interaction Input Polling
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                PerformInteraction();
            }
        }

        moveInput = new Vector2(x, y).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    void PerformInteraction()
    {
        // Search for objects on the Interactable layer within a circle around the player
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRange, interactableLayer);
        
        if (hit != null)
        {
            // Try to get the LockController from whatever we hit
            if (hit.TryGetComponent(out LockController lockObj))
            {
                lockObj.Interact();
            }
            // You can add more 'else if' checks here for other interactable types later
        }
    }

    // Helper to see the interaction range in the Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}