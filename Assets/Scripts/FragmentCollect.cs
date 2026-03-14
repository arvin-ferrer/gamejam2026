using UnityEngine;

public class FragmentCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ReddPush pushScript = other.GetComponent<ReddPush>();

            if (pushScript != null)
            {
                // Call the transformation function
                pushScript.TransformToRedd();
            }

            // You could add a particle effect here for the "Pop" of color!
            Destroy(gameObject);
        }
    }
}