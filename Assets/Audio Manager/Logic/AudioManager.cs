using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Static Access (Can only be set from inside the class) -> Property
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        // Make sure this class is no duplicate
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        // If no duplicate -> Set instance to this class
        Instance = this;

        // Prevent this class from being destroyed
        DontDestroyOnLoad(this.gameObject);
    }
}
