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
/// con su sonido, aplicando un retroceso al jugador
/// en función del del lado desde
/// el que el jugador golpee al pato. 
/// Además tiene animación al chocar
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

    [SerializeField] //ANIMATOR DE ADRIÁN
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
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
            _animator.SetTrigger("choque");
        }
    }

} 
