using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour
{
    public Image fadeImage;  // L'image UI noire
    public float fadeDuration = 1f;  // Durée du fondu
    public string sceneToReload = "SampleScene";  // Nom de la scène à recharger

    private void Start()
    {
        // Initialiser l'image comme transparente
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    // Appelé lorsque le joueur est touché
    public void TriggerGameOver()
    {
        StartCoroutine(FadeToBlackAndRestart());
    }

    private IEnumerator FadeToBlackAndRestart()
    {
        // Fondu au noir (fade in)
        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(timeElapsed / fadeDuration));  // Augmente l'alpha
            yield return null;
        }

        // Après le fondu, redémarre la scène
        SceneManager.LoadScene(sceneToReload);
    }
}
