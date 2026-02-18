using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{

    [Header("Music Effects")]
    [SerializeField] private AudioLibrary.Music mainMenu;
    [SerializeField] private AudioLibrary.Music storySong;


    private AudioManager a;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        a = AudioManager.Instance;
        startSong();
    }

    public void updateSongToStory()
    {
        Debug.Log("UPDATING SOUND TO STORY");
        PlaySound(storySong);
    }

    public void startSong()
    {
        PlaySound(mainMenu);
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
