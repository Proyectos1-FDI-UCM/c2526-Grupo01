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
/// al gameManager para que luego en el start() del player se spawnee ahí
/// </summary
/// 
public class Checkpoint : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        //asigno el animator del hijo
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("usado", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            anim.SetBool("usado", true);

           GameManager.Instance.SetRespawnPoint(transform.position);
           

        }
    }
} 
