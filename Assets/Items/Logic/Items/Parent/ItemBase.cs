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
    protected ItemEvents _itemEvents { get; private set; }

    // Event -> Called when the script is loaded
    private void Awake()
    {
        // Get the player object
        GetPlayer();
    }

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

    // Called when the script is loaded. Use to get the player obj
    private void GetPlayer()
    {
        // Find the player (Remove item if invalid)
        GameObject _player = GameObject.FindWithTag("Player");
        if(_player is null)
        {
            // Deytroy item
            Destroy(this.gameObject);
            return;
        }

        // Fint the items event script (Remove item if invalid)
        _itemEvents = _player.GetComponent<ItemEvents>();
        if(_itemEvents is null)
        {
            // Deytroy item
            Destroy(this.gameObject);
            return;
        }
    }

    // Abstract Function -> Called when the item is triggered (Must be implemented by children)
    protected abstract void OnItemTrigger(GameObject triggerer);
}
