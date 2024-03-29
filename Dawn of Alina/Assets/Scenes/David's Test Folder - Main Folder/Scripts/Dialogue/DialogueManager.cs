using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Queue<string> sentences;

    public GameObject DialogueBox;
    public GameObject Crosshair;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        UnlockMouse();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        LockMouse();
        DialogueBox.SetActive(false);
        Debug.Log("End of conversation");
    }

    void UnlockMouse()
    {
        /* 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        */

        Cursor.lockState = CursorLockMode.Confined;
        Crosshair.SetActive(false);
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    void LockMouse()
    {
        /*
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        */

        Cursor.lockState = CursorLockMode.Locked;
        Crosshair.SetActive(true);
        Cursor.visible = false;
        Time.timeScale = 1;
    }

}
