using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Pour recharger la scène

public class EnemyController : MonoBehaviour
{
   public float normalSpeed = 2f;       // Vitesse normale
    public float chaseSpeed = 4f;        // Vitesse en mode poursuite
    public float turnTime = 3f;          // Temps avant rotation aléatoire
    public float minTurnAngle = 90f;     // Rotation minimale si obstacle détecté
    public float maxTurnAngle = 180f;    // Rotation maximale aléatoire
    public float detectionRange = 5f;    // Distance à laquelle l'ennemi poursuit
    public float attackRange = 1.5f;     // Distance à laquelle le joueur est attrapé
    public float obstacleCheckDistance = 1.5f;  // Masque pour détecter les obstacles

    public LayerMask obstacleMask;
    private CharacterController controller;
    private Animator animator;
    private Vector3 moveDirection;
    private Transform player;
    private bool isChasing = false;
    private bool hasPlayedSound = false;
    private AudioSource audioSource;
    
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        moveDirection = transform.forward;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ChangeDirectionRoutine());
        
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Mode poursuite si le joueur est proche
         if (distanceToPlayer <= detectionRange && !hasPlayedSound)
        {
            audioSource.Play(); // 🔊 Joue le son
            hasPlayedSound = true; // Empêche de rejouer en boucle
        }
        else if (distanceToPlayer > detectionRange)
        {
            hasPlayedSound = false; // Réinitialise quand le joueur s'éloigne
        }

        // Mode poursuite si le joueur est proche
        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
            ChasePlayer();
        }
        else
        {
            isChasing = false;
            Patrol();
        }

        // Redémarrer le jeu si le joueur est trop proche
        if (distanceToPlayer <= attackRange)
        {
            RestartGame();
        }
    }

    void Patrol()
    {
        // Détection des obstacles devant
        if (Physics.Raycast(transform.position, transform.forward, obstacleCheckDistance, obstacleMask))
        {
            Debug.Log("Obstacle détecté, changement de direction !");
            ChangeDirection();
        }

        // Déplacement en ligne droite
        controller.Move(moveDirection * normalSpeed * Time.deltaTime);
    }

    void ChasePlayer()
    {
        // Aller vers le joueur
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        moveDirection = new Vector3(directionToPlayer.x, 0, directionToPlayer.z);
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        controller.Move(moveDirection * chaseSpeed * Time.deltaTime);
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(turnTime);

            if (!isChasing) // Changer de direction uniquement en mode patrouille
            {
                ChangeDirection();
            }
        }
    }

    void ChangeDirection()
    {
        // Tourne d'un angle aléatoire entre minTurnAngle et maxTurnAngle
        float randomAngle = Random.Range(minTurnAngle, maxTurnAngle);
        transform.Rotate(0, randomAngle, 0);

        // Met à jour la direction de mouvement
        moveDirection = transform.forward;
        Debug.Log("Nouvelle direction après rotation : " + moveDirection);
    }

    void RestartGame()
    {
        Debug.Log("Le joueur a été attrapé ! Rechargement de la scène...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDrawGizmos()
    {
        // Afficher le Raycast dans la scène pour voir la détection d'obstacles
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * obstacleCheckDistance);
    }
}

