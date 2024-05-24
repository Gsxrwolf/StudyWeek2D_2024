using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Health,
    HealthTimer,
    DoubleDamage,
    ExtraSpeed,
    ExtraJumpPower
}

[System.Serializable]
public struct SpriteMapping
{
    public ItemType Type;
    public Sprite Sprite;
}

public class Item : ItemBase
{
    // Editor Setting -> Defines the type of the item
    [SerializeField] private ItemType _type = ItemType.None;

    // Editor Setting -> The items sprite renderer
    [SerializeField] private SpriteRenderer _spriteRenderer;

    // Editor Settings -> Mapping that holds the sprite for each type
    [SerializeField] private List<SpriteMapping> _spriteMapping = new List<SpriteMapping>();

    /// <summary>
    /// Called when the item is loaded. Will perform some checkups
    /// and then initialize the item.
    /// </summary>
    private void Start()
    {
        // End function -> Make sure that the item type is valid
        if(_type == ItemType.None)
        {
            Debug.LogWarning("Item type is set to NONE!");
            Destroy(gameObject);
            return;
        }

        // End function -> Make sure the sprite renderer is valid
        if(_spriteRenderer == null)
        {
            Debug.LogWarning("Item has no Sprite Renderer assigned!");
            Destroy(gameObject);
            return;
        }

        // Set the items sprite based on type
        SetItemSprite();
    }

    /// <summary>
    ///  Overriden abstract function. Will be fired when the player
    ///  interacted with this item.
    /// </summary>
    /// <param name="triggerer"></param>
    protected override void OnItemTrigger(GameObject triggerer)
    {

        // Find the items event script (Remove item if invalid)
        _itemEvents = triggerer.GetComponent<ItemEvents>();

        if (_itemEvents is null)
        {
            // Deytroy item
            Destroy(this.gameObject);
            Debug.Log(triggerer.name);
            return;
        }

        // Call event based on item type
        switch (_type)
        {
            case ItemType.Health:
                _itemEvents.AddHealth(triggerer);
                break;

            case ItemType.HealthTimer:
                _itemEvents.AddHealthOverTime(triggerer);
                break;

            case ItemType.DoubleDamage:
                _itemEvents.DoubleDamage(triggerer);
                break;

            case ItemType.ExtraSpeed:
                _itemEvents.ExtraSpeed(triggerer);
                break;

            case ItemType.ExtraJumpPower:
                _itemEvents.ExtraJumpPower(triggerer);
                break;

            default:
                Debug.LogWarning("Item has no valid type!");
                Destroy(gameObject);
                break;
        }


        gameObject.SetActive(false);
    }

    /// <summary>
    /// Will be called on event begin play. Will set
    /// the item sprite based on items type.
    /// </summary>
    private void SetItemSprite()
    {
        // Get the sprite from map based on items type
        Sprite sprite = _spriteMapping.Find(mapping => mapping.Type == _type).Sprite;

        // Set the sprite in sprite renderer
        _spriteRenderer.sprite = sprite;
    }
}
