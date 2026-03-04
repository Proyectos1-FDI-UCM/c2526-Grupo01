//---------------------------------------------------------
// Comportamiento del enemigo pato de goma
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Comportamiento del pato de goma
/// con su sonido, rebotes y pequeña animación
/// </summary>
public class DuckEnemy : MonoBehaviour
{
    [SerializeField]
    private float knockbackForceX = 6f;

    [SerializeField]
    private float knockbackForceY = 4f;

    //para el sonido del quack
    [SerializeField]
    private AudioSource quackAudio;


    //DEFORMAR EL SPRITE, como estrujarlo cuando se toca
    [SerializeField]
    private Transform spriteTransform;
    private Vector3 originalScale;

    [SerializeField]
    private float squashX = 1f;
    [SerializeField] 
    private float squashY = 0.5f;

    private void Start()
    {
        //nos guardamos la escala original del sprite
        originalScale = spriteTransform.localScale; 
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
            
            //reproducimos el sonido del pato cuando choca el jugador
            quackAudio.Play();
            //animacion como de estruje para el pato
            Squash();
        }
    }

    public void Squash() 
    {
        spriteTransform.localScale = new Vector3(squashX, squashY, 1f);
        //el invoke lo que hace es esperar un tiempo y luego llama al metodo
        //en este caso llama a resetear el squash
        Invoke(nameof(ResetSquash), 0.2f);
    }

    void ResetSquash()
    {
        spriteTransform.localScale = originalScale;
    }
} 
