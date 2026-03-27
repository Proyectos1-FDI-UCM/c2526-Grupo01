//---------------------------------------------------------
// Script para controlar el movimiento del jugador
// Daniel García Andrés, Samuel McDermott y Hector Prous Arroyo con créditos a André Cardoso del canal "Mix and Jam"
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Collision coll;

    private Rigidbody2D rb;

    private animacionprov anim;

    [SerializeField]
    private ParticleSystem sprintParticles;

    //Estadísitcas
    [Space]
    [Header("Stats")]

    //velocidad
    [SerializeField]
    private float speed = 10f;

    //Fuerza (velocidad) del salto
    [SerializeField]
    private float jumpForce = 7f;

    [Space]
    [Header("Sprint")]

    [SerializeField]
    private float sprintSpeed = 17f;

    //Velocidad actual del player.
    [SerializeField]
    private float currentSpeed = 10f;
    [SerializeField]
    private float verticalSpeed = 0f;

    //el tiempo que tarda en llegar a la velocidad máxima del sprint (a más alto mas rápido)
    [SerializeField]
    private float timeAceleration = 60f;

    //el tiempo que tarda en llegar a la velocidad normal
    [SerializeField]
    private float timeDeceleration = 30f;

    [Header("Knockback")]
    private float knockbackLockTimer = 0f;
    [SerializeField] 
    private float knockbackLockTime = 0.15f;


    [Space]
    [Header("Booleanos")]

    public bool canMove = true;
    public bool isDashing = false;
    public bool isSprinting = false;
    public bool wasSprinting = false;


    [Space]

    private bool groundTouch; //para detectar si esta en el suelo
    private bool hasDashed;  //para detectar si ya ha dasheado

    private float moveInput;



    //sprint
    [SerializeField]
    private float sprintMultiplier = 1.8f;

    void Start()
    {
        //asignamos el rigidbody del jugador
        rb = GetComponent<Rigidbody2D>();

        //asignamos el script encargado de las colisiones del jugador
        coll = GetComponent<Collision>();

        //asignamos la animacion para voltear el sprite
        anim = GetComponentInChildren<animacionprov>();

        //para los checkpoints (comentad esta línea para comodidad en el testing)
        //transform.position = GameManager.Instance.respawnPoint;

    }

    void Update()
    {
        //Debug.Log("Estoy tocando la pared?:  " + coll.onWall);


        //MOVIMIENTO HORIZONTAL

        //hay q confirmar que existe el InputManager
        if (!InputManager.HasInstance())
        {
            Debug.LogWarning("no hay InputManager en escena");
        }

        //leeemos input del eje x e y
        float moveX = InputManager.Instance.MovementVector.x;
        float moveY = InputManager.Instance.MovementVector.y;


        Vector2 dir = new Vector2(moveX, moveY);
        moveInput = moveX;

        


        isSprinting = InputManager.Instance.SprintIsPressed();


        //CAMBIO DE ORIENTACIÓN       
        if (moveX < 0)
        {
            anim.Flip(-1);
        }
        else if (moveX > 0)
        {
            anim.Flip(1);
        }




        if (coll.onGround && !isDashing)
        {
           GetComponent<BetterJumping>().enabled = true;
        }


        if (InputManager.Instance.JumpWasPressedThisFrame()) 
        {
            //Debug.Log("SALTOOOOOOOOOOOO");
            //si esta en el suelo puede saltar
            if (coll.onGround) 
            {
                Jump(Vector2.up);
            }
        }

        //si esta en el suelo, verificamos que todas las booleanas esten bien
        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }


        //Sprint
        if(coll.onGround && InputManager.Instance.SprintIsPressed()) 
        {
            //Debug.Log("sprintttt");
            Sprint();

            if (isSprinting && !wasSprinting)
            {
                sprintParticles.Play();
            }

            wasSprinting = isSprinting;
        }

        //si suelta el botón de sprintar volvemos a la velociad normal
        if (!isSprinting) 
        {
            //currentSpeed = speed;
            currentSpeed = Mathf.MoveTowards(currentSpeed,speed, timeDeceleration * Time.deltaTime);
            sprintParticles.Stop();
        }

        //x si no estan las particulas
        if (isSprinting && coll.onGround)
        {
            if (!sprintParticles.isPlaying)
                sprintParticles.Play();
        }

        //Para que giren las particulas
        var shape = sprintParticles.shape;

        if (InputManager.Instance.MovementVector.x > 0)
        {
            shape.rotation = new Vector3(0, 180, 0);
        }
        else if (InputManager.Instance.MovementVector.x < 0)
        {
            shape.rotation = new Vector3(0, 0, 0);
        }


    }

    private void FixedUpdate()
    {
 
            Walk(moveInput);
    
    }

    //método para que el personaje ande, le pasamos la dirección x parámetro
    private void Walk(float moveX)
    {
        // rb.linearVelocity = (new Vector2(moveX * currentSpeed, rb.linearVelocity.y));
       
        if (knockbackLockTimer > 0)
        {
            knockbackLockTimer -= Time.deltaTime;

            // NO tocamos la X mientras dura el knockback
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(moveX * currentSpeed, rb.linearVelocity.y);
        }

    }


    //método para saltar
    private void Jump(Vector2 dir)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, 0);
        rb.linearVelocity += dir * jumpForce;
    }


    private void Sprint()
    {
        // currentSpeed = sprintSpeed;

        float targetSpeed = sprintSpeed;

        //aceleración progresiva antes del sprint
        currentSpeed = Mathf.MoveTowards(currentSpeed,targetSpeed, timeAceleration * Time.deltaTime);
    }


    //método para saber que el jugador a tocado el suelo, (para el dash)
    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
    }









    //funcion para aplicar el retroceso causado x los enemigos
    public void ApplyKnockback(float forceX, float forceY)
    {
        float dir = Mathf.Sign(transform.localScale.x);

        rb.linearVelocity = new Vector2(forceX * dir, forceY);

        knockbackLockTimer = knockbackLockTime;
    }













    public void DeadZone()
    {
        System.GC.Collect();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MUERTE");
        System.GC.Collect();
    }


}

