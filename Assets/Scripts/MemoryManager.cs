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
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.saturation.value = -100f; // Start Grey
        }
    }

    public void RestoreRedMemory()
    {
        StartCoroutine(FadeSaturation(0f, 2f));
        Debug.Log("All Memories Processed by Manager!");
    }

    IEnumerator FadeSaturation(float targetValue, float duration)
    {
        float elapsed = 0;
        float startValue = colorAdjustments.saturation.value;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float newValue = Mathf.Lerp(startValue, targetValue, elapsed / duration);
            
            colorAdjustments.saturation.Override(newValue);
            
            yield return null;
        }
        
        colorAdjustments.saturation.Override(targetValue);
        Debug.Log("Color fully restored!");
    }

    public void ShowRestartPrompt()
    {
        StartCoroutine(WaitAndPromptRestartCoroutine());
    }

    IEnumerator WaitAndPromptRestartCoroutine()
    {
        // Wait 3 seconds in real time to let the color fade finish and be admired
        yield return new WaitForSecondsRealtime(3f);
        
        DialogueManager dm = FindFirstObjectByType<DialogueManager>();
        if (dm != null)
        {
            dm.ShowMemory(new string[] { "Would you like to try again?", "Press [Space] to restart." }, () => {
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            });
        }
    }
}