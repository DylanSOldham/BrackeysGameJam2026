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
        masterVolume = 50;
        musicVolume = 40;
        effectsVolume = 40;

        ApplyVolumes(masterVolume, musicVolume, effectsVolume);
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
        float masterNormalized = master / 100f;
        float musicNormalized = music / 100f;
        float effectsNormalized = sfx / 100f;

        musicSource.volume = musicNormalized * masterNormalized;
        effectsSource.volume = effectsNormalized * masterNormalized;
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