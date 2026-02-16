using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    public AudioSource musicSource;
    public AudioSource effectsSource;

    [Header("Library")]
    public AudioLibrary audioLibrary;


    [SerializeField] private float masterVolume;
    [SerializeField] private float musicVolume;
    [SerializeField] private float effectsVolume;


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

        setSound();
    }

    public void setSound()
    {
        //runs at start of program
        masterVolume = 100;
        musicVolume = 100;
        effectsVolume = 100;

        musicSource.volume = 1;
        effectsSource.volume = 1;
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
            effectsSource.PlayOneShot(clip);
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
        effectsSource.volume = sfx;
        Debug.Log($"[AudioManager] Volumes applied: Master={master}, Music={music}, SFX={sfx}");
    }

    public float returnMasterVolume()
    {
        return masterVolume;
    }
    public float returnMusicVolume()
    {
        return musicVolume;
    }
    public float returnEffectsVolume()
    {
        return effectsVolume;
    }

    public void changeMasterVolume(float num)
    {
        masterVolume = num;
        UpdateVolumes();
    }

    public void changeMusicVolume(float num)
    {
        musicVolume = num;
        UpdateVolumes();
    }

    public void changeEffectsVolume(float num)
    {
        effectsVolume = num;
        UpdateVolumes();
    }

    private void UpdateVolumes()
    {
        float masterNormalized = masterVolume / 100f;
        float musicNormalized = musicVolume / 100f;
        float effectsNormalized = effectsVolume / 100f;

        musicSource.volume = musicNormalized * masterNormalized;
        effectsSource.volume = effectsNormalized * masterNormalized;
    }
}