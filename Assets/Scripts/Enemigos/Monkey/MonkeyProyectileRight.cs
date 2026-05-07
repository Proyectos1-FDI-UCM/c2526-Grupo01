//---------------------------------------------------------
// Comportamiento de los proyectiles lanzados por el mono hacia la DERECHA
// Adrián de la Calle
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;



public class MonkeyProyectileRight : MonoBehaviour
{
    [SerializeField] 
    private float speed = 3f;
    [SerializeField] 
    private float timestay = 8f;


    private int direction = 1; // 1 = Derecha -1 = Izquierda
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        
        rb.linearVelocity = new Vector2(direction * speed, 0f);

        Destroy(gameObject, timestay); //El proyectil desaparece tras un tiempo
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Vector2.right * 5f * direction;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null) Destroy(gameObject); //Si choca con el player, desaparece
    }
} // class MonkeyProyectile 
// namespace
