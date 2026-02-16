using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioScreen : MonoBehaviour
{
    public TextMeshProUGUI masterValue;
    public TextMeshProUGUI musicValue;
    public TextMeshProUGUI effectsValue;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectsSlider;


    private AudioManager a;
    private void Awake()
    {
        a = AudioManager.Instance;
    }

    private void OnEnable()
    {
        masterSlider.value = a.returnMasterVolume();
        masterValue.text = a.returnMasterVolume().ToString();

        musicSlider.value = a.returnMusicVolume();
        musicValue.text = a.returnMusicVolume().ToString();

        effectsSlider.value = a.returnEffectsVolume();
        effectsValue.text = a.returnEffectsVolume().ToString();

    }

    public void changeMasterVolume(float volume)
    {
        Debug.Log("changing master volume");
        masterSlider.value = volume;
        masterValue.text = volume.ToString();
        a.changeMasterVolume(volume);
    }

    public void changeMusicVolume(float volume)
    {
        Debug.Log("changing master volume");
        musicSlider.value = volume;
        musicValue.text = volume.ToString();
        a.changeMusicVolume(volume);
    }

    public void changeEffectsVolume(float volume)
    {
        Debug.Log("changing master volume");
        effectsSlider.value = volume;
        effectsValue.text = volume.ToString();
        a.changeEffectsVolume(volume);
    }

}
