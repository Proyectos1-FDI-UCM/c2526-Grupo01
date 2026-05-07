//---------------------------------------------------------
// Comportamiento del AREA DE DAÑO generada por la muñeca
// Adrián de la Calle
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------
using Unity.Mathematics;
using UnityEngine;

public class DollArea : MonoBehaviour
{
    [SerializeField]
    private float knockbackForceX = 6f;

    [SerializeField]
    private float knockbackForceY = 4f;

    [SerializeField]
    private float attackduration = 2f;

    [SerializeField]
    private int noisedamage = 3;

    private float timer = 0f;

    private void FixedUpdate()
    {
        if (timer >= attackduration) Destroy(gameObject);
        else timer += Time.deltaTime;
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

            Noise noise = collision.gameObject.GetComponent<Noise>();
            noise.TakeNoise(noisedamage);
        }
    }
}
