using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // El objeto que la cámara seguirá (la esfera o el jugador)
    public float smoothSpeed = 0.125f; // Velocidad de suavizado de la cámara
    public Vector3 offset;          // Desplazamiento fijo de la cámara respecto al jugador

    public float minDistance = 3f;   // Distancia mínima a la que la cámara puede acercarse
    public float maxDistance = 8f;   // Distancia máxima a la que la cámara puede alejarse
    public float distanceSpeed = 2f; // Velocidad de desplazamiento de la cámara cuando se usa la rueda del ratón

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("No target assigned to CameraFollow script!");
        }
    }

    void LateUpdate()
    {
        // Suavizamos el movimiento de la cámara
        Vector3 desiredPosition = target.position + offset; // Posición deseada en función del offset

        // Suavizamos el movimiento
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualizamos la posición de la cámara
        transform.position = smoothedPosition;

        // La cámara mira al jugador todo el tiempo
        transform.LookAt(target);

        // Control para ajustar la distancia con la rueda del ratón (opcional)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        offset = offset * (1 + scroll * distanceSpeed); // Cambia el offset según la rueda del ratón

        // Limitar la distancia de la cámara para no alejarla o acercarla demasiado
        offset = Vector3.ClampMagnitude(offset, maxDistance);
        offset = Vector3.ClampMagnitude(offset, minDistance);
    }
}
