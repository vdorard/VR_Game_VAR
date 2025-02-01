using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class RayInteractorToggle : MonoBehaviour
{
    private XRRayInteractor rayInteractor;
    private InputDevice controller; // Contrôleur VR
    public XRNode handType = XRNode.LeftHand; // 🖐 Définit si c'est la main gauche ou droite

    void Start()
    {
        // Récupérer le XRRayInteractor attaché à l'objet
        rayInteractor = GetComponent<XRRayInteractor>();

        // Trouver le contrôleur correspondant
        controller = InputDevices.GetDeviceAtXRNode(handType);

        // Désactiver le Ray Interactor au démarrage
        rayInteractor.enabled = false;
    }

    void Update()
    {
        // Lire la valeur de la gâchette (float entre 0 et 1)
    float triggerValue = 0f;
    if (controller.TryGetFeatureValue(CommonUsages.trigger, out triggerValue))
        {
        rayInteractor.enabled = triggerValue > 0.1f;
        Debug.Log("Gâchette pressée : " + triggerValue + " - Ray Interactor: " + rayInteractor.enabled);
        }
    }   

    
}
