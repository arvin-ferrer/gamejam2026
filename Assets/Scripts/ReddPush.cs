using UnityEngine;

public class ReddPush : MonoBehaviour
{
    [Header("Status")]
    public bool isRedd = false; // The master "Transformation" variable
    public Color reddColor = Color.red; // The color he turns into
    
    [Header("Push Settings")]
    public float pushPower = 2.0f;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // This is the function the Fragment will call
    public void TransformToRedd()
    {
        isRedd = true;
        if (sr != null) sr.color = reddColor; // Turn the square Red!
        Debug.Log("TRANSFORMED: Redd has regained his color and strength.");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Only push if transformed!
        if (!isRedd) return;

        if (collision.gameObject.CompareTag("Pushable"))
        {
            Rigidbody2D rb = collision.collider.attachedRigidbody;
            if (rb != null)
            {
                Vector2 forceDirection = (collision.gameObject.transform.position - transform.position).normalized;
                rb.linearVelocity = forceDirection * pushPower;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            Rigidbody2D rb = collision.collider.attachedRigidbody;
            if (rb != null) rb.linearVelocity = Vector2.zero;
        }
    }
}