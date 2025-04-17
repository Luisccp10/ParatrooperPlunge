using UnityEngine;

public class PlayerMovementRotated : MonoBehaviour
{
    public float moveSpeed = 5f;
    public string groundTag = "Ground"; // Etiqueta del objeto que representa la isla

    private Rigidbody rb;
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

        // Asegurarse de la rotación inicial (si no se hizo en el Inspector)
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        isGrounded = false; // Asegurar que no esté grounded al inicio
    }

    void FixedUpdate()
    {
        if (!isGrounded)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Debug.Log("Horizontal Input: " + moveHorizontal);
            Debug.Log("Vertical Input: " + moveVertical);

            Vector3 movement = transform.forward * moveVertical + transform.right * moveHorizontal;
            movement = movement.normalized * moveSpeed;

            rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z); // <--- ¡Aquí está el cambio!

            // Opcional: Rotación horizontal al moverse
            if (moveHorizontal != 0)
            {
                float rotationSpeed = 100f * Time.fixedDeltaTime * moveHorizontal;
                rb.angularVelocity = new Vector3(0f, rotationSpeed, 0f);
            }
            else
            {
                rb.angularVelocity = Vector3.zero;
            }
        }
        else
        {
            rb.linearVelocity = Vector3.zero; // <--- ¡Y aquí también!
            rb.angularVelocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisión fue con un objeto etiquetado como "Ground"
        if (collision.gameObject.CompareTag(groundTag))
        {
            isGrounded = true;
            Debug.Log(gameObject.name + " ha aterrizado.");
            // Opcional: Puedes desactivar este script aquí si ya no necesitas el movimiento
            // enabled = false;
        }
    }

    // Opcional: Si el jugador puede caerse de la isla
    // void OnCollisionExit(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag(groundTag))
    //     {
    //         isGrounded = false;
    //         Debug.Log(gameObject.name + " ha dejado el suelo.");
    //     }
    // }
}