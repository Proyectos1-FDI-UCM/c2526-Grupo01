//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using JetBrains.Annotations;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class BujaranaEnemy : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    private Vector3 homeposition;
    private bool playerInZone = false;

    [SerializeField]
    private float knockbackForceX = 6f;

    [SerializeField]
    private float knockbackForceY = 4f;

    void Start()
    {
        homeposition = transform.position;
    }

    void Update()
    {
        if (playerInZone && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;

        }
        else
        {
            transform.position = Vector2.MoveTowards
                (transform.position, homeposition, speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            // dirección entre enemigo y jugador
            Vector2 direction = collision.transform.position - transform.position;

            // signo para saber izquierda o derecha
            float pushX = Mathf.Sign(direction.x);

            // aplicar knockback al jugador
            player.ApplyKnockback(pushX * knockbackForceX, knockbackForceY);
        }
    }
    public void PlayerEnteredZone()
    {
        playerInZone = true;
    }
   public void PlayerLeftZone()
    {
        playerInZone = false;
    }
    
} 
