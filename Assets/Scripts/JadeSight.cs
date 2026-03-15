using UnityEngine;
using System.Collections;

public class JadeSight : MonoBehaviour
{
    [Header("Settings")]
    public float duration = 4f;
    public float cooldown = 5f;
    public float revealRadius = 8f; // How close the player must be to reveal hidden objects

    [Header("Status")]
    public static bool isActive = false;
    private bool canActivate = true;

    void Update()
    {
        if (MemoryState.Instance == null) return;
        if (MemoryState.Instance.currentPersonality != MemoryState.Personality.Jade) return;

        if (Input.GetKeyDown(KeyCode.F) && canActivate && !isActive)
        {
            StartCoroutine(ActivateSight());
        }
    }

    IEnumerator ActivateSight()
    {
        canActivate = false;
        isActive = true;

        Debug.Log("Jade Sight Activated!");

        yield return new WaitForSeconds(duration);

        isActive = false;
        Debug.Log("Jade Sight Faded.");

        yield return new WaitForSeconds(cooldown);

        canActivate = true;
        Debug.Log("Jade Sight Ready.");
    }
}
