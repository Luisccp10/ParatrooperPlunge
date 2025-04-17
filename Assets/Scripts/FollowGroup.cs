using UnityEngine;

public class FollowGroup : MonoBehaviour
{
    public Transform[] targets; // Array de los Transforms de los jugadores
    public float smoothSpeed = 0.125f;
    public float heightOffset = 10f; // Altura de la cámara sobre el grupo
    public float zoomFactor = 2f;     // Factor de zoom basado en la dispersión
    public float minZoom = 5f;        // Zoom mínimo
    public float maxZoom = 15f;       // Zoom máximo

    private Vector3 velocity;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("Este script necesita estar adjunto a una Cámara.");
            enabled = false;
        }
    }

    void LateUpdate()
    {
        if (targets.Length == 0)
        {
            return;
        }

        // Calcular el centro del grupo de jugadores
        Bounds groupBounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform target in targets)
        {
            groupBounds.Encapsulate(target.position);
        }
        Vector3 centerPoint = groupBounds.center;

        // Calcular la posición deseada de la cámara
        Vector3 desiredPosition = centerPoint + Vector3.up * heightOffset;

        // Suavizar el movimiento de la cámara
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;

        // Ajustar el zoom de la cámara basado en la dispersión del grupo
        float maxDistance = 0f;
        foreach (Transform target1 in targets)
        {
            foreach (Transform target2 in targets)
            {
                float distance = Vector3.Distance(target1.position, target2.position);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }
        }

        float requiredZoom = Mathf.Clamp(maxDistance * zoomFactor, minZoom, maxZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, requiredZoom, Time.deltaTime * 2f);
    }

    // Método público para establecer los objetivos (jugadores)
    public void SetTargets(Transform[] newTargets)
    {
        targets = newTargets;
    }
}
