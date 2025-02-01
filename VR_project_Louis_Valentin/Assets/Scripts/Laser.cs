using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Laser : MonoBehaviour
{
    [Header("Laser Settings")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserDistance = 10f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveRange = 2f;

    [Header("Collision Settings")]
    [SerializeField] private LayerMask collisionMask;

    private Vector3 startPosition;

    private void Awake()
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is not assigned.");
            return;
        }

        lineRenderer.positionCount = 2;
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveLaser();
        UpdateLaser();
    }

    private void MoveLaser()
    {
        // Mouvement vertical entre y = 0 et y = 2
        float offset = Mathf.Sin(Time.time * moveSpeed) * (moveRange / 2f);
        transform.position = new Vector3(startPosition.x, startPosition.y + offset, startPosition.z);
    }

    private void UpdateLaser()
    {
        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + transform.forward * laserDistance;

        Ray ray = new Ray(startPoint, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, laserDistance, collisionMask))
        {
            // Collision détectée
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, hit.point);
            Debug.Log("Le joueur a touché le laser !");
            SceneManager.LoadScene("SampleScene");
        
        }
        else
        {
            // Pas de collision, dessine jusqu'à la distance maximale
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}
