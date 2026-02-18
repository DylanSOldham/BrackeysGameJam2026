using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    [Header("Sound Effects")]
    [SerializeField] private AudioLibrary.SFX clickSFX;
    //[SerializeField] private float sfxCooldown = 0.2f;


    public void PlayClick()
    {
        
        PlaySound(clickSFX);
    }

    private void PlaySound(AudioLibrary.SFX sfx)
    {
        if (AudioManager.Instance == null) return;

        AudioClip clip = AudioManager.Instance.audioLibrary.GetSFX(sfx);

        if (clip != null)
        {
            AudioManager.Instance.PlaySFX(clip);
        }
    }
}
