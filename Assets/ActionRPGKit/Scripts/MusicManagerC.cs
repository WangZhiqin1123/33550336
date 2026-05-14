using UnityEngine;

public class MusicManagerC : MonoBehaviour
{
    public static MusicManagerC instance;
    public AudioClip backgroundMusic;
    private AudioSource audioSource;
    public bool isMuted = false;
    public float targetVolume = 0.7f;

    void Awake()
    {
        Debug.Log("MusicManager: Awake() called");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudio();
        }
        else
        {
            Debug.Log("MusicManager: Instance already exists, destroying duplicate");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("MusicManager: Start() called");
        if (audioSource != null && !audioSource.isPlaying && backgroundMusic != null)
        {
            Debug.Log("MusicManager: Start() attempting to play music...");
            audioSource.Play();
        }
    }

    void Update()
    {
        if (audioSource != null)
        {
            audioSource.volume = isMuted ? 0 : targetVolume;

            if (!audioSource.isPlaying && backgroundMusic != null)
            {
                Debug.Log("MusicManager: Update() - attempting to play music...");
                audioSource.Play();
            }
        }
    }

    void InitializeAudio()
    {
        Debug.Log("MusicManager: Initializing audio...");
        EnsureAudioListenerExists();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = targetVolume;
        audioSource.playOnAwake = false;
        audioSource.priority = 0;
        audioSource.spatialBlend = 0;

        if (backgroundMusic == null)
        {
            Debug.Log("MusicManager: No clip assigned, loading from Resources...");
            LoadMusicFromResources();
        }

        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            Debug.Log("MusicManager: Clip loaded - " + backgroundMusic.name + ", duration: " + backgroundMusic.length + "s");
            Debug.Log("MusicManager: Starting playback immediately...");
            audioSource.Play();
            Debug.Log("MusicManager: Play() called, isPlaying: " + audioSource.isPlaying);
        }
        else
        {
            Debug.LogError("MusicManager: Failed to load background music!");
        }
    }

    void EnsureAudioListenerExists()
    {
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        Debug.Log("MusicManager: Found " + listeners.Length + " AudioListener(s) in scene");

        if (listeners.Length == 0)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.gameObject.AddComponent<AudioListener>();
                Debug.Log("MusicManager: Added AudioListener to main camera");
            }
            else
            {
                GameObject listenerObj = new GameObject("AudioListener");
                listenerObj.AddComponent<AudioListener>();
                DontDestroyOnLoad(listenerObj);
                Debug.Log("MusicManager: Created standalone AudioListener object");
            }
        }
        else
        {
            Debug.Log("MusicManager: AudioListener already exists in scene");
        }
    }

    void LoadMusicFromResources()
    {
        Debug.Log("MusicManager: Loading music from Resources/Music...");
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Music");
        Debug.Log("MusicManager: Found " + clips.Length + " audio clip(s) in Resources/Music");

        if (clips.Length > 0)
        {
            backgroundMusic = clips[0];
            Debug.Log("MusicManager: Loaded - " + backgroundMusic.name + ", Length: " + backgroundMusic.length + "s");
        }
        else
        {
            Debug.LogError("MusicManager: No music files found in Resources/Music folder. Supported formats: MP3, WAV, OGG");
        }
    }

    public void SetMusic(AudioClip music)
    {
        backgroundMusic = music;
        audioSource.clip = music;
        audioSource.Play();
    }

    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            Debug.Log("MusicManager: PlayMusic() called");
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = Mathf.Clamp01(volume);
        }
    }
}
