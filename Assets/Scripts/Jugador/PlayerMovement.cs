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

    //velocidad maxima andando
    [SerializeField]
    private float maxWalkSpeed = 10f;

    //ratio de aumento de velocidad al andar
    [SerializeField]
    private float walkAceleration = 40f;

    //Fuerza (velocidad) del salto
    [SerializeField]
    private float jumpForce = 7f;

    [Space]
    [Header("Sprint")]

    [SerializeField]
    private float maxSprintSpeed = 17f;

    //Velocidad actual del player.
    private float currentSpeed = 10f;

    //ratio de aumento de velocidad al sprintar (mayor = llega antes al límite)
    [SerializeField]
    private float sprintAceleration = 60f;

    //rozamiento al soltar el input o al volver de sprint
    [SerializeField]
    private float rozamiento = 30f;

    [SerializeField]
    private float sprintBrakeBoost = 3f;

    [SerializeField]
    private float walkBrakeBoost = 2f;

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


    //Dash
    [Space]
    [Header("Dash")]
    [SerializeField]
    private float dashSpeed = 25f;
    [SerializeField]
    private float dashTime = 0.15f;
    [SerializeField]
    private DashHUD dashUI;
    [SerializeField]
    private AudioSource dashSound;


    //para no tocar la serializble d arriba
    private float dashTimer;
    private Vector2 dashDirection;



    [Header("Push")]
    [SerializeField] private float grabSpeedMultiplier = 0.5f;

    private bool isGrabbing = false;

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

            //dirección del dash en función de donde esta moviendo el joystick o las teclas (el 0.1 es para q no pille dashes fantasmas en la dirección q no es)
            if (Mathf.Abs(moveX) > 0.1f)
            {
                //nos guardamos la dirección
                dashDirection = new Vector2(moveX, 0).normalized;
            }
            else
            {
                //si no hay input hace el dash hacia donde mira el personaje
                dashDirection = new Vector2(Mathf.Sign(transform.localScale.x), 0);
            }
        }

        if (coll.onGround && !isDashing)
        {
            hasDashed = false;
        }


        isSprinting = InputManager.Instance.SprintIsPressed();


        //CAMBIO DE ORIENTACIÓN       
        if (moveX < 0 && !isGrabbing)
        {
            anim.Flip(-1);
        }
        else if (moveX > 0 && !isGrabbing)
        {
            anim.Flip(1);
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
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxWalkSpeed, rozamiento * Time.deltaTime);
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
            return;
        }

        float maxSpeed = isGrabbing ? currentSpeed * grabSpeedMultiplier : currentSpeed;
        float velX = rb.linearVelocity.x;
        float accel = isSprinting ? sprintAceleration : walkAceleration;
        bool hasInput = Mathf.Abs(moveX) > 0.01f;
        bool changingDir = hasInput && Mathf.Sign(moveX) != Mathf.Sign(velX) && Mathf.Abs(velX) > 0.1f;

        float newVelX;
        if (!hasInput)
            newVelX = Mathf.MoveTowards(velX, 0f, rozamiento * Time.fixedDeltaTime);
        else if (changingDir)
        {
            float boost = isSprinting && Mathf.Abs(velX) < maxWalkSpeed ? sprintBrakeBoost : !isSprinting ? walkBrakeBoost : 1f;
            newVelX = Mathf.MoveTowards(velX, 0f, accel * boost * Time.fixedDeltaTime);
        }
        else
            newVelX = Mathf.MoveTowards(velX, moveX * maxSpeed, accel * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector2(newVelX, rb.linearVelocity.y);

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

        float targetSpeed = maxSprintSpeed;

        //aceleración progresiva antes del sprint
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, sprintAceleration * Time.deltaTime);
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
        //esto es para que no se repita muchas veces y no distor
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


    //esto es para la interfaz del dash
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

    // <<METODOS DE PUSHABLE>>
    public void SetGrabbing(bool value)
    {
        isGrabbing = value;
    }

    public float GetCurrentVelX()
    {
        return rb.linearVelocity.x;
    }
}
