using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //Message displayed to player when looking at and interactable obj
    public string promptMessage;
    
    //Will be called by player
    public void BaseInteract() 
    {
        Interact();
    }
    
    protected virtual void Interact() 
    { 
        //No code is written in this function
        //This is a template that the subclasses will overwrite
    }

    
}
