using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    [SerializeField] private CanvasGroup fadeCanvas;
    [SerializeField] private float fadeDuration = 3f;
    public GameObject clickable;

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
    }


    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
