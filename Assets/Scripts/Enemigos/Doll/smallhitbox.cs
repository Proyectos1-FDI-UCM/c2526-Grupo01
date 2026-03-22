//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class smallhitbox : MonoBehaviour
{
    [SerializeField]
    private float knockbackForceX = 6f;

    [SerializeField]
    private float knockbackForceY = 4f;

    [SerializeField]
    private float attackduration = 1f;

    public void Start()
    {
        Invoke(nameof(destroySelf), attackduration);
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
        }
    }
    private void destroySelf()
    {
        Destroy(gameObject);
    }
}
