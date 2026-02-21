using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
    [Header("Music Effects")]
    [SerializeField] private AudioLibrary.Music normalSong;
    [SerializeField] private AudioLibrary.Music DeathSong;
    [SerializeField] private AudioLibrary.Music bossMusic;
    [SerializeField] private AudioLibrary.Music storyMusic;

    private AudioManager a;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        a = AudioManager.Instance;
        startSong();
    }

    public void startSong()
    {
        PlaySound(normalSong);
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

    public void updateSongDeath()
    {
        PlaySound(DeathSong);
    }

    public void updateBossSong()
    {
        PlaySound(bossMusic);
    }

    public void updateStoryMusic()
    {
        PlaySound(storyMusic);
    }

}
