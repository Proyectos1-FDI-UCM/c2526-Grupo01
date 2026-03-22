//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class BujaranaZone : MonoBehaviour
{
    public BujaranaEnemy bujarana;
    public Transform player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == player)
        {
            bujarana.PlayerEnteredZone();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.transform == player)
        {
            bujarana.PlayerLeftZone();
        }
    }
} // class BujaranaZone 
// namespace
