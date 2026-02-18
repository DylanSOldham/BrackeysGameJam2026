using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionCode : MonoBehaviour
{
    [Header("Text Settings")]
    public TextMeshProUGUI textBox;      // Your TMP text object
    [TextArea] public string fullText;   // Full string to display
    public float delay = 0.1f;          // Delay between each character

    private Coroutine typingCoroutine;

    private void OnEnable()
    {
        StartTyping(fullText);
    }

    // Start the effect
    public void StartTyping(string newText)
    {
        // Convert literal "\n" sequences into actual newline characters
        fullText = newText.Replace("\\n", "\n");

        // Stop previous typing if still running
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        textBox.text = "";  // Clear text first

        foreach (char c in fullText)
        {
            textBox.text += c;
            yield return new WaitForSeconds(delay);
        }

        typingCoroutine = null; // Done typing
    }

    // Optional: instantly finish typing
    public void FinishTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        textBox.text = fullText;
    }


    public void ClickToContinue()
    {
        SceneManager.LoadScene("InGameScene");
    }

}
