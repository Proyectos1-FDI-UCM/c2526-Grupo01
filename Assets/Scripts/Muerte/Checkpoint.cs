//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Comportamiento de los checkpoints que pasan su ubicación
/// al gameManager para que luego en el start() del player se spawnee ahi
///
/// </summary>
public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            //luego aqui igual poner por

            GameManager.Instance.SetRespawnPoint(transform.position);
        }
    }
} 
