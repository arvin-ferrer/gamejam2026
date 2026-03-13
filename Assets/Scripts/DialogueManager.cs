using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI textDisplay;
    public float typingSpeed = 0.05f;

    void Start()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
    }
        void Update()
        {
            if (dialogueBox.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    CloseDialogue();
                }
            }
        }

    public void ShowMemory(string message)
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(true);
            
            StopAllCoroutines(); 
            StartCoroutine(TypeMessage(message));
            
            Time.timeScale = 0f; // Freeze game
            Debug.Log("Game Paused for Dialogue.");
        }
    }
    public void CloseDialogue()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
            
            textDisplay.text = "";

            Time.timeScale = 1f; 
            
            Debug.Log("Dialogue Closed. Game Resumed.");
        }
    }

    IEnumerator TypeMessage(string message)
    {
        textDisplay.text = ""; 
        foreach (char letter in message.ToCharArray())
        {
            textDisplay.text += letter;            
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }
 
}   