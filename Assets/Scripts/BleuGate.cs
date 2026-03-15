using UnityEngine;

public class BleuGate : MonoBehaviour
{
    [Header("Settings")]
    public string requiredKey = "Bleu";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerInventory.Instance.HasKey(requiredKey))
            {
                OpenGate();
            }
            else
            {
                string[] hint = { "This gate requires the " + requiredKey + " key to open." };
                Object.FindFirstObjectByType<DialogueManager>().ShowMemory(hint);
            }
        }
    }

    void OpenGate()
    {
        Debug.Log("Bleu's path is open!");
        Destroy(gameObject);
    }
}
