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
public class Camera : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;

    [SerializeField]
    public float alturalimitearriba = 5f;

    [SerializeField]
    public float alturalimiteabajo = 5f;

    [SerializeField]
    public float alturabase = 5f;


    void LateUpdate()          //late update, para que la cámara no se mueva instantáneamente
    {
        if (player == null) return; //es una línea de seguridad para que si no hay jugador activo no se rompa nada.
        Vector3 targetPosition = new Vector3
            (
                player.position.x, //X, la cámara sigue la X del jugador horizontalmente.
                transform.position.y,//Y del vector, hace que la cámara no suba cuando saltamos.
                -10 //Z, para posicionar la cámara delante
            );
//SUAVIZADO DE LA CAMARA
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
            );


        /*if (player.position.y >= Mathf.Abs(transform.position.y + alturalimitearriba))//(Mathf.Abs(player.position.y - transform.position.y) > 5f)
        { 
            Vector3 newPos = transform.position;
            newPos.y = Mathf.Lerp(transform.position.y, player.position.y, 5f * Time.deltaTime);
            transform.position = newPos;
        }

        if (player.position.y <= Mathf.Abs(transform.position.y - alturalimiteabajo))//(Mathf.Abs(player.position.y - transform.position.y) > 5f)
        {
            Vector3 newPos = transform.position;
            newPos.y = Mathf.Lerp(transform.position.y, player.position.y + alturabase, 5f * Time.deltaTime);
            transform.position = newPos;
        }*/
    }
} // class Camera 
// namespace
