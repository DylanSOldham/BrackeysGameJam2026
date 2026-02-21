using UnityEngine;
using UnityEngine.UI;

public class SnakeHealthUI : MonoBehaviour
{
    public Slider slider;
    public GameObject transition;

    public void setSlider(float percentHealth)
    {
        slider.value = percentHealth;

    }

    public void setTransition()
    {
        transition.SetActive(true);
    }

}
