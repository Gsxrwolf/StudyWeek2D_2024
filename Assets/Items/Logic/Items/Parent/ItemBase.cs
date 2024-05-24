using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    /// Rules
    /// -> Must be on sprite object
    /// -> Sprite Object must have collider
    /// -> Collider must be set to be trigger


    // Protected Property -> The item events script contained on player
    protected ItemEvents _itemEvents { get; set; }

    // Called whenerver something collides with the item

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // is player?
        if (collider.gameObject.CompareTag("Player"))
        {
            // Call trigger event
            OnItemTrigger(collider.gameObject);
        }
    }

    // Abstract Function -> Called when the item is triggered (Must be implemented by children)
    protected abstract void OnItemTrigger(GameObject triggerer);
}
