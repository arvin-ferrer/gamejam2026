using UnityEngine;
using TMPro;

public class CodeKeypadUI : MonoBehaviour
{
    [Header("Settings")]
    public string correctCode = "369";
    public TextMeshProUGUI inputDisplay;
    
    private string currentEntry = "";
    private LockController targetLock;

    public void SetTargetLock(LockController lockObj) => targetLock = lockObj;

    public void PressButton(string value)
    {
        if (currentEntry.Length < 3) 
        {
            currentEntry += value;
            inputDisplay.text = currentEntry;
        }

        if (currentEntry.Length == 3)
        {
            CheckCode();
        }
    }

    void CheckCode()
    {
        if (currentEntry == correctCode)
        {
            targetLock.OnCodeCorrect();
            currentEntry = ""; 
        }
        else
        {
            currentEntry = "";
            inputDisplay.text = "WRONG";
        }
    }

    public void DeleteLastNumber()
    {
        if (currentEntry.Length > 0)
        {
            currentEntry = currentEntry.Substring(0, currentEntry.Length - 1);
            inputDisplay.text = currentEntry.Length == 0 ? "---" : currentEntry;
        }
    }
    
    public void CloseKeypad()
    {
        currentEntry = ""; 
        inputDisplay.text = "---";
        gameObject.SetActive(false);
        Time.timeScale = 1f; // Unpause the game
    }
}