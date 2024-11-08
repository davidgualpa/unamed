using UnityEngine;

public class PursuingEnemy : EnemyEntity
{
    public float catchRange = 1.5f;  // Rango para "atrapar" al jugador
    public float catchTime = 1f;     // Tiempo de espera para la acción (por ejemplo, para atacar o hacer una animación)
    private float catchTimer = 0f;   // Temporizador para la acción al atrapar

    protected override void Update()
    {
        // Verificar si el jugador está dentro del rango de visión
        DetectPlayer();

        // Si el jugador ha sido detectado, perseguirlo
        if (isPlayerDetected)
        {
            ChasePlayer();

            // Comprobar si el enemigo ha alcanzado al jugador
            if (Vector3.Distance(transform.position, player.position) < catchRange)
            {
                catchTimer += Time.deltaTime;

                // Si el enemigo está cerca del jugador por el tiempo suficiente, realizar la acción
                if (catchTimer >= catchTime)
                {
                    PerformActionOnCatch();  // Realizar la acción
                    catchTimer = 0f;         // Resetear el temporizador
                }
            }
            else
            {
                // Si el jugador se aleja, resetear el temporizador
                catchTimer = 0f;
            }
        }
    }

    // Método para detectar al jugador usando raycast (heredado de EnemyEntity)
    protected override void DetectPlayer()
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

    // Método para que el enemigo persiga al jugador (heredado de EnemyEntity)
    protected override void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        // Mover al enemigo hacia el jugador
        transform.position += direction * speed * Time.deltaTime;

        // Rotar al enemigo hacia el jugador de manera suave
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    // Método para realizar una acción cuando el enemigo atrapa al jugador
    private void PerformActionOnCatch()
    {
        // Aquí puedes definir lo que sucede cuando el enemigo atrapa al jugador
        // Por ejemplo, un mensaje en consola o una animación
        Debug.Log("¡El enemigo atrapó al jugador!");

        // Puedes hacer que el enemigo ataque o haga una animación
        // Por ejemplo, podrías hacer que el jugador reciba daño, o realizar una animación de captura
        // Si tienes animaciones:
        // animator.SetTrigger("CatchAnimation");

        // Si el enemigo ataca al jugador, puedes hacer algo como esto:
        // playerHealth.TakeDamage(damageAmount);
    }

    // Método para realizar el ataque (no se usa en este enemigo, pero puede ser utilizado para otros enemigos)
    protected override void AttackPlayer()
    {
        // Aquí no hacemos nada porque este enemigo no ataca
    }
}
