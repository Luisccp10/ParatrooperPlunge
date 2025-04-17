using UnityEngine;

public class Blooper : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Vector3 moveDirection;
    private float changeDirectionInterval = 2f;
    private float nextDirectionChangeTime;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on " + gameObject.name);
            enabled = false;
            return;
        }
        SetRandomDirection();
        nextDirectionChangeTime = Time.time + changeDirectionInterval;
    }

    void Update()
    {
        if (Time.time >= nextDirectionChangeTime)
        {
            SetRandomDirection();
            nextDirectionChangeTime = Time.time + changeDirectionInterval;
        }

        // Mover el Blooper usando Rigidbody para mejor manejo de colisiones
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    void SetRandomDirection()
    {
        // Generar una dirección aleatoria en el plano XY
        moveDirection = Random.insideUnitSphere;
        moveDirection.z = 0f;
        moveDirection = moveDirection.normalized;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Opcional: Hacer que el Blooper rebote al chocar con algo
        moveDirection = Vector3.Reflect(moveDirection, collision.contacts[0].normal).normalized;
    }
}
