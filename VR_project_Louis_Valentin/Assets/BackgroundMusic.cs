using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource; // Référence à l'AudioSource

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.loop = true;  // Assure que la musique se répète
            audioSource.Play();       // Joue la musique
        }
    }
}
