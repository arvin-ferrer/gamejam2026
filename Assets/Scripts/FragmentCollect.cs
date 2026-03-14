using UnityEngine;

public class FragmentCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the thing that touched the fragment is the Player
        if (other.CompareTag("Player"))
        {
            // 1. Find the ReddPush script on the player
            ReddPush pushScript = other.GetComponent<ReddPush>();

            if (pushScript != null)
            {
                // 2. Turn on the ability!
                pushScript.canPush = true;
                Debug.Log("STRENGTH RESTORED: You can now push boulders.");
            }

            // 3. Optional: Play a sound or show a dialogue message here
            
            // 4. Destroy the fragment so it disappears
            Destroy(gameObject);
        }
    }
}