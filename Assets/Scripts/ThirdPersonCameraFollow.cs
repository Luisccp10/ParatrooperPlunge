using UnityEngine;

public class ThirdPersonCameraFollow : MonoBehaviour
{
    public Transform target; // El objeto al que la cámara debe seguir (el jugador)
    public float smoothSpeed = 0.125f; // Factor de suavizado para el movimiento de la cámara
    public Vector3 offset; // Desplazamiento de la cámara respecto al objetivo

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("La cámara no tiene un objetivo asignado.");
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;

        // Opcional: Hacer que la cámara siempre mire al objetivo
        // transform.LookAt(target);
    }

    // Método público para establecer el objetivo de la cámara
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
