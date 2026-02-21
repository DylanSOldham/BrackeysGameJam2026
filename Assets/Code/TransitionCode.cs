using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TransitionCode : MonoBehaviour
{
    [Header("Text Settings")]
    public TextMeshProUGUI textBox;      // Your TMP text object
    [TextArea(3, 10)] public string[] texts;   // Full string to display
    public float delay = 0.1f;          // Delay between each character
    public bool mainMenu;

    public int currentIndex = 0;

    [SerializeField] private CanvasGroup fadeCanvas;
    [SerializeField] private float fadeDuration = 3f;
    public GameObject clickable;

    private Coroutine typingCoroutine;

    private void OnEnable()
    {

        fadeCanvas.alpha = 0f;
        StartCoroutine(FadeAndLoad());
    }

    private IEnumerator FadeAndLoad()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvas.alpha = timer / fadeDuration;
            yield return null;
        }

        fadeCanvas.alpha = 1f;
        clickable.SetActive(true);
        currentIndex = 0;
        if (texts.Length > 0)
            StartTyping(texts[currentIndex]);
    }


    // Start the effect
    public void StartTyping(string newText)
    {
        // Convert literal "\n" sequences into actual newline characters
        newText = newText.Replace("\\n", "\n");

        // Stop previous typing if still running
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(newText));
    }

    private IEnumerator TypeText(string text)
    {
        textBox.text = "";  // Clear text first

        foreach (char c in text)
        {
            textBox.text += c;
            yield return new WaitForSeconds(delay);
        }

        typingCoroutine = null; // Done typing
    }

    // Optional: instantly finish typing
    public void FinishTyping(string text)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        textBox.text = text;
    }

    public void ClickToContinue()
    {

        // If typing is still running, finish instantly
        if (typingCoroutine != null)
        {
            FinishTyping(texts[currentIndex]);
            return;
        }

        // Move to the next text
        currentIndex++;

        if (currentIndex < texts.Length)
        {
            StartTyping(texts[currentIndex]);
        }
        else
        {
            // All texts shown, load next scene
            if (mainMenu)
            {
                SceneManager.LoadScene("InGameScene");
            }
            else
            {
                SceneManager.LoadScene("MainMenuScene");
            }
        }
    }

}
