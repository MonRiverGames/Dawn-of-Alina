using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableZach : MonoBehaviour
{
    //Message displayed to player when looking at and interactable obj
    [SerializeField]
    public string promptMessage;
    public bool useEvents;
    
    //Will be called by player
    public void BaseInteract() 
    {
        if (useEvents)
        {
            GetComponent<InteractionEvent>().onInteract.Invoke();
        }
        Interact();
    }
    
    public virtual void Interact() 
    { 
        //No code is written in this function
        //This is a template that the subclasses will overwrite
    }

    
}
