//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using



public class MonkeyProyectileLeft : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float timestay = 8f;


    private int direction = -1; // 1 = Derecha -1 = Izquierda
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        rb.linearVelocity = new Vector2(direction * speed, 0f);

        Destroy(gameObject, timestay);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Vector2.right * 5f * direction;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null) Destroy(gameObject);
    }
} // class MonkeyProyectile 
// namespace
