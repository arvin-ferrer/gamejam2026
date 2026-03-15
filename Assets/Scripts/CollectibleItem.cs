using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public enum ItemType { Key, Fragment }
    public ItemType type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayPickupSound();
            
            // Add your logic here (e.g., IncrementKeyCount())
            
            Destroy(gameObject); // Remove the item from the scene
        }
    }

    private void PlayPickupSound()
    {
        if (type == ItemType.Key)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.keySound);
        }
        else if (type == ItemType.Fragment)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.fragmentSound);
        }
    }
}