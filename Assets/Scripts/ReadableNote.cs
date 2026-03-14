using UnityEngine;
using TMPro; // Add this for TextMeshPro

public class ReadableNote : MonoBehaviour
{
    [Header("UI Prompts")]
    public GameObject interactPrompt; // Drag your 'InteractionPrompt' object here
    
    [Header("Note Content")]
    [TextArea(3, 10)]
    public string[] noteContent;
    
    private bool isPlayerNearby = false;
    private DialogueManager dm;

    void Start()
    {
        dm = FindFirstObjectByType<DialogueManager>();
        if(interactPrompt != null) interactPrompt.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (dm != null)
            {
                // Hide prompt when reading
                interactPrompt.SetActive(false); 
                dm.ShowMemory(noteContent);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if(interactPrompt != null) interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if(interactPrompt != null) interactPrompt.SetActive(false);
        }
    }
}