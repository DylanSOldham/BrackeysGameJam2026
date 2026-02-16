using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{

    [Header("Music Effects")]
    [SerializeField] private AudioLibrary.Music song;

    private AudioManager a;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        a = AudioManager.Instance;
        startSong();
    }

    public void startSong()
    {
        PlaySound(song);
    }

    private void PlaySound(AudioLibrary.Music song)
    {
        if (AudioManager.Instance == null) return;

        AudioClip clip = AudioManager.Instance.audioLibrary.GetMusic(song);

        if (clip != null)
        {
            AudioManager.Instance.PlayMusic(clip);
        }
    }

}
