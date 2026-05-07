//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Leopoldo Gutiérrez Cobo (Adaptado de CameraFollow de Hector Abdón Prous Arroyo)
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Código específico para la cámara del boss.
/// Contiene:
/// - Lógica de cámara base
/// - Método que cambia el encuadre y la altura de la cámara del boss.
/// </summary>
public class CameraBoss : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Suavizado")]
    [SerializeField] private float smoothCamSpeedX = 6f;//velocidad del recorrido de la camara a la posicion del jugador en el eje X (suavizado)

    [Header("Deadzone vertical")]
    [SerializeField] private float limitUp = 1.19f; //distancia desde el origen (centro) hasta el jugador donde el movimiento no se aplica ( hacia arriba)
    [SerializeField] private float limitDown = -15.14f; //distancia desde el origen (centro) hasta el jugador donde el movimiento no se aplica (hacia abajo)

    [SerializeField]
    private float limitDownBoss = -1.16f;
    [SerializeField]
    private float limitUpBoss = -10.42f;

    [Header("Lookahead dash")]
    [SerializeField] private float dashCamDistance = 3f; //distancia a la que se dirigira la camara en caso de que se detecte un dash
    [SerializeField] private float dashCamSpeed = 10f; //velocidad de la camara durante y despues del dash con direccion a la dashCamDistance y posteriormente al jugador nuevamente



    //Campos para hacer el zoom out
    [Header("Zoom Out Boss")]
    [SerializeField]
    private float interp;
    [SerializeField]
    private float fieldIni = 4f;



    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    //Componente de la cámara en la escena
    private Camera cam;

    //Componentes del código original
    private float recorridoDashCam = 0f;
    private int dirPlayer = 1;


    
    
    //Variables para la transición de encuadre
    private float zoomT = 0f;
    private float startSize;
    private float targetSize;
    private bool zooming = false;
    private float zoomLimit = 1f;


    #endregion

   




    void Start()
    {
        //Inicializamos cámara y encuadre inicial
        cam = GetComponent<Camera>();
        cam.orthographicSize = fieldIni;
        Debug.Log("se cambia");

       
    }

    void LateUpdate()   //late update necesario para la camara (movimiento despues del calculo de fisicas y elementos)
    {
        //Condicional que hace con el update el cambio de encuadre
        if (zooming)
        {
            //Se guarda el frame actual multiplicado por el valor elegido
            zoomT += Time.deltaTime * interp;

            //se hace el cambio con un smooth
            cam.orthographicSize = Mathf.Lerp(startSize, targetSize, zoomT);

            
            //Evitamos que zoomee más de lo deseado
            if (zoomT >= zoomLimit)
            {
                zooming = false;
            }
        }

        //Movimiento genérico del código de la cámara
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




    //metodo de cambio de tamaño ortografico
    public void ChangeField(float size)
    {
        startSize = cam.orthographicSize;
        targetSize = size;
        zoomT = 0f;
        zooming = true;
    }



    // Getters y setters para usar los atributos en el CameraTrigger

    //Getters
    public float GetLimitUp()
    {
        return limitUp;
    }
    public float GetLimitDown()
    {
        return limitDown;
    }

    //Setters
    public void SetLimitUp(float up)
    {
        limitUp = up;
    }
    public void SetLimitDown(float down)
    {
        limitDown = down;
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

} // class CameraBoss 
// namespace
