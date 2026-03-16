//---------------------------------------------------------
// Script para controlar el movimiento del jugador
// Daniel García Andrés, Samuel McDermott y Hector Prous Arroyo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //velocidad configurable en el editor
    [SerializeField]
    private float speed = 10f;

    //velocidad normal del jugador sin sprint
    private float baseSpeed = 10f;

    //Velocidad actual del player.
    private float currentSpeed = 0f;

    [SerializeField]
    private float sprintAcceler = 5f;

    //Grado de aceleración inicial.
    [SerializeField]
    private float walkAcceler = 15f;

    //Grado de frenado final.
    [SerializeField]
    private float walkDeceler = 15f;

    //gravedad al presionar
    [SerializeField] 
    private float gravityUp = 20f;

    //gravedad al no presionar
    [SerializeField] 
    private float gravityDown = 40f;

    //velocidad del salto
    [SerializeField]
    private float jumpSpeed = 7f; 

    //velocidad maxima de caida
    [SerializeField]
    private float maxFallSpeed = 15f;

    //coyotetime (salto despues de tocar el suelo)
    [SerializeField]
    private float coyoteTime = 0.15f;

    //jumpbuffer (salto antes de tocar el suelo)
    [SerializeField]
    private float jumpBufferTime = 0.2f;

    private float coyoteTimeCounter;

    private float jumpBufferCounter;

    private bool isSprinting = false;

    private Vector3 respawnPoint;



    private float verticalSpeed = 0f;

    //referencia al trigger del los pies del jugador
    [SerializeField]
    private GroundCheck groundCheck;
    //groundCheck.grouended variable para saber si esta el jugador en el suelo o no

    [SerializeField]
    private WallChecker wallChecker;


    //sprint
    [SerializeField]
    private float sprintMultiplier = 1.8f;

    void Start()
    {
        transform.position = GameManager.Instance.respawnPoint;
    }

    void Update()
    {

        //Debug.Log("El jugador esta en el suelo?: " + groundCheck.grounded);
        
        //MOVIMIENTO HORIZONTAL

        //hay q confirmar que existe el InputManager
        if (!InputManager.HasInstance())
        {
            Debug.LogWarning("no hay InputManager en escena");
            return;
        }

        //leeemos input del eje x
        float moveX = InputManager.Instance.MovementVector.x;


        //CAMBIO DE ORIENTACIÓN       
        if (moveX < 0)

        {
            transform.rotation = Quaternion.Euler(0, -180, 0);

        }
        else if (moveX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }



        if (moveX != 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveX * speed, walkAcceler * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, walkDeceler * Time.deltaTime);
        }

        // nos movemos el función del input
        transform.position += new Vector3(currentSpeed * Time.deltaTime, 0f, 0f);

        //SALTO

        //CoyoteTime
        if (groundCheck.grounded == true)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // JumpBuffer 
        if (InputManager.Instance.JumpWasPressedThisFrame())
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        //si el jugador ha pulsado el botón d salto y esta en el suelo
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            verticalSpeed = jumpSpeed;
            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
        }

        // PARED

        if (wallChecker.isTouchingWall == true)
        {

            if ((transform.eulerAngles.y == 0 && currentSpeed > 0) ||
                (transform.eulerAngles.y != 0 && currentSpeed < 0))
            {
                currentSpeed = 0f;
            }
        }

        //afecta la gravedad
        
        bool jumpHeld = InputManager.Instance.JumpIsPressed();
        float gravity;

        if (verticalSpeed > 0 && jumpHeld)
        {
            gravity = gravityUp;
        }
        else
        {
            gravity = gravityDown;
        }

        verticalSpeed = verticalSpeed - (gravity * Time.deltaTime);

        //maxima velocidad de caida
        if (verticalSpeed < -maxFallSpeed)
        {
            verticalSpeed = -maxFallSpeed;
        }

        //si esta tocando suelo y sigue cayendo, cortamos la caida
         if (groundCheck.grounded == true && verticalSpeed < 0f)
         {
             verticalSpeed = 0f;
         } 

        //aplicamos movimiento vertical manual
        Vector3 pos = transform.position;
        pos.y += verticalSpeed * Time.deltaTime;

        transform.position = pos;

        //SPRINT

        //desactivacion y limitacion del sprint en el aire

        if (InputManager.Instance.SprintWasPressedThisFrame() && groundCheck.grounded)
        {
            isSprinting = true;
        }
        if (InputManager.Instance.SprintWasReleasedThisFrame())
        {
            isSprinting = false;
        }

        //aceleracion del sprint
        float targetSpeed;
        if (isSprinting)
        {
            targetSpeed = baseSpeed * sprintMultiplier;
        }
        else
        {
            targetSpeed = baseSpeed;
        }
        speed = Mathf.MoveTowards(speed, targetSpeed, sprintAcceler * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("estoy tocando: " + collision.gameObject.name);
    }

    //funcion para aplicar el retroceso causado x los enemigos
    public void ApplyKnockback(float forceX, float forceY)
    {
        currentSpeed = forceX;
        verticalSpeed = forceY;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Final"))
        {
            //lo mismo con esto
            UnityEngine.SceneManagement.SceneManager.LoadScene("Salida");
        }
    }

    //igual es mejor ponerlo como privado en Deadzone.cs pero no lo se
    //***DUDA***
    public void DeadZone()
    {
        System.GC.Collect();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MUERTE");
        System.GC.Collect();
    }


}

