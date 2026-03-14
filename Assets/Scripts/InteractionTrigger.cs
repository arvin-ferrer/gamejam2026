using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    private DialogueManager dm;
    private bool isPlayerNearby = false;

    void Start()
    {
        dm = Object.FindFirstObjectByType<DialogueManager>();
    }

    void Update()
    {
        // By adding this 'if', we are now USING the variable. 
        // Warning: GONE. Functionality: ADDED.
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player pressed E at the " + gameObject.name);
            
            // This is where the Keypad will open later.
            // For now, let's just hide the [E] prompt so it feels like you 'entered' the lock.
            if (dm != null) dm.ToggleInteractPrompt(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (dm != null) dm.ToggleInteractPrompt(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (dm != null) dm.ToggleInteractPrompt(false);
        }
    }
}