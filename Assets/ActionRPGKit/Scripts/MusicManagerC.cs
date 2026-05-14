using UnityEngine;

public class MusicManagerC : MonoBehaviour
{
    public static MusicManagerC instance;
    public AudioClip backgroundMusic;
    private AudioSource audioSource;
    public bool isMuted = false;
    public float targetVolume = 1.0f;

    void Awake()
    {
        Debug.Log("MusicManager: Awake() called");
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            EnsureAudioListenerExists();
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("MusicManager: Start() called");
        
        if (backgroundMusic == null)
        {
            Debug.Log("MusicManager: No clip assigned, loading from Resources...");
            LoadMusicFromResources();
        }
        
        PlayMusic();
    }

    void Update()
    {
        if (audioSource != null)
        {
            audioSource.volume = isMuted ? 0 : targetVolume;
            
            if (!audioSource.isPlaying && backgroundMusic != null)
            {
                Debug.Log("MusicManager: Music stopped, restarting...");
                audioSource.clip = backgroundMusic;
                audioSource.Play();
            }
        }
    }

    void InitializeAudio()
    {
        Debug.Log("MusicManager: Initializing audio source...");
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = targetVolume;
        audioSource.playOnAwake = false;
        audioSource.priority = 0;
        audioSource.spatialBlend = 0f;
        
        Debug.Log("MusicManager: Audio source initialized");
    }

    void EnsureAudioListenerExists()
    {
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        Debug.Log("MusicManager: Found " + listeners.Length + " AudioListener(s) in scene");
        
        if (listeners.Length == 0)
        {
            Debug.Log("MusicManager: No AudioListener found, creating one...");
            
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.gameObject.AddComponent<AudioListener>();
                Debug.Log("MusicManager: Added AudioListener to Main Camera");
            }
            else
            {
                GameObject listenerObj = new GameObject("AudioListener");
                listenerObj.AddComponent<AudioListener>();
                DontDestroyOnLoad(listenerObj);
                Debug.Log("MusicManager: Created standalone AudioListener");
            }
        }
    }

    void LoadMusicFromResources()
    {
        Debug.Log("MusicManager: Loading music from Resources/Music...");
        
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Music");
        
        if (clips.Length > 0)
        {
            backgroundMusic = clips[0];
            Debug.Log("MusicManager: Loaded - " + backgroundMusic.name + ", Length: " + backgroundMusic.length + "s");
        }
        else
        {
            Debug.LogError("MusicManager: No music files found in Resources/Music folder!");
        }
    }

    public void PlayMusic()
    {
        Debug.Log("MusicManager: PlayMusic() called");
        
        if (audioSource != null)
        {
            Debug.Log("MusicManager: AudioSource is ready");
            
            if (backgroundMusic != null)
            {
                Debug.Log("MusicManager: Background music is set: " + backgroundMusic.name);
                
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = backgroundMusic;
                    audioSource.Play();
                    Debug.Log("MusicManager: Starting playback...");
                }
                else
                {
                    Debug.Log("MusicManager: Already playing");
                }
            }
            else
            {
                Debug.LogError("MusicManager: Background music is null!");
            }
        }
        else
        {
            Debug.LogError("MusicManager: AudioSource is null!");
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
        targetVolume = Mathf.Clamp01(volume);
        if (audioSource != null)
        {
            audioSource.volume = targetVolume;
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        if (audioSource != null)
        {
            audioSource.volume = isMuted ? 0 : targetVolume;
        }
    }
}
