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

    void InitializeAudio()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = targetVolume;
        audioSource.playOnAwake = false;

        if (backgroundMusic == null)
        {
            LoadMusicFromResources();
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
