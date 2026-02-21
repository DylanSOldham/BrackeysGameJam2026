using UnityEngine;
using UnityEngine.UI;

public class SnakeHealthUI : MonoBehaviour
{
    public Slider slider;
    public GameObject transition;
    public Player player;
    public GameMusicManager manager;

    public void setSlider(float percentHealth)
    {
        slider.value = percentHealth;

    }

    public void setTransition()
    {
        player.won = true;
        manager.updateStoryMusic();
        transition.SetActive(true);
    }

}
