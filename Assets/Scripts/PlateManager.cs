using UnityEngine;

public class PlateManager : MonoBehaviour
{
    [Header("Plates")]
    public PressurePlate[] plates; // Drag PlateA, PlateB, PlateC here

    [Header("Door")]
    public GameObject jadeDoor; // Drag the JadeDoor here

    [Header("Feedback")]
    [TextArea]
    public string[] successDialogue = { "The ground trembles... the ancient lock disengages." };

    private bool doorOpened = false;

    void Update()
    {
        if (doorOpened) return;

        // Check if ALL plates are pressed
        bool allPressed = true;

        for (int i = 0; i < plates.Length; i++)
        {
            if (!plates[i].isPressed)
            {
                allPressed = false;
                break;
            }
        }

        if (allPressed)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        doorOpened = true;
        Debug.Log("All plates pressed! Jade door opens!");

        // Show success dialogue
        DialogueManager dm = Object.FindFirstObjectByType<DialogueManager>();
        if (dm != null)
        {
            dm.ShowMemory(successDialogue);
        }

        // Destroy the door
        if (jadeDoor != null)
        {
            Destroy(jadeDoor);
        }
    }
}
