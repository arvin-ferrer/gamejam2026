using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Objects")]
    public GameObject dialogueBox;
    public TextMeshProUGUI textDisplay;
    
    [Header("Prompts")]
    public GameObject spacePrompt;    // The "[Space]" text inside the box
    public GameObject interactPrompt; // The universal "Press [E]" text
    
    [Header("Settings")]
    public float typingSpeed = 0.05f;

    private string[] currentStory;
    private int currentIndex = 0;
    private bool isTyping = false; // Prevents skipping while typing
    private System.Action onDialogueComplete;

    void Start()
    {
        if (dialogueBox != null) dialogueBox.SetActive(false);
        if (spacePrompt != null) spacePrompt.SetActive(false);
        if (interactPrompt != null) interactPrompt.SetActive(false);
    }

    public void ShowMemory(string[] storyLines, System.Action onComplete = null)
    {
        if (dialogueBox != null)
        {
            currentStory = storyLines;
            currentIndex = 0;
            onDialogueComplete = onComplete;
            dialogueBox.SetActive(true);
            
            // Hide prompts when starting a new dialogue
            if (interactPrompt != null) interactPrompt.SetActive(false);
            if (spacePrompt != null) spacePrompt.SetActive(false);

            StopAllCoroutines(); 
            StartCoroutine(TypeMessage(currentStory[currentIndex]));
            
            Time.timeScale = 0f; 
        }
    }

    void Update()
    {
        // Only allow progression if the box is open and we aren't currently typing
        if (dialogueBox.activeSelf && Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            NextLine();
        }
    }

    void NextLine()
    {
        currentIndex++;
        if (currentIndex < currentStory.Length)
        {
            if (spacePrompt != null) spacePrompt.SetActive(false); // Hide while typing next
            StartCoroutine(TypeMessage(currentStory[currentIndex]));
        }
        else
        {
            CloseDialogue();
        }
    }

    public void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        if (spacePrompt != null) spacePrompt.SetActive(false);
        Time.timeScale = 1f; 

        if (onDialogueComplete != null)
        {
            var temp = onDialogueComplete;
            onDialogueComplete = null;
            temp.Invoke();
        }
    }

    // Call this from scripts like 'ReadableNote' when player enters/exits trigger
    public void ToggleInteractPrompt(bool show)
    {
        // Don't show the 'E' prompt if the dialogue box is already open
        if (dialogueBox.activeSelf && show) return; 

        if (interactPrompt != null) interactPrompt.SetActive(show);
    }

    public int GetCurrentIndex() => currentIndex;

    IEnumerator TypeMessage(string message)
    {
        isTyping = true;
        textDisplay.text = ""; 
        
        foreach (char letter in message.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed); 
        }

        isTyping = false;
        
        // Show the Space prompt only when the full line is visible
        if (spacePrompt != null) spacePrompt.SetActive(true);
    }
}