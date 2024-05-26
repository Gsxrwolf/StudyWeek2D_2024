using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum holding all sound options.
/// For now placeholders
/// </summary>

public enum SoundType
{
    HandStamp,
    ButtonClick
}

public enum WeaponType
{
    Pan,
    Skateboard,
    Toy
}

public enum FoleyType
{
    Supermarket,
    SubUrb,
    SantaMonica
}

public enum BackgroundMusicType
{
    Chill,
    Rock
}


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



    [Space()]


    [Header("Sounds")]


    [SerializeField] private AudioClip _stamp;
    [SerializeField] private AudioClip _button;
    [SerializeField] private AudioClip _ingameMusic;
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _supermarketFoley;
    [SerializeField] private AudioClip _suburbFoley;
    [SerializeField] private AudioClip _pierFoley;
    [SerializeField] private List<AudioClip> _walkingSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _damageSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _karenSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _karenDamageSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _itemSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _panSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _skateboardSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _toySounds = new List<AudioClip>();

    #endregion

    // When script is loaded -> Call init logic
    private void Awake()
    {
        // Ensure singleton pattern
        SingletonCheck();

        // Validate Source´s
        ValidateSources();
    }

    // First Frame -> Start internal logic
    private void Start()
    {
        // Start Background Music
        //StartCoroutine(BackgroundMusicRoutine());
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
            _musicSource = gameObject.AddComponent<AudioSource>();
        }

        // 2. Make sure foley source valid
        if(_foleySource is null)
        {
            _foleySource = gameObject.AddComponent<AudioSource>();
        }

        // 3. Make sure effects source valid
        if(_effectsSource is null)
        {
            _effectsSource = gameObject.AddComponent<AudioSource>();
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
    /// Plays a single sound on a global scale (2D)
    /// </summary>
    /// <param name="sound">Which sound to play</param>
    public void PlaySound2D(AudioClip _clip)
    {
        // End function -> Make sure the effects source is valid
        if(EffectSourceValid() is false)
        {
            return;
        }

        // Make sure sound is valid
        if(_clip is null)
        {
            Debug.Log("Audio Manager: Sound selection invalid!");
            return;
        }

        // Play sound
        _effectsSource.clip = _clip;
        _effectsSource.loop = false;
        _effectsSource.Play();
    }

    /// <summary>
    /// Plays a single sound.
    /// </summary>
    /// <param name="sound">Which sound to play</param>
    /// <param name="volume">Volume multiplier (0 -> 1)</param>
    public void PlaySingleSound(AudioClip _clip, float volume = 1.0f)
    {
        // End function -> Make sure the source is valid
        if(EffectSourceValid() is false)
        {
            return;
        }

        // Play single audio
        _effectsSource.PlayOneShot(_clip, volume);
        _effectsSource.loop = false;
    }

    /// <summary>
    /// Plays a single sound at given location.
    /// </summary>
    /// <param name="sound">Which sound to play</param>
    /// <param name="location">Location to play at</param>
    /// <param name="volume">Volume multiplier (0 -> 1)</param>
    public void PlaySoundAtLocation(AudioClip _clip, Vector3 location, float volume = 1.0f)
    {
        // Play sound at location
        AudioSource.PlayClipAtPoint(_clip, location, volume);
    }

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


    public void PlayUISound(SoundType type)
    {
        switch(type)
        {
            case SoundType.HandStamp:
            PlaySingleSound(_stamp);
            break;

            case SoundType.ButtonClick:
            PlaySingleSound(_button);
            break;
        }
    }

    public void PlayKarenSound(GameObject karen, bool isDamage = false)
    {
        AudioClip _clip;

        if(isDamage)
        {
            _clip = _karenDamageSounds[Random.Range(0, _karenDamageSounds.Count - 1)];
            PlaySoundAtLocation(_clip, karen.transform.position);
            return;
        }


        _clip = _karenSounds[Random.Range(0, _karenSounds.Count - 1)];
        PlaySoundAtLocation(_clip, karen.transform.position);
    }

    public void PlayWalkingSound(GameObject player)
    {
        AudioClip _clip = _walkingSounds[Random.Range(0, _walkingSounds.Count - 1)];
        PlaySoundAtLocation(_clip, player.transform.position);
    }

    public void PlayDamageSound(GameObject damaged)
    {
        AudioClip _clip = _damageSounds[Random.Range(0, _damageSounds.Count - 1)];
        PlaySoundAtLocation(_clip, damaged.transform.position);
    }

    public void PlayWeaponSound(WeaponType type)
    {
        List<AudioClip> sounds = new List<AudioClip>();

        switch(type)
        {
            case WeaponType.Pan:
            sounds = _panSounds;
            break;

            case WeaponType.Skateboard:
            sounds = _skateboardSounds;
            break;

            case WeaponType.Toy:
            sounds = _toySounds;
            break;
        }

        AudioClip selection = sounds[Random.Range(0, sounds.Count - 1)];
        PlaySingleSound(selection);
    }

    public void PlayItemSound()
    {
        AudioClip _clip = _itemSounds[Random.Range(0, _itemSounds.Count - 1)];
        PlaySingleSound(_clip);
    }

    public void SetBackgroundFoley(FoleyType type)
    {
        switch(type)
        {
            case FoleyType.Supermarket:
            _foleySource.clip = _supermarketFoley;
            break;

            case FoleyType.SubUrb:
            _foleySource.clip = _suburbFoley;
            break;

            case FoleyType.SantaMonica:
            _foleySource.clip = _pierFoley;
            break;
        }

        _foleySource.loop = true;
        _foleySource.Play();
    }

    public void SetBackgroundMusic(BackgroundMusicType type)
    {
        switch (type)
        {
            case BackgroundMusicType.Chill:
            _musicSource.clip = _ingameMusic;
            break;

            case BackgroundMusicType.Rock:
            _musicSource.clip = _menuMusic;
            break;
        }

        _musicSource.loop = true;
        _musicSource.Play();
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
        while(_backgroundMusic && _backgroundTracks.Count > 0 && MusicSourceValid())
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
            Debug.Log("Audio Manager: New track selected!");

            // Routine Return -> Wait for end of track (+ pause)
            yield return new WaitForSeconds(audioClip.length + _loopPause);
        }
    }

    #endregion
}
