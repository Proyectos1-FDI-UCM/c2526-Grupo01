//---------------------------------------------------------
// Comportamiento de los checkpoints
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Comportamiento de los checkpoints que pasan su ubicación
/// al gameManager para que luego en el start() del player el jugador se spawnee ahí
/// </summary
/// 
public class Checkpoint : MonoBehaviour
{
    private Animator anim;

    private AudioSource sfx;

    private const float SFX_VOLUME = 1f;

    private void Start()
    {
        //asigno el animator del hijo
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("usado", false);
        sfx = GetComponent<AudioSource>();
        sfx.volume = SFX_VOLUME;  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            sfx.Play();
           //cambio la animación
           anim.SetBool("usado", true);

           //le paso al GM el nuevo punto de reaparición
           GameManager.Instance.SetRespawnPoint(transform.position);
           

        }
    }
} 
