using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class OpeningSequence : MonoBehaviour
{
    public CinemachineCamera keyCamera; 
    public string[] introLines; 

    void Start()
    {
        // 1. Force Key Camera to be LOWER than the player at the very start
        keyCamera.Priority = 5; 
        StartCoroutine(StartIntro());
    }

    IEnumerator StartIntro()
    {
        yield return new WaitForSeconds(0.1f);

        DialogueManager dm = FindFirstObjectByType<DialogueManager>();

        if (dm == null) { yield break; }

        // Start the dialogue (Camera is on Player because 10 > 5)
        dm.ShowMemory(introLines);

        // 2. Wait until the player hits the second line
        while (dm.GetCurrentIndex() < 1) 
        {
            yield return null;
        }

        // 3. BOOST to win the war (20 > 10)
        Debug.Log("BOOSTING CAMERA PRIORITY NOW!");
        keyCamera.Priority = 20; 

        // 4. Wait until dialogue is closed
        while (dm.dialogueBox.activeInHierarchy)
        {
            yield return null; 
        }

        // 5. Drop back to 0 so Player wins again (10 > 0)
        keyCamera.Priority = 0; 
    }
}