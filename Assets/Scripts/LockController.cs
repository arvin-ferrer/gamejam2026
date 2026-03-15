using UnityEngine;

public class LockController : MonoBehaviour
{
    public string requiredKey = "Redd";
    public GameObject keypadUI; 

    public void Interact()
    {
        if (PlayerInventory.Instance.HasKey(requiredKey))
        {
            OpenKeypad();
        }
        else
        {
            string[] hint = { "The mechanism is locked. It needs a specific key to activate the keypad." };
            FindObjectOfType<DialogueManager>().ShowMemory(hint);
        }
    }

    void OpenKeypad()
    {
        keypadUI.SetActive(true);
        Time.timeScale = 0f; 
        
        keypadUI.GetComponent<CodeKeypadUI>().SetTargetLock(this);
    }

    public void OnCodeCorrect()
    {
        Debug.Log("Lock Disengaged!");
        
        // --- ADDED AUDIO CALL ---
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.gateOpenSound);
        }
        // -------------------------

        Time.timeScale = 1f;
        keypadUI.SetActive(false);
        
        gameObject.SetActive(false); 
    }
}