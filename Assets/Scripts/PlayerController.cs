using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Environment States")]
    private bool isInWindyZone = false;
    private Vector2 windVelocity;

    [Header("Dash (Khalil)")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing;
    private bool canDash = true;

    [Header("Interaction")]
    public float interactRange = 1.5f;
    public LayerMask interactableLayer; // Make sure to set this to 'Interactable' in Unity

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; 
        rb.freezeRotation = true;
    }

    void Update()
    {
        float x = 0;
        float y = 0;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) y = 1;
            if (Keyboard.current.sKey.isPressed) y = -1;
            if (Keyboard.current.aKey.isPressed) x = -1;
            if (Keyboard.current.dKey.isPressed) x = 1;

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                PerformInteraction();
            }

            if (Keyboard.current.shiftKey.wasPressedThisFrame && canDash && !isDashing)
            {
                if (MemoryState.Instance != null && MemoryState.Instance.currentPersonality == MemoryState.Personality.Khalil)
                {
                    StartCoroutine(DashCoroutine());
                }
            }
        } // End of if (Keyboard.current != null)

        if (!isDashing && !isInWindyZone) // Disable normal input movement in wind
        {
            moveInput = new Vector2(x, y).normalized;
        }
        else if (isInWindyZone && !isDashing) // If in wind, kill intentional movement input
        {
            moveInput = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        if (isInWindyZone)
        {
            rb.linearVelocity = windVelocity; // Force push the player
        }
        else
        {
            rb.linearVelocity = moveInput * moveSpeed; // Normal movement
        }
    }

    private System.Collections.IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;
        
        // If moving, dash in that direction. Otherwise, default to facing right.
        Vector2 dashDirection = moveInput != Vector2.zero ? moveInput : new Vector2(1, 0);

        rb.linearVelocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    // --- Windy Zone Methods ---
    public void EnterWindyZone(Vector2 force)
    {
        isInWindyZone = true;
        windVelocity = force;
    }

    public void ExitWindyZone()
    {
        isInWindyZone = false;
        windVelocity = Vector2.zero;
    }

    void PerformInteraction()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRange, interactableLayer);
        
        if (hit != null)
        {
            if (hit.TryGetComponent(out LockController lockObj))
            {
                lockObj.Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}