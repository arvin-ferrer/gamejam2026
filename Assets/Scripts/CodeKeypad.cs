using  UnityEngine;

public class CodeKeypad : MonoBehaviour
{
    public string requiredKey = "Redd";
    public GameObject keypadUI; 

    public void OpenKeypad()
    {
        
        if (PlayerInventory.Instance.HasKey(requiredKey))
        {
            keypadUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            string[] lines = { "The keypad is locked. I need to find the Red Key first." };
            FindObjectOfType<DialogueManager>().ShowMemory(lines);
        }
    }

    public void CodeCorrect()
    {
        keypadUI.SetActive(false);
        Time.timeScale = 1f;
        
        gameObject.SetActive(false); 
        Debug.Log("The lock clicks open!");
    }
}