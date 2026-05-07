//---------------------------------------------------------
// Sensor de visión del robot
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Script que define el campo de visión del robot utilizando 
/// un collider configurado como Trigger para detectar
/// cuándo el jugador entra o sale del área de visión.
///
/// Cuando el jugador entra en el trigger:
/// - Se guarda su Transform como objetivo.
///
/// Cuando el jugador sale del trigger:
/// - El objetivo se elimina.
///
/// Otros scripts del enemigo pueden consultar este objetivo
/// para saber si el jugador está siendo detectado.
/// </summary>
public class VisionSensor : MonoBehaviour
{
    //guardamos el objetivo (q en este caso será el jugador)
    private Transform target;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player != null)
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //si ya no ve al jugador le metemos el target null
        if (target != null && other.transform == target)
        {
            target = null;
        }
    }

    public Transform GetTarget()
    {
        return target;
    }

} 
