using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    // shows the message to the player when looking at interactable
    public string promptMessage;
    public void BaseInteract() {
        Interact();
    }
    protected virtual void Interact() {
        //we wont have any code here 
        //this is a template function to be overridden by our subclasses 
    }
}
