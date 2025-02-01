using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableRigidbodyOnMove : MonoBehaviour
{
	private XRGrabInteractable grabInteractable;
	private Rigidbody rb;
	private ActionBasedContinuousMoveProvider moveProvider;

	private bool isMoving = false; // Pour suivre l'état du mouvement

	void Start()
	{
    	grabInteractable = GetComponent<XRGrabInteractable>();
    	rb = GetComponent<Rigidbody>();

    	// Trouvez le composant de mouvement VR dans la scène
    	moveProvider = FindObjectOfType<ActionBasedContinuousMoveProvider>();

    	if (moveProvider == null)
    	{
        	Debug.LogError("ActionBasedContinuousMoveProvider non trouvé dans la scène !");
        	return;
    	}
	}

	void Update()
	{
    	// Détectez si le joueur est en train de bouger
    	if (moveProvider != null)
    	{
        	// Vérifiez si l'input du joystick est actif
        	bool isJoystickActive = moveProvider.leftHandMoveAction.action.ReadValue<Vector2>().magnitude > 0.1f;

        	if (isJoystickActive && !isMoving)
        	{
            	// Le joueur commence à bouger
            	OnBeginMove();
            	isMoving = true;
        	}
        	else if (!isJoystickActive && isMoving)
        	{
            	// Le joueur arrête de bouger
            	OnEndMove();
            	isMoving = false;
        	}
    	}
	}

	void OnBeginMove()
	{
    	// Désactivez le Rigidbody pendant le mouvement
    	if (grabInteractable.isSelected) // Vérifiez si l'objet est tenu
    	{
        	rb.isKinematic = true; // Désactive les forces physiques
    	}
	}

	void OnEndMove()
	{
    	// Réactivez le Rigidbody après le mouvement
    	if (grabInteractable.isSelected) // Vérifiez si l'objet est tenu
    	{
        	rb.isKinematic = false; // Réactive les forces physiques
    	}
	}
}
