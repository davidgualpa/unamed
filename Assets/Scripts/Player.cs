using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public float speed = 10f;           // Fuerza de movimiento de la esfera
    public float maxSpeed = 5f;         // Velocidad máxima de la esfera
    public float friction = 0.95f;      // Fricción para desaceleración
    public float jumpForce = 10f;       // Fuerza del salto (aumentado)
    public float groundCheckDistance = 0.5f; // Distancia para detectar si está en el suelo

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // Obtenemos el Rigidbody desde el Inspector
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Verificamos si la esfera está tocando el suelo
        isGrounded = IsGrounded();

        // Capturamos el input horizontal (A/D) y vertical (W/S)
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // Crear un vector de dirección en base al input
        Vector3 direction = new Vector3(horizontalMovement, 0, verticalMovement).normalized;

        // Aplicar fuerza para mover la esfera
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(direction * speed, ForceMode.Acceleration);
        }

        // Aplicar fricción artificial para desacelerar cuando no se presionan teclas
        if (direction == Vector3.zero)
        {
            rb.linearVelocity *= friction;
        }

        // Detectar salto
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // Si está en el suelo y se presiona la tecla espacio
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Aplicar fuerza hacia arriba
        }
    }

    void FixedUpdate()
    {
        // Limitamos la velocidad máxima para que no supere el valor maxSpeed
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    // Método para comprobar si está tocando el suelo usando un Raycast
    private bool IsGrounded()
    {
        // Raycast hacia abajo para verificar si tocamos el suelo
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
}
