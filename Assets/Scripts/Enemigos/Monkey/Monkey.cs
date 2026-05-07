//---------------------------------------------------------
// Comportamiento del mono, y ciclo de ataques
// Adrián de la Calle
// Coulro
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using System.Runtime.CompilerServices;
public class Monkey : MonoBehaviour
{
    [Header("Sonidos Mono")]

    [SerializeField]
    private AudioClip monkeySound;


    [SerializeField]
    private AudioClip attackSound;

    [SerializeField]
    private AudioClip hitSound;


    //para controlar el volumen de cada sonido
    [SerializeField]
    private float volumeMonkey;

    [SerializeField]
    private float volumeHit;

    [SerializeField]
    private float volumeAttack;

    //para ver cual es el clip actual y hacer que no suene todo el rato
    private AudioClip currentClip;

    private AudioSource audioSource;

    [Space]
    [Header("Atributos Mono")]

    [SerializeField]
    private float descansoAtaques = 6f; //TIEMPO ENTRE ATAQUES

    [SerializeField]
    private float descansoProyectiles = 1f; //TIEMPO ENTRE PROYECTILES

    [SerializeField]
    private GameObject proyectilMonoDerecha;
    [SerializeField]
    private GameObject proyectilMonoIzquierda;

    [SerializeField]
    private int numeroProyectiles = 3;
    [SerializeField]
    private int attacksBeforeJump = 2;

    [SerializeField]
    private int knockbackForceX = 30;
    [SerializeField]
    private int knockbackForceY = 2;

    [SerializeField]
    private float alturaSalto = 10f;
    [SerializeField]
    private float velocidadSalto = 7f;

    private Rigidbody2D rb;

    [SerializeField]
    private MonkeyArea area;

    int i = 0;
    int jump = 0;
    private float timer = 0f;
    private float timerWave = 0f;
    private Vector3 posicionOriginal;

    private bool attacking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posicionOriginal = transform.position;
        audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (jump == attacksBeforeJump)
        {
            attacking = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocidadSalto);
            jump = 0;
        }

        if(transform.position.y == posicionOriginal.y) rb.linearVelocity = new Vector2(0, 0);

        if (area != null && area.PlayerInArea())
        {
            if (timer >= descansoAtaques)
            {
                //activo el sonido de mono
                if (currentClip != attackSound)
                {
                    activateAttackSound();
                    currentClip = attackSound;
                }

                attacking = true;
                if (timerWave >= i * descansoProyectiles)
                {
                    if (i == numeroProyectiles)
                    {
                        i = 0;
                        timerWave = 0f;
                        timer = 0f;
                        jump += 1;
                    }
                    else
                    {
                        SummonWave();
                        i++;
                    }
                }

                timerWave = timerWave + Time.fixedDeltaTime;
            }
            else
            {
                //cambia el ruido de ataque a estándar
                if (currentClip != monkeySound)
                {
                    activateMonkeySound();
                    currentClip = monkeySound;
                }

                timer = timer + Time.fixedDeltaTime;
                attacking = false;
            }
        }
        else
        {
            //activo el sonido de mono
            if (currentClip != monkeySound)
            {
                activateMonkeySound();
                currentClip = monkeySound;
            }

            timerWave = 0f;
            timer = 0f;
            attacking = false;
        }
    }

    private void SummonWave()
    {
        Vector3 extra = new Vector3(3, 0, 0);
        Vector3 posder = transform.position + extra;
        Vector3 posizq = transform.position - extra;

        Instantiate(proyectilMonoDerecha, posder, Quaternion.identity);
        Instantiate(proyectilMonoIzquierda, posizq, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {

            //activo el sonido de golpe
            activateHitSound();

            //calculamos el vector para ver en que dirección viene el player y x donde hay q aplicar la fuercza
            Vector2 direction = collision.transform.position - transform.position;


            //mathf.sign lo q hace es que devuelve solo el signo del número
            //entonces si el jugador viene por la derecha saldra positivo (+1) y si viene por la izquierda saldra negativo (-1)
            float pushX = Mathf.Sign(direction.x);


            //aplicamos el retroceso al jugador
            player.ApplyKnockback(pushX * knockbackForceX, knockbackForceY);

        }
    }
    public bool IsAttacking()
    {
        return attacking;
    }

    private void activateMonkeySound()
    {
        audioSource.Stop();
        audioSource.clip = monkeySound;
        audioSource.volume = volumeMonkey;
        audioSource.loop = true;
        audioSource.Play();

    }

    private void activateAttackSound()
    {
        audioSource.Stop();
        audioSource.clip = attackSound;
        audioSource.volume = volumeAttack;
        audioSource.loop = true;
        audioSource.Play();

    }


    private void activateHitSound()
    {
        audioSource.Stop();
        audioSource.clip = hitSound;
        audioSource.volume = volumeHit;
        //el sonido de golpe solo lo reproduzco una vez porque no tiene sentido que sea un loop
        audioSource.loop = false;
        audioSource.Play();
        currentClip = hitSound;
    }
}