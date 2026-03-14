using  UnityEngine;

public class CodeKeypad : MonoBehaviour
{
    public string requiredKey = "Redd";
    public GameObject keypadUI; 

    public void OpenKeypad()
    {
        
        // PRECISE CHECK: You can only try the code if you have the key!
        if (PlayerInventory.Instance.HasKey(requiredKey))
        {
            keypadUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            // Trigger a message saying "The keypad is rusted shut. I need a key."
            string[] lines = { "The keypad is locked. I need to find the Red Key first." };
            FindObjectOfType<DialogueManager>().ShowMemory(lines);
        }
    }

    // Call this from your UI "Submit" button when the code is correct
    public void CodeCorrect()
    {
        keypadUI.SetActive(false);
        Time.timeScale = 1f;
        
        // This opens the path to the Fragment
        gameObject.SetActive(false); 
        Debug.Log("The lock clicks open!");
    }
}