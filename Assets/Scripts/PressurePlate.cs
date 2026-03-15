using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Visual Feedback")]
    public Color unpressedColor = new Color(0f, 0.4f, 0.3f, 1f);  // Dark teal
    public Color pressedColor = new Color(0f, 1f, 0.7f, 1f);      // Bright jade

    [HideInInspector]
    public bool isPressed = false;

    private SpriteRenderer spriteRenderer;
    private int objectsOnPlate = 0; // Track how many things are standing on it

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = unpressedColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Accept the Player or a Pushable boulder
        if (other.CompareTag("Player") || other.CompareTag("Pushable"))
        {
            objectsOnPlate++;
            isPressed = true;
            spriteRenderer.color = pressedColor;
            Debug.Log(gameObject.name + " is pressed!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Pushable"))
        {
            objectsOnPlate--;

            if (objectsOnPlate <= 0)
            {
                objectsOnPlate = 0;
                isPressed = false;
                spriteRenderer.color = unpressedColor;
                Debug.Log(gameObject.name + " is released.");
            }
        }
    }
}
