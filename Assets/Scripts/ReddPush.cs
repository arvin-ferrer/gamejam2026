using UnityEngine;

public class ReddPush : MonoBehaviour
{
    public float pushPower = 2.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            Rigidbody2D boulderRb = collision.collider.attachedRigidbody;

            if (boulderRb != null)
            {
                // If we are REDD, make the boulder move
                if (MemoryState.Instance.currentPersonality == MemoryState.Personality.Redd)
                {
                    boulderRb.bodyType = RigidbodyType2D.Dynamic;
                }
                else
                {
                    // If we are NONE, turn the boulder into a brick wall
                    boulderRb.bodyType = RigidbodyType2D.Static;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Only apply force if we are currently Redd
        if (MemoryState.Instance.currentPersonality != MemoryState.Personality.Redd) return;

        if (collision.gameObject.CompareTag("Pushable"))
        {
            Rigidbody2D rb = collision.collider.attachedRigidbody;
            if (rb != null && rb.bodyType == RigidbodyType2D.Dynamic)
            {
                Vector2 forceDirection = (collision.gameObject.transform.position - transform.position).normalized;
                rb.linearVelocity = forceDirection * pushPower;
            }
        }
    }
}