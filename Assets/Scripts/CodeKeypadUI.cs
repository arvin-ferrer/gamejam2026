using UnityEngine;
using TMPro;

public class CodeKeypadUI : MonoBehaviour
{
    [Header("Settings")]
    public string correctCode = "369";
    public TextMeshProUGUI inputDisplay;
    
    private string currentEntry = "";
    private LockController targetLock;

    // This is called by the Lock to tell the UI which door it belongs to
    public void SetTargetLock(LockController lockObj) => targetLock = lockObj;

    // 1. Linked to Buttons 0-9
    public void PressButton(string value)
    {
        if (currentEntry.Length < 3) 
        {
            currentEntry += value;
            inputDisplay.text = currentEntry;
        }

        // Automatic check once 3 digits are reached
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

    // 2. Linked to the "X" Button
    public void DeleteLastNumber()
    {
        if (currentEntry.Length > 0)
        {
            currentEntry = currentEntry.Substring(0, currentEntry.Length - 1);
            inputDisplay.text = currentEntry.Length == 0 ? "---" : currentEntry;
        }
    }
    
    // 3. Linked to the "Close" Button
    public void CloseKeypad()
    {
        currentEntry = ""; 
        inputDisplay.text = "---";
        gameObject.SetActive(false);
        Time.timeScale = 1f; // Unpause the game
    }
}