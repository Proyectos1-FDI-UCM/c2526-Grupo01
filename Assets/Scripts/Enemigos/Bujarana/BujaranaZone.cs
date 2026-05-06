//---------------------------------------------------------
// Zona invisible que detecta cuando el jugador entra o sale y avisa al enemigo para que empiece o deje de perseguirlo.
// Darío Pérez Zamorano
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

/*
Define la zona de detección del enemigo Bujarana.

Detecta cuándo el jugador entra o sale de un área determinada (trigger)
y notifica al enemigo para que comience o detenga la persecución.
*/

public class BujaranaZone : MonoBehaviour
{
    // referencia al enemigo (la asignamos desde unity)
    [SerializeField] private BujaranaEnemy bujarana;

    // referencia al jugador
    [SerializeField] private Transform player;

    // se ejecuta cuando algo entra en la zona (trigger)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // comprobamos si el que entra es el jugador
        if (collision.transform == player)
        {
            // avisamos al enemigo de que el jugador ha entrado
            bujarana.PlayerEnteredZone();
        }
    }

    // se ejecuta cuando algo sale de la zona
    private void OnTriggerExit2D(Collider2D collision)
    {
        // comprobamos si el que sale es el jugador
        if (collision.transform == player)
        {
            // avisamos al enemigo de que el jugador se ha ido
            bujarana.PlayerLeftZone();
        }
    }
} // class BujaranaZone 
// namespace
