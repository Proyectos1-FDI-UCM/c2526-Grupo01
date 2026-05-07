//---------------------------------------------------------
// Archivo encargado de gestionar las zonas en las que el jugador puede morir
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
///Scrpit encargado de gestionar las zonas en las que el jugador puede morir
///como acantilados con grandes caídas por ejemplo
/// </summary>
public class Deadzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement jugador = collision.GetComponent<PlayerMovement>();

        //si el jugador entra en contacto con la deadzone muere y sale el menu principal
        if (jugador != null) 
        {
            jugador.DeadZone();
        }

    }

} 
