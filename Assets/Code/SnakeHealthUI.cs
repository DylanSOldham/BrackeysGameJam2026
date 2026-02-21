using UnityEngine;
using UnityEngine.UI;

public class SnakeHealthUI : MonoBehaviour
{
    public Slider slider;

    public void setSlider(float percentHealth)
    {
        slider.value = percentHealth;

    }

}
