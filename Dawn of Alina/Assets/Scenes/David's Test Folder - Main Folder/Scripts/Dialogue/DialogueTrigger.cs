using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public GameObject DialogueBox;
    public GameObject physicalBox;
    public bool firstTime;

    void Start()
    {
        DialogueBox.SetActive(false);
        firstTime = true;
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player" && firstTime == true)
        {
            Debug.Log("Yussss");

            DialogueBox.SetActive(true);
            firstTime = false;

            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }

    void OnTriggerExit(Collider player)
    {
        Destroy(physicalBox);
    }


    public void startDialogue()
    {
        DialogueBox.SetActive(true);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    
    
}
