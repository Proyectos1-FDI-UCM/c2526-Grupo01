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


    //Partículas
    [Space]
    [Header("Partículas")]


    [SerializeField]
    private ParticleSystem sprintParticles;

    [SerializeField]
    private ParticleSystem dashParticles;

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

    //settea si el jugador está agarrando/empujando algo
    private bool isGrabbing = false;


    [Space]

    private bool groundTouch; //para detectar si esta en el suelo
    private bool hasDashed;  //para detectar si ya ha dasheado

    private float moveInput;

    //esta booleana es para saber si el jugador esta mirando a la izquierda o a la derecha
    private bool isRight;


    //Dash
    [Space]
    [Header("Dash")]
    [SerializeField]
    private float dashSpeed = 25f;
    //[SerializeField] 
    private float dashTime = 0.15f;
    [SerializeField]
    private DashHUD dashUI;
    [SerializeField]
    private AudioSource dashSound;


    //para no tocar la serializble d arriba
    private float dashTimer;
    private Vector2 dashDirection;



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

        //si no esta dasheando y no ha dasheado puede hacerlo
        if (InputManager.Instance.DashtWasPressedThisFrame() && !isDashing && !hasDashed)
        {
            //activamos q está dasheando y que ha dasheado (ha pulsado el botón)
            isDashing = true;
            hasDashed = true;

            dashParticles.Play();

            //lo igualo para trabajar con timer y no con la del campo serializable (para q no haya conflictos)
            dashTimer = dashTime;

            //si el jugador mira a la derech
            if (isRight)
            {
                //nos guardamos la dirección
                anim.Flip(1);
                dashDirection = new Vector2(1, 0).normalized;
            }
            else if(!isRight) //esta mirando a la izquierda
            {
                //si no hay input hace el dash hacia donde mira el personaje
                anim.Flip(-1);
                dashDirection = new Vector2(-1, 0).normalized;
            }
        }

        if (coll.onGround && !isDashing)
        {
            hasDashed = false;
        }


        isSprinting = InputManager.Instance.SprintIsPressed();


        //CAMBIO DE ORIENTACIÓN       
        if (moveX < 0)
        {
            anim.Flip(-1);
            isRight = false;
        }
        else if (moveX > 0)
        {
            anim.Flip(1);
            isRight = true;
        }


        if (coll.onGround && !isDashing)
        {
            GetComponent<BetterJumping>().enabled = true;
        }


        if (InputManager.Instance.JumpWasPressedThisFrame())
        {
            //si esta en el suelo puede saltar
            if (coll.onGround && rb.linearVelocityY <= 0)
            {
                Jump(Vector2.up);
            }
        }



        //Sprint
        if (coll.onGround && InputManager.Instance.SprintIsPressed())
        {
            Sprint();

            if (isSprinting && !wasSprinting)
            {
                //esto es para activar las particulas
                sprintParticles.Play();
            }

            wasSprinting = isSprinting;
        }

        //si suelta el botón de sprintar volvemos a la velociad normal
        if (!isSprinting)
        {
            //currentSpeed = speed;
            currentSpeed = Mathf.MoveTowards(currentSpeed, speed, timeDeceleration * Time.deltaTime);
            sprintParticles.Stop();
        }

        //x si no estan las particulas
        if (isSprinting && coll.onGround)
        {
            if (!sprintParticles.isPlaying)
                sprintParticles.Play();
        }

        flipParticles();

    }

    private void FixedUpdate()
    {
        //si ha pulsado el dash, dashea
        if (isDashing)
        {
            Dash();
        }
        else
        {
            Walk(moveInput);
        }

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
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, timeAceleration * Time.deltaTime);
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

    void Dash()
    {
        //esto es para que no se repita muchas veces y no distorsione
        if (!dashSound.isPlaying)
        {
            dashSound.Play();
        }

        dashTimer = dashTimer - Time.deltaTime;

        //velocidad de dash
        rb.linearVelocity = dashDirection * dashSpeed;

        //quito la gravedad durante dash para que vaya recto
        rb.gravityScale = 0f;

        if (dashTimer <= 0)
        {
            isDashing = false;

            //restauro la gravedad
            rb.gravityScale = 1f;

        }


    }


    //esto es para la interfaz del dsash
    public bool canDashUI()
    {
        if (!isDashing && !hasDashed && dashTimer <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }


    }


    private void flipParticles()
    {
        //Para que giren las particulas
        var shape = sprintParticles.shape;
        var shapeDash = dashParticles.shape;

        if (InputManager.Instance.MovementVector.x > 0)
        {
            shape.rotation = new Vector3(0, 180, 0);
            shapeDash.rotation = new Vector3(0, 180, 0);
        }
        else if (InputManager.Instance.MovementVector.x < 0)
        {
            shape.rotation = new Vector3(0, 0, 0);
            shapeDash.rotation = new Vector3(0, 0, 0);
        }
    }

    //getter q vuelve la velocidad horizontal actual del jugador (se usa en la caja)
    public float GetCurrentVelX()
    {
        return rb.linearVelocity.x;
    }


    public void SetGrabbing(bool grabbing)
    {
        isGrabbing = grabbing;
    }


}
