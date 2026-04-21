//---------------------------------------------------------
// Sript para teletransportar al jugador a otra escena cuando entra por una puerta
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
// Sript para teletransportar al jugador a otra escena cuando entra por una puerta
/// </summary>
public class ChangeLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        PlayerMovement juagador = collision.GetComponent<PlayerMovement>();

        if (juagador != null)
        {
           UnityEngine.SceneManagement.SceneManager.LoadScene("Salida");
        }

    }

} 
