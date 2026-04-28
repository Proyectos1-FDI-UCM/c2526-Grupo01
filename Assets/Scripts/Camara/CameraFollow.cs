//---------------------------------------------------------
// Script para el movimiento de la cámara continuo con el jugador
// Darío Pérez Zamorano y Hector Prous
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
    [Header("Referencias")]
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Suavizado")]
    [SerializeField] private float smoothCamSpeedX = 6f;//velocidad del recorrido de la camara a la posicion del jugador en el eje X (suavizado)

    [Header("Deadzone vertical")]
    [SerializeField] private float limitUp = 1f; //distancia desde el origen (centro) hasta el jugador donde el movimiento no se aplica ( hacia arriba)
    [SerializeField] private float limitDown = 1.5f; //distancia desde el origen (centro) hasta el jugador donde el movimiento no se aplica (hacia abajo)

    [Header("Lookahead dash")]
    [SerializeField] private float dashCamDistance = 3f; //distancia a la que se dirigira la camara en caso de que se detecte un dash
    [SerializeField] private float dashCamSpeed = 10f; //velocidad de la camara durante y despues del dash con direccion a la dashCamDistance y posteriormente al jugador nuevamente

    private float recorridoDashCam = 0f;
    private int dirPlayer = 1;

    void LateUpdate()   //late update necesario para la camara (movimiento despues del calculo de fisicas y elementos)
    {
        if (playerMovement != null)
        {
            UpdateDashCam();
            UpdateCameraPosition();
        }
    }

    private void UpdateDashCam()
    {
        //si el getter de playermovement es true, activar logica
        if (playerMovement.GetIsDashing()) 
        {
            recorridoDashCam = Mathf.Lerp(recorridoDashCam, dirPlayer * dashCamDistance, dashCamSpeed * Time.deltaTime);
        }
        else
        {
            recorridoDashCam = Mathf.Lerp(recorridoDashCam, 0f, dashCamSpeed * Time.deltaTime);

            //si el jugador no esta quieto, guardar su direccion en una variable comparando el signo de la velocidad de playerMovement
            if (Mathf.Abs(playerMovement.GetCurrentVelX()) > (0.1f)) 
            {
                if (playerMovement.GetCurrentVelX() > 0)
                {
                    dirPlayer = 1;
                }
                else
                {
                    dirPlayer = -1;
                }
            }
        }

    }

    private void UpdateCameraPosition()
    {
        float diffY = playerMovement.transform.position.y - transform.position.y; 
        float camY = transform.position.y; 
        float smoothY;

        //asignamos a una variable cual va a ser el movimiento de la camara respecto al jugador cuando se supere o se rebaje un limite (inspirado en celeste)
        if (diffY > limitUp)
        {
            camY = playerMovement.transform.position.y - limitUp; //el jugador va hacia arriba del limite, la camara lo sigue un poco mas por abajo (no en su centro)
        }
        else if (diffY < -limitDown)
        {
            camY = playerMovement.transform.position.y + limitDown; //el jugador va hacia abajo del limite, la camara lo sigue un poco mas por arriba (no en su centro)
        }

        if (diffY > 0)
        {
            smoothY = smoothCamSpeedX * 0.5f; //hacia arriba mas lento el smooth de la camara
        }
        else
        {
            smoothY = smoothCamSpeedX; //hacia abajo mas rapido el smooth (misma logica que la camara del celeste, de donde nos hemos inspirado)
        }
        //logica de movimiento vertical final con lerp
        float smoothCamY = Mathf.Lerp(transform.position.y, camY, smoothY * Time.deltaTime);
        //logica de movimiento horizontal final con lerp
        float camX = playerMovement.transform.position.x + recorridoDashCam;
        float smoothCamX = Mathf.Lerp(transform.position.x, camX, smoothCamSpeedX * Time.deltaTime);
        //hacemos transform de la camara con los parametros finales
        transform.position = new Vector3(smoothCamX, smoothCamY, -10f);
    }
}
 // class Camera 
// namespace
