//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using



public class MonkeyProyectile : MonoBehaviour
{
    [SerializeField] 
    private float speed = 3f;
    [SerializeField] 
    private float timestay = 8f;

    [SerializeField]
    private float knockbackForceX = 4f;
    [SerializeField]
    private float knockbackForceY = 2f;

    private int direction = 1; // 1 = Derecha -1 = Izquierda
    private SpriteRenderer sprite;


    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        GameObject player = Object.FindAnyObjectByType<PlayerMovement>().gameObject;

        if (player != null)
        {
            if (player.transform.position.x < transform.position.x)
            {
                direction = -1;

                sprite.flipX = true;
            }
        }

        Destroy(gameObject, timestay);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            //calculamos el vector para ver en que dirección viene el player y x donde hay q aplicar la fuercza
            Vector2 direction = collision.transform.position - transform.position;

            //mathf.sign lo q hace es que devuelve solo el signo del número
            //entonces si el jugador viene por la derecha saldra positivo (+1) y si viene por la izquierda saldra negativo (-1)
            float pushX = Mathf.Sign(direction.x);

            //aplicamos el retroceso al jugador
            player.ApplyKnockback(pushX * knockbackForceX, knockbackForceY);
            
            Destroy(gameObject);
        }
    }
} // class MonkeyProyectile 
// namespace
