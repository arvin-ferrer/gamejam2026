using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class OpeningSequence : MonoBehaviour
{
    public CinemachineCamera keyCamera; 
    public string[] introLines; 

    void Start()
    {
        keyCamera.Priority = 5; 
        StartCoroutine(StartIntro());
    }

    IEnumerator StartIntro()
    {
        yield return new WaitForSeconds(0.1f);

        DialogueManager dm = FindFirstObjectByType<DialogueManager>();

        if (dm == null) { yield break; }

        dm.ShowMemory(introLines);

        while (dm.GetCurrentIndex() < 1) 
        {
            yield return null;
        }

        Debug.Log("BOOSTING CAMERA PRIORITY NOW!");
        keyCamera.Priority = 20; 

        while (dm.dialogueBox.activeInHierarchy)
        {
            yield return null; 
        }

        keyCamera.Priority = 0; 
    }
}