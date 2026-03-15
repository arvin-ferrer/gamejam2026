using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Sprites - Idle (set by PersonalityManager)")]
    public SpriteRenderer spriteRenderer;
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;
    private Vector2 lastDirection = Vector2.down;

    [Header("Sprites - Walk Frames (drag sliced sprites here)")]
    public Sprite[] walkUp;     // 4 frames
    public Sprite[] walkDown;   // 4 frames
    public Sprite[] walkLeft;   // 4 frames
    public Sprite[] walkRight;  // 4 frames

    [Header("Walk Animation")]
    public float frameRate = 8f; // Frames per second
    private float frameTimer = 0f;
    private int currentFrame = 0;
    private bool isMoving = false;

    // Backup for main character walk frames (restored when switching to None)
    private Sprite[] mainWalkUp;
    private Sprite[] mainWalkDown;
    private Sprite[] mainWalkLeft;
    private Sprite[] mainWalkRight;
    private bool mainWalkSaved = false;

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
    public LayerMask interactableLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; 
        rb.freezeRotation = true;
    }

    void Start()
    {
        // Save main character walk frames
        if (walkUp != null && walkUp.Length > 0)
        {
            mainWalkUp = walkUp;
            mainWalkDown = walkDown;
            mainWalkLeft = walkLeft;
            mainWalkRight = walkRight;
            mainWalkSaved = true;
        }
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
        }

        if (!isDashing && !isInWindyZone)
        {
            moveInput = new Vector2(x, y).normalized;
        }
        else if (isInWindyZone && !isDashing)
        {
            moveInput = Vector2.zero;
        }

        // Track facing direction
        if (moveInput != Vector2.zero)
        {
            lastDirection = moveInput;
            isMoving = true;
        }
        else
        {
            isMoving = false;
            currentFrame = 0;
            frameTimer = 0f;
        }

        UpdateSprite();
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        if (isInWindyZone)
        {
            rb.linearVelocity = windVelocity;
        }
        else
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }

    private System.Collections.IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;
        
        Vector2 dashDirection = moveInput != Vector2.zero ? moveInput : new Vector2(1, 0);
        rb.linearVelocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

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

    void UpdateSprite()
    {
        if (spriteRenderer == null) return;

        // Get the right set of walk frames based on direction
        Sprite[] currentWalkFrames = null;
        Sprite idleSprite = null;

        if (Mathf.Abs(lastDirection.x) > Mathf.Abs(lastDirection.y))
        {
            if (lastDirection.x > 0)
            {
                currentWalkFrames = walkRight;
                idleSprite = spriteRight;
            }
            else
            {
                currentWalkFrames = walkLeft;
                idleSprite = spriteLeft;
            }
        }
        else
        {
            if (lastDirection.y > 0)
            {
                currentWalkFrames = walkUp;
                idleSprite = spriteUp;
            }
            else
            {
                currentWalkFrames = walkDown;
                idleSprite = spriteDown;
            }
        }

        // If moving AND we have walk frames, animate
        if (isMoving && currentWalkFrames != null && currentWalkFrames.Length > 0)
        {
            frameTimer += Time.deltaTime;

            if (frameTimer >= 1f / frameRate)
            {
                frameTimer = 0f;
                currentFrame = (currentFrame + 1) % currentWalkFrames.Length;
            }

            spriteRenderer.sprite = currentWalkFrames[currentFrame];
        }
        else
        {
            // Idle — show the idle sprite
            if (idleSprite != null)
            {
                spriteRenderer.sprite = idleSprite;
            }
        }
    }

    // Called by PersonalityManager when switching personalities
    public void SetSprites(Sprite up, Sprite down, Sprite left, Sprite right)
    {
        spriteUp = up;
        spriteDown = down;
        spriteLeft = left;
        spriteRight = right;
        UpdateSprite();
    }

    // Called by PersonalityManager to set walk frames (null = no walk animation)
    public void SetWalkSprites(Sprite[] up, Sprite[] down, Sprite[] left, Sprite[] right)
    {
        walkUp = up;
        walkDown = down;
        walkLeft = left;
        walkRight = right;
        currentFrame = 0;
        frameTimer = 0f;
    }

    // Restore main character walk frames
    public void RestoreMainWalkSprites()
    {
        if (mainWalkSaved)
        {
            walkUp = mainWalkUp;
            walkDown = mainWalkDown;
            walkLeft = mainWalkLeft;
            walkRight = mainWalkRight;
        }
    }

    // Clear walk frames (personality forms without walk animation)
    public void ClearWalkSprites()
    {
        walkUp = null;
        walkDown = null;
        walkLeft = null;
        walkRight = null;
        currentFrame = 0;
        frameTimer = 0f;
    }
}