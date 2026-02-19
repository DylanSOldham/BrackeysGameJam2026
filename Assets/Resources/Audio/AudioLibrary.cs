using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio/AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    [Header("Music")]
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;
    public AudioClip storyMusic;

    [Header("SFX")]
    public AudioClip buttonClick;
    public AudioClip buttonHighlight;
    public AudioClip closeMenu;
    public AudioClip sliderMoving;
    public AudioClip playerMovement;
    public AudioClip frogMovement;
    public AudioClip playerShooting;
    public AudioClip playerHit;
    public AudioClip frogHit;
    public AudioClip frogShooting;
    public AudioClip roomChange;


    // Enum for easy dropdown selection
    public enum Music
    {
        MainMenu,
        GameMusic,
        StoryMusic
    }

    // Enum for easy dropdown selection
    public enum SFX
    {
        ButtonClick,
        ButtonHighlight,
        closeMenu,
        sliderMoving,
        playerMovement,
        frogMovement,
        playerShooting,
        playerHit,
        frogHit,
        frogShooting,
        roomChange,
    }

    // Helper to get clip from enum
    public AudioClip GetMusic(Music song)
    {
        switch (song)
        {
            case Music.MainMenu: return mainMenuMusic;
            case Music.GameMusic: return gameMusic;
            case Music.StoryMusic: return storyMusic;
            default: return null;
        }
    }

    // Helper to get clip from enum
    public AudioClip GetSFX(SFX sfx)
    {
        switch (sfx)
        {
            case SFX.ButtonClick: return buttonClick;
            case SFX.ButtonHighlight: return buttonHighlight;
            case SFX.closeMenu: return closeMenu;
            case SFX.sliderMoving: return sliderMoving;
            case SFX.playerMovement: return playerMovement;
            case SFX.frogMovement: return frogMovement;
            case SFX.playerShooting: return playerShooting;
            case SFX.playerHit: return playerHit;
            case SFX.frogHit: return frogHit;
            case SFX.frogShooting: return frogShooting;
            case SFX.roomChange: return roomChange;
            default: return null;
        }
    }
}
