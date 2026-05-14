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
        if (backgroundMusic == null)
        {
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
                audioSource.clip = backgroundMusic;
                audioSource.Play();
            }
        }
    }

    void InitializeAudio()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = targetVolume;
        audioSource.playOnAwake = false;
        audioSource.priority = 0;
        audioSource.spatialBlend = 0f;
    }

    void EnsureAudioListenerExists()
    {
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        if (listeners.Length == 0)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.gameObject.AddComponent<AudioListener>();
            }
            else
            {
                GameObject listenerObj = new GameObject("AudioListener");
                listenerObj.AddComponent<AudioListener>();
                DontDestroyOnLoad(listenerObj);
            }
        }
    }

    void LoadMusicFromResources()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Music");
        if (clips.Length > 0)
        {
            backgroundMusic = clips[0];
        }
    }

    public void PlayMusic()
    {
        if (audioSource != null && backgroundMusic != null && !audioSource.isPlaying)
        {
            audioSource.clip = backgroundMusic;
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
