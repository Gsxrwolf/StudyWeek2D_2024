using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ItemType
{
    None,
    Health,
    HealthTimer,
    DoubleDamage,
    ExtraSpeed,
    ExtraJumpPower
}

public class Item : ItemBase
{
    // Editor Setting -> Defines the type of the item
    [SerializeField] private ItemType _type = ItemType.None;

    protected override void OnItemTrigger(GameObject triggerer)
    {
        // Call event based on item type
        switch(_type)
        {
            case ItemType.None:
            Debug.LogWarning("Item type is set to NONE!");
            Destroy(gameObject);
            break;

            case ItemType.Health:
            _itemEvents.AddHealth();
            break;

            case ItemType.HealthTimer:
            _itemEvents.AddHealthOverTime();
            break;

            case ItemType.DoubleDamage:
            _itemEvents.DoubleDamage();
            break;

            case ItemType.ExtraSpeed:
            _itemEvents.ExtraSpeed();
            break;

            case ItemType.ExtraJumpPower:
            _itemEvents.ExtraJumpPower();
            break;

            default:
            Debug.LogWarning("Item has no valid type!");
            Destroy(gameObject);
            break;
        }
    }
}
