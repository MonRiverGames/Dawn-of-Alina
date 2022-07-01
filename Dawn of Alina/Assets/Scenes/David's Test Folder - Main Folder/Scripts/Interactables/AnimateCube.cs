using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCube : Interactable
{
    Animator animator;
    private string startPrompt;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startPrompt = promptMessage;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Still"))
        {
            promptMessage = startPrompt;
        }
        else
        {
            promptMessage = "Animating..";
        }
    }

    protected override void Interact()
    {
        animator.Play("Spin");
        StartCoroutine(BackToStill());
    }

    IEnumerator BackToStill(){
        yield return new WaitForSeconds(5);
        animator.Play("Still");
    }

}
