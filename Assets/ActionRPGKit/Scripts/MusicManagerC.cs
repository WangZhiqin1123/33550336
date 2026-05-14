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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic();
    }

    void Update()
    {
        if (audioSource != null)
        {
            audioSource.volume = isMuted ? 0 : targetVolume;
            
            if (!audioSource.isPlaying && backgroundMusic != null)
            {
                Debug.Log("Attempting to play music...");
                audioSource.Play();
            }
        }
    }

    void InitializeAudio()
    {
        EnsureAudioListenerExists();
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = targetVolume;
        audioSource.playOnAwake = true;
        audioSource.priority = 0;
        audioSource.spatialBlend = 0;
        
        if (backgroundMusic == null)
        {
            LoadMusicFromResources();
        }
        
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            Debug.Log("MusicManager initialized with clip: " + backgroundMusic.name);
        }
        else
        {
            Debug.LogError("MusicManager: No background music clip assigned!");
        }
    }

    void EnsureAudioListenerExists()
    {
        if (FindObjectOfType<AudioListener>() == null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.gameObject.AddComponent<AudioListener>();
                Debug.Log("Added AudioListener to main camera");
            }
            else
            {
                GameObject listenerObj = new GameObject("AudioListener");
                listenerObj.AddComponent<AudioListener>();
                DontDestroyOnLoad(listenerObj);
                Debug.Log("Created standalone AudioListener object");
            }
        }
        else
        {
            Debug.Log("AudioListener already exists in scene");
        }
    }

    void LoadMusicFromResources()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Music");
        if (clips.Length > 0)
        {
            backgroundMusic = clips[0];
            Debug.Log("Loaded background music from Resources: " + backgroundMusic.name + ", Length: " + backgroundMusic.length + "s");
        }
        else
        {
            Debug.LogWarning("No music files found in Resources/Music folder. Supported formats: MP3, WAV, OGG");
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
