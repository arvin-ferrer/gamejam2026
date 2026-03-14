using UnityEngine;

public class LockController : MonoBehaviour
{
    public string requiredKey = "Redd";
    public GameObject keypadUI; // Drag your Keypad Canvas/Panel here

    // This is called by the Player's Interaction script (pressing E)
    public void Interact()
    {
        // 1. Check if the player actually found the key in the other areas
        if (PlayerInventory.Instance.HasKey(requiredKey))
        {
            OpenKeypad();
        }
        else
        {
            // 2. If no key, tell the player they need to go find it
            string[] hint = { "The mechanism is locked. It needs a specific key to activate the keypad." };
            FindObjectOfType<DialogueManager>().ShowMemory(hint);
        }
    }

    void OpenKeypad()
    {
        keypadUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game while typing the code
        
        // Tell the Keypad which Lock it's currently talking to
        keypadUI.GetComponent<CodeKeypadUI>().SetTargetLock(this);
    }

    public void OnCodeCorrect()
    {
        Debug.Log("Lock Disengaged!");
        Time.timeScale = 1f;
        keypadUI.SetActive(false);
        
        // Remove the lock so the player can reach the Fragment behind it
        gameObject.SetActive(false); 
    }
}