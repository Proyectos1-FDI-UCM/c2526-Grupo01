//---------------------------------------------------------
// Comprotamiento del enemigo Robot
// Daniel García Andrés
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Runtime.CompilerServices;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Comprotamiento del enemigo Robot: Este enimgo va patrullando de derecha
/// a izquierda, si entra en visión con el jugador (detectado con un trigger)
/// entonces persigue al enemigo a una mayor velocidad "chaseSpeed" mientras se encuentre bajo su visión,
/// cuando el jugador escapa de su rango de visión el robot regresa a su comportamiento original,
/// es decir, que baja su velocidad hasta "patrolSpeed" y sigue moviendose de derecha a izquierda.
/// </summary>
public class RobotEnemy : MonoBehaviour
{

    private Animator anim;

    [Space]
    [Header("Configuración del Robot")]

    //velocidad de patrullaa
    [SerializeField]
    private float patrolSpeed = 3.5f;
    //velocidad de persecución
    [SerializeField]
    private float chaseSpeed = 8f;

    //cada cuanto tiempo se gira patrullando
    [SerializeField]
    private float patrolTurnTime = 2.0f;

    [SerializeField]
    private VisionSensor vision;


    [Header("Retroceso al jugador")]

    [SerializeField]
    private float knockbackForceX = 6f;

    [SerializeField]
    private float knockbackForceY = 4f;


    [Space]
    [Header("Tiempo del robot parado")]

    [SerializeField]
    private float cooldownAfterHit = 3f;

    private float dir = 1f;
    private float timer = 0f;

    private float patrolSpeedDefault;
    private float chaseSpeedDefault;

    //el robot tras chocar se queda estuneado durante unos segundos
    private bool stunned = false;
    //esto es para q el sprite no gire cuando esta stunned
    private Quaternion lockedRotation;

    private Rigidbody2D rb;

    private float temporizador;


    private void Start()
    {
        patrolSpeedDefault = patrolSpeed;
        //aqui estaban las 2 inicializadas a patrolSpeed x eso perdía velocidad
        chaseSpeedDefault = chaseSpeed;
        stunned = false;
        rb = GetComponent<Rigidbody2D>();
        temporizador = 10000f;
        //incializo el animator
        anim = GetComponentInChildren<Animator>();

        //x defecto esta la de modo patrulla, aunque debería entrar igual
        anim.SetBool("modoPersecucion", false);
        anim.SetBool("parado", false);
    }


    void FixedUpdate()
    {
        //si ha pasado el tiempo del cooldown podemos quitarle el stun
        if (temporizador <= Time.time) 
        { 
            EndStun();   
        }

        Transform target = vision.GetTarget();

        //si ve al jugador lo persique
        if (vision != null && target != null)
        {
            //activo la animación de perseguir
            anim.SetBool("modoPersecucion", true);

            //calculamos la dirección de donde esta el jugador (izquierda o derecha)
            float dx = target.position.x - transform.position.x;
            dir = Mathf.Sign(dx);

            //se mueve el robot hacia el jugador (solo en el eje x claro)
            rb.linearVelocity = new Vector2(dir * chaseSpeed, rb.linearVelocity.y);

            //giramos
            if (dir < 0)
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else if (dir > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else //si no ve al jugador patrulla (anda d lado a lado)
        {
            //animación en modo patrulla
            anim.SetBool("modoPersecucion", false);

            //si no ve al jugador sigue patrullando
            timer = timer + Time.deltaTime;

            //si el temporizador (timer) supera los segundos de patrolTurnTime 
            //(el tiempo que tarda en girar patrollando) le damos la vuelta y reinciamos el temporizador
            if (timer >= patrolTurnTime)
            {
                timer = 0f;
                dir = dir * (-1f);
            }

            //movemos al robot
            rb.linearVelocity = new Vector2(dir * patrolSpeed, rb.linearVelocity.y);

            //y giramos el sprite
            if (dir < 0)
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            else if (dir > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            //para que no gire el sprite si deberia estar quieto
            if (stunned)
            {
                transform.rotation = lockedRotation;
            }
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            //calculamos el vector para ver en que dirección viene el player y x donde hay q aplicar la fuercza
            Vector2 direction = collision.transform.position - transform.position;

            //mathf.sign lo q hace es que devuelve solo el signo del número
            //entonces si el jugador viene por la derecha saldra positivo (+1) y si viene por la izquierda saldra negativo (-1)
            float pushX = Mathf.Sign(direction.x);


            //aplicamos el retroceso al jugador
            player.ApplyKnockback(pushX * knockbackForceX, knockbackForceY);

            //reproducimos el sonido del robot cuando choca el jugador
            //robot.Play();



            //paramos al robot durante unos segundos tras tocarlo
            if (!stunned) 
            {
                anim.SetBool("parado", true);

                stunned = true;
                //guardamos la direccion del sprite
                lockedRotation = transform.rotation;
                rb.linearVelocity = Vector2.zero;
                patrolSpeed = 0f;
                chaseSpeed = 0f;

                //me guardo el momento en que el jugador toco al robot en una variable para luego comprobarlo en el fixedUpdate
                temporizador = Time.time + cooldownAfterHit;

            }  
        }
    }

    //finaliza el stun del robot y continua como de costummbre
    private void EndStun() 
    {
        anim.SetBool("modoPersecucion", false);
        anim.SetBool("parado", false);

        patrolSpeed = patrolSpeedDefault;
        chaseSpeed = chaseSpeedDefault;
        stunned = false;

    } 
} 