using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Library")]
    public AudioLibrary audioLibrary;


    [SerializeField] private float masterVolume;
    [SerializeField] private float musicVolume;
    [SerializeField] private float sfxVolume;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource.clip == clip) return;
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
        Debug.Log($"[AudioManager] Playing music: {clip.name}");
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
            Debug.Log($"[AudioManager] Playing SFX: {clip.name}");
        }
    }

    // Stop music
    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void ApplyVolumes(float master, float music, float sfx)
    {
        AudioListener.volume = master;
        musicSource.volume = music;
        sfxSource.volume = sfx;
        Debug.Log($"[AudioManager] Volumes applied: Master={master}, Music={music}, SFX={sfx}");
    }

}