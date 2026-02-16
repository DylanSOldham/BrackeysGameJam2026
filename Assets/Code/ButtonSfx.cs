using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(Button))]
public class ButtonSfx : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    ISelectHandler,
    IDeselectHandler
{
    [Header("Sound Effects")]
    [SerializeField] private AudioLibrary.SFX clickSFX;
    [SerializeField] private AudioLibrary.SFX highlightSFX;

    [Header("Highlight Cooldown")]
    [SerializeField] private float highlightCooldown = 0.25f;

    private Button button;
    private TextMeshProUGUI textObject;
    private float lastHighlightTime;

    private void Start()
    {
        button = GetComponent<Button>();
        textObject = GetComponentInChildren<TextMeshProUGUI>();
        button.onClick.AddListener(PlayClick);
    }

    private void PlayClick()
    {
        PlaySound(clickSFX);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TryPlayHighlight();
        textObject.fontStyle = FontStyles.Bold | FontStyles.Italic;
        textObject.fontSize = textObject.fontSize + 10;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textObject.fontStyle = FontStyles.Bold;
        textObject.fontSize = 80;
    }

    public void OnSelect(BaseEventData eventData)
    {
        TryPlayHighlight();
    }

    private void TryPlayHighlight()
    {
        if (Time.unscaledTime - lastHighlightTime < highlightCooldown)
            return;

        lastHighlightTime = Time.unscaledTime;
        PlaySound(highlightSFX);
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

    

    public void OnDeselect(BaseEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
