using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    /// Rules
    /// -> Must be on sprite object
    /// -> Sprite Object must have collider
    /// -> Collider must be set to be trigger


    // Called whenerver something collides with the item
    private void OnTriggerEnter(Collider other)
    {
        // is player?
        if(other.gameObject.CompareTag("Player"))
        {
            // Call trigger event
            OnItemTrigger(other.gameObject);
        }
    }

    // Abstract Function -> Called when the item is triggered (Must be implemented by children)
    protected abstract void OnItemTrigger(GameObject triggerer);
}
