using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class redKey : MonoBehaviour
{
    public Volume globalVolume; 
    private ColorAdjustments colorAdjustments;

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        // 1. Precise check for Global Volume
        if (globalVolume != null && globalVolume.profile.TryGet(out colorAdjustments))
        {
            StartCoroutine(FadeToColor());
        }

        // 2. Precise check for Dialogue Manager
        DialogueManager diag = FindObjectOfType<DialogueManager>();
        if (diag != null)
        {   
            Debug.LogError("Works?");
            diag.ShowMemory("I remember... this red was the warmth of home. I'm more than just a shape.");
        }
        else
        {
            Debug.LogError("PRECISION ERROR: DialogueManager script not found in scene!");
        }

        // 3. Precise Visual Handling
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
    IEnumerator FadeToColor()
    {
        Debug.Log("fading");
        
        float duration = 2.0f;
        float elapsed = 0;
        float startValue = colorAdjustments.saturation.value;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, 0f, elapsed / duration);
            colorAdjustments.saturation.Override(newValue);
            yield return null;
        }

        colorAdjustments.saturation.Override(0f);
        Debug.Log("SUCCESS: Color fully restored!");
        Destroy(gameObject);
    }
}