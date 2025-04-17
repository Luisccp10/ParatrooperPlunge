using UnityEngine;

public class RandomMovementAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float changeDirectionInterval = 2f;
    public float randomForceMagnitude = 1f;
    public string groundTag = "Ground"; // Etiqueta del objeto que representa la isla

    private Rigidbody rb;
    private float nextDirectionChangeTime;
    private Vector3 randomForce;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on " + gameObject.name);
            enabled = false;
            return;
        }
        nextDirectionChangeTime = Time.time + changeDirectionInterval;
        ApplyRandomForce(); // Aplicar una fuerza inicial
    }

    void FixedUpdate()
    {
        if (!isGrounded)
        {
            if (Time.time >= nextDirectionChangeTime)
            {
                ApplyRandomForce();
                nextDirectionChangeTime = Time.time + changeDirectionInterval;
            }

            // Aplicar una fuerza constante en la dirección aleatoria actual
            rb.AddForce(randomForce * moveSpeed, ForceMode.Force);

            // Opcional: Ligeramente alinear la rotación con la dirección del movimiento
            if (rb.linearVelocity.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z));
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 2f);
            }
        }
        else
        {
            // Si está en el suelo, detener toda la velocidad
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void ApplyRandomForce()
    {
        // Generar una fuerza aleatoria en el plano horizontal (XY en su perspectiva local, que es global XZ)
        randomForce = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * randomForceMagnitude;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisión fue con un objeto etiquetado como "Ground"
        if (collision.gameObject.CompareTag(groundTag))
        {
            isGrounded = true;
            Debug.Log(gameObject.name + " (IA) ha aterrizado.");
            // Opcional: Puedes desactivar este script aquí si ya no necesitas el movimiento
            // enabled = false;
        }
    }

    // Opcional: Si la IA puede caerse de la isla
    // void OnCollisionExit(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag(groundTag))
    //     {
    //         isGrounded = false;
    //         Debug.Log(gameObject.name + " (IA) ha dejado el suelo.");
    //     }
    // }
}
