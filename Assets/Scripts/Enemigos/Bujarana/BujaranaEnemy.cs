//---------------------------------------------------------
// Enemigo que persigue al jugador cuando entra en su zona y vuelve a su posicion inicial cuando el jugador se va tambien empuja al jugador cuando chocan.
// Darío Pérez Zamorano
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class BujaranaEnemy : MonoBehaviour
{
    // referencia al jugador
    [SerializeField] private Transform player;

    // velocidad del enemigo
    [SerializeField] private float speed = 2f;

    // posicion inicial del enemigo (para volver cuando el jugador se va)
    private Vector3 homeposition;

    // indica si el jugador esta dentro de la zona
    private bool playerInZone = false;

    // fuerza del empujon en X
    [SerializeField] private float knockbackForceX = 6f;

    // fuerza del empujon en Y
    [SerializeField] private float knockbackForceY = 4f;

    void Start()
    {
        // guardamos la posicion inicial al empezar
        homeposition = transform.position;
    }

    void Update()
    {
        // si el jugador esta en la zona y existe
        if (playerInZone && player != null)
        {
            // calculamos la direccion hacia el jugador
            Vector2 direction = (player.position - transform.position).normalized;

            // movemos el enemigo hacia el jugador
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
        else
        {
            // si no, vuelve a su posicion inicial poco a poco
            transform.position = Vector2.MoveTowards
                (transform.position, homeposition, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // intentamos coger el script del jugador
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        // si es el jugador
        if (player != null)
        {
            // calculamos direccion entre enemigo y jugador
            Vector2 direction = collision.transform.position - transform.position;

            // esto nos dice si el jugador esta a la izquierda o derecha
            float pushX = Mathf.Sign(direction.x);

            // aplicamos el empujon al jugador
            player.ApplyKnockback(pushX * knockbackForceX, knockbackForceY);
        }
    }

    // se llama cuando el jugador entra en la zona
    internal void PlayerEnteredZone()
    {
        playerInZone = true;
    }

    // se llama cuando el jugador sale de la zona
    internal void PlayerLeftZone()
    {
        playerInZone = false;
    }
}
