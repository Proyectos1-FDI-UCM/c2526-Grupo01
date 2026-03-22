//---------------------------------------------------------
// Script para el movimiento de la cámara continuo con el jugador
// Darío Pérez Zamorano
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;


/// <summary>
///Lógica básica para el movimiento de la cámara, que sigue al jugador manteniendo siempre su posición en el eje X como referencia, pero con un pequeño retraso en el movimiento.
///Este retardo genera un desplazamiento más suave y natural, evitando que la cámara se mueva de forma brusca y mejorando la sensación de fluidez
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 6f;

    [SerializeField]
    public float alturalimitearriba = 2f;

    [SerializeField]
    public float alturalimiteabajo = 2f;

    [SerializeField]
    public float alturabase = 3f;


    void LateUpdate()          //late update, para que la cámara no se mueva instantáneamente
    {
        if (player == null) return; //es una línea de seguridad para que si no hay jugador activo no se rompa nada.
        Vector3 targetPosition = new Vector3(player.position.x,transform.position.y,-10);

        // CONTROL VERTICAL CON LÍMITES
        if (player.position.y > transform.position.y + alturalimitearriba)
        {
            targetPosition.y = player.position.y;
        }
        else if (player.position.y < transform.position.y - alturalimiteabajo)
        {
            targetPosition.y = player.position.y + alturabase;
        }

        // MOVIMIENTO FINAL (solo una vez)
        transform.position = Vector3.Lerp(transform.position,targetPosition,smoothSpeed * Time.deltaTime);
    }
} // class Camera 
// namespace
