using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI textDisplay;
    public float typingSpeed = 0.05f;

    private string[] currentStory;
    private int currentIndex = 0;

    void Start()
    {
        if (dialogueBox != null) dialogueBox.SetActive(false);
    }

    // UPDATED: This now accepts string[] (the list of sentences)
    public void ShowMemory(string[] storyLines)
    {
        if (dialogueBox != null)
        {
            currentStory = storyLines;
            currentIndex = 0;
            dialogueBox.SetActive(true);
            
            StopAllCoroutines(); 
            StartCoroutine(TypeMessage(currentStory[currentIndex]));
            
            Time.timeScale = 0f; // Freeze game
        }
    }

    void Update()
    {
        // Press Space to go to the next line of the story
        if (dialogueBox.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            if (textDisplay.text == currentStory[currentIndex]) 
            {
                NextLine();
            }
        }
    }

    void NextLine()
    {
        currentIndex++;
        if (currentIndex < currentStory.Length)
        {
            StartCoroutine(TypeMessage(currentStory[currentIndex]));
        }
        else
        {
            CloseDialogue();
        }
    }
    public int GetCurrentIndex()
    {
        // Make sure 'currentIndex' is the name of your int variable in DialogueManager
        return currentIndex; 
    }
    public void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        Time.timeScale = 1f; // Unfreeze game
    }
    IEnumerator TypeMessage(string message)
        {
            textDisplay.text = ""; 
            foreach (char letter in message.ToCharArray())
            {
                textDisplay.text += letter;
                // Use Realtime so it types while Time.timeScale is 0
                yield return new WaitForSecondsRealtime(typingSpeed); 
            }
        }
}