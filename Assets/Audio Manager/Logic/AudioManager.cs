using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Private Member -> Music Source
    [SerializeField] private AudioSource _musicSource;

    // Private Member -> Foley Source
    [SerializeField] private AudioSource _foleySource;

    // Private Mmeber -> Effects Source
    [SerializeField] private AudioSource _effectsSource;



    private void Awake()
    {
        // Ensure singleton pattern
        SingletonCheck();

        // Validate Source´s
        ValidateSources();
    }

    #region Singleton Pattern
    // Static Access (Can only be set from inside the class) -> Property
    public static AudioManager Instance { get; private set; }

    /// <summary>
    /// This function is used to make sure that this
    /// class follows the singleton pattern. Should be called in Awake.
    /// </summary>
    private void SingletonCheck()
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
    #endregion

    #region Source Management

    /// <summary>
    /// This function makes sure that all source´s are valid.
    /// Invalid sources will be replaced by a new source.
    /// </summary>
    private void ValidateSources()
    {
        // 1. Make sure music source valid
        if(_musicSource is null)
        {
            _musicSource = this.gameObject.AddComponent<AudioSource>();
        }

        // 2. Make sure foley source valid
        if(_foleySource is null)
        {
            _foleySource = this.gameObject.AddComponent<AudioSource>();
        }

        // 3. Make sure effects source valid
        if(_effectsSource is null)
        {
            _effectsSource = this.gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Checks if the music source is valid.
    /// </summary>
    /// <returns>Returns result as boolean.</returns>
    private bool MusicSourceValid()
    {
        return _musicSource is not null;
    }

    /// <summary>
    /// Checks if the music source is valid.
    /// </summary>
    /// <returns>Returns result as boolean.</returns>
    private bool FoleySourceValid()
    {
        return _foleySource is not null;
    }

    /// <summary>
    /// Checks if the music source is valid.
    /// </summary>
    /// <returns>Returns result as boolean.</returns>
    private bool EffectSourceValid()
    {
        return _effectsSource is not null;
    }
    #endregion
}
