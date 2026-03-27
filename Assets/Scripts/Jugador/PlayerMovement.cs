//---------------------------------------------------------
// Script para controlar el movimiento del jugador
// Daniel García Andrés, Samuel McDermott y Hector Prous Arroyo
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;


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

    //Fuerza (velocidad) del salto
    [SerializeField]
    private float jumpForce = 7f; 

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
        //asignamos el rigidbody del jugador
        rb = GetComponent<Rigidbody2D>();

        //transform.position = GameManager.Instance.respawnPoint;
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

        //leeemos input del eje x e y
        float moveX = InputManager.Instance.MovementVector.x;
        float moveY = InputManager.Instance.MovementVector.y;

        Vector2 dir = new Vector2(moveX, moveY);

        Walk(dir);


        //CAMBIO DE ORIENTACIÓN       
        if (moveX < 0)

        {
            transform.rotation = Quaternion.Euler(0, -180, 0);

        }
        else if (moveX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }



        //SALTO
        if (groundCheck.grounded == true && InputManager.Instance.JumpWasPressedThisFrame())
        {
            Jump();
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

    }


    //método para que el personaje ande, le pasamos la dirección x parámetro
    private void Walk(Vector2 dir)
    {
        rb.linearVelocity = (new Vector2(dir.x * speed, rb.linearVelocity.y));
    }



    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, 0);
        rb.linearVelocity += Vector2.up * jumpForce;
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


    public void DeadZone()
    {
        System.GC.Collect();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MUERTE");
        System.GC.Collect();
    }


}

