using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnDialogueDelay : MonoBehaviour
{
    [SerializeField]
    private GameObject disembodiedVoice;

    void Start()
    {
        StartCoroutine(StartVoice(5));

        // Can't run the coroutine because disembodied voice isn't active but 

    }

 
    IEnumerator StartVoice(int secs)
    {
        yield return new WaitForSeconds(secs);
        GameObject.Destroy(disembodiedVoice);
    }

}
