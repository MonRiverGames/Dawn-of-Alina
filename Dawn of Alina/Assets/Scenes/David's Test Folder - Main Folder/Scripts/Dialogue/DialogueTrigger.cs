using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public GameObject DialogueBox;

    void Start()
    {
        DialogueBox.SetActive(false);
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            Debug.Log("Yussss");

            DialogueBox.SetActive(true);

            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }

    public void startDialogue()
    {
        DialogueBox.SetActive(true);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    
    
}
