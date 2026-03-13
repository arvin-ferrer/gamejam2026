using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class MemoryManager : MonoBehaviour
{
    public Volume globalVolume;
    private ColorAdjustments colorAdjustments;

    void Start()
    {
        // Try to find the color settings in your Volume
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.saturation.value = -100f; // Start Grey
        }
    }

    // This is the function the RedKey will "Call"
    public void RestoreRedMemory()
    {
        StartCoroutine(FadeSaturation(0f, 2f));
        Debug.Log("Red Memory Processed by Manager!");
    }

    IEnumerator FadeSaturation(float targetValue, float duration)
    {
        float elapsed = 0;
        float startValue = colorAdjustments.saturation.value;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, targetValue, elapsed / duration);
            
            // This forces Unity to override the current value
            colorAdjustments.saturation.Override(newValue);
            
            yield return null;
        }
        
        // Final snap to ensure it's exactly the target
        colorAdjustments.saturation.Override(targetValue);
        Debug.Log("Color fully restored!");
    }
}