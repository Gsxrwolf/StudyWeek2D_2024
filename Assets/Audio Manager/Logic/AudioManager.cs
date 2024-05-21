using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Editor Settings
    [Header("Background Music")]

    // Private Member -> Should play bg music
    [SerializeField] private bool _backgroundMusic = true;

    // Private Member -> List of background tracks
    [SerializeField] private List<AudioClip> _backgroundTracks = new List<AudioClip>();

    // Private Memeber -> Pause between tracks
    [SerializeField] private int _loopPause = 1;


    [Space()]


    [Header("Audio Source")]

    // Private Member -> Music Source
    [SerializeField] private AudioSource _musicSource;

    // Private Member -> Foley Source
    [SerializeField] private AudioSource _foleySource;

    // Private Mmeber -> Effects Source
    [SerializeField] private AudioSource _effectsSource;

    #endregion



    private void Awake()
    {
        // Ensure singleton pattern
        SingletonCheck();

        // Validate Source´s
        ValidateSources();
    }

    private void Start()
    {
        // Start Background Music
        StartCoroutine(BackgroundMusicRoutine());
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

    #region Public Functions

    /// <summary>
    /// Allows and starts background music.
    /// </summary>
    public void StartBackgroundMusic()
    {
        // Endable background music
        _backgroundMusic = true;

        // Start routine
        StartCoroutine(BackgroundMusicRoutine());
    }

    /// <summary>
    /// Stops the background music.
    /// </summary>
    /// <param name="finish">Should wait for current track to end?</param>
    public void StopBackgroundMusic(bool finish = false)
    {
        // Stop the routine
        StopCoroutine(BackgroundMusicRoutine());

        // Make sure source does not loop
        _musicSource.loop = false;

        // Disable background music
        _backgroundMusic = false;

        
        if(finish is false)
        {
            // Stop the source immediatly
            _musicSource.Stop();

            // End function
            return;
        }
    }

    #endregion

    #region Routines
    /// <summary>
    /// Runs in the "background" (No multithreading) and manages the
    /// background music. Randomly selects next track, makes pauses etc ...
    /// </summary>
    /// <returns></returns>
    private IEnumerator BackgroundMusicRoutine()
    {
        // Local Memeber: Current Playing Track (Placeholder)
        AudioClip audioClip = null;

        // Loop -> While music should play and tracks are available
        while(_backgroundMusic && _backgroundTracks.Count > 0)
        {
            // Local Member -> Select Random Track
            AudioClip selection = _backgroundTracks[Random.Range(0, _backgroundTracks.Count - 1)];

            // End Loop -> if there are more than 2 tracks, end loop if selection is last played
            if(_backgroundTracks.Count >= 2 && selection == audioClip) { yield return null; }

            // Play new clip
            audioClip = selection;
            _musicSource.clip = audioClip;
            _musicSource.loop = false;
            _musicSource.Play();

            // Debug -> New track selected
            Debug.Log("Background Music: New track selected!");

            // Routine Return -> Wait for end of track (+ pause)
            yield return new WaitForSeconds(audioClip.length + _loopPause);
        }
    }

    #endregion
}
