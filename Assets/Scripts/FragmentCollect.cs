using UnityEngine;

public class FragmentCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Mark Redd as unlocked in the global state
            MemoryState.Instance.reddUnlocked = true;

            // 2. Automatically transform the player into Redd right now
            PersonalityManager pm = other.GetComponent<PersonalityManager>();
            if (pm != null)
            {
                pm.TransformTo(MemoryState.Personality.Redd);
            }

            // Play the fragment pickup sound
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("Fragment Aquired");

            Destroy(gameObject);
        }
    }
}