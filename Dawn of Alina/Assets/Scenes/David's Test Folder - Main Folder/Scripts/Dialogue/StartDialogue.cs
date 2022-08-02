using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public GameObject dialogueBackground;
    public GameObject uiObject;
    
    
    void Start()
    {
        dialogueBackground.SetActive(false);
        uiObject.SetActive(false);
    }

    void OnTriggerEnter (Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            dialogueBackground.SetActive(true);
            uiObject.SetActive(true);
        }
    }
   

}
