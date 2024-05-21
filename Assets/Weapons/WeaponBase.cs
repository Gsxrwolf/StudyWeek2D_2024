using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    // Editor Setting -> Damage
    [SerializeField] private int _damage = 1;

    // Editor Setting -> Use area damage
    [SerializeField] private bool _areaDamage = false;




    // Property -> Weapon Damage (read only -> can only be set within class) (def. 1)
    public int WeaponDamage { get; protected set; }

    // Property -> Area Damage (read only -> can only be set within class) (def. false)
    public bool AreaDamage { get; protected set; }




    /// <summary>
    /// Can be overwritten. Use this function to set the weapons values.
    /// </summary>
    protected virtual void Initialize()
    {
        WeaponDamage = _damage;
        AreaDamage = _areaDamage;
    }

    // Called whenever the script is loaded
    private void Awake()
    {
        // Call init logic once the script is loaded
        Initialize();
    }
}
