using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio/AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    [Header("Music")]
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;

    [Header("SFX")]
    public AudioClip buttonClick;
    public AudioClip buttonHighlight;
    public AudioClip upgradeSound;

    // Enum for easy dropdown selection
    public enum SFX
    {
        ButtonClick,
        ButtonHighlight,
        Upgrade
    }

    // Helper to get clip from enum
    public AudioClip GetSFX(SFX sfx)
    {
        switch (sfx)
        {
            case SFX.ButtonClick: return buttonClick;
            case SFX.ButtonHighlight: return buttonHighlight;
            case SFX.Upgrade: return upgradeSound;
            default: return null;
        }
    }
}
