using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public Transform player;           // Referencia al jugador (esfera)
    public float detectionRange = 10f;  // Rango de visión para detectar al jugador
    public float speed = 3f;           // Velocidad de movimiento del enemigo
    public float rotationSpeed = 5f;   // Velocidad de rotación hacia el jugador
    public float attackRange = 1.5f;   // Rango para atacar al jugador
    public float attackCooldown = 1f;  // Tiempo entre ataques

    protected bool isPlayerDetected = false; // Bandera para saber si el jugador ha sido detectado
    private float attackTimer = 0f;   // Temporizador para el cooldown de ataques

    protected virtual void Update()
    {
        // Verificar si el jugador está dentro del rango de visión
        DetectPlayer();

        // Si el jugador ha sido detectado, perseguirlo
        if (isPlayerDetected)
        {
            ChasePlayer(); // No es necesario cambiar el acceso aquí

            // Comprobar si podemos atacar
            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                if (attackTimer <= 0)
                {
                    AttackPlayer();
                    attackTimer = attackCooldown; // Resetear el temporizador de ataque
                }
            }
        }

        // Actualizar el temporizador de ataque
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    // Método para detectar al jugador usando raycast
    protected virtual void DetectPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        // Realizar un raycast hacia la dirección del jugador
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, detectionRange))
        {
            if (hit.transform == player) // Si el raycast golpea al jugador
            {
                isPlayerDetected = true;
            }
        }
        else
        {
            isPlayerDetected = false; // Si el raycast no golpea al jugador, se desactiva la detección
        }
    }

    // Método para que el enemigo persiga al jugador
    protected virtual void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        // Mover al enemigo hacia el jugador
        transform.position += direction * speed * Time.deltaTime;

        // Rotar al enemigo hacia el jugador de manera suave
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    // Método para realizar el ataque (no se usa en este enemigo)
    protected virtual void AttackPlayer()
    {
        // Aquí no hacemos nada porque este enemigo no ataca
    }
}
