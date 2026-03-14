using UnityEngine;

public class ReddPush : MonoBehaviour
{
    [Header("Push Settings")]
    public float pushPower = 2.0f;
    public bool canPush = false; // This starts FALSE until you get the fragment

    // This runs when you are physically touching the boulder
    private void OnCollisionStay2D(Collision2D collision)
    {
        // If we haven't found the fragment yet, we are too weak!
        if (!canPush) return;

        // Check if the thing we hit is tagged as 'Pushable'
        if (collision.gameObject.CompareTag("Pushable"))
        {
            Rigidbody2D rb = collision.collider.attachedRigidbody;

            if (rb != null)
            {
                // Calculate direction from Player to Boulder
                Vector2 forceDirection = collision.gameObject.transform.position - transform.position;
                forceDirection.Normalize();

                // Apply the movement to the boulder
                rb.linearVelocity = forceDirection * pushPower;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // When you stop touching it, make the boulder stop moving instantly
        if (collision.gameObject.CompareTag("Pushable"))
        {
            Rigidbody2D rb = collision.collider.attachedRigidbody;
            if (rb != null) rb.linearVelocity = Vector2.zero;
        }
    }
}