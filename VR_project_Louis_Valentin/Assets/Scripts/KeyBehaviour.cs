using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class KeyBehaviour : MonoBehaviour
{
    public string keyID; // Identifiant unique de la clé

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision détectée avec : " + col.gameObject.name);

        // Vérifie si l'objet touché a le tag "Door" (optionnel mais conseillé)
        if (col.gameObject.CompareTag("Door"))
        {
            Debug.Log("La clé a touché une porte.");

            // Vérifie si l'objet possède le script Door
            if (col.gameObject.TryGetComponent<Door>(out Door door))
            {
                Debug.Log("La porte possède un script Door.");

                if (door.Unlock(keyID))
                {
                    Debug.Log("Porte déverrouillée !");
                    Destroy(gameObject); // Détruit la clé après usage
                }
                else
                {
                    Debug.Log("Cette clé ne correspond pas à la porte.");
                }
            }
            else
            {
                Debug.LogWarning("L'objet touché n'a pas de script Door !");
            }
        }
    }
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        transform.parent = null; // Détache l'objet du contrôleur
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
        // Facultatif : Appliquer une physique réaliste après le lâcher
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void OnDestroy()
    {
        grabInteractable.onSelectEntered.RemoveListener(OnGrab);
        grabInteractable.onSelectExited.RemoveListener(OnRelease);
    }
}
