using UnityEngine;

public class AutoMusicLoader : MonoBehaviour
{
    void Awake()
    {
        if (MusicManagerC.instance == null)
        {
            GameObject musicManager = new GameObject("MusicManager");
            musicManager.AddComponent<MusicManagerC>();
            DontDestroyOnLoad(musicManager);
            Debug.Log("MusicManager created automatically");
        }
    }
}
